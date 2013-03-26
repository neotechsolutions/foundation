// -----------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Injection
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    /// <summary>
    /// Facade for injection of controls.
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// Container for the current application.
        /// </summary>
        private static UnityContainer container;

        /// <summary>
        /// Indicates if the container is initialized or not.
        /// </summary>
        private static bool initialized;

        /// <summary>
        /// Lock object for preventing multiple access during initialization.
        /// </summary>
        private static object initSyncLock = new object();

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        public static void Initialize()
        {
            if (!initialized)
            {
                lock (initSyncLock)
                {
                    if (!initialized)
                    {
                        container = new UnityContainer();
                        container.LoadConfiguration();
                        initialized = true;
                    }
                }
            }
        }

        /// <summary>
        /// Register an instance with the container.
        /// </summary>
        /// <typeparam name="TInterface">Type of instance to register (may be an implemented interface instead of the full type).</typeparam>
        /// <param name="name">Name for registration.</param>
        /// <param name="instance"> Object to returned.</param>
        public static void RegisterInstance<TInterface>(string name, TInterface instance)
        {
            IUnityContainer calledContainer = container.RegisterInstance<TInterface>(name, instance);
            Contract.Ensures(calledContainer == container);
        }

        /// <summary>
        /// Resolve an instance of the default requested type. 
        /// </summary>
        /// <typeparam name="T">Type to resolve.</typeparam>
        /// <returns>The retrieved object.</returns>
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        /// <summary>
        /// Return instances of all registered types requested.
        /// </summary>
        /// <typeparam name="T">The type requested.</typeparam>
        /// <returns>Set of objects of type T.</returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }

        /// <summary>
        /// Unregister a object instance.
        /// </summary>
        /// <typeparam name="TInterface">Type of the object to unregister.</typeparam>
        /// <param name="instance">Instance to unregister and clean up.</param>
        public static void UnRegisterInstance<TInterface>(TInterface instance)
        {
            container.Teardown(instance);
        }
    }
}
