using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TraineeManagement.Models;
using TraineeManagement.Models.Enums;

namespace TraineeManagement.Models.Entities
{
    public class LearningTask
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ExpectedTechStack { get; set; }
        public required DateTime DueDate { get; set; }
        public LearningTaskStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}