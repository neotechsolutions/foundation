// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Logging
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Base logger.
    /// </summary>
    [ContractClass(typeof(ILoggerContract))]
    public interface ILogger
    {
        /// <summary>
        /// Logs warning messages in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Warn(string category, IFormatProvider formatProvider, string format, params object[] args);

        /// <summary>
        /// Logs error messages in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Error(string category, IFormatProvider formatProvider, string format, params object[] args);

        /// <summary>
        /// Logs error messages and exception in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(string category, string message, Exception exception);

        /// <summary>
        /// Logs fatal error messages and exception in the specified logging category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Fatal(string category, string message, Exception exception);
    }
}
