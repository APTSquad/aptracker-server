using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands;
using APTracker.Server.WebApi.Commands.Articles.GetAll;
using APTracker.Server.WebApi.Commands.Articles.Modify;
using APTracker.Server.WebApi.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ArticlesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleGetAllResponse>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _context.ConsumptionArticles.CountAsync(x => x.Id == id) == 0) return NotFound();
            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleGetAllResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost("setBag")]
        public async Task<IActionResult> SetBag([FromBody] SetBagRequest request)
        {
            var bag = await _context.Bags.FirstOrDefaultAsync(b => b.Id == request.BagId);
            if (bag == null) return BadRequest();
            var article = await _context.ConsumptionArticles.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (article == null)
                return NotFound("Client wasn't found");

            article.BagId = request.BagId;

            _context.ConsumptionArticles.Update(article);
            await _context.SaveChangesAsync();

            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleGetAllResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == article.Id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ArticleModifyRequest request)
        {
            var foundArticle = await _context.ConsumptionArticles.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (foundArticle == null) return NotFound("Article wasn't found");

            foundArticle.Name = request.Name;

            _context.ConsumptionArticles.Update(foundArticle);
            await _context.SaveChangesAsync();

            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleGetAllResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id));
        }
    }
}