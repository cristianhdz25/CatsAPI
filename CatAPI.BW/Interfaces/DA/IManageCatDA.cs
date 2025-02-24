using CatAPI.BC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatAPI.BW.Interfaces.DA
{
    // This interface defines the contract for data access operations related to cat breeds.
    public interface IManageCatDA
    {
        // Loads cat breeds asynchronously, typically from an external API.
        Task LoadCatBreedsAsync();

        // Registers a new cat breed asynchronously.
        Task<bool> RegisterCatBreedAsync(CatBreed catBreed);

        // Retrieves a paginated list of cat breeds based on the page number and size.
        Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize);

        // Retrieves a specific cat breed by its ID.
        Task<CatBreed> GetCatBreedByIdAsync(int id);

        // Updates the details of an existing cat breed asynchronously.
        Task<bool> UpdateCatBreedAsync(CatBreed catBreed);

        // Deletes a cat breed based on its ID asynchronously.
        Task<bool> DeleteCatBreedAsync(int id);
    }
}
