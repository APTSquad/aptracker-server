using System.Threading.Tasks;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels;
using APTracker.Server.WebApi.ViewModels.Commands.Client.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Client.Modify;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClientsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientCreateCommand command)
        {
            var bag = await _context.Bags.FirstOrDefaultAsync(b => b.Id == command.BagId);
            if (bag == null) return BadRequest();
            var ent = await _context.Clients.AddAsync(_mapper.Map<Client>(command));
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == ent.Entity.Id));
        }
        
        [HttpPost("setBag")]
        public async Task<IActionResult> SetBag([FromBody] SetBagCommand command)
        {
            if (!command.BagId.HasValue)
            {
                return BadRequest("BagId is required");
            }
            var bag = await _context.Bags.FirstOrDefaultAsync(b => b.Id == command.BagId);
            if (bag == null) return BadRequest();
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (client == null)
                return NotFound("Client wasn't found");

            client.BagId = command.BagId.Value;
            
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == client.Id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClientModifyCommand client)
        {
            var foundClient = await _context.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);
            if (foundClient == null) return NotFound("Client wasn't found");

            foundClient.Name = client.Name;

            _context.Clients.Update(foundClient);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == client.Id));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }
    }
}