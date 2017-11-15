using ISystemBizManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.System;
using Unity.Attributes;
using Models.System.ViewModel;
using ISystemRepository;
using AnyWay.Apps.Core.Parameter;
using AnyWay.Apps.Persistence.Transaction;
using AnyWay.Apps.Core.Message;
using AnyWay.Apps.Core.BizManager;

namespace SystemBizManager
{
    public class SysGroupMenuActionBizManager : BaseBizManager, ISysGroupMenuActionBizManager
    {

        [Dependency]
        public ISysGroupMenuActionRepository resp { get; set; }

        public IList<SysGroupMenuActionCheck> GetCheckList(SysGroupMenuActionQuery sysGroupMenuActionQuery)
        {
            return resp.GetCheckList(sysGroupMenuActionQuery);
        }
    }
}
