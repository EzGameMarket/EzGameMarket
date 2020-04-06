using EventBus.Shared.Abstraction;
using MarketingService.API.Data;
using MarketingService.API.Exceptions.Model.Newsletter;
using MarketingService.API.IntegrationEvents.Events.NewsletterPublishing;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using MarketingService.API.ViewModels.NewsletterPublish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Implementations
{
    public class NewsletterPublisherService : INewsletterPublisherService
    {
        private MarketingDbContext _dbContext;
        private INewsletterRepository _newsletterRepository;
        private IEventBusRepository _eventBus;
        private ISubscriberRepository _subscriberRepository;

        public NewsletterPublisherService(MarketingDbContext dbContext,
                                          INewsletterRepository newsletterRepository,
                                          IEventBusRepository eventBus,
                                          ISubscriberRepository subscriberRepository)
        {
            _dbContext = dbContext;
            _newsletterRepository = newsletterRepository;
            _eventBus = eventBus;
            _subscriberRepository = subscriberRepository;
        }

        public async Task SendMailsAsync(PublishToEmailsViewModel model)
        {
            var newsLetter = await _newsletterRepository.Get(model.ID);

            if (newsLetter == default)
            {
                throw new NewsletterNotFoundException() { ID = model.ID, Title = default };
            }

            newsLetter.SetSended();

            await _dbContext.SaveChangesAsync();

            try
            {
                var publishingModel = new NewsletterPublishedIntegrationEvent(newsLetter,model.Emails);

                _eventBus.Publish(publishingModel);
            }
            catch (Exception)
            {
                newsLetter.RollbackSend();
                await _dbContext.SaveChangesAsync();

                throw;
            }
        }

        public async Task SendMailsToAllAsync(int id)
        {
            var emails = await LoadActiveEmails();

            await SendMailsAsync(new PublishToEmailsViewModel(emails, id));
        }

        private async Task<IEnumerable<string>> LoadActiveEmails()
        {
            var allActiveMember = await _subscriberRepository.GetActiveMembers();

            return allActiveMember.Select(s => s.EMail);
        }

        public async Task SetModelToSendAtSpecificTime(PublishAtSpecifiedTimeViewModel model)
        {
            var emails = await LoadActiveEmails();

            await SetModelToSendAtSpecificTimeToTheSpecificEmails(new PublishToEmailsAtSpecifiedTimeViewModel(emails,model.Time, model.ID));
        }

        public async Task SetModelToSendAtSpecificTimeToTheSpecificEmails(PublishToEmailsAtSpecifiedTimeViewModel model)
        {
            var newsLetter = await _newsletterRepository.Get(model.ID);

            if (newsLetter == default)
            {
                throw new NewsletterNotFoundException() { ID = model.ID, Title = default };
            }

            newsLetter.SetSended(model.Time);

            await _dbContext.SaveChangesAsync();

            try
            {
                var publishingModel = new NewsletterPublishingScheduleWithEmailsIntegrationEvent(newsLetter, model.Time, model.Emails);

                _eventBus.Publish(publishingModel);
            }
            catch (Exception)
            {
                newsLetter.RollbackSend();
                await _dbContext.SaveChangesAsync();

                throw;
            }
        }
    }
}
