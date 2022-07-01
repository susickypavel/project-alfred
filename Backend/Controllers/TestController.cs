using Domain.Context;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly ProjectAlfredContext _context;

    public TestController(ILogger<TestController> logger, ProjectAlfredContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "test")]
    public SongRecord Get()
    {
        _logger.LogInformation("Fetch the First SongRecord");
        return _context.Songs.First();
    }
}