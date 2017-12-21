using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    [TestFixture]
    public class EmailParserTests
    {
        [Test]
        public void QuotedPrintableNewlinesAreDecoded()
        {
            var parser = new EmailParser(new FakeSubjectParser());

            var email = parser.ParseEmail(Resources.FormSubmittedEmail);

            Assert.IsFalse(email.Body.Contains("=0D=0A"));
        }
    }
}
