﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BugChang.DES.Core.Authorization.Operations;

namespace BugChang.DES.Application.Operations
{
    public interface IOperationAppService
    {
        IList<Operation> GetOperationsByUrl(string url, out string module);

    }
}
