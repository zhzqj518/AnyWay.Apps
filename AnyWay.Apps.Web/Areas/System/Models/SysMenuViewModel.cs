using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyWay.Apps.Web.Areas.System.Models
{
    public class SysMenuViewModel
    {

    }

    public class SysInitModel
    {
        public SysIndexJsonModel indexJson { set; get; }

        public List<SysMenuJsonModel> menuJson { set; get; }
    }

    public class SysIndexJsonModel
    {
        public string id { set; get; }

        public string text { set; get; }

        public string icon { set; get; }

        public string iconCls { set; get; }

        public string href { set; get; }
    }

    public class SysMenuJsonModel
    {
        //顶部菜单
        public string text { set; get; }

        public string icon { set; get; }

        public string iconCls { set; get; }

        public string href { set; get; }

        public bool isChecked { set; get; }

        //侧边菜单
        public List<SysMenuSlider> children { set; get; }
    }

    public class SysMenuSlider
    {
        //侧边菜单
        public string id { set; get; }

        public string pid { set; get; }

        public string text { set; get; }

        public string icon { set; get; }

        public string iconCls { set; get; }

        public bool isChecked { set; get; }

        public string href { set; get; }

        //左侧树形菜单
        public List<SysMenuTree> children { set; get; }
    }

    public class SysMenuTree
    {
        public string id { set; get; }

        public string pid { set; get; }

        public string text { set; get; }

        public string icon { set; get; }

        public string iconCls { set; get; }

        public string href { set; get; }

        public int sort { set; get; }
    }
}