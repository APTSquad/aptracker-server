using System.Collections.Generic;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands;
using APTracker.Server.WebApi.Commands.Project.Create;
using APTracker.Server.WebApi.Commands.Project.GetAll;
using APTracker.Server.WebApi.Commands.Project.Modify;
using APTracker.Server.WebApi.Commands.Transfer;
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
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ProjectGetAllResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Projects.ProjectTo<ProjectGetAllResponse>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectGetAllResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _context.Projects.CountAsync(x => x.Id == id) == 0) return NotFound();
            return Ok(await _context.Projects.ProjectTo<ProjectGetAllResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjectCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] ProjectCreateRequest request)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(b => b.Id == request.ClientId);
            if (client == null) return BadRequest();
            var ent = await _context.Projects.AddAsync(_mapper.Map<Project>(request));
            await _context.SaveChangesAsync();

            return Ok(await _context.Projects.ProjectTo<ProjectCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == ent.Entity.Id));
        }
        
        [HttpPost("transfer")]
        //[ProducesResponseType(typeof(ProjectCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> TransferProject([FromBody] TransferRequest request)
        {
            var foundProject = await _context.Projects.FirstOrDefaultAsync(c => c.Id == request.ItemId);
            if (foundProject == null) return NotFound("Project not found");

            var existsClient = await _context.Clients.AnyAsync(c => c.Id == request.DestinationId);
            if (!existsClient) return NotFound("Client not exists");

            foundProject.ClientId = request.DestinationId;
            
            _context.Projects.Update(foundProject);
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProjectCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] ProjectModifyRequest request)
        {
            var foundProject = await _context.Projects.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (foundProject == null) return NotFound();

            foundProject.Name = request.Name;

            _context.Projects.Update(foundProject);
            await _context.SaveChangesAsync();

            return Ok(await _context.Projects.ProjectTo<ProjectCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id));
        }

        [HttpPost("setBag")]
        [ProducesResponseType(typeof(ProjectCreateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetBag([FromBody] SetBagRequest request)
        {
            /*var bag = await _context.Bags.FirstOrDefaultAsync(b => b.Id == request.BagId);
            if (bag == null) return BadRequest();*/
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (project == null)
                return NotFound("Client wasn't found");

            project.BagId = request.BagId;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return Ok(await _context.Projects.ProjectTo<ProjectCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == project.Id));
        }
    }
}