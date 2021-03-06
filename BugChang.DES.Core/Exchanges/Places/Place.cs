﻿using System.ComponentModel.DataAnnotations.Schema;
using BugChang.DES.Core.Commons;
using BugChang.DES.Core.Departments;

namespace BugChang.DES.Core.Exchanges.Places
{
    public class Place : BaseEntity, ISoftDelete
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 上级交换场所
        /// </summary>
        public int? ParentId { get; set; }


        public bool IsDeleted { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [ForeignKey("ParentId")]
        public virtual Place Parent { get; set; }
    }
}
