using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Bases;

namespace Upgrade.Core.DomainModels
{
    /// <summary>
    /// 软件列表
    /// </summary>
    public class Software : EntityBase
    {
        /// <summary>
        /// 软件编码
        /// </summary>
        public string SNumber { get; set; }

        /// <summary>
        /// 软件名称
        /// </summary>
        public string SName { get; set; }

        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FilePathName { get; set; }
    }
}
