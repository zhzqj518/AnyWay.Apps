using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
    [Serializable]
    public class SysGroupMenuActionQuery
    {
        public string GroupId { set; get; }

        public string MenuId { set; get; }
    }
}
