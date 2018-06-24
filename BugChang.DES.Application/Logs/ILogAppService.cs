﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BugChang.DES.Application.Logs.Dtos;
using BugChang.DES.Core.Commons;

namespace BugChang.DES.Application.Logs
{
    public interface ILogAppService
    {
        Task<PageResultModel<AuditLogListDto>> GetAuditLogs(PageSearchModel pageSearchModel);

        Task<PageResultModel<SystemLogListDto>> GetSystemLogs(PageSearchModel pageSearchModel);


    }
}
