////////////////////////////////////////上下左右滑动
function getOffset(e) {
    var target = e.target;
    if (target.offsetLeft == undefined) {
        target = target.parentNode;
    }
    var pageCoord = getPageCoord(target);
    var eventCoord = {
        x: window.pageXOffset + e.clientX,
        y: window.pageYOffset + e.clientY
    };
    var offset = {
        offsetX: eventCoord.x - pageCoord.x,
        offsetY: eventCoord.y - pageCoord.y
    };
    return offset;
}

function getPageCoord(element) {
    var coord = {
        x: 0,
        y: 0
    };//0.125     4     0.1250    4    5+1
    while (element) {
        coord.x += element.offsetLeft;
        coord.y += element.offsetTop;
        element = element.offsetParent;
    }
    return coord;
}
String.prototype.fixed = function (num) {
    var tValue = this;
    var index = tValue.indexOf(".");
    if (index < 0) {
        tValue = tValue + ".";
        for (var i = 0; i < num; i++) {
            tValue = tValue + "0";
        }
        index = tValue.indexOf(".");
    }
    if (tValue.length < index + num + 1) {
        for (var i = tValue.length; i < index + num + 1; i++) {
            tValue = tValue + "0";
        }
    }
    return tValue.substring(0, index + num + 1);
}

$(document).ready(function () {
    $(".products_items .products_index_list").hover(function (e) {
        var _this = $(this), //闭包
        _desc = _this.find(".products_index_list_info").stop(true),
        width = _this.width(), //取得元素宽
        height = _this.height(), //取得元素高
        left = (e.offsetX == undefined) ? getOffset(e).offsetX : e.offsetX, //从鼠标位置，得到左边界，利用修正ff兼容的方法
        top = (e.offsetY == undefined) ? getOffset(e).offsetY : e.offsetY, //得到上边界
        right = width - left, //计算出右边界
        bottom = height - top, //计算出下边界
        rect = {}, //坐标对象，用于执行对应方法。
        _min = Math.min(left, top, right, bottom), //得到最小值
        _out = e.type == "mouseleave", //是否是离开事件
        spos = {}; //起始位置

        rect[left] = function (epos) { //鼠从标左侧进入和离开事件
            spos = { "left": -width, "top": 0 };
            if (_out) {
                _desc.animate(spos, "fast"); //从左侧离开
            } else {
                _desc.css(spos).animate(epos, "fast"); //从左侧进入
            }
        };

        rect[top] = function (epos) { //鼠从标上边界进入和离开事件
            spos = { "top": -height, "left": 0 };
            if (_out) {
                _desc.animate(spos, "fast"); //从上面离开
            } else {
                _desc.css(spos).animate(epos, "fast"); //从上面进入
            }
        };

        rect[right] = function (epos) { //鼠从标右侧进入和离开事件
            spos = { "left": left, "top": 0 };
            if (_out) {
                _desc.animate(spos, "fast"); //从右侧成离开
            } else {
                _desc.css(spos).animate(epos, "fast"); //从右侧进入
            }
        };

        rect[bottom] = function (epos) { //鼠从标下边界进入和离开事件
            spos = { "top": height, "left": 0 };
            if (_out) {
                _desc.animate(spos, "fast"); //从底部离开
            } else {
                _desc.css(spos).animate(epos, "fast"); //从底部进入
            }
        };

        rect[_min]({ "left": 0, "top": 0 }); // 执行对应边界 进入/离开 的方法

    });

});


////////////////////////////////////////分享时刻



