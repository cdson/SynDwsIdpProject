using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Models
{

    public class User
    {
        public string id { get; set; }
        public string givenName { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string email { get; set; }
    }

    public class UserResources
    {
        public int itemsPerPage { get; set; }
        public int startIndex { get; set; }
        public int totalResults { get; set; }
        public List<User> resources { get; set; }
    }
}
