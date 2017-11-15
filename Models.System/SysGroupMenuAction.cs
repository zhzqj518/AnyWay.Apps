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
    public class SysGroupMenuAction : BaseModel
    {
        [Display(Name = "用户组ID")]
        public string GroupId { set; get; }

        [Display(Name = "菜单ID")]
        public string MenuId { set; get; }

        [Display(Name = "操作项ID")]
        public string ActionId { set; get; }
    }

    [Serializable]
    public class SysGroupMenuActionCheck : SysGroupMenuAction
    {
        public string ActionName { set; get; }

        public bool ActionIsCheck
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GroupId))
                { return true; }
                return false;
            }
        }
    }
}
