// -----------------------------------------------------------------------
// <copyright file="LoggerTests.cs" company="Neotech Solutions">
// Copyright (c) 2013, Neotech Solutions
// All rights reserved.
// See License.txt in the project root for license information.
// </copyright>
// -----------------------------------------------------------------------

namespace NeotechSolutions.Foundation.Core_UnitTests.Validation
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NeotechSolutions.Foundation.Core.Validation;

    /// <summary>
    /// Test class for Mail Validator.
    /// </summary>
    [TestClass]
    public class MailValidatorTests
    {
        /// <summary>
        /// Test method for Validate with a valide email.
        /// </summary>
        [TestMethod]
        public void MailValidator_Validate_ValidMail()
        {
            MailValidator validator = new MailValidator();
            bool valid = validator.Validate<string>("open-source@test-neotech.fr");
            Assert.IsTrue(valid);
        }

        /// <summary>
        /// Test method for Validate with a invalid email.
        /// </summary>
        [TestMethod]
        public void MailValidator_Validate_InvalidMail()
        {
            MailValidator validator = new MailValidator();
            bool valid = validator.Validate<string>("login");
            Assert.IsFalse(valid);
        }
    }
}
