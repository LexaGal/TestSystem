namespace MvcTestSystem.Authentication
{
    public interface ICryptor
    {
        string Encrypt(string str);
    }
}