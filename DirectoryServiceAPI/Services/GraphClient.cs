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

        //TODO//access these from appsetting.json
        private static string clientId = $"e9a97d45-1fc8-49c0-aaf2-baa7337749d7";
        private static string clientSecret = $"oTfUEBJGN9yPyce4A3Z/7Gk60IYqfn4EUm0LHOdZVis=";
        private static string tenantId = $"6d8173a5-e794-43d6-b2cc-d7704238aa56";
        private static string aadInstance = $"https://login.microsoftonline.com/";
        private static string graphResource = $"https://graph.microsoft.com/";


        public async Task<GraphServiceClient> GetGraphServiceClient()
        {
            var graphAPIEndpoint = $"{graphResource}v1.0";

            var accessToken = await GetAccessToken();

            graphClient = new GraphServiceClient(graphAPIEndpoint, new DelegateAuthenticationProvider(
                async requestMessage =>
                {
                    // Append the access token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString());
                }));

            return graphClient;
        }

        private async Task<string> GetAccessToken()
        {
            string authority = String.Concat(aadInstance, tenantId);


            AuthenticationContext authenticationContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(clientId, clientSecret);

            // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(graphResource, clientCred);
            var token = authenticationResult.AccessToken;

            return token;
        }
    }
}
