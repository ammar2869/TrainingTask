namespace TraineeManagement.Models.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public int ExpiresIn { get; set; }

        public object User { get; set; } = default!;
    }
}