using cmmsClone.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cmmsClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestConnectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestConnectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Try to open connection and execute a simple query
                var canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Database connection successful!",
                        database = _context.Database.GetDbConnection().Database
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = "Cannot connect to database"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Database connection failed",
                    error = ex.Message
                });
            }
        }
    }
}
