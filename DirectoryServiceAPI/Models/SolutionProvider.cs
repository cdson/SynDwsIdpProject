using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolutionAPI.Models
{
    public class SolutionProvider
    {
        public string SolutionPorviderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SubscriptionURL { get; set; }
        public string StatusURL { get; set; }
        public string CreateAndSubscriptionURL { get; set; }


        public const string SkuRegEx = @"^[a-zA-Z0-9\s_,]*$";
        internal static bool IsValidSku(string sku)
        {
            //Todo : validate below correctly for now sending as valid sku.
            return true;
           // return Regex.IsMatch(sku, SkuRegEx);
        }
    }
}
