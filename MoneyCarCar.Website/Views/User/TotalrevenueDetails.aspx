<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        MoneyCarCar.Models.Statisticals.Return.Earnings_Return model = (MoneyCarCar.Models.Statisticals.Return.Earnings_Return)ViewBag.Result;
        ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return> datas = ViewBag.Record as ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return>;
    %>
    <div class="right">
        <div class="user_right_content">
		<h2 class="user_plate_title">总收益--明细</h2>
		<!--投资统计 strat-->
		<div class="touzi_tj">
			<div class="break_10"></div>
			<div class="break_1.0"></div>
			<ul class="touzi_tj_info">
                <li class="hover">
                    <a href="/user/totalrevenue" class="tj_info_list tj_info_ico_2"><span></span><b>总收益: <%=model.TotalInterest.ToMoney(2)%>元</b></a>
                </li>
				<li class="hover">
                    <a href="/user/totalinvestment" class="tj_info_list tj_info_ico_1"><span></span><b>总投资: <%=model.TotalInvestment.ToMoney(2)%>元</b></a>
                </li>
				<li class="tj_info_list_on hover">
                    <a href="#" class="tj_info_list tj_info_ico_3"><span></span><b>收益明细</b></a>
                </li>
			</ul>
		</div> 
		<!--投资统计 end-->

		<div class="break_30"></div>
		<div class="user_plate_box">
            <div class="srmx_block">
            	<ul class="srmx_list">
                    <li><a class="srmx_current">收益明细</a></li>
                    <%--<li><a href="###">不可用金额明细</a></li>--%>
            	</ul>
                <table class="tb srmx_content" border="0" cellpadding="0" cellspacing="0">
                    <thead class="user_table_title">    
                        <tr>
                            <th width="100">日期</th>
                            <th width="120">类型</th>
                            <th width="100">收入</th>
                            <th width="100">支出</th>
                            <th width="100">当前余额</th>
                            <th width="100">说明</th>
                        </tr>
                    </thead>				
                    <tbody>
                        <%if (datas.AllCount <= 0){%>
                        <tr>
                            <td colspan="8"><font class="font_red font_max">暂无数据</font></td>
                        </tr>
                        <%}else{foreach (MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return item in datas.ListAll){%>
                        <tr>
                            <td class="user_table_gray"><%=item.PayTime.ToString(1)%></td>
                            <td class="user_table_gray"><%=(PayType)item.PayType%></td>
                            <td class="user_table_green"><%=item.InMoney.ToMoney(2)%></td>
                            <td class="user_table_red"><%=item.OutMoney.ToMoney(2)%></td>
                            <td class="user_table_gray"><span class="button_blue"><%=item.RemainMoney.ToMoney(2)%></span></td>
                            <td class="user_table_gray"><%=item.Remark%></td>
                        </tr>
                        <%}}%>
                    </tbody>
                </table>
                <div class="break_30"></div>
                <div id="pagerarea"><span class="current">1</span><span class="total">共 1 页</span></div>
<%--                <table class="tb srmx_content un_display" border="0" cellpadding="0" cellspacing="0">
                    <thead class="user_table_title">    
                        <tr>
                            <th width="100">日期</th>
                            <th width="120">类型</th>
                            <th width="100">收入</th>
                            <th width="100">支出</th>
                            <th width="100">当前余额</th>
                            <th width="100">说明</th>
                        </tr>
                    </thead>				
                    <tbody>
                        <tr>
                            <td class="user_table_gray">2015-01-20</td>
                            <td class="user_table_gray">每日利息</td>
                            <td class="user_table_green">0.090</td>
                            <td class="user_table_red">1元</td>
                            <td class="user_table_gray"><a href="###" class="button_blue">202.45</a></td> 
                            <td class="user_table_gray">世纪名车会法拉利F430超跑抵押借款标一期</td>   
                        </tr>	
                    </tbody>
                </table>--%>
            </div>
		</div>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <%
        ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return> datas = ViewBag.Record as ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return>;
    %>
    <script type="text/javascript">
        $(function () {
            $("#pagerarea").myPagination({
                currPage:<%=datas.PageIndex%>,
                pageCount:<%=datas.PageCount%>,
                pageNumber:5,
                ajax: {
                    on: false,
                    onClick: function (page) {
                        ZENG.msgbox.show(" 正在加载" + page + "页，请稍后...", 6, 5000);
                        location.href = "/user/totalrevenuedetails/" + page;
                    }
                }
            });
        });
    </script>
</asp:Content>
