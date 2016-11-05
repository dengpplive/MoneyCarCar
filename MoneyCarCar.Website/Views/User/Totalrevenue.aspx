<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%MoneyCarCar.Models.Statisticals.Return.Earnings_Return model = (MoneyCarCar.Models.Statisticals.Return.Earnings_Return)ViewBag.Datas; %>
    <div class="right">
        <div class="user_right_content">
		    <h2 class="user_plate_title">总收益--明细</h2>
			    <!--投资统计 strat-->
			    <div class="touzi_tj">
				    <div class="break_10"></div>
				    <div class="break_1.0"></div>
				    <ul class="touzi_tj_info">
                	    <li class="tj_info_list_on hover">
                            <a href="#" class="tj_info_list tj_info_ico_2"><span></span><b>总收益: <%=model.TotalInterest.ToMoney(2)%>元</b></a>
                        </li>
					    <li class=" hover">
                            <a href="/user/totalInvestment" class="tj_info_list tj_info_ico_1"><span></span><b>总投资: <%=model.TotalInvestment.ToMoney(2)%>元</b></a>
                        </li>
					    <li class="hover">
                            <a href="/user/totalrevenuedetails" class="tj_info_list tj_info_ico_3"><span></span><b>收益明细</b></a>
                        </li>
				    </ul>
			    </div> 
			    <!--投资统计 end-->
		    <div class="break_30"></div>
		    <div class="user_plate_box">
			    <h2 class="user_plate_title_min">详细数据</h2>
			    <div class="tj_data_info"> 
				    <div class="break_5"></div>	
				    <div class="fl" id="dayCount">
					    <span>搜索:</span>
                        <a href="#" class="on" onclick="Search(1,0)">全部</a>
					    <a href="#" onclick="Search(1,7)">一周</a>
					    <a href="#" onclick="Search(1,30)">一月</a>
					    <a href="#" onclick="Search(1,365)">一年</a>
				    </div>
                     <input class="input" value="起始日期" type="text" id="beginTime">
				     <font>至</font>
                     <input class="input" value="结束日期" type="text" id="endTime"> 
				     <input class="button_blue hover" onclick="Search(2,0)" value="搜索" type="submit"> 
				    <div class="break_10"></div> 
			    </div>

			    <div class="break_20"></div>
			    <table class="tb" border="0" cellpadding="0" cellspacing="0">
           		    <thead>
            	    <tr style="background:#fafafa!important">
					    <td colspan="6"><font style="font-size:18px;font-weight:bold;font-family:'微软雅黑';color:#666;text-align:left!important">全部收益明细</font></td>
				    </tr>
                    </thead>
				    <thead class="user_table_title">
					    <tr>
						    <th width="120">现金购买利息</th>
						    <th width="140">虚拟本金购买利息</th>
						    <th width="120">奖励利息</th>
						    <th width="120">总金额</th>
                       	    <th width="120">操作</th>
					    </tr>
				    </thead>				
				    <tbody><tr>
					    <td class="user_table_green" id="CashInterest"><%=model.CashInterest.ToMoney(2)%>元</td>
					    <td class="user_table_green" id="VirtualInterest"><%=model.VirtualInterest.ToMoney(2)%>元</td>
					    <td class="user_table_green" id="RewardInterest"><%=model.RewardInterest.ToMoney(2)%>元</td>
					    <td class="user_table_green" id="SumInterest"><%=model.SumInterest.ToMoney(2)%>元</td>
                        <td class="user_table_edit">
                            <a href="/user/totalrevenuedetails" class="button_blue">查看明细</a>
                        </td>
				    </tr>	
			    </tbody></table>
		    </div>
	    </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript">
        $(function () {
            var _temp = $("#dayCount a");
            $("#dayCount a").mouseover(function () {
                _temp.removeClass();
                $(this).addClass("on");
            });
        })
        function Search(way, datas) {
            var datas ;
            switch (way) {
                case 1: {
                    var datas = { SearchWay: way, Datas: datas};
                    break;
                }
                case 2: {
                    var datas = { SearchWay: way, Datas: 0, BeginDate: $("#beginTime").val(), EndDate: $("#endTime").val() };
                    break;
                }
            }
            $.post("/Submit/Totalrevenue", datas, function (result) {
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
                    $("#CashInterest").html(result.Tag.CashInterest.toFixed(2) + "元");
                    $("#VirtualInterest").html(result.Tag.VirtualInterest.toFixed(2) + "元");
                    $("#RewardInterest").html(result.Tag.RewardInterest.toFixed(2) + "元");
                    $("#SumInterest").html(result.Tag.SumInterest.toFixed(2) + "元");
                    return true;
                }
            });
        }
    </script>
</asp:Content>
