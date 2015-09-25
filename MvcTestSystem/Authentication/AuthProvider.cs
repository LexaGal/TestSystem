using System.Linq;
using TestDatabase.Entities;
using TestDatabase.Repository.Abstract;

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

        public User Register(string username, string password, string role)
        {
            User user;
            if (role == "Admin")
            {
                user = new User(username, _cryptor.Encrypt(password), "Admin");
                _usersRepository.Add(user);
                return user;    
            }
            user = new User(username, _cryptor.Encrypt(password), "User");
            _usersRepository.Add(user);
            return user;    
        }
    }
}