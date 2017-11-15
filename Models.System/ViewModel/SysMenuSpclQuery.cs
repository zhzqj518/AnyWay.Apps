using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
    [Serializable]
    public class SysMenuSpclQuery
    {
        public List<string> OperateType { set; get; }

        public string SortBy { set; get; }

        public string SortOrder { set; get; }
    }
}
