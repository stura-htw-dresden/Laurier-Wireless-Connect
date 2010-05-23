﻿#region Copyright
// <copyright file="LICENSE.txt" company="Open Source at Laurier">
//
// Copyright (C) 2010 Open Source at Laurier
//
// This file is part of the Laurier Wireless Autoconfiguration Tool.
//
// The Laurier Wireless Autoconfiguration Tool is free software: you can
// redistribute it and/or modify it under the terms of the GNU General 
// Public License as published by the Free Software Foundation, either 
// version 3 of the License, or (at your option) any later version.
//
// The Laurier Wireless Autoconfiguration Tool is distributed in the 
// hope that it will be useful, but WITHOUT ANY WARRANTY; without 
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
// PARTICULAR PURPOSE.  See the GNU General Public License for more 
// details.
//
// You should have received a copy of the GNU General Public License
// along with the Laurier Wireless Autoconfiguration Tool.  If not,
// see <http://www.gnu.org/licenses/>.
// </copyright>
#endregion

using System;
using System.Windows.Forms;

namespace OpenSourceAtLaurier.LaurierWirelessClientAutoconf
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!IsClientSupportable())
            {
                MessageBox.Show("Your operating system does not meet the minimum requirements for this application.",
                    "Unsupportable operating system");
                Application.Exit();
            }
            
            if (!IsClientOfficiallySupported())
            {
                if(MessageBox.Show(@"This application has not been tested on your operating system. 
                    It may or may not run successfully. Do you wish to continue?", "Unsupported operating system",
                                                                                 MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    Application.Exit();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormSetupProgress());

            ICommand installSecureW2 = new InstallSecureW2Command();
            ICommand mergeRegistryKeys = new MergeRegistryKeysCommand();
            ICommand setupProfile = new SetupProfileCommand();

            try
            {
                installSecureW2.Execute();
                mergeRegistryKeys.Execute();
                setupProfile.Execute();
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred: " + e.Message);
            }

            Application.Exit();
        }

        /// <summary>
        /// Returns whether the application has been tested successfully on this operating system
        /// </summary>
        /// <returns>True if the application has been tested successfully with this operating system, false if not</returns>
        static bool IsClientOfficiallySupported()
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            Version requiredMinimumVersion = new Version(5, 1, 5512);

            bool isXpSp3 = (osInfo.Version.Major == 5 && osInfo.Version.Minor == 1 && osInfo.Version.Build == 5512);
            bool isVistaSp2 = (osInfo.Version.Major == 6 && osInfo.Version.Minor == 0 && osInfo.Version.Build == 6002);
            bool is7Sp0 = (osInfo.Version.Major == 6 && osInfo.Version.Minor == 1 && osInfo.Version.Build == 7600);

            return (isXpSp3 || isVistaSp2 || is7Sp0) ? true : false;
        }

        /// <summary>
        /// Returns whether this application should work on this operating system
        /// </summary>
        /// <returns>True if the application should run successfully on this operating system, false if not</returns>
        static bool IsClientSupportable()
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            Version requiredMinimumVersion = new Version(5, 1, 5512);

            bool isXpSp3OrGreater = (osInfo.Version.Major == 5 && osInfo.Version.Minor == 1 && osInfo.Version.Build >= 5512);
            bool isVistaSp2OrGreater = (osInfo.Version.Major == 6 && osInfo.Version.Minor == 0 && osInfo.Version.Build >= 6002);
            bool is7Sp0OrGreater = (osInfo.Version.Major == 6 && osInfo.Version.Minor == 1  && osInfo.Version.Build >= 7600);
            bool isGreaterThan7 = (osInfo.Version.Major >= 7);

            return (isXpSp3OrGreater || isVistaSp2OrGreater || is7Sp0OrGreater || isGreaterThan7) ? true : false;
        }
    }
}
