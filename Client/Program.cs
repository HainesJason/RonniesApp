using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp.Client;
using StanwayRonnies.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });
builder.Services.AddRefitClient<IPlayerData>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://smsapi20230130193419.azurewebsites.net/api");
});
builder.Services.AddRefitClient<IFixtureData>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://smsapi20230130193419.azurewebsites.net/api");
});
builder.Services.AddRefitClient<IAvailabilityData>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://smsapi20230130193419.azurewebsites.net/api");
});
await builder.Build().RunAsync();

//https://smsapi20230130193419.azurewebsites.net/api/Fixture
