using EntertainmentUnitTests.Helper;
using EntertainmentWebApiApplication.Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntertainmentUnitTests
{
    class TeamCreateServiceTest
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
        public async Task Teams_CreateServiceTest_Success()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.CreateTeam("CSK", "India", "Sathish");
                if(result.Status == 200)
                {
                    actual = true;
                }    
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_CreateServiceTest_Failure_NoName()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.CreateTeam("", "India", "Sathish");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_CreateServiceTest_Failure_NoCountry()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.CreateTeam("CSK", null, "Sathish");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_CreateServiceTest_Failure_NoOwner()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.CreateTeam("CSK", "India", " ");
                if (result.Status == 204)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }

        [Test]
        public async Task Teams_CreateServiceTest_Failure_NameAlreadyInUse()
        {
            var actual = false;
            using (var context = ContextHelper.CreateContext(DbName))
            {
                var teamsService = ContextHelper.CreateTeamsService(context);
                var result = await teamsService.CreateTeam("KKR", "India", "Sathish");
                if (result.Status == 409)
                {
                    actual = true;
                }
            }
            Assert.AreEqual(true, actual);
        }
    }
}
