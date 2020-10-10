using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemRequirement.API.Models
{
    public class CPUNeeds
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string AMDType { get; set; }
        [Required]
        public string IntelType { get; set; }
    }
}