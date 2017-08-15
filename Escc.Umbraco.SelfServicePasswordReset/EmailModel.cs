using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.SelfServicePasswordReset
{
    class EmailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public DateTime Sent { get; set; }
        public string Body { get; set; }
        public string XSender { get; set; }
        public string XReceiver { get; set; }
        public string MimeVersion { get; set; }
        public string ContentType { get; set; }
        public string ContentTransferEncoding { get; set; }

        public string PathToFile { get; set; }

        public EmailModel(string pathToFile)
        {
            PathToFile = pathToFile;
        }
    }
}
