using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysCommon.Repository.Core
{
    public abstract class Entity
    {
        public Entity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsDelete = false;
        }
        [Browsable(false)]
        public string Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
