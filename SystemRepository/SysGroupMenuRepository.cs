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
    public class SysGroupMenuRepository : RepositoryMyBatis<SysGroupMenu>, ISysGroupMenuRepository
    {
        public SysGroupMenuRepository() : base(MyBatisWebMapper.Instance()) { }

        public bool DeleteByGroupID(SysGroupMenu entity)
        {
            String stmtid = "SysGroupMenu.DeleteByGroupID";
            MyBatisWebMapper.Instance().Delete(stmtid, entity);
            return true;
        }

        public IList<SysGroupMenuCheck> GetMenuListByGroupId(string groupId)
        {
            String stmtid = "SysGroupMenu.GetMenuListByGroupId";
            return MyBatisWebMapper.Instance().QueryForList<SysGroupMenuCheck>(stmtid, groupId);
        }
    }
}
