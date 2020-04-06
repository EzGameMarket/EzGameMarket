using EventBus.Shared.Abstraction;
using MarketingService.API.Data;
using MarketingService.API.Exceptions.Domain.Campaign;
using MarketingService.API.Exceptions.Model.Campaign;
using MarketingService.API.IntegrationEvents.Events;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Implementations
{
    public class CampaignService : ICampaignService
    {
        private MarketingDbContext _dbContext;
        private ICampaignRepository _campaignRepository;
        private IEventBusRepository _eventBus;

        public CampaignService(MarketingDbContext dbContext, ICampaignRepository campaignRepository, IEventBusRepository eventBus)
        {
            _dbContext = dbContext;
            _campaignRepository = campaignRepository;
            _eventBus = eventBus;
        }

        public async Task CancelAsync(int id)
        {
            var campaign = await _campaignRepository.Get(id);

            CheckCampaignForCancel(id, campaign);

            campaign.Cancel();
            await _dbContext.SaveChangesAsync();

            var eventModel = new CampaignCanceledIntegrationEvent(campaign);

            try
            {
                _eventBus.Publish(eventModel);
            }
            catch (Exception)
            {
                campaign.RollbackCancel();
                await _dbContext.SaveChangesAsync();

                throw;
            }
        }

        private void CheckCampaignForCancel(int id, Campaign campaign)
        {
            if (campaign == default)
            {
                throw new CampaignNotFoundException() { ID = id };
            }

            if (campaign.Published == false)
            {
                throw new CampaignNotPublishedYetException() { ID = id };
            }

            if (campaign.Started == false)
            {
                throw new CampaignNotStartedYetException() { ID = id };
            }

            if (campaign.Canceled == true)
            {
                throw new CampaignAlreadyCanceledException() { ID = id };
            }
        }

        public async Task DeleteAsync(int id)
        {
            var campaign = await _campaignRepository.Get(id);

            CheckCampaignForDelete(id, campaign);

            campaign.Delete();
            await _dbContext.SaveChangesAsync();

            var eventModel = new CampaignCanceledIntegrationEvent(campaign);

            try
            {
                _eventBus.Publish(eventModel);
            }
            catch (Exception)
            {
                campaign.RollbackDelete();
                await _dbContext.SaveChangesAsync();

                throw;
            }
        }

        private void CheckCampaignForDelete(int id, Campaign campaign)
        {
            if (campaign == default)
            {
                throw new CampaignNotFoundException() { ID = id };
            }

            if (campaign.Started)
            {
                throw new CampaignAlreadyStartedException() { ID = id };
            }

            if (campaign.Deleted == true)
            {
                throw new CampaignAlreadyDeletedException() { ID = id };
            }
        }

        public async Task PublishAsync(int id)
        {
            var campaign = await _campaignRepository.Get(id);

            CheckCampaignForPublish(id, campaign);

            campaign.Publish();
            await _dbContext.SaveChangesAsync();

            var eventModel = new CampaignPublishedIntegrationEvent(campaign);

            try
            {
                _eventBus.Publish(eventModel);
            }
            catch (Exception)
            {
                campaign.RollbackPublish();
                await _dbContext.SaveChangesAsync();

                throw;
            }
        }

        private void CheckCampaignForPublish(int id, Campaign campaign)
        {
            if (campaign == default)
            {
                throw new CampaignNotFoundException() { ID = id };
            }

            if (campaign.Published)
            {
                throw new CampaignAlreadyPublishedException() { ID = id };
            }
        }

        public async Task StartAsync(int id)
        {
            var campaign = await _campaignRepository.Get(id);

            CheckCampaignForStart(id, campaign);

            campaign.StartCampaign();
            await _dbContext.SaveChangesAsync();

            var eventModel = new CampaignStartedIntegrationEvent(campaign);

            try
            {
                _eventBus.Publish(eventModel);
            }
            catch (Exception)
            {
                campaign.RollbackStart();
                await _dbContext.SaveChangesAsync();

                throw;
            }
        }

        private void CheckCampaignForStart(int id, Campaign campaign)
        {
            if (campaign == default)
            {
                throw new CampaignNotFoundException() { ID = id };
            }

            if (campaign.Published == false)
            {
                throw new CampaignNotPublishedYetException() { ID = id };
            }

            if (campaign.Started)
            {
                throw new CampaignAlreadyStartedException() { ID = id };
            }
        }
    }
}
