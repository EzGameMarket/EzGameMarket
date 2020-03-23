namespace CatalogService.API.Events
{
    public class DiscountCreatedEventHandler
    {
        public string ItemID { get; set; }
        public double Price { get; set; }
    }
}