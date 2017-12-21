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
        /// Locates the subject header and returns the start and end position in the string
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public (int, int) LocateSubjectHeader(string text)
        {
            return (text.IndexOf("Subject:"), text.IndexOf("Content-Type:"));
        }

        /// <summary>
        /// Parses the subject.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string ParseSubject(string text)
        {
            var header = LocateSubjectHeader(text);
            if (header.Item1 > -1 && header.Item2 > -1)
            {
                return text.Substring(header.Item1 + 9, header.Item2 - header.Item1 - 9).Replace(Environment.NewLine, String.Empty);
            }
            return String.Empty;
        }
    }
}
