using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryServiceAPI.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }

    public class MockedDataForGroup
    {
        public static List<Group> AllGroups = new List<Group>() { new Group { Id = 1, DisplayName = "Test group 1" }, new Group { Id = 2, DisplayName = "Test group 2" }};
    }
}
