using CloudGamingSupport.API.Data;
using CloudGamingSupport.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudGamingSupport.API.Tests.FakeImplementations
{
    public class FakeCGDbContext
    {
        public CGDbContext DbContext { get; set; }

        public static DbContextOptions<CGDbContext> DbOptions { get; set; }

        public static List<CloudGamingProvider> Providers { get; set; }

        public static List<CloudGamingSupported> Games { get; set; }

        public static List<CloudGamingProvidersAndGames> Matches { get; set; }

        public FakeCGDbContext()
        {
            if (Matches == default)
            {
                Matches = new List<CloudGamingProvidersAndGames>()
                {
                    new CloudGamingProvidersAndGames()
                    {
                        CloudGamingSupportedID = 1,
                        CloudGamingProviderID = 1,
                    },
                    new CloudGamingProvidersAndGames()
                    {
                        CloudGamingSupportedID = 2,
                        CloudGamingProviderID = 1,
                    },new CloudGamingProvidersAndGames()
                    {
                        CloudGamingSupportedID = 2,
                        CloudGamingProviderID = 2,
                    },
                };
            }

            if (Providers == default)
            {
                Providers = new List<CloudGamingProvider>()
                {
                    new CloudGamingProvider()
                    {
                        ID = 1,
                        Name = "NVIDIA Geforce Now",
                        SearchURl = "https://www.gamewatcher.com/news/nvidia-geforce-now-games-list",
                        Url = "https://www.nvidia.com/en-eu/geforce-now/",
                        SupportedGames = new List<CloudGamingProvidersAndGames>()
                        {
                            Matches[0],
                            Matches[1],
                        }
                    },
                    new CloudGamingProvider()
                    {
                        ID = 2,
                        Name = "Google Stadia",
                        SearchURl = "https://support.google.com/stadia/answer/9363495?hl=en",
                        Url = "https://stadia.google.com/",
                        SupportedGames = new List<CloudGamingProvidersAndGames>()
                        {
                            Matches[2]
                        }
                    },
                };
            }

            if (Games == default)
            {
                Games = new List<CloudGamingSupported>()
                {
                    new CloudGamingSupported()
                    {
                        ID = 1,
                        ProductID = "csgo",
                        Providers = new List<CloudGamingProvidersAndGames>()
                        {
                            Matches[0]
                        }
                    },
                    new CloudGamingSupported()
                    {
                        ID = 2,
                        ProductID = "r6s",
                        Providers = new List<CloudGamingProvidersAndGames>()
                        {
                            Matches[1],
                            Matches[2]
                        }
                    },
                    new CloudGamingSupported()
                    {
                        ID = 3,
                        ProductID = "bfv",
                        Providers = new List<CloudGamingProvidersAndGames>()
                    },
                };
            }

            DbOptions = new DbContextOptionsBuilder<CGDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-cgsupport-test").EnableSensitiveDataLogging()
                .Options;

            if (DbContext == default)
            {
                try
                {
                    DbContext = new CGDbContext(DbOptions);
                    DbContext.AddRange(Games);
                    DbContext.AddRange(Providers);
                    DbContext.AddRange(Matches);
                    DbContext.SaveChanges();
                }
                catch (Exception)
                {
                    DbContext.ChangeTracker.AcceptAllChanges();
                }
            }
        }
    }
}
