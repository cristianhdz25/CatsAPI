using CatAPI.BC.Models;

namespace CatAPI.BW.Interfaces.BW
{
    // This interface defines the contract for business logic operations related to user authentication.
    public interface IAuthUserBW
    {
        // Authenticates a user based on username and password, returning a JWT token if successful.
        Task<string> LoginAsync(string username, string password);

        // Registers a new user and returns a response model indicating success or failure.
        Task<ResponseModel> RegisterAsync(string username, string password);
    }
}
