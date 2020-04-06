using MarketingService.API.Models;
using MarketingService.API.ViewModels.NewsletterPublish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Abstractions
{
    public interface INewsletterPublisherService
    {
        /// <summary>
        /// A leveleket kiküldi minden feliratkozott e-mailcímre
        /// </summary>
        Task SendMailsToAllAsync(int id);

        /// <summary>
        /// Egy futó campaignt lehet vele leállítani
        /// </summary>
        Task SendMailsAsync(PublishToEmailsViewModel model);

        /// <summary>
        /// Az üzenetet a megadott időpontban kiküldi
        /// </summary>
        /// <param name="message">Az üzenet ami a megadott időpontban kiküldésre fog kerülni</param>
        /// <param name="date">Az idő amikor az üzenetet kell kiküldeni</param>
        Task SetModelToSendAtSpecificTime(PublishAtSpecifiedTimeViewModel model);
        /// <summary>
        /// Az üzenetet a megadott időpontban kiküldi a megadott email címekre
        /// </summary>
        /// <param name="message">Az üzenet ami a megadott időpontban kiküldésre fog kerülni</param>
        /// <param name="date">Az idő amikor az üzenetet kell kiküldeni</param>
        Task SetModelToSendAtSpecificTimeToTheSpecificEmails(PublishToEmailsAtSpecifiedTimeViewModel model);
    }
}
