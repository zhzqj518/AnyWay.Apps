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
    public class SysMenuAction : BaseModel
    {
        [Display(Name = "ID")]
        public string ActionId { set; get; }

        [Display(Name = "菜单ID")]
        public string MenuId { set; get; }

        [Display(Name = "操作项名称")]
        public string ActionName { set; get; }

        [Display(Name = "控件Id")]
        public string ActionBtnId { set; get; }

        [Display(Name = "是否有效")]
        public int ActionIsValid { set; get; }

        [Display(Name = "创建人")]
        public string ActionCreateBy { set; get; }

        [Display(Name = "创建时间")]
        public DateTime? ActionCreateTime { set; get; }

        [Display(Name = "更新人")]
        public string ActionUpdateBy { set; get; }

        [Display(Name = "更新时间")]
        public DateTime? ActionUpdateTime { set; get; }
    }

    [Serializable]
    public class SysMenuAction_MiniUI : SysMenuAction
    {
        public string _menuid { set; get; }
    }

    [Serializable]
    public class SysMenuActionLink : SysMenuAction
    {
        public string MenuLink { set; get; }

        public int ActionIsCheck { set; get; }
    }
}
