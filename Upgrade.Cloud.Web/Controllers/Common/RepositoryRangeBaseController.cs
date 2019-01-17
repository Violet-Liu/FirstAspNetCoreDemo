using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Upgrade.Infrastructure.Interfaces;
using Upgrade.Infrastructure.Repositories;
using Upgrade.Core.DomainModels;
using Upgrade.Core.Interfaces;

namespace Upgrade.Cloud.Web.Controllers.Common
{
    public class RepositoryRangeBaseController<T> : BaseController<T>
    {
        protected readonly IRepository<UpgradeItem> _upgradeItemRepository;
        protected readonly IRepository<ClientUpgradeItem> _clientUpgradeItemRepository;
        protected readonly IRepository<UpgradeFiles> _upgradeFilesRepository;
        public RepositoryRangeBaseController(ICoreService<T> coreService,
            IRepository<UpgradeItem> upgradeItemRepository,
            IRepository<ClientUpgradeItem> clientUpgradeItemRepository,
            IRepository<UpgradeFiles> upgradeFilesRepository) : base(coreService)
        {
            _upgradeItemRepository = upgradeItemRepository;
            _clientUpgradeItemRepository = clientUpgradeItemRepository;
            _upgradeFilesRepository = upgradeFilesRepository;
        }
    }
}