using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Models.Entities;
using TraineeManagement.Models.DTOs.Trainee;
using TraineeManagement.Models.DTOs.LearningTask;
using TraineeManagement.Models.DTOs.Mentor;
using TraineeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using TraineeManagement.Models.Enums;
using TraineeManagement.Services.LearningTasks;
namespace TraineeManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LearningTaskController : ControllerBase
    {
        private readonly ILearningTaskService _service;

        public LearningTaskController(ILearningTaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search,[FromQuery] LearningTaskStatus? status,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAll(search, status, pageNumber, pageSize);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            LearningTask? task = await _service.GetById(id);

            if (task == null)
                return NotFound(new { message = "Task not found" });

            return Ok(_service.GetResponseData(task));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLearningTaskDto dto)
        {
            
            LearningTask newTask = await _service.Create(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = newTask.Id },
                newTask);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLearningTaskDto dto)
        {
            LearningTask? task = await _service.Update(id, dto);

            if (task == null)
                return NotFound(new { message = "Task not found" });

            return Ok(_service.GetResponseData(task));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound(new { message = "Task not found" });

            return NoContent();
        }
    }
}