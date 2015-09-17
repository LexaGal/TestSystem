using System;

namespace MvcTestSystem.Authentication
{
    public class AuthenticationException : ApplicationException
    {
        public override string Message
        {
            get { return (new UnauthorizedAccessException()).Message; }
        }
    }
}