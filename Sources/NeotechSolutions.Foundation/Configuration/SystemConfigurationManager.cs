// -----------------------------------------------------------------------
// <copyright file="SystemConfigurationManager.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Configuration
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using SysConfigurationManager = System.Configuration.ConfigurationManager;

    /// <summary>
    /// Configuration Manager that uses <see cref="System.Configuration.ConfigurationManager"/>.
    /// </summary>
    public sealed class SystemConfigurationManager : IConfigurationManager
    {
        /// <inheritdoc />
        public NameValueCollection AppSettings
        {
            get
            {
                return SysConfigurationManager.AppSettings;
            }

            set
            {
                throw new InvalidOperationException();
            }
        }

        /// <inheritdoc />
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                return SysConfigurationManager.ConnectionStrings;
            }

            set
            {
                throw new InvalidOperationException();
            }
        }

        /// <inheritdoc />
        public object GetSection(string sectionName)
        {
            return SysConfigurationManager.GetSection(sectionName);
        }
    }
}
