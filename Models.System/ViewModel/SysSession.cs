using AnyWay.Apps.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
    [Serializable]
    public class SysSession : BaseModel
    {
        public SysUser User { set; get; }

        public List<SysUserMenu> UserMenu { set; get; }
    }

    [Serializable]
    public class SysUserMenu
    {
        public string MenuId { set; get; }

        public string MenuLink { set; get; }
    }
}
