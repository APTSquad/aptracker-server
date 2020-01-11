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
                    {
                        stateItem.State = (ReportState) report.State;
                        days.Add(stateItem);

                    }
                    else
                    {
                        stateItem.State = ReportState.Empty;
                        if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
                        {
                            days.Add(stateItem);
                        }
                    }
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
                .Where(x => x.Date <= date && x.UserId == req.UserId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.ReportItems)
                .ThenInclude(x => x.Article)
                .ThenInclude(x => x.Project)
                .ThenInclude(x => x.Client)
                .FirstOrDefaultAsync();

            var data = new ReportTemplateResponse {Common = await GenerateDefaultTemplate()};


            if (theLatestDayBefore == null) return Ok(data);

            var clients = await _context.Clients
                .ProjectTo<ReportClientItem>(_mapper.ConfigurationProvider)
                .ToListAsync();


            foreach (var elem in theLatestDayBefore.ReportItems.Where(x => !x.Article.IsCommon).Select(x => x.Article))
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

        class DayInfoRepsponse
        {
            public ReportState ReportState { get; set; }
            public int HoursRequired { get; set; }
            public object Data { get; set; }
        }
        
        [HttpPost("getDayInfo")]
        //[ProducesResponseType(typeof(ReportTemplateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDayInfo([FromBody] ReportTemplateRequest req)
        {
            var response = new DayInfoRepsponse { };
            var date = req.Date.Date;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == req.UserId);
            if (user == null)
                return BadRequest("User wasn't found");

            var report = await _context.DailyReports
                .FirstOrDefaultAsync(x => x.Date == date && x.UserId == req.UserId);

            
            if (report != null)
            {
                response.ReportState = (ReportState) report.State;
                response.Data = await GetReportImpl(new ReportGetCommand{Date = date, UserId = req.UserId});
            }
            else
            {
                response.ReportState = ReportState.Empty;
                response.Data = await GetTemplateImpl(new ReportTemplateRequest {Date = date, UserId = req.UserId});
            }

            response.HoursRequired = (int)(user.Rate * 8);

            return Ok(response);
        }

        private async Task<object> GetTemplateImpl(ReportTemplateRequest req)
        {
            var date = req.Date.Date;
            var theLatestDayBefore = await _context.DailyReports
                .Where(x => x.Date <= date && x.UserId == req.UserId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.ReportItems)
                .ThenInclude(x => x.Article)
                .ThenInclude(x => x.Project)
                .ThenInclude(x => x.Client)
                .FirstOrDefaultAsync();

            var data = new ReportTemplateResponse {Common = await GenerateDefaultTemplate()};

            var clients = await _context.Clients
                .ProjectTo<ReportClientItem>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (theLatestDayBefore != null)
            {
                foreach (var elem in theLatestDayBefore.ReportItems.Where(x => !x.Article.IsCommon).Select(x => x.Article))
                {
                    var client = clients.FirstOrDefault(x => x.Id == elem.Project.ClientId);
                    var project = client.Projects.FirstOrDefault(x => x.Id == elem.Project.Id);
                    var article = project.Articles.FirstOrDefault(x => x.Id == elem.Id);
                    client.IsChecked = true;
                    project.IsChecked = true;
                    article.IsChecked = true;
                }
            }
            
            data.Clients = clients;

            return data;
        }

        private async Task<object> GetReportImpl(ReportGetCommand req)
        {
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
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Date == date);

            if (dailyReport == null)
                return BadRequest("Report wasn't found");


            var itemsByClient = dailyReport.ReportItems.Where(x => !x.Article.IsCommon).GroupBy(x => x.Article.Project.Client).Distinct();
            var clients = itemsByClient.Select(x => new
            {
                x.Key.Id,
                x.Key.Name,
                Projects = x.GroupBy(x => x.Article.Project).Select(x =>
                    new {
                        x.Key.Id,
                        x.Key.Name,
                        Articles = x.Select(x => new
                        {
                            x.Article.Name,
                            x.Article.Id,
                            x.HoursConsumption
                        })
                    })
            });

            var common = new List<ArticleInfo>();
            foreach (var x in _context.ConsumptionArticles.Where(x => x.IsCommon))
            {

                var artInfo = new ArticleInfo
                {
                    Id = x.Id,
                    Name = x.Name,
                    HoursConsumption = null
                };
                var art = dailyReport.ReportItems.FirstOrDefault(a => a.Article.Id == x.Id);
                if (art != null)
                {
                    artInfo.HoursConsumption = art.HoursConsumption;
                }
                
                common.Add(artInfo);
            }
            
            return new {common, clients};
        }

        class ArticleInfo
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public double? HoursConsumption { get; set; }
        }
        double GetHoursConsumption(ConsumptionArticle consumptionArticle, DailyReport report)
        {
            var art = report.ReportItems.FirstOrDefault(a => a.Article.Id == consumptionArticle.Id);
            return art?.HoursConsumption ?? 0;
        }
        [HttpPost("getReport")]
        public async Task<IActionResult> GetReport([FromBody] ReportGetCommand req)
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
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Date == date);

            if (dailyReport == null)
                return BadRequest("Report wasn't found");


            var itemsByClient = dailyReport.ReportItems.Where(x => !x.Article.IsCommon).GroupBy(x => x.Article.Project.Client).Distinct();
            var clients = itemsByClient.Select(x => new
            {
                x.Key.Id,
                x.Key.Name,
                Projects = x.GroupBy(x => x.Article.Project).Select(x =>
                new {
                    x.Key.Id,
                        x.Key.Name,
                            Articles = x.Select(x => new
                            {
                                x.Article.Name,
                                x.Article.Id,
                                x.HoursConsumption
                            })
                })
            });


            var common = new List<ArticleInfo>();
            foreach (var x in _context.ConsumptionArticles.Where(x => x.IsCommon))
            {

                var artInfo = new ArticleInfo
                {
                    Id = x.Id,
                    Name = x.Name,
                    HoursConsumption = null
                };
                var art = dailyReport.ReportItems.FirstOrDefault(a => a.Article.Id == x.Id);
                if (art != null)
                {
                    artInfo.HoursConsumption = art.HoursConsumption;
                }
                
                common.Add(artInfo);
            }
            
            return Ok(new {common, clients});
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
                var newReport = new DailyReport {UserId = userId, Date = date, ReportItems = reportItems, State = (Persistence.Entities.ReportState) req.ReportState};
                await _context.DailyReports.AddAsync(newReport);
            }
            else if (dailyReport.State == Persistence.Entities.ReportState.Fixed)
            {
                return BadRequest("Report is already fixed");
            }
            else
            {
                dailyReport.ReportItems = reportItems;
                dailyReport.State = (Persistence.Entities.ReportState) req.ReportState;
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