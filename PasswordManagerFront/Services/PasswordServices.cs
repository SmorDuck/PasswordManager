using System.Net.Http.Json;
using Domain.Model;

namespace PasswordManagerFront.Services
{
    public class PasswordServices
    {
        private readonly HttpClient _httpClient;

        public PasswordServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PasswordEntity>> GetPasswordEntities()
        {
            return await _httpClient.GetFromJsonAsync<List<PasswordEntity>>("https://localhost:7153/api/PasswordControllers");
        }

        public async Task<HttpResponseMessage> AddPasswordEntity(PasswordEntity entity)
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7153/api/PasswordControllers", entity);
        }
    }
}