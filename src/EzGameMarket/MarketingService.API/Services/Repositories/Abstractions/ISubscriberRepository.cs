using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Abstractions
{
    public interface ISubscriberRepository
    {
        Task<SubscribedMember> Get(int id);

        Task<List<SubscribedMember>> GetMemebersSubsribedBeetwenFromDateToDate(DateTime start, DateTime end);

        Task Add(SubscribedMember model);

        Task Modify(int id, SubscribedMember model);
    }
}
