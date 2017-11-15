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
    public class SysGroupMenuActionRepository : RepositoryMyBatis<SysGroupMenuAction>, ISysGroupMenuActionRepository
    {
        public SysGroupMenuActionRepository() : base(MyBatisWebMapper.Instance()) { }

        public IList<SysGroupMenuActionCheck> GetCheckList(SysGroupMenuActionQuery sysGroupMenuActionQuery)
        {
            String stmtid = "SysGroupMenuAction.GetCheckList";
            return MyBatisWebMapper.Instance().QueryForList<SysGroupMenuActionCheck>(stmtid, sysGroupMenuActionQuery);
        }

        public bool DeleteByGroupMenu(SysGroupMenuActionQuery sysGroupMenuActionQuery)
        {
            String stmtid = "SysGroupMenuAction.DeleteByGroupMenu";
            MyBatisWebMapper.Instance().Delete(stmtid, sysGroupMenuActionQuery);

            return true;
        }
    }
}
