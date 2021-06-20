using EntertainmentUnitTests.Helper;
using EntertainmentWebApiApplication.Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace EntertainmentUnitTests
{
    public class TeamsFetchServiceTest
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
                context.Add(team1);
                context.Add(team2);
                context.SaveChanges();
            }
        }

        [Test]
        public async Task Teams_FetchServiceTest()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var totalResult = await teamsService.GetAllTeams();
                if(totalResult.Count == 2)
                {
                    var result = await teamsService.GetTeam(1);
                    if (result != null)
                    {
                        actual = true;
                    }
                }
            }
            Assert.AreEqual(true, actual);
        }
    }
}