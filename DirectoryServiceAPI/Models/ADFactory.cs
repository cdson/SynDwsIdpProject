using DirectoryServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectoryServiceAPI.Services.Interfaces;

namespace DirectoryServiceAPI.Models
{
  
    public class ConcreteADFactory : IADFactory //Concrete Creator
    {
        public IADHandler GetIAM()
        {
            return new AzureADHandler();

            //switch (Ad) // get this from parameter
            //{
            //    case "AzureAD":
            //        return new AzureADHandler();
            //    default:
            //        throw new ApplicationException(string.Format("IAM '{0}' is not found", Ad));
            //}
        }

    }
}
