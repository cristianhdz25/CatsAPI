using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.BC.Models
{
    // This class represents a paginated result containing a list of items and the total count.
    public class PaginatedResult<T>
    {
        public List<T>? Items { get; set; }
        public int TotalCount { get; set; }
    }
}
