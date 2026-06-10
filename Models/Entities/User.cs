using System.ComponentModel.DataAnnotations;
using TraineeManagement.Models.Enums;

namespace TraineeManagement.Models.Entities
{
    public class User
    {
        public int Id {get; set;}
        [Required]
        [MaxLength(50)]
        public string Username {get; set;} = string.Empty;
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email {get; set;} = string.Empty;
        [Required]
        public string PasswordHash {get; set;} = string.Empty;
        [Required]
        public UserRole  Role {get; set;} = UserRole.Trainee;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;


        
    }
}