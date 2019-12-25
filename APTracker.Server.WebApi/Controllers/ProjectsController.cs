using System.Threading.Tasks;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using APTracker.Server.WebApi.ViewModels;
using APTracker.Server.WebApi.ViewModels.Commands.Client.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Project.Create;
using APTracker.Server.WebApi.ViewModels.Commands.Project.GetAll;
using APTracker.Server.WebApi.ViewModels.Commands.Project.Modify;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Projects.ProjectTo<ProjectGetAllResponse>(_mapper.ConfigurationProvider).ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if ((await _context.Projects.CountAsync(x => x.Id == id)) == 0)
            {
                return NotFound();
            }
            return Ok(await _context.Projects.ProjectTo<ProjectGetAllResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCreateCommand command)
        {
            var client = _context.Clients.FirstOrDefaultAsync(b => b.Id == command.ClientId);
            if (client == null) return BadRequest();
            var ent = await _context.Projects.AddAsync(_mapper.Map<Project>(command));
            await _context.SaveChangesAsync();

            return Ok(await _context.Projects.ProjectTo<ProjectCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == ent.Entity.Id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProjectModifyCommand command)
        {
            var foundProject = await _context.Projects.FirstOrDefaultAsync(c => c.Id == command.Id);
            if (foundProject == null) return NotFound();

            foundProject.Name = command.Name;

            _context.Projects.Update(foundProject);
            await _context.SaveChangesAsync();

            return Ok(await _context.Projects.ProjectTo<ProjectCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == command.Id));
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
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (project == null)
                return NotFound("Client wasn't found");

            project.BagId = command.BagId.Value;
            
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return Ok(await _context.Projects.ProjectTo<ProjectCreateResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == project.Id));
        }
    }
}