/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;
using DirectoryServiceAPI.Services;
using System.Collections.Generic;

namespace DirectoryServiceAPI.Controllers
{
    [Route("solution")]
    [Produces("application/json")]
    public class SolutionController : Controller
    {
        internal static class RouteNames
        {
            public const string GetSolutionProvidersForSku = nameof(GetSolutionProvidersForSku);
        }

        private readonly IRequestHandler requestHandler;

        public SolutionController(IRequestHandler requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        [HttpGet(Name = RouteNames.GetSolutionProvidersForSku)]
        public async Task<IActionResult> GetSolutionProvidersForSku([FromQuery] string sku)
        {
            List<SolutionProvider> objSolutionProvider = null;
            try
            {
                if (!string.IsNullOrEmpty(sku))
                {
                    // Validate sku *is* one of ours matching formats.
                    if (!SolutionProvider.IsValidSku(sku))
                    {
                        Log.Warning($"SKU provided to GetSolutionProvidersForSku does not meet our restrictions. sku: '{sku}'");
                        return BadRequest();
                    }
                    //objSolutionProvider = await requestHandler.GetSolutionProvidersForSKU(sku);
                    
                }
                else
                {
                    return BadRequest();
                }

                if (objSolutionProvider == null)
                {
                    Log.Warning($"Solution Providers For Sku : {sku} not found");
                    throw new SolutionProvidersNotFoundException(sku);
                }
                return Ok(objSolutionProvider);
            }
            catch (SolutionProvidersNotFoundException ex)
            {
                Log.Warning(ex, ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred while handling a GetSolutionProvidersForSku message.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool IsValidGuid(string guid)
        {
            return Guid.TryParse(guid, out Guid guidOut);
        }

    }

}
