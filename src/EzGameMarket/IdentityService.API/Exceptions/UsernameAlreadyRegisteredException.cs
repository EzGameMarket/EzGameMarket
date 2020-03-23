using System;

namespace IdentityService.API.Exceptions
{
    public class UsernameAlreadyRegisteredException : Exception
    {
        public UsernameAlreadyRegisteredException()
        {
        }

        public UsernameAlreadyRegisteredException(string message) : base(message)
        {
        }

        public string UserName { get; set; }
    }
}