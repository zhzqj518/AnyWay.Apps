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
    public interface ISysGroupUserBizManager : IBaseBizManager
    {
        ResultInfo SaveAssignGroupUser(List<SysGroupUser> addList);

        ResultInfo SaveUnAssignGroupUser(List<SysGroupUser> removeList);

        TotalData GetUnAssignSysUserTotalData(SysGroupUserQuery sysUserQuery);

        TotalData GetAssignSysUserTotalData(SysGroupUserQuery sysUserQuery);
    }
}
