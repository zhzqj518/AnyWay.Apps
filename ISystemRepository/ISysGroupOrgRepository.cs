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
    public interface ISysGroupOrgRepository : IRepository<SysGroupOrg>
    {
        bool DeleteByGroupID(SysGroupOrg entity);

        IList<SysGroupOrgCheck> GetOrgListByGroupId(string groupId);
    }
}
