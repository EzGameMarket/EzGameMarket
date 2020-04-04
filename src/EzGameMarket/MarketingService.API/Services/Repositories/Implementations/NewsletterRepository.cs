using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Implementations
{
    public class NewsletterRepository : INewsletterRepository
    {
        public Task Add(NewsletterMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<NewsletterMessage> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Modify(int id, NewsletterMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
