using CatAPI.BC.Models;
using System.Threading.Tasks;

namespace CatAPI.BW.Interfaces.DA
{
    // This interface defines the contract for data access operations related to user management.
    public interface IManageUserDA
    {
        // Retrieves a user by their username asynchronously.
        Task<User> GetByUsernameAsync(string username);

        // Registers a new user asynchronously.
        Task<bool> RegisterAsync(User user);
    }
}
