using Escc.Services;
using Exceptionless;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;

namespace Escc.Umbraco.PickupAndSendEmails
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main()
        {
            ExceptionlessClient.Default.Startup();
            XmlConfigurator.Configure();

            try
            {
                var Files = new Dictionary<string, string>();

                //Open directory
                string path = ConfigurationManager.AppSettings["EmailDirectory"];
                DirectoryInfo directory = new DirectoryInfo(path);
                log.Info(string.Format("Looking at path: {0}", path));

                // For each file in the directory, record the file name and full path
                foreach (FileInfo file in directory.GetFiles())
                {
                    Files.Add(file.Name, file.FullName);
                    log.Info(string.Format("Found the file: {0}", file.Name));
                }

                log.Info(string.Format("No more files found. Moving on to file processing."));
                var EmailsToSend = ProcessFiles(Files);

                log.Info(string.Format("No more files to process. Moving on to sending emails..."));
                SendEmails(EmailsToSend);

                log.Info(string.Format("No more emails to process."));
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                log.Error(ex.Message);
            }
        }

        private static void SendEmails(List<EmailModel> EmailsToSend)
        {
            // For each Email to send, process into a MailMessage Object and send using Escc.Services
            foreach (var Email in EmailsToSend)
            {
                log.Info(string.Format("Sending email to: {0}", Email.To));
                var Mail = new MailMessage(Email.From, Email.To, Email.Subject, Email.Body);
                Mail.IsBodyHtml = true;
                Mail.BodyEncoding = System.Text.Encoding.UTF8;

                var emailService = ServiceContainer.LoadService<IEmailSender>(new ConfigurationServiceRegistry(), null);
                emailService.SendAsync(Mail);

                log.Info(string.Format("Deleting .eml file at \"{0}\"", Email.PathToFile));
                File.Delete(Email.PathToFile); 
            }
        }

        private static List<EmailModel> ProcessFiles(Dictionary<string, string> Files)
        {
            var EmailsToSend = new List<EmailModel>();
            var subjectMatchers = new IEmailMatcher[] {
                new SubjectMatcher("Umbraco: Reset Password"),
                new RegexSubjectMatcher("^The Form '(\n|\r|\r\n|.)*' was submitted$")
            };

            var emailParser = new EmailParser(new SubjectParser());
            //Look for files that end in .eml
            foreach (var file in Files)
            {
                log.Info(string.Format("Looking at file: {0}", file.Key));
                if (file.Key.Contains(".eml"))
                {
                    log.Info(string.Format("File {0} is an .eml file.", file.Key));
                    var email = emailParser.ParseEmail(File.ReadAllText(file.Value));
                    email.PathToFile = string.Format("{0}\\{1}", ConfigurationManager.AppSettings["EmailDirectory"], file.Key);

                    var isMatch = false;
                    foreach (var matcher in subjectMatchers) isMatch = isMatch || matcher.IsMatch(email);

                    if (isMatch)
                    {
                        log.Info(string.Format("File {0} contains recognised subject: {1}", file.Key, email.Subject));
                        EmailsToSend.Add(email);
                    }
                    else
                    {
                        log.Info(string.Format("File {0} does not contain a recognised subject: {1}", file.Key, email.Subject));
                    }
                }
                else
                {
                    log.Info(string.Format("File {0} is not an .eml file.", file.Key));
                }
               
            }
            return EmailsToSend;
        }






    }
}
