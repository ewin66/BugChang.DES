﻿using BugChang.DES.Application.Commons;

namespace BugChang.DES.Application.Menus.Dtos
{
    public class MenuEditDto : EditDto
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public int? ParentId { get; set; }
    }
}
