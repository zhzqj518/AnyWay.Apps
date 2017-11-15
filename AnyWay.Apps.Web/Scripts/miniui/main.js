var menuList = new Array(), menuIndex = 0, currentTab = null;;

var mainPlatform = {
    initIndex: function (indexJson) {
        var e = { id: indexJson.id, name: "first", icon: indexJson.icon, iconCls: indexJson.iconCls, text: indexJson.text, url: indexJson.href, showCloseButton: false };

        this.addMainTabs(e);
    },

    initMenu: function () {
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
            //debugger;
            self._createSiderMenu(SystemMenu[m], m);
            $('.pf-sider[arrindex=' + m + ']').find('.sider-nav li.current').find(".sider-nav-s").slideDown(500);
        });

        //左侧父级菜单点击事件
        $(document).on('click', '.sider-nav .pf-menu-title', function () {
            //debugger;
            if (!$(this).closest('li').hasClass('current') || $(this).closest('li').find(".sider-nav-s").css("display") == "none") {
                $(this).closest('.pf-sider').find('.sider-nav li').removeClass('current');
                $(this).closest('.pf-sider').find('.sider-nav li').find(".sider-nav-s").hide(500);
                $(this).closest('li').addClass('current');
                $(this).closest('li').find(".sider-nav-s").slideDown(500);
            }

            //如果没有子节点
            if ($(this).closest('.no-child').size() > 0) {
                //debugger;
                var id = $(this).find('.sider-nav-title-id').text();
                var text = $(this).find('.sider-nav-title').text();
                var url = $(this).closest('.no-child').data('href');
                var icon = $(this).find('.sider-nav-title-icon').text();
                var iconCls = $(this).find('.sider-nav-title-iconCls').text();

                mainPlatform.addMainTabs({ id: id, text: text, url: url, icon: icon, iconCls: iconCls });
            }
        });

        //左侧菜单收起
        $(document).on('click', '.toggle-icon', function () {
            $(this).closest("#pf-bd").toggleClass("toggle");
            $(window).resize();
            mini.layout();
        });

        $(document).ready(function () {
            mini.get("tabsMenu").on("beforeopen", function (e) {
                currentTab = mini.get("main_tabs").getTabByEvent(e.htmlEvent);
                if (!currentTab) {
                    e.cancel = true;
                }
            });

            $("#close_current").on("click", function () {
                if (currentTab.name != "first") {
                    mini.get("main_tabs").removeTab(currentTab);
                }
            });

            $("#close_all").on("click", function () {
                var but = [];
                but.push(mini.get("main_tabs").getTab("first"));
                mini.get("main_tabs").removeAll(but);
            });

            $("#close_other").on("click", function () {
                var but = [currentTab];
                but.push(mini.get("main_tabs").getTab("first"));
                mini.get("main_tabs").removeAll(but);
            });
        });

        $('.pf-modify-pwd a').on('click', function () {
            mini.open({
                title: "修改密码",
                iconCls: "fa-key",
                url: "../Account/ModifyPwd",
                width: "360px",
                height: "255px",
                allowResize: false
            });
        });

        $('.pf-modify-info a').on('click', function () { });

        $('.pf-logout a').on('click', function () {
            mini.confirm("确定退出系统吗？", "确定？",
             function (action) {
                 if (action == "ok") {
                     mini.mask({
                         el: document.body,
                         cls: 'mini-mask-loading',
                         html: "正在退出..."
                     });
                     $.ajax({
                         url: "../Account/UserLogout",
                         type: "post",
                         success: function (text, flag, res) {
                             mini.unmask();
                             var msg = JSON.parse(arguments[2].responseText);
                             if (msg.isSuccess) {
                                 location.href = "../";
                             }
                         },
                         error: function (jqXHR, textStatus, errorThrown) {
                             mini.unmask();
                             mini.alert("退出错误！");
                         }
                     });
                 }
             });
            $('.messager-window').find('.l-btn-small').eq(0).addClass('l-btn-selected');
        });

        $(document).on('click', '.pf-nav-prev,.pf-nav-next', function () {
            if ($(this).hasClass('disabled')) return;
            if ($(this).hasClass('pf-nav-next')) {
                page++;
                $('.pf-nav').stop().animate({ 'margin-top': -70 * page }, 200);
                if (page == pages) {
                    $(this).addClass('disabled');
                    $('.pf-nav-prev').removeClass('disabled');
                } else {
                    $('.pf-nav-prev').removeClass('disabled');
                }

            } else {
                page--;
                $('.pf-nav').stop().animate({ 'margin-top': -70 * page }, 200);
                if (page == 0) {
                    $(this).addClass('disabled');
                    $('.pf-nav-next').removeClass('disabled');
                } else {
                    $('.pf-nav-next').removeClass('disabled');
                }
            }
        })

        $(window).resize(function () {
            $('.tabs-panels').height($("#pf-page").height() - 46);
            $('.panel-body').not('.messager-body').height($(".easyui-dialog").height)
        }).resize();

        var page = 0,
            pages = ($('.pf-nav').height() / 70) - 1;

        if (pages === 0) {
            $('.pf-nav-prev,.pf-nav-next').hide();
        }
    },

    setMsgts: function (cnt) {
        if (cnt == 0 || !cnt) {
            $("#msgts").removeClass("msgts");
            $("#msgts").html("");
            $(".mailtext").html("");
        } else {
            $("#msgts").addClass("msgts");
            $("#msgts").html(cnt);
            $(".mailtext").html(cnt);
        }
    },

    _createTopMenu: function () {
        var menuStr = '',
            currentIndex = 0;
        for (var i = 0, len = SystemMenu.length; i < len; i++) {
            menuStr += '<li class="pf-nav-item project" data-sort="' + i + '" data-menu="system_menu_' + i + '">' +
                      '<a href="javascript:;">' +
                          '<span class="iconfont-nav">';
            if (SystemMenu[i].icon == "iconfont") {
                menuStr += '<i class="iconfont ' + SystemMenu[i].iconCls + '"></i>';
            } else if (SystemMenu[i].icon == "fontawesome") {
                menuStr += '<i class="fa ' + SystemMenu[i].iconCls + '"></i>';
            } else {
                menuStr += '<span class="miniui-icon ' + SystemMenu[i].iconCls + '"></span>';
            }

            menuStr += '<span class="pf-nav-title">' + SystemMenu[i].text + '</span>' +
                      '</a>' +
                      '</li>';
            // 渲染当前
            if (SystemMenu[i].isChecked) {
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
                    '<span class="iconfont-model">';
        if (menu.icon == "iconfont") {
            menuStr += '<i class="iconfont ' + menu.iconCls + '"></i>' + '</span>';
        } else if (menu.icon == "fontawesome") {
            menuStr += '<i class="fa ' + menu.iconCls + '"></i>' + '</span>';
        } else {
            menuStr += '<span class="' + menu.iconCls + '"></span>';
        }
        menuStr += '<span class="pf-name">' + menu.text + '</span>' +
                    '<span class="toggle-icon"></span>' +
                '</h2><ul class="sider-nav">';

        if (menu.children.length > 0) {

            for (var i = 0, len = menu.children.length; i < len; i++) {
                var m = menu.children[i],
                    mstr = '';
                var str = '';

                if (m.isChecked) {
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
                 '<span class="iconfont sider-nav-icon">';
                if (m.icon == "iconfont") {
                    str += '<i class="iconfont ' + m.iconCls + '"></i>' + '</span>';
                } else if (m.icon == "fontawesome") {
                    str += '<i class="fa ' + m.iconCls + '"></i>' + '</span>';
                } else {
                    str += '<span class="' + m.iconCls + '"></span>';
                }
                str += '<span class="sider-nav-title">' + m.text + '</span>' +
                 '<span class="sider-nav-title-id">' + m.id + '</span>' +
                 '<span class="sider-nav-title-icon">' + m.icon + '</span>' +
                 '<span class="sider-nav-title-iconCls">' + m.iconCls + '</span>' +
                 '<i class="iconfont icon-xiangyou"></i>' + '</a>' + '<ul class="sider-nav-s"><li class="sider-nav-menutree">';

                var childStr = '';
                if (m.children.length > 0) {
                    childStr = this._renderTreeMenu(m.children, index);
                }

                mstr = str + childStr + '</li></ul></li>';
                menuStr += mstr;
            }
        }

        $('.pf-sider-wrap').append($('<div class="pf-sider" arrindex="' + index + '"></div>').html(menuStr + '</ul>'));
        mini.parse();
        this._createTreeMenu(index);
    },

    _renderTreeMenu: function (list, index) {
        menuList.push(list);
        var innerHtml = ' <ul name="sider-nav-t" id="sider-nav-t-' + index + '-' + menuIndex + '" class="mini-tree"  showTreeIcon="true" textField="text" idField="text" parentField="pid" resultAsTree="false" iconField="iconCls"></ul>'
        menuIndex++;
        return innerHtml;
    },

    _createTreeMenu: function (index) {
        for (var i = 0; i < menuList.length; i++) {
            mini.get("sider-nav-t-" + index + '-' + i).loadList(menuList[i], "id", "pid");
            mini.get("sider-nav-t-" + index + '-' + i).on("nodeclick", function (e) {
                mainPlatform.addMainTabs({ id: e.node.id, text: e.node.text, url: e.node.href, icon: e.node.icon, iconCls: e.node.iconCls });
            });
        }

        menuList = new Array();
        menuIndex = 0;
    },

    addMainTabs: function (e) {
        var tabname = e.name ? e.name : ("tab$" + e.id);
        var tabs = mini.get("main_tabs");
        var tab = tabs.getTab(tabname);

        if (!tab) {
            tab = {};
            tab.name = tabname;
            if (e.icon == "iconfont") {
                tab.title = '<span style="width:16px;height:16px;"><i class="iconfont ' + e.iconCls + '"></i></span>' + e.text;
            } else {
                tab.title = e.text;
                tab.iconCls = e.iconCls;
            }
            tab.url = e.url;
            if (e.url != null && e.url.indexOf('?') > -1) {
                tab.url += "&menuid=" + e.id;
            } else {
                tab.url += "?menuid=" + e.id;
            }
            tab.showCloseButton = e.showCloseButton != undefined ? e.showCloseButton : true;
            tabs.addTab(tab);
        }
        tabs.activeTab(tab);
    }
};