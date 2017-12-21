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
        public int[] LocateSubjectHeader(string text)
        {
            return new int[] { text.IndexOf("Subject:"), text.IndexOf("Content-Type:") };
        }

        /// <summary>
        /// Parses the subject.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string ParseSubject(string text)
        {
            var header = LocateSubjectHeader(text);
            if (header[0] > -1 && header[1] > -1)
            {
                return text.Substring(header[0] + 9, header[1] - header[0] - 9).Replace(Environment.NewLine, String.Empty);
            }
            return String.Empty;
        }
    }
}
