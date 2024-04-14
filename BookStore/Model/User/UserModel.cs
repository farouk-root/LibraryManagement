using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Model.User
{
    public class UserModel
    {
        public long ID { get; set; }
        public long IDLIB { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public UserModel(long id, long IDLIB ,string username, string password, string role, string email, string contact, DateTime registrationDate, DateTime lastLoginDate)
        {
            ID = id;
            this.IDLIB = IDLIB;
            Username = username;
            Password = password;
            Role = role;
            Email = email;
            Contact = contact;
            RegistrationDate = registrationDate;
            LastLoginDate = lastLoginDate;
        }
        public UserModel(long IDLIB ,string username, string password, string role, string email, string contact, DateTime registrationDate)
        {
            this.IDLIB = IDLIB;
            this.Username = username;
            this.Password = password;
            this.Role = role;
            this.Email = email;
            this.Contact = contact;
            this.RegistrationDate = registrationDate;
            this.LastLoginDate = new DateTime(2025, 06, 01);
        }
        // Constructor without ID (useful for creating new users)
        public UserModel(string username, string password, string role, string email, string contact, DateTime registrationDate)
        {
            Username = username;
            Password = password;
            Role = role;
            Email = email;
            Contact = contact;
            RegistrationDate = registrationDate;
            LastLoginDate = new DateTime(2025, 06, 01);
        }
        public UserModel() { }
    }
}
