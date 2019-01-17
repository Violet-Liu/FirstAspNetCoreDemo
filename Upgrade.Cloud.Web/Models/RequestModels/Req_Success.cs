using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.Models.RequestModels
{
    public class Req_Success
    {
        /// <summary>
        /// 停车场ID
        /// </summary>
        public string ParkId { get; set; }

        /// <summary>
        /// 更新项ID
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        public string Msg { get; set; }
    }
}
