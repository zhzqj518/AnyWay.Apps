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
    public class SysGroupUser : BaseModel
    {
        [Display(Name = "用户组ID")]
        public string GroupId { get; set; }

        [Display(Name = "用户ID")]
        public string UserId { set; get; }
    }
}
