using Refit;
using SMS.Shared.Models;

namespace StanwayRonnies.Services;

public interface IEventData
{
    [Get("/Event")]
    Task<IEnumerable<Event>> GetAllEvents();

    

    [Post("/Event")]
    Task SaveEvent([Body] Event entry);
}
