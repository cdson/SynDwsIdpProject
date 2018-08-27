using DirectoryServiceAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services
{
    public class GraphClient : IGraphClient
    {
        private GraphServiceClient graphClient;
        private IConfiguration configuration;

        private string clientId;
        private string clientSecret;
        private string tenantId;
        private string aadInstance;
        private string graphResource;
        private string graphAPIEndpoint;
        private string authority;
        
        public GraphClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            // Set AzureAD options
            SetAzureADOptions();
        }

        public async Task<GraphServiceClient> GetGraphServiceClient()
        {
            // Get Access Token and Microsoft Graph Client using access token and microsoft graph v1.0 endpoint
            var delegateAuthProvider = await GetAuthProvider();
            // Initializing the GraphServiceClient
            graphClient = new GraphServiceClient(graphAPIEndpoint, delegateAuthProvider);

            return graphClient;
        }

        private void SetAzureADOptions()
        {
            var azureOptions = new AzureAD();
            configuration.Bind("AzureAD", azureOptions);

            clientId = azureOptions.ClientId;
            clientSecret = azureOptions.ClientSecret;
            tenantId = azureOptions.TenantId;
            aadInstance = azureOptions.Instance;
            graphResource = azureOptions.GraphResource;
            graphAPIEndpoint = $"{azureOptions.GraphResource}{azureOptions.GraphResourceEndPoint}";
            authority = String.Concat(aadInstance, tenantId);
        }

        private async Task<IAuthenticationProvider> GetAuthProvider()
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(clientId, clientSecret);

            // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(graphResource, clientCred);
            var token = authenticationResult.AccessToken;

            var delegateAuthProvider = new DelegateAuthenticationProvider((requestMessage) =>
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token.ToString());
                return Task.FromResult(0);
            });

            return delegateAuthProvider;
        }
    }
}
