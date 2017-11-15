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
    public class SysUserBizManager : BaseBizManager, ISysUserBizManager
    {

        [Dependency]
        public ISysUserRepository resp { get; set; }

        public IList<SysUser> GetList(SysUserQuery sysUserQuery)
        {
            ModelQuery modelQuery = new ModelQuery();
            modelQuery.PageSize = 0;
            modelQuery.QueryParam = sysUserQuery;

            return resp.GetList(modelQuery);
        }

        public TotalData GetTotalData(SysUserQuery sysUserQuery)
        {
            TotalData totalData = new TotalData();

            ModelQuery modelQuery = new ModelQuery();
            modelQuery.PageIndex = sysUserQuery.PageIndex;
            modelQuery.PageSize = sysUserQuery.PageSize;
            modelQuery.QueryParam = sysUserQuery;

            totalData.total = resp.GetListCnt(modelQuery);
            totalData.data = resp.GetList(modelQuery);

            return totalData;
        }

        public ResultInfo SaveUserChanges(List<SysUser> addList, List<SysUser> modifyList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            foreach (SysUser entity in addList)
            {
                entity.UserId = Guid.NewGuid().ToString("N");
                resp.Insert(entity);
            }

            foreach (SysUser entity in modifyList)
            {
                SysUser existModel = resp.GetById(entity);
                if (existModel == null)
                {
                    resultInfo.isSuccess = false;
                    resultInfo.ErrorMsgList.Add(string.Concat("用户名:", entity.UserName, ",数据中不存在！"));
                    continue;
                }
                if (existModel.UserUpdateTime != entity.UserUpdateTime)
                {
                    if (existModel.UserUpdateTime != null && entity.UserUpdateTime != null)
                    {
                        if (existModel.UserUpdateTime.Value.Subtract(entity.UserUpdateTime.Value).TotalSeconds > 1)
                        {
                            resultInfo.isSuccess = false;
                            resultInfo.ErrorMsgList.Add(string.Concat("用户名:", entity.UserName, ",数据在您操作之前已被修改！"));
                            continue;
                        }
                    }
                    else
                    {
                        resultInfo.isSuccess = false;
                        resultInfo.ErrorMsgList.Add(string.Concat("用户名:", entity.UserName, ",数据在您操作之前已被修改！"));
                        continue;
                    }
                }
                //一下几项不更新
                entity.UserTicketID = existModel.UserTicketID;
                entity.UserEmailVerificationCode = existModel.UserEmailVerificationCode;
                entity.UserSMSVerificationCode = existModel.UserSMSVerificationCode;
                entity.UserCreateBy = existModel.UserCreateBy;
                entity.UserCreateTime = existModel.UserCreateTime;

                entity.UserUpdateTime = DateTime.Now;
                resp.Update(entity);
            }

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }

        public UserLoginResult UserLogin(UserLoginModel userLoginModel)
        {
            UserLoginResult result = new UserLoginResult();

            SysUser user = resp.GetUserByLogin(userLoginModel);

            if (user == null)
            {
                result.IsSuccess = false;
                result.User = new SysUser();
                result.ErrorMsg = "未找到用户";
                return result;
            }

            if (user.UserPwd != userLoginModel.UserPwd)
            {
                result.IsSuccess = false;
                result.User = new SysUser();
                result.ErrorMsg = "密码错误";
                return result;
            }

            result.IsSuccess = true;
            result.User = user;

            //生成凭据，更新数据库
            user.UserTicketID = Guid.NewGuid().ToString("N");
            resp.Update(user);

            return result;
        }
    }
}
