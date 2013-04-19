// -----------------------------------------------------------------------
// <copyright file="IValidator.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Core.Validation
{
    /// <summary>
    /// Defines contract for validate a value.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns><c>True</c> if the value is valid, <c>False</c> otherwise.</returns>
        bool Validate<T>(T value);
    }
}
