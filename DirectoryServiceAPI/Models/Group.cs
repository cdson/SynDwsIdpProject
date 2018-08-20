using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Models
{
    public class Group
    {
        public string id { get; set; }
        public string displayName { get; set; }
    }

    public class GroupResources
    {
        public int itemsPerPage { get; set; }
        public int startIndex { get; set; }
        public int totalResults { get; set; }
        public List<Group> resources { get; set; }
    }
}
