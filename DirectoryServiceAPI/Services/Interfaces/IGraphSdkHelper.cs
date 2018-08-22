using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace DirectoryServiceAPI.Services
{
    public interface IGraphSdkHelper
    {
        GraphServiceClient GetAuthenticatedClient(string userId);
    }
}
