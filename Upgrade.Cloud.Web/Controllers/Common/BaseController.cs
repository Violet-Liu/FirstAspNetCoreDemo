using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Upgrade.Core.Interfaces;
using Upgrade.Infrastructure.Interfaces;

namespace Upgrade.Cloud.Web.Controllers.Common
{
    public class BaseController<T> : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILogger<T> Logger;
        protected readonly IMapper Mapper;
        protected readonly ICoreService<T> CoreService;

        public BaseController(ICoreService<T> coreService)
        {
            UnitOfWork = coreService.UnitOfWork;
            Logger = coreService.Logger;
            Mapper = coreService.Mapper;
            CoreService = coreService;
        }
    }
}