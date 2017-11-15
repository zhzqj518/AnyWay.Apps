using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System.ViewModel
{
    [Serializable]
    public class SysUserQuery
    {
        public string UserName { set; get; }

        public string UserPwd { set; get; }

        public List<string> UserType { set; get; }

        public string UserIsEmailVerificate { set; get; }

        public string UserEmailVerificationCode { set; get; }

        public string UserIsSMSVerificate { set; get; }

        public string UserSMSVerificationCode { set; get; }

        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public string SortBy { set; get; }

        public string SortOrder { set; get; }
    }
}
