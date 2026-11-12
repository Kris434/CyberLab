using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CyberLab_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            Status = "Healthy",
            Application = "CyberLab Backend"
        });
    }
    
    
    [HttpGet("database")]
    public async Task<IActionResult> GetDatabaseHealth(
        [FromServices] CyberLabDbContext dbContext)
    {
        bool connected = await dbContext.Database.CanConnectAsync();

        if (!connected)
        {
            return StatusCode(503, new
            {
                Status = "Unhealthy",
                Database = "PostgreSQL"
            });
        }

        return Ok(new
        {
            Status = "Healthy",
            Database = "PostgreSQL"
        });
    }

}