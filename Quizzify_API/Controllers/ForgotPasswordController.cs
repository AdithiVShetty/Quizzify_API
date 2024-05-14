using Microsoft.AspNetCore.Mvc;
using Quizzify_API.Requests;
using Quizzify_API;

namespace Quizzify_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserService _userService;

        public ForgotPasswordController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("sendotp")]
        public IActionResult SendOTP([FromBody] SendOtpRequest request)
        {
            try
            {
                _userService.SendOTP(request.Email);
                return Ok("OTP sent successfully");
            }
            catch (Exception ex)
            {
                return Ok($"Failed to send OTP: {ex.Message}");
            }
        }

        [HttpPost("verifyotp")]
        public IActionResult VerifyOTP([FromBody] VerifyOtpRequest request)
        {
            try
            {
                bool isVerified = _userService.VerifyOTP(request.Email, request.OTP);
                return Ok(new { isVerified });
            }
            catch (Exception ex)
            {
                return Ok($"Failed to verify OTP: {ex.Message}");
            }
        }

        [HttpPost("updatepassword")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            try
            {
                _userService.UpdatePassword(request.Email, request.NewPassword);
                return Ok("Password updated successfully");
            }
            catch (Exception ex)
            {
                return Ok($"Failed to update password: {ex.Message}");
            }
        }
    }
}