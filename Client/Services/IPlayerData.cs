using Refit;
using SMS.Shared.DTOs.Availability;
using SMS.Shared.DTOs.Players;

namespace StanwayRonnies.Services;

public interface IPlayerData
{
    [Get("/Player")]
    Task<IEnumerable<PlayerSummaryDto>> GetPlayers();

   
    [Get("/Player/myAvailability/{id}")]
    Task<MeDTO> MyAvailability(int id);
}
