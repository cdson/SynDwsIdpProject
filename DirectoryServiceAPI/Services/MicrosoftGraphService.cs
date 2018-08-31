using DirectoryServiceAPI.Helpers;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;
using Microsoft.Graph;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services
{
    public class MicrosoftGraphService : IGraphService //Concrete Product , similar such product classes can be added
    {
        //private readonly MicrosoftGraphClient graphClient;
        //public AzureADHandler()
        //{
        //    this.graphClient = MicrosoftGraphClient.graphClient;
        //}

        public async Task<Models.User> GetUser(string id)
        {
            try
            {
                Models.User objUser = new Models.User();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await MicrosoftGraphClient.GetGraphServiceClient();

                // Load user profile.
                var user = await client.Users[id].Request().GetAsync();

                // Copy Microsoft-Graph User to DTO User
                objUser = CopyHandler.UserProperty(user);

                return objUser;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    Log.Warning(ex.Message);
                    throw new NotFoundException();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<UserResources> GetUsers(string filter, int? startIndex, int? count, string sortBy)
        {
            try
            {
                UserResources users = new UserResources();
                users.resources = new List<Models.User>();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await MicrosoftGraphClient.GetGraphServiceClient();

                // Load users profiles.
                var userList = await client.Users.Request().Filter($"{filter}").GetAsync();

                // Copy Microsoft User to DTO User
                foreach (var user in userList)
                {
                    var objUser = CopyHandler.UserProperty(user);
                    users.resources.Add(objUser);
                }
                users.totalResults = users.resources.Count;

                return users;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    Log.Warning(ex.Message);
                    throw new BadRequestException();
                }
                else if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    Log.Warning(ex.Message);
                    throw new NotFoundException();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<Models.Group> GetGroup(string id)
        {
            try
            {
                Models.Group objGroup = new Models.Group();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await MicrosoftGraphClient.GetGraphServiceClient();

                // Load group profile.
                var group = await client.Groups[id].Request().GetAsync();

                // Copy Microsoft-Graph Group to DTO Group
                objGroup = CopyHandler.GroupProperty(group);

                return objGroup;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    Log.Warning(ex.Message);
                    throw new NotFoundException();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<GroupResources> GetGroups(string filter, int? startIndex, int? count, string sortBy)
        {
            try
            {
                GroupResources groups = new GroupResources();
                groups.resources = new List<Models.Group>();

                // Initialize the GraphServiceClient.
                GraphServiceClient client = await MicrosoftGraphClient.GetGraphServiceClient();

                // Load groups profiles.
                var groupList = await client.Groups.Request().Filter($"{filter}").GetAsync();

                // Copy Microsoft-Graph Group to DTO Group
                foreach (var group in groupList)
                {
                    var objGroup = CopyHandler.GroupProperty(group);
                    groups.resources.Add(objGroup);
                }
                groups.totalResults = groups.resources.Count;

                return groups;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    Log.Warning(ex.Message);
                    throw new BadRequestException();
                }
                else if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    Log.Warning(ex.Message);
                    throw new NotFoundException();
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
