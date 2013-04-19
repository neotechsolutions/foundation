// -----------------------------------------------------------------------
// <copyright file="MailValidator.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Core.Validation
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Validates that input value is an email.
    /// </summary>
    public class MailValidator : IValidator
    {
        /// <summary>
        /// Mail Pattern.
        /// </summary>
        private const string ValidEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                        + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                        + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        /// <summary>
        /// Regex that validates that input is a mail.
        /// </summary>
        private static Regex mailRegex = new Regex(ValidEmailPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

        /// <inheritdoc />
        public bool Validate<T>(T value)
        {
            string valueAsString = value as string;
            bool result = false;
            if (!string.IsNullOrWhiteSpace(valueAsString))
            {
                Match match = mailRegex.Match(valueAsString);
                if (match != null)
                {
                    result = match.Success;
                }
            }

            return result;
        }
    }
}
