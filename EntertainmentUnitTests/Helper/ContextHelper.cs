using EntertainmentWebApiApplication.Infrastructure;
using EntertainmentWebApiApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentUnitTests.Helper
{
    class ContextHelper
    {
        public static string CreateDbName() => $"Commerce_" + Guid.NewGuid();

        public static EntertainmentDataContext CreateContext(string DbName)
        {
            var options = new DbContextOptionsBuilder<EntertainmentDataContext>()
                .UseInMemoryDatabase(DbName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            return new EntertainmentDataContext(options);
        }

        public static TeamsService CreateTeamsService(
            EntertainmentDataContext dbContext)
        {
            IRepository appRepository = CreateRepository(dbContext);            
            ILogger<TeamsService> logger = Mock.Of<ILogger<TeamsService>>();
            return new TeamsService(appRepository, logger);
        }

        public static MatchService CreateMatchService(
            EntertainmentDataContext dbContext)
        {
            IRepository appRepository = CreateRepository(dbContext);
            ILogger<MatchService> logger = Mock.Of<ILogger<MatchService>>();
            return new MatchService(appRepository, logger);
        }

        public static EntertainmentRepository<EntertainmentDataContext> CreateRepository(EntertainmentDataContext dbContext)
        {
            return new EntertainmentRepository<EntertainmentDataContext>(dbContext);
        }
    }
}
