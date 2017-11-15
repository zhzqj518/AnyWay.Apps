//mini.mask({
//    el: document.body,
//    cls: 'mini-mask-loading',
//    html: '正在加载菜单...'
//});

var SystemMenu = new Array();

$.ajax({
    url: "../System/SysAuthority/GetSysRoleMenu",
    type: "get",
    dataType: "json",
    success: function (data) {
        SystemMenu = data.menuJson;
        if (SystemMenu.length == 0) {
            mini.alert("您尚未获取任何权限！请联系管理员！");
        } 
        mainPlatform.initMenu();
        $("#loading_msg").html("菜单初始化完毕...");
        mainPlatform.initIndex(data.indexJson);
        $("#loading_msg").html("首页初始化完毕...");
        mainPlatform.setMsgts(0);
        $(".init-loading").hide();
        //mini.unmask(document.body);
    },
    error: function (e) {
        $(".init-loading").hide();
        var status = e.status;
        var responseText = mini.decode(e.responseText);
        mini.alert("错误发生在:" + responseText.ErrorUrl + "<br/>原因:" + responseText.Message);
        //mini.unmask(document.body);
    }
});

//var SystemMenu = [{
//    text: '系统管理',
//    iconCls: 'icon-system-manager',
//    isChecked: true,
//    children: [{
//        text: '技术文档',
//        iconCls: 'icon-technical-doc',
//        href: '',
//        isChecked: true,
//        children: [{
//            id: 0,
//            pid:-1,
//            text: 'iconfont-unicode',
//            iconCls: "icon-rightarrow",
//            href: '/Content/fonts/demo_unicode.html'
//        }, {
//            id: 1,
//            pid: -1,
//            text: 'iconfont-symbol',
//            iconCls: "icon-rightarrow",
//            href: '/Content/fonts/demo_symbol.html'
//        }, {
//            id: 2,
//            pid: -1,
//            text: "iconfont-fontclass",
//            iconCls: "icon-rightarrow",
//            href: '/Content/fonts/demo_fontclass.html'
//        }]
//    }, {
//        text: '用户组织机构管理',
//        iconCls: 'icon-zuzhijiagou',
//        href: '',
//        children: [{
//            text: '用户信息管理',
//            iconCls: "icon-rightarrow",
//            href: ''
//        }, {
//            text: '组织机构管理',
//            iconCls: "icon-rightarrow",
//            href: ''
//        }]
//    },
//    {
//        id: 100,
//        pid:0,
//        text: '测试',
//        iconCls: 'icon-zuzhijiagou',
//        href: 'https://wwww.baidu.com',
//        children: []
//    }]
//}, {
//    text: '主数据',
//    iconCls: 'icon-dingdanhetong',
//    children: [{
//        text: '数据信息',
//        iconCls: 'icon-daishouhuo;',
//        isChecked: true,
//        children: [{
//            text: '数据管理',
//            iconCls: "icon-rightarrow",
//            href: '',
//            isChecked: true
//        }, {
//            text: '企业荣誉',
//            iconCls: "icon-rightarrow",
//            href: ''
//        }, {
//            text: '组织架构',
//            iconCls: "icon-rightarrow",
//            href: 'index.html'
//        }, {
//            text: '自定义',
//            iconCls: "icon-rightarrow",
//            href: 'index.html'
//        }]
//    }, {
//        text: '数据表',
//        iconCls: 'icon-bianji',
//        href: 'basic_info.html',
//        children: []
//    }]
//}];