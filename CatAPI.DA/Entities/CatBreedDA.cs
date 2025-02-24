using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatAPI.DA.Entities
{
    // This class represents the CatBreed entity in the database.
    [Table("CatBreeds")] // Specifies the table name in the database for cat breeds
    public class CatBreedDA
    {
        // The primary key for the CatBreeds table, with automatic identity generation
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCatBreed { get; set; }

        // The name of the cat breed. This field is required.
        [Required]
        public string Name { get; set; } = string.Empty;

        // The temperament of the cat breed. This field is required.
        [Required]
        public string Temperament { get; set; } = string.Empty;

        // The origin of the cat breed. This field is required.
        [Required]
        public string Origin { get; set; } = string.Empty;

        // A description of the cat breed. This field is required.
        [Required]
        public string Description { get; set; } = string.Empty;

        // The URL of an image representing the cat breed. This field is required.
        [Required]
        public string ImageURL { get; set; } = string.Empty;
    }
}
