using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class TreeNode
    {
        /// <summary>
        /// 子id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<TreeNode> Children { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TreeNode()
        {
            Children = new List<TreeNode>();
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="id">子id</param>
        /// <param name="name">名称</param>
        /// <param name="parentId">父id</param>
        public TreeNode(string id, string name, string parentId)
        {
            this.Id = id;
            this.Name = name;
            this.ParentId = parentId;
            Children = new List<TreeNode>();
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="id">子id</param>
        /// <param name="name">名称</param>
        /// <param name="parent">父节点</param>
        public TreeNode(string id, string name, TreeNode parent)
        {
            this.Id = id;
            this.Name = name;
            this.ParentId = parent.Id;
            Children = new List<TreeNode>();
        }
    }
}
