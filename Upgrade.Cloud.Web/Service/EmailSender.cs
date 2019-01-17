using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Upgrade.Cloud.Web.Options;
using System.Linq;
using Microsoft.Extensions.Logging;
namespace Upgrade.Cloud.Web.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailOptions> _options;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailOptions> options,ILogger<EmailSender> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task SendEmailAsync(string subject, string message)
        {
            await Task.Run(() => Send(subject, message));
        }

        public void Send(string mailSubject, string mailBody, string[] attachmentsPath = null)
        {
            if (string.IsNullOrEmpty(_options?.Value.Host))
            {
                _logger.LogError(new SmtpException("setting's Host can't be null or empty"),"Send Mail error");
                return;
            }

            if (string.IsNullOrEmpty(_options?.Value.MailFrom))
            {
                _logger.LogError(new SmtpException("setting's MailFrom can't be null or empty"), "Send Mail error");
                return;
            }

            if (string.IsNullOrEmpty(_options?.Value.MailPwd))
            {
                _logger.LogError(new SmtpException("setting's MailPwd can't be null or empty"), "Send Mail error");
                return;
            }


            var maddr = new MailAddress(_options?.Value.MailFrom);
            var myMail = new MailMessage();
            _options?.Value.MailToArray?.Split(',').ToList().ForEach(x => myMail.To.Add(x));
            _options?.Value.MailCcArray?.Split(',').ToList().ForEach(x => myMail.CC.Add(x));

            myMail.From = maddr;
            myMail.Subject = mailSubject;
            myMail.SubjectEncoding = Encoding.UTF8;
            myMail.Body = mailBody;
            myMail.BodyEncoding = Encoding.Default;
            myMail.Priority = MailPriority.High;
            myMail.IsBodyHtml = _options?.Value.IsbodyHtml ?? true;

            attachmentsPath?.ToList().ForEach(x => myMail.Attachments.Add(new Attachment(x)));

            var smtp = new SmtpClient();
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //QQ邮箱为例，以上两行不能省略，在新建smtp.Credentials对象时，上面两行先要赋值，或者发送会执行异常
            smtp.Credentials = new System.Net.NetworkCredential(_options?.Value.MailFrom, _options?.Value.MailPwd);
            smtp.Host = _options?.Value.Host;

            try
            {
                smtp.Send(myMail);
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "Send Email error");
            }
        }


    }
}
