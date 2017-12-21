using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Determines whether an email matches criteria 
    /// </summary>
    interface IEmailMatcher
    {
        /// <summary>
        /// Determines whether the specified content matches the criteria of this <see cref="IEmailMatcher"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is a match; otherwise, <c>false</c>.
        /// </returns>
        bool IsMatch(EmailModel email);
    }
}
