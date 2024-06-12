using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using webAssemblyExam;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using webAssemblyExam.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/*var handler = new HttpClientHandler
{
    UseCookies = true,
    CookieContainer = new System.Net.CookieContainer()
};

builder.Services.AddScoped(sp => new HttpClient(handler)
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
*/


/*builder.Services.AddHttpClient("BlazorWasmAppCookieAuth.ServerAPI",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
   .AddHttpMessageHandler<CookieHandler>();
*/

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthService, AuthService>();
/*builder.Services.AddTransient<CookieHandler>();
builder.Services.AddHttpClient(
    "Auth",
    opt => opt.BaseAddress = new Uri(builder.Configuration["AuthUrl"]!))
    .AddHttpMessageHandler<CookieHandler>();
*/

builder.Services.AddSingleton<LocalStorageService>();



builder.Services.AddAuthorizationCore();





builder.Services.AddCascadingAuthenticationState();



builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://localhost:7069/"; // or your local auth server
    options.ProviderOptions.ClientId = "your-client-id";
    options.ProviderOptions.RedirectUri = "https://localhost:7172/authentication/login-callback";
    options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:7172/authentication/logout-callback";


    options.ProviderOptions.AdditionalProviderParameters.Add("useCookies", "true");
    options.ProviderOptions.AdditionalProviderParameters.Add("useSessionCookies", "true");
});


/*builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});
*/

builder.Services.AddBlazoredLocalStorage();



builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
