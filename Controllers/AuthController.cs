using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Data;
using TraineeManagement.Models.DTOs;
using TraineeManagement.Services;
using TraineeManagement.Models.DTOs.User;
using TraineeManagement.Models.Entities;
using TraineeManagement.Helpers;

namespace TraineeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, IConfiguration configuration, JwtService jwtService)
        {
            _context = context;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (AuthHelper.IsLoginRequestInvalid(request, out string validationMessage))
            {
                return BadRequest(new
                {
                    message = validationMessage
                });
            }

            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (AuthHelper.IsUserInvalid(user, out string userMessage))
            {
                return Unauthorized(new
                {
                    message = userMessage
                });
            }

            if (AuthHelper.IsPasswordInvalid(request.Password, user!.PasswordHash, out string passwordMessage))
            {
                return Unauthorized(new
                {
                    message = passwordMessage
                });
            }

            string token = _jwtService.GenerateToken(user);

            double expiryMinutes = Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]);

            return Ok(new LoginResponseDto
            {
                Token = token,
                ExpiresIn = (int)(expiryMinutes * 60),
                User = new
                {
                    user.Id,
                    user.Username,
                    Role = user.Role.ToString()
                }
            });
        }
    }
}