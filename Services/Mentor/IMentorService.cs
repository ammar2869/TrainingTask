using TraineeManagement.Models.Enums;
using TraineeManagement.Models.DTOs.Trainee;
using TraineeManagement.Models.DTOs.Mentor;
using TraineeManagement.Models.Entities;

namespace TraineeManagement.Services
{
    public interface IMentorService
    {
        // Task<List<Trainee>> GetAll();
        Task<PagedResponse<Mentor>> GetAll(string? search = null,MentorStatus? status = null,int pageNumber = 1,int pageSize = 10);
        Task<Mentor?> GetById(int id);
        Task<Mentor> Create(CreateMentorDto dto);
        Task<Mentor?> Update(int id, UpdateMentorDto dto);
        Task<bool> Delete(int id);
        MentorResponse GetResponseData(Mentor trainee);
    }
}
