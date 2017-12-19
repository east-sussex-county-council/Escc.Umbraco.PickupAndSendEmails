using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Parse an email and turn it into an <see cref="EmailModel"/>
    /// </summary>
    interface IEmailParser
    {
        /// <summary>
        /// Parses the email.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        EmailModel ParseEmail(string content);
    }
}
