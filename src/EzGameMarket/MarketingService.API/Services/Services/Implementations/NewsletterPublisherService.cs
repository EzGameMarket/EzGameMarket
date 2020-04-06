using EventBus.Shared.Abstraction;
using MarketingService.API.Data;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Implementations
{
    public class NewsletterPublisherService : INewsletterPublisherService
    {
        private INewsletterRepository _newsletterRepository;
        private IEventBusRepository _eventBus;

        public NewsletterPublisherService(INewsletterRepository newsletterRepository, IEventBusRepository eventBus)
        {
            _newsletterRepository = newsletterRepository;
            _eventBus = eventBus;
        }

        public Task SendMailsAsync(NewsletterMessage newsletter, IEnumerable<string> emails)
        {
            throw new NotImplementedException();
        }

        public Task SendMailsToAllAsync(NewsletterMessage newsletter)
        {
            throw new NotImplementedException();
        }

        public Task SetModelToSendAtSpecificTime(NewsletterMessage message, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task SetModelToSendAtSpecificTime(NewsletterMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
