namespace CatalogService.API.Events
{
    public class PriceUpdatedEventMessage
    {
        public string ProductID { get; set; }
        public double NewPrice { get; set; }
    }
}