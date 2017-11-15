using AnyWay.Apps.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System
{
    [Serializable]
    public class SysGroup : BaseModel
    {
        [Display(Name = "主键")]
        public string GroupId { get; set; }

        [Display(Name = "用户组名称")]
        public string GroupName { set; get; }

        [Display(Name = "用户组类型:func功能组,data数据组")]
        public string GroupType { get; set; }

        [Display(Name="用户组是否有效")]
        public int GroupIsValid { get; set; }

        [Display(Name = "用户组备注")]
        public string GroupRemark { get; set; }

        [Display(Name = "用户组创建人")]
        public string GroupCreateBy { get; set; }

        [Display(Name = "用户组创建时间")]
        public DateTime? GroupCreateTime { get; set; }

        [Display(Name = "用户组更新人")]
        public int GroupUpdateBy { get; set; }

        [Display(Name = "用户组更新时间")]
        public DateTime? GroupUpdateTime { get; set; }
    }
}
