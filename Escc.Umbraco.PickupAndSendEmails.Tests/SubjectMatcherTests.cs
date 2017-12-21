using NUnit.Framework;
using System;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    public class SubjectMatcherTests
    {
        [Test]
        public void ExactSubjectIsMatched()
        {
            var matcher = new SubjectMatcher("Umbraco: Reset Password");
            var email = new EmailModel() { Subject = "Umbraco: Reset Password" };

            var isMatch = matcher.IsMatch(email);

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void WrongSubjectIsNotMatched()
        {
            var matcher = new SubjectMatcher("Umbraco: Reset Password");
            var email = new EmailModel() { Subject = "And another thing..." };

            var isMatch = matcher.IsMatch(email);

            Assert.IsFalse(isMatch);
        }
    }
}
