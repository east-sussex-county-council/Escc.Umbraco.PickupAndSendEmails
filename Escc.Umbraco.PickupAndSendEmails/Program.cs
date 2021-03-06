﻿using Escc.Services;
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
            Exception firstException = null;

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

                // continue to process the next email, but store the exception to throw later
                if (firstException == null) firstException = ex;
            }

            // Rethrow the error if there was one, so that if this is run as an Azure Webjob it will correctly report success or failure
            if (firstException != null) throw firstException;
        }

        private static void SendEmails(List<EmailModel> EmailsToSend)
        {
            // For each Email to send, process into a MailMessage Object and send using Escc.Services
            foreach (var Email in EmailsToSend)
            {
                log.Info($"Sending email {Email.Subject} to: {Email.To}");
                var mail = new MailMessage();
                mail.From = new MailAddress(Email.From);
                foreach (var address in Email.To)
                {
                    mail.To.Add(address);
                }
                mail.Subject = Email.Subject;
                mail.Body = Email.Body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                var emailService = ServiceContainer.LoadService<IEmailSender>(new ConfigurationServiceRegistry(), null);
                emailService.SendAsync(mail);

                log.Info(string.Format("Deleting .eml file at \"{0}\"", Email.PathToFile));
                File.Delete(Email.PathToFile); 
            }
        }

        private static List<EmailModel> ProcessFiles(Dictionary<string, string> Files)
        {
            var EmailsToSend = new List<EmailModel>();

            var emailParser = new EmailParser(new SubjectParser());
            //Look for files that end in .eml
            foreach (var file in Files)
            {
                try
                {
                    log.Info(string.Format("Looking at file: {0}", file.Key));
                    if (file.Key.Contains(".eml"))
                    {
                        log.Info(string.Format("File {0} is an .eml file.", file.Key));
                        var email = emailParser.ParseEmail(File.ReadAllText(file.Value));
                        email.PathToFile = string.Format("{0}\\{1}", ConfigurationManager.AppSettings["EmailDirectory"], file.Key);
                        EmailsToSend.Add(email);
                    }
                    else
                    {
                        log.Info(string.Format("File {0} is not an .eml file.", file.Key));
                    }
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().Submit();
                    log.Error(ex.Message);
                }

            }
            return EmailsToSend;
        }






    }
}
