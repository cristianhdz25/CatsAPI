using CatAPI.DA.Entities;
using Microsoft.EntityFrameworkCore;


namespace CatAPI.DA.Context
{
    public class CatAPIDbContext : DbContext
    {
        public CatAPIDbContext(DbContextOptions<CatAPIDbContext> options) : base(options)
        {
        }
        public DbSet<UserDA> Users { get; set; }

        public DbSet<CatBreedDA> CatBreeds { get; set; }
    }
    
    
}
