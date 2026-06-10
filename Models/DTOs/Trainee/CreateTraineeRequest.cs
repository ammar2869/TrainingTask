using System.ComponentModel.DataAnnotations;
using TraineeManagement.Models;
using TraineeManagement.Models.Enums;

namespace TraineeManagement.Models.DTOs.Trainee
{
    public class CreateTraineeRequest
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string TechStack { get; set; }

        [Required]
        [EnumDataType(typeof(TraineeStatus))]
        public required TraineeStatus Status { get; set; }
    }
}