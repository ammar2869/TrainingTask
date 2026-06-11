using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Models.Entities;
using TraineeManagement.Models.DTOs.Trainee;
using TraineeManagement.Models.DTOs.Mentor;
using TraineeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using TraineeManagement.Models.Enums;
using TraineeManagement.Services;
namespace TraineeManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService _service;

        public MentorController(IMentorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search,[FromQuery] MentorStatus? status,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAll(search, status, pageNumber, pageSize);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Mentor? mentor = await _service.GetById(id);

            if (mentor == null)
                return NotFound(new { message = "Mentor not found" });

            return Ok(_service.GetResponseData(mentor));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMentorDto dto)
        {
            Mentor newMentor = await _service.Create(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = newMentor.Id },
                newMentor);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMentorDto dto)
        {
            Mentor? mentor = await _service.Update(id, dto);

            if (mentor == null)
                return NotFound(new { message = "Mentor not found" });

            return Ok(_service.GetResponseData(mentor));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound(new { message = "Mentor not found" });

            return NoContent();
        }
    }
}