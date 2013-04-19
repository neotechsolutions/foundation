// -----------------------------------------------------------------------
// <copyright file="IConfigurationManager.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Core.Configuration
{
    using System.Collections.Specialized;
    using System.Configuration;

    /// <summary>
    /// Base configuration manager.
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets or sets the application settings.
        /// </summary>
        NameValueCollection AppSettings { get; set; }

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        ConnectionStringSettingsCollection ConnectionStrings { get; set; }

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>The found configuration section.</returns>
        object GetSection(string sectionName);
    }
}
