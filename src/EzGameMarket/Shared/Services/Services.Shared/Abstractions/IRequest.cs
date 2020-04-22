using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.API.Communication.Models.Abstractions
{
    interface IRequest
    {
        Guid ID { get; }

        DateTime CreationDate { get; }
    }
}
