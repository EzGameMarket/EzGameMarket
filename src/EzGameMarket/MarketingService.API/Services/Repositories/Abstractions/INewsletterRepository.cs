using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Abstractions
{
    public interface INewsletterRepository
    {
        Task<NewsletterMessage> Get(int id);
        Task<NewsletterMessage> GetByTitle(string title);

        Task Modify(int id, NewsletterMessage message);

        Task Add(NewsletterMessage message);
    }
}
