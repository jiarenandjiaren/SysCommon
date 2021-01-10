using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCommon.Service.Response
{
    public class LabelView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public static implicit operator LabelView(SysCommon.Repository.Domain.Label label)
        {
            return label.MapTo<LabelView>();
        }
        public static implicit operator SysCommon.Repository.Domain.Label(LabelView labelView)
        {
            return labelView.MapTo<SysCommon.Repository.Domain.Label>();
        }
        public LabelView()
        { }
    }
}
