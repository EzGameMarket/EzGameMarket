using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Models
{
    public class Search
    {
        [Required]
        [Key]
        public Guid UserID { get; set; }
        public string Query { get; set; }
        public DateTime Date { get; set; }
    }
}
