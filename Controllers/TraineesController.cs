using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Models;
using TraineeManagement.Models.DTOs;
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
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var trainee = _service.GetById(id);

            if (trainee == null)
                return NotFound(new { message = "Trainee not found" });

            return Ok(_service.GetResponseData(trainee));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTraineeRequest dto)
        {
            var newTrainee = _service.Create(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = newTrainee.Id },
                newTrainee);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateTraineeRequest dto)
        {
            var trainee = _service.Update(id, dto);

            if (trainee == null)
                return NotFound(new { message = "Trainee not found" });

            return Ok(_service.GetResponseData(trainee));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);

            if (!deleted)
                return NotFound(new { message = "Trainee not found" });

            return NoContent();
        }
    }
}