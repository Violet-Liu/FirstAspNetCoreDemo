using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Bases;
using Upgrade.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Upgrade.Core.DomainModels
{
    /// <summary>
    /// 更新项-由云端发起的单次更新，更新项目包含需要客户端下载的所有项目更新文件
    /// </summary>
    public class UpgradeItem : EntityBase
    { 

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 更新内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 对应客户端更新项
        /// </summary>

        public virtual ICollection<ClientUpgradeItem> ClientUpgradeItems { get; set; }

        /// <summary>
        /// 对应更新文件列表
        /// </summary>
        public virtual ICollection<UpgradeFiles> UpgradeFiles { get; set; }

        /// <summary>
        /// 允许更新的停车场ID
        /// </summary>
        public virtual ICollection<ClientSet> ClientSets { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creater { get; set; }
    }
}
