using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Abstractions
{
    public interface ISubscribeManagerService
    {
        /// <summary>
        /// Feliratkozás a hírlevélre
        /// </summary>
        /// <param name="member">A felhasználó aki feliratkozzik a hírlevélre</param>
        Task Subscribe(SubscribedMember member);
        /// <summary>
        /// Leiratkozás a hírlevélre
        /// </summary>
        /// <param name="member">A felhasználó aki leiratkozzik a hírlevélre</param>
        Task UnSubscribe(SubscribedMember member);
    }
}
