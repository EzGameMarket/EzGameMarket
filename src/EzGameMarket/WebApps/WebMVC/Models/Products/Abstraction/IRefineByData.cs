namespace WebMVC.Models.Products.Abstraction
{
    public interface IRefineByData
    {
        public string Data { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public bool Active { get; set; }
    }
}