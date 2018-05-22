﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugChang.DES.EntityFrameWorkCore
{
    /// <summary>
    /// 工作单元，负责统一提交
    /// </summary>
    /// <typeparam name="TDbContext">EF上下文</typeparam>
    public class UnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}