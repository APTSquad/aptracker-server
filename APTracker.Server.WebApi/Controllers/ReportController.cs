using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands.Report;
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
                .OrderByDescending(x => x.Date)
                .Include(x => x.ReportItems)
                .ThenInclude(x => x.Article)
                .ThenInclude(x => x.Project)
                .ThenInclude(x => x.Client)
                .FirstOrDefaultAsync();

            var data = new ReportTemplateResponse {CommonArticles = await GenerateDefaultTemplate()};


            if (theLatestDayBefore == null) return Ok(data);

            var clients = await _context.Clients
                .ProjectTo<ReportClientItem>(_mapper.ConfigurationProvider)
                .ToListAsync();
            

            foreach (var elem in theLatestDayBefore.ReportItems.Select(x => x.Article))
            {
                if (elem.IsCommon) continue;
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
        
        [HttpPost("saveReport")]
        public async Task<IActionResult> SaveReport([FromBody] SaveReportCommand req)
        {
            var userId = req.UserId;
            var date = req.Date.Date;

            var cnt = await _context.Users.CountAsync(x => x.Id == userId);

            if (cnt == 0)
                return BadRequest();
            
            var reportItems = _mapper.Map<ICollection<ReportConsumptionItem>, List<ConsumptionReportItem>>(req.Articles);

            var dailyReport = await _context.DailyReports.Include(x => x.ReportItems).FirstOrDefaultAsync(x => x.UserId == userId && x.Date == date);

            if (dailyReport == null)
            {
                var newReport = new DailyReport {UserId = userId, Date = date, ReportItems = reportItems};
                await _context.DailyReports.AddAsync(newReport);
            }
            else
            {
                dailyReport.ReportItems = reportItems;
                _context.DailyReports.Update(dailyReport);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private async Task<ICollection<ReportArticleItem>> GenerateDefaultTemplate()
        {
            return await _context.ConsumptionArticles
                .Where(x => x.IsCommon && x.IsActive)
                .ProjectTo<ReportArticleItem>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public class Date
        {
            public int Day { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }
    }
}