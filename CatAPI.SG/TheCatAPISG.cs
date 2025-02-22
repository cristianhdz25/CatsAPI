using CatAPI.BC.Constants;
using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.SG;
using System.Text.Json;


namespace CatAPI.SG
{
    public class TheCatAPISG : ITheCatAPISG
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public TheCatAPISG(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = ApiKeyTheCatsAPI.ApiKey;
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        }

        // Method to get the cat breeds from The Cat API
        public async Task<List<CatBreed>> GetCatBreedsAsync()
        {
            var response = await _httpClient.GetAsync(URLTheCatAPIConstant.URL);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error on {URLTheCatAPIConstant.URL}");

            var content = await response.Content.ReadAsStringAsync();

            JsonElement catBreeds = JsonSerializer.Deserialize<JsonElement>(content);

            List<CatBreed> catBreedsToReturn = new List<CatBreed>();

            foreach (var breed in catBreeds.EnumerateArray())
            {
                // Obtain the image property if it exists in the breed object
                JsonElement image = breed.TryGetProperty("image", out var imageElement) ? imageElement : default;

                //Map the CatBreed object
                CatBreed catBreed = new CatBreed
                {
                    Name = breed.TryGetProperty("name", out var nameElement) ? nameElement.GetString() ?? string.Empty : string.Empty,
                    Temperament = breed.TryGetProperty("temperament", out var temperamentElement) ? temperamentElement.GetString() ?? string.Empty : string.Empty,
                    Origin = breed.TryGetProperty("origin", out var originElement) ? originElement.GetString() ?? string.Empty : string.Empty,
                    Description = breed.TryGetProperty("description", out var descriptionElement) ? descriptionElement.GetString() ?? string.Empty : string.Empty,
                    ImageURL = image.ValueKind != JsonValueKind.Undefined && image.TryGetProperty("url", out var urlElement) ? urlElement.GetString() ?? string.Empty : string.Empty
                };

                catBreedsToReturn.Add(catBreed);
            }

            return catBreedsToReturn;
        }

    }
}
