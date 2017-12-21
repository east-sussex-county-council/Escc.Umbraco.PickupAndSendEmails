﻿using NUnit.Framework;
using System;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    public class RegexSubjectMatcherTests
    {
        [Test]
        public void FormSubmittedSubjectIsMatched()
        {
            var matcher = new RegexSubjectMatcher("The Form '(\n|\r|\r\n|.)*' was submitted");
            var email = new EmailModel() { Subject = "The Form 'My test form' was submitted" };

            var isMatch = matcher.IsMatch(email);

            Assert.IsTrue(isMatch);
        }

        [Test]
        public void WrongSubjectIsNotMatched()
        {
            var matcher = new RegexSubjectMatcher("The Form '(\n|\r|\r\n|.)*' was submitted");
            var email = new EmailModel() { Subject = "And another thing..." };

            var isMatch = matcher.IsMatch(email);

            Assert.IsFalse(isMatch);
        }
    }
}