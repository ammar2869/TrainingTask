using TraineeManagement.Models;
using TraineeManagement.Models.DTOs;

namespace TraineeManagement.Services
{
    public interface ITraineeService
    {
        List<Trainee> GetAll();
        Trainee? GetById(int id);
        Trainee Create(CreateTraineeRequest dto);
        Trainee? Update(int id, UpdateTraineeRequest dto);
        bool Delete(int id);
        TraineeResponse GetResponseData(Trainee trainee);
    }
}
