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
    public class SysMenuRepository : RepositoryMyBatis<SysMenu>, ISysMenuRepository
    {
        public SysMenuRepository() : base(MyBatisWebMapper.Instance()) { }

        public SysMenu GetUserRoleMenu(SysMenuQuery menuQuery)
        {
            String stmtid = "SysMenu.GetUserRoleMenu";
            return MyBatisWebMapper.Instance().QueryForObject<SysMenu>(stmtid, menuQuery);
        }

        public IList<SysMenu> GetListByUser(SysMenuQuery menuQuery)
        {
            String stmtid = "SysMenu.GetListByUser";
            return MyBatisWebMapper.Instance().QueryForList<SysMenu>(stmtid, menuQuery);
        }
    }
}
