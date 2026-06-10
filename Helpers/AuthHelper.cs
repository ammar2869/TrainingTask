using TraineeManagement.Models.DTOs.User;
using TraineeManagement.Models.Entities;

namespace TraineeManagement.Helpers
{
    public static class AuthHelper
    {
        public static bool IsLoginRequestInvalid(LoginRequestDto request, out string message)
        {
            if (request == null)
            {
                message = "Request body is required";
                return true;
            }

            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                message = "Username and password are required";
                return true;
            }

            message = string.Empty;
            return false;
        }

        public static bool IsUserInvalid(User? user, out string message)
        {
            if (user == null)
            {
                message = "Invalid username or password";
                return true;
            }

            message = string.Empty;
            return false;
        }

        public static bool IsPasswordInvalid(string enteredPassword, string storedPasswordHash, out string message)
        {
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);

            if (!isPasswordValid)
            {
                message = "Invalid username or password";
                return true;
            }

            message = string.Empty;
            return false;
        }
    }
}