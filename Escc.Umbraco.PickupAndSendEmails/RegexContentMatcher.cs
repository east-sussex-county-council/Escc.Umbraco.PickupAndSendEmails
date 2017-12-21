﻿using System;
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
    /// <seealso cref="Escc.Umbraco.PickupAndSendEmails.IEmailMatcher" />
    public class RegexSubjectMatcher : IEmailMatcher
    {
        private readonly string _regexToMatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectMatcher" /> class.
        /// </summary>
        /// <param name="regexToMatch">The pattern to match.</param>
        public RegexSubjectMatcher(string regexToMatch)
        {
            this._regexToMatch = regexToMatch;
        }

        /// <summary>
        /// Determines whether the specified content matches the criteria of this <see cref="IEmailMatcher" />.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is a match; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsMatch(EmailModel email)
        {
            if (email ==null || String.IsNullOrEmpty(email.Subject)) return false;
            return new Regex(_regexToMatch).IsMatch(email.Subject);
        }
    }
}
