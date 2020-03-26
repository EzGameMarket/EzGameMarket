﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Products.Abstraction
{
    public interface IHasReview
    {
        IEnumerable<Review> Reviews { get; set; }
    }
}
