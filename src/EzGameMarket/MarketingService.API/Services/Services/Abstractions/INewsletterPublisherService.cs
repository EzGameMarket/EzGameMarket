using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Abstractions
{
    public interface INewsletterPublisherService
    {
        /// <summary>
        /// A leveleket ki
        /// </summary>
        Task SendMailsToAllAsync(NewsletterMessage newsletter);

        /// <summary>
        /// Egy futó campaignt lehet vele leállítani
        /// </summary>
        Task SendMailsAsync(NewsletterMessage newsletter, IEnumerable<string> emails);

        /// <summary>
        /// Az üzenetet a megadott időpontban kiküldi
        /// </summary>
        /// <param name="message">Az üzenet ami a megadott időpontban kiküldésre fog kerülni</param>
        /// <param name="date">Az idő amikor az üzenetet kell kiküldeni</param>
        Task SetModelToSendAtSpecificTime(NewsletterMessage message, DateTime date);
        /// <summary>
        /// Beállítja, hogy az üzentet a Sended DateTime Propertynak megadott időpontban kell kiküldeni
        /// </summary>
        /// <param name="message">Az üzenet amit ki kell küldeni</param>
        Task SetModelToSendAtSpecificTime(NewsletterMessage message);
    }
}
