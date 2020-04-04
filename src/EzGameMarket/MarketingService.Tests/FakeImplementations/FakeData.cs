using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingService.Tests.FakeImplementations
{
    public class FakeData
    {
        public static List<SubscribedMember> GetMembers() => new List<SubscribedMember>()
        {
            new SubscribedMember()
            {
                ID = default,
                Active = true,
                EMail = "werdnikkrisz@gmail.com",
                SubscribedDate = DateTime.Now,
                UnSubscribedDate = default
            },
            new SubscribedMember()
            {
                ID = default,
                Active = true,
                EMail = "test@gmail.com",
                SubscribedDate = DateTime.Now,
                UnSubscribedDate = DateTime.Now.AddDays(-2)
            },
            new SubscribedMember()
            {
                ID = default,
                Active = true,
                EMail = "darky.krisz@gmail.com",
                SubscribedDate = DateTime.Now.AddDays(5),
                UnSubscribedDate = DateTime.Now
            },
            new SubscribedMember()
            {
                ID = default,
                Active = false,
                EMail = "werdnik.krisztian@gmail.com",
                SubscribedDate = DateTime.Now.AddDays(-10),
                UnSubscribedDate = DateTime.Now.AddDays(-1)
            }
        };

        public static List<NewsletterMessage> GetNewsletters() => new List<NewsletterMessage>()
        {
            new NewsletterMessage()
            {
                Created = DateTime.Now.AddDays(-10),
                Sended = DateTime.Now.AddDays(10),
                ID = default,
                Message = "Megérkezett hozzánk a Half Life 3",
                Title = "Ezt nem fogod elhinni!"
            },
            new NewsletterMessage()
            {
                ID = default,
                Title = "Szülinap!",
                Message = "Most lettünk egy évesek, váltsd be az 5%-os kuponkódunkat: BRTHDY1",
                Created = DateTime.Now.AddDays(30),
                Sended = DateTime.Now.AddDays(31),
            },
            new NewsletterMessage()
            {
                ID = default,
                Title = "Nyár!",
                Message = "Elkezdődik a nyár váltsd be a SMMR kuponkódot",
                Created = DateTime.Now.AddDays(60),
                Sended = default,
            }
        };

        public static List<Campaign> GetCampaigns() => new List<Campaign>()
        {
            new Campaign()
            {
                ID = default,
                CampaignImage = "blckfrdy.png",
                CouponCode = "BLCKFRDY",
                Description = "Megérkezett a black friday",
                End = DateTime.Now.AddDays(30),
                Start = DateTime.Now,
                ShortDescription = "BLACK FRIDAY!",
                Title = "BLCKFRDY itt van"
            },
            new Campaign()
            {
                ID = default,
                CampaignImage = "birthday.png",
                CouponCode = "BRTHDY1",
                Description = "Most léttünk 1 évesek",
                End = DateTime.Now.AddDays(31),
                Start = DateTime.Now.AddDays(30),
                ShortDescription = "SZÜLINAP!",
                Title = "1 évesek lettünk"
            }
        };
    }
}
