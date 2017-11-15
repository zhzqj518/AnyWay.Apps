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
    public class SysGroupMenu : BaseModel
    {
        [Display(Name = "用户组ID")]
        public string GroupId { get; set; }

        [Display(Name = "菜单ID")]
        public string MenuId { set; get; }
    }

    [Serializable]
    public class SysGroupMenuCheck : SysMenu
    {
        public string GroupId { set; get; }

        public List<SysGroupMenuActionCheck> MenuActions { set; get; }

        public bool MenuIsCheck
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
