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
    public interface ISysUserBizManager : IBaseBizManager
    {
        IList<SysUser> GetList(SysUserQuery sysUserQuery);

        TotalData GetTotalData(SysUserQuery sysUserQuery);

        ResultInfo SaveUserChanges(List<SysUser> addList, List<SysUser> modifyList);

        UserLoginResult UserLogin(UserLoginModel userLoginModel);
    }
}
