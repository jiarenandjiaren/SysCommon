﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Effort.Internal.DbManagement.Schema;
using Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.Service
{
    public class DatabaseService : BaseService<ContentReplace>
    {
        private SysCommonDBContext _context;
        public DatabaseService(IUnitWork unitWork, IRepository<ContentReplace> repository, SysCommonDBContext context) : base(unitWork, repository)
        {
            _context = context;
        }
        //public List<KeyDescription> GetPropertiesById(string moduleId)
        //{
        //    var moduleName = _context.Modules.FirstOrDefault(u => u.Id == moduleId)?.Code;
        //    return GetProperties(moduleName);
        //}

        /// <summary>
        /// 获取数据库一个表的所有属性值及属性描述
        /// </summary>
        /// <param name="moduleName">模块名称/表名</param>
        /// <returns></returns>
        public CheckData GetProperties(GetTableNameReq TableName)
        {
            var result = new List<KeyDescription>();
            var entity = _context.Model.GetEntityTypes().FirstOrDefault(u => u.Name.Contains(TableName.TableName));
            if (entity == null)
            {
                throw new Exception($"未能找到{TableName}对应的实体类");
            }

            foreach (var property in entity.ClrType.GetProperties())
            {
                object[] objs = property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                object[] browsableObjs = property.GetCustomAttributes(typeof(BrowsableAttribute), true);
                var description = objs.Length > 0 ? ((DescriptionAttribute) objs[0]).Description : property.Name;
                if (string.IsNullOrEmpty(description)) description = property.Name;
                //如果没有BrowsableAttribute或 [Browsable(true)]表示可见，其他均为不可见，需要前端配合显示
                bool browsable = browsableObjs == null || browsableObjs.Length == 0 ||
                                 ((BrowsableAttribute) browsableObjs[0]).Browsable;
                var typeName = property.PropertyType.Name;
                if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    typeName = Nullable.GetUnderlyingType(property.PropertyType).Name;
                }
                result.Add(new KeyDescription
                {
                    Key = property.Name,
                    Description = description,
                    Browsable = browsable,
                    Type = typeName
                });
            }
            return new CheckData
            {
                data = result,
            };
            //return result;
        }

        /// <summary>
        /// 获取数据库所有的表名
        /// </summary>
        public CheckData GetTableNames()
        {
            var names = new List<string>();
            var model = _context.Model;

            // Get all the entity types information contained in the DbContext class, ...
            var entityTypes = model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
                names.Add(tableNameAnnotation.Value.ToString());
            }
            return new CheckData
            {
                data = names,
            };
           // return names;
        }
        public void ContentReplace(ContentReplaceReq contentReplaceReq)
        {
            ContentReplace requser = contentReplaceReq;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("	UPDATE  " + contentReplaceReq.TableName + "  SET ");
            stringBuilder.Append("" + contentReplaceReq.FieldName + " =  ");
            stringBuilder.Append("REPLACE(" + contentReplaceReq.FieldName + ", '" + contentReplaceReq.OldContent + "', '" + contentReplaceReq.NewContent + "') ");

            int i = Repository.ExecuteSql(stringBuilder.ToString());
            if (i > 0)//替换成功
                Repository.Add(requser);//添加记录
        }
    }
}