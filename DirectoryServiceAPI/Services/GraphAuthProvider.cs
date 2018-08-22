using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using DirectoryServiceAPI.Models;
using Microsoft.Graph;
using DirectoryServiceAPI.Helpers;


namespace DirectoryServiceAPI.Services
{
    public class GraphAuthProvider : IGraphAuthProvider
    {
        private readonly IMemoryCache memoryCache;
        private TokenCache userTokenCache;

        // Properties used to get and manage an access token.
        private readonly string appId;
        private readonly ClientCredential credential;
        private readonly string[] scopes;
        private readonly string redirectUri;

        public GraphAuthProvider(IMemoryCache memoryCache, IConfiguration configuration)
        {
            var azureOptions = new AzureAdOptions();
            configuration.Bind("AzureAd", azureOptions);

            appId = azureOptions.ClientId;
            credential = new ClientCredential(azureOptions.ClientSecret);
            scopes = azureOptions.GraphScopes.Split(new[] { ' ' });
            redirectUri = azureOptions.BaseUrl + azureOptions.CallbackPath;

            this.memoryCache = memoryCache;
        }


        // Gets an access token. First tries to get the access token from the token cache.
        // Using password (secret) to authenticate. Production apps should use a certificate.
        public async Task<string> GetUserAccessTokenAsync(string userId)
        {
            userTokenCache = new SessionTokenCache(userId, memoryCache).GetCacheInstance();

            var cca = new ConfidentialClientApplication(
                appId,
                redirectUri,
                credential,
                userTokenCache,
                null);

            if (!cca.Users.Any()) throw new ServiceException(new Error
            {
                Code = "TokenNotFound",
                Message = "User not found in token cache. Maybe the server was restarted."
            });

            try
            {
                var result = await cca.AcquireTokenSilentAsync(scopes, cca.Users.First());
                return result.AccessToken;
            }

            // Unable to retrieve the access token silently.
            catch (Exception)
            {
                throw new ServiceException(new Error
                {
                    Code = GraphErrorCode.AuthenticationFailure.ToString(),
                    Message = "Caller needs to authenticate. Unable to retrieve the access token silently."
                });
            }
        }
    }
}
