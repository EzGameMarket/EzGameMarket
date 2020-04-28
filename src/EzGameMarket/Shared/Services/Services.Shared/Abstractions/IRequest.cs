using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.API.Communication.Models.Abstractions
{
    interface IRequest : IBase
    {
        Guid ID { get; }

        DateTime CreationDate { get; }
    }
}
