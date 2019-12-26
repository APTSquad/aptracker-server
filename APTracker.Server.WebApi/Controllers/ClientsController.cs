using System.Collections.Generic;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands;
using APTracker.Server.WebApi.Commands.Client.Create;
using APTracker.Server.WebApi.Commands.Client.Modify;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClientsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _context.Clients.CountAsync(x => x.Id == id) == 0) return NotFound();
            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] ClientCreateRequest request)
        {
            var bag = await _context.Bags.FirstOrDefaultAsync(b => b.Id == request.BagId);
            if (bag == null) return BadRequest();
            var ent = await _context.Clients.AddAsync(_mapper.Map<Client>(request));
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == ent.Entity.Id));
        }

        [HttpPost("setBag")]
        [ProducesResponseType(typeof(ClientCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetBag([FromBody] SetBagRequest request)
        {
            if (!request.BagId.HasValue) return BadRequest("BagId is required");
            var bag = await _context.Bags.FirstOrDefaultAsync(b => b.Id == request.BagId);
            if (bag == null) return BadRequest();
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (client == null)
                return NotFound("Client wasn't found");

            client.BagId = request.BagId.Value;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == client.Id));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ClientCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] ClientModifyRequest client)
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
        [ProducesResponseType(typeof(ICollection<ClientCreateResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Clients.ProjectTo<ClientCreateResponse>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }
    }
}