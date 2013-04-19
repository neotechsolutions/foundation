// -----------------------------------------------------------------------
// <copyright file="LoggerTests.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Core_UnitTests.Logging
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NeotechSolutions.Foundation.Core.Injection;
    using NeotechSolutions.Foundation.Core.Logging;
    using TypeMock.ArrangeActAssert;

    /// <summary>
    /// Test class for <see cref="NeotechSolutions.Foundation.Logging.Logger"/>.
    /// </summary>
    [TestClass]
    public class LoggerTests
    {
        /// <summary>
        /// Log category.
        /// </summary>
        private const string LogCategory = "TestLog";

        /// <summary>
        /// Log message format.
        /// </summary>
        private const string MessageFormat = LogCategory + "_{0}";

        /// <summary>
        /// Private logger.
        /// </summary>
        private static FieldInfo loggerField = typeof(Logger).GetField("logger", BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Initializes the service locator.
        /// </summary>
        /// <param name="context">The test context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ServiceLocator.Initialize();
        }

        /// <summary>
        /// Initializes the fake logger.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            ILogger fakeLogger = Isolate.Fake.Instance<ILogger>();
            ServiceLocator.RegisterInstance<ILogger>(string.Empty, fakeLogger);
            loggerField.SetValue(null, fakeLogger);
        }

        /// <summary>
        /// Removes the fake logger.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            ILogger fakeLogger = ServiceLocator.Resolve<ILogger>();
            ServiceLocator.UnRegisterInstance<ILogger>(fakeLogger);
            loggerField.SetValue(null, null);
        }

        /// <summary>
        /// Test for the Warn method.
        /// </summary>
        [TestMethod]
        public void Logger_Warn_Test()
        {
            ILogger fakeLogger = loggerField.GetValue(null) as ILogger;
            CultureInfo culture = CultureInfo.InvariantCulture;
            Logger.Warn(LogCategory, culture, MessageFormat, "Logger_Warn_Test");
            Isolate.Verify.WasCalledWithExactArguments(() => fakeLogger.Warn(LogCategory, culture, MessageFormat, "Logger_Warn_Test"));
        }

        /// <summary>
        /// Test for the Error method with a message.
        /// </summary>
        [TestMethod]
        public void Logger_Error_WithMessage_Test()
        {
            ILogger fakeLogger = loggerField.GetValue(null) as ILogger;
            CultureInfo culture = CultureInfo.InvariantCulture;
            Logger.Error(LogCategory, culture, MessageFormat, "Logger_Error_WithMessage_Test");
            Isolate.Verify.WasCalledWithExactArguments(() => fakeLogger.Error(LogCategory, culture, MessageFormat, "Logger_Error_WithMessage_Test"));
        }

        /// <summary>
        /// Test for the Error method with an exception.
        /// </summary>
        [TestMethod]
        public void Logger_Error_WithException_Test()
        {
            ILogger fakeLogger = loggerField.GetValue(null) as ILogger;
            string message = "Logger_Error_WithException_Test";
            Exception exception = new Exception(message);
            Logger.Error(LogCategory, message, exception);
            Isolate.Verify.WasCalledWithExactArguments(() => fakeLogger.Error(LogCategory, message, exception));
        }

        /// <summary>
        /// Test for the Fatal method with an exception.
        /// </summary>
        [TestMethod]
        public void Logger_Fatal_WithException_Test()
        {
            ILogger fakeLogger = loggerField.GetValue(null) as ILogger;
            string message = "Logger_Fatal_WithException_Test";
            Exception exception = new Exception(message);
            Logger.Fatal(LogCategory, message, exception);
            Isolate.Verify.WasCalledWithExactArguments(() => fakeLogger.Fatal(LogCategory, message, exception));
        }
    }
}
