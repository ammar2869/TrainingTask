using TraineeManagement.Models.Enums;

namespace TraineeManagement.Models.Entities
{
    public class Trainee
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string TechStack { get; set; }
        public TraineeStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}