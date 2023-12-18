using HospitalWebsiteApi.Models;
using HospitalWebsiteApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HospitalWebsiteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly SqlService _appointmentService;
        private readonly ApplicationDbContext _context;

        public AppointmentController(SqlService appointmentService, ApplicationDbContext context)
        {
            _appointmentService = appointmentService;
            _context = context;
        }

        [Authorize]
        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] Appointment appointment)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }
            if (await _appointmentService.IsDoctorAppointmentSlotAvailableAsync(appointment.DoctorId, appointment.Date, appointment.Time))
            {
                if (await _appointmentService.IsUserAppointmentSlotAvailableAsync(appointment.UserId, appointment.Date, appointment.Time))
                {
                    try
                    {
                        int appointmentId = await _appointmentService.ScheduleAppointmentAsync(appointment);

                        var response = new AppointmentResponse
                        {
                            AppointmentId = appointmentId,
                            UserId = appointment.UserId,
                            DoctorId = appointment.DoctorId,
                            Date = appointment.Date,
                            Time = appointment.Time,
                            Comment = appointment.Comment
                        };

                        return Ok(response);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Appointment scheduling failed: " + ex.Message);
                    }
                }
                else
                {
                    return BadRequest("User have another Appointment on this Date and time. Please choose another date or time.");
                }
            }
            else
            {
                return BadRequest("Doctor's Appointment slot not available. Please choose another date or time.");
            }
        }


        [HttpGet("available-doctors")]
        public async Task<ActionResult<IEnumerable<DoctorInfo>>> GetAvailableDoctors(DateTime date, string time)
        {
            try
            {
                var availableDoctors = await _context.UserParameters
                    .Where(doctor => doctor.UserRole == 1)  
                    .Where(doctor => !_context.Appointments
                        .Any(appointment => appointment.DoctorId == doctor.Id.ToString() && appointment.Date == date.ToString("MM/dd/yyyy") && appointment.Time == time))
                    .Select(doctor => new DoctorInfo
                    {
                        Id = doctor.Id,
                        Name = doctor.Name,
                        Surname = doctor.Surname,
                        Position = _context.Doctor_Roles
                            .Where(role => role.DoctorID == doctor.Id.ToString())
                            .Select(role => role.Position)
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                return Ok(availableDoctors);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [Authorize]
        [HttpGet("user-appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetUserAppointments()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            try
            {
                var userAppointments = await _context.Appointments
                    .Where(appointment => appointment.UserId == userId.ToString())
                    .Select(appointment => new AppointmentResponse
                    {
                        AppointmentId = appointment.Id,
                        UserId = appointment.UserId,
                        DoctorId = appointment.DoctorId,
                        Date = appointment.Date,
                        Time = appointment.Time,
                        Comment = appointment.Comment,
                        DoctorInfo = _context.UserParameters
                         .Where(doctor => doctor.Id.ToString() == appointment.DoctorId)
                            .Select(doctor => new DoctorInfo
                            {
                                Id = doctor.Id,
                                Name = doctor.Name,
                                Surname = doctor.Surname,
                                Position = _context.Doctor_Roles
                                    .Where(role => role.DoctorID == doctor.Id.ToString())
                                    .Select(role => role.Position)
                                    .FirstOrDefault()
                            })
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                return Ok(userAppointments);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


    }

}
