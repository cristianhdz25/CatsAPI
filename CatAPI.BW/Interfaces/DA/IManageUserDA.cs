using CatAPI.BC.Models;

namespace CatAPI.BW.Interfaces.DA
{
    public interface IManageUserDA
    {
        Task<User> GetByUsernameAsync(string username);
        Task<bool> RegisterAsync(User user);
    }
}
