using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SolutionAPI.Models;
using System.Collections.Generic;

namespace SolutionAPI.Services
{
    public interface IRequestHandler
    {
        Task<List<SolutionProvider>> GetSolutionProvidersForSKU(string sku);
    }

    
}
