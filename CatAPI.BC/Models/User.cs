
namespace CatAPI.BC.Models
{
    //This class is used to represent the User entity in the business logic
    public class User
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] Salt { get; set; } = new byte[0];

    }
}
