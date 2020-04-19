using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Addresses.API.Models
{
    public class UserAddresses
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public IEnumerable<AddressModel> Addresses { get; set; }

        [Required]
        public AddressModel DefaultAddress { get; set; }
    }
}
