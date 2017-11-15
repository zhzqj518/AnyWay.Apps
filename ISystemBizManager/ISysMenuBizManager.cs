using AnyWay.Apps.Core.BizManager;
using AnyWay.Apps.Core.Message;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISystemBizManager
{
    public interface ISysMenuBizManager : IBaseBizManager
    {
        SysMenu GetUserRoleMenu(SysMenuQuery menuQuery);

        IList<SysMenu> GetUserList(SysMenuQuery menuQuery);

        IList<SysMenu> GetList(SysMenuQuery menuQuery);

        bool InsertList(List<SysMenu> entity);

        bool UpdateList(List<SysMenu> entity);

        bool DeleteList(List<SysMenu> entity);

        ResultInfo SaveMenuChanges(List<SysMenu> addList, List<SysMenu> modifyList, List<SysMenu> removeList, List<SysMenuAction> addActionList, List<SysMenuAction> modifyActionList, List<SysMenuAction> removeActionList);
    }
}
