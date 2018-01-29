using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Parses an email from the text of an encoded file
    /// </summary>
    /// <seealso cref="Escc.Umbraco.PickupAndSendEmails.IEmailParser" />
    public class EmailParser : IEmailParser
    {
        private readonly ISubjectParser _subjectParser;

        public EmailParser(ISubjectParser subjectParser)
        {
            if (subjectParser == null) throw new ArgumentNullException(nameof(subjectParser));
            this._subjectParser = subjectParser;
        }

        /// <summary>
        /// Parses the email.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public EmailModel ParseEmail(string content)
        {
            var email = new EmailModel();

            // Parse the subject, then remove it from the source so that it's not included in the body
            email.Subject = _subjectParser.ParseSubject(content);
            var subjectPostion = _subjectParser.LocateSubjectHeader(content);
            content = content.Substring(0, subjectPostion[0]) + content.Substring(subjectPostion[1]); 

            var fileLines = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in fileLines)
            {
                if (line.Contains("From:"))
                {
                    email.From = line.Replace("From:", "").Replace("\r", "");
                }
                else if (line.Contains("To:"))
                {
                    email.To = line.Replace("To:", "").Replace("\r", "");
                }
                else if (line.Contains("Date:"))
                {
                    email.Sent = DateTime.Parse(line.Replace("Date:", ""));
                }
                else if (line.Contains("X-Sender:"))
                {
                    email.XSender = line.Replace("X-Sender:", "").Replace("\r", "");
                }
                else if (line.Contains("X-Receiver:"))
                {
                    email.XReceiver = line.Replace("X-Receiver:", "").Replace("\r", "");
                }
                else if (line.Contains("MIME-Version:"))
                {
                    email.MimeVersion = line.Replace("MIME-Version:", "").Replace("\r", "");
                }
                else if (line.Contains("Content-Type:"))
                {
                    email.ContentType = line.Replace("Content-Type:", "").Replace("\r", "");
                }
                else if (line.Contains("Content-Transfer-Encoding:"))
                {
                    email.ContentTransferEncoding = line.Replace("Content-Transfer-Encoding:", "").Replace("\r", "");
                }
                else
                {
                    email.Body += line.Replace("=\r", "").Replace("=3D", "=").Replace("=0D=0A", Environment.NewLine).Replace("=0A", Environment.NewLine);
                }
            }

            return email;
        }
    }
}
