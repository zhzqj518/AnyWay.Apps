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
    public interface ISysMenuActionBizManager : IBaseBizManager
    {
        TotalData GetTotalData(SysMenuActionQuery menuActionQuery);

        IList<SysMenuActionLink> GetMenuRoleActions(SysMenuActionQuery sysMenuActionQuery);
    }
}
