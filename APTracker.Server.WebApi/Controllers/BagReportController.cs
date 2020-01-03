using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands.BagReport;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BagReportController : Controller
    {
        private readonly AppDbContext _context;

        public BagReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        // TODO: [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReport([FromBody] BagReportRequest request)
        {
            var bag = await _context.Bags
                .Include(x => x.Responsible)
                .FirstOrDefaultAsync(x => x.Id == request.BagId);
            if (bag == null)
                return NotFound();

            var startDate = request.Begin.Date;
            var endDate = request.End.Date;

            var reportItems = await _context.ConsumptionReportItems
                .Where(x =>
                    x.DailyReport.Date >= startDate && x.DailyReport.Date <= endDate)
                .Include(x => x.DailyReport)
                .Include(x => x.DailyReport.User)
                .Include(x => x.Article)
                .Include(x => x.Article.Bag)
                .Include(x => x.Article.Project)
                .Include(x => x.Article.Project.Bag)
                .Include(x => x.Article.Project.Client)
                .Include(x => x.Article.Project.Client.Bag)
                .ToListAsync();

            var reportItemsWithProjects = reportItems
                .Where(x => x.Article.Project != null).ToList();
            var clientsFromBag = reportItemsWithProjects
                .Where(x => x.Article.Project.Client.BagId == bag.Id).ToList();

            var usersClients = clientsFromBag.Select(x => x.DailyReport.User).Distinct();
            var projectsFromBag = reportItemsWithProjects
                .Where(x => x.Article.Project.BagId == bag.Id).ToList();

            var usersProjects = projectsFromBag.Select(x => x.DailyReport.User).Distinct();
            var articlesFromBag = reportItemsWithProjects
                .Where(x => x.Article.BagId == bag.Id).ToList();

            var usersArticles = articlesFromBag.Select(x => x.DailyReport.User).Distinct();
            var relatedUsers = usersProjects.Union(usersArticles).Union(usersClients).ToList();

            var byClient = GetSummariesByUser(clientsFromBag, relatedUsers);
            var byProject = GetSummariesByUser(projectsFromBag, relatedUsers);
            var byArticle = GetSummariesByUser(articlesFromBag, relatedUsers);
            var usersViews = relatedUsers.Select(x => new {x.Id, x.Name}).ToList();


            return Ok(new {Clients = byClient, Projects = byProject, Articles = byArticle, Users = usersViews});
        }

        private static object GetUsersData(IEnumerable<ConsumptionReportItem> consumptionReportItems,
            IEnumerable<User> allUsers)
        {
            var groupByUsers = consumptionReportItems
                .GroupBy(x => x.DailyReport.User);

            var resultingItems = allUsers.Select(user =>
            {
                var foundItem = groupByUsers.FirstOrDefault(x => x.Key.Id == user.Id);

                return new
                {
                    UserId = user.Id,
                    Summary = foundItem?.Sum(x => x.HoursConsumption) ?? 0
                };
            });

            return resultingItems;
        }

        private static object GetSummariesByUser(IEnumerable<ConsumptionReportItem> projects, IEnumerable<User> users)
        {
            return projects
                .GroupBy(x => x.Article.Project.Client)
                .Select(x => new
                {
                    ClientName = x.Key.Name,
                    Users = GetUsersData(x, users),
                    Projects = x.GroupBy(x => x.Article.Project)
                        .Select(x => new
                        {
                            ProjectName = x.Key.Name,
                            Users = GetUsersData(x, users),
                            Articles = x.GroupBy(x => x.Article)
                                .Select(x => new
                                {
                                    ArticleName = x.Key.Name,
                                    Users = GetUsersData(x, users)
                                })
                        })
                }).ToList();
        }
    }
}