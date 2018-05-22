﻿using BugChang.DES.Application.Accounts;
using BugChang.DES.Application.Menus;
using BugChang.DES.Application.Users;
using BugChang.DES.Core.Authentication;
using BugChang.DES.Core.Authorization.Menus;
using BugChang.DES.Core.Authorization.Powers;
using BugChang.DES.Core.Authorization.Roles;
using BugChang.DES.Core.Authorization.Users;
using BugChang.DES.EntityFrameWorkCore;
using BugChang.DES.EntityFrameWorkCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugChang.DES.Web.Mvc
{
    public static class DependencyInjectionConfig
    {
        public static void Initialize(IServiceCollection services, IConfigurationRoot configuration)
        {


            #region DB

            var mainConnectionString = configuration.GetConnectionString("MainConnectionString");
            var logConnectionString = configuration.GetConnectionString("LogConnectionString");

            //注册业务数据库上下文
            services.AddDbContext<MainDbContext>(option => option.UseMySql(mainConnectionString));
            //注入日志数据库上下文
            services.AddDbContext<LogDbContext>(option => option.UseMySql(logConnectionString));

            services.AddScoped<UnitOfWork<MainDbContext>>();
            services.AddScoped<UnitOfWork<LogDbContext>>();

            #endregion


            #region AppService

            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IMenuAppService, MenuAppService>();

            #endregion


            #region Logic Manager

            services.AddScoped<LoginManager>();
            services.AddScoped<MenuManager>();

            #endregion


            #region Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPowerRepository, PowerRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            #endregion

        }
    }
}