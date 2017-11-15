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
    public interface ISysMenuSpclBizManager : IBaseBizManager
    {
        IList<SysMenuSpcl> GetList(SysMenuSpclQuery menuSpclQuery);

        ResultInfo SaveMenuSpclChanges(List<SysMenuSpcl> addList, List<SysMenuSpcl> modifyList, List<SysMenuSpcl> removeList);
    }
}
