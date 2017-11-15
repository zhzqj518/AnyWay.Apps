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
    public class SysGroupUserRepository : RepositoryMyBatis<SysGroupUser>, ISysGroupUserRepository
    {
        public SysGroupUserRepository() : base(MyBatisWebMapper.Instance()) { }

        public int GetUnAssignUserListCnt(SysGroupUserQuery sysUserQuery)
        {
            String stmtid = "SysGroupUser.GetUnAssignUserListCnt";
            return MyBatisWebMapper.Instance().QueryForObject<int>(stmtid, sysUserQuery);
        }

        public IList<SysUser> GetUnAssignUserList(SysGroupUserQuery sysUserQuery)
        {
            String stmtid = "SysGroupUser.GetUnAssignUserList";
            return MyBatisWebMapper.Instance().QueryForList<SysUser>(stmtid, sysUserQuery, sysUserQuery.PageIndex * sysUserQuery.PageSize, sysUserQuery.PageSize);
        }

        public int GetAssignUserListCnt(SysGroupUserQuery sysUserQuery)
        {
            String stmtid = "SysGroupUser.GetAssignUserListCnt";
            return MyBatisWebMapper.Instance().QueryForObject<int>(stmtid, sysUserQuery);
        }

        public IList<SysUser> GetAssignUserList(SysGroupUserQuery sysUserQuery)
        {
            String stmtid = "SysGroupUser.GetAssignUserList";
            return MyBatisWebMapper.Instance().QueryForList<SysUser>(stmtid, sysUserQuery, sysUserQuery.PageIndex * sysUserQuery.PageSize, sysUserQuery.PageSize);
        }
    }
}
