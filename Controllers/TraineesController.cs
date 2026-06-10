using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Models.Entities;
using TraineeManagement.Models.DTOs.Trainee;
using TraineeManagement.Services;

namespace TraineeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraineesController : ControllerBase
    {
        private readonly ITraineeService _service;

        public TraineesController(ITraineeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, int pageNumber, int pageSize, bool ascending)
        {
            List<Trainee> trainees = await _service.GetAll(search,pageNumber,pageSize,ascending);

            return Ok(trainees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Trainee? trainee = await _service.GetById(id);

            if (trainee == null)
                return NotFound(new { message = "Trainee not found" });

            return Ok(_service.GetResponseData(trainee));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTraineeRequest dto)
        {
            Trainee newTrainee = await _service.Create(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = newTrainee.Id },
                newTrainee);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTraineeRequest dto)
        {
            Trainee? trainee = await _service.Update(id, dto);

            if (trainee == null)
                return NotFound(new { message = "Trainee not found" });

            return Ok(_service.GetResponseData(trainee));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound(new { message = "Trainee not found" });

            return NoContent();
        }
    }
}