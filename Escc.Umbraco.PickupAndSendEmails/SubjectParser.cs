using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Parses the subject from a serialised email
    /// </summary>
    public class SubjectParser : ISubjectParser
    {
        /// <summary>
        /// Parses the subject.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string ParseSubject(string text)
        {
            text = text.Replace(Environment.NewLine, String.Empty);
            var subjectPos = text.IndexOf("Subject:");
            var contentTypePos = text.IndexOf("Content-Type:");
            if (subjectPos > -1 && contentTypePos > -1)
            {
                return text.Substring(subjectPos + 9, contentTypePos - subjectPos - 9);
            }
            return String.Empty;
        }
    }
}
