using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Upgrade.Core.Interfaces;

namespace Upgrade.Infrastructure.Interfaces
{
    public interface ICoreService<out T>: IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        ILogger<T> Logger { get; }
        IMapper Mapper { get; }
    }
}
