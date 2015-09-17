using MvcTestSystem.Models;

namespace MvcTestSystem.Authentication
{
    public interface IAuthProvider
    {
        User Authenticate(string username, string password);
        User Register(string username, string password, Role role);
    }
}