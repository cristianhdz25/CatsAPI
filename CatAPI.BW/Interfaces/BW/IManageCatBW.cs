using CatAPI.BC.Models;

namespace CatAPI.BW.Interfaces.BW
{
    // Interface for managing cat breeds in the business layer.
    public interface IManageCatBW
    {
        // Registers a new cat breed and returns a response model indicating success or failure.
        Task<ResponseModel> RegisterCatBreedAsync(CatBreed catBreed);

        // Retrieves a paginated list of cat breeds.
        Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize);

        // Retrieves a specific cat breed by its ID.
        Task<CatBreed> GetCatBreedByIdAsync(int id);

        // Updates an existing cat breed and returns a response model indicating success or failure.
        Task<ResponseModel> UpdateCatBreedAsync(CatBreed catBreed);

        // Deletes a cat breed by its ID and returns a response model indicating success or failure.
        Task<ResponseModel> DeleteCatBreedAsync(int id);
    }
}
