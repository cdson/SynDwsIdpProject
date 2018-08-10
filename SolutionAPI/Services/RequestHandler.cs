using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolutionAPI.Models;

namespace SolutionAPI.Services
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IDataAccess dataAccess;

        public RequestHandler(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku)
        {
            return await dataAccess.GetSolutionProvidersForSKU(sku);
        }
    }
}
