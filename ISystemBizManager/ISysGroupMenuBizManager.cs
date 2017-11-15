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
    public interface ISysGroupMenuBizManager : IBaseBizManager
    {
        ResultInfo SaveGroupMenuChanges(List<SysGroupMenu> addList, List<SysGroupMenuAction> addActionList);

        IList<SysGroupMenuCheck> GetMenuListByGroupId(string groupId);
    }
}
