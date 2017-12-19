using NUnit.Framework;
using System;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    public class StringContentMatcherTests
    {
        [Test]
        public void ExactSubjectIsMatched()
        {
            var matcher = new StringContentMatcher("Umbraco: Reset Password");

            var isMatch = matcher.IsMatch(Resources.PasswordResetEmail);

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void WrongSubjectIsNotMatched()
        {
            var matcher = new StringContentMatcher("Umbraco: Reset Password");

            var isMatch = matcher.IsMatch(Resources.FormSubmittedEmail);

            Assert.IsFalse(isMatch);
        }
    }
}
