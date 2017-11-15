using Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyWay.Apps.Core.Model;
using AnyWay.Apps.Core.BizManager;

namespace ISystemBizManager
{
    public interface ISysLogBizManager : IBaseBizManager
    {
        bool Insert(List<SysLog> List);
    }
}
