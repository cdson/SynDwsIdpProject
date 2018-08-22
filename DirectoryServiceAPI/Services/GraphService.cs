using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;


namespace DirectoryServiceAPI.Services
{
    public static class GraphService
    {
        public static async Task<string> GetUserJsonById(GraphServiceClient graphClient, string userID)
        {
            if (userID == null)
                return JsonConvert.SerializeObject(new { Message = "User Id cannot be null." }, Formatting.Indented);

            // Load user profile.
            var user = await graphClient.Users[userID].Request().GetAsync();
            return JsonConvert.SerializeObject(user, Formatting.Indented);
        }

        public static async Task<string> GetAllUsersJson(GraphServiceClient graphClient, string filter, int? startIndex, int? count, string sortBy)
        {
            // Load users profile.
            var users = await graphClient.Users.Request().GetAsync();
            return JsonConvert.SerializeObject(users, Formatting.Indented);
        }
    }
}
