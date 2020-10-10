using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Reviews.Abstractions
{
    public interface IHasReview
    {
        IEnumerable<IReview> Reviews { get; set; }
    }
}
