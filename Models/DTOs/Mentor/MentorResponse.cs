using System.ComponentModel.DataAnnotations;
using TraineeManagement.Models;
using TraineeManagement.Models.Enums;

namespace  TraineeManagement.Models.DTOs.Mentor
{
    public class MentorResponse
    {
        [Required(ErrorMessage ="First Name is required")]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is required")]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Email Address is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Expertise { get; set; }
        [Required]
        // [AllowedValues("Available","Do not disturb","Busy","Appear away",ErrorMessage = "Status must be from the given values Available, Do not disturb, Busy, Appear away ")]
        public MentorStatus Status { get; set; }
        public DateTime UpdatedDate { get; set; }


    }    
}