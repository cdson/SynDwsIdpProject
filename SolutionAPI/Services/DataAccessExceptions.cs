/*
 * ©Copyright 2018 Dell, Inc., All Rights Reserved.
 * This material is confidential and a trade secret.  Permission to use this
 * work for any purpose must be obtained in writing from Dell, Inc.
 */

using System;
using SolutionAPI.Models;

namespace SolutionAPI.Services
{
    public class SolutionProvidersNotFoundException : Exception
    {
        public string Sku { get; private set; }

        public SolutionProvidersNotFoundException(string sku)
            : base($"Solution Providers not found for Sku")
        {
            Sku = sku ?? string.Empty;
        }
    }
}
