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
    }
}
