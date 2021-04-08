using BusinessService.Business;
using BusinessService.Models;
using CoreLibrary.Data;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using Database.Model;
using log4net;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BusinessService.Utility
{
    public class EmailHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string AppPath
        {
            get
            {
                try
                {
                    string path = HttpContext.Current.Request.ApplicationPath;
                    return path.EndsWith("/") ? path : path + "/";
                }
                catch
                {
                    return "???";
                }
            }
        }

        private static string AppPathUrl
        {
            get
            {
                try
                {
                    string host = HttpContext.Current.Request.Url.Scheme + "://" +
                                  (HttpContext.Current.Request.Url.IsDefaultPort ? HttpContext.Current.Request.Url.Host : HttpContext.Current.Request.Url.Authority);
                    return host + AppPath;
                }
                catch
                {
                    return "???" + AppPath;
                }
            }
        }

        private static string FixPath(string path)
        {
            if (path.StartsWith("~/"))
            {
                return AppPath + path.Substring(2);
            }

            if (path.StartsWith("/"))
            {
                return AppPath + path.Substring(1);
            }
            else
            {
                return path;
            }
        }

        private static string GetEmailTemplate(string type)
        {
            WebRequest request = null;

            if (type.Equals(Constant.EmailType.ConfirmationEmail))
            {
                request = WebRequest.Create(Constant.EmailType.ConfirmationEmailTemplate);
            }

            if (type.Equals(Constant.EmailType.RecoveryPasswordEmail))
            {
                request = WebRequest.Create(Constant.EmailType.RecoveryPasswordEmailTemplate);
            }

            if (type.Equals(Constant.EmailType.RegisterAccountEmail))
            {
                request = WebRequest.Create(Constant.EmailType.RegistrationEmailTemplate);
            }

            if (type.Equals(Constant.EmailType.SystemErrorEmail))
            {
                request = WebRequest.Create(Constant.EmailType.SystemErrorEmailTemplate);
            }

            if (type.Equals(Constant.EmailType.SystemTestEmail))
            {
                request = WebRequest.Create(Constant.EmailType.SystemTestEmailTemplate);
            }

            if (type.Equals(Constant.EmailType.NewsletterEmail))
            {
                request = WebRequest.Create(Constant.EmailType.NewsletterEmailTemplate);
            }

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string template = reader.ReadToEnd();

            template = Regex.Replace(template, @"( (?:href|src)=[""'])~/?(\S*?[""'])", "$1" + AppPathUrl + "$2");

            return template;
        }
        
        public static bool SendSystemErrorEmail(string errorMessage, string exception)
        {
            string body = GetEmailTemplate(Constant.EmailType.SystemErrorEmail);
            string senderEmail = ConfigurationManager.AppSettings["EmailUsername"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            if (!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(senderEmail))
            {
                body = body.Replace("##Error_Message##", errorMessage);
                body = body.Replace("##Exception##", exception);
                body = body.Replace("##Date_Time##", DateTime.UtcNow.ToString(CultureInfo.GetCultureInfo("en-AU")));

                bool res = SendEmailToTeam(senderEmail, body, password, "System Error");

                return res;
            }

            return false;
        }

        public static bool SendSystemTestEmail()
        {
            string body = GetEmailTemplate(Constant.EmailType.SystemTestEmail);
            string senderEmail = ConfigurationManager.AppSettings["EmailUsername"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            if (!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(senderEmail))
            {
                body = body.Replace("##Date_Time##", DateTime.UtcNow.ToString(CultureInfo.GetCultureInfo("en-AU")));

                bool res = SendEmailToTeam(senderEmail, body, password, "System Test");

                return res;
            }

            return false;
        }

        public static bool SendEmailToTeam(string senderEmail, string body, string password, string header)
        {
            try
            {
                log.Info($"Sending error email in Azure by SendGrid. To: {Constant.CommonValue.TechnicalEmail} / Header: {header}");

                string apiKey = ConfigurationManager.AppSettings["SendGridAPIKey"];
                SendGridClient client = new SendGridClient(apiKey);

                EmailAddress from = new EmailAddress(senderEmail, "House Home Love");
                string subject = $"HouseHomeLove - {header}";
                EmailAddress to = new EmailAddress(Constant.CommonValue.TechnicalEmail, "Technical Team");

                SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, null, body);

                Response response = client.SendEmailAsync(msg).Result;

                log.Info($"Sending email to {Constant.CommonValue.TechnicalEmail} {header} Status: {response.StatusCode}");

                return true;
            }
            catch (Exception ex)
            {
                log.Debug($"Cannot send email from system: {ex.Message}");
                throw new DataLayerException(ex.Message);
            }
        }

        public static bool SendNewsletterEmail(string content, string header, string toEmail)
        {
            string body = GetEmailTemplate(Constant.EmailType.NewsletterEmail);
            string senderEmail = ConfigurationManager.AppSettings["EmailUsername"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            if (!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(senderEmail))
            {
                body = body.Replace("##Content##", content);

                bool res = SendEmailToCustomer(senderEmail, body, password, header, toEmail);

                return res;
            }

            return false;
        }

        public static bool SendEmailToCustomer(string senderEmail, string body, string password, string header, string toEmail)
        {
            try
            {
                log.Info($"Sending email to customer by SendGrid. To: {toEmail} / Header: {header}");

                string apiKey = ConfigurationManager.AppSettings["SendGridAPIKey"];
                SendGridClient client = new SendGridClient(apiKey);

                EmailAddress from = new EmailAddress(senderEmail, "House Home Love");
                string subject = $"HouseHomeLove - {header}";
                EmailAddress to = new EmailAddress(toEmail);

                SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, null, body);

                Response response = client.SendEmailAsync(msg).Result;

                log.Info($"Sending email to customer: {toEmail} {header} Status: {response.StatusCode}");

                return true;
            }
            catch (Exception ex)
            {
                log.Debug($"Cannot send email from system: {ex.Message}");
                throw new DataLayerException(ex.Message);
            }
        }
    }
}
