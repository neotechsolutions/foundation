// -----------------------------------------------------------------------
// <copyright file="ConfigurationManager.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Configuration
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using NeotechSolutions.Foundation.Injection;

    /// <summary>
    /// A manager for application configuration.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Base configuration manager to use.
        /// </summary>
        private static List<IConfigurationManager> configurationManagers;

        /// <summary>
        /// Merged application settings.
        /// </summary>
        private static NameValueCollection appSettings;

        /// <summary>
        /// Merge connection strings.
        /// </summary>
        private static ConnectionStringSettingsCollection connectionStrings;

        /// <summary>
        /// Found configuration section.
        /// </summary>
        private static ConcurrentDictionary<string, ConfigurationSection> sections = new ConcurrentDictionary<string, ConfigurationSection>();

        /// <summary>
        /// Prevents multi-threading access on properties of the static instance of the <see cref="ConfigurationManager"/>.
        /// </summary>
        private static object syncLock = new object();

        /// <summary>
        /// Initializes static members of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        static ConfigurationManager()
        {
            Initialize();
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        public static NameValueCollection AppSettings
        {
            get
            {
                if (appSettings == null)
                {
                    lock (syncLock)
                    {
                        if (appSettings == null)
                        {
                            appSettings = GetAppSettings();
                        }
                    }
                }

                return appSettings;
            }
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if (connectionStrings == null)
                {
                    lock (syncLock)
                    {
                        if (connectionStrings == null)
                        {
                            connectionStrings = GetConnectionStrings();
                        }
                    }
                }

                return connectionStrings;
            }
        }

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <typeparam name="TSection">The type of the section.</typeparam>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>The found configuration section.</returns>
        public static TSection GetSection<TSection>(string sectionName) where TSection : ConfigurationSection
        {
            return sections.GetOrAdd(sectionName, GetSection) as TSection;
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <returns>Merged Application Settings as a <see cref="NameValueCollection"/>.</returns>
        private static NameValueCollection GetAppSettings()
        {
            NameValueCollection appSettings = new NameValueCollection();
            foreach (IConfigurationManager configManager in configurationManagers)
            {
                NameValueCollection confManagerAppSettings = configManager.AppSettings;
                if (confManagerAppSettings == null || confManagerAppSettings.Count < 1)
                {
                    continue;
                }

                int count = confManagerAppSettings.Count;
                for (int i = 0; i < count; ++i)
                {
                    string name = confManagerAppSettings.GetKey(i);
                    if (appSettings.Get(name) == null)
                    {
                        string value = confManagerAppSettings.Get(name);
                        if (value != null)
                        {
                            appSettings.Set(name, value);
                        }
                    }
                }
            }

            return appSettings;
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <returns>Merged ConnectionStrings for the current application.</returns>
        private static ConnectionStringSettingsCollection GetConnectionStrings()
        {
            ConnectionStringSettingsCollection connectionStrings = new ConnectionStringSettingsCollection();
            foreach (IConfigurationManager configManager in configurationManagers)
            {
                ConnectionStringSettingsCollection configManagerConnectionStrings = configManager.ConnectionStrings;
                if (configManagerConnectionStrings == null || configManagerConnectionStrings.Count < 1)
                {
                    continue;
                }

                foreach (ConnectionStringSettings connectionString in configManagerConnectionStrings)
                {
                    if (connectionStrings[connectionString.Name] == null)
                    {
                        connectionStrings.Add(connectionString);
                    }
                }
            }

            return connectionStrings;
        }

        /// <summary>
        /// Gets the configuration section with the specified name.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>The found configuration section.</returns>
        private static ConfigurationSection GetSection(string sectionName)
        {
            ConfigurationSection section = null;
            foreach (IConfigurationManager configManager in configurationManagers)
            {
                section = configManager.GetSection(sectionName) as ConfigurationSection;
                if (section != null)
                {
                    break;
                }
            }

            return section;
        }

        /// <summary>
        /// Initializes configuration managers.
        /// </summary>
        private static void Initialize()
        {
            IEnumerable<IConfigurationManager> configManagers = ServiceLocator.ResolveAll<IConfigurationManager>();
            if (configManagers != null)
            {
                configurationManagers = configManagers.ToList<IConfigurationManager>();
            }
        }
    }
}
