﻿using AutoMapper;
using BugChang.DES.Application.Departments.Dtos;
using BugChang.DES.Application.Logs.Dtos;
using BugChang.DES.Application.Menus.Dtos;
using BugChang.DES.Application.Places.Dtos;
using BugChang.DES.Application.Roles.Dtos;
using BugChang.DES.Application.Users.Dtos;
using BugChang.DES.Core.Authorization.Menus;
using BugChang.DES.Core.Authorization.Roles;
using BugChang.DES.Core.Authorization.Users;
using BugChang.DES.Core.Departments;
using BugChang.DES.Core.Exchanges.Places;
using BugChang.DES.Core.Logs;
using BugChang.DES.Core.Tools;

namespace BugChang.DES.Application.Commons
{
    public static class DesMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuListDto>();
                cfg.CreateMap<MenuEditDto, Menu>();


                cfg.CreateMap<DepartmentEditDto, Department>();
                cfg.CreateMap<Department, DepartmentListDto>()
                    .ForMember(a => a.ParentName, b => b.MapFrom(c => c.Parent.Name))
                    .ForMember(a => a.CreateUserName, b => b.MapFrom(c => c.CreateUser.DisplayName))
                    .ForMember(a => a.UpdateUserName, b => b.MapFrom(c => c.UpdateUser.DisplayName))
                    .ForMember(a => a.CreateTime, b => b.MapFrom(c => c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ForMember(a => a.UpdateTime, b => b.MapFrom(c => c.UpdateTime == null ? "" : c.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));



                cfg.CreateMap<UserEditDto, User>();
                cfg.CreateMap<User, UserListDto>()
                    .ForMember(a => a.DepartmentName, b => b.MapFrom(c => c.Department.Name))
                    .ForMember(a => a.CreateUserName, b => b.MapFrom(c => c.CreateUser.DisplayName))
                    .ForMember(a => a.UpdateUserName, b => b.MapFrom(c => c.UpdateUser.DisplayName))
                    .ForMember(a => a.CreateTime, b => b.MapFrom(c => c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ForMember(a => a.UpdateTime, b => b.MapFrom(c => c.UpdateTime == null ? "" : c.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));

                cfg.CreateMap<RoleEditDto, Role>();
                cfg.CreateMap<Role, RoleListDto>()
                    .ForMember(a => a.CreateUserName, b => b.MapFrom(c => c.CreateUser.DisplayName))
                    .ForMember(a => a.UpdateUserName, b => b.MapFrom(c => c.UpdateUser.DisplayName))
                    .ForMember(a => a.CreateTime, b => b.MapFrom(c => c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ForMember(a => a.UpdateTime, b => b.MapFrom(c => c.UpdateTime == null ? "" : c.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));

                cfg.CreateMap<RoleOperationEditDto, RoleOperation>();

                cfg.CreateMap<Log, SystemLogListDto>()
                    .ForMember(a => a.CreateTime, b => b.MapFrom(c => c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ForMember(a => a.Level, b => b.MapFrom(c => EnumHelper.GetEnumDescription(c.Level)));


                cfg.CreateMap<Log, AuditLogListDto>()
                    .ForMember(a => a.CreateTime, b => b.MapFrom(c => c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ForMember(a => a.OperatorName, b => b.MapFrom(c => c.Operator.DisplayName))
                    .ForMember(a => a.Level, b => b.MapFrom(c => EnumHelper.GetEnumDescription(c.Level)));


                cfg.CreateMap<PlaceEditDto, Place>()
                    .ForMember(a => a.ParentId, b => b.MapFrom(c => c.ParentId == 0 ? null : c.ParentId));
                cfg.CreateMap<Place, PlaceEditDto>()
                    .ForMember(a => a.ParentId, b => b.MapFrom(c => c.ParentId ?? 0));
                cfg.CreateMap<Place, PlaceListDto>()
                    .ForMember(a => a.DepartmentName, b => b.MapFrom(c => c.Department.FullName))
                    .ForMember(a => a.ParentName, b => b.MapFrom(c => c.Parent.Name))
                    .ForMember(a => a.CreateUserName, b => b.MapFrom(c => c.CreateUser.DisplayName))
                    .ForMember(a => a.CreateUserName, b => b.MapFrom(c => c.CreateUser.DisplayName))
                    .ForMember(a => a.UpdateUserName, b => b.MapFrom(c => c.UpdateUser.DisplayName))
                    .ForMember(a => a.CreateTime, b => b.MapFrom(c => c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ForMember(a => a.UpdateTime, b => b.MapFrom(c => c.UpdateTime == null ? "" : c.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));

            });
        }

    }
}
