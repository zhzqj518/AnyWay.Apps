using AnyWay.Apps.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System
{
    [Serializable]
    public class SysUser : BaseModel
    {
        [Display(Name = "主键")]
        public string UserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { set; get; }

        [Display(Name = "用户登录名")]
        public string UserLoginName { get; set; }

        [Display(Name = "单点登录用户名")]
        public string UserSSOName { get; set; }

        [Display(Name = "组织机构")]
        public string UserOrgID { get; set; }

        [Display(Name = "密码")]
        public string UserPwd { get; set; }

        [Display(Name = "性别")]
        public string UserSex { get; set; }

        [Display(Name = "邮箱")]
        public string UserMail { get; set; }

        [Display(Name = "手机")]
        public string UserMobile { get; set; }

        [Display(Name = "座机")]
        public string UserTel { get; set; }

        [Display(Name = "用户类型")]
        public string UserType { get; set; }

        [Display(Name = "票据编号")]
        public string UserTicketID { get; set; }

        [Display(Name = "是否需要邮箱验证")]
        public int UserIsEmailVerificate { get; set; }

        [Display(Name = "邮箱验证码")]
        public string UserEmailVerificationCode { get; set; }

        [Display(Name = "是否需要短信验证")]
        public int UserIsSMSVerificate { get; set; }

        [Display(Name = "短信验证码")]
        public string UserSMSVerificationCode { get; set; }

        [Display(Name = "备注")]
        public string UserRemark { get; set; }

        [Display(Name = "有效")]
        public int UserIsValid { get; set; }

        [Display(Name = "创建人")]
        public string UserCreateBy { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? UserCreateTime { get; set; }

        [Display(Name = "更新人")]
        public string UserUpdateBy { get; set; }

        [Display(Name = "更新时间")]
        public DateTime? UserUpdateTime { get; set; }
    }
}
