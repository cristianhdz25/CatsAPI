using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatAPI.DA.Entities
{
    //This class is used to represent the User entity in the database
    [Table("Users")]
    public class UserDA
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = new byte[0];
        [Required]
        public byte[] Salt { get; set; } = new byte[0];
    }
}
