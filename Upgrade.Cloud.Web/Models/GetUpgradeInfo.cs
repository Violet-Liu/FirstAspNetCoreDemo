using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Upgrade.Core.DomainModels;
using Mall.Common.Extension;

namespace Upgrade.Cloud.Web.Models
{
    public class GetUpgradeInfo
    {
        public string Code { get; set; } = nameof(UpgradeEnum.Failed);

        public List<FileDto> data { get; set; }

        public string Msg
        {
            get => Code.GetDescriptionByName(typeof(UpgradeEnum));
        }
    }

    public enum UpgradeEnum
    {
        [Description("暂无升级内容")]
        None,
        [Description("获取升级成功")]
        Success,
        [Description("未找到升级文件")]
        FilesNotFound,
        [Description("访问失败")]
        Failed,
        [Description("此前已成功升级")]
        Yet
    }
}
