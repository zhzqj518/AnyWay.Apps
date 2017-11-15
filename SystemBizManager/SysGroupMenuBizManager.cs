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
    public class SysGroupMenuBizManager : BaseBizManager, ISysGroupMenuBizManager
    {

        [Dependency]
        public ISysGroupMenuRepository resp { get; set; }

        [Dependency]
        public ISysGroupMenuActionRepository respAction { get; set; }

        public ResultInfo SaveGroupMenuChanges(List<SysGroupMenu> addList, List<SysGroupMenuAction> addActionList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            SysGroupMenu item = addList.FirstOrDefault();

            resp.DeleteByGroupID(item);

            foreach (SysGroupMenu entity in addList)
            {
                if (!string.IsNullOrEmpty(entity.MenuId))
                {
                    resp.Insert(entity);
                    SysGroupMenuActionQuery sysGroupMenuActionQuery = new SysGroupMenuActionQuery();
                    sysGroupMenuActionQuery.GroupId = entity.GroupId;
                    sysGroupMenuActionQuery.MenuId = entity.MenuId;
                    respAction.DeleteByGroupMenu(sysGroupMenuActionQuery);
                }
            }

            foreach (SysGroupMenuAction entity in addActionList)
            {
                respAction.Insert(entity);
            }

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }

        public IList<SysGroupMenuCheck> GetMenuListByGroupId(string groupId)
        {
            return resp.GetMenuListByGroupId(groupId);
        }
    }
}
