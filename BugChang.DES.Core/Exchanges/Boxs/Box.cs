﻿using System.ComponentModel.DataAnnotations.Schema;
using BugChang.DES.Core.Commons;
using BugChang.DES.Core.Exchanges.ExchangeObjects;
using BugChang.DES.Core.Exchanges.Places;

namespace BugChang.DES.Core.Exchanges.Boxs
{
    public class Box : BaseEntity, ISoftDelete
    {
        /// <summary>
        /// 设备码
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件数量
        /// </summary>
        public int FileCount { get; set; }

        /// <summary>
        /// 有紧急文件
        /// </summary>
        public bool HasUrgentFile { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips { get; set; }

        /// <summary>
        /// 场所ID
        /// </summary>
        public int PlaceId { get; set; }

        /// <summary>
        /// 流转对象ID
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Order { get; set; }

        public bool IsDeleted { get; set; }


        [ForeignKey("PlaceId")]
        public virtual Place Place { get; set; }

        [ForeignKey("ObjectId")]
        public virtual ExchangeObject Object { get; set; }
    }
}
