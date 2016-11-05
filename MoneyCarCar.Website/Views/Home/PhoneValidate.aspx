<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrap_1200_content mt125">   
    	<div class="login_content">
        	<div class="login_bg"></div>
	        <div class="login_main2">
        	<h2 class="lm_title">注册 - 手机验证</h2>
            <ul class="lm_list">
            	<li><input type="text" value="" placeholder="账户名：<%= ViewBag.UserName%>" class="input_account_name" readonly="readonly" /></li>
                <li>手机号码</li>
                <li><input type="text" id="PhoneNumber" value="<%= ViewBag.PhoneNumber%>" placeholder="手机号码" class="input_text" /></li>
                <li><input type="button" id="SendBtn" onclick="return sendPhoneValidata()" value="发送短信验证码" class="button_blue2"/></li>
                <li><input type="text" id="PhoneVcodeEl" value="" placeholder="手机验证码" class="yzm_input" /></li>
                <li>
                    <input type="text" id="VcodeEl" value="" placeholder="验证码" class="yzm_input" />
                    <img src="/Home/ValidateCode" onclick="code('js-mail_vcode_img','/Home/ValidateCode')" height="34" title="点击换一张" id="js-mail_vcode_img" class="yzm_img">
                    <a href="javascript:code('js-mail_vcode_img','/Home/ValidateCode')" class="re_a">换一张</a>
                </li>
            </ul>
            <p class="login_position"><input type="button" onclick="return RegUserToDatabase()" value="完成" class="l_btn" /></p>
        </div>
    	</div> 	
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SEOContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript" src="/Contents/js/jquery.form.js"></script>
    <script type="text/javascript">
        document.onload = function () {
            var a = $("#mail_vcode_img");
            a.click(function () {
                a.attr("src", "/ValidateCode?" + new Date().getTime());
            });
        }
        function sendPhoneValidata() {
            $.post("/Submit/SendPhoneValidate/", { userPhone: $("#PhoneNumber").val() }, function (result) {
                if (result > 0) {
                    countdown = result;
                    settime(document.getElementById("SendBtn"));
                } else if (result < 0) {
                    alert("发送失败!");
                    countdown = 0;
                    settime(document.getElementById("SendBtn"));
                }
            });
        }

        var countdown = 0;
        function settime(val) {
            if (countdown == 0) {
                val.removeAttribute("disabled");
                val.value = "免费获取验证码";
                countdown = 60;
            } else {
                val.setAttribute("disabled", true);
                val.value = "重新发送(" + countdown + ")";
                countdown--;
            }
            setTimeout(function () {
                settime(val)
            }, 1000)
        }
        function RegUserToDatabase() {
            var Vcode = VcodeEl.value;
            var PhoneVcode = PhoneVcodeEl.value;
            var datas = { UserName: "", UserPwd: "", UserPwdRe: "", UserPhone: "", Vcode: Vcode, PhoneVcode: PhoneVcode, UserTpye: 2 };

            $.post("/Submit/RegUserToDatabase/", datas, function (result) {
                if (result == -1) {
                    alert("验证码错误");
                    VcodeEl.value = "";
                    VcodeEl.focus();
                    return false;
                }else if (result == -2) {
                    alert("手机验证码错误");
                    PhoneVcode.value = "";
                    PhoneVcode.focus();
                    return false;
                }else {
                    alert("注册成功！");
                    location.href = "/Home/Login";//跳转到登录页面
                    return true;
                }
            });
        }
    </script>
</asp:Content>
