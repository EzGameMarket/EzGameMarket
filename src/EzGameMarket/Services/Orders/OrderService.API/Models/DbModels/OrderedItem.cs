using System.ComponentModel.DataAnnotations;

namespace OrderService.API.Models.DbModels
{
    public class OrderedItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int BasePrice { get; set; }
    }
}