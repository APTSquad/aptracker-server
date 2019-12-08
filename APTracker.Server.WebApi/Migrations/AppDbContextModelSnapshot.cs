﻿// <auto-generated />

using System;
using APTracker.Server.WebApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APTracker.Server.WebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    internal class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.Bag", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<long>("ResponsibleId")
                    .HasColumnType("bigint");

                b.HasKey("Id");

                b.HasIndex("ResponsibleId");

                b.ToTable("Bags");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.Client", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<long?>("BagId")
                    .HasColumnType("bigint");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("BagId");

                b.ToTable("Clients");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.ConsumptionArticle", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<long?>("BagId")
                    .HasColumnType("bigint");

                b.Property<bool>("IsActive")
                    .HasColumnType("boolean");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<long?>("ProjectId")
                    .HasColumnType("bigint");

                b.HasKey("Id");

                b.HasIndex("BagId");

                b.HasIndex("ProjectId");

                b.ToTable("ConsumptionArticles");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.ConsumptionReportItem", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<long?>("ArticleId")
                    .HasColumnType("bigint");

                b.Property<long?>("DailyReportId")
                    .HasColumnType("bigint");

                b.Property<double>("HoursConsumption")
                    .HasColumnType("double precision");

                b.HasKey("Id");

                b.HasIndex("ArticleId");

                b.HasIndex("DailyReportId");

                b.ToTable("ConsumptionReportItems");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.DailyReport", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<DateTime>("Date")
                    .HasColumnType("timestamp without time zone");

                b.Property<DateTime>("Saved")
                    .HasColumnType("timestamp without time zone");

                b.HasKey("Id");

                b.ToTable("DailyReports");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.Project", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<long?>("BagId")
                    .HasColumnType("bigint");

                b.Property<long?>("ClientId")
                    .HasColumnType("bigint");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("BagId");

                b.HasIndex("ClientId");

                b.ToTable("Projects");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.User", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Email")
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<double>("Rate")
                    .HasColumnType("double precision");

                b.Property<int>("Role")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.Bag", b =>
            {
                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.User", "Responsible")
                    .WithMany("Bags")
                    .HasForeignKey("ResponsibleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.Client", b =>
            {
                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.Bag", "Bag")
                    .WithMany("Clients")
                    .HasForeignKey("BagId");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.ConsumptionArticle", b =>
            {
                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.Bag", "Bag")
                    .WithMany()
                    .HasForeignKey("BagId");

                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.Project", "Project")
                    .WithMany()
                    .HasForeignKey("ProjectId");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.ConsumptionReportItem", b =>
            {
                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.ConsumptionArticle", "Article")
                    .WithMany()
                    .HasForeignKey("ArticleId");

                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.DailyReport", null)
                    .WithMany("ReportItems")
                    .HasForeignKey("DailyReportId");
            });

            modelBuilder.Entity("APTracker.Server.WebApi.Persistence.Entities.Project", b =>
            {
                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.Bag", "Bag")
                    .WithMany("Projects")
                    .HasForeignKey("BagId");

                b.HasOne("APTracker.Server.WebApi.Persistence.Entities.Client", "Client")
                    .WithMany("Projects")
                    .HasForeignKey("ClientId");
            });
#pragma warning restore 612, 618
        }
    }
}