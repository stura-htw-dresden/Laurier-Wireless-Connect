﻿#region Copyright
// <copyright file="LICENSE.txt" company="Open Source at Laurier">
//
// Copyright (C) 2010 Open Source at Laurier
//
// This file is part of the Laurier Wireless Connect application.
//
// The Laurier Wireless Connect application is free software: you can
// redistribute it and/or modify it under the terms of the GNU General 
// Public License as published by the Free Software Foundation, either 
// version 3 of the License, or (at your option) any later version.
//
// The Laurier Wireless Connect application is distributed in the 
// hope that it will be useful, but WITHOUT ANY WARRANTY; without 
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
// PARTICULAR PURPOSE.  See the GNU General Public License for more 
// details.
//
// You should have received a copy of the GNU General Public License
// along with the Laurier Wireless Connect application.  If not,
// see <http://www.gnu.org/licenses/>.
// </copyright>
#endregion

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace OpenSourceAtLaurier.LaurierWirelessConnect
{
    class MergeRegistryKeysCommand : ICommand
    {
        private const string humanReadableExecutionDescription = "Now registering SecureW2 EAP client settings";

        public string HumanReadableExecutionDescription
        {
            get
            {
                return humanReadableExecutionDescription;
            }
        }

        /// <summary>
        /// Merge SecureW2 default profile settings and blank user credentials for profile into the registry
        /// </summary>
        public void Execute()
        {
            HelperMethods.WriteEmbeddedFileToDisk("SecureW2_ProfileSetup.reg");
            Process mergeRegistryKeys = Process.Start(HelperMethods.SetupProcess("REG", "IMPORT SecureW2_ProfileSetup.reg"));
            HelperMethods.MonitorProcessOutput(mergeRegistryKeys, "Error importing SecureW2 registry keys");
            File.Delete("SecureW2_ProfileSetup.reg");
        }
    }
}
