// -----------------------------------------------------------------------
// <copyright file="NamedElementCollection{T}.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Configuration
{
    using System;
    using System.Configuration;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Base configuration element collection.
    /// </summary>
    /// <typeparam name="T">Type of the configuration element.</typeparam>
    public class NamedElementCollection<T> : ConfigurationElementCollection where T : NamedConfigurationElement, new()
    {
        /// <inheritdoc />
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        /// <summary>
        /// Gets or sets a property, attribute, or child element of this configuration element.
        /// </summary>
        /// <returns>The specified property, attribute, or child element.</returns>
        /// <param name="index">Index of the element.</param>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException"><paramref name="prop"/> is read-only or locked.</exception>
        public T this[int index]
        {
            get
            {
                Contract.Requires<ArgumentException>(index > -1, "index");
                return (T)this.BaseGet(index);
            }

            set
            {
                Contract.Requires<ArgumentException>(index > -1);
                Contract.Requires<ArgumentNullException>(value != null, "value");
                ConfigurationElement element = this.BaseGet(index);
                if (element != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets or sets a property, attribute, or child element of this configuration element.
        /// </summary>
        /// <param name="name">Name of the configuration element.</param>
        /// <returns>The specified property, attribute, or child element.</returns>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException"><paramref name="prop"/> is read-only or locked.</exception>
        public new T this[string name]
        {
            get
            {
                Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
                return this.Get(name);
            }

            set
            {
                T element = this.Get(name);
                if (element != null)
                {
                    this.Remove(name);
                }

                if (value != null)
                {
                    this.Add(value);
                }
            }
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <returns>Index of the value, -1 if not found.</returns>
        public int IndexOf(T value)
        {
            Contract.Requires<ArgumentNullException>(value != null);
            return this.BaseIndexOf(value);
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element to add.</param>
        public void Add(T element)
        {
            Contract.Requires<ArgumentNullException>(element != null, "element");
            this.BaseAdd(element);
        }

        /// <summary>
        /// Gets the specified element with name <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the configuration element.</param>
        /// <returns>The found configuration element, null if not found.</returns>
        public T Get(string name)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
            return (T)this.BaseGet(name);
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        public void Remove(T value)
        {
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(value.Name) && value.Name.Length > 0);
            this.Remove(value.Name);
        }

        /// <summary>
        /// Removes the specified element with key is <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the element to remove.</param>
        public void Remove(string name)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
            this.BaseRemove(name);
        }

        /// <summary>
        /// Removes the element at the specified index.
        /// </summary>
        /// <param name="index">The index to remove.</param>
        public void RemoveAt(int index)
        {
            Contract.Requires<IndexOutOfRangeException>(index >= 0);
            this.BaseRemoveAt(index);
        }

        /// <inheritdoc />
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        /// <inheritdoc />
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NamedConfigurationElement)element).Name;
        }
    }
}
