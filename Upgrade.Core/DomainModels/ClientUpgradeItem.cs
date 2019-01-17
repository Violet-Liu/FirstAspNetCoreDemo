using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Bases;
using Upgrade.Core.Interfaces;

namespace Upgrade.Core.DomainModels
{
    public class ClientUpgradeItem : EntityBase
    {
        /// <summary>
        /// 停车场标识
        /// </summary>
        public string ParkId { get; set; }

        /// <summary>
        /// 是否更新成功
        /// </summary>
        public bool IsUpgradeSucess { get; set; }

        public string UpgradeRespMsg { get; set; }


        public int UpgradeItemId { get; set; }
        /// <summary>
        /// 更新项
        /// </summary>
        public virtual UpgradeItem UpgradeItem { get; set; }

        public DateTime CreateTime { get; set; }
        public string Creater { get; set; }
    }
}
