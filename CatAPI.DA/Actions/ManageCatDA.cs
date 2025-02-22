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



    }
}
