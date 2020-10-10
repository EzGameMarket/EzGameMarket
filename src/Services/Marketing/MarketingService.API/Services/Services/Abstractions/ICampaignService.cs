using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Abstractions
{
    public interface ICampaignService
    {
        /// <summary>
        /// Egy publishált kampányt lehet vele elindítani
        /// </summary>
        Task StartAsync(int id);

        /// <summary>
        /// Egy futó campaignt lehet vele leállítani
        /// </summary>
        Task CancelAsync(int id);

        /// <summary>
        /// A campaignt láthatóvá teszi a weboldalon, és egy campaignt csak az után lehet elindítani, hogy publishálva volt
        /// ellenőrzés céljából
        /// </summary>
        Task PublishAsync(int id);

        /// <summary>
        /// Egy campaignt lehet vele törölni. Ezzel lehet még nem elindított campaignokat is leállítani és törölni
        /// </summary>
        Task DeleteAsync(int id);
    }
}
