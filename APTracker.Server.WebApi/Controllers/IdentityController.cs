using System.Threading.Tasks;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class IdentityController : Controller
    {
        private readonly AppDbContext _context;

        public IdentityController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserEmail()
        {
            return User.FindFirst("preferred_username")?.Value;
        }

        private string GetUserName()
        {
            return User.FindFirst("name")?.Value;
        }


        // GET
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var email = GetUserEmail();
            var name = GetUserName();
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (foundUser != null)
            {
                if (name != foundUser.Name) foundUser.Name = name;
                return Ok(foundUser);
            }

            var userEntityEntry =
                await _context.Users.AddAsync(new User {Email = email, Role = Role.Admin, Name = name});
            await _context.SaveChangesAsync();
            return Ok(userEntityEntry.Entity);
        }
    }
}