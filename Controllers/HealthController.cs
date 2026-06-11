using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraineeManagement.Controllers;
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new
        {
            status = "running",
            application = "Trainee Management API",
            timestamp = DateTime.UtcNow
        });
    }
}
