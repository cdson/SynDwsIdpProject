using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace DirectoryServiceAPI.Controllers
{
    [Produces("application/json")]
    [Route("Directory")]
    public class DirectoryController : Controller
    {
        #region Graph Api
        internal static class RouteNames
        {
            public const string Users = nameof(Users);
            public const string UserById = nameof(UserById);
            public const string Groups = nameof(Groups);
            public const string GroupById = nameof(GroupById);
        }

        private readonly IADFactory factory;

        public DirectoryController(IADFactory factory)
        {
            this.factory = factory;
        }

        //directory/users/{id}
        [Route("users/{id}")]
        [HttpGet("{id}", Name = RouteNames.UserById)]
        public async Task<IActionResult> GetUser(string id)
        {
            User objUser = null;
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IADHandler adHandler = factory.GetIAM();
                    objUser = await adHandler.GetUser(id);
                }

                if (objUser == null)
                {
                    Log.Warning("No user found.");
                    throw new UserNotFoundException(id);
                }
                return Ok(objUser);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUserById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/users
        [Route("users/{filter?}/{startIndex?}/{count?}/{sortBy?}")]
        [HttpGet(Name = RouteNames.Users)]
        public async Task<IActionResult> GetUsers(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            UserResources objUsers = null;
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    // Get user's id for token cache.
                    var identifier = User.FindFirst(Startup.ObjectIdentifierType)?.Value;

                    IADHandler adHandler = factory.GetIAM();
                    objUsers = await adHandler.GetUsers(filter, startIndex, count, sortBy, identifier);
                }

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
        public async Task<IActionResult> GetGroup(string id)
        {
            Group objGroup = null;
            try
            {
                IADHandler adHandler = factory.GetIAM();
                objGroup = await adHandler.GetGroup(id);

                if (objGroup == null)
                {
                    Log.Warning("No group found.");
                    throw new GroupNotFoundException(id);
                }
                return Ok(objGroup);
            }
            catch (GroupNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroupById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //directory/groups
        [Route("groups/{filter?}/{startIndex?}/{count?}/{sortBy?}")]
        [HttpGet(Name = RouteNames.Groups)]
        public async Task<IActionResult> GetGroups(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            GroupResources objGroups = null;
            try
            {
                IADHandler adHandler = factory.GetIAM();
                objGroups = await adHandler.GetGroups(filter, startIndex, count, sortBy);

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
        #endregion


        #region Account api

        [Route("load")]
        public IActionResult Load()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity; // Azure AD V2 endpoint specific
                string preferred_username = identity.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                string name = identity.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                string userId = User.FindFirst(Startup.ObjectIdentifierType)?.Value;

                return RedirectToAction("GetUser", new { id = userId });
                //return $"Welcome {name} {preferred_username}";
            }

            //return "Not Authenticated...";
            return RedirectToAction("error");
        }


        [Route("signin")]
        [HttpGet]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action("Load", "Directory");
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                OpenIdConnectDefaults.AuthenticationScheme);
        }


        [Route("signout")]
        [HttpGet]
        public IActionResult SignOut()
        {
            var callbackUrl = Url.Action(nameof(SignedOut), "Directory", values: null, protocol: Request.Scheme);
            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Route("signedout")]
        [HttpGet]
        public string SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {

                return $"You are successfully signed out.";
            }

            return $"You are not authenticated";
        }


        [Route("error")]
        public string Error(string errorMsg)
        {
            return $"{errorMsg}";
        }
        #endregion
    }
}