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
    public interface ISysGroupMenuRepository : IRepository<SysGroupMenu>
    {
        bool DeleteByGroupID(SysGroupMenu entity);

        IList<SysGroupMenuCheck> GetMenuListByGroupId(string groupId);
    }
}
