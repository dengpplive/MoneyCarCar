<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%MoneyCarCar.Models.SystemClaims model = ViewBag.SystemClaims; %>
    <!--msg table start -->
	<div class="path_link  mt145"><a href="/" title="首页">首页</a><span>/</span><a href="index.html" title="投资信息">投资信息</a><span>/</span><a href="###"  title=""></a></div>
    <div class="wrap_1200 mb20">
		<div class="wrap_1200_content content_max help_main">
			<div class="msg_table">
				<div class="msg_title">投资确认信息<img class="sun_ico" src="/Contents/images/sun_ico.png" width="120"></div>
				<div class="msg_table box_shadow">
                    <ul class="msg_list">
                    	<li><span>投资项目名称:</span><%=model.Title%></li>
                        <li><span>投资份数:</span><%=ViewBag.BuyCount%>份</li>
                        <li><span>投资总金额:</span><%=(model.SingleAmount*ViewBag.BuyCount).ToString("0.00")%>元  &nbsp;&nbsp;&nbsp;<b style="font-size:20px;font-family:'微软雅黑';color:red">大写:人民币<%=ExpandHelper.ToMoneyChina(model.SingleAmount*ViewBag.BuyCount) %></b></li>
                        <li><span>投资虚拟本金:</span><%=ViewBag.HaveVirtualMoney%>元</li>
                        <li><span>预期年化收益:</span><%=model.APR%>%</li>
                        <li><span>每日利息:</span><%=ExpandHelper.ToMoney(model.SingleAmount*ViewBag.BuyCount*model.APR/100/365,2)%></li>
                        <li><span>虚拟本金日息:</span><%=ExpandHelper.ToMoney(ViewBag.HaveVirtualMoney*model.APR/100/365,2)%></li>
                        <li><span>借款期限:</span><%=model.LoanPeriod%>个月</li>
                        <li><span>投资日期:</span><%=DateTime.Now.ToString("yyyy-MM-dd")%></li>
                        <li><span>还款日期:</span><%=Convert.ToDateTime(model.EarningsStartTime).AddMinutes(model.LoanPeriod).ToString("yyyy-MM-dd")%></li>
                        <li><span>担保方式:</span><%=model.GuaranteeWay%></li>
                        <li><span>还款方式:</span><%=model.RepaymentWat%></li>
                        <li><span>借款公司:</span><%=model.Borrower%></li>
                    </ul>
					<table border="1" cellpadding="0" cellspacing="0">
						<thead class="msg_table_title">
							<tr>
								<td colspan="5" style="text-align:left;padding-left:20px;height:30px; line-height:30px;background:#F6F7F7"><font style="font-size:30px;font-family:'微软雅黑';color:#666">担保质押物信息</font></td>
							</tr>
							<tr>
								<td>货品图片</td>
								<td>货品名称</td>
								<td>货品规格</td>
								<td>单份价格</td>
								<td>总金额</td>
							</tr>
						</thead>
						<tbody><tr class="hover">
							<td width="150"><img src="<%=model.TitleImagePath %>" width="118" height="118"></td>
							<td width="300"><p>
                            <a target="_blank" title="<%=model.Title%>" class="hover_img" href="/Home/InvestmentDetail/<%=model.ID%>"><%=model.Title%></a>
                            </p></td>
							<td><%=model.PawnSpec%> </td>				
							<td width="100"><%=model.SingleAmount.ToString("0.00")%>元</td>
							<td width="100"><%=(model.SingleAmount*ViewBag.BuyCount).ToString("0.00")%>元</td>
						</tr>
					</tbody></table>
				</div>
				<div class="msg_bottom">
				    <div class="clause_box">
                        范例合同<span class="font_red">（投资期结束或满标后生成正式合同）</span>:
					    <a href="#" target="_blank">《Hello Money 借款担保合同》</a><br>
                        <a href="#" target="_blank">《授权委托书》</a>
				        <span class="clause_box_span"><input type="checkbox" onclick="checkinput()" name="tyht" id="contract_input">我已经阅读并同意</span>
				    </div>
					<div class="msg_form">
                        <div class="msg_money">
                        	<p>投资总金额:<span><%=(model.SingleAmount*ViewBag.BuyCount).ToString("0.00")%></span>元 = 实付款:<span><%=(model.SingleAmount*ViewBag.BuyCount).ToString("0.00")%></span>元</p>
							<p>实付款:<i>人民币<%= ExpandHelper.ToMoneyChina(model.SingleAmount*ViewBag.BuyCount)%></i></p>                           	
                    	</div>
						<div class="msg_form_button">
                            <span>账户总额: <b><%=ViewBag.HaveMoney.ToString("0.00")%>元</b><a href="http://www.hellomoney.com/index.php?c=pay" class="account_prepaid">充值</a></span><span>实付款: <b><%=(model.SingleAmount*ViewBag.BuyCount).ToString("0.00")%>元</b></span><span>账户余额: <b><%=(ViewBag.HaveMoney-model.SingleAmount*ViewBag.BuyCount).ToString("0.000")%>元</b></span>
                        </div>
                        <input type="hidden" name="InvestorsID" id="InvestorsID" value="<%=model.ID%>" />
                        <input type="hidden" name="BuyCount" id="BuyCount" value="<%=ViewBag.BuyCount%>" />
                        <input type="hidden" name="IsUserBounty" id="IsUserBounty" value="<%=ViewBag.IsUserBounty%>" />
                        <input type="hidden" name="BountyCount" id="BountyCount" value="<%=ViewBag.BountyCount%>" />
				    </div>
                    <p class="msg_check">使用虚拟本金</p>
                    <div class="msg_btn_block">
                        <input class="b_btn fr un_display" type="button" onclick="return submitform()" value="提交订单">
                     	<a href="http://www.hellomoney.com/index.php?transactionid=108&c=transaction&a=transactionact#" class="button_no" style="display:block">请勾选右侧同意,右侧协议</a>
					</div>
				</div>
				<div class="break_50"></div>
			</div>
		</div>
	</div>
	<!--msg table end-->
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript">
        function checkinput() {
            //用户注册协议 验证
            contractinput();
        }
        function contractinput() {
            var contract = $('#contract_input').prop("checked");
            if (contract == true) {
                $('.b_btn').show();
                $('.button_no').hide();
                return false;
            } else {
                $('.b_btn').hide();
                $('.button_no').css('display', 'block');
                return true;
            }
        }
        function submitform() {
            var contract = $('#contract_input').prop("checked");
            if (contract != true) {
                alert("请勾选右侧同意,右侧协议");
                return false;
            }
            var datas = { UserID: 0, InvestorsID: $("#InvestorsID").val(), BuyCount: $("#BuyCount").val(), IsUserBounty: $("#IsUserBounty").val(), BountyCount: $("#BountyCount").val() };
            $.post("/Submit/SubmitOrderToDatabase/", datas, function (result) {
                result = $.parseJSON(result);
                if (!result.IsSeccess) {
                    if (result.ErrorCode == -2) {
                        alert("用户未登录或登录超时");
                        location.href = "/User";
                        return false;
                    } else {
                        alert(result.ErrorMsg);
                        return false;
                    }
                }
                else {
                    ToPayForm(result);
                    return true;
                }
            });
        }
    </script>
</asp:Content>