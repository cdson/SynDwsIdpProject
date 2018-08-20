using DirectoryServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Models
{
    //public abstract class ADFactory //Creator
    //{
    //    public abstract IRequestHandler GetIAM(string Ad);

    //}

    //public class ConcreteADFactory : ADFactory //Concrete Creator
    //{
    //    public override IRequestHandler GetIAM(string Ad)
    //    {
    //        switch (Ad)
    //        {
    //            case "AzureAD":
    //                return new AzureADHandler();
    //            default:
    //                throw new ApplicationException(string.Format("IAM '{0}' is not found", Ad));
    //        }
    //    }

    //}


    public interface IADFactory //Creator
    {
        IRequestHandler GetIAM();

    }

    public class ConcreteADFactory : IADFactory //Concrete Creator
    {
        public IRequestHandler GetIAM()
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
