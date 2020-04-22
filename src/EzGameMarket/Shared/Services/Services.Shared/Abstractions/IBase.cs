using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.API.Communication.Models.Abstractions
{
    interface IBase<T> : IBase where T : class
    {
        T Data { get; }
    }

    interface IBase
    {
        Guid ID { get; }
    }
}
