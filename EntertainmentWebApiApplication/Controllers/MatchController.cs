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
    public class MatchController : Controller
    {
        private IMatchService _matchService;
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("GetAllTeamResults")]
        public async Task<IActionResult> GetAllTeamResults()
        {
            return Ok(await _matchService.GetAllTeamResults());
        }

        [HttpGet("GetAllMatches")]
        public async Task<IActionResult> GetAllMatches()
        {
            return Ok(await _matchService.GetAllMatches());
        }

        [HttpGet("GetMatch")]
        public async Task<IActionResult> GetMatch(int matchId)
        {
            return Ok(await _matchService.GetMatch(matchId));
        }

        [HttpPost("CreateMatch")]
        public async Task<IActionResult> CreateMatch(string teamA, string teamB, int teamAScore, int teamBScore, string matchLocation)
        {
            var result = await _matchService.CreateMatch(teamA, teamB, teamAScore, teamBScore, matchLocation);
            return Ok(result);
        }

        [HttpPost("UpdateMatch")]
        public async Task<IActionResult> UpdateMatch(int matchId, int teamAScore, int teamBScore)
        {
            var result = await _matchService.UpdateMatch(matchId, teamAScore, teamBScore);
            return Ok(result);
        }


        [HttpPost("DeleteMatch")]
        public async Task<IActionResult> DeleteMatch(int matchId)
        {
            var result = await _matchService.DeleteMatch(matchId);
            return Ok(result);
        }
    }
}
