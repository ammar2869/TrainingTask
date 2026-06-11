using System.Runtime.CompilerServices;
using TraineeManagement.Models.Entities;
using TraineeManagement.Models.Enums;
using TraineeManagement.Models.DTOs.Trainee;

namespace TraineeManagement.Services
{
    public interface ITraineeService
    {
        // Task<List<Trainee>> GetAll();
        Task<PagedResponse<Trainee>> GetAll(string? search = null,TraineeStatus? status = null,int pageNumber = 1,int pageSize = 10);
        Task<Trainee?> GetById(int id);
        Task<Trainee> Create(CreateTraineeRequest dto);
        Task<Trainee?> Update(int id, UpdateTraineeRequest dto);
        Task<bool> Delete(int id);
        TraineeResponse GetResponseData(Trainee trainee);
    }
}
