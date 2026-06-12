using TraineeManagement.Models.Entities;
using TraineeManagement.Models.DTOs.Mentor;
using TraineeManagement.Models.Enums;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.Models.DTOs.Trainee;

namespace TraineeManagement.Services
{
    public class MentorService : IMentorService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MentorService> _logger;
        public MentorService(AppDbContext context, ILogger<MentorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedResponse<Mentor>> GetAll( string? search,MentorStatus? status,int pageNumber = 1,int pageSize = 10)
        {
            IQueryable<Mentor> query = _context.Mentors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(t =>
                    t.FirstName.ToLower().Contains(search) ||
                    t.LastName.ToLower().Contains(search) ||
                    t.Email.ToLower().Contains(search) ||
                    t.Expertise.ToLower().Contains(search)
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

            return new PagedResponse<Mentor>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = data
            };
        }

    
        public async Task<Mentor?> GetById(int id)
        {
            Mentor? mentor = await _context.Mentors.FirstOrDefaultAsync(t => t.Id == id);
            if (mentor == null)
            {
                _logger.LogWarning("Mentor not found with Id: {MentorId}", id);
            }
            return mentor;
        }

        public async Task<Mentor> Create(CreateMentorDto dto)
        {
            int newId = _context.Mentors.Any() ? _context.Mentors.Max(t => t.Id) + 1 : 1;

            Mentor mentor = new Mentor
            {
                Id = newId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Expertise = dto.Expertise,
                Status = dto.Status,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _logger.LogInformation(
                    "Mentor created with Id: {MentorId}, Email: {Email}, Status: {Status}",
                    mentor.Id,
                    mentor.Email,
                    mentor.Status
                );


            await _context.Mentors.AddAsync(mentor);
            await _context.SaveChangesAsync();
            return mentor;
        }

        public MentorResponse GetResponseData(Mentor mentor)
        {
            MentorResponse dto = new MentorResponse();
            dto.FirstName = mentor.FirstName;
            dto.LastName = mentor.LastName;
            dto.Email = mentor.Email;
            dto.Expertise = mentor.Expertise;
            dto.Status = mentor.Status;
            dto.UpdatedDate = mentor.UpdatedDate;

            return dto;
        }

        public async Task<Mentor?> Update(int id, UpdateMentorDto dto)
        {
            Mentor? mentor = await _context.Mentors.FirstOrDefaultAsync(t => t.Id == id);
            
            if (mentor == null) 
            {
                _logger.LogWarning("Update failed - mentor not found with Id: {mentorId}", id);
                return null;
            }
            mentor.FirstName = dto.FirstName;
            mentor.LastName = dto.LastName;
            mentor.Email = dto.Email;
            mentor.Expertise = dto.Expertise;
            mentor.Status = dto.Status;
            mentor.UpdatedDate = DateTime.UtcNow;

            
            _logger.LogInformation(
                    "mentor updated with Id: {mentorId}, Email: {Email}, Status: {Status}",
                    mentor.Id,
                    mentor.Email,
                    mentor.Status
                );

            await _context.SaveChangesAsync();
            return mentor;
        }

        public async Task<bool> Delete(int id)
        {
            Mentor? mentor = await _context.Mentors.FirstOrDefaultAsync(t => t.Id == id);

            if (mentor == null)
            {
                _logger.LogWarning("Delete failed - Mentor not found with Id: {MentorId}", id);
                return false;
            }

            _logger.LogInformation(
                    "Mentor deleted with Id: {MentorId}, Email: {Email}",
                    mentor.Id,mentor.Email);
    
            _context.Mentors.Remove(mentor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}