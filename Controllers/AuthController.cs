using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Data;
using TraineeManagement.Models.DTOs;
using TraineeManagement.Services;
using TraineeManagement.Models.DTOs.User;
using TraineeManagement.Models.Entities;

namespace TraineeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context,IConfiguration configuration,JwtService jwtService)
        {
            _context = context;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message = "Username and password are required"
                });
            }

            User? user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash
                );

            if (!isPasswordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }

            string token = _jwtService.GenerateToken(user);

            double expiryMinutes = Convert.ToDouble(
                _configuration["Jwt:ExpiryMinutes"]
            );

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