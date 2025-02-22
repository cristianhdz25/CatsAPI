using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.DA.Entities
{
    [Table("CatBreeds")]
    public class CatBreedDA
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCatBreed { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Temperament { get; set; } = string.Empty;
        [Required]
        public string Origin { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string ImageURL { get; set; } = string.Empty;
    }
}
