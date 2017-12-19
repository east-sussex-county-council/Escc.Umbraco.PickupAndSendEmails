using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Determines whether an email is a match based on the presence of a given string
    /// </summary>
    /// <seealso cref="Escc.Umbraco.PickupAndSendEmails.IContentMatcher" />
    public class StringContentMatcher : IContentMatcher
    {
        private readonly string _stringToMatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringContentMatcher"/> class.
        /// </summary>
        /// <param name="stringToMatch">The exact string to match.</param>
        public StringContentMatcher(string stringToMatch)
        {
            this._stringToMatch = stringToMatch;
        }

        /// <summary>
        /// Determines whether the specified content matches the criteria of this <see cref="IContentMatcher" />.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is a match; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsMatch(string content)
        {
            if (String.IsNullOrEmpty(content)) return false;
            return content.Contains(_stringToMatch);
        }
    }
}
