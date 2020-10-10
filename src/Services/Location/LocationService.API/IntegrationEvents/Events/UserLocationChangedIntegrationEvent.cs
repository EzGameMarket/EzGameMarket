using EventBus.Shared.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.API.IntegrationEvents.Events
{
    public class UserLocationChangedIntegrationEvent : IntegrationEvent
    {
        public UserLocationChangedIntegrationEvent(string userID, double longitude, double latitude, string country, string city) : base()
        {
            UserID = userID;
            Longitude = longitude;
            Latitude = latitude;
            Country = country;
            City = city;
        }

        [JsonConstructor]
        public UserLocationChangedIntegrationEvent(string userID, double longitude, double latitude, string country, string city, Guid id, DateTime creationTime) : base(id, creationTime) 
        {
            UserID = userID;
            Longitude = longitude;
            Latitude = latitude;
            Country = country;
            City = city;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public string UserID { get; set; }
    }
}
