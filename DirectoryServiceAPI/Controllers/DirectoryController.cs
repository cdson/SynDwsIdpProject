using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;

namespace DirectoryServiceAPI.Controllers
{
    [Produces("application/json")]
    [Route("Directory")]
    public class DirectoryController : Controller
    {
        internal static class RouteNames
        {
            public const string Users = nameof(Users);
            public const string UserById = nameof(UserById);
            public const string Groups = nameof(Groups);
            public const string GroupById = nameof(GroupById);
        }

        private readonly IADFactory _factory;

        public DirectoryController(IADFactory factory)
        {
            _factory = factory;
        }

        //directory/users/{id}
        [Route("users/{id}")]
        [HttpGet("{id}", Name = RouteNames.UserById)]
        public async Task<IActionResult> GetUserById(int id)
        {
            User objUser = null;
            try
            {
                IRequestHandler azureObj = _factory.GetIAM();
                objUser = await azureObj.GetUserById(id);

                //objUser = await requestHandler.GetUserById(id);

                if (objUser == null)
                {
                    Log.Warning("No user found.");
                    throw new UserNotFoundException(id.ToString());
                }
                return Ok(objUser);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUserById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/users
        [Route("users/{filter?}/{startIndex?}/{count?}/{sortBy?}")]
        [Route("users")]
        [HttpGet(Name = RouteNames.Users)]
        [HttpGet(Name = RouteNames.Users)]
        public async Task<IActionResult> GetUsers(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            List<User> objUsers = null;
            try
            {
                //ADFactory factory = new ConcreteADFactory();
                //IRequestHandler azureObj = factory.GetIAM("AzureAD");
                //objUsers = await azureObj.GetUsers();

                if (objUsers == null)
                {
                    Log.Warning("No users found.");
                    throw new UserNotFoundException();
                }
                return Ok(objUsers);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUsers message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/groups/{id}
        [Route("groups/{id}")]
        [HttpGet("{id}", Name = RouteNames.GroupById)]
        public async Task<IActionResult> GetGroupById(int id)
        {
            Group objGroup = null;
            try
            {
                //ADFactory factory = new ConcreteADFactory();
                //IRequestHandler azureObj = factory.GetIAM("AzureAD");
                //objGroup = await azureObj.GetGroupById(id);

                if (objGroup == null)
                {
                    Log.Warning("No group found.");
                    throw new GroupNotFoundException(id.ToString());
                }
                return Ok(objGroup);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroupById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/groups
        //directory/groups
        [Route("groups/{filter?}/{startIndex?}/{count?}/{sortBy?}")]
        [Route("groups")]
        [HttpGet(Name = RouteNames.Groups)]
        [HttpGet(Name = RouteNames.Groups)]
        public async Task<IActionResult> GetGroups(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            List<Group> objGroups = null;
            try
            {
                //ADFactory factory = new ConcreteADFactory();
                //IRequestHandler azureObj = factory.GetIAM("AzureAD");
                //objGroups = await azureObj.GetGroups();

                if (objGroups == null)
                {
                    Log.Warning("No groups found.");
                    throw new GroupNotFoundException();
                }
                return Ok(objGroups);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroups message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}