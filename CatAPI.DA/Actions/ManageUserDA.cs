using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.DA;
using CatAPI.DA.Context;
using CatAPI.DA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatAPI.DA.Actions
{
    // This class handles data access operations related to users.
    // It includes methods for retrieving and registering users in the database.
    public class ManageUserDA : IManageUserDA
    {
        private readonly CatAPIDbContext _context; // The database context for the CatAPI application

        // Constructor to inject the database context
        public ManageUserDA(CatAPIDbContext context)
        {
            _context = context;
        }

        // Retrieves a user by their username from the database
        public async Task<User> GetByUsernameAsync(string username)
        {
            // Find the user with the given username
            var userDA = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // If the user does not exist, return an empty User object
            if (userDA == null)
            {
                return new User();
            }

            // Map the data access object (UserDA) to the business object (User)
            User user = new User
            {
                Username = userDA.Username,
                PasswordHash = userDA.PasswordHash,
                Salt = userDA.Salt
            };

            // Return the user object
            return user;
        }

        // Registers a new user by adding them to the database
        public async Task<bool> RegisterAsync(User user)
        {
            // Create a new UserDA object to store in the database
            UserDA userDA = new UserDA
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Salt = user.Salt
            };

            // Add the new user to the Users table in the database
            await _context.Users.AddAsync(userDA);

            // Save changes and return whether the operation was successful
            bool result = await _context.SaveChangesAsync() > 0;

            return result;
        }
    }
}
