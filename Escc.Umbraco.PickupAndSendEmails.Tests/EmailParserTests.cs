using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    class EmailParserTests
    {
        [Test]
        public void PasswordResetSubjectIsParsed()
        {
            var parser = new EmailParser();

            var result = parser.ParseEmail(Resources.PasswordResetEmail);

            Assert.AreEqual("Umbraco: Reset Password", result.Subject);
        }

        [Test]
        public void FormSubmittedSubjectIsParsed()
        {
            var parser = new EmailParser();

            var result = parser.ParseEmail(Resources.FormSubmittedEmail);

            Assert.AreEqual("The Form 'Test form' was submitted", result.Subject);
        }
    }
}
