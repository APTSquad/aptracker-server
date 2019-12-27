using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands;
using APTracker.Server.WebApi.Commands.Articles.Create;
using APTracker.Server.WebApi.Commands.Articles.Detail;
using APTracker.Server.WebApi.Commands.Articles.GetAll;
using APTracker.Server.WebApi.Commands.Articles.Modify;
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
        [ProducesResponseType(typeof(ICollection<ArticleGetAllResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleGetAllResponse>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleDetailResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            if (await _context.ConsumptionArticles.CountAsync(x => x.Id == id) == 0) return NotFound();
            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleDetailResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost("setBag")]
        [ProducesResponseType(typeof(ArticleDetailResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetBag([FromBody] SetBagRequest request)
        {
            /*var bagFound = await _context.Bags.AnyAsync(b => b.Id == request.BagId);
            if (!bagFound) return BadRequest();*/
            var article = await _context.ConsumptionArticles.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (article == null)
                return NotFound("Article wasn't found");

            article.BagId = request.BagId;

            _context.ConsumptionArticles.Update(article);
            await _context.SaveChangesAsync();

            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleDetailResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == article.Id));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ArticleDetailResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] ArticleModifyRequest request)
        {
            var foundArticle = await _context.ConsumptionArticles.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (foundArticle == null) return NotFound("Article wasn't found");

            foundArticle.Name = request.Name;

            _context.ConsumptionArticles.Update(foundArticle);
            await _context.SaveChangesAsync();

            return Ok(await _context.ConsumptionArticles.ProjectTo<ArticleDetailResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ArticleDetailResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOne([FromBody] ArticleCreateRequest request)
        {
            var found = await _context.Projects.AnyAsync(x => x.Id == request.ProjectId);
            if (!found)
                return NotFound("Project wasn't found");


            var res = await _context.ConsumptionArticles.AddAsync(_mapper.Map<ConsumptionArticle>(request));
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateOne),
                await _context.ConsumptionArticles.ProjectTo<ArticleDetailResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == res.Entity.Id));
        }
    }
}