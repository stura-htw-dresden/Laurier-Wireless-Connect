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
using System.Runtime.Serialization;

namespace OpenSourceAtLaurier.LaurierWirelessClientAutoconf
{
    [Serializable]
    public class ProcessException : ApplicationException
    {
        private int exitCode;
        private string standardError;

        public int ExitCode
        {
            get
            {
                return exitCode;
            }
        }
        
        public string StandardError
        {
            get
            {
                return standardError;
            }
        }

        public ProcessException() 
            : base()
        {
        }

        public ProcessException(string message)
            : base(message)
        {
        }

        public ProcessException(string message, int exitCode)
            : base(message)
        {
            this.exitCode = exitCode;
        }

        public ProcessException(string message, string standardError)
            : base(message)
        {
            this.standardError = standardError;
        }

        public ProcessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ProcessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}