using DirectoryServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Helpers
{
    public class CopyHandler
    {
        public static User PropertyCopy(Microsoft.Graph.User graphUser)
        {
            User user = new User();
            user.id = graphUser.Id;
            user.givenName = graphUser.GivenName;
            user.surname = graphUser.Surname;
            user.userPrincipalName = graphUser.UserPrincipalName;
            user.email = graphUser.Mail;

            return user;
        }
    }
}
