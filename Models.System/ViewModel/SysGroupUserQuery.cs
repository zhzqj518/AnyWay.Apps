using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
    [Serializable]
    public class SysGroupUserQuery
    {
        public string GroupId { set; get; }

        public string UserName { set; get; }

        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public string SortBy { set; get; }

        public string SortOrder { set; get; }
    }
}
