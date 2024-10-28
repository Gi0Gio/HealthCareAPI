using System.Data;

namespace giowebtestAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }  // Relation to Role
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }  // Indicates if the user is active

        public Role Role { get; set; }
    }

}
