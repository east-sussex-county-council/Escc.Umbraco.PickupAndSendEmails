using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    class SubjectParserTests
    {
        [Test]
        public void PasswordResetSubjectIsParsed()
        {
            var parser = new SubjectParser();

            var result = parser.ParseSubject(Resources.PasswordResetEmail);

            Assert.AreEqual("Umbraco: Reset Password", result);
        }

        [Test]
        public void FormSubmittedSubjectIsParsed()
        {
            var parser = new SubjectParser();

            var result = parser.ParseSubject(Resources.FormSubmittedEmail);

            Assert.AreEqual("The Form 'Test form' was submitted", result);
        }

        [Test]
        public void FormSubmittedMultilineSubjectIsParsed()
        {
            var parser = new SubjectParser();

            var result = parser.ParseSubject(Resources.FormSubmittedEmailMultilineSubject);

            Assert.AreEqual("The Form 'Report an incident or issue to Trading Standards (version 1 / 2017-12-20-current)' was submitted", result);
        }
    }
}
