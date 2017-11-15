using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.System.ViewModel;
using Models.System;
using ISystemRepository;
using AnyWay.Apps.Persistence.MyBatis;

namespace SystemRepository
{
    public class SysMenuSpclRepository : RepositoryMyBatis<SysMenuSpcl>, ISysMenuSpclRepository
    {
        public SysMenuSpclRepository() : base(MyBatisWebMapper.Instance()) { }
    }
}
