using TestDatabase.Entities;

namespace MvcTestSystem.Authentication
{
    public interface IAuthProvider
    {
        User Authenticate(string username, string password);
        User Register(string username, string password, string role);
    }
}