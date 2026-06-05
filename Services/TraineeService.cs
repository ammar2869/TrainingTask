using TraineeManagement.Models;
using TraineeManagement.Models.DTOs;

namespace TraineeManagement.Services
{
    public class TraineeService : ITraineeService
    {
        private static List<Trainee> trainees = new List<Trainee>
        {
            new Trainee
            {
                Id = 1,
                FirstName = "Ammar",
                LastName = "Karimi",
                Email = "ammarkarimi@gmail.com",
                TechStack = "Python",
                Status = "Active",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        };

        public List<Trainee> GetAll()
        {
            return trainees;
        }

        public Trainee? GetById(int id)
        {
            return trainees.FirstOrDefault(t => t.Id == id);
        }

        public Trainee Create(CreateTraineeRequest dto)
        {
            int newId = trainees.Any() ? trainees.Max(t => t.Id) + 1 : 1;

            Trainee trainee = new Trainee
            {
                Id = newId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                TechStack = dto.TechStack,
                Status = dto.Status,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            trainees.Add(trainee);
            return trainee;
        }

        public TraineeResponse GetResponseData(Trainee trainee)
        {
            TraineeResponse dto = new TraineeResponse();
            dto.FirstName = trainee.FirstName;
            dto.LastName = trainee.LastName;
            dto.Email = trainee.Email;
            dto.TechStack = trainee.TechStack;
            dto.Status = trainee.Status;
            dto.UpdatedDate = trainee.UpdatedDate;

            return dto;
        }

        public Trainee? Update(int id, UpdateTraineeRequest dto)
        {
            Trainee? trainee = trainees.FirstOrDefault(t => t.Id == id);

            if (trainee == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                trainee.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                trainee.LastName = dto.LastName;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                trainee.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.TechStack))
                trainee.TechStack = dto.TechStack;

            if (!string.IsNullOrWhiteSpace(dto.Status))
                trainee.Status = dto.Status;

            trainee.UpdatedDate = DateTime.UtcNow;

            return trainee;
        }

        public bool Delete(int id)
        {
            Trainee? trainee = trainees.FirstOrDefault(t => t.Id == id);

            if (trainee == null)
                return false;

            trainees.Remove(trainee);
            return true;
        }
    }
}