using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.ViewModels.Subscribe
{
    public class SubscribeViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
    }
}
