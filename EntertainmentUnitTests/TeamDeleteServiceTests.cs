using EntertainmentUnitTests.Helper;
using EntertainmentWebApiApplication.Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntertainmentUnitTests
{
    class TeamDeleteServiceTests
    {
        string DbName;
        [SetUp]
        public void Setup()
        {
            DbName = ContextHelper.CreateDbName();
            using (var context = ContextHelper.CreateContext(DbName))
            {
                Team team1 = new Team() { Country = "India", CreatedBy = "Sathish", CreatedDttm = DateTime.Now, TeamName = "CSK" };
                Team team3 = new Team() { Country = "India", CreatedBy = "Yash", CreatedDttm = DateTime.Now, TeamName = "MI" };
                Team team2 = new Team() { Country = "India", CreatedBy = "Sam", CreatedDttm = DateTime.Now, TeamName = "KKR" };

                context.Add(team1);
                context.Add(team2);
                context.Add(team3);
                context.SaveChanges();

                Match match = new Match() { MatchLocation = "Chennai", RecordCreatedDttm = DateTime.Now, TeamA_Id = 1, TeamA_Score = 200, TeamB_Id = 3, TeamB_Score = 180, Winner_Id = 1 };
                context.Add(match);
                context.SaveChanges();
            }
        }

        [Test]
        public async Task Teams_DeleteServiceTest_Success()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.DeleteTeam("KKR");
                if (result.Status == 200)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_DeleteServiceTest_Failure_NoName()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.DeleteTeam("");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_DeleteServiceTest_Failure_NoTeam()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.DeleteTeam("CSK_");
                if (result.Status == 404)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_DeleteServiceTest_Failure_MatchExists()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.DeleteTeam("CSK");
                if (result.Status == 409)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

    }
}
