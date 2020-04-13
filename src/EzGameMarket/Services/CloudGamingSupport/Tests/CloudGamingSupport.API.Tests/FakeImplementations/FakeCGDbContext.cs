using CloudGamingSupport.API.Data;
using CloudGamingSupport.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Linq;

namespace CloudGamingSupport.API.Tests.FakeImplementations
{
    public class FakeCGDbContext
    {
        public CGDbContext DbContext { get; set; }

        public static List<CloudGamingProvider> CreateProviders() => new List<CloudGamingProvider>()
                {
                    new CloudGamingProvider()
                    {
                        ID = 1,
                        Name = "NVIDIA Geforce Now",
                        SearchURl = "https://www.gamewatcher.com/news/nvidia-geforce-now-games-list",
                        Url = "https://www.nvidia.com/en-eu/geforce-now/",
                    },
                    new CloudGamingProvider()
                    {
                        ID = 2,
                        Name = "Google Stadia",
                        SearchURl = "https://support.google.com/stadia/answer/9363495?hl=en",
                        Url = "https://stadia.google.com/",
                    },
                };

        public static List<CloudGamingSupported> CreateGames() => new List<CloudGamingSupported>()
                {
                    new CloudGamingSupported()
                    {
                        ID = 1,
                        ProductID = "csgo",
                    },
                    new CloudGamingSupported()
                    {
                        ID = 2,
                        ProductID = "r6s",
                    },
                    new CloudGamingSupported()
                    {
                        ID = 3,
                        ProductID = "bfv",
                    },
                };

        public static List<CloudGamingProvidersAndGames> CreateMatches() => new List<CloudGamingProvidersAndGames>()
                {
                    new CloudGamingProvidersAndGames()
                    {
                        ID = 1,
                        CloudGamingSupportedID = 1,
                        CloudGamingProviderID = 1,
                    },
                    new CloudGamingProvidersAndGames()
                    {
                        ID = 2,
                        CloudGamingSupportedID = 2,
                        CloudGamingProviderID = 1,
                    },new CloudGamingProvidersAndGames()
                    {
                        ID = 3,
                        CloudGamingSupportedID = 2,
                        CloudGamingProviderID = 2,
                    },
                };

        private static List<string> usedDbNames = new List<string>();

        public FakeCGDbContext(string callerName)
        {
            var dbNames = usedDbNames.Count(n => n == callerName);

            callerName += $"-{dbNames}";

            var dbOptions = new DbContextOptionsBuilder<CGDbContext>()
            .UseInMemoryDatabase(databaseName: $"in-memory-cgsupport-test-{Guid.NewGuid()}-{callerName}").EnableSensitiveDataLogging()
            .Options;

            var output = new CGDbContext(dbOptions);

            if (output.Games.Any() == false)
            {
                output.AddRange(CreateGames());
            }
            if (output.Matches.Any() == false)
            {
                output.AddRange(CreateMatches());
            }
            if (output.Providers.Any() == false)
            {
                output.AddRange(CreateProviders());
            }

            output.SaveChanges();

            DbContext = output;
        }
    }
}
