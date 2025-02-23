using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.BC.Models
{
    public class PaginatedResult<T>
    {
        public List<T>? Items { get; set; }
        public int TotalCount { get; set; }
    }
}
