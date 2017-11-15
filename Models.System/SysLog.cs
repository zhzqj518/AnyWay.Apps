using AnyWay.Apps.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.System
{
    [Serializable]
    public class SysLog : BaseModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "操作人")]
        public string Operator { get; set; }

        [Display(Name = "操作人IP地址")]
        public string OperatorIP { get; set; }

        [Display(Name = "信息")]
        public string Message { get; set; }

        [Display(Name = "结果")]
        public string Result { get; set; }

        [Display(Name = "类型")]
        public string Type { get; set; }

        [Display(Name = "模块")]
        public string Module { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}
