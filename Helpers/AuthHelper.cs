using Microsoft.Extensions.Logging;
using TraineeManagement.Models.DTOs.User;
using TraineeManagement.Models.Entities;

namespace TraineeManagement.Helpers
{
    public static class AuthHelper
    {
        private static readonly ILogger _logger =
            LoggerFactory.Create(builder => builder.AddDebug().AddConsole())
                         .CreateLogger("AuthHelper");

        public static bool IsLoginRequestInvalid(LoginRequestDto request)
        {
            if (request == null)
            {
                _logger.LogWarning("Request is null");
                return true;
            }

            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                _logger.LogWarning("Username or Password missing");
                return true;
            }

            _logger.LogInformation("Login request valid for username: {Username}", request.Username);
            return false;
        }

        public static bool IsUserInvalid(User? user)
        {
            if (user == null)
            {
                _logger.LogWarning("User not found");
                return true;
            }

            _logger.LogInformation("User found: {Username}", user.Username);
            return false;
        }

        public static bool IsPasswordInvalid(string enteredPassword, string storedPasswordHash)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);

            if (!isValid)
            {
                _logger.LogWarning("Invalid password attempt");
                return true;
            }

            _logger.LogInformation("Password verified successfully");
            return false;
        }
    }
}