using System.ComponentModel.DataAnnotations;
using TraineeManagement.Models;
using TraineeManagement.Models.Enums;

namespace TraineeManagement.Models.DTOs.LearningTask
{
    public class LearningTaskResponseDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500)]
        public required string Description { get; set; }
        public required string ExpectedTechStack { get; set; }
        public required DateTime DueDate { get; set; }
        public LearningTaskStatus Status { get; set; }
    }
}