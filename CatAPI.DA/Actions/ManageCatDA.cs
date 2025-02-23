using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.DA;
using CatAPI.BW.Interfaces.SG;
using CatAPI.DA.Context;
using CatAPI.DA.Entities;
using CatAPI.SG;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.DA.Actions
{
    public class ManageCatDA : IManageCatDA
    {
        private readonly CatAPIDbContext _context;
        private readonly ITheCatAPISG _theCatAPISG;
        public ManageCatDA(CatAPIDbContext context, ITheCatAPISG theCatAPISG)
        {
            _context = context;
            _theCatAPISG = theCatAPISG;
        }

        public async Task LoadCatBreedsAsync()
        {
            // Obtener los datos de The Cat API
            var breeds = await _theCatAPISG.GetCatBreedsAsync();

            // Verificar y cargar los datos en la base de datos
            foreach (var breed in breeds)
            {
                // Verificar si la raza ya existe en la base de datos
                var existingBreed = await _context.CatBreeds
                    .FirstOrDefaultAsync(b => b.Name == breed.Name);

                if (existingBreed == null)
                {
                    // Insertar la nueva raza
                    var newBreed = new CatBreedDA
                    {
                        Name = breed.Name,
                        Temperament = breed.Temperament,
                        Origin = breed.Origin,
                        Description = breed.Description,
                        ImageURL = breed.ImageURL ?? string.Empty // Manejar el caso en que Image sea null
                    };

                    _context.CatBreeds.Add(newBreed);
                }
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RegisterCatBreedAsync(CatBreed catBreed)
        {
            // Verify if the breed already exists in the database
            var existingBreed = await _context.CatBreeds
                .FirstOrDefaultAsync(b => b.Name == catBreed.Name);
            if (existingBreed != null)
            {
                return false;// Breed already exists 
            }
            // Insert
            var newBreed = new CatBreedDA
            {
                Name = catBreed.Name,
                Temperament = catBreed.Temperament,
                Origin = catBreed.Origin,
                Description = catBreed.Description,
                ImageURL = catBreed.ImageURL ?? string.Empty // Handle the case where Image is null
            };

            await _context.CatBreeds.AddAsync(newBreed);

            bool result = await _context.SaveChangesAsync() > 0;

            return result;

        }

        public async Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize)
        {
            var query = _context.CatBreeds.AsQueryable();

            //Calculates the total number of items
            int totalCount = await query.CountAsync();

            //Applies pagination
            var breeds = await query
                .OrderBy(b => b.IdCatBreed) // Need to order by a unique field
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

        public async Task<CatBreed> GetCatBreedByIdAsync(int id)
        {
            var breed = await _context.CatBreeds.FirstOrDefaultAsync(b => b.IdCatBreed == id);
            if (breed == null)
            {
                return null;
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

        public async Task<bool> UpdateCatBreedAsync(CatBreed catBreed)
        {
            var existingBreed = await _context.CatBreeds
                .FirstOrDefaultAsync(b => b.IdCatBreed == catBreed.IdCatBreed);
            if (existingBreed == null)
            {
                return false;
            }
            existingBreed.Name = catBreed.Name;
            existingBreed.Temperament = catBreed.Temperament;
            existingBreed.Origin = catBreed.Origin;
            existingBreed.Description = catBreed.Description;
            existingBreed.ImageURL = catBreed.ImageURL ?? string.Empty;
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> DeleteCatBreedAsync(int id)
        {
            var existingBreed = await _context.CatBreeds
                .FirstOrDefaultAsync(b => b.IdCatBreed == id);
            if (existingBreed == null)
            {
                return false;
            }
            _context.CatBreeds.Remove(existingBreed);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

    }
}
