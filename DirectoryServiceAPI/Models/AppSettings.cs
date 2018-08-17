using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Models
{
    public class AppSettings
    {
        public int PendingTaskPollIntervalInMins { get; set; } = 15;
        public int RequestLockExpiryTimeInMins { get; set; } = 30;

        public static AppSettings InitializeSettings(IConfiguration configuration)
        {
            AppSettings settings = new AppSettings();
            configuration.Bind("AppSettings", settings);

            return settings;
        }
    }
}
