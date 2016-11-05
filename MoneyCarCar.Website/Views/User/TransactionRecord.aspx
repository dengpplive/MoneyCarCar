<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return> datas = ViewBag.Datas as MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return>;
    %>
    <div class="right">
        <div class="user_right_content">
		    <h2 class="user_plate_title">交易记录</h2>
			<table class="tb" border="0" cellpadding="0" cellspacing="0">
				<thead>
					<tr>
						<th width="100">日期</th>
						<th width="120">类型</th>
						<th width="100">收入</th>
						<th width="100">支出</th>
						<th width="120">余额</th>
						<th width="200">说明</th>
					</tr>
				</thead>
                <tbody>
                    <%if (datas.AllCount <= 0){%>
                    <tr>
                        <td colspan="8"><font class="font_red font_max">暂无数据</font></td>
                    </tr>
                    <%}else{foreach (MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return item in datas.ListAll){%>
                    <tr>
                        <td><%=item.PayTime.ToString(1)%></td>
                        <td><%=(PayType)item.PayType%></td>
                        <td><%=item.InMoney.ToMoney(2)%></td>
                        <td><%=item.OutMoney.ToMoney(2)%></td>
                        <td><%=item.RemainMoney.ToMoney(2)%></td>
                        <td><%=item.Remark%></td>
                    </tr>
                    <%}}%>
                </tbody>
            </table>
            <div class="break_30"></div>
            <div id="pagerarea"><span class="current">1</span><span class="total">共 1 页</span></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <%
        MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return> datas = ViewBag.Datas as MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.Statisticals.Return.TransactionRecord_Return>;
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
                        location.href = "/user/transactionrecord/" + page;
                    }
                }
            });
        });
    </script>
</asp:Content>
