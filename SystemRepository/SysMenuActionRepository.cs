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
    public class SysMenuActionRepository : RepositoryMyBatis<SysMenuAction>, ISysMenuActionRepository
    {
        public SysMenuActionRepository() : base(MyBatisWebMapper.Instance()) { }

        public IList<SysMenuActionLink> GetMenuRoleActions(SysMenuActionQuery sysMenuActionQuery)
        {
            String stmtid = "SysMenuAction.GetMenuRoleActions";
            return MyBatisWebMapper.Instance().QueryForList<SysMenuActionLink>(stmtid, sysMenuActionQuery);
        }

        public bool DeleteByMenuId(string menuId)
        {
            String stmtid = "SysMenuAction.DeleteByMenuId";
            MyBatisWebMapper.Instance().Delete(stmtid, menuId);
            return true;
        }
    }
}
