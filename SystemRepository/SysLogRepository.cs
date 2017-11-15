using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.System;
using ISystemRepository;
using AnyWay.Apps.Persistence.MyBatis;

namespace SystemRepository
{
    public class SysLogRepository : RepositoryMyBatis<SysLog>, ISysLogRepository
    {
        public SysLogRepository() : base(MyBatisWebMapper.Instance()) { }
    }
}
