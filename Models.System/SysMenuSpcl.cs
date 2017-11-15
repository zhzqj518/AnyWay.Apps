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
    public class SysMenuSpcl : BaseModel
    {
        [Display(Name = "主键")]
        public string MenuId { get; set; }

        [Display(Name = "菜单名称")]
        public string MenuName { get; set; }

        [Display(Name = "菜单地址")]
        public string MenuLink { get; set; }

        [Display(Name = "菜单图标")]
        public string MenuIcon { get; set; }

        [Display(Name = "菜单图标地址")]
        public string MenuIconPath { get; set; }

        [Display(Name = "菜单是否子节点")]
        public int MenuIsLeaf { get; set; }

        [Display(Name = "菜单是否可用")]
        public int MenuIsValid { get; set; }

        [Display(Name = "菜单编号")]
        public int MenuNo { get; set; }

        [Display(Name = "菜单备注")]
        public string MenuRemark { get; set; }

        [Display(Name = "菜单父Id")]
        public string MenuParentId { get; set; }

        [Display(Name = "菜单操作")]
        public string MenuOperation { get; set; }

        [Display(Name = "菜单创建人")]
        public string MenuCreateBy { get; set; }

        [Display(Name = "菜单创建时间")]
        public DateTime? MenuCreateTime { get; set; }

        [Display(Name = "菜单更新人")]
        public string MenuUpdateBy { get; set; }

        [Display(Name = "菜单更新时间")]
        public DateTime? MenuUpdateTime { get; set; }
    }

    public class SysMenuSpcl_MiniUI : SysMenuSpcl
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
