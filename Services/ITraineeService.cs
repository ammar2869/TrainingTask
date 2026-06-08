using System.Runtime.CompilerServices;
using TraineeManagement.Models;
using TraineeManagement.Models.DTOs;

namespace TraineeManagement.Services
{
    public interface ITraineeService
    {
        // Task<List<Trainee>> GetAll();
        Task<List<Trainee>> GetAll(string? search = null);
        Task<Trainee?> GetById(int id);
        Task<Trainee> Create(CreateTraineeRequest dto);
        Task<Trainee?> Update(int id, UpdateTraineeRequest dto);
        Task<bool> Delete(int id);
        TraineeResponse GetResponseData(Trainee trainee);
    }
}
