using Inventory.API.Helpers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly DbLogger _dbLogger;

    public HomeController(DbLogger dbLogger)
    {
        _dbLogger = dbLogger;
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
        try
        {
            string msg = "API Home endpoint called successfully";
            _dbLogger.LogToDb(msg); // log to DB only what you pass
            return Ok(new { message = "Logged to DB!" });
        }
        catch (Exception ex)
        {
            _dbLogger.LogToDb(ex.Message, "Error"); // log exception if occurs
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("test")]
    public IActionResult TestAction()
    {
        try
        {
            string msg = "API TestAction called";
            _dbLogger.LogToDb(msg);
            return Ok(new { message = "TestAction logged to DB!" });
        }
        catch (Exception ex)
        {
            _dbLogger.LogToDb(ex.Message, "Error");
            return StatusCode(500, new { error = ex.Message });
        }
    }
}