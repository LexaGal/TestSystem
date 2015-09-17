using System.Configuration;
using System.Linq;
using MvcTestSystem.Models;
using MvcTestSystem.Repository.Abstract;

namespace MvcTestSystem.Authentication
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IUserRepository _usersRepository;
        private readonly ICryptor _cryptor;

        public AuthProvider(IUserRepository usersRepository, ICryptor cryptor)
        {
            _usersRepository = usersRepository;
            _cryptor = cryptor;
        }

        public User Authenticate(string username, string password)
        {
            User user =_usersRepository.GetAll().ToList().SingleOrDefault(c =>
                c.Name == username && c.PasswordHash == _cryptor.Encrypt(password));
            return user;
        }

        public User Register(string username, string password, Role role)
        {
            User user = new User(username, _cryptor.Encrypt(password), role);
            _usersRepository.Add(user);
            return user;    
        }
    }
}