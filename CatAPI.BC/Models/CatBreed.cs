
namespace CatAPI.BC.Models
{
    public class CatBreed
    {
        public int IdCatBreed { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Temperament { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Description { get; set; } =  string.Empty;
        public string ImageURL { get; set; } = string.Empty;

    }
}
