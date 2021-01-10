using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysCommon.Repository.Core
{
    public abstract class BaseEntity:Entity
    {
        public BaseEntity()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            this.IsEnable = true;
            this.Sort = 0;
            this.Description = string.Empty;
            this.CreateId = string.Empty; 
            this.UpdateId = string.Empty;
        }
        [Description("创建人Id")]
        public string CreateId { get; set; }
        [Description("创建时间")]
        public DateTime? CreateTime { get; set; }
        [Description("修改人Id")]
        public string  UpdateId { get; set; }
        [Description("修改时间")]
        public DateTime? UpdateTime { get; set; }
        [Description("是否启动")]
        public bool IsEnable { get; set; }
        [Description("是否删除")]
        public bool IsDelete { get; set; }
        [Description("排序")]
        public int Sort { get; set; }
        [Description("备注")]
        public string Description { get; set; }
    }
}
