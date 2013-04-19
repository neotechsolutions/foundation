// -----------------------------------------------------------------------
// <copyright file="ConfigurationManagerTests.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Core_UnitTests.Configuration
{
    using System.Collections.Specialized;
    using System.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NeotechSolutions.Foundation.Core.Configuration;
    using NeotechSolutions.Foundation.Core.Injection;
    using TypeMock.ArrangeActAssert;
    using NSConfigurationManager = NeotechSolutions.Foundation.Core.Configuration.ConfigurationManager;

    /// <summary>
    /// Test class for <see cref="NeotechSolutions.Foundation.Configuration.ConfigurationManager"/>.
    /// </summary>
    [TestClass]
    public class ConfigurationManagerTests
    {
        /// <summary>
        /// A first fake configuration manager.
        /// </summary>
        private static IConfigurationManager manager1;

        /// <summary>
        /// A second fake configuration manager.
        /// </summary>
        private static IConfigurationManager manager2;

        /// <summary>
        /// Initializes the current test class.
        /// </summary>
        /// <param name="context">The test context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ServiceLocator.Initialize();
            manager1 = Isolate.Fake.Instance<IConfigurationManager>();
            NameValueCollection appSettings1 = new NameValueCollection();
            appSettings1.Add("Test1", "Value1");
            manager1.AppSettings = appSettings1;
            ServiceLocator.RegisterInstance<IConfigurationManager>("manager1", manager1);
            manager2 = Isolate.Fake.Instance<IConfigurationManager>();
            NameValueCollection appSettings2 = new NameValueCollection();
            appSettings2.Add("Test2", "Value2");
            manager2.AppSettings = appSettings2;
            ConnectionStringSettingsCollection connectionStrings2 = new ConnectionStringSettingsCollection();
            connectionStrings2.Add(new ConnectionStringSettings("ConnectionString", "ConnectionStringTest"));
            manager2.ConnectionStrings = connectionStrings2;
            ServiceLocator.RegisterInstance<IConfigurationManager>("manager2", manager2);
        }

        /// <summary>
        /// Test AppSettings
        /// </summary>
        [TestMethod]
        [TestCategory("Configuration")]
        public void ConfigurationManager_AppSettings_Test()
        {
            NameValueCollection appSettings = NSConfigurationManager.AppSettings;
            Isolate.Verify.WasCalledWithAnyArguments(() => manager1.AppSettings);
            Isolate.Verify.WasCalledWithAnyArguments(() => manager2.AppSettings);
            Assert.AreEqual<int>(2, appSettings.Count);

            string value1 = NSConfigurationManager.AppSettings["Test1"];
            string value2 = NSConfigurationManager.AppSettings["Test2"];

            Assert.AreEqual<string>("Value1", value1);
            Assert.AreEqual<string>("Value2", value2);
        }
    }
}
