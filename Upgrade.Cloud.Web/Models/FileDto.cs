using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.Models
{
    public class FileDto
    {
        /// <summary>
        /// 存储空间
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// 存储空间的key值
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径-在客户端生成对应云端的相对于该项目的文件路径
        /// </summary>
        public string FilePath { get; set; }

        public int UpgradeItemId { get; set; }

        public DateTime CreateTime { get; set; }
        public string Creater { get; set; }
    }
}
