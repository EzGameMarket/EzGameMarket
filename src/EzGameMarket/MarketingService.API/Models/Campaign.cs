using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Models
{
    public class Campaign
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Url]
        public string CampaignImageUrl { get; set; }

        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }

        public string CouponCode { get; set; }



        public bool Published { get; set; }
        public DateTime? PublishedDate { get; set; }



        public bool Canceled { get; set; }
        public DateTime? CanceledDate { get; set; }



        public bool Deleted { get; set; }
        public DateTime? DeletedTime { get; set; }



        public bool Started { get; set; }
        public DateTime? StartedDate { get; set; }

        public void Publish()
        {
            Published = true;
            PublishedDate = DateTime.Now;
        }

        public void RollbackPublish()
        {
            Published = false;
            PublishedDate = default;
        }

        public void Delete()
        {
            Deleted = true;
            DeletedTime = DateTime.Now;
        }

        public void RollbackDelete()
        {
            Deleted = false;
            DeletedTime = default;
        }

        public void Cancel()
        {
            Canceled = true;
            CanceledDate = DateTime.Now;
        }

        public void RollbackCancel()
        {
            Canceled = false;
            CanceledDate = default;
        }

        public void StartCampaign()
        {
            Started = true;
            StartedDate = DateTime.Now;
        }

        public void RollbackStart()
        {
            Started = false;
            StartedDate = default;
        }
    }
}
