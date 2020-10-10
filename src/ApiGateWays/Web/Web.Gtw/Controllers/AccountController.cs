using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models.ViewModels.Account;

namespace Web.Gtw.Controllers
{
    public class AccountController
    {
        public async Task<IEnumerable<AddressViewModel>> GetAddresses(string userID)
        {
            return new List<AddressViewModel>();
        }

        public async Task<IEnumerable<AddressViewModel>> GetCreditCards(string userID)
        {
            return new List<AddressViewModel>();
        }
    }
}
