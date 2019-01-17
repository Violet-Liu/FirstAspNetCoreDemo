using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.Options
{
    public class EmailOptions
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string MailFrom { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string MailToArray { get; set; }
        /// <summary>
        /// 抄送
        /// </summary>
        public string MailCcArray { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string MailPwd { get; set; }

        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool IsbodyHtml { get; set; }

    }
}
