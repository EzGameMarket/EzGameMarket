using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Carts
{
    public class SendCartToUpdateViewModel<T>
    {
        public string UserName { get; set; }
        public T Data { get; set; }
    }
}
