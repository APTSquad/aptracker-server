using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands.Bag.Create;
using APTracker.Server.WebApi.Commands.Bag.GetAll;
using APTracker.Server.WebApi.Commands.Bag.GetById;
using APTracker.Server.WebApi.Commands.Bag.Modify;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class BagsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BagsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Bags.ProjectTo<BagGetAllResponse>(_mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _context.Bags.CountAsync(x => x.Id == id) == 0) return NotFound();
            return Ok(await _context.Bags.ProjectTo<BagGetByIdResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] BagCreateRequest request)
        {
            var res = await _context.Bags.AddAsync(_mapper.Map<Bag>(request));
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateOne),
                await _context.Bags.ProjectTo<BagGetAllResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == res.Entity.Id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BagModifyRequest request)
        {
            var bag = await _context.Bags.Include(x => x.Responsible).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (bag == null)
                return NotFound();

            bag.Name = request.Name;
            bag.ResponsibleId = request.ResponsibleId;
            _context.Bags.Update(bag);
            await _context.SaveChangesAsync();
            return Ok(await _context.Bags.ProjectTo<BagGetAllResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == bag.Id));
        }
    }
}