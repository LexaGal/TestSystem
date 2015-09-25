using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDatabase.Entities
{
    public class User
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }

        public User(string username, string passwordHash, string role)
        {
            Name = username;
            PasswordHash = passwordHash;
            Role = role;
            Tasks = new List<Task>();
            SolvedTasks = new List<Task>();
        }

        public User()
        {
        }

        [NotMapped]
        public IList<Task> Tasks { get; set; }
    
        [NotMapped]
        public IList<Task> SolvedTasks { get; set; }
    }
}