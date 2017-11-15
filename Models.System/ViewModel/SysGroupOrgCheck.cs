using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
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
