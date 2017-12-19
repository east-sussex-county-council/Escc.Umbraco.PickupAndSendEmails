using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Determines whether an email is a match based on matching a regular expression
    /// </summary>
    /// <seealso cref="Escc.Umbraco.PickupAndSendEmails.IContentMatcher" />
    public class RegexContentMatcher : IContentMatcher
    {
        private readonly string _regexToMatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringContentMatcher"/> class.
        /// </summary>
        /// <param name="regexToMatch">The exact string to match.</param>
        public RegexContentMatcher(string regexToMatch)
        {
            this._regexToMatch = regexToMatch;
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
            return new Regex(_regexToMatch).IsMatch(content);
        }
    }
}
