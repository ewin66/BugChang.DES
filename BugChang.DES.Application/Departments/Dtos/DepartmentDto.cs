﻿using System;
using System.Collections.Generic;

namespace BugChang.DES.Application.Departments.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Code { get; set; }

        public int? ParentId { get; set; }

        public bool IsDeleted { get; set; }

        public IList<DepartmentDto> Children { get; set; }

        public DepartmentDto Parent { get; set; }

        public string CreateUserName { get; set; }

        public string UpdateUserName { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
