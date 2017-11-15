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
    public class SysUserRepository : RepositoryMyBatis<SysUser>, ISysUserRepository
    {
        public SysUserRepository() : base(MyBatisWebMapper.Instance()) { }

        public SysUser GetUserByLogin(UserLoginModel userLoginModel)
        {
            String stmtid = "SysUser.GetUserByLogin";
            return MyBatisWebMapper.Instance().QueryForObject<SysUser>(stmtid, userLoginModel);
        }
    }
}
