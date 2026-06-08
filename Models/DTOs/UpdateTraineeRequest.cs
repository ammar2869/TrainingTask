using System.ComponentModel.DataAnnotations;
using TraineeManagement.Models;

namespace  TraineeManagement.Models.DTOs
{
    public class UpdateTraineeRequest
    {
        [Required(ErrorMessage ="First Name is required")]
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is required")]
        [MaxLength(50)]
        public required string LastName { get; set; }
        [Required(ErrorMessage ="Email Address is required")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string TechStack { get; set; }
        [Required]
        // [AllowedValues("Available","Do not disturb","Busy","Appear away",ErrorMessage = "Status must be from the given values Available, Do not disturb, Busy, Appear away ")]
        public TraineeStatus Status { get; set; }

    }    
}