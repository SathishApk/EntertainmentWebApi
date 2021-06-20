using EntertainmentUnitTests.Helper;
using EntertainmentWebApiApplication.Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntertainmentUnitTests
{
    class TeamUpdateServiceTests
    {
        string DbName;
        [SetUp]
        public void Setup()
        {
            DbName = ContextHelper.CreateDbName();
            using (var context = ContextHelper.CreateContext(DbName))
            {
                Team team = new Team() { Country = "India", CreatedBy = "Sam", CreatedDttm = DateTime.Now, TeamName = "KKR" };
                context.Add(team);
                context.SaveChanges();
            }
        }

        [Test]
        public async Task Teams_UpdateServiceTest_Success()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.UpdateTeam("KKR", "India", "Sathish");
                if (result.Status == 200)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_UpdateServiceTest_Failure_NoName()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.UpdateTeam("", "India", "Sathish");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_UpdateServiceTest_Failure_NoCountry()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.UpdateTeam("KKR", null, "Sathish");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_UpdateServiceTest__Failure_NoOwner()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.UpdateTeam("KKR", "India", "    ");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_UpdateServiceTest__Failure_NoTeam()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.UpdateTeam("KKR_", "India", "Sathish");
                if (result.Status == 404)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }
    }
}
