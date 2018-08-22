using DirectoryServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DirectoryServiceAPI.Services
{
  
    public class ADFactory : IADFactory //Concrete Creator
    {
        private readonly IGraphSdkHelper graphSdkHelper;
        public ADFactory(IGraphSdkHelper graphSdkHelper)
        {
            this.graphSdkHelper = graphSdkHelper;
        }

        public IADHandler GetIAM()
        {
            return new AzureADHandler(graphSdkHelper);

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
