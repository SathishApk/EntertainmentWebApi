using EntertainmentWebApiApplication.Infrastructure;
using EntertainmentWebApiApplication.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository _entertainmentRepository;
        private readonly ILogger<MatchService> _logger;
        public MatchService(IRepository entertainmentRepository, ILogger<MatchService> logger)
        {
            _entertainmentRepository = entertainmentRepository;
            _logger = logger;
        }


        public async Task<IReadOnlyCollection<Match>> GetAllMatches()
        {
            return await _entertainmentRepository.GetAllAsync<Match>();
        }

        public async Task<IReadOnlyCollection<PointsTable>> GetAllTeamResults()
        {
            return await _entertainmentRepository.GetAllAsync<PointsTable>();
        }

        public async Task<Match> GetMatch(int matchId)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetMatch)} Api Called");
                if (_entertainmentRepository.GetExists<Match>(x => x.MatchId == matchId))
                {
                    return await _entertainmentRepository.GetOneAsync<Match>(x => x.MatchId == matchId);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Result> CreateMatch(string teamA, string teamB, int teamAScore, int teamBScore, string matchLocation)
        {
            try
            {
                _logger.LogInformation($"{nameof(CreateMatch)} Api Called");
                Match matchObj = new Match();
                matchObj.MatchLocation = matchLocation?.Trim();
                matchObj.RecordCreatedDttm = DateTime.Now;
                matchObj.TeamA_Score = teamAScore;
                matchObj.TeamB_Score = teamBScore;
                teamA = teamA?.Trim();
                teamB = teamB?.Trim();
                if (!String.IsNullOrEmpty(teamA) && !String.IsNullOrEmpty(teamB) && !String.IsNullOrEmpty(matchObj.MatchLocation))
                {
                    if (_entertainmentRepository.GetExists<Team>(x => x.TeamName.ToLower().Equals(teamA.ToLower()))
                        && _entertainmentRepository.GetExists<Team>(x => x.TeamName.ToLower().Equals(teamB.ToLower())))
                    {
                        matchObj.TeamA_Id = _entertainmentRepository.GetOne<Team>(x => x.TeamName.ToLower().Equals(teamA.ToLower())).TeamId;
                        matchObj.TeamB_Id = _entertainmentRepository.GetOne<Team>(x => x.TeamName.ToLower().Equals(teamB.ToLower())).TeamId;
                        if (teamAScore > teamBScore)
                        {
                            matchObj.Winner_Id = matchObj.TeamA_Id;
                        }
                        else if (teamBScore < teamAScore)
                        {
                            matchObj.Winner_Id = matchObj.TeamB_Id;
                        }
                        else
                        {
                            matchObj.Winner_Id = null;
                        }
                        using (var transaction = await _entertainmentRepository.BeginTransactionAsync())
                        {
                            _entertainmentRepository.Create<Match>(matchObj);
                            _entertainmentRepository.SaveChanges();
                            transaction.Commit();
                            return new Result() { Status = 200, Message = "Successful !!!" };
                        }
                    }
                    else
                    {
                        return new Result() { Status = 404, Message = $"Team name not found !" };
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

        public async Task<Result> UpdateMatch(int matchId, int teamAScore, int teamBScore)
        {
            try
            {
                _logger.LogInformation($"{nameof(UpdateMatch)} Api Called");
                Match matchObj = _entertainmentRepository.GetOne<Match>(x => x.MatchId == matchId);
                if (matchObj != null)
                {
                    if (teamAScore > teamBScore)
                    {
                        matchObj.Winner_Id = matchObj.TeamA_Id;
                    }
                    else if (teamBScore > teamAScore)
                    {
                        matchObj.Winner_Id = matchObj.TeamB_Id;
                    }
                    else
                    {
                        matchObj.Winner_Id = null;
                    }
                    using (var transaction = await _entertainmentRepository.BeginTransactionAsync())
                    {
                        _entertainmentRepository.Update<Match>(matchObj);
                        _entertainmentRepository.SaveChanges();
                        transaction.Commit();
                        return new Result() { Status = 200, Message = "Successful !!!" };
                    }
                }
                else
                {
                    return new Result() { Status = 404, Message = "Match Id not correct or not found" };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new Result() { Status = 500, Message = "Server Error !!!" };
            }
        }

        public async Task<Result> DeleteMatch(int matchId)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteMatch)} Api Called");
                Match matchObj = _entertainmentRepository.GetOne<Match>(x => x.MatchId == matchId);
                if (matchObj != null)
                {
                    using (var transaction = await _entertainmentRepository.BeginTransactionAsync())
                    {
                        _entertainmentRepository.Delete<Match>(matchObj);
                        _entertainmentRepository.SaveChanges();
                        transaction.Commit();
                        return new Result() { Status = 200, Message = "Successful !!!" };
                    }
                }
                else
                {
                    return new Result() { Status = 404, Message = "Match Not found!" };
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
