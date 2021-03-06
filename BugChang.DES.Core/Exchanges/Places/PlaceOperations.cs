﻿using BugChang.DES.Core.Authorization.Operations;

namespace BugChang.DES.Core.Exchanges.Places
{
    public class PlaceOperations : IOperations
    {
        public string GetMenuUrl()
        {
            return "/Place/Index";
        }

        public string GetModuleName()
        {
            return "Place";
        }

        public Operation PlaceCreate => new Operation
        {
            Name = Operation.CreateName,
            Code = "Place.Create"
        };

        public Operation PlaceEdit => new Operation
        {
            Name = Operation.EditName,
            Code = "Place.Edit"
        };

        public Operation PlaceDelete => new Operation
        {
            Name = Operation.DeleteName,
            Code = "Place.Delete"
        };

        public Operation AssignWarden => new Operation
        {
            Name = "分配管理员",
            Code = "Place.AssignWarden"
        };
    }
}
