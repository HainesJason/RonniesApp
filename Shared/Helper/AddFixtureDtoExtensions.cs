using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.Models;

namespace SMS.Shared.Helper
{

    public static class AddFixtureDtoExtensions
    {
        /// <summary>
        /// Converts a AddFixtureDto to a Fixture
        /// </summary>
        /// <param name="value">The dto object</param>
        /// <returns>The model object</returns>
        public static Fixture ToFixtureModel(this AddFixtureDto value)
        {
            return new Fixture
            {
                Opponent = value.Opponent,
                DateOfFixture = value.DateOfFixture,
                NumberOfPlayersRequired = value.NumberOfPlayersRequired,
                Postcode = value.Postcode,
                StartTime = value.StartTime,
                Venue = value.Venue,
            };
        }
    }
}