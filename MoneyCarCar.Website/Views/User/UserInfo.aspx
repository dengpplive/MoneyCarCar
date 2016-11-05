<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script src="/Contents/js/area.js" type="text/javascript"></script>
    <script src="/Contents/js/jquery.SuperSlide.2.1.1.source.js" type="text/javascript"></script>
    <script src="/Contents/js/Validform_v5.3.2_min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //修改密码
        function editloginpass() {
            art.dialog({
                title: "错误",
                lock: true,
                opacity: 0.2,
                fixed: true,
                content: "看看啥效果",
                ok: function () {
                    this.close();
                    return false;
                }
            });
            return false;
            $.post("/Submit/EditLoginPass", $("#loginfm").serialize(), function (data) {
                if (data.error == '') {
                    var list = art.dialog.list;
                    for (var i in list) {
                        list[i].close();
                    };
                    art.dialog({
                        content: data.msg,
                        close: function () { location.reload(); }
                    });
                } else {
                    art.dialog({ content: data.msg });
                }
                $('#token').val(data.token);
            }, 'json');

        }

        //修改身份信息弹窗
        function sfrz_form_read() {
            art.dialog({
                lock: true,
                opacity: 0.2,
                fixed: true,
                padding: 0,
                content: document.getElementById('sfrz_form_read'),
                init: function () {
                    var that = this, i = 10;
                    var fn = function () {
                        that.title(i + '秒后自动关闭');
                        !i && that.close();
                        i--;
                    };
                    timer = setInterval(fn, 1000);
                    fn();
                },
                close: function () {
                    sfrz_form()
                    clearInterval(timer);
                },
                cancel: function () {
                    return false;
                }
            });
            $(".aui_buttons button").hide();
        }

        function sfrz_form() {
            art.dialog({
                title: "添加身份信息",
                lock: true,
                opacity: 0.2,
                fixed: true,
                padding: 0,
                content: document.getElementById('sfrz_form')
            });
        }

        function sfrz_form1() {
            art.dialog({
                title: "提示",
                lock: true,
                opacity: 0.2,
                fixed: true,
                padding: 0,
                content: document.getElementById('sfrz_form_1')
            });
        }

        //修改登录密码弹窗
        function edit_login_form() {
            art.dialog({
                title: "修改交易密码",
                lock: true,
                opacity: 0.2,
                fixed: true,
                padding: 0,
                content: document.getElementById('edit_login_form')
            });
        }

        //表单验证
        $(function () {
            //tips
            $('.yuyin_tips').poshytip({ className: 'tip_min', alignTo: 'target', alignX: 'center', alignY: 'top', offsetX: 0, offsetY: 6, showTimeout: 100, allowTipHover: false, });

            $.extend($.Datatype, {
                //中文
                "zh2-20": /^([\u4E00-\u9FA5\uf900-\ufa2d]){2,20}$/,
                "e4-4": /^[0-9a-zA-Z]{1}\w{3,3}$/,
            });

            $(".phone_step_1_form").Validform({
                tiptype: 3,
            });
            $(".phone_step_2_form").Validform({
                tiptype: 3,
            });

            $(".trde_form").Validform({
                tiptype: 3,
            });

            $(".edit_login_form").Validform({
                tiptype: 3,
            });

            $(".sfrz_form").Validform({
                tiptype: 3,
                datatype: {
                    //身份证验证
                    "idcard": function (gets, obj, curform, datatype) {
                        var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];// 加权因子;
                        var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2];// 身份证验证位值，10代表X;

                        if (gets.length == 15) {
                            return isValidityBrithBy15IdCard(gets);
                        } else if (gets.length == 18) {
                            var a_idCard = gets.split("");// 得到身份证数组   
                            if (isValidityBrithBy18IdCard(gets) && isTrueValidateCodeBy18IdCard(a_idCard)) {
                                return true;
                            }
                            return false;
                        }
                        return false;

                        function isTrueValidateCodeBy18IdCard(a_idCard) {
                            var sum = 0; // 声明加权求和变量   
                            if (a_idCard[17].toLowerCase() == 'x') {
                                a_idCard[17] = 10;// 将最后位为x的验证码替换为10方便后续操作   
                            }
                            for (var i = 0; i < 17; i++) {
                                sum += Wi[i] * a_idCard[i];// 加权求和   
                            }
                            valCodePosition = sum % 11;// 得到验证码所位置   
                            if (a_idCard[17] == ValideCode[valCodePosition]) {
                                return true;
                            }
                            return false;
                        }

                        function isValidityBrithBy18IdCard(idCard18) {
                            var year = idCard18.substring(6, 10);
                            var month = idCard18.substring(10, 12);
                            var day = idCard18.substring(12, 14);
                            var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
                            // 这里用getFullYear()获取年份，避免千年虫问题   
                            if (temp_date.getFullYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
                                return false;
                            }
                            return true;
                        }

                        function isValidityBrithBy15IdCard(idCard15) {
                            var year = idCard15.substring(6, 8);
                            var month = idCard15.substring(8, 10);
                            var day = idCard15.substring(10, 12);
                            var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
                            // 对于老身份证中的你年龄则不需考虑千年虫问题而使用getYear()方法   
                            if (temp_date.getYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
                                return false;
                            }
                            return true;
                        }

                    }

                }

            });
        })

        //身份证验证
        function yztruename() {
            $.post("/Submit/AuthenticateIDCard/", {
                RealName: $("#truename").val(),
                IDCard: $("#idcard").val(),
                Address: $("#s_province").val() + $("#s_city").val() + $("#s_county").val(),
                Vcode: $("#txtResetCode").val()
            }, function (data) {
                if (data.IsSeccess) {
                    var list = art.dialog.list;
                    for (var i in list) {
                        list[i].close();
                    };
                    if (data.ErrorCode == -1) {
                        art.dialog({
                            content: data.msg,
                            close: function () { location.href = "/Home/Login"; },
                            ok: function () {
                                location.href = "/Home/Login";
                                return false;
                            }
                        });
                    } else if (data.ErrorCode == -2) {
                        art.dialog({
                            content: data.msg,
                            close: function () { location.href = "/Home/Login"; },
                            ok: function () {
                                location.href = "/Home/Login";
                                return false;
                            }
                        });
                    } else {
                        ToPayForm(data);
                    }
                } else {
                    art.dialog({
                        content: data.msg,
                        ok: function () {
                            this.close();
                            return false;
                        }
                    });
                }
            }, 'json');

        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        SystemUsers user = (SystemUsers)ViewBag.User;
    %>
    <div class="right">
        <div class="user_right_content"> 
		    <h2 class="user_plate_title">个人信息
                <%if(user.IDNumberIsAuthenticate){%>
			    <span class="title_wd_ico">实名认证已完成</span>
                <%}else{%>
                <span class="title_hot_ico">您还没有进行实名认证</span>
                <%}%>
            </h2>
		    <div class="user_plate_box">
        	    <ul class="userinfo_list">
                    <%if(user.IDNumberIsAuthenticate){%>
                    <li><span class="input_box_span"><b>*</b>姓  名:</span><input readonly name="" value="<%=user.RealName.Substring(0, 1) + "**"%>" class="input" type="text"></li>
                    <li><span class="input_box_span"><b>*</b>身份证:</span><input readonly name="" value="<%="***************" + user.IDNumber.Substring(user.IDNumber.Length - 3, 3)%>"  class="input" type="text"></li>
                    <li><span class="input_box_span"><b>*</b>现居地址:</span><input readonly name="" value="<%=user.UserAddress.Substring(0, 5) + "**"%>" class="input" type="text"></li>
                    <div class="btn_position">
            		    <span class="button_no">已认证</span>
					    <span class="correct_ico">√</span><span class="correct_ico_text">实名认证已完成</span>
           		    </div>  
                    <%}else{%>
                    <li><span class="input_box_span"><b>*</b>姓  名:</span><input readonly name="" value="**" class="input" type="text"></li>
                    <li><span class="input_box_span"><b>*</b>身份证:</span><input readonly name="" value="***************"  class="input" type="text"></li>
                    <li><span class="input_box_span"><b>*</b>现居地址:</span><input readonly name="" value="***>" class="input" type="text"></li>
                    <div class="btn_position">
            		    <a href="javascript:void(0)" class="button_blue" onclick="sfrz_form_read();">添加</a>
					    <span class="no_ico">X</span><span class="no_ico_text">您还没有进行实名认证</span>
           		    </div>  
                    <%}%>
                </ul>
			    <div class="clear"></div>
			    <div class="border_line"></div>

                <ul class="userinfo_list2">
                    <li><span class="input_box_span"><b>*</b>手机号码:</span><input readonly id="Text3" name="" value="<%="*******" + user.CellPhone.Substring(user.CellPhone.Length - 4, 4)%>" type="text" class="input"></li>
                    <div class="btn_position2">
            		    <span class="button_no">已验证</span>
					    <span class="correct_ico">√</span><span class="correct_ico_text">手机已验证</span>
           		    </div>   
                </ul>

			    <div class="clear"></div>
			    <div class="border_line"></div>

                <ul class="userinfo_list2">
                    <li><span class="input_box_span"><b>*</b>登录密码:</span><input readonly id="Password2" name="" value="************" type="password" class="input"></li>
                    <div class="btn_position2">
            		    <a href="javascript:void(0)" class="button_blue" onclick="edit_login_form();">修改</a>
           		    </div> 
                </ul>

				<!--个人信息注意事项 start-->
				<div id="sfrz_form_read" style="display:none;width:500px">
					<div class="warn_box_min">
						<div class="warn_box_min_conten">
							<font style="font-size:16px;font-weight:bold;font-family:'微软雅黑'; line-height:25px; color:#ED3C3C;">实名认证里的信息会用于绑定银行卡和提现使用,请确保信息的真实性,否则会造成账户无法提现!<br>且实名认证一次绑定,不可修改,请认真填写!</font>
						</div>
					</div>
				</div>

				<!--个人信息 表单 start-->
				<div id="sfrz_form" style="display:none;width:336px">
					<form class="sfrz_form">
						<div class="warn_box_min">
							<div class="warn_box_min_conten">
								<font color="#ED3C3C">实名认证里的信息会用于绑定银行卡和提现使用,请确保信息的真实性,否则会造成账户无法提现,且实名认证一次绑定,不可修改,请认真填写!</font>
							</div>
						</div>
						<div class="break_5"></div>
						<p class="input_lable"><strong>*</strong>姓名</p>
						<p class="input_and_button">
							<input class="input_100" value="" datatype="zh2-20" errormsg="请填写中文姓名" sucmsg="&nbsp;" nullmsg="请填写姓名!" id="truename" type="text">
						<span class="Validform_checktip"></span></p>
						<p class="input_lable"><strong>*</strong>身份证号码</p>
						<p class="input_and_button">
                        <input class="input_100 hover yuyin_tips" datatype="idcard" value="" errormsg="请填写正确的身份证号码" sucmsg="&nbsp;" nullmsg="请填写身份证号码!" id="idcard" type="text">
						<span class="Validform_checktip"></span></p><p class="input_lable"><strong>*</strong>现居地址</p>
							<select id="s_province" name="s_province" class="input" style="width:100px;padding:5px 0;"><option value="">省份</option><option value="北京市">北京市</option><option value="天津市">天津市</option><option value="上海市">上海市</option><option value="重庆市">重庆市</option><option value="河北省">河北省</option><option value="山西省">山西省</option><option value="内蒙古">内蒙古</option><option value="辽宁省">辽宁省</option><option value="吉林省">吉林省</option><option value="黑龙江省">黑龙江省</option><option value="江苏省">江苏省</option><option value="浙江省">浙江省</option><option value="安徽省">安徽省</option><option value="福建省">福建省</option><option value="江西省">江西省</option><option value="山东省">山东省</option><option value="河南省">河南省</option><option value="湖北省">湖北省</option><option value="湖南省">湖南省</option><option value="广东省">广东省</option><option value="广西">广西</option><option value="海南省">海南省</option><option value="四川省">四川省</option><option value="贵州省">贵州省</option><option value="云南省">云南省</option><option value="西藏">西藏</option><option value="陕西省">陕西省</option><option value="甘肃省">甘肃省</option><option value="青海省">青海省</option><option value="宁夏">宁夏</option><option value="新疆">新疆</option><option value="香港">香港</option><option value="澳门">澳门</option><option value="台湾省">台湾省</option></select>&nbsp;&nbsp;
			    			<select id="s_city" name="s_city" class="input" style="width:100px;padding:5px 0;"><option value="">地级市</option></select>&nbsp;&nbsp;
			                <select id="s_county" name="s_county" class="input" style="width:100px;padding:5px 0;"><option value="">市、县级市</option></select>
						<div class="break_10"></div>
						<div class="border_line"></div>
					
						<%--<div class="clear"></div>
						<p class="input_lable"><strong>*</strong>验证码</p>
						<p class="input_and_button Validform_right_3">
						    <input class="input hover fl" datatype="e4-4" errormsg="验证码位数不正确,为4位!" sucmsg="&nbsp;" nullmsg="请填写验证码!" style="width:122px" id="txtResetCode" type="text">						
							<a href="javascript:void;" class="button_blue fr" style="width:50px;margin:2px 6px 0 0" id="sendphone" onclick="sendphone()">短信验证</a>
						<span class="Validform_checktip"></span></p>--%>
						<div class="break_20"></div>
                             
						<input name="button" class="button_blue hover" value="确认" style="padding:10px 0;font-size:14px" onclick="yztruename()" type="button">
						<div class="break_10"></div>
					</form>
				</div>
                    
                <div id="sfrz_form_1" style="display:none;width:460px">
						<div class="warn_box_min">
							<div class="warn_box_min_conten">
								<font style="font-size:20px;font-weight:bold;font-family:'微软雅黑'" color="#ED3C3C">为你的账户安全,实名认证为一次绑定,不可修改!</font>
							</div>
						</div>
				</div>
				<!--个人信息 表单 end-->

					    <!--登录密码修改 start-->
					    <div id="edit_login_form" style="width:340px;display:none">
						    <form class="edit_login_form" id="loginfm">
							    <div class="warn_box_min">
								    <div class="warn_box_min_conten">
									    <font color="#ED3C3C">您可以通过经常性修改交易密码更好的保护您的账号安全，以避免您受到意外损失</font><br>
									    1.经常性修改登录密码能有效的保护您的帐号安全<br>
									    2.涉及到您的资金安全，请勿设置简单的登录密码，不要设置和其它网站相同的登录密码<br>
								    </div>
							    </div>
							    <div class="break_10"></div>
							    <p class="input_lable"><strong>*</strong>原登录密码</p>
							    <div class="clear"></div>
							    <p class="input_and_button">
								    <input class="input_100 hover" datatype="*6-18" errormsg="密码为6到18个字符!" nullmsg="请填写原登录密码" sucmsg="&nbsp;" id="Password3" name="oldpassword" type="password">
							    <span class="Validform_checktip"></span></p>
							    <p class="input_lable"><strong>*</strong>新登录密码</p>
							    <p class="input_and_button">
								    <input class="input_100 hover" datatype="*6-18" errormsg="密码为6到18个字符!" nullmsg="请填写新登录密码" sucmsg="&nbsp;" id="Password4" name="newpassword" type="password">
							    <span class="Validform_checktip"></span></p>
							    <p class="input_lable"><strong>*</strong>重复新登录密码</p>
							    <p class="input_and_button">
								    <input class="input_100 hover" id="Password5" name="newpassword1" recheck="newpassword" datatype="*6-18" errormsg="两次输入的密码不一致!" nullmsg="请重复新登录密码" sucmsg="&nbsp;" type="password">
							    <span class="Validform_checktip"></span></p>
							    <div class="break_20"></div>
                                <input value="" id="Hidden2" name="token" type="hidden">
							    <input name="button" class="button_blue hover" value="确认" style="padding:10px 0;font-size:14px" onclick="editloginpass()" type="button">
							    <div class="break_10"></div>
						
					    </form></div>
					    <!--登录密码修改 start-->
		    </div>
	    </div>
    </div>
</asp:Content>