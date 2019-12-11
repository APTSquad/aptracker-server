using System.Linq;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.ViewModels;
using AutoMapper.QueryableExtensions;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_context.Users.OrderBy(x => x.Id));
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserPutViewModel resource)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == resource.Id);
            if (user != null)
            {
                user.Name = resource.Name;
                user.Rate = resource.Rate;
                var entry = _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok(entry.Entity);
            }

            return NotFound();
        }
    }
}