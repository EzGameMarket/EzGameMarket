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
                ID = 1,
                Active = true,
                EMail = "werdnikkrisz@gmail.com",
                SubscribedDate = DateTime.Now,
                UnSubscribedDate = default
            },
            new SubscribedMember()
            {
                ID = 2,
                Active = true,
                EMail = "test@gmail.com",
                SubscribedDate = DateTime.Now,
                UnSubscribedDate = DateTime.Now.AddDays(-2)
            },
            new SubscribedMember()
            {
                ID = 3,
                Active = true,
                EMail = "darky.krisz@gmail.com",
                SubscribedDate = DateTime.Now.AddDays(5),
                UnSubscribedDate = DateTime.Now
            },
            new SubscribedMember()
            {
                ID = 4,
                Active = false,
                EMail = "werdnik.krisztian@gmail.com",
                SubscribedDate = DateTime.Now.AddDays(-10),
                UnSubscribedDate = DateTime.Now.AddDays(-1)
            },
            new SubscribedMember()
            {
                ID = 5,
                Active = false,
                EMail = "testExisting@gmail.com",
                SubscribedDate = DateTime.Now.AddDays(-10),
                UnSubscribedDate = DateTime.Now.AddDays(-1)
            },
            new SubscribedMember()
            {
                ID = 6,
                Active = false,
                EMail = "unSubtestExisting@gmail.com",
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
                ID = 1,
                Message = "Megérkezett hozzánk a Half Life 3",
                Title = "Ezt nem fogod elhinni!"
            },
            new NewsletterMessage()
            {
                ID = 2,
                Title = "Szülinap!",
                Message = "Most lettünk egy évesek, váltsd be az 5%-os kuponkódunkat: BRTHDY1",
                Created = DateTime.Now.AddDays(30),
                Sended = DateTime.Now.AddDays(31),
            },
            new NewsletterMessage()
            {
                ID = 3,
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
                ID = 1,
                CampaignImageUrl = "blckfrdy.png",
                CouponCode = "BLCKFRDY",
                Description = "Megérkezett a black friday",
                End = DateTime.Now.AddDays(-30),
                Start = DateTime.Now,
                ShortDescription = "BLACK FRIDAY!",
                Title = "BLCKFRDY itt van",
                Published = false,
                PublishedDate = default,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 21,
                CampaignImageUrl = "blckfrdy.png",
                CouponCode = "BLCKFRDY",
                Description = "Megérkezett a black friday",
                End = DateTime.Now.AddDays(-30),
                Start = DateTime.Now,
                ShortDescription = "BLACK FRIDAY!",
                Title = "BLCKFRDY itt van2",
                Published = false,
                PublishedDate = default,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 31,
                CampaignImageUrl = "blckfrdy.png",
                CouponCode = "BLCKFRDY",
                Description = "Megérkezett a black friday",
                End = DateTime.Now.AddDays(-30),
                Start = DateTime.Now,
                ShortDescription = "BLACK FRIDAY!",
                Title = "BLCKFRDY itt van3",
                Published = true,
                PublishedDate = DateTime.Now,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 71,
                CampaignImageUrl = "blckfrdy.png",
                CouponCode = "BLCKFRDY",
                Description = "Megérkezett a black friday",
                End = DateTime.Now.AddDays(-30),
                Start = DateTime.Now,
                ShortDescription = "BLACK FRIDAY!",
                Title = "BLCKFRDY itt van4",
                Published = false,
                PublishedDate = default,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },

            new Campaign()
            {
                ID = 20,
                CampaignImageUrl = "blckfrdy.png",
                CouponCode = "BLCKFRDY",
                Description = "Megérkezett a black friday",
                End = DateTime.Now.AddDays(-30),
                Start = DateTime.Now,
                ShortDescription = "BLACK FRIDAY!",
                Title = "BLCKFRDY itt van",
                Published = false,
                PublishedDate = default,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 2,
                CampaignImageUrl = "birthday.png",
                CouponCode = "BRTHDY1",
                Description = "Most léttünk 1 évesek",
                End = DateTime.Now.AddDays(31),
                Start = DateTime.Now.AddDays(30),
                ShortDescription = "SZÜLINAP!",
                Title = "1 évesek lettünk",
                Published = true,
                PublishedDate = DateTime.Now,
                Started = true,
                StartedDate = DateTime.Now,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 42,
                CampaignImageUrl = "birthday.png",
                CouponCode = "BRTHDY1",
                Description = "Most léttünk 1 évesek",
                End = DateTime.Now.AddDays(31),
                Start = DateTime.Now.AddDays(30),
                ShortDescription = "SZÜLINAP!",
                Title = "1 évesek lettünk",
                Published = false,
                PublishedDate = default,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 3,
                CampaignImageUrl = "hello.png",
                CouponCode = "Hello",
                Description = "Elindultunk",
                End = DateTime.Now.AddDays(1),
                Start = DateTime.Now.AddDays(5),
                ShortDescription = "HLL!",
                Title = "Most indultunk",
                Published = true,
                PublishedDate = DateTime.Now,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 23,
                CampaignImageUrl = "hello.png",
                CouponCode = "Hello2",
                Description = "Elindultunk",
                End = DateTime.Now.AddDays(1),
                Start = DateTime.Now.AddDays(5),
                ShortDescription = "HLL!",
                Title = "Most indultunk2",
                Published = true,
                PublishedDate = DateTime.Now,
                Started = false,
                StartedDate = default,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
                        new Campaign()
            {
                ID = 53,
                CampaignImageUrl = "hello.png",
                CouponCode = "Hello2",
                Description = "Elindultunk",
                End = DateTime.Now.AddDays(1),
                Start = DateTime.Now.AddDays(5),
                ShortDescription = "HLL!",
                Title = "Most indultunk2",
                Published = true,
                PublishedDate = DateTime.Now,
                Started = true,
                StartedDate = DateTime.Now,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = default,
                Canceled = false
            },
            new Campaign()
            {
                ID = 4,
                CampaignImageUrl = "test.png",
                CouponCode = "HEE",
                Description = "Test",
                End = DateTime.Now.AddDays(5),
                Start = DateTime.Now.AddDays(20),
                ShortDescription = "TEST!",
                Title = "Ez egy test.",
                Published = true,
                PublishedDate = DateTime.Now,
                Started = true,
                StartedDate = DateTime.Now,
                Deleted = false,
                DeletedTime = default,
                CanceledDate = DateTime.Now.AddDays(10),
                Canceled = true
            },
            new Campaign()
            {
                ID = 5,
                CampaignImageUrl = "test.png",
                CouponCode = "HEE",
                Description = "Test",
                End = DateTime.Now.AddDays(5),
                Start = DateTime.Now.AddDays(20),
                ShortDescription = "TEST!",
                Title = "Ez egy test.",
                Published = true,
                PublishedDate = DateTime.Now,
                Started = false,
                StartedDate = default,
                Deleted = true,
                DeletedTime = DateTime.Now,
                CanceledDate = default,
                Canceled = false
            }
        };
    }
}
