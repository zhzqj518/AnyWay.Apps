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
    public interface ISysGroupMenuActionRepository : IRepository<SysGroupMenuAction>
    {

        IList<SysGroupMenuActionCheck> GetCheckList(SysGroupMenuActionQuery sysGroupMenuActionQuery);

        bool DeleteByGroupMenu(SysGroupMenuActionQuery sysGroupMenuActionQuery);
    }
}
