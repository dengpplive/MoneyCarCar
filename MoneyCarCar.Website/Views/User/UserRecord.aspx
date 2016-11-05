<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemLog> datas = ViewBag.Datas as MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemLog>;
    %>
    <div class="right">
        <div class="user_right_content">
		    <!--登录日志 start-->
		    <h2 class="user_plate_title">登录日志</h2>
		    <div class="warn_box_min"><div class="warn_box_min_conten">尊敬的MoneyCarCar用户,MoneyCarCar为您记录和保存了您的登录记录(最后登陆10天),敬请审阅</div></div>
		    <div class="break_10"></div>
		    <table border="0" cellpadding="0" cellspacing="0" class="tb">
			    <tr>
				    <th width="100">用户名</th>
				    <th>登录时间</th>
				    <th width="150">登录IP</th>
			    </tr>
                 <%foreach (MoneyCarCar.Models.SystemLog item in datas.ListAll){%>
			    <tr>
				    <td><%=item.OperatorUserName%></td>
				    <td><%=item.OperatorTime.ToDateTime().ToString(12)%></td>
				    <td><%=item.OperatorIP%></td>
			    </tr>
                <%}%>
            </table>
		    <!--登录日志 end--> 
            <div class="break_30"></div>
            <div id="pagerarea"><span class="current">1</span><span class="total">共 1 页</span></div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <%
        MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemLog> datas = ViewBag.Datas as MoneyCarCar.Models.ModelByCount<MoneyCarCar.Models.SystemLog>;
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
                        location.href = "/User/UserRecord/" + page;
                    }
                }
            });
        });
    </script>
</asp:Content>
