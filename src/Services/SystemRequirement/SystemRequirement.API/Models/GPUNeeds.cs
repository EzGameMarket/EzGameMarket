using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace SystemRequirement.API.Models
{
    public class GPUNeeds
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string AMDType { get; set; }
        [Required]
        public string NVIDIAType { get; set; }
    }
}