// -----------------------------------------------------------------------
// <copyright file="Logger.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Logging
{
    using System;
    using NeotechSolutions.Foundation.Injection;

    /// <summary>
    /// Log events, exceptions, or messages.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Current Logger.
        /// </summary>
        private static ILogger logger = ServiceLocator.Resolve<ILogger>();

        /// <summary>
        /// Logs warning messages in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Warn(string category, IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.Warn(category, formatProvider, format, args);
        }

        /// <summary>
        /// Logs error messages in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Error(string category, IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.Error(category, formatProvider, format, args);
        }

        /// <summary>
        /// Logs error messages and exception in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Error(string category, string message, Exception exception)
        {
            logger.Error(category, message, exception);
        }

        /// <summary>
        /// Logs fatal error messages and exception in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Fatal(string category, string message, Exception exception)
        {
            logger.Fatal(category, message, exception);
        }
    }
}
