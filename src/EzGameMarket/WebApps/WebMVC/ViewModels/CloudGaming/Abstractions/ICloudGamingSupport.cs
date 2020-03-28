namespace WebMVC.ViewModels.CloudGaming.Abstractions
{
    public interface ICloudGamingSupport
    {
        public string Name { get; set; }
        public bool Support { get; set; }
        public string DownloadUrl { get; set; }
        public string ProductSupportUrl { get; set; }
        public string BaseUrl { get; set; }
    }
}