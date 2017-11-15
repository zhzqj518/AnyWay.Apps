var SystemMenu = [{
    text: '系统管理',
    iconCls: 'icon-system-manager',
    checked: true,
    children: [{
        text: '技术文档',
        iconCls: 'icon-technical-doc',
        href: '',
        checked: true,
        children: [{
            text: 'iconfont-unicode',
            iconCls: "icon-rightarrow",
            href: '/Content/fonts/demo_unicode.html'
        }, {
            text: 'iconfont-symbol',
            iconCls: "icon-rightarrow",
            href: '/Content/fonts/demo_symbol.html'
        }, {
            text: "iconfont-fontclass",
            iconCls: "icon-rightarrow",
            href: '/Content/fonts/demo_fontclass.html'
        }]
    }, {
        text: '用户组织机构管理',
        iconCls: 'icon-zuzhijiagou',
        href: '',
        children: [{
            text: '用户信息管理',
            iconCls: "icon-rightarrow",
            href: ''
        }, {
            text: '组织机构管理',
            iconCls: "icon-rightarrow",
            href: ''
        }]
    }]
}, {
    text: '主数据',
    iconCls: 'icon-dingdanhetong',
    children: [{
        text: '数据信息',
        iconCls: 'icon-daishouhuo;',
        checked: true,
        children: [{
            text: '数据管理',
            iconCls: "icon-rightarrow",
            href: '',
            checked: true
        }, {
            text: '企业荣誉',
            iconCls: "icon-rightarrow",
            href: ''
        }, {
            text: '组织架构',
            iconCls: "icon-rightarrow",
            href: 'index.html'
        }, {
            text: '自定义',
            iconCls: "icon-rightarrow",
            href: 'index.html'
        }]
    }, {
        text: '数据表',
        iconCls: 'icon-bianji',
        href: 'basic_info.html',
        children: []
    }]
}];