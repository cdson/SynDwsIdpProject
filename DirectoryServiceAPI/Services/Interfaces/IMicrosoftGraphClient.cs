using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services
{
    public interface IMicrosoftGraphClient
    {
        Task<GraphServiceClient> GetGraphServiceClient();
    }
}
