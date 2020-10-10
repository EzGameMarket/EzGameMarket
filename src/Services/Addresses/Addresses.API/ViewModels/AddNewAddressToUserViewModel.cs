using Addresses.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.API.ViewModels
{
    public class AddNewAddressToUserViewModel
    {
        public string UserID { get; set; }
        public AddressModel NewAddress { get; set; }

        public bool SetToDefault { get; set; }
    }
}
