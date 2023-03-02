using Refit;
using SMS.Shared.DTOs.Availability;

namespace StanwayRonnies.Services;

public interface IAvailabilityData
{
    [Get("/Availability/FixtureAvailabilityCounts")]
    Task<IEnumerable<FixtureCountSummaryDto>> FixtureAvailabilitySummary();

    [Get("/Availability/PlayersAvailableForFixture/{fixtureId}")]
    Task<PlayersAvailableForFixtureDto> FixtureAvailabilityDetail(int fixtureId);

    [Get("/Availability/myAvailability/{id}")]
    Task<MeDTO> MyAvailability(int id);

    [Post("/Availability")]
    Task SaveAvailability([Body] AddAvailabilityDto entry);
}
