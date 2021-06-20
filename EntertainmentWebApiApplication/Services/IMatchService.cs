using EntertainmentWebApiApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Services
{
    public interface IMatchService
    {
        Task<IReadOnlyCollection<Match>> GetAllMatches();
        Task<IReadOnlyCollection<PointsTable>> GetAllTeamResults();
        Task<Match> GetMatch(int matchId);
        Task<Result> CreateMatch(string teamA, string teamB, int teamAScore, int teamBScore, string matchLocation);
        Task<Result> UpdateMatch(int matchId, int teamAScore, int teamBScore);
        Task<Result> DeleteMatch(int matchId);
    }
}
