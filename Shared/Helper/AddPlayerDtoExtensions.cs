using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;

namespace SMS.Shared.Helper
{
    public static class AddPlayerDtoExtensions
    {
        /// <summary>
        /// Converts a AddPlayerDto to a Player model
        /// </summary>
        /// <param name="value">dto</param>
        /// <returns>model</returns>
        public static Player ToPlayerModel(this AddPlayerDto value)
        {
            return new Player
            {
                Firstname = value.Firstname,
                Lastname = value.Lastname,
                Email = value.Email,
                PhoneNumber = value.PhoneNumber,
                IsActivePlayer = value.IsActivePlayer,

            };
        }
    }
}