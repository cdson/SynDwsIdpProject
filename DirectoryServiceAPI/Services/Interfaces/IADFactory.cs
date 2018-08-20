using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Services.Interfaces
{
    public interface IADFactory //Creator
    {
        IADHandler GetIAM();

    }
}
