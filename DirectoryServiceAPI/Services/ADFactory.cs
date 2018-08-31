using DirectoryServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DirectoryServiceAPI.Services
{
    public class ADFactory
    {
        private static readonly MicrosoftGraphService microsoftGraphService;

        static ADFactory()
        {
            if(microsoftGraphService == null)
            {
                microsoftGraphService = new MicrosoftGraphService();
            }
        }

        public static IGraphService GetIAM(string Ad)
        {
            //return new AzureADHandler(graphService);

            switch (Ad)
            {
                case "AzureAD":
                    return microsoftGraphService;
                default:
                    throw new NotImplementedException(string.Format("IAM '{0}' not found", Ad));
            }
        }
    }

}
