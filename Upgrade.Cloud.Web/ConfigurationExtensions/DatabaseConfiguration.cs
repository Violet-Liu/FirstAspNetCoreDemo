using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upgrade.Infrastructure.Data;

namespace Upgrade.Cloud.Web.ConfigurationExtensions
{
    public static class DatabaseConfiguration
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var uDBContext = serviceScope.ServiceProvider.GetRequiredService<UDBContext>();
                uDBContext.Database.Migrate();
            }
        }
    }
}
