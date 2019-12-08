using System.Threading.Tasks;
using APTracker.Server.WebApi.Dto.Bag;
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
            return Ok(_context.Bags.ProjectTo<BagSimplifiedDto>(_mapper.ConfigurationProvider));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _context.Bags.ProjectTo<BagSimplifiedDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] BagCreateDto dto)
        {
            var res = await _context.Bags.AddAsync(_mapper.Map<Bag>(dto));
            var r = await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateOne),
                await _context.Bags.ProjectTo<BagSimplifiedDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == res.Entity.Id));
            //return Ok(_context.Bags.ProjectTo<BagSimplifiedDto>(_mapper.ConfigurationProvider));
        }
    }
}