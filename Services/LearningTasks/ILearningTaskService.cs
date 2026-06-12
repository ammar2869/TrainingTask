using TraineeManagement.Models.Enums;
using TraineeManagement.Models.DTOs.Trainee;
using TraineeManagement.Models.DTOs.Mentor;
using TraineeManagement.Models.Entities;
using TraineeManagement.Models.DTOs.LearningTask;

namespace TraineeManagement.Services.LearningTasks
{
    public interface ILearningTaskService
    {
        // Task<List<Trainee>> GetAll();
        Task<PagedResponse<LearningTask>> GetAll(string? search = null,LearningTaskStatus? status = null,int pageNumber = 1,int pageSize = 10);
        Task<LearningTask?> GetById(int id);
        Task<LearningTask> Create(CreateLearningTaskDto dto);
        Task<LearningTask?> Update(int id, UpdateLearningTaskDto dto);
        Task<bool> Delete(int id);
        LearningTaskResponseDto GetResponseData(LearningTask trainee);
    }
}
