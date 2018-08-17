using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionAPI.Models
{
    public class UserSearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GroupSearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
