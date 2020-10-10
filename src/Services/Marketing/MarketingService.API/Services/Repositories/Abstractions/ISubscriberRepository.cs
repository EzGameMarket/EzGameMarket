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
        Task<SubscribedMember> GetByEmail(string email);

        Task Add(SubscribedMember model);

        Task Modify(int id, SubscribedMember model);

        Task<List<SubscribedMember>> GetBeetwen(DateTime start, DateTime end = default, bool active = true);

        Task<List<SubscribedMember>> GetActiveMembers();
    }
}
