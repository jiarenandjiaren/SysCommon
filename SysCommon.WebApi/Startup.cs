using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using IdentityServer4.AccessTokenValidation;
using Infrastructure.Extensions.AutofacManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SysCommon.App;
using SysCommon.App.HostedService;
using SysCommon.Repository;
using SysCommon.WebApi.Model;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SysCommon.WebApi
{
    public class Startup
    {
        public IHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           services.Configure<ApiBehaviorOptions>(options =>
           {
               options.SuppressModelStateInvalidFilter = true;
           });
            
            services.AddSingleton(provider =>
            {
                var service = provider.GetRequiredService<ILogger<StartupLogger>>();
                return new StartupLogger(service);
            });
            var logger = services.BuildServiceProvider().GetRequiredService<StartupLogger>();
            
            var identityServer = ((ConfigurationSection)Configuration.GetSection("AppSetting:IdentityServerUrl")).Value;
            if (!string.IsNullOrEmpty(identityServer))
            {
                services.AddAuthorization();

                services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = identityServer;
                        options.RequireHttpsMetadata = false;  // 指定是否为HTTPS
                        options.Audience = "SysCommonapi";
                   });
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VOL.Core后台Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "请输入token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                logger.LogInformation($"api doc basepath:{AppContext.BaseDirectory}");
                foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.*",
                    SearchOption.AllDirectories).Where(f => Path.GetExtension(f).ToLower() == ".xml"))
                {
                    c.IncludeXmlComments(name, includeControllerXmlComments: true);
                    logger.LogInformation($"find api file{name}");
                }
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            }).AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
                options.ClientErrorMapping[404].Link =
                    "https://*/404";
            });




            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));
            //访问验证（token验证）
            services.AddControllers(option =>
            {
                option.Filters.Add<SysCommonFilter>();
            }).AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();    
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            services.AddMemoryCache();

            //必须appsettings.json中配置    跨域请求
            string corsUrls = Configuration["CorsUrls"];
            if (string.IsNullOrEmpty(corsUrls))
            {
                throw new Exception("请配置跨域请求的前端Url");
            }
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(corsUrls.Split(","))
                        //添加预检请求过期时间
                         .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                        .AllowCredentials()
                        .AllowAnyHeader().AllowAnyMethod();
                    });
            });



            //在startup里面只能通过这种方式获取到appsettings里面的值，不能用IOptions😰
            var dbType = ((ConfigurationSection)Configuration.GetSection("AppSetting:DbType")).Value;
            if (dbType == Define.DBTYPE_SQLSERVER)
            {
                services.AddDbContext<SysCommonDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SysCommonDBContext")));
            }
            else  //mysql
            {
                services.AddDbContext<SysCommonDBContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("SysCommonDBContext")));
            }

            services.AddHttpClient();

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Configuration["DataProtection"]));
            
            //设置定时启动的任务
            services.AddHostedService<QuartzService>();
            
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            AutofacExt.InitAutofac(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //可以访问根目录下面的静态文件
            var staticfile = new StaticFileOptions {FileProvider = new PhysicalFileProvider(AppContext.BaseDirectory) };
            app.UseStaticFiles(staticfile);
            

            //todo:测试可以允许任意跨域，正式环境要加权限
            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseRouting();
            app.UseAuthentication();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            //配置ServiceProvider
            AutofacContainerModule.ConfigServiceProvider(app.ApplicationServices);

          app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.DocExpansion(DocExpansion.None);
                c.OAuthClientId("SysCommon.WebApi");  //oauth客户端名称
                c.OAuthAppName("开源版webapi认证"); // 描述
            });

        }
    }
}
