using System;
using System.Collections.Generic;
using System.Linq;
using APTracker.Server.WebApi.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Persistence
{
    public static class ContextSeeder
    {
        /// <summary>
        ///     Версия сида
        /// </summary>
        private const int SeedVersion = 2;

        /// <summary>
        ///     Адрес пользователя-индикатора текущей версии сида
        /// </summary>
        private static readonly string DatabaseSeedName = $"seed@database.ver-{SeedVersion}";

        public static void SeedDatabase(AppDbContext context)
        {
            var seedUser = context.Users.FirstOrDefault(x => x.Email == DatabaseSeedName);
            if (seedUser != null) return;
            // Если нет пользователя с заданным адресом, требуется пересоздать и инициализировать базу
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var users = new List<User>
            {
                new User {Id = 1, Name = "Seed", Email = DatabaseSeedName, Rate = 1.0, Role = Role.Developer}, // 0
                new User {Id = 2, Name = "Admin", Email = "admin@live.com", Rate = 1.0, Role = Role.Admin}, // 1
                new User {Id = 3, Name = "Sarah", Email = "sarah@live.com", Rate = 1.0, Role = Role.Manager}, // 2
                new User {Id = 4, Name = "John", Email = "john@live.com", Rate = 0.75, Role = Role.Manager}, // 3
                new User {Id = 5, Name = "Kate", Email = "kate@live.com", Rate = 0.7, Role = Role.Developer} // 4
            };

            var bags = new List<Bag>
            {
                new Bag {Name = "Sarah's Bag", Responsible = users[2]},
                new Bag {Name = "John's Bag", Responsible = users[3]}
            };

            var commonArticles = new List<ConsumptionArticle>
            {
                new ConsumptionArticle {Bag = null, Id = 1, IsCommon = true, Name = "Простой"},
                new ConsumptionArticle {Bag = null, Id = 2, IsCommon = true, Name = "Отсутствие"}
            };

            var clients = new List<Client>
            {
                new Client
                {
                    Bag = bags[0], Name = "ICL", Projects = new List<Project>
                    {
                        new Project
                        {
                            Name = "ICL-2020", Bag = bags[0], Articles = new List<ConsumptionArticle>
                            {
                                new ConsumptionArticle
                                    {Id = 10, Bag = bags[0], IsCommon = false, Name = "Разработка (ICL-20)"},
                                new ConsumptionArticle
                                    {Id = 11, Bag = bags[0], IsCommon = false, Name = "Анализ (ICL-20)"},
                                new ConsumptionArticle
                                {
                                    Id = 12, Bag = bags[1], IsCommon = false, Name = "Статья другого портфеля (ICL-20)"
                                }
                            }
                        },
                        new Project
                        {
                            Name = "ICL-2019", Bag = bags[0], Articles = new List<ConsumptionArticle>
                            {
                                new ConsumptionArticle
                                    {Id = 20, Bag = bags[0], IsCommon = false, Name = "Разработка (ICL-19)"},
                                new ConsumptionArticle
                                    {Id = 21, Bag = bags[0], IsCommon = false, Name = "Анализ (ICL-19)"},
                                new ConsumptionArticle
                                {
                                    Id = 22, Bag = bags[1], IsCommon = false, Name = "Статья другого портфеля (ICL-19)"
                                }
                            }
                        }
                    }
                },
                new Client
                {
                    Bag = bags[1], Name = "MG", Projects = new List<Project>
                    {
                        new Project
                        {
                            Name = "MG-2019", Bag = bags[1], Articles = new List<ConsumptionArticle>
                            {
                                new ConsumptionArticle
                                    {Id = 30, Bag = bags[1], IsCommon = false, Name = "Разработка (MG)"},
                                new ConsumptionArticle {Id = 31, Bag = bags[1], IsCommon = false, Name = "Анализ (MG)"},
                                new ConsumptionArticle
                                    {Id = 32, Bag = null, IsCommon = false, Name = "Статья не в портфеле (MG)"},
                                new ConsumptionArticle
                                    {Id = 33, Bag = bags[0], IsCommon = false, Name = "Статья другого портфеля (MG)"}
                            }
                        }
                    }
                }
            };

            var reports = new List<DailyReport>
            {
                new DailyReport
                {
                    Date = new DateTime(2019, 9, 1),
                    User = users[2],
                    State = ReportState.Fixed,
                    ReportItems = new List<ConsumptionReportItem>
                    {
                        new ConsumptionReportItem {ArticleId = 1, HoursConsumption = 1},
                        new ConsumptionReportItem {ArticleId = 20, HoursConsumption = 1.5},
                        new ConsumptionReportItem {ArticleId = 21, HoursConsumption = 2.5},
                        new ConsumptionReportItem {ArticleId = 22, HoursConsumption = 4}
                    }
                },
                new DailyReport
                {
                    Date = new DateTime(2019, 9, 2),
                    User = users[4],
                    State = ReportState.Fixed,
                    ReportItems = new List<ConsumptionReportItem>
                    {
                        new ConsumptionReportItem {ArticleId = 20, HoursConsumption = 3.5},
                        new ConsumptionReportItem {ArticleId = 21, HoursConsumption = 3.5},
                        new ConsumptionReportItem {ArticleId = 22, HoursConsumption = 1}
                    }
                },
                new DailyReport
                {
                    Date = new DateTime(2019, 10, 1),
                    User = users[3],
                    State = ReportState.Editable,
                    ReportItems = new List<ConsumptionReportItem>
                    {
                        new ConsumptionReportItem {ArticleId = 1, HoursConsumption = 1},
                        new ConsumptionReportItem {ArticleId = 30, HoursConsumption = 1.5},
                        new ConsumptionReportItem {ArticleId = 31, HoursConsumption = 2.5},
                        new ConsumptionReportItem {ArticleId = 32, HoursConsumption = 4}
                    }
                }
            };

            context.AddRange(users);
            context.AddRange(bags);
            context.AddRange(clients);
            context.AddRange(commonArticles);
            context.AddRange(reports);

            context.SaveChanges();
        }
    }
}