using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.DA;
using CatAPI.DA.Context;
using CatAPI.DA.Entities;
using Microsoft.EntityFrameworkCore;


namespace CatAPI.DA.Actions
{
    public class ManageUserDA : IManageUserDA
    {
        private readonly CatAPIDbContext _context;
        public ManageUserDA(CatAPIDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            var userDA = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (userDA == null)
            {
                return new User();
            }

            User user = new User
            {
                Username = userDA.Username,
                PasswordHash = userDA.PasswordHash,
                Salt = userDA.Salt
            };

            return user;
        }
        public async Task<bool> RegisterAsync(User user)
        {
            UserDA userDA = new UserDA
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Salt = user.Salt
            };

            await _context.Users.AddAsync(userDA);

            bool result = await _context.SaveChangesAsync() > 0;

            return result;
        }
    }

}
