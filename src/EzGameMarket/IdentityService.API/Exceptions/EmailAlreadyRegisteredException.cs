using System;

namespace IdentityService.API.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public EmailAlreadyRegisteredException()
        {
        }

        public EmailAlreadyRegisteredException(string message) : base(message)
        {
        }

        public string Email { get; set; }
    }
}