﻿using System;
using System.Collections.Generic;
using System.Linq;
using BugChang.DES.Core.Authorization.Menus;
using BugChang.DES.Core.Authorization.Roles;
using BugChang.DES.Core.Authorization.Users;
using BugChang.DES.Core.Departments;
using BugChang.DES.Core.Security;
using Microsoft.Extensions.DependencyInjection;

namespace BugChang.DES.EntityFrameWorkCore
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DesDbContext>();


                //dbContext.Database.EnsureDeleted();

                dbContext.Database.EnsureCreated();

                if (!dbContext.Users.Any())
                {
                    #region Department

                    var department = new Department
                    {
                        Code = "001",
                        FullName = "初始单位",
                        Name = "初始单位"
                    };

                    dbContext.Departments.Add(department);

                    #endregion

                    #region User

                    var sysAdmin = new User
                    {
                        UserName = "sysadmin",
                        DisplayName = "系统管理员",
                        Enabled = true,
                        LoginErrorCount = 0,
                        Password = HashHelper.Md5(User.DefaultPassword),
                        Department = department,
                        UserRoles = new List<UserRole>
                        {
                            new UserRole
                            {
                                Role = new Role
                                {
                                    Name = Role.SysAdmin
                                }
                            }
                        }
                    };
                    dbContext.Users.Add(sysAdmin);

                    var secAdmin = new User
                    {
                        UserName = "secadmin",
                        DisplayName = "安全管理员",
                        Enabled = true,
                        LoginErrorCount = 0,
                        Password = HashHelper.Md5(User.DefaultPassword),
                        Department = department,
                        UserRoles = new List<UserRole>
                        {
                            new UserRole
                            {
                                Role = new Role
                                {
                                    Name = Role.SecAdmin
                                }
                            }
                        }
                    };
                    dbContext.Users.Add(secAdmin);

                    var audAdmin = new User
                    {
                        UserName = "audadmin",
                        DisplayName = "审计管理员",
                        Enabled = true,
                        LoginErrorCount = 0,
                        Password = HashHelper.Md5(User.DefaultPassword),
                        Department = department,
                        UserRoles = new List<UserRole>
                        {
                            new UserRole
                            {
                                Role = new Role
                                {
                                    Name = Role.AudAdmin
                                }
                            }
                        }
                    };
                    dbContext.Users.Add(audAdmin);

                    #endregion

                    #region Menu

                    var sysMenu = new Menu
                    {
                        Name = "系统管理",
                        Items = new List<Menu>
                        {
                            new Menu
                            {
                                Name = "用户管理",
                                Url = "/User/Index"
                            },
                            new Menu
                            {
                                Name = "组织机构",
                                Url = "/Department/Index"
                            },
                            new Menu
                            {
                                Name = "菜单管理",
                                Url = "/Menu/Index"
                            }
                        }
                    };

                    dbContext.Menus.Add(sysMenu);

                    #endregion

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
