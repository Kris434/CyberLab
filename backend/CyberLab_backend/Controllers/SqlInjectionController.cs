using Application.Scenarios.SqlInjection;
using Application.Scenarios.SqlInjection.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CyberLab_backend.Controllers;

[ApiController]
[Route("api/sqli")]
public class SqlInjectionController : ControllerBase
{
    private readonly SqlInjectionService _service;
    private readonly IHostEnvironment _environment;

    public SqlInjectionController(
        SqlInjectionService service,
        IHostEnvironment environment)
    {
        _service = service;
        _environment = environment;
    }

    [HttpGet("secure")]
    public async Task<ActionResult<SqlInjectionResultDto>> SearchSecure(
        [FromQuery] string search = "")
    {
        if (!_environment.IsDevelopment())
            return NotFound();

        if (search.Length > 100)
            return BadRequest("Search phrase is too long.");

        return Ok(await _service.SearchSecureAsync(search));
    }

    [HttpGet("vulnerable")]
    public async Task<ActionResult<SqlInjectionResultDto>> SearchVulnerable(
        [FromQuery] string search = "")
    {
        if (!_environment.IsDevelopment())
            return NotFound();

        if (search.Length > 100)
            return BadRequest("Search phrase is too long.");

        return Ok(await _service.SearchVulnerableAsync(search));
    }
}