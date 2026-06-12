using TraineeManagement.Models.Entities;
using TraineeManagement.Models.DTOs.Mentor;
using TraineeManagement.Models.Enums;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Models.DTOs.Trainee;
using TraineeManagement.Models.DTOs.LearningTask;

namespace TraineeManagement.Services.LearningTasks
{
    public class LearningTaskService : ILearningTaskService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LearningTaskService> _logger;
        public LearningTaskService(AppDbContext context, ILogger<LearningTaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedResponse<LearningTask>> GetAll( string? search,LearningTaskStatus? status,int pageNumber = 1,int pageSize = 10)
        {
            IQueryable<LearningTask> query = _context.LearningTasks.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(t =>
                    t.Title.ToLower().Contains(search) ||
                    t.Description.ToLower().Contains(search) ||
                    t.ExpectedTechStack.ToLower().Contains(search)
                );
            }
            if (status.HasValue)
            {
                query = query.Where(t => t.Status==status.Value);
            }
            int totalRecords = await query.CountAsync();
            query = query.OrderBy(t => t.Title).ThenBy(t => t.Description);

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<LearningTask>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

    
        public async Task<LearningTask?> GetById(int id)
        {
            LearningTask? task = await _context.LearningTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                _logger.LogWarning("Task not found with Id: {TaskId}", id);
            }
            return task;
        }

        public async Task<LearningTask> Create(CreateLearningTaskDto dto)
        {
            int newId = _context.LearningTasks.Any() ? _context.LearningTasks.Max(t => t.Id) + 1 : 1;

            LearningTask task = new LearningTask
            {
                Id = newId,
                Title = dto.Title,
                Description = dto.Description,
                ExpectedTechStack = dto.ExpectedTechStack,
                DueDate = dto.DueDate,
                Status = dto.Status,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _logger.LogInformation(
                    "Task created with Id: {TaskId}, Title: {Title}, Status: {Status}",
                    task.Id,
                    task.Title,
                    task.Status
                );


            await _context.LearningTasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public LearningTaskResponseDto GetResponseData(LearningTask task)
        {
            LearningTaskResponseDto dto = new LearningTaskResponseDto
            {
                Title = task.Title,
                Description = task.Description,
                ExpectedTechStack = task.ExpectedTechStack,
                DueDate = task.DueDate,
                Status = task.Status,
            };

            return dto;
        }

        public async Task<LearningTask?> Update(int id, UpdateLearningTaskDto dto)
        {
            LearningTask? task = await _context.LearningTasks.FirstOrDefaultAsync(t => t.Id == id);
            
            if (task == null) 
            {
                _logger.LogWarning("Update failed - mentor not found with Id: {mentorId}", id);
                return null;
            }
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.ExpectedTechStack = dto.ExpectedTechStack;
            task.DueDate = dto.DueDate;
            task.Status = dto.Status;
            task.UpdatedDate = DateTime.UtcNow;

            
            _logger.LogInformation(
                    "Task updated with Id: {TaskId}, ExpectedTechStack: {Email}, Status: {Status}",
                    task.Id,
                    task.ExpectedTechStack,
                    task.Status
                );

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Delete(int id)
        {
            LearningTask? task = await _context.LearningTasks.FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                _logger.LogWarning("Delete failed - Task not found with Id: {TaskId}", id);
                return false;
            }

            _logger.LogInformation(
                    "Task deleted with Id: {TaskId}, Title: {Title}",
                    task.Id,task.Title);
    
            _context.LearningTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}