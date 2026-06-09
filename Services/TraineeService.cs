using TraineeManagement.Models;
using TraineeManagement.Models.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace TraineeManagement.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly AppDbContext _context;
        public TraineeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Trainee>> GetAll(string? search = null, int pageNumber = 2, int pageSize = 10, bool ascending = true)
        {
            IQueryable<Trainee> query = _context.Trainees.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(t =>
                        t.FirstName.ToLower().Contains(search) ||
                        t.LastName.ToLower().Contains(search) ||
                        t.Email.ToLower().Contains(search) ||
                        t.TechStack.ToLower().Contains(search));
            }
            query = query
                    .OrderBy(t => t.FirstName)
                    .ThenBy(t => t.LastName);

            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

    
        public async Task<Trainee?> GetById(int id)
        {
            return await _context.Trainees.FirstOrDefaultAsync(t => t.Id == id);
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
            
            if (trainee == null) return null;

            trainee.FirstName = dto.FirstName;
            trainee.LastName = dto.LastName;
            trainee.Email = dto.Email;
            trainee.TechStack = dto.TechStack;
            trainee.Status = dto.Status;

            trainee.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return trainee;
        }

        public async Task<bool> Delete(int id)
        {
            Trainee? trainee = await _context.Trainees.FirstOrDefaultAsync(t => t.Id == id);

            if (trainee == null)
                return false;

            _context.Trainees.Remove(trainee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}