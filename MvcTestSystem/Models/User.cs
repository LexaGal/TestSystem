using System;
using System.ComponentModel.DataAnnotations;

namespace MvcTestSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; private set; }

        public string Username { get; private set; }
        public Role Role { get; private set; }
        public string PasswordHash { get; private set; }

        public User(string username, string passwordHash, Role role)
        {
            Id = (new Random()).Next(1, 1000);
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}