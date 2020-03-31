using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review.API.Exceptions
{
    public class ReviewAlreadyExistsException : Exception
    {
        public ReviewAlreadyExistsException()
        {
        }

        public ReviewAlreadyExistsException(string message) : base(message)
        {
        }

        public int ReviewID { get; set; }
        public string ProductID { get; set; }
        public string UserID { get; set; }
    }
}
