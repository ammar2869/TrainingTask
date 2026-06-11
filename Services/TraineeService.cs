using TraineeManagement.Models.Entities;
using TraineeManagement.Models.DTOs.Trainee;
using TraineeManagement.Models.Enums;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace TraineeManagement.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TraineeService> _logger;
        public TraineeService(AppDbContext context, ILogger<TraineeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedResponse<Trainee>> GetAll( string? search,TraineeStatus? status,int pageNumber = 1,int pageSize = 10)
        {
            IQueryable<Trainee> query = _context.Trainees.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(t =>
                    t.FirstName.ToLower().Contains(search) ||
                    t.LastName.ToLower().Contains(search) ||
                    t.Email.ToLower().Contains(search) ||
                    t.TechStack.ToLower().Contains(search)
                );
            }
            if (status.HasValue)
            {
                query = query.Where(t => t.Status==status.Value);
            }
            int totalRecords = await query.CountAsync();
            query = query.OrderBy(t => t.FirstName).ThenBy(t => t.LastName);

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Trainee>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

    
        public async Task<Trainee?> GetById(int id)
        {
            Trainee? trainee = await _context.Trainees.FirstOrDefaultAsync(t => t.Id == id);
            if (trainee == null)
            {
                _logger.LogWarning("Trainee not found with Id: {TraineeId}", id);
            }
            return trainee;
        }

        public async Task<Trainee> Create(CreateTraineeRequest dto)
        {
            int newId = _context.Trainees.Any() ? _context.Trainees.Max(t => t.Id) + 1 : 1;

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

            _logger.LogInformation(
                    "Trainee created with Id: {TraineeId}, Email: {Email}, Status: {Status}",
                    trainee.Id,
                    trainee.Email,
                    trainee.Status
                );


            await _context.Trainees.AddAsync(trainee);
            await _context.SaveChangesAsync();
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

        public async Task<Trainee?> Update(int id, UpdateTraineeRequest dto)
        {
            Trainee? trainee = await _context.Trainees.FirstOrDefaultAsync(t => t.Id == id);
            
            if (trainee == null) 
            {
                _logger.LogWarning("Update failed - Trainee not found with Id: {TraineeId}", id);
                return null;
            }
            trainee.FirstName = dto.FirstName;
            trainee.LastName = dto.LastName;
            trainee.Email = dto.Email;
            trainee.TechStack = dto.TechStack;
            trainee.Status = dto.Status;
            trainee.UpdatedDate = DateTime.UtcNow;

            
            _logger.LogInformation(
                    "Trainee updated with Id: {TraineeId}, Email: {Email}, Status: {Status}",
                    trainee.Id,
                    trainee.Email,
                    trainee.Status
                );

            await _context.SaveChangesAsync();
            return trainee;
        }

        public async Task<bool> Delete(int id)
        {
            Trainee? trainee = await _context.Trainees.FirstOrDefaultAsync(t => t.Id == id);

            if (trainee == null)
            {
                _logger.LogWarning("Delete failed - Trainee not found with Id: {TraineeId}", id);
                return false;
            }

            _logger.LogInformation(
                    "Trainee deleted with Id: {TraineeId}, Email: {Email}",
                    trainee.Id,trainee.Email);
    
            _context.Trainees.Remove(trainee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}