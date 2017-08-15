using Escc.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;

namespace Escc.Umbraco.SelfServicePasswordReset
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(string.Format("<<<--Starting-->>>"));
            var Files = new Dictionary<string, string>();

            //Open directory
            string path = ConfigurationManager.AppSettings["EmailDirectory"];
            DirectoryInfo directory = new DirectoryInfo(path);
            Console.WriteLine(string.Format("<#> Looking at path: {0}", path));

            // For each file in the directory, record the file name and full path
            foreach (FileInfo file in directory.GetFiles())
            {
                Files.Add(file.Name, file.FullName);
                Console.WriteLine(string.Format("<!> Found the file: {0}", file.Name));
            }

            Console.WriteLine(string.Format("<O> No more files found. Moving on to file processing."));
            Console.WriteLine(string.Format(""));
            Console.WriteLine(string.Format("<<<--Processing Files-->>>"));
            var EmailsToSend = ProcessFiles(Files);

            Console.WriteLine(string.Format("<O> No More Files To Process. Moving on to Send Procedure...."));
            Console.WriteLine(string.Format(""));
            Console.WriteLine(string.Format("<<<--Sending Emails-->>>"));
            SendEmails(EmailsToSend);

            Console.WriteLine(string.Format("<O> No More Emails To Process."));
            Console.WriteLine(string.Format("<<<--Finished-->>>"));

            Console.ReadLine();
        }

        private static void SendEmails(List<EmailModel> EmailsToSend)
        {
            // For each Email to send, process into a MailMessage Object and send using Escc.Services
            foreach (var Email in EmailsToSend)
            {
                Console.WriteLine(string.Format("<#> Processing password reset email to: {0}", Email.To));
                var Mail = new MailMessage(Email.From, Email.To, Email.Subject, Email.Body);
                Mail.IsBodyHtml = true;
                Mail.BodyEncoding = System.Text.Encoding.UTF8;

                var emailService = ServiceContainer.LoadService<IEmailSender>(new ConfigurationServiceRegistry(), null);
                emailService.SendAsync(Mail);
                Console.WriteLine(string.Format("<!> Email Sent to: {0}", Email.To));
            }
        }

        private static List<EmailModel> ProcessFiles(Dictionary<string, string> Files)
        {
            var EmailsToSend = new List<EmailModel>();
            //Look for files that end in .eml
            foreach (var file in Files)
            {
                Console.WriteLine(string.Format("<#> Looking at file: {0}", file.Key));
                if (file.Key.Contains(".eml"))
                {
                    Console.WriteLine(string.Format("<!> File {0} is an .eml file.", file.Key));
                    var FileText = File.ReadAllText(file.Value);
                    // if the file contains the text "subject something something password reset" etc
                    if (FileText.Contains("Subject: Umbraco: Reset Password"))
                    {
                        Console.WriteLine(string.Format("<!> File {0} contains password reset subject.", file.Key));
                        var Email = new EmailModel();
                        var FileLines = FileText.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var line in FileLines)
                        {
                            if (line.Contains("From:"))
                            {
                                Email.From = line.Replace("From:", "").Replace("\r", "");
                            }
                            else if (line.Contains("To:"))
                            {
                                Email.To = line.Replace("To:", "").Replace("\r", "");
                            }
                            else if (line.Contains("Subject:"))
                            {
                                Email.Subject = line.Replace("Subject:", "").Replace("\r", "");
                            }
                            else if (line.Contains("Umbraco: Reset Password"))
                            {
                                Email.Subject += line.Replace("\r", "");
                            }
                            else if (line.Contains("Date:"))
                            {
                                Email.Sent = DateTime.Parse(line.Replace("Date:", ""));
                            }
                            else if (line.Contains("X-Sender:"))
                            {
                                Email.XSender = line.Replace("X-Sender:", "").Replace("\r", "");
                            }
                            else if (line.Contains("X-Receiver:"))
                            {
                                Email.XReceiver = line.Replace("X-Receiver:", "").Replace("\r", "");
                            }
                            else if (line.Contains("MIME-Version:"))
                            {
                                Email.MimeVersion = line.Replace("MIME-Version:", "").Replace("\r", "");
                            }
                            else if (line.Contains("Content-Type:"))
                            {
                                Email.ContentType = line.Replace("Content-Type:", "").Replace("\r", "");
                            }
                            else if (line.Contains("Content-Transfer-Encoding:"))
                            {
                                Email.ContentTransferEncoding = line.Replace("Content-Transfer-Encoding:", "").Replace("\r", "");
                            }
                            else
                            {
                                Email.Body += line.Replace("=\r","").Replace("=3D","=");
                            }
                        }
                        EmailsToSend.Add(Email);
                        Console.WriteLine(string.Format("<!> File {0} processed and added to EmailsToSend list.", file.Key));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("<X> File {0} does not contain password reset subject.", file.Key));
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("<X> File {0} is not an .eml file.", file.Key));
                }
               
            }
            return EmailsToSend;
        }






    }
}
