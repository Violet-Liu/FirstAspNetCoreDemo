using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Upgrade.Cloud.Web.Models;
using Upgrade.Core.DomainModels;

namespace Upgrade.Cloud.Web.ConfigurationExtensions
{
    public class AutoMapperConfigurationProfile : Profile
    {
        public override string ProfileName => "UpgradeMappings";
        public AutoMapperConfigurationProfile()
        {
            CreateMap<UpgradeFiles, FileDto>();
        }
    }
}
