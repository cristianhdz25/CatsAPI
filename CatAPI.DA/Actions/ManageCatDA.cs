using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.DA;
using CatAPI.BW.Interfaces.SG;
using CatAPI.DA.Context;
using CatAPI.DA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatAPI.DA.Actions
{
    // This class handles data access operations related to cat breeds.
    // It includes methods for managing, registering, retrieving, updating, and deleting cat breeds.
    public class ManageCatDA : IManageCatDA
    {
        private readonly CatAPIDbContext _context; // The database context for the CatAPI application
        private readonly ITheCatAPISG _theCatAPISG; // Service to interact with The Cat API

        // Constructor to inject the database context and the service for The Cat API
        public ManageCatDA(CatAPIDbContext context, ITheCatAPISG theCatAPISG)
        {
            _context = context;
            _theCatAPISG = theCatAPISG;
        }

        // This method loads cat breeds from The Cat API and stores them in the database.
        public async Task LoadCatBreedsAsync()
        {
            // Get the data from The Cat API
            var breeds = await _theCatAPISG.GetCatBreedsAsync();

            // Check and load the data into the database
            foreach (var breed in breeds)
            {
                // Check if the breed already exists in the database
                var existingBreed = await _context.CatBreeds
                    .FirstOrDefaultAsync(b => b.Name == breed.Name);

                if (existingBreed == null)
                {
                    // Insert the new breed into the database
                    var newBreed = new CatBreedDA
                    {
                        Name = breed.Name,
                        Temperament = breed.Temperament,
                        Origin = breed.Origin,
                        Description = breed.Description,
                        ImageURL = breed.ImageURL ?? string.Empty // Handle null ImageURL
                    };

                    _context.CatBreeds.Add(newBreed);
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        // Registers a new cat breed in the database if it doesn't already exist
        public async Task<bool> RegisterCatBreedAsync(CatBreed catBreed)
        {
            // Check if the breed already exists in the database
            var existingBreed = await _context.CatBreeds
                .FirstOrDefaultAsync(b => b.Name == catBreed.Name);
            if (existingBreed != null)
            {
                return false; // Breed already exists
            }

            // Insert the new breed into the database
            var newBreed = new CatBreedDA
            {
                Name = catBreed.Name,
                Temperament = catBreed.Temperament,
                Origin = catBreed.Origin,
                Description = catBreed.Description,
                ImageURL = catBreed.ImageURL ?? string.Empty // Handle null ImageURL
            };

            await _context.CatBreeds.AddAsync(newBreed);

            // Save changes and return whether the operation was successful
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        // Retrieves a paginated list of cat breeds from the database
        public async Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize)
        {
            // Check if the cat breeds table is empty
            var catBreedsExist = await _context.CatBreeds.AnyAsync();
            if (!catBreedsExist)
            {
                // If the table is empty, load the data from The Cat API
                await LoadCatBreedsAsync();
            }

            var query = _context.CatBreeds.AsQueryable();

            // Calculate the total number of items
            int totalCount = await query.CountAsync();

            // Apply pagination to the query
            var breeds = await query
                .OrderBy(b => b.IdCatBreed) // Order by a unique field
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new CatBreed
                {
                    IdCatBreed = b.IdCatBreed,
                    Name = b.Name,
                    Temperament = b.Temperament,
                    Origin = b.Origin,
                    Description = b.Description,
                    ImageURL = b.ImageURL
                })
                .ToListAsync();

            // Return the paginated result
            return new PaginatedResult<CatBreed>
            {
                Items = breeds,
                TotalCount = totalCount
            };
        }

        // Retrieves a cat breed by its ID
        public async Task<CatBreed> GetCatBreedByIdAsync(int id)
        {
            var breed = await _context.CatBreeds.FirstOrDefaultAsync(b => b.IdCatBreed == id);
            if (breed == null)
            {
                return new CatBreed(); // Return an empty CatBreed if not found
            }

            return new CatBreed
            {
                Name = breed.Name,
                Temperament = breed.Temperament,
                Origin = breed.Origin,
                Description = breed.Description,
                ImageURL = breed.ImageURL
            };
        }

        // Updates an existing cat breed in the database
        public async Task<bool> UpdateCatBreedAsync(CatBreed catBreed)
        {
            var existingBreed = await _context.CatBreeds
                .FirstOrDefaultAsync(b => b.IdCatBreed == catBreed.IdCatBreed);
            if (existingBreed == null)
            {
                return false; // Breed not found
            }

            // Update the breed's properties
            existingBreed.Name = catBreed.Name;
            existingBreed.Temperament = catBreed.Temperament;
            existingBreed.Origin = catBreed.Origin;
            existingBreed.Description = catBreed.Description;
            existingBreed.ImageURL = catBreed.ImageURL ?? string.Empty;

            // Save changes and return whether the update was successful
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        // Deletes a cat breed from the database by its ID
        public async Task<bool> DeleteCatBreedAsync(int id)
        {
            var existingBreed = await _context.CatBreeds
                .FirstOrDefaultAsync(b => b.IdCatBreed == id);
            if (existingBreed == null)
            {
                return false; // Breed not found
            }

            // Remove the breed from the database
            _context.CatBreeds.Remove(existingBreed);

            // Save changes and return whether the deletion was successful
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }
    }
}
