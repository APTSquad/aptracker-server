using System;
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
        public enum ReportState
        {
            Editable,
            Fixed,
            Empty
        }

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        [HttpGet("getDays/{UserId}")]
        [ProducesResponseType(typeof(ICollection<ReportStateItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDaysData(long UserId)
        {
            var days = new List<ReportStateItem>();
            var today = DateTime.Now.Date;
            var reportItem = await _context.DailyReports
                .Where(x => x.UserId == UserId)
                .OrderBy(x => x.Date)
                .FirstOrDefaultAsync();

            if (reportItem != null)
            {
                var startDate = reportItem.Date.Date;
                var reportItemsDict = await _context.DailyReports
                    .ToDictionaryAsync(x => x.Date.Date);
                foreach (var day in EachDay(startDate, today))
                {
                    var stateItem = new ReportStateItem {Date = day};

                    if (reportItemsDict.TryGetValue(day, out var report))
                        stateItem.State = (ReportState) report.State;
                    else
                        stateItem.State = ReportState.Empty;

                    days.Add(stateItem);
                }
            }

            return Ok(days);
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

        [HttpPost("getReport")]
        public async Task<IActionResult> ReportGetCommand([FromBody] ReportGetCommand req)
        {
            // TODO: Date checks & mapping queries
            var userId = req.UserId;
            var date = req.Date.Date;

            var user = await _context.Users.AnyAsync(x => x.Id == userId);

            if (!user)
                return BadRequest("User wasn't found");

            var dailyReport = await _context.DailyReports
                .Include(x => x.ReportItems)
                .ThenInclude(x => x.Article)
                .ThenInclude(x => x.Project)
                .ThenInclude(x => x.Client)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (dailyReport == null)
                return BadRequest("Report wasn't found");

            var grouped = dailyReport.ReportItems
                .Where(x => !x.Article.IsCommon)
                .GroupBy(x => new {x.Article.Project.Client, x.Article.Project}).ToList();


            foreach (var item in grouped) item.Key.Client.Projects = new List<Project>();

            foreach (var elem in grouped)
            {
                var proj = elem.Key.Project;
                var client = elem.Key.Client;
                proj.Client = null;
                client.Projects.Add(proj);

                var lst = elem.Select(x => x.Article).ToList();
                lst.ForEach(x => x.Project = null);
                elem.Key.Project.Articles = lst;
            }

            var clients = grouped.Select(x => x.Key.Client).ToList();

            return Ok(clients);
        }

        [HttpPost("saveReport")]
        public async Task<IActionResult> SaveReport([FromBody] ReportSaveCommand req)
        {
            if (req.Date.Date > DateTime.Today)
                return BadRequest("Cannot report the future");

            var userId = req.UserId;
            var date = req.Date.Date;

            var user = await _context.Users.AnyAsync(x => x.Id == userId);

            if (!user)
                return BadRequest("User wasn't found");

            var reportItems =
                _mapper.Map<ICollection<ReportConsumptionItem>, List<ConsumptionReportItem>>(req.Articles);

            var dailyReport = await _context.DailyReports.Include(x => x.ReportItems)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Date == date);

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
            var data = await _context.ConsumptionArticles
                .Where(x => x.IsCommon && x.IsActive)
                .ProjectTo<ReportArticleItem>(_mapper.ConfigurationProvider)
                .ToListAsync();
            data.ForEach(x => x.IsChecked = true);

            return data;
        }

        public class ReportStateItem
        {
            public DateTime Date { get; set; }
            public ReportState State { get; set; }
        }
    }
}