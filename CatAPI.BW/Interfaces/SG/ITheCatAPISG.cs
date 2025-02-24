using CatAPI.BC.Models;

namespace CatAPI.BW.Interfaces.SG
{
    // This interface defines the contract for interacting with the Cat API service layer.
    public interface ITheCatAPISG
    {
        // Asynchronously retrieves a list of cat breeds from the API.
        Task<List<CatBreed>> GetCatBreedsAsync();
    }
}
