using CatAPI.DA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatAPI.DA.Context
{
    // This class represents the database context for the CatAPI application.
    // It inherits from DbContext and provides access to the application's database tables.
    public class CatAPIDbContext : DbContext
    {
        // Constructor that takes DbContextOptions and passes it to the base class constructor
        // to configure the context options for the database.
        public CatAPIDbContext(DbContextOptions<CatAPIDbContext> options) : base(options)
        {
        }

        // DbSet for Users table, representing the collection of UserDA entities in the database.
        public DbSet<UserDA> Users { get; set; }

        // DbSet for CatBreeds table, representing the collection of CatBreedDA entities in the database.
        public DbSet<CatBreedDA> CatBreeds { get; set; }
    }
}
