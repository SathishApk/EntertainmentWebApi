using EntertainmentUnitTests.Helper;
using EntertainmentWebApiApplication.Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntertainmentUnitTests
{
    class MatchDeleteServiceTests
    {
        string DbName;
        [SetUp]
        public void Setup()
        {
            DbName = ContextHelper.CreateDbName();
            using (var context = ContextHelper.CreateContext(DbName))
            {
                Team team1 = new Team() { Country = "India", CreatedBy = "Sathish", CreatedDttm = DateTime.Now, TeamName = "CSK" };
                Team team2 = new Team() { Country = "India", CreatedBy = "Sam", CreatedDttm = DateTime.Now, TeamName = "KKR" };
                Team team3 = new Team() { Country = "India", CreatedBy = "Yash", CreatedDttm = DateTime.Now, TeamName = "MI" };
                context.Add(team1);
                context.Add(team2);
                context.SaveChanges();

                Match match1 = new Match() { MatchLocation = "Chennai", RecordCreatedDttm = DateTime.Now, TeamA_Id = 1, TeamA_Score = 200, TeamB_Id = 3, TeamB_Score = 180, Winner_Id = 1 };
                Match match2 = new Match() { MatchLocation = "Mumbai", RecordCreatedDttm = DateTime.Now, TeamA_Id = 2, TeamA_Score = 210, TeamB_Id = 3, TeamB_Score = 215, Winner_Id = 3 };
                context.Add(match1);
                context.Add(match2);
                context.SaveChanges();
            }
        }

        [Test]
        public async Task Match_DeleteServiceTest_Success()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.DeleteMatch(1);
                if (result.Status == 200)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Match_DeleteServiceTest_Failure_NoMatch()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var matchService = ContextHelper.CreateMatchService(context);
                var result = await matchService.DeleteMatch(4);
                if (result.Status == 404)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }
    }
}
