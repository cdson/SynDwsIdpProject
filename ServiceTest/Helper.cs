using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using SolutionAPI.Models;
using System.Collections.Generic;

namespace ServiceTest
{
    public static class Helper
    {
        #region Tenant Subscriptions Test cases
        internal static async Task<List<SolutionProvider>> GetSolutionProvidersForSKURequestAsync(string sku)
        {
            return await Startup.DataAccess.GetSolutionProvidersForSKU(sku);
        }
        #endregion 
    }
}
