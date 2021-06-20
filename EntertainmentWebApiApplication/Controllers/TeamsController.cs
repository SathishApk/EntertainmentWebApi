using EntertainmentWebApiApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        private ITeamsService _teamsService;
        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet("GetAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
            return Ok(await _teamsService.GetAllTeams());
        }

        [HttpGet("GetTeam")]
        public async Task<IActionResult> GetTeam(int teamId)
        {
            return Ok(await _teamsService.GetTeam(teamId));
        }

        [HttpPost("CreateTeam")]
        public async Task<IActionResult> CreateTeam(string TeamName, string country, string owner)
        {
            var result = await _teamsService.CreateTeam(TeamName, country, owner);
            return Ok(result);
        }

        [HttpPost("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam(string TeamName, string country, string owner)
        {
            var result = await _teamsService.UpdateTeam(TeamName, country, owner);
            return Ok(result);
        }


        [HttpPost("DeleteTeam")]
        public async Task<IActionResult> DeleteTeam(string TeamName)
        {
            var result = await _teamsService.DeleteTeam(TeamName);
            return Ok(result);
        }
    }
}
