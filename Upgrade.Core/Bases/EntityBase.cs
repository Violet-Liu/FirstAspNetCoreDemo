using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Interfaces;

namespace Upgrade.Core.Bases
{
    public abstract class EntityBase : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

    }
}
