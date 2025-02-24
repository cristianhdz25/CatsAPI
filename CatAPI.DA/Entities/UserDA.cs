using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatAPI.DA.Entities
{
    // This class is used to represent the User entity in the database.
    [Table("Users")] // Specifies the table name in the database
    public class UserDA
    {
        // The primary key for the Users table, with automatic identity generation
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        // The username of the user. This field is required.
        [Required]
        public string Username { get; set; } = string.Empty;

        // The hashed password for the user. This field is required.
        [Required]
        public byte[] PasswordHash { get; set; } = new byte[0];

        // The salt used for password hashing. This field is required.
        [Required]
        public byte[] Salt { get; set; } = new byte[0];
    }
}
