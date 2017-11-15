var mainPlatform = {

    init: function () {
        this._createTopMenu();
        this.bindEvent();
    },

    bindEvent: function () {
        var self = this;
        // 顶部大菜单单击事件
        $(document).on('click', '.pf-nav-item', function () {
            $('.pf-nav-item').removeClass('current');
            $(this).addClass('current');

            // 渲染对应侧边菜单
            var m = $(this).data('sort');
            self._createSiderMenu(SystemMenu[m], m);
        });

        //左侧父级菜单点击事件
        $(document).on('click', '.sider-nav .pf-menu-title', function () {
            //debugger;
            if ($(this).closest('li').hasClass('current')) {
                return;
            }
            $(this).closest('.pf-sider').find('.sider-nav li').removeClass('current');
            $(this).closest('.pf-sider').find('.sider-nav li').find(".sider-nav-s").hide(500);
            $(this).closest('li').addClass('current');
            $(this).closest('li').find(".sider-nav-s").slideDown(500);

            //如果没有子节点
            if ($(this).closest('.no-child').size() > 0) {
                if ($("#main_tabs").tabs('exists', $(this).find('.sider-nav-title').text())) {
                    $("#main_tabs").tabs('select', $(this).find('.sider-nav-title').text());
                    return false;
                }

                //$(this).closest('.no-child').data('href', "https://www.baidu.com/");

                $("#main_tabs").tabs('add', {
                    title: $(this).find('.sider-nav-title').text(),
                    content: '<iframe class="page-iframe" src="' + $(this).closest('.no-child').data('href') + '" frameborder="no" border="no" height="100%" width="100%" scrolling="auto"></iframe>',
                    closable: true
                });
            }
        });

        //左侧菜单收起
        $(document).on('click', '.toggle-icon', function () {
            $(this).closest("#pf-bd").toggleClass("toggle");
            $(window).resize();
        });

        $('.pf-logout a').on('click', function () {
            $.messager.confirm({
                title: '对话框',
                ok: '确定',
                cancel: '取消',
                msg: '你确定要退出系统吗？',
                fn: function (r) {

                    if (r) {
                        window.location.href = 'login.html';

                    }

                }
            });
            $('.messager-window').find('.l-btn-small').eq(0).addClass('l-btn-selected');
        })

        $(document).ready(function () {
            //监听右键事件，创建右键菜单
            $('#main_tabs').tabs({
                onContextMenu: function (e, title, index) {
                    e.preventDefault();
                    if (index > 0) {
                        $('#mm').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        }).data("tabTitle", title);

                        $("#mm").data("option-tab", title);
                    }
                }
            });

            $("#mm").menu({
                onClick: function (item) {
                    switch (item.name) {
                        case "close_current": {
                            $("#main_tabs").tabs("close", $("#mm").data("option-tab"));
                            break;
                        }
                        case "close_all": {
                            var allTabtitle = [];
                            $.each($("#main_tabs").tabs('tabs'), function (i, n) {
                                var opt = $(n).panel('options');
                                if (opt.closable)
                                    allTabtitle.push(opt.title);
                            });

                            for (var i = 0; i < allTabtitle.length; i++) {
                                $("#main_tabs").tabs('close', allTabtitle[i]);
                            }
                            break;
                        }
                        case "close_other": {
                            var allTabtitle = [];
                            $.each($("#main_tabs").tabs('tabs'), function (i, n) {
                                var opt = $(n).panel('options');
                                if (opt.closable && opt.title != $("#mm").data("option-tab")) {
                                    allTabtitle.push(opt.title);
                                }
                            });

                            for (var i = 0; i < allTabtitle.length; i++) {
                                $("#main_tabs").tabs('close', allTabtitle[i]);
                            }
                            break;
                        }
                    }
                }
            });
        });

        $("ul[name='sider-nav-t']").tree({
            onClick: function (node) {
                if (node.children && node.children.length > 0) {
                    return;
                }

                if ($("#main_tabs").tabs('exists', node.text)) {
                    $("#main_tabs").tabs('select', node.text);
                    return;
                }

                //node.href = "https://www.baidu.com/";

                $("#main_tabs").tabs('add', {
                    title: node.text,
                    content: '<iframe class="page-iframe" src="' + node.href + '" frameborder="no" border="no" height="100%" width="100%" scrolling="auto"></iframe>',
                    closable: true
                });
            }
        });
    },

    setMsgts: function (cnt) {
        if (cnt == 0 || !cnt) {
            $("#msgts").removeClass("msgts");
            $("#msgts").html("");
        } else {
            $("#msgts").addClass("msgts");
            $("#msgts").html(cnt);
        }
    },

    _createTopMenu: function () {
        var menuStr = '',
			currentIndex = 0;
        for (var i = 0, len = SystemMenu.length; i < len; i++) {
            menuStr += '<li class="pf-nav-item project" data-sort="' + i + '" data-menu="system_menu_' + i + '">' +
                      '<a href="javascript:;">' +
                          '<span class="iconfont-nav">' +
                          '<i class="iconfont ' + SystemMenu[i].iconCls + '"></i>' +
                          '<span class="pf-nav-title">' + SystemMenu[i].text + '</span>' +
                      '</a>' +
                      '</li>';
            // 渲染当前
            if (SystemMenu[i].checked) {
                currentIndex = i;
                this._createSiderMenu(SystemMenu[i], i);
            }
        }

        $('.pf-nav').html(menuStr);
        $('.pf-nav-item').eq(currentIndex).addClass('current');
        $('.pf-sider[arrindex=' + currentIndex + ']').find('.sider-nav li.current').find(".sider-nav-s").slideDown(500);
    },

    _createSiderMenu: function (menu, index) {
        $('.pf-sider').hide();
        if ($('.pf-sider[arrindex=' + index + ']').size() > 0) {

            $('.pf-sider[arrindex=' + index + ']').show();
            return false;
        };
        var menuStr = '<h2 class="pf-model-name">' +
                    '<span class="iconfont-model">' +
                    '<i class="iconfont ' + menu.iconCls + '"></i>' + '</span>' +
                    '<span class="pf-name">' + menu.text + '</span>' +
                    '<span class="toggle-icon"></span>' +
                '</h2><ul class="sider-nav">';

        if (menu.children.length > 0) {

            for (var i = 0, len = menu.children.length; i < len; i++) {
                var m = menu.children[i],
                    mstr = '';
                var str = '';

                if (m.checked) {
                    if (m.children && m.children.length > 0) {
                        str = '<li class="current">';
                    } else {
                        str = '<li class="current no-child" data-href="' + m.href + '">';
                    }
                } else {
                    if (m.children && m.children.length > 0) {
                        str = '<li>';
                    } else {
                        str = '<li class="no-child" data-href="' + m.href + '">';
                    }
                }
                str += '<a href="javascript:;" class="pf-menu-title">' +
                 '<span class="iconfont sider-nav-icon">' +
                 '<i class="iconfont ' + m.iconCls + '"></i>' + '</span>' +
                 '<span class="sider-nav-title">' + m.text + '</span>' +
                 '<i class="iconfont icon-xiangyou"></i>' + '</a>' + '<ul class="sider-nav-s"><li class="sider-nav-menutree">';

                var childStr = '';

                if (m.children.length > 0) {
                    childStr = this._renderTreeMenu(m.children);
                }

                mstr = str + childStr + '</li></ul></li>';
                menuStr += mstr;
            }
        }

        $('.pf-sider-wrap').append($('<div class="pf-sider" arrindex="' + index + '"></div>').html(menuStr + '</ul>'));
    },

    _renderTreeMenu: function (list) {
        var str = JSON.stringify(list);
        str = str.replace(/"/g, "'")
        var innerHtml = ' <ul name="sider-nav-t" class="easyui-tree" data-options="{data:' + str + '}"></ul>'
        return innerHtml;
    },
};

mainPlatform.init();

mainPlatform.setMsgts(10);