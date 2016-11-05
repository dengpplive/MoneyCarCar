<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%
            ModelByCount<SystemRequestRecord> datas = ViewBag.Datas as ModelByCount<SystemRequestRecord>;
        %>    
        <div class="right">
        	<div class="user_right_content">
			<h2 class="user_plate_title">提现记录</h2>
			<table class="tb" border="0" cellpadding="0" cellspacing="0">
				<thead>
					<tr>
						<th width="50">编号</th>
						<th width="120">申请时间</th>
						<th width="100">提现金额</th>
						<th width="100">审核状态</th>
						<th width="120">提现结果</th>
					</tr>
				</thead>
				<tbody>
                    <%if (datas.AllCount <= 0){%>
                    <tr>
                	    <td colspan="5"><font class="font_red font_max">暂无数据</font></td>
                    </tr>
                    <%}else{foreach (SystemRequestRecord item in datas.ListAll){%>
                    <tr>
                        <td><%=item.Id%></td>
                        <td><%=item.RequestDate.ToString(1)%></td>
                        <td><%=item.RequestMoney.ToMoney(2)%></td>
                        <td>审核通过</td>
                        <td><%=(RequestOperStatus)item.RequestOperStatus%></td>
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
        ModelByCount<SystemRequestRecord> datas = ViewBag.Datas as ModelByCount<SystemRequestRecord>;
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
                        location.href = "/User/WithdrawRecord/" + page;
                    }
                }
            });
        });
    </script>
</asp:Content>
