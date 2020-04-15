using MarketingService.API.Data;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Exceptions.Model.Newsletter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Implementations
{
    public class NewsletterRepository : INewsletterRepository
    {
        private MarketingDbContext _dbContext;

        public NewsletterRepository(MarketingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(NewsletterMessage message)
        {
            var newsLetter = await Get(message.ID.GetValueOrDefault(-1));

            if (newsLetter != default)
            {
                throw new NewsletterAlreadyInDbException() { ID = message.ID.GetValueOrDefault(-1), Title = message.Title };
            }

            var newsLetterWithTitle = await GetByTitle(message.Title);

            if (newsLetterWithTitle != default)
            {
                throw new NewslettersWithTitleAlreadyInDbException() { Title = message.Title };
            }

            await _dbContext.Newsletters.AddAsync(message);

            await _dbContext.SaveChangesAsync();
        }

        public Task<NewsletterMessage> Get(int id) => _dbContext.Newsletters.FirstOrDefaultAsync(n=> n.ID == id);

        public Task<NewsletterMessage> GetByTitle(string title) => _dbContext.Newsletters.FirstOrDefaultAsync(n => n.Title == title);

        public async Task Modify(int id, NewsletterMessage message)
        {
            var newsLetter = await Get(id);

            if (newsLetter == default)
            {
                throw new NewsletterNotFoundException() { ID = message.ID.GetValueOrDefault(-1), Title = message.Title };
            }

            _dbContext.Entry(newsLetter).CurrentValues.SetValues(message);

            await _dbContext.SaveChangesAsync();
        }
    }
}
