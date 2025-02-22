using CatAPI.BC.Models;

namespace CatAPI.BW.Interfaces.BW
{
    public interface IAuthUserBW
    {
        Task<string> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password);
    }
}
