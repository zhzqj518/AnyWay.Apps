using AnyWay.Apps.Core.Repository;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISystemRepository
{
    public interface ISysMenuRepository : IRepository<SysMenu>
    {
        SysMenu GetUserRoleMenu(SysMenuQuery menuQuery);

        IList<SysMenu> GetListByUser(SysMenuQuery menuQuery);
    }
}
