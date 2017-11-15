using AnyWay.Apps.Persistence.MyBatis;
using ISystemRepository;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRepository
{
    public class SysGroupRepository : RepositoryMyBatis<SysGroup>, ISysGroupRepository
    {
        public SysGroupRepository() : base(MyBatisWebMapper.Instance()) { }
    }
}
