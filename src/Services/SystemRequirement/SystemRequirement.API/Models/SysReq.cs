using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SystemRequirement.API.Models
{
    public class SysReq
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string ProductID { get; set; }

        [Required]
        public SysReqType Type { get; set; }

        [Required]
        public int CPUID { get; set; }
        public CPUNeeds CPU { get; set; }
        [Required]
        public int RAMID { get; set; }
        public RAMNeeds RAM { get; set; }
        [Required]
        public int GPUID { get; set; }
        public GPUNeeds GPU { get; set; }
        [Required]
        public int StorageID { get; set; }
        public StorageNeeds Storage { get; set; }
        [Required]
        public int NetworkID { get; set; }
        public NetworkNeeds Network { get; set; }
    }
}
