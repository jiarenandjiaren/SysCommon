/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 数据列表
*         
* 类 名： PageReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 9:18   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
namespace SysCommon.Service.Request
{
    public class PageReq
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int state { get; set; }
        public string key { get; set; }

        public PageReq()
        {
            page = 1;
            limit = 1000000000;
            state = 2;
        }
    }
    public class SearchParameters
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DisplayType { get; set; }
    }
}
