using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Upgrade.Cloud.Web.Controllers.Common;
using Upgrade.Infrastructure.Interfaces;
using Upgrade.Infrastructure.Repositories;
using Upgrade.Core.DomainModels;
using Mall.Common.Extension;
using Upgrade.Cloud.Web.Models;
using Upgrade.Cloud.Web.Service;
using Upgrade.Core.Interfaces;
using Upgrade.Cloud.Web.Models.RequestModels;
using Microsoft.Extensions.Logging;
using Upgrade.Cloud.Web.ConfigurationExtensions;

namespace Upgrade.Cloud.Web.Controllers
{
    [Route("api/upgrade")]
    public class ApiController : RepositoryRangeBaseController<ApiController>
    {
        private readonly IEmailSender _emailSender;
        private readonly IRepository<RequestLog> _requestLogRepository;
        public ApiController(ICoreService<ApiController> coreService,
            IRepository<UpgradeItem> upgradeItemRepository,
            IRepository<ClientUpgradeItem> clientUpgradeItemRepository,
            IRepository<UpgradeFiles> upgradeFilesRepository,
            IRepository<RequestLog> requestLogRepository,
            IEmailSender emailSender) : base(coreService, upgradeItemRepository, clientUpgradeItemRepository, upgradeFilesRepository)
        {
            _requestLogRepository = requestLogRepository;
            _emailSender = emailSender;
        }

        [HttpPost("")]
        public async Task<IActionResult> Pull([FromBody]Req_Pull request)
        {
            var response = new GetUpgradeInfo();
            using (Logger.BeginScope("Message attached to logs gets the latest upgrade"))
            {
                Logger.LogInformation($"Beging getting item by parkid:{request.ParkId} ");
                var upgradeItem = await _upgradeItemRepository.GetSingleAsync(b => b.IsValid, d => d.Id, false);
                if (upgradeItem.IsNotNull())
                {
                    var client =await _clientUpgradeItemRepository.GetSingleAsync(d => d.IsUpgradeSucess == true
                        && d.UpgradeItemId == upgradeItem.Id);
                    if (client.IsNull())
                    {
                        var files = await _upgradeFilesRepository.ListAsync(d => d.UpgradeItemId == upgradeItem.Id);
                        if (!files.IsNullOrEmpty())
                        {
                            Logger.LogInformation("Getting succcess with upgrade files");
                            response.Code = nameof(UpgradeEnum.Success);
                            response.data = Mapper.Map<List<FileDto>>(files);
                        }
                        else
                        {
                            Logger.LogWarning("Cloud has set upgrade,but not set upgrade files");
                            response.Code = nameof(UpgradeEnum.FilesNotFound);
                        }

                    }
                    else
                    {
                        Logger.LogInformation("The client has been upgraded successfully");
                        response.Code = nameof(UpgradeEnum.Yet);
                    }
                }
                else
                {
                    Logger.LogInformation($"No upgrade item set");
                    response.Code = nameof(UpgradeEnum.None);
                }
            }

            var newLog = new RequestLog { ParkId = request.ParkId, ParkIP = request.ParkIP, CreateTime = DateTimeOffset.Now.DateTime, RespMsg = response.ToJson(),ActionName=$"{nameof(ApiController)}-Pull" };
            _requestLogRepository.Add(newLog);
            await UnitOfWork.SaveAsync();

            return Ok(response);
        }

        [HttpPost]
        [Route("success")]
        public async Task<IActionResult> Success([FromBody]Req_Success request)
        {

            var newItem = new ClientUpgradeItem
            {
                ParkId = request.ParkId,
                UpgradeItemId = request.UId,
                IsUpgradeSucess = request.Success,
                UpgradeRespMsg=request.Msg,
                Creater = "sys",
                CreateTime = DateTimeOffset.Now.DateTime
            };

            _clientUpgradeItemRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return Ok(new { Code = "Failed", Msg = "服务端保存失败" });
            }

            return Ok(new { Code = "Success", Msg = "保存成功" });
        }

    }
}