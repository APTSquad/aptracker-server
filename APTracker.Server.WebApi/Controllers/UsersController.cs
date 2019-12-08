using System.Threading.Tasks;
using APTracker.Server.WebApi.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Users.ToArrayAsync());
        }
    }
}