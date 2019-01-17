using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Upgrade.Infrastructure.Data;
using AutoMapper;
using Upgrade.Cloud.Web.Filters;
using Upgrade.Core.Interfaces;
using Upgrade.Infrastructure.Repositories;
using Upgrade.Infrastructure.Services;
using Upgrade.Infrastructure.Interfaces;
using Upgrade.Cloud.Web.ConfigurationExtensions;
using Upgrade.Cloud.Web.Options;
using Microsoft.Extensions.Logging;
using Upgrade.Cloud.Web.Service;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Extensions;

namespace Upgrade.Cloud.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("Upgrade.Cloud.Web", LogLevel.Debug)
                .AddFile();
            });


            services.AddDbContextPool<UDBContext>(options =>
            {
                options.UseLazyLoadingProxies()
                    .UseMySql(Configuration.GetConnectionString("UpgradeMysql"));
            });

            services.AddAutoMapper(cfg=> {
                cfg.AddProfile(typeof(AutoMapperConfigurationProfile));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(ICoreService<>), typeof(CoreService<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.Configure<EmailOptions>(Configuration.GetSection("EmailSetting"));
            services.Configure<OSSOptions>(Configuration.GetSection("ALIOSS"));
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IOSSBaseProvider, OSSBaseProvider>();
            services.AddMvc(x=> {
                x.Filters.Add<GloableExceptionFilter>();
            }).AddJsonOptions(options=>{
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.InitializeDatabase();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
