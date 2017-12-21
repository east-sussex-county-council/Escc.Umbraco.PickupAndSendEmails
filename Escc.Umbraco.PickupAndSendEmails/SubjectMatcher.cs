using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Determines whether an email is a match based on a case-insenstive match of its subject
    /// </summary>
    /// <seealso cref="Escc.Umbraco.PickupAndSendEmails.IEmailMatcher" />
    public class SubjectMatcher : IEmailMatcher
    {
        private readonly string _expectedSubject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectMatcher"/> class.
        /// </summary>
        /// <param name="expectedSubject">The exact string to match.</param>
        public SubjectMatcher(string expectedSubject)
        {
            this._expectedSubject = expectedSubject;
        }

        /// <summary>
        /// Determines whether the specified content matches the criteria of this <see cref="IEmailMatcher" />.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        ///   <c>true</c> if the specified content is a match; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsMatch(EmailModel email)
        {
            if (email == null || String.IsNullOrEmpty(email.Subject)) return false;
            return email.Subject.ToUpperInvariant() == _expectedSubject.ToUpperInvariant();
        }
    }
}
