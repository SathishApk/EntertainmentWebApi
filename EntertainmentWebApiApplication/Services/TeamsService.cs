using EntertainmentWebApiApplication.Infrastructure;
using EntertainmentWebApiApplication.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Services
{
    public class TeamsService :ITeamsService
    {
        private readonly IRepository _entertainmentRepository;
        private readonly ILogger<TeamsService> _logger;
        public TeamsService(IRepository entertainmentRepository, ILogger<TeamsService> logger)
        {
            _entertainmentRepository = entertainmentRepository;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Team>> GetAllTeams()
        {
            return await _entertainmentRepository.GetAllAsync<Team>();
        }

        public async Task<Team> GetTeam(int teamId)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTeam)} Api Called");
                if (_entertainmentRepository.GetExists<Team>(x => x.TeamId == teamId))
                {
                    return await _entertainmentRepository.GetOneAsync<Team>(x => x.TeamId == teamId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Result> CreateTeam(string name, string country, string owner)
        {
            try
            {
                _logger.LogInformation($"{nameof(CreateTeam)} Api Called");
                Team teamObj = new Team();
                teamObj.TeamName = name?.Trim();
                teamObj.Country = country?.Trim();
                teamObj.CreatedBy = owner?.Trim();
                teamObj.CreatedDttm = DateTime.Now;
                if (!String.IsNullOrEmpty(teamObj.TeamName) && !String.IsNullOrEmpty(teamObj.Country) && !String.IsNullOrEmpty(teamObj.CreatedBy))
                {
                    if (!_entertainmentRepository.GetExists<Team>(x => x.TeamName.ToLower().Equals(teamObj.TeamName.ToLower())))
                    {
                        using (var transaction = await _entertainmentRepository.BeginTransactionAsync())
                        {
                            _entertainmentRepository.Create<Team>(teamObj);
                            _entertainmentRepository.SaveChanges();
                            transaction.Commit();
                            return new Result() { Status = 200, Message = "Successful !!!" };
                        }
                    }
                    else
                    {
                        return new Result() { Status = 409, Message = $"The Team name {teamObj.TeamName} is already in use" };
                    }

                }
                else
                {
                    return new Result() { Status = 204, Message = "The Fields cannot be empty or whitespace" };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new Result() { Status = 500, Message = "Server Error !!!" };
            }
        }

        public async Task<Result> UpdateTeam(string nameOfTeamToBeUpdated, string country, string owner)
        {
            try
            {
                _logger.LogInformation($"{nameof(UpdateTeam)} Api Called");
                string name = nameOfTeamToBeUpdated?.Trim();
                if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(country?.Trim()) && !String.IsNullOrEmpty(owner?.Trim()))
                {
                    var team = await _entertainmentRepository.GetOneAsync<Team>(x => x.TeamName.ToLower().Equals(name.ToLower()));
                    if (team != null)
                    {
                        team.Country = country.Trim();
                        team.CreatedBy = owner.Trim();
                        team.LastUpdatedDttm = DateTime.Now;
                        using (var transaction = await _entertainmentRepository.BeginTransactionAsync())
                        {
                            _entertainmentRepository.Update<Team>(team);
                            _entertainmentRepository.SaveChanges();
                            transaction.Commit();
                            return new Result() { Status = 200, Message = "Successful !!!" };
                        }
                    }
                    else
                    {
                        return new Result() { Status = 404, Message = "Team Not found! Please check the team Name in Get all Teams" };
                    }
                }
                else
                {
                    return new Result() { Status = 204, Message = "The Fields cannot be empty or whitespace" };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new Result() { Status = 500, Message = "Server Error !!!" };
            }
        }

        public async Task<Result> DeleteTeam(string nameOfTeamToBeDeleted)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteTeam)} Api Called");
                string name = nameOfTeamToBeDeleted?.Trim();
                if (!String.IsNullOrEmpty(name))
                {
                    var team = await _entertainmentRepository.GetOneAsync<Team>(x => x.TeamName.ToLower().Equals(name.ToLower()));
                    if (team != null)
                    {
                        if (!_entertainmentRepository.GetExists<Match>(x => x.TeamA_Id == team.TeamId || x.TeamB_Id == team.TeamId))
                        {
                            using (var transaction = await _entertainmentRepository.BeginTransactionAsync())
                            {
                                _entertainmentRepository.Delete<Team>(team);
                                _entertainmentRepository.SaveChanges();
                                transaction.Commit();
                                return new Result() { Status = 200, Message = "Successful !!!" };
                            }
                        }
                        else
                        {
                            return new Result() { Status = 409, Message = "Team cannot be deleted as the team's record is present in the Matches  try Deleting Matches and then Delete Team" };
                        }
                    }
                    else
                    {
                        return new Result() { Status = 404, Message = "Team Not found! Please check the team Name in Get all Teams" };
                    }
                }
                else
                {
                    return new Result() { Status = 204, Message = "The Fields cannot be empty or whitespace" };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new Result() { Status = 500, Message = "Server Error !!!" };
            }
        }
    }
}
