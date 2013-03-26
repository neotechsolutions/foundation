// -----------------------------------------------------------------------
// <copyright file="NamedConfigurationElement.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Configuration
{
    using System.Configuration;

    /// <summary>
    /// A configuration element defined by its name.
    /// </summary>
    public abstract class NamedConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Name of the xml attribute "name".
        /// </summary>
        private const string PropertyName = "name";

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedConfigurationElement"/> class.
        /// </summary>
        protected NamedConfigurationElement()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of the configuration element.
        /// </value>
        [ConfigurationProperty(PropertyName, IsKey = true, IsRequired = true)]
        public virtual string Name
        {
            get
            {
                return this[PropertyName] as string;
            }

            set
            {
                this[PropertyName] = value;
            }
        }
    }
}
