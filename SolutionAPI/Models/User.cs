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

    public class MockedDataForUser
    {
           public static List<User> AllUsers = new List<User>() { new User { UserId = 1, UserName = "Chaitanya" }, new User { UserId = 2, UserName = "Saket" }, new User { UserId = 3, UserName = "Pallavi" } };
    }
}
