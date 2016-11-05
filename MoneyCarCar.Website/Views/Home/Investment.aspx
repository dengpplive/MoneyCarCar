<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <%
        MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemClaims> datas = ViewBag.Datas as MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemClaims>;
    %>
    <!--banner start-->
	<div class="tz_banner">
    	<img src="/Contents/images/banner/tz_bg.jpg" />
	</div>
	<!--banner end-->

	<!--提醒框 start-->
	<div class="warn_box">
		<b class="warn_ico"></b>
		<a href="###" class="warn_link" target="_blank" title="温馨提示">温馨提示：点击阅读新手指引,再投资你会得到更大的收益!</a>
	</div>
	<!--提醒框 end-->

	<!--投资列表 start-->
	<div class="wrap_1200 products_wrap">
		<div class="products_content">
        	<div class="pd_title"><h2>投资产品列表</h2></div>
			<ul class="products_box">
                <%foreach (MoneyCarCar.Models.SystemClaims item in datas.ListAll){%>
                 <li>
                     <%=item.AlreadyAmount>=item.LoanAmount?"<a href=\"#\" class=\"products_mask\"></a>":""%>
					<a target="_blank" href="/Home/InvestmentDetail/<%=item.ID%>" class="products_img fl">
						<img class="img_loading hover_img hover" src="<%=item.TitleImagePath %>" width="168" height="168">
						<div class="products_img_tag_box">
							<span class="imt_tag_bao">担保物</span>
							<%--<span class="imt_tag_shou no_hover_right_tips" title="可获得销售奖励">可售48万</span>--%>
						</div>
					</a>
					<div class="products_title fl">
						<a target="_blank" href="/Home/InvestmentDetail/<%=item.ID%>" class="products_link text fl" title="<%=item.Title %>"><%=item.Title %></a>
						    <%
                                List<MoneyCarCar.Models.IconModel>  icons = MoneyCarCar.Commons.ExpandHelper.ToModel<List<MoneyCarCar.Models.IconModel>>(item.Icons);
                                foreach (MoneyCarCar.Models.IconModel icos in icons)
                                {
                            %>
                            <span class="tag_ico tag_<%=icos.StyleName%> fr no_hover_top_tips" title="<%=icos.Description%>"><%=icos.Title%></span>
                            <%
                                }
                            %>
					</div>
					<div class="products_money fl">
						<div class="products_money_info">
							<p class="fl">
								<span>借款金额</span>
								<b><font>¥</font><%=item.LoanAmount.ToString("#,##0.00") %></b>
							</p>
							<p class="fr">
								<span>年化利率</span>
								<b><%=item.APR %>%</b>
							</p>
							<div class="clear"></div>
						</div>
                        <%if (item.AlreadyAmount >= item.LoanAmount){%>
						<ul class="end_time_over">
							<b>投资期已于 <%=item.InvestmentEndTime.ToDateTime().ToString("yyyy-MM-dd")%> 日 结束</b>
						</ul>
                        <%}else{%>
                        <ul class="end_time_box" data-seconds="<%=((int)(Convert.ToDateTime(item.EarningsStartTime)-DateTime.Now).TotalSeconds)%>">
							<b>可投时间:</b>
						    <span>-</span>天
						    <span>-</span>时
						    <span>-</span>分
						    <span>-</span>秒
						</ul>
                        <%}%>
					</div>
					<div class="products_info fl">
						<p class="text">借款期限: <span class="font_red font"><%=item.LoanPeriod %>个月</span></p>
						<p class="text">还款方式: <span class="font_red"><%=item.RepaymentWat%></span></p>
						<p class="text">担保方式: <span class="font_red"><%=item.GuaranteeWay%></span></p>
						<p class="text">收益起计日期: <%=item.EarningsStartTime.ToDateTime().ToString("yyyy-MM-dd") %> 或 <font class="font_red no_hover_top_tips" title="一旦投满,立即发息(工作时间),不用等待投资期结束">满标即发息</font></p>
						<p class="text">借款公司: <%=item.Borrower %></p>
					</div>
					<div class="products_step fr">
						<p class="text">剩余份数: <font class="font_red"><%=((item.LoanAmount-item.AlreadyAmount)/item.SingleAmount).ToString("0.00") %>份</font></p>
						<P><span class="fl">投资进度:</span><span class="fr"><%=100-(item.LoanAmount-item.AlreadyAmount)*100/item.LoanAmount%>%</span><div class="break_5"></div></P>
						<div class="products_step_box">
							<div class="products_step_loading" style="width:<%=100-(item.LoanAmount-item.AlreadyAmount)*100/item.LoanAmount%>%"></div>
						</div>
						<div class="break_10"></div>
						<a target="_blank" href="/Home/InvestmentDetail/<%=item.ID%>" class="button_blue"><%=(item.AlreadyAmount >= item.LoanAmount)?"还款中":"立即投资"%></a>
					</div>
				</li>
                <%}%>
			</ul>
			<div class="button_more_box">
				<div class="break_10"></div>
				<div class="fenye  list_page">
					<div id="pagerarea"><span class="disabled">首页</span><span class="disabled">上一页</span><span class="current">1</span><a href="###">2</a><a href="###">下一页</a><a href="###">尾页</a><span class="total">共 2 页</span></div>				</div>	
                <div class="break_10"></div>
			</div>
		</div>
	</div>
	<!--投资列表 end-->
