using EntertainmentUnitTests.Helper;
using EntertainmentWebApiApplication.Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntertainmentUnitTests
{
    class MatchCreateServiceTests
    {
        string DbName;
        [SetUp]
        public void Setup()
        {
            DbName = ContextHelper.CreateDbName();
            using (var context = ContextHelper.CreateContext(DbName))
            {
                Team team1 = new Team() { Country = "India", CreatedBy = "Sam", CreatedDttm = DateTime.Now, TeamName = "KKR" };
                Team team2 = new Team() { Country = "India", CreatedBy = "Sathish", CreatedDttm = DateTime.Now, TeamName = "CSK" };
                context.Add(team1);
                context.Add(team2);
                context.SaveChanges();
            }
        }

        [Test]
        public async Task Match_CreateServiceTest_Success()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.CreateMatch("CSK", "KKR",180, 200,"India");
                if (result.Status == 200)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Match_CreateServiceTest_Failure_NoTeamA_Name()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.CreateMatch("", "KKR", 180, 200, "India");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Match_CreateServiceTest_Failure_NoTeamB_Name()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.CreateMatch("CSK", null, 180, 200, "India");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Match_CreateServiceTest_Failure_NoCountryName()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.CreateMatch("CSK", "KKR", 180, 200, "  ");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Match_CreateServiceTest_Failure_WrongTeamA_Name()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.CreateMatch("CSK_", "KKR", 180, 200, "India");
                if (result.Status == 404)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Match_CreateServiceTest_Failure_WrongTeamB_Name()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.CreateMatch("CSK", "KKR_", 180, 200, "India");
                if (result.Status == 404)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

    }
}
