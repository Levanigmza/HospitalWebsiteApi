using HospitalWebsiteApi.Models;
using HospitalWebsiteApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HospitalWebsiteApi;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace HospitalWebsiteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly SqlService _userService;
        private readonly JwtService _jwtService;


        public AuthorizationController(SqlService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody] LoginModel login)
        {
            
            var user = await _userService.FindByEmailAsync(login.Email);

            if (user != null)
            {
                var storedPasswordHash = user.Password;

                if (user.UserRole == 2)
                {
                    if (_userService.VerifyPassword(login.Password, storedPasswordHash))
                    {

                        var token = _jwtService.GenerateJwtToken(user.Email, user.Id);
                        string userRole = GetUserRole(user.UserRole);
                        return Ok(new { Token = token  , UserRole = userRole});
                    }
                    else
                    {
                        return BadRequest("Invalid Admin's email or password");
                    }
                }
                else
                {
                    if (_userService.VerifyPassword(login.Password, storedPasswordHash))
                    {
                        var token = _jwtService.GenerateJwtToken(user.Email, user.Id);
                        string userRole = GetUserRole(user.UserRole);
                        return Ok(new { Token = token, UserRole = userRole });
                    }
                    else
                    {
                        return BadRequest("Invalid email or password");
                    }
                }
            }
            else
            {
                return BadRequest("Invalid email or password");
            }
        }
        private string GetUserRole(int userRole)
        {
            switch (userRole)
            {
                case 0:
                    return "User";
                case 1:
                    return "Doctor";
                case 2:
                    return "Admin";
                default:
                    return "Unknown";
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }


            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }
            if (user.UserRole == 1)
            {
                var DoctorResponse = new

                {
                    
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,

                };

                return Ok(DoctorResponse);

            }
            if (user.UserRole == 2)
            {
                var AdminResponse = new
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,

                };

                return Ok(AdminResponse);
            }
            else
            {

                var userResponse = new
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Birthdate = user.Birthdate?.ToString("yyyy-MM-dd"),
                    PersonalID = user.PersonalID,
                    Address = user.Address
                };

                return Ok(userResponse);
            }
        }


        [Authorize]
        [HttpPut("user/update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserParameters userUpdate)
        {
            if (userUpdate == null)
            {
                return BadRequest("Invalid user update data.");
            }

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            try
            {
                if (userUpdate.Name != null)
                {
                    await _userService.UpdateUserNameAsync(userId, userUpdate.Name);
                }
                if (userUpdate.PersonalID != null)
                {
                    await _userService.UpdateUserPersonalIdAsync(userId, (int)userUpdate.PersonalID);
                }
                if (userUpdate.Surname != null)
                {
                    await _userService.UpdateSurnameAsync(userId, userUpdate.Surname);
                }
                if(userUpdate.Phone != null)
                {
                    await _userService.UpdateUserPhoneAsync(userId , (int)userUpdate.Phone);
                }
                if (userUpdate.Birthdate != null)
                {
                    await _userService.UpdateUserBirthdateAsync(userId, userUpdate.Birthdate);
                }
                if (userUpdate.Address != null)
                {
                    await _userService.UpdateUserAddressAsync(userId, userUpdate.Address);
                }


                return Ok("User data has been updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }









    }


}

