
using Blazored.LocalStorage;
using ExamTest.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using webAssemblyExam.Services;

namespace webAssemblyExam
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
         public async Task<HttpResponseMessage> Register(RegisterModel registerModel)
         {
            var json = JsonSerializer.Serialize(registerModel);

            var response = await _httpClient.PostAsync("https://localhost:7069/register", new StringContent(json, Encoding.UTF8, "application/json"));

            return response.EnsureSuccessStatusCode();
            //return loginResult ?? new RegisterResult();      
         }

        async Task<AuthenticationState> LoginAndGetAuthenticationState(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync(
                "login?useCookies=true", new  
                {
                    loginModel.Email,
                    loginModel.Password
                });

            return null;
        }

        //NotifyAuthenticationStateChanged(LoginAndGetAuthenticationState());

     


        public async Task<HttpResponseMessage> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("https://localhost:7069/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var s = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<TokenResponse>(s, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
      
           /* if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }
           */
             await _localStorage.SetItemAsync("authToken", loginResult.AccessToken);

         
            ((CustomAuthStateProvider)_authenticationStateProvider).AuthenticateUser(loginModel.Email);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

            return response.EnsureSuccessStatusCode();
          
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)_authenticationStateProvider).SignOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
