using System;
using System.Collections.Generic;
using System.Text;

namespace Upgrade.Core.Interfaces
{
    interface IEntityBasic:IEntity
    { 
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        String Creater { get; set; }

    }
}
