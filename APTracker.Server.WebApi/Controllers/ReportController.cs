using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands.Report;
using APTracker.Server.WebApi.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///     Генерирует шаблон по отчету на заданную дату на основе последнего предыдущего отчета
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("getTemplate")]
        [ProducesResponseType(typeof(ReportTemplateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplate([FromBody] ReportTemplateRequest req)
        {
            var date = req.Date.Date;
            var theLatestDayBefore = await _context.DailyReports
                .Where(x => x.Date < date)
                .OrderByDescending(x => x.Date).FirstOrDefaultAsync();

            var data = new ReportTemplateResponse {CommonArticles = await GenerateDefaultTemplate()};


            if (theLatestDayBefore == null) return Ok(data);

            var clients = await _context.Clients
                .ProjectTo<ReportClientItem>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach (var elem in theLatestDayBefore.ReportItems.Select(x => x.Article))
            {
                var client = clients.FirstOrDefault(x => x.Id == elem.Project.ClientId);
                var project = client.Projects.FirstOrDefault(x => x.Id == elem.Project.Id);
                var article = project.Articles.FirstOrDefault(x => x.Id == elem.Id);
                client.IsChecked = true;
                project.IsChecked = true;
                article.IsChecked = true;
            }

            data.Clients = clients;

            return Ok(data);
        }

        private async Task<ICollection<ReportArticleItem>> GenerateDefaultTemplate()
        {
            var globals = await _context.ConsumptionArticles
                .Where(x => x.IsCommon && x.IsActive)
                .ProjectTo<ReportArticleItem>(_mapper.ConfigurationProvider).ToListAsync();
            return globals;
        }

        public class Date
        {
            public int Day { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }
    }
}