</asp:Content>

<asp:Content ID="Style" ContentPlaceHolderID="StyleContent" runat="server">
    <link rel="stylesheet" type="text/css" href="/Contents/css/global.css" />
	<link rel="stylesheet" type="text/css" href="/Contents/css/index.css" />
    <link rel="stylesheet" href="/Contents/css/animator.css"/>
</asp:Content>

<asp:Content ID="Scription" ContentPlaceHolderID="ScriptionContent" runat="server">
    <%
        MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemClaims> datas = ViewBag.Datas as MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemClaims>;
    %>
    <!-- 动画控制 -->
	<script type="text/javascript" src="/Contents/js/wow.min.js"></script>
	<script type="text/javascript" src="/Contents/js/iframeTools.js"></script>
	<!--表单验证插件-->
	<script type="text/javascript" src="/Contents/js/Validform_v5.3.2_min.js"></script> 
    <!-- 验证码和短信倒计时 重要-->
    <script type="text/javascript" src="/Contents/js/jquery.form.js"></script>
	<!-- 其他效果 函数 重要-->
	<script type="text/javascript" src="/Contents/js/other.js"></script> 
	<!--tips 提示框 重要-->
	<script type="text/javascript" src="/Contents/js/jquery.poshytip.min.js"></script>
    <!--图片懒加载-->
    <script type="text/javascript" src="/Contents/js/jquery.lazyload.js"></script>
    <!--倒计时-->
    <script type="text/javascript" src="/Contents/js/Countdown.js"></script>
    <script type="text/javascript">
        $(function () {
            $(function () {
                $("#pagerarea").myPagination({
                    currPage:<%=datas.PageIndex%>,
                    pageCount:<%=datas.PageCount%>,
                    pageNumber:5,
                    ajax: {
                        on: false,
                        onClick: function (page) {
                            ZENG.msgbox.show(" 正在加载" + page + "页，请稍后...", 6, 5000);
                            location.href = "/Home/Investment/" + page;
                        }
                    }
                });
            });
            $('.end_time_box').countdown(function (s, d) {
                var items = $(this).find('span');
                //items.eq(0).text(d.total);
                items.eq(0).text(d.day);
                items.eq(1).text(d.hour);
                items.eq(2).text(d.minute);
                items.eq(3).text(d.second);
            });
        });
    </script>
</asp:Content>