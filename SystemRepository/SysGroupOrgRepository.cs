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
    public class SysGroupOrgRepository : RepositoryMyBatis<SysGroupOrg>, ISysGroupOrgRepository
    {
        public SysGroupOrgRepository() : base(MyBatisWebMapper.Instance()) { }

        public bool DeleteByGroupID(SysGroupOrg entity)
        {
            String stmtid = "SysGroupOrg.DeleteByGroupID";
            MyBatisWebMapper.Instance().Delete(stmtid, entity);
            return true;
        }

        public IList<SysGroupOrgCheck> GetOrgListByGroupId(string groupId)
        {
            String stmtid = "SysGroupOrg.GetOrgListByGroupId";
            return MyBatisWebMapper.Instance().QueryForList<SysGroupOrgCheck>(stmtid, groupId);
        }
    }
}
