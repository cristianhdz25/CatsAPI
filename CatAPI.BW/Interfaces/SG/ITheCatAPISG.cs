using CatAPI.BC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.BW.Interfaces.SG
{
    public interface ITheCatAPISG
    {
        Task<List<CatBreed>> GetCatBreedsAsync();
    }
}
