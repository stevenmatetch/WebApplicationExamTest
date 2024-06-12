using ExamTest.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace webAssemblyExam
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly IHttpClientFactory _httpClientFactory;
        private UserProfileDto? _userProfileDto;
        /* public CustomAuthStateProvider(IHttpClientFactory httpClientFactory)
         {
             _httpClientFactory = httpClientFactory;
         }
        */

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_userProfileDto == null)
            {
                //IHttpClientFactory _httpClientFactory = HttpClient("");

                //var client = _httpClientFactory.CreateClient("BlazorWasmAppCookieAuth.ServerAPI");

                var client = new HttpClient();

                var response = await client.GetAsync("https://localhost:7069/user-profile");

                if (response.IsSuccessStatusCode)
                {
                    _userProfileDto = await response.Content.ReadFromJsonAsync<UserProfileDto>();

                    var identity = new ClaimsIdentity(
                        [
                        new Claim(ClaimTypes.Email, _userProfileDto?.Email ?? ""),
                        new Claim(ClaimTypes.Name, _userProfileDto ?.Name ?? ""),
                        new Claim("UserId", _userProfileDto?.ToString() ?? "")
                        ], "AuthCookie");

                    claimsPrincipal = new ClaimsPrincipal(identity);
                    //NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                }
            }
            return new AuthenticationState(claimsPrincipal);
        }

        public void AuthenticateUser(string userIdentifier)
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, userIdentifier),
        }, "Custom Authentication");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(user)));
        }

        public void SignOut()
        {
            _userProfileDto = null;
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
