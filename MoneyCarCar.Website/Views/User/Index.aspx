<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="right">
        	<div class="user_info">
				<div class="user_info_content">
					<p>用户名: <span class="bt"><%=ViewBag.UserName %></span></p>
					<p class="account_balance"><span>账户余额: </span><b>￥<%=ViewBag.UserBalance.ToString("#,##0.00") %></b><a href="#" onclick="return account_recharge();" class="button_blue">充值</a><a href="#" onclick="return account_withdraw()" class="button_blue">提现</a></p>
					<p class="invest_profit">
						<span>不可用金额: </span><b>￥<%=ViewBag.UserFreeze.ToString("#,##0.00") %></b>(日息发放时间,为次日02:00-02:30)
						<span class="question_ico hover no_hover_tips" title="不可用金额为您<span class='red'>未确认的到账资金或者是已经在商城购买产品使用过的资金</span>，请注意左边竖排导航条有红色数字图标提示的栏目，进入相应栏目确认对应资金到账，即可转为可用资金.">?</span>
					</p>
					<p>
                    	<span class="fl">账户安全: </span>
                    </p>
                    <div class="account_security">
						<div class="account_security_bg account_tips" title="为了你的账户安全,请进行实名认证">
						    <div class="account_security_step_<%=ViewBag.UserIDNumberIsAuthenticate?"2":"1"%>"></div>
					    </div> 
					    <span class="fr">较高</span>
                    </div>
				</div>
			</div>
             <!--账户充值-->
            <div id="account_recharge" style="width:340px;display:none">
					<form class="account_recharge">
						<div class="break_10"></div>
						<p class="input_lable"><strong>*</strong>帐户金额</p>
						<div class="clear"></div>
						<p class="input_and_button">
							<div class="input_100"><span class="account_maoney">0.00</span>元</div>
						<span class="Validform_checktip"></span></p>
						<p class="input_lable"><strong>*</strong>充值金额</p>
						<p class="input_and_button">
							<input class="input_100 hover" nullmsg="请填写充值金额" sucmsg="&nbsp;" id="RechareMoney" name="RechareMoney" type="text">
						    <span class="Validform_checktip"></span>
						</p>
						<div class="break_20"></div>
						<input name="button" class="button_blue hover" value="确 认" style="padding:10px 0;font-size:14px" onclick="RechareBtn()" type="button">
						<div class="break_10"></div>						
				</form>
            </div>
            <!--账户提现-->
            <div id="account_withdraw" style="width:340px;display:none">
				<form class="account_recharge">
					<div class="break_10"></div>
					<p class="input_lable"><strong>*</strong>帐户金额</p>
					<div class="clear"></div>
					<p class="input_and_button">
						<div class="input_100"><span class="account_maoney">0.00</span>元</div>
					<span class="Validform_checktip"></span></p>
					<p class="input_lable"><strong>*</strong>提现金额</p>
					<p class="input_and_button">
						<input class="input_100 hover" nullmsg="请填写提现金额" sucmsg="&nbsp;" id="WithdrawMoney" name="WithdrawMoney" type="text">
					<span class="Validform_checktip"></span></p>
					<div class="break_20"></div>
					<input name="button" class="button_blue hover" value="确 认" style="padding:10px 0;font-size:14px" onclick="WithdrawBtn()" type="button">
					<div class="break_10"></div>						
				</form>
            </div>
            <div class="user_info_bottom">
				<div class="fl rz_box">
					<span class="rz_info_ico fl"></span>
					<div class="fl">
						<b>实名认证<span><%=ViewBag.UserIDNumberIsAuthenticate?ViewBag.UserIDNumber:"(**)"%></span></b>
						<%=ViewBag.UserIDNumberIsAuthenticate?"<span class=\"correct_ico\">√</span><span>已认证</span>":"<span class=\"no_ico\">×</span><a href=\"/User/UserInfo/\">添加</a>"%>
					</div>
					<div class="break_10"></div>
					<p>为了你的账户安全,以及享受更多的服务,请进行实名认证</p>
				</div>
				<div class="fl rz_box">													
					<span class="rz_phone_ico fl"></span>
					<div class="fl">
						<b>手机认证 <span><%=ViewBag.UserCellPhone %></span></b>
						<span class="correct_ico">√</span><span>修改</span>
					</div>
					<div class="break_10"></div>
					<p>您可享有手机相关的服务  </p>
				</div>
			</div>
        </div>
</asp:Content>

<asp:Content ID="ScriptionContent" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript">
        //充值弹出窗口
        function account_recharge() {
            art.dialog({
                title: "充值",
                lock: true,
                opacity: 0.2,
                fixed: true,
                padding: 0,
                content: document.getElementById('account_recharge')
            });
        }

        function RechareBtn() {
            $.post("/Submit/Rechare/", { Balance: $("#RechareMoney").val() }, function (data) {
                if (data.IsSeccess) {
                    ToPayForm(data);
                    return true;
                } else {
                    if (data.ErrorCode == -2) {
                        location.href = "/Home/Login";
                        return false;
                    } else if (data.ErrorCode == -3) {
                        art.dialog({
                            title: "错误",
                            content: data.msg,
                            ok: function () {
                                location.href = "/User/UserInfo";
                                this.close();
                                return false;
                            }
                        });
                    } else {
                        art.dialog({
                            title: "错误",
                            content: data.msg,
                            ok: function () {
                                this.close();
                                return false;
                            }
                        });
                    }

                    return false;
                }
            }, 'json');
        }

        //提现弹出窗口
        function account_withdraw() {
            art.dialog({
                title: "提现",
                lock: true,
                opacity: 0.2,
                fixed: true,
                padding: 0,
                content: document.getElementById('account_withdraw')
            });
        }
        function WithdrawBtn() {
            $.post("/Submit/Withdraw/", { Balance: $("#WithdrawMoney").val() }, function (data) {
                if (data.IsSeccess) {
                    if (data.ErrorCode == -2) {
                        location.href = "/Home/Login";
                        return false;
                    } else {
                        ToPayForm(data);
                        return true;
                    }
                } else {
                    alert(data.msg);
                    return false;
                }
            }, 'json');
        }
    </script>
</asp:Content>