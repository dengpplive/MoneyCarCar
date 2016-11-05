<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Scription" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript" src="/Contents/js/jquery.form.js"></script>
    <script type="text/javascript" src="/Contents/js/artDialog.js"></script>
   <script type="text/javascript">
       $(document).ready(
        function () {
            var doc = document,
                inputs = doc.getElementsByTagName('input'),
                supportPlaceholder = 'placeholder' in doc.createElement('input'),
                placeholder = function (input) {
                    var text = input.getAttribute('placeholder'),
                        defaultValue = input.defaultValue;
                    if (defaultValue == '') {
                        input.value = text;
                    }
                    input.onfocus = function () {
                        if (input.value === text) {
                            this.value = '';
                        }
                    };
                    input.onblur = function () {
                        if (input.value === '') {
                            this.value = text;
                        }
                    }
                }; if (!supportPlaceholder) {
                for (var i = 0, len = inputs.length; i < len; i++) {
                    var input = inputs[i], text = input.getAttribute('placeholder');
                    if (input.type === 'text' && text) {
                        placeholder(input);
                    }
                }
            };
        });
       function CheckUser(UserNameEl) {
           var UserName = UserNameEl.value;
           if (UserName == null || UserName == "") {
               return false;
           }
           $.post("/Submit/CheckAccount/", { Infos: UserName, UserType: 1 }, function (result) {
               if (result > 0) {
                   alert("用户名重复");
                   UserNameEl.value = "";
                   UserNameEl.focus();
               }
           });
       }
       function CheckPhonte(PhonteEl) {
           var Phonte = PhonteEl.value;
           if (Phonte == null || Phonte == "") {
               return false;
           }
           $.post("/Submit/CheckPhone/", { Infos: Phonte, UserType: 1 }, function (result) {
               if (result > 0) {
                   alert("手机号码重复");
                   PhonteEl.value = "";
                   PhonteEl.focus();
               }
           });
       }
       function RegUser() {
           var UserName = UserNameEl.value;
           var Pwd = PwdEl.value;
           var PwdRe = PwdReEl.value;
           var Email = EmailEl.value;
           var Phonte = PhonteEl.value;
           var Vcode = VcodeEl.value;
           var Recommended = RecommendedEL.value;
           if (UserName == "" || UserName.length == 0) {
               alert("请输入用户名");
           }
           if (Pwd != Pwd) {
               alert("两次密码输入不一样");
               PwdRe.focus();
               return false;
           }
           if (Email == "" || Email.length == 0) {
               alert("请输入电子邮箱");
           }
           if (Phonte == "" || Phonte.length == 0) {
               alert("请输入手机号码");
           }
           var datas = { UserName: UserName, UserPwd: Pwd, UserPwdRe: PwdRe, UserEmail: Email, UserPhone: Phonte, Vcode: Vcode, PhoneVcode: "111111", UserTpye: 1, Recommended: Recommended };

           $.post("/Submit/RegUser/", datas, function (result) {
               if (result == -1) {
                   alert("验证码错误");
                   VcodeEl.value = "";
                   VcodeEl.focus();
                   return false;
               }
               else if (result == -2) {
                   alert("两次密码输入不相同");
                   return false;
               } else {
                   location.href = "/Home/PhoneValidate";
               }
           });
       }
       //用户注册协议
       function contract() {
           art.dialog({
               title: "用户注册协议",
               lock: true,
               opacity: 0.2,
               fixed: true,
               padding: 0,
               content: '<iframe height=440 width=690 src="/index.php?c=login&a=agreement" frameborder=0 allowfullscreen></iframe>'
           });
       }
       var countdown = 0;
       function settime(val) {
           if (countdown <= 0) {
               val.removeAttribute("disabled");
               val.value = "免费获取验证码";
               //countdown = 60;
           } else {
               val.setAttribute("disabled", true);
               val.value = "重新发送(" + countdown + ")";
               countdown--;
               setTimeout(function () {
                   settime(val)
               }, 1000)
           }
           
       }
       function sendPhoneValidata() {
           var Phonte = PhonteEl.value;
           if (Phonte == "" || Phonte.length == 0) {
               alert("请输入手机号码");
               return;
           }
           $.post("/Submit/SendPhoneValidate/", { userPhone: $("#VcodeEl").val() + "-" + Phonte }, function (result) {
               if (result == -2) {
                   alert("验证码输入错误。");
               }
               else if (result > 0) {
                   countdown = result;
                   settime(document.getElementById("SendBtn"));
               } else{
                   alert("发送失败!");
                   countdown = 0;
                   settime(document.getElementById("SendBtn"));
               }
               code('js-mail_vcode_img', '/Home/ValidateCode');
           });
       }
   </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrap_1200_content">
        <div class="reg_content">
            <div class="reg_bg fl"></div>
            <div class="reg_main fr">
                <h2 class="reg_title">账户注册</h2>
                <ul class="reg_list">
                    <li><span>账 户：</span><input type="text" id="UserNameEl" value="" placeholder="账户名" onchange="return CheckUser(this)" class="reg_input" /></li>
                    <li><span>密 码：</span><input type="password" id="PwdEl" value="" placeholder="密码" class="reg_input" /></li>
                    <li><span>重复密码：</span><input type="password" id="PwdReEl" value="" placeholder="重复密码" class="reg_input" /></li>
                    <li><span>电子邮箱：</span><input type="text" id="EmailEl" value="" placeholder="电子邮箱" class="reg_input" /></li>
                    <li><span>手机号码：</span><input type="text" id="PhonteEl" value="" placeholder="手机号码" onchange="return CheckPhonte(this)" class="reg_input" /></li>
                    <li><span>推荐号码：</span><input type="text" id="RecommendedEL" value="" placeholder="没有可以不填" onchange="" class="reg_input" /></li>
                    <li><span style="width:auto">手机验证码：</span><input style="width:50px" type="text" id="PhoneVCodeEL" value="" placeholder="手机验证码" /><input type="button" value="发送验证码" id="SendBtn" onclick="sendPhoneValidata()" /></li>
                    <li><span>验 证 码：</span>
                        <input type="text" id="VcodeEl" value="" placeholder="验证码" class="yzm_input" /><img src="/Home/ValidateCode" onclick="code('js-mail_vcode_img','/Home/ValidateCode')" height="34" title="点击换一张" id="js-mail_vcode_img" class="yzm_img">
                        <a href="javascript:code('js-mail_vcode_img','/Home/ValidateCode')" class="re_a">换一张</a></li>
                </ul>
                <p class="reg_info3">
                    <input type="checkbox" checked="checked" id="contract_input" class="fl">
                    <label class="fl" for="contract_input">
                        我已阅读并同意
                    </label>
					<a href="javascript:void(0)" onclick="contract();">《财富车车注册协议》</a>
                </p>
                <p class="login_position"> <input type="button" value="下一步" class="l_btn" onclick="return RegUser()" /></p>
            </div>
        </div>
    </div>
</asp:Content>
