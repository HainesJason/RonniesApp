using BlazorApp.Shared.Models;
using Refit;
using SMS.Shared.Models;

namespace BlazorApp.Client.Services
{
    public interface IRegisteredUser
    {
        [Post("/RegisteredUsers")]
        Task Register([Body] RegisteredUsers registeredUser);
    }
}
