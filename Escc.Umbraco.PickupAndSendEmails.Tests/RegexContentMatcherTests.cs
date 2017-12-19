using NUnit.Framework;
using System;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    public class RegexContentMatcherTests
    {
        [Test]
        public void FormSubmittedSubjectIsMatched()
        {
            var matcher = new RegexContentMatcher("Subject: The Form '.*' was submitted");

            var isMatch = matcher.IsMatch(Resources.FormSubmittedEmail);

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void WrongSubjectIsNotMatched()
        {
            var matcher = new RegexContentMatcher("Subject: The Form '.*' was submitted");

            var isMatch = matcher.IsMatch(Resources.PasswordResetEmail);

            Assert.IsFalse(isMatch);
        }
    }
}
