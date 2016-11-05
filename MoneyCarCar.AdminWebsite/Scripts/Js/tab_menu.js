$(function () {
    InitLeftMenu();
    tabClose();
    tabCloseEven();
})
function showMask() {
    $("#mainPanle").mask();
}
function hideMask() {
    $("#mainPanle").mask("hide");
}
//初始化左侧
function InitLeftMenu() {
    var menulist = '';
    for (var i = 0; i < _Menus.Menus.length; i++) {
        var m_Menus = _Menus.Menus[i];
        menulist += '<div title="' + m_Menus.MenuName + '" iconCls="icon ' + m_Menus.icon + '" style="overflow:auto;">';
        menulist += '<ul>';
        if (m_Menus.Menus != null && m_Menus.Menus.length > 0) {
            for (var j = 0; j < m_Menus.Menus.length; j++) {
                var o = m_Menus.Menus[j];
                if (o != null)
                    menulist += '<li style="padding: 5px;list-style-type:none;"><div><a ref="' + o.MenuId + '" href="#" code="A" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.MenuName + '</span></a></div></li> ';
            }
        }
        menulist += '</ul>';
        menulist += '</div>';
    }
    $("#nav").html(menulist);
    $('#nav ul li a').click(function () {//当单击菜单某个选项时，在右边出现对用的内容       
        var tabTitle = $(this).children('.nav').text();//获取超链里span中的内容作为新打开tab的标题
        var url = $(this).attr("rel");
        var MenuId = $(this).attr("ref");//获取超链接属性中ref中的内容
        var icon = getIcon(MenuId, icon);
        try {
            //增加tab
            addTab(tabTitle, url, icon);
        } catch (e) {
            // $.messager.alert("提示", e.message);
        }

        $('#nav ul li div').removeClass("selected");
        $(this).parent().addClass("selected");
        showMask();
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });
    //为id为nav的div增加手风琴效果，并去除动态滑动效果
    $("#nav").accordion({
        animate: true,
        border: false
        //fit: true
    })

    //选中第一个
    var panels = $('#nav').accordion('panels');
    var t = panels[0].panel('options').title;
    $('#nav').accordion('select', t);
}
//获取左侧导航的图标
function getIcon(MenuId) {
    var icon = 'icon ';
    for (var i = 0; i < _Menus.Menus.length; i++) {
        var n = _Menus.Menus[i];
        if (n.Menus != null && n.Menus.length > 0) {
            try {
                for (var j = 0; j < n.Menus.length; j++) {
                    var o = n.Menus[j];
                    if (o.MenuId == MenuId) {
                        icon += o.icon;
                    }
                }
            } catch (e) {
                //alert(e.message);
            }
        }
    }
    return icon;
}

function addTab(subtitle, url, icon) {
    showMask();
    var noExists = !$('#tabs').tabs('exists', subtitle);
    if (noExists) {
        var t = new Date().toString();
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url, t),
            closable: true,
            icon: icon
        });
        $("iframe#f_" + t).load(function () {
            // hideMask();
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        $('#mm-tabupdate').click();
    }
    tabClose();
}

function createFrame(url, t) {
    //var t = new Date().toString();
    url = url + "?_time=" + t;
    var s = '<iframe scrolling="auto" id="f_' + t + '" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}
//绑定右键菜单事件
function tabCloseEven() {
    //刷新
    $('#mm-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');

        if (url != null) {
            $('#tabs').tabs('update', {
                tab: currTab,
                options: {
                    content: createFrame(url)
                }
            })
        }
    })
    //关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    })
    //全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            //msgShow('系统提示','后边没有啦~~','error');
            //alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            //alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    //退出
    $("#mm-exit").click(function () {
        $('#mm').menu('hide');
    })
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}
