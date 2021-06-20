using EntertainmentWebApiApplication.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Services
{
    public interface ITeamsService
    {
        Task<IReadOnlyCollection<Team>> GetAllTeams();
        Task<Team> GetTeam(int teamId);
        Task<Result> CreateTeam(string name, string country, string owner);
        Task<Result> UpdateTeam(string name, string country, string owner);
        Task<Result> DeleteTeam(string name);
    }
}
