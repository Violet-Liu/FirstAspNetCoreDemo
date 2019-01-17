using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Upgrade.Core.Interfaces;
using Upgrade.Infrastructure.Interfaces;

namespace Upgrade.Infrastructure.Services
{
    public class CoreService<T> : ICoreService<T>
    {
        public IUnitOfWork UnitOfWork { get; }

        public ILogger<T> Logger { get; }

        public IMapper Mapper { get; }

        public CoreService(IUnitOfWork unitOfWork, ILogger<T> logger, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
            Mapper = mapper;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}
