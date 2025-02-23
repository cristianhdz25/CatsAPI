﻿using CatAPI.BC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.BW.Interfaces.DA
{
    public interface IManageCatDA
    {
        Task LoadCatBreedsAsync();
        Task<bool> RegisterCatBreedAsync(CatBreed catBreed);
        Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize);
        Task<CatBreed> GetCatBreedByIdAsync(int id);
        Task<bool> UpdateCatBreedAsync(CatBreed catBreed);
        Task<bool> DeleteCatBreedAsync(int id);

    }
}
