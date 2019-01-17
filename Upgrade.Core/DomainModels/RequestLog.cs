using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Bases;

namespace Upgrade.Core.DomainModels
{
    public class RequestLog:EntityBase
    {
        public string ParkId { get; set; }

        public DateTime CreateTime { get; set; }

        public string ActionName { get; set; }

        public string ParkIP { get; set; }

        public string RespMsg { get; set; }
    }
}
