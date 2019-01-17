using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Bases;
using Upgrade.Core.Interfaces;

namespace Upgrade.Core.DomainModels
{
    public class ClientSet : EntityBase
    {
        public string ParkId { get; set; }

        public string ParkName { get; set; }
        public int UpgradeItemId { get; set; }
        public virtual UpgradeItem UpgradeItem { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creater { get; set; }
    }
}
