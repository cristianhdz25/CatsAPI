using CatAPI.BC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.BW.Interfaces.BW
{
    public interface IManageCatBW
    {
        Task<ResponseModel> RegisterCatBreedAsync(CatBreed catBreed);
        Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize);
        Task<CatBreed> GetCatBreedByIdAsync(int id);
        Task<ResponseModel> UpdateCatBreedAsync(CatBreed catBreed);

        Task<ResponseModel> DeleteCatBreedAsync(int id);
    }
}
