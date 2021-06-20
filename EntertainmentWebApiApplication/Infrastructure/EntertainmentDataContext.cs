using EntertainmentWebApiApplication.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Infrastructure
{
    public class EntertainmentDataContext : DbContext
    {
        public EntertainmentDataContext(DbContextOptions<EntertainmentDataContext> options) : base(options)
        {
        }

        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<PointsTable> PointsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>()
                .ToTable("Teams")
                .HasKey(x => x.TeamId);

            modelBuilder.Entity<Match>()
                .ToTable("Matches")
                .HasKey(x => x.MatchId);

            modelBuilder.Entity<PointsTable>()
                .ToTable("PointsTable")
                .HasKey(x => x.RowId);
        }
    }
}
