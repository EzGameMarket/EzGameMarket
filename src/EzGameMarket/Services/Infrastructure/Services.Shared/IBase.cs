using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shared.Models
{
    interface IBase<T> where T : class
    {
        Guid ID { get; }
        T Data { get; }
    }
}
