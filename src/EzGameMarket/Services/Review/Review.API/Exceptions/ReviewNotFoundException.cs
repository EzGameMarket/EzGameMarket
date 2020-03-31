using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review.API.Exceptions
{
    public class ReviewNotFoundException : Exception
    {
        public ReviewNotFoundException()
        {
        }

        public ReviewNotFoundException(string message) : base(message)
        {
        }

        public int ReviewID { get; set; }
    }
}
