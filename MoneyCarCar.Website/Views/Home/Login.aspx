<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrap_1200_content mt125">   
    	<div class="login_content">
        	<div class="login_bg"></div>
	        <div class="login_main">
        	    <h2 class="lm_title">登录财富车车</h2>
                <ul class="lm_list">
            	    <li><span>账&nbsp;&nbsp;&nbsp;号：</span><input type="text" id="UserNameOrPhone" name="UserNameOrPhone" value="" placeholder="用户名/手机号" class="lm_input" /></li>
                    <li><span>密&nbsp;&nbsp;&nbsp;码：</span><input type="password" id="UserPassword" name="UserPassword" value="" placeholder="密码" class="lm_input" /></li>
                    <li>
                        <span>验证码：</span><input type="text" id="VcodeEl" name="VcodeEl" value="" placeholder="验证码" class="yzm_input" /><img src="/Home/ValidateCode" onclick="code('js-mail_vcode_img','/Home/ValidateCode')" height="34" title="点击换一张" id="js-mail_vcode_img" class="yzm_img"> <a href="javascript:code('js-mail_vcode_img','/Home/ValidateCode')" class="re_a">换一张</a>
                    </li>
                    <li><input type="hidden" id ="UserType" name="UserType" value="1" /></li>
                </ul>
                <p class="login_position"><input type="button" value="登录" onclick="return login()" class="l_btn" /></p>
                <p class="reg_info2"><span class="fl">没有账号?点击<a href="/Home/Reg" class="reg_btn">免费注册</a></span> <a href="#" class="reg fr">忘记密码?</a></p>
            </div>
    	</div>
	</div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript" src="/Contents/js/jquery.form.js"></script>
    <script type="text/javascript">
        function login() {
            var datas = { UserNameOrPhone: $("#UserNameOrPhone").val(), UserPassword: $("#UserPassword").val(), UserIP: "", UserType: 1, Vcode: $("#VcodeEl").val() };
            $.post("/Submit/Login/", datas, function (result) {
                if (result == 1) {
                    location.href = "/User";
                }
                else if (result == -1) {
                    alert("验证码错误");
                    code('js-mail_vcode_img', '/Home/ValidateCode');
                    VcodeEl.value = "";
                    VcodeEl.focus();
                    return false;
                }
                else {
                    alert(result);
                    return false;
                }
            });
        }
    </script>
</asp:Content>
