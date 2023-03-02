using SMS.Shared.DTOs;
using SMS.Shared.DTOs.Availability;
using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.BLL
{
    public interface ISMSLogic
    {
        #region Players Logic contracts
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersSummary();


        Task<Player> GetPlayer(int id);
        Task<ExecuteCommandResponseDto> SavePlayer(AddPlayerDto playerToSave);
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersByStatus(bool isActivePlayer);
        Task<ExecuteCommandResponseDto> DeletePlayer(int id);
        Task<ExecuteCommandResponseDto> AmendPlayer(int id, Player playerToChange);
        #endregion


        #region Fixture logic contracts
        Task<IEnumerable<FixtureSummaryDto>> GetAllFixtures();
        Task<Fixture> GetFixture(int FixtureId);

        Task<ExecuteCommandResponseDto> SaveFixture(AddFixtureDto fixtureToAdd);
        Task<ExecuteCommandResponseDto> AmendFixture(int id, Fixture fixtureToChange);

        Task<ExecuteCommandResponseDto> DeleteFixture(int fixtureId);
        #endregion

        #region availability logic contracts

        Task<MeDTO> MyAvailabilitySummary(int playerId);
        Task<PlayersAvailableForFixtureDto> GetPlayersAvailableForFixture(int fixtureId);
        Task<MyAvailabilitySummaryDto> GetPlayerAvailabilitySummary(int playerId);
        Task<List<FixtureCountSummaryDto>> FixtureAvailabilityCounts();

        Task<ExecuteCommandResponseDto> SaveAvailability(AddAvailabilityDto input);
        #endregion

    }
}