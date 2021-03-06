﻿using System;
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
    [Route("directory")]
    public class DirectoryController : Controller
    {
        internal static class RouteNames
        {
            public const string Users = nameof(Users);
            public const string UserById = nameof(UserById);
            public const string Groups = nameof(Groups);
            public const string GroupById = nameof(GroupById);
        }

        private IGraphService graphService;
        private string ADType = "AzureAD"; // We can get this in constructor as parameter , or we can query tenent service for the same.

        public DirectoryController(IGraphService graphService)
        {
            this.graphService = graphService != null ? graphService : ADFactory.GetIAM(ADType);
        }

        [HttpGet("users/{id}", Name = RouteNames.UserById)]
        public async Task<IActionResult> GetUser(string id)
        {
            User objUser = null;
            try
            {
                if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }
                
                objUser = await graphService.GetUser(id);
                return Ok(objUser);
            }
            catch (NotFoundException ex)
            {
                Log.Warning(ex, "User not found.");
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUserById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //swagger does not support optional parameters.
        //[HttpGet("users/{filter?}/{startIndex?}/{count?}/{sortBy?}", Name = RouteNames.Users)]
        [HttpGet("users/")]
        public async Task<IActionResult> GetUsers(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            UserResources objUsers = null;
            try
            {
                objUsers = await graphService.GetUsers(filter, startIndex, count, sortBy);
                return Ok(objUsers);
            }
            catch (NotFoundException ex)
            {
                Log.Warning(ex, "Users not found.");
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (BadRequestException ex)
            {
                Log.Warning(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetUsers message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("groups/{id}", Name = RouteNames.GroupById)]
        public async Task<IActionResult> GetGroup(string id)
        {
            Group objGroup = null;
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }
                
                objGroup = await graphService.GetGroup(id);
                return Ok(objGroup);
            }
            catch (NotFoundException ex)
            {
                Log.Warning(ex, "Group not found.");
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroupById message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //swagger does not support optional parameters.
        //[HttpGet("groups/{filter?}/{startIndex?}/{count?}/{sortBy?}",Name = RouteNames.Groups)]
        [HttpGet("groups/")]
        public async Task<IActionResult> GetGroups(string filter = null, int? startIndex = null, int? count = null, string sortBy = null)
        {
            GroupResources objGroups = null;
            try
            {
                objGroups = await graphService.GetGroups(filter, startIndex, count, sortBy);
                return Ok(objGroups);
            }
            catch (NotFoundException ex)
            {
                Log.Warning(ex, "Groups not found.");
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (BadRequestException ex)
            {
                Log.Warning(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetGroups message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}