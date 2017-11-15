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
    public class SysGroupOrg : BaseModel
    {
        [Display(Name = "用户组ID")]
        public string GroupId { get; set; }

        [Display(Name = "组织机构ID")]
        public string OrgId { set; get; }
    }

    [Serializable]
    public class SysGroupOrgCheck : SysOrg
    {
        public string GroupId { set; get; }

        public bool OrgIsCheck
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
