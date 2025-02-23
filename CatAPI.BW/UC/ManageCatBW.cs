using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.BW;
using CatAPI.BW.Interfaces.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.BW.UC
{
    public class ManageCatBW : IManageCatBW
    {
        private readonly IManageCatDA _manageCatDA;

        public ManageCatBW(IManageCatDA manageCatDA)
        {
            _manageCatDA = manageCatDA;
        }

        public async Task<ResponseModel> RegisterCatBreedAsync(CatBreed catBreed)
        {
            bool result = await _manageCatDA.RegisterCatBreedAsync(catBreed);
            if (result)
            {
                return new ResponseModel
                {
                    Success = true,
                    Message = "Cat breed registered successfully"
                };
            }
            else
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Error registering cat breed"
                };
            }
        }

        public async Task<PaginatedResult<CatBreed>> GetCatBreedsAsync(int page, int pageSize)
        {
            return await _manageCatDA.GetCatBreedsAsync(page,pageSize);
        }

        public async Task<CatBreed> GetCatBreedByIdAsync(int id)
        {
            return await _manageCatDA.GetCatBreedByIdAsync(id);
        }

        public async Task<ResponseModel> UpdateCatBreedAsync(CatBreed catBreed)
        {
            bool result = await _manageCatDA.UpdateCatBreedAsync(catBreed);
            if (result)
            {
                return new ResponseModel
                {
                    Success = true,
                    Message = "Cat breed updated successfully"
                };
            }
            else
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Error updating cat breed"
                };
            }
        }

        public async Task<ResponseModel> DeleteCatBreedAsync(int id)
        {
            bool result = await _manageCatDA.DeleteCatBreedAsync(id);
            if (result)
            {
                return new ResponseModel
                {
                    Success = true,
                    Message = "Cat breed deleted successfully"
                };
            }
            else
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Error deleting cat breed"
                };
            }
        }
    }
}
