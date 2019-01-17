using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Bases;

namespace Upgrade.Core.DomainModels
{
    public class Park: EntityBase
    {
        public string ParkId { get; set; }

        public string ParkName { get; set; }
    }
}
