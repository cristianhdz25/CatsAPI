using CatAPI.BC.Constants;
using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.SG;
using System.Text.Json;

namespace CatAPI.SG
{
    public class TheCatAPISG : ITheCatAPISG
    {
        private readonly HttpClient _httpClient; // HttpClient instance to make API requests
        private readonly string _apiKey; // API key for authentication with The Cat API

        // Constructor that initializes the HttpClient and adds the API key to the request headers
        public TheCatAPISG(HttpClient httpClient)
        {
            _httpClient = httpClient; // Assign the HttpClient instance
            _apiKey = ApiKeyTheCatsAPI.ApiKey; // Retrieve the API key from the constant
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey); // Add API key to the request headers
        }

        /// <summary>
        /// Method to get the list of cat breeds from The Cat API.
        /// </summary>
        /// <returns>List of CatBreed objects representing different cat breeds.</returns>
        public async Task<List<CatBreed>> GetCatBreedsAsync()
        {
            // Send an HTTP GET request to fetch the cat breeds from the API
            var response = await _httpClient.GetAsync(URLTheCatAPIConstant.URL);

            // Check if the response status code is successful
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error on {URLTheCatAPIConstant.URL}");

            // Read the response content as a string
            var content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON content into a JsonElement
            JsonElement catBreeds = JsonSerializer.Deserialize<JsonElement>(content);

            // List to hold the mapped CatBreed objects
            List<CatBreed> catBreedsToReturn = new List<CatBreed>();

            // Iterate through the JSON array of cat breeds
            foreach (var breed in catBreeds.EnumerateArray())
            {
                // Retrieve the "image" property, if it exists
                JsonElement image = breed.TryGetProperty("image", out var imageElement) ? imageElement : default;

                // Map the properties from the JSON response to the CatBreed object
                CatBreed catBreed = new CatBreed
                {
                    Name = breed.TryGetProperty("name", out var nameElement) ? nameElement.GetString() ?? string.Empty : string.Empty,
                    Temperament = breed.TryGetProperty("temperament", out var temperamentElement) ? temperamentElement.GetString() ?? string.Empty : string.Empty,
                    Origin = breed.TryGetProperty("origin", out var originElement) ? originElement.GetString() ?? string.Empty : string.Empty,
                    Description = breed.TryGetProperty("description", out var descriptionElement) ? descriptionElement.GetString() ?? string.Empty : string.Empty,
                    ImageURL = image.ValueKind != JsonValueKind.Undefined && image.TryGetProperty("url", out var urlElement) ? urlElement.GetString() ?? string.Empty : string.Empty
                };

                // Add the CatBreed object to the list
                catBreedsToReturn.Add(catBreed);
            }

            // Return the list of CatBreed objects
            return catBreedsToReturn;
        }
    }
}
