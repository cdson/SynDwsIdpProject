﻿using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using DirectoryServiceAPI.Models;
using System.Collections.Generic;

namespace ServiceTest
{
    public static class Helper
    {
        public static User CreateUser()
        {
            User expected = new User() {id = "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0", givenName="Saket", surname= "Adhav", email=null, userPrincipalName= "saket.adhav@synerzipindia.onmicrosoft.com" };
            return expected;
        }

        public static UserResources CreateUserResources()
        {
            List<User> users = new List<User>() { new User() { id = "12502bbc-5e91-4e08-8de6-2f4163c127d7", givenName = "Chaitanya", surname = "Sonavale", email = null, userPrincipalName = "chaitanya.sonavale_synerzip.com#EXT#@synerzipindia.onmicrosoft.com" }, new User() { id = "7887e707-587f-44a7-98a9-002a0ad55580", givenName = "Pallavi", surname = "Jadhav", email = null, userPrincipalName = "pallavi.jadhav@synerzipindia.onmicrosoft.com" }, new User() { id = "8c09a9d1-f1a2-463b-bd0f-b10c51c437d0", givenName = "Saket", surname = "Adhav", email = null, userPrincipalName = "saket.adhav@synerzipindia.onmicrosoft.com" }};
            UserResources expected = new UserResources();
            expected.resources = users;
            return expected;
        }

        public static Group CreateGroup()
        {
            Group expected = new Group() { id = "722fd891-fe44-4bd1-b529-963f573ec969", displayName = "TestGroup1" };
            return expected;
        }

        public static GroupResources CreateGroupResources()
        {
            List<Group> groups = new List<Group>() { new Group() { id = "722fd891-fe44-4bd1-b529-963f573ec969", displayName= "TestGroup1" }, new Group() { id = "1986e105-886b-43b3-a1ff-cb2d89e04b9f", displayName = "TestGroup2" } };
            GroupResources expected = new GroupResources();
            expected.resources = groups;
            return expected;
        }
    }
}
