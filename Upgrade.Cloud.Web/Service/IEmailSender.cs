using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string subject, string message);
    }
}
