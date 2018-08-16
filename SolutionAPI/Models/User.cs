using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class MSGraphUser
    {
        [JsonProperty(PropertyName = "@odata.type")]
        public string odataType { get; set; }

        [JsonProperty(PropertyName = "@odata.id")]
        public string odataId { get; set; }

        public List<string> businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string jobTitle { get; set; }
        public string mail { get; set; }
        public string mobilePhone { get; set; }
        public string officeLocation { get; set; }
        public string preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }
    }

    public class MsGraphUserListResponse
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string context { get; set; }

        public List<MSGraphUser> value { get; set; }
    }

    public class MockedDataForUser
    {
           public static List<User> AllUsers = new List<User>() { new User { UserId = 1, UserName = "Chaitanya" }, new User { UserId = 2, UserName = "Saket" }, new User { UserId = 3, UserName = "Pallavi" } };
    }
}
