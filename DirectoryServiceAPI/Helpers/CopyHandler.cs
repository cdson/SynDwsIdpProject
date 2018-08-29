using DirectoryServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Helpers
{
    public class CopyHandler
    {
        public static User UserProperty(Microsoft.Graph.User graphUser)
        {
            User user = new User();
            user.id = graphUser.Id;
            user.givenName = graphUser.GivenName;
            user.surname = graphUser.Surname;
            user.userPrincipalName = graphUser.UserPrincipalName;
            user.email = graphUser.Mail;

            return user;
        }

        public static Group GroupProperty(Microsoft.Graph.Group graphGroup)
        {
            Group group = new Group();
            group.id = graphGroup.Id;
            group.displayName = graphGroup.DisplayName;

            return group;
        }
    }
}
