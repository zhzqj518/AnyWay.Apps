using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
    [Serializable]
    public class SysMenuQuery
    {
        public string UserId { set; get; }

        public string UserType { set; get; }

        public string MenuId { set; get; }

        public string MenuLink { set; get; }

        public List<string> OperateType { set; get; }

        public string SortBy { set; get; }

        public string SortOrder { set; get; }
    }
}
