using Refit;
using SMS.Shared.DTOs.Fixtures;

namespace StanwayRonnies.Services;

public interface IFixtureData
{
    [Get("/Fixture")]
    Task<IEnumerable<FixtureSummaryDto>> GetFixtures();


}
