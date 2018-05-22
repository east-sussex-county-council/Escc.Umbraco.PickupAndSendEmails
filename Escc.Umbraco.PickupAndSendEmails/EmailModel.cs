using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.PickupAndSendEmails
{
    public class EmailModel
    {
        public string From { get; set; }
        public IList<string> To { get; set; } = new List<string>();
        public string Subject { get; set; }
        public DateTime Sent { get; set; }
        public string Body { get; set; }
        public string MimeVersion { get; set; }
        public string ContentType { get; set; }
        public string ContentTransferEncoding { get; set; }

        public string PathToFile { get; set; }
    }
}
