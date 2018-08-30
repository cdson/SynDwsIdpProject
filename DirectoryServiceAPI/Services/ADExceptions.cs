/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */

using System;
using DirectoryServiceAPI.Models;

namespace DirectoryServiceAPI.Services
{

    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Not found")
        {

        }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(): base("Bad Request")
        {

        }
    }
}
