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
    public class SysOrg : BaseModel
    {
        [Display(Name = "ID")]
        public string OrgId { set; get; }

        [Display(Name = "父节点ID")]
        public string OrgParentId { set; get; }

        [Display(Name = "组织机构名称")]
        public string OrgName { set; get; }

        [Display(Name = "组织机构编码")]
        public string OrgCode { set; get; }

        [Display(Name = "组织机构图标")]
        public string OrgIcon { set; get; }

        [Display(Name = "组织机构图标地址")]
        public string OrgIconPath { set; get; }

        [Display(Name = "组织机构类型")]
        public string OrgType { set; get; }

        [Display(Name = "组织机构有效性")]
        public int OrgIsValid { set; get; }

        [Display(Name = "组织机构编号")]
        public int OrgNo { set; get; }

        [Display(Name = "组织机构备注")]
        public string OrgRemark { set; get; }

        [Display(Name = "组织机构创建人")]
        public string OrgCreateBy { set; get; }

        [Display(Name = "组织机构创建时间")]
        public DateTime? OrgCreateTime { set; get; }

        [Display(Name = "组织机构更新人")]
        public string OrgUpdateBy { set; get; }

        [Display(Name = "组织机构更新时间")]
        public DateTime? OrgUpdateTime { set; get; }
    }

    [Serializable]
    public class SysOrg_MiniUI : SysOrg
    {
        /// <summary>
        /// miniui post _id
        /// </summary>
        public string _id { set; get; }

        /// <summary>
        /// miniui post _pid
        /// </summary>
        public string _pid { set; get; }

        /// <summary>
        /// miniui post _level
        /// </summary>
        public string _level { set; get; }
    }
}
