(function () {
    function trim(el) {
        return (''.trim) ? el.val().trim() : $.trim(el.val());
    }
    $.srw = function (config) {
        var fields = [], item;
        var funs = false;
        for (item in config) {
            if (!processField(config[item], item)) {
                funs = false;
                break;
            }
            funs = true;
        }
        function processField(opts, selector) {
            //radio属性特殊，提前判断
            var R_D = selector.split('#');
            if (R_D['0'] != '') {
                if (R_D['0'] == 'radio') {
                    selector = 'input:radio[name="' + R_D['1'] + '"]:checked';
                } else if (R_D['0'] == 'checkbox') {
                    selector = 'input:checkbox[name="' + R_D['1'] + '"]:checked';
                }
            } else {
                selector = '#' + R_D['1'];
            }
            if ($(selector) && opts.nl == true) {	//是否允许为空
                if ($(selector).val() == '' || typeof ($(selector).val()) == 'undefined') {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                }
            }

            if ($(selector) && opts.eq && opts.eq != "") {	//两次值是否一致
                if ($(selector).val() != $(opts.eq).val()) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                }
            }

            if ($(selector) && opts.len) {	//长度
                var tlen = opts.len.split(',');
                if (tlen['0'] && tlen['1'] && $(selector).val().length < tlen['0'] && $(selector).val().length > tlen['1']) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                } else if (tlen['0'] && $(selector).val().length < tlen['0']) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                } else if (tlen['1'] && $(selector).val().length > tlen['1']) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                }
            }

            if ($(selector) && opts.daxiao) {	//幅度 大于或者小于
                var tdaxiao = opts.daxiao.split(',');
                var myval = parseFloat($(selector).val());
                if (isNaN(myval)) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                }
                if (tdaxiao['0'] && tdaxiao['1'] && myval < tdaxiao['0'] && myval > tdaxiao['1']) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                } else if (tdaxiao['0'] && myval < tdaxiao['0']) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                } else if (tdaxiao['1'] && myval > tdaxiao['1']) {
                    art.dialog({ content: opts.nl_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                }
            }

            if ($(selector) && opts.reg) {	//正则验证
                var tlen = opts.reg.split(',');
                var regR = false;
                for (regd in tlen) {
                    if (tlen[regd] && regexEnum && regexEnum[tlen[regd]]) {
                        regstr = eval("regexEnum." + tlen[regd]);
                        if (regstr == undefined || regstr == "") {
                            art.dialog({ content: '校验配置错误', ok: function () { this.close(); return false; } });
                            return false;
                        }
                        if (new RegExp(regstr).test($(selector).val())) {	//有一次验证通过则为通过;
                            regR = true;
                        }
                    }
                }
                if (!regR) {
                    art.dialog({ content: opts.reg_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                }
            }

            if ($(selector) && opts.fun) {	//自定义函数验证
                if (!eval(opts.fun + '()')) {
                    art.dialog({ content: opts.fun_err, lock: true, ok: function () { this.close(); return false; } });
                    $(selector).focus();
                    return false;
                }
            }

            return true;
        }
        return funs;
    };
})(this.jQuery);

//倒计时[发送短信，发送邮件]
var djs = {
    node: null,
    count: 30,
    start: function () {
        if (this.count > 0) {
            this.node.val(this.count-- + '秒后重新发送');
            var _this = this;
            setTimeout(function () {
                _this.start();
            }, 1000);
        } else {
            this.node.prop({ disabled: false });
            this.node.val("验证再次发送");
            this.count = 30;
        }
    },
    init: function (noded, count) {
        var node = $('#' + noded);
        this.count = count;
        this.node = node;
        this.node.prop({ disabled: true });
        this.start();
    }
}

function code(Did, codeurl) {
    var SID = Math.round(Math.random() * (9999 - 1000) + 1000);
    //document.getElementById(Did).src = codeurl + '&fp=' + SID;
    document.getElementById(Did).src = codeurl + '/' + SID;
}