////////////////////////////////////////表单数量加减
$.fn.iVaryVal = function (iSet, CallBack) {
    /*
	 * Minus:点击元素--减小
	 * Add:点击元素--增加
	 * Input:表单元素
	 * Min:表单的最小值，非负整数
	 * Max:表单的最大值，正整数
	 */
    iSet = $.extend({ Minus: $('.J_minus'), Add: $('.J_add'), Input: $('.J_input'), Min: 0, Max: 10000 }, iSet);
    var C = null, O = null;
    //插件返回值
    var $CB = {};
    //增加
    iSet.Add.each(function (i) {
        $(this).click(function () {
            O = parseInt(iSet.Input.eq(i).val());
            (O + 1 <= iSet.Max) || (iSet.Max == null) ? iSet.Input.eq(i).val(O + 1) : iSet.Input.eq(i).val(iSet.Max);
            //输出当前改变后的值
            $CB.val = iSet.Input.eq(i).val();
            $CB.index = i;



            //回调函数
            if (typeof CallBack == 'function') {
                CallBack($CB.val, $CB.index);

                if ($CB.val == iSet.Max) {
                    $.sticky('输入的数量超过最大值', { speed: 'fast', autoclose: 5000 });
                }
            }
            //hello mall 价格计算

            var arp = $('#investmentinterest').val() * 0.01;//利率
            var sharemoney = $('#sharemoney').val();//单份金额
            var buyCount = $CB.val;//购买份数
            var days = $('#days').val();//天数

            $('#investmentmoney').html((sharemoney * buyCount).toString().fixed(2));//投资总金额
            var daymoney = (sharemoney * buyCount * arp / 365).toString().fixed(2) * days;//预期收益
            $('#daymoney').html(daymoney.toFixed(2));
            //提示购买数量
            if ($CB.val > 2) {
                $('.unit_tips').show();
            }
        });
    });
    //减少
    iSet.Minus.each(function (i) {
        $(this).click(function () {
            O = parseInt(iSet.Input.eq(i).val());
            O - 1 < iSet.Min ? iSet.Input.eq(i).val(iSet.Min) : iSet.Input.eq(i).val(O - 1);
            $CB.val = iSet.Input.eq(i).val();
            $CB.index = i;
            //回调函数
            //回调函数
            if (typeof CallBack == 'function') {
                CallBack($CB.val, $CB.index);
            }
            var arp = $('#investmentinterest').val() * 0.01;//利率
            var sharemoney = $('#sharemoney').val();//单份金额
            var buyCount = $CB.val;//购买份数
            var days = $('#days').val();//天数

            $('#investmentmoney').html((sharemoney * buyCount).toString().fixed(2));//投资总金额
            var daymoney = (sharemoney * buyCount * arp / 365).toString().fixed(2) * days;//预期收益
            $('#daymoney').html(daymoney.toFixed(2));//预期收益
            if ($CB.val < 3) {
                $('.unit_tips').hide();
            }
        });
    });
    //手动
    iSet.Input.bind({
        'click': function () {
            O = parseInt($(this).val());
            $(this).select();
        },
        'keyup': function (e) {
            if ($(this).val() != '') {
                C = parseInt($(this).val());
                //非负整数判断
                if (/^[1-9]\d*|0$/.test(C)) {
                    $(this).val(C);
                    O = C;
                } else {
                    $(this).val(O);
                }
            }
            //键盘控制：上右--加，下左--减
            if (e.keyCode == 38 || e.keyCode == 39) {
                iSet.Add.eq(iSet.Input.index(this)).click();
            }
            if (e.keyCode == 37 || e.keyCode == 40) {
                iSet.Minus.eq(iSet.Input.index(this)).click();
            }
            //输出当前改变后的值
            $CB.val = $(this).val();
            $CB.index = iSet.Input.index(this);
            //回调函数
            if (typeof CallBack == 'function') {
                CallBack($CB.val, $CB.index);
            }
            //parseInt($CB.val) * (parseInt($('#investmentinterest').val())* 0.01)

            if (O > iSet.Max) {
                $(this).val(iSet.Max);
                $.sticky('输入的数量超过最大值', { speed: 'fast', autoclose: 5000 })
            }

            var arp = $('#investmentinterest').val() * 0.01;//利率
            var sharemoney = $('#sharemoney').val();//单份金额
            var buyCount = $CB.val;//购买份数

            $('#investmentmoney').html((sharemoney * buyCount).toString().fixed(2));//投资总金额
            var daymoney = (sharemoney * buyCount * arp / 365).toString().fixed(2) * days;//预期收益
            $('#daymoney').html(daymoney.toFixed(2));//预期收益

        },
        'blur': function () {
            $(this).trigger('keyup');
            if ($(this).val() == '') {
                $(this).val(O);
                $('.J_minus').addClass("xxx");
            }

            //提示购买数量
            if ($CB.val > 2) {
                $('.unit_tips').show();
            } else {
                $('.unit_tips').hide();
            }

            //判断输入值是否超出最大最小值
            if (iSet.Max) {
                if (O > iSet.Max) {
                    $(this).val(iSet.Max);
                    $.sticky('输入的数量超过最大值', { speed: 'fast', autoclose: 5000 })
                }
            }
            if (O < iSet.Min) {
                $(this).val(iSet.Min);
            }
            //输出当前改变后的值
            $CB.val = $(this).val();
            $CB.index = iSet.Input.index(this);
            //回调函数
            if (typeof CallBack == 'function') {
                CallBack($CB.val, $CB.index);
            }
            var arp = $('#investmentinterest').val() * 0.01;//利率
            var sharemoney = $('#sharemoney').val();//单份金额
            var buyCount = $CB.val;//购买份数

            $('#investmentmoney').html((sharemoney * buyCount).toString().fixed(2));//投资总金额
            var daymoney = (sharemoney * buyCount * arp / 365).toString().fixed(2) * days;//预期收益
            $('#daymoney').html(daymoney.toFixed(2));//预期收益
        }
    });
}
