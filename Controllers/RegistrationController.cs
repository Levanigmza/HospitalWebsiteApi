using HospitalWebsiteApi.Models;
using HospitalWebsiteApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HospitalWebsiteApi;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

namespace HospitalWebsiteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly SqlService _userService;

        public RegistrationController(SqlService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserParameters user)
        {
            if (_userService.IsEmailRegistered(user.Email))
            {
                return BadRequest("Error: Email is already registered!");
            }
            else
                try
                {
                    int userId = await _userService.RegisterUserAsync(user);

                    var response = new RegistrationResponse
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email
                    };

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return BadRequest("Registration failed: " + ex.Message);
                }
        }


        [Authorize]
        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctorUser([FromBody] DoctorRegistrationRequest doctorRequest)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdAsync(userId);

            if (user.UserRole == 2)
            {
                if (_userService.IsEmailRegistered(doctorRequest.Doctor.Email))
                {
                    return BadRequest("Error: Email is already registered!");
                }
                else
                {
                    try
                    {
                        int newDoctorUserId = await _userService.RegisterDoctorAsync(doctorRequest.Doctor, doctorRequest.Position);

                        var response = new RegistrationResponse
                        {
                            Name = doctorRequest.Doctor.Name,
                            Surname = doctorRequest.Doctor.Surname,
                            Email = doctorRequest.Doctor.Email
                        };

                        return Ok(response);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Registration failed: " + ex.Message);
                    }
                }
            }
            else
            {
                return Forbid();
            }
        }



    }

}


