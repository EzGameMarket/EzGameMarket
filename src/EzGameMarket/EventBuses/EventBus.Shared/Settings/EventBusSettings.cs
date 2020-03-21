using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Shared.Settings
{
    public class EventBusSettings
    {
        public static EventBusSettings Settings => LoadSettings();

        public bool AzureServiceBusEnabled { get; set; }
        public bool AzureStorageEnabled { get; set; }
        public bool SubscriptionClientName { get; set; }
        public int EventBusRetryCount { get; set; }

        private static EventBusSettings LoadSettings()
        {
            if (Settings == default)
            {
                
            }

            return Settings;
        }
    }
}
