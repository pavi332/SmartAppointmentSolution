using System.Net.Http.Json;
using SmartAppointment.Web.Models;

namespace SmartAppointment.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthService(HttpClient http, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _http = _httpClientFactory.CreateClient("BaseUrl");
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                // Assuming the API returns a JSON object with a "token" property
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result?.Token;
            }
            return null;
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/register", model);
            return response.IsSuccessStatusCode;
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
