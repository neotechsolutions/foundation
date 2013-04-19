// -----------------------------------------------------------------------
// <copyright file="ILoggerContract.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Core.Logging
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Contract class for <see cref="ILogger" />.
    /// </summary>
    [ContractClassFor(typeof(ILogger))]
    internal abstract class ILoggerContract : ILogger
    {
        /// <inheritdoc />
        public virtual void Warn(string category, IFormatProvider formatProvider, string format, params object[] args)
        {
            Contract.Requires<ArgumentNullException>(format != null, "format");
            Contract.Requires<ArgumentNullException>(formatProvider != null, "formatProvider");
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual void Error(string category, IFormatProvider formatProvider, string format, params object[] args)
        {
            Contract.Requires<ArgumentNullException>(format != null, "format");
            Contract.Requires<ArgumentNullException>(formatProvider != null, "formatProvider");
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual void Error(string category, string message, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(message != null, "message");
            Contract.Requires<ArgumentNullException>(exception != null, "exception");
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual void Fatal(string category, string message, Exception exception)
        {
            Contract.Requires<ArgumentNullException>(message != null, "message");
            Contract.Requires<ArgumentNullException>(exception != null, "exception");
            throw new NotImplementedException();
        }
    }
}
