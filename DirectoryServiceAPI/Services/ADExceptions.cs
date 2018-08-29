/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */

using System;
using DirectoryServiceAPI.Models;

namespace DirectoryServiceAPI.Services
{

    public class UserNotFoundException : Exception
    {
        public string UserId { get; private set; }

        public UserNotFoundException()
            : base("No users found")
        {

        }

        public UserNotFoundException(string userId)
            : base("No user found for UserId")
        {
            UserId = userId ?? string.Empty;
        }
    }

    public class UserBadRequestException : Exception
    {
        public string UserId { get; private set; }

        public UserBadRequestException()
            : base("Bad Request")
        {

        }
    }

    public class GroupNotFoundException : Exception
    {
        public string GroupId { get; private set; }

        public GroupNotFoundException()
            : base("No groups found")
        {

        }

        public GroupNotFoundException(string groupId)
            : base("No group found for groupId")
        {
            GroupId = groupId ?? string.Empty;
        }
    }

    public class GroupBadRequestException : Exception
    {
        public string GroupId { get; private set; }

        public GroupBadRequestException()
            : base("Bad Request")
        {

        }
    }
}
