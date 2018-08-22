using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using System.Net.Http.Headers;

namespace DirectoryServiceAPI.Services
{
    public class GraphSdkHelper : IGraphSdkHelper
    {
        private readonly IGraphAuthProvider authProvider;
        private GraphServiceClient graphClient;

        public GraphSdkHelper(IGraphAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        // Get an authenticated Microsoft Graph Service client.
        public GraphServiceClient GetAuthenticatedClient(string userId)
        {
            //read this from appsettings.json
            var graphAPIEndpoint = $"https://graph.microsoft.com/v1.0";

            graphClient = new GraphServiceClient(graphAPIEndpoint, new DelegateAuthenticationProvider(
                async requestMessage =>
                {
                    // Passing tenant ID to the sample auth provider to use as a cache key
                    var accessToken = await authProvider.GetUserAccessTokenAsync(userId);

                    // Append the access token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // This header identifies the sample in the Microsoft Graph service. If extracting this code for your project please remove.
                    ////requestMessage.Headers.Add("SampleID", "aspnetcore-connect-sample");
                }));

            return graphClient;
        }
    }
}
