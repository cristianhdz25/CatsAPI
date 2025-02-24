using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.BW;
using CatAPI.BW.Interfaces.DA;

namespace CatAPI.BW.UC
{
    // This class handles business logic for managing cat breeds, including registration, retrieval, 
    // update, and deletion operations.
    public class ManageCatBW : IManageCatBW
    {
        private readonly IManageCatDA _manageCatDA;  // Interface for data access related to cat breeds

        // Constructor to inject the data access interface
        public ManageCatBW(IManageCatDA manageCatDA)
        {
            _manageCatDA = manageCatDA;
        }

        // Method to register a new cat breed.
        // Returns a ResponseModel indicating success or failure.
        public async Task<ResponseModel> RegisterCatBreedAsync(CatBreed catBreed)
        {
            // Attempt to register the cat breed using the data access layer
            bool result = await _manageCatDA.RegisterCatBreedAsync(catBreed);

            // Return a success or failure response based on the result
            if (result)
            {
                return new ResponseModel
                {
                    Success = true,
                    Message = "Cat breed registered successfully"
                };
            }
            else
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Cat breed name already exists"
                };
            }
        }

        // Method to retrieve a paginated list of cat breeds.
        public async Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize)
        {
            // Retrieve and return the paginated cat breeds from the data access layer
            return await _manageCatDA.GetCatBreedsAsync(page, pageSize);
        }

        // Method to retrieve a cat breed by its ID.
        public async Task<CatBreed> GetCatBreedByIdAsync(int id)
        {
            // Retrieve and return the cat breed with the given ID
            return await _manageCatDA.GetCatBreedByIdAsync(id);
        }

        // Method to update an existing cat breed.
        // Returns a ResponseModel indicating success or failure.
        public async Task<ResponseModel> UpdateCatBreedAsync(CatBreed catBreed)
        {
            // Attempt to update the cat breed using the data access layer
            bool result = await _manageCatDA.UpdateCatBreedAsync(catBreed);

            // Return a success or failure response based on the result
            if (result)
            {
                return new ResponseModel
                {
                    Success = true,
                    Message = "Cat breed updated successfully"
                };
            }
            else
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Error updating cat breed"
                };
            }
        }

        // Method to delete a cat breed by its ID.
        // Returns a ResponseModel indicating success or failure.
        public async Task<ResponseModel> DeleteCatBreedAsync(int id)
        {
            // Attempt to delete the cat breed using the data access layer
            bool result = await _manageCatDA.DeleteCatBreedAsync(id);

            // Return a success or failure response based on the result
            if (result)
            {
                return new ResponseModel
                {
                    Success = true,
                    Message = "Cat breed deleted successfully"
                };
            }
            else
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Error deleting cat breed"
                };
            }
        }
    }
}
