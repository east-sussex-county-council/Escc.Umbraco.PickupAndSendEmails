using NUnit.Framework;
using System;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    public class RegexSubjectMatcherTests
    {
        [Test]
        public void FormSubmittedSubjectIsMatched()
        {
            var matcher = new RegexSubjectMatcher(new SubjectParser(), "The Form '(\n|\r|\r\n|.)*' was submitted");

            var isMatch = matcher.IsMatch(Resources.FormSubmittedEmail);

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void FormSubmittedMultilineSubjectIsMatched()
        {
            var matcher = new RegexSubjectMatcher(new SubjectParser(), "The Form '(\n|\r|\r\n|.)*' was submitted");

            var isMatch = matcher.IsMatch(Resources.FormSubmittedEmailMultilineSubject);

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void WrongSubjectIsNotMatched()
        {
            var matcher = new RegexSubjectMatcher(new SubjectParser(), "The Form '(\n|\r|\r\n|.)*' was submitted");

            var isMatch = matcher.IsMatch(Resources.PasswordResetEmail);

            Assert.IsFalse(isMatch);
        }
    }
}
