namespace CatAPI.BC.Models
{
    // This class represents a paginated result containing a list of items and the total count.
    public class PaginatedResult<T>
    {
        public List<T>? Items { get; set; }
        public int TotalCount { get; set; }
    }
}
