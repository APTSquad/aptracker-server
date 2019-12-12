using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels;
using APTracker.Server.WebApi.ViewModels.Commands.User;
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

/*        [HttpPost("setBags")]
        public async Task<IActionResult> SetBags([FromBody] UserSetBagsCommand resource)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == resource.Id);
            if (user == null) return NotFound();
            
            var bags = new List<Bag>();
            foreach (var bagId in resource.Bags)
            {
                var bag = await _context.Bags.FirstOrDefaultAsync(x => x.Id == bagId);
                if (bag == null || bag.Responsible != null)
                    return BadRequest();
                bag.Responsible = user;
                bags.Add(bag);
            }

            _context.Bags.UpdateRange(bags);
            await _context.SaveChangesAsync();
            return Ok();
        }*/

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserModifyCommand resource)
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