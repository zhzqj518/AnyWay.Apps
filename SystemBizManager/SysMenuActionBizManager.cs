using AnyWay.Apps.Core.BizManager;
using AnyWay.Apps.Core.Message;
using AnyWay.Apps.Core.Parameter;
using AnyWay.Apps.Persistence.MyBatis;
using AnyWay.Apps.Persistence.Transaction;
using ISystemBizManager;
using ISystemRepository;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace SystemBizManager
{
    public class SysMenuActionBizManager : BaseBizManager, ISysMenuActionBizManager
    {
        [Dependency]
        public ISysMenuActionRepository resp { get; set; }

        public TotalData GetTotalData(SysMenuActionQuery menuActionQuery)
        {
            TotalData totalData = new TotalData();

            ModelQuery modelQuery = new ModelQuery();
            modelQuery.PageIndex = menuActionQuery.PageIndex;
            modelQuery.PageSize = menuActionQuery.PageSize;
            modelQuery.QueryParam = menuActionQuery;

            totalData.total = resp.GetListCnt(modelQuery);
            totalData.data = resp.GetList(modelQuery);

            return totalData;
        }

        public IList<SysMenuActionLink> GetMenuRoleActions(SysMenuActionQuery sysMenuActionQuery)
        {
            return resp.GetMenuRoleActions(sysMenuActionQuery);
        }

        public override void Dispose()
        {
            resp.Dispose();
        }
    }
}
