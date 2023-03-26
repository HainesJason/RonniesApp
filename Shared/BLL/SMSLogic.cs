using Microsoft.Extensions.Configuration;
using SMS.Shared.DAL;
using SMS.Shared.DTOs;
using SMS.Shared.DTOs.Availability;
using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.BLL
{
    public class SMSLogic : ISMSLogic
    {
        private readonly IDataAccess _dataAccess;
        private readonly IConfiguration _config;
        private readonly string _connectionString = string.Empty;

        public SMSLogic(IDataAccess dataAccess, IConfiguration config)
        {
            _dataAccess = dataAccess;
            _config = config;
            _connectionString = _config.GetConnectionString("SMS");
        }

        #region Players logic


        public async Task<IEnumerable<PlayerSummaryDto>> GetPlayersSummary()
        {
            // pre processing 
            var sqlStatement = "select * from Player;";
            var players = await _dataAccess.RunAQuery<Player, dynamic>(
                sqlStatement,
                new { },
                _connectionString);
            // post processing logic - mapping from player objects to PlayerSummaryDto logics
            var summaries = (from p in players
                             select new PlayerSummaryDto
                             {
                                 Id = p.Id,
                                 Fullname = $"{p.Firstname} {p.Lastname}",
                                 IsActivePlayer = p.IsActivePlayer
                             }).ToList();
            return summaries;
        }



        public async Task<Player> GetPlayer(int id)
        {
            var sqlStatement = "select * from Player WHERE id = @id;";
            var player = await _dataAccess.RunAQuery<Player, dynamic>(
                sqlStatement,
                new { id },
                _connectionString);
            return player.FirstOrDefault();
        }

        public async Task<IEnumerable<PlayerSummaryDto>> GetPlayersByStatus(bool isActivePlayer)
        {
            var summary = await GetPlayersSummary();
            return summary.Where(p => p.IsActivePlayer == isActivePlayer).ToList();
        }

        public async Task<ExecuteCommandResponseDto> SavePlayer(AddPlayerDto playerToSave)
        {
            // pre - processing example
            // map addPlayerDto to player
            var response = new ExecuteCommandResponseDto();
            var player = new Player
            {
                Email = playerToSave.Email,
                Firstname = playerToSave.Firstname,
                Lastname = playerToSave.Lastname,
                PhoneNumber = playerToSave.PhoneNumber,
                IsActivePlayer = playerToSave.IsActivePlayer
            };

            var sqlStatement =
                @"insert INTO Player
                ([FirstName]
                ,[Lastname]
                ,[Email]
                ,[PhoneNumber]
                ,[IsActivePlayer])
                VALUES
                (@Firstname,
                @Lastname,
                @Email,
                @PhoneNumber,
                @IsActivePlayer)";
            try
            {
                await _dataAccess.ExecuteACommand(sqlStatement,
                            new
                            {
                                player.Firstname,
                                player.Lastname,
                                player.Email,
                                player.PhoneNumber,
                                player.IsActivePlayer
                            },
                            _connectionString);
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.Ok;
                return response;
            }
            catch (Exception ex)
            {

                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.InternalException;
                response.ErrorMessage = ex.Message;
                return response;
            }

        }

        public async Task<ExecuteCommandResponseDto> DeletePlayer(int id)
        {
            var response = new ExecuteCommandResponseDto();
            var sqlStatement = "DELETE FROM Player WHERE id = @id";
            try
            {
                await _dataAccess.ExecuteACommand(sqlStatement, new { id }, _connectionString);
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.Ok;
                return response;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.InternalException;
                return response;
            }
        }

        public async Task<ExecuteCommandResponseDto> AmendPlayer(int id, Player playerToChange)
        {
            var response = new ExecuteCommandResponseDto();
            if (id != playerToChange.Id)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.BadData;
                response.ErrorMessage = "Id mis-match";
                return response;
            }
            var playerToUpdate = await GetPlayer(id);
            if (playerToUpdate == null)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.ResourceNotFound;
                response.ErrorMessage = "Could not find the player to update";
                return response;
            }

            var sqlStatement = "UPDATE Player SET Firstname=@Firstname, Lastname=@Lastname, Email=@Email, PhoneNumber=@PhoneNumber, IsActivePlayer=@IsActivePlayer, NumberOfPlayersRequired=@NumberOfPlayersRequired WHERE Id=@Id";
            try
            {
                await _dataAccess.ExecuteACommand(
                    sqlStatement,
                    new
                    {
                        playerToChange.Firstname,
                        playerToChange.Lastname,
                        playerToChange.Email,
                        playerToChange.PhoneNumber,
                        playerToChange.IsActivePlayer,
                        playerToChange.Id
                    },
                    _connectionString);
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.InternalException;
                response.ErrorMessage = ex.Message;
                return response;
            }

        }
        #endregion


        #region Fixtures Logic
        public async Task<IEnumerable<FixtureSummaryDto>> GetAllFixtures()
        {
            var sqlStatement = "SELECT * FROM Fixture";
            var fixtures = await _dataAccess.RunAQuery<Fixture, dynamic>(
                sqlStatement,
                new { },
                _connectionString);

            return (from f in fixtures
                    select new FixtureSummaryDto
                    {
                        Id = f.Id,
                        Opponent = f.Opponent,
                        DateOfFixture = f.DateOfFixture,
                        NumberOfPlayersRequired = f.NumberOfPlayersRequired,
                        StartTime = f.StartTime,
                        Venue = f.Venue.ToString()
                    }).ToList();
        }

        public async Task<Fixture> GetFixture(int FixtureId)
        {
            var sqlStatement = "SELECT * FROM Fixture WHERE Id = @FixtureId";
            var fixture = await _dataAccess.RunAQuery<Fixture, dynamic>(
                sqlStatement,
                new { FixtureId = @FixtureId },
                _connectionString);
            return fixture.FirstOrDefault();
        }

        public async Task<ExecuteCommandResponseDto> SaveFixture(AddFixtureDto fixtureToAdd)
        {
            var response = new ExecuteCommandResponseDto();
            var sqlStatement = "INSERT INTO Fixture(Opponent, DateOfFixture, StartTime, Venue, Postcode, NumberOfPlayersRequired) VALUES (@Opponent, @DateOfFixture, @StartTime, @Venue, @Postcode, @NumberOfPlayersRequired)";

            try
            {
                await _dataAccess.ExecuteACommand(
                                   sqlStatement,
                                   new
                                   {
                                       fixtureToAdd.Opponent,
                                       fixtureToAdd.DateOfFixture,
                                       fixtureToAdd.StartTime,
                                       fixtureToAdd.Venue,
                                       fixtureToAdd.Postcode,
                                       fixtureToAdd.NumberOfPlayersRequired
                                   },
                                   _connectionString);
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.InternalException;
                response.ErrorMessage = ex.Message;
                return response;
                throw;
            }


        }

        public async Task<ExecuteCommandResponseDto> DeleteFixture(int fixtureId)
        {
            var response = new ExecuteCommandResponseDto();
            //var sqlStatement = "DELETE FROM Fixture WHERE id = @id"; // only works if no availability records
            var sqlStatement = "usp_DeleteFixture";
            try
            {
                await _dataAccess.ExecuteACommand(sqlStatement,
                                                  new { FixtureId = fixtureId },
                                                  _connectionString,
                                                  System.Data.CommandType.StoredProcedure);
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.Ok;
                return response;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.InternalException;
                return response;
            }
        }



        public async Task<ExecuteCommandResponseDto> AmendFixture(int id, Fixture fixtureToChange)
        {
            var response = new ExecuteCommandResponseDto();
            if (id != fixtureToChange.Id)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.BadData;
                response.ErrorMessage = "Id mis-match";
                return response;
            }
            var fixtureToUpdate = await GetFixture(id);
            if (fixtureToUpdate == null)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.ResourceNotFound;
                response.ErrorMessage = "Could not find the fixture to update";
                return response;
            }

            var sqlStatement = "UPDATE Fixture SET Opponent=@Opponent, DateOfFixture=@DateOfFixture, StartTime=@StartTime, Venue=@Venue, PostCode=@Postcode, NumberOfPlayersRequired=@NumberOfPlayersRequired WHERE Id=@Id";
            try
            {
                await _dataAccess.ExecuteACommand(
                    sqlStatement,
                    new
                    {
                        fixtureToChange.Opponent,
                        fixtureToChange.DateOfFixture,
                        fixtureToChange.StartTime,
                        fixtureToChange.Venue,
                        fixtureToChange.Postcode,
                        fixtureToChange.NumberOfPlayersRequired,
                        fixtureToChange.Id
                    },
                    _connectionString);
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.InternalException;
                response.ErrorMessage = ex.Message;
                return response;
            }


        }
        #endregion

        // availability
        #region availability
        public async Task<PlayersAvailableForFixtureDto> GetPlayersAvailableForFixture(int fixtureId)
        {
            var fixture = await GetFixture(fixtureId);
            if (fixture is null)
            {
                return null;
            }
            // Shape the fixture data into PlayersAvailableForFixtureDto
            var fixtureDetails = new PlayersAvailableForFixtureDto
            {
                FixtureId = fixture.Id,
                Opponents = fixture.Opponent,
                DateOfFixture = fixture.DateOfFixture.ToString(),
                Venue = fixture.Venue,
                StartTime = fixture.StartTime
            };

            // Get the players who have signed up for the fixture....
            var sqlStatement = "SELECT p.Id AS 'playerId', p.Firstname + ' ' + p.Lastname AS 'Fullname' FROM PLayer as p JOIN Availability as a on p.Id = a.PlayerId where a.FixtureId = @fixtureId";
            var playersAvailable = await _dataAccess.RunAQuery<PlayersAvailableDto, dynamic>(sqlStatement, new { fixtureId }, _connectionString);
            fixtureDetails.AvailablePlayers = playersAvailable.ToList();
            return fixtureDetails;
        }

        public async Task<MyAvailabilitySummaryDto> GetPlayerAvailabilitySummary(int playerId)
        {
            var player = await GetPlayer(playerId);
            if (player is null)
            {
                return null;
            }
            var me = new MyAvailabilitySummaryDto
            {
                PlayerId = player.Id,
                Fullname = $"{player.Firstname} {player.Lastname}"
            };

            var sqlStatement = "SELECT f.Id AS 'FixtureId', f.Opponent + ' (' + f.StartTime + ')' AS 'FixtureDetail'  FROM Fixture AS f JOIN Availability AS a ON f.Id = a.FixtureId WHERE a.PlayerId = @playerId";
            IEnumerable<MyFixturesDto> myFixtures;
            try
            {
                myFixtures = await _dataAccess.RunAQuery<MyFixturesDto, dynamic>(sqlStatement, new { playerId }, _connectionString);
            }
            catch (Exception ex)
            {

                throw;
            }

            if (myFixtures is null)
            {
                return null;
            }

            me.MyFixtures = myFixtures.OrderBy(f => f.DateOfFixture).ToList();

            return me;

        }

        public async Task<MeDTO> MyAvailabilitySummary(int playerId)
        {
            var allfixtures = await GetAllFixtures();
            var currentAvailability = await GetPlayerAvailabilitySummary(playerId);
            var me = new MeDTO();
            var lst = new List<AvailableToggleDTO>();
            if (currentAvailability != null)
            {
                me.PlayerId = currentAvailability.PlayerId;
                me.Fullname = currentAvailability.Fullname;
                foreach (var f in allfixtures)
                {
                    var availabilityFlag = false;
                    if (currentAvailability.MyFixtures.Any(x => x.FixtureId == f.Id))
                    {
                        availabilityFlag = true;
                    }
                    lst.Add(new AvailableToggleDTO
                    {
                        FixtureId = f.Id,
                        Opponent = f.Opponent,
                        DateOfFixture = f.DateOfFixture,
                        Venue = f.Venue == "Home" ? "H" : "A",
                        StartTime = f.StartTime,
                        CurrentAvailabilityStatus = availabilityFlag
                    });
                }
            }
            me.PlayersCurrentAvailability = lst.OrderBy(x => x.DateOfFixture).ToList();
            return me;
        }

        public async Task<List<FixtureCountSummaryDto>> FixtureAvailabilityCounts()
        {
            var sqlStatement1 = "SELECT a.FixtureID, COUNT(a.FixtureID) as 'PlayersAvailableCount' FROM AVAILABILITY AS a GROUP BY a.FixtureId ORDER BY COUNT(a.FixtureID)";
            var counts = await _dataAccess.RunAQuery<FixtureCountDto, dynamic>(sqlStatement1, new { }, _connectionString);

            var allFixtures = await GetAllFixtures();
            var finalCount = new List<FixtureCountSummaryDto>();

            foreach (var fixture in allFixtures)
            {
                var playerForFixture = counts.FirstOrDefault(x => x.FixtureId == fixture.Id);
                var playersAvailable = 0;
                if (playerForFixture != null)
                {
                    playersAvailable = playerForFixture.PlayersAvailableCount;
                }
                finalCount.Add(new FixtureCountSummaryDto
                {
                    FixtureId = fixture.Id,
                    FixtureDetail = $"{fixture.Opponent} ({fixture.Venue}) - {fixture.StartTime}",
                    DateOfFixture = fixture.DateOfFixture,
                    PlayersAvailable = playersAvailable,
                    PlayersRequired = fixture.NumberOfPlayersRequired
                });
            }
            return finalCount.OrderBy(fc => fc.DateOfFixture).ToList();
        }

        public async Task<ExecuteCommandResponseDto> SaveAvailability(AddAvailabilityDto input)
        {
            var response = new ExecuteCommandResponseDto();
            //var sqlStatement1 = "SELECT COUNT(*) AS 'Total' FROM Availability AS A WHERE a.FixtureId = @fixtureId AND a.playerId = @playerId;";
            var sqlStatement = "usp_TogglePlayerAvailability";

            try
            {
                await _dataAccess.ExecuteACommand(
                          sqlStatement,
                          new { fixtureId = input.FixtureId, playerid = input.PlayerId, isAvailable = input.IsAvailable },
                          _connectionString,
                          System.Data.CommandType.StoredProcedure);
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.ExecutionStatus = SMS.Shared.Enums.ExecuteCommandEnum.InternalException;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
        #endregion
    }
}