using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
    [Serializable]
    public class UserLoginModel
    {
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserLoginName { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { set; get; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificateCode { set; get; }
    }

    public class UserLoginResult
    {
        public bool IsSuccess { set; get; }

        public SysUser User { set; get; }

        public string ErrorMsg { set; get; }
    }
}
