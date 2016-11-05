<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        MoneyCarCar.Models.Statisticals.Return.Earnings_Return model = (MoneyCarCar.Models.Statisticals.Return.Earnings_Return)ViewBag.Result;
        System.Data.DataTable[] datas = ViewBag.Record as System.Data.DataTable[];
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
                    <a href="#" class="tj_info_list tj_info_ico_1"><span></span><b>总投资: <%=model.TotalInvestment.ToMoney(2)%>元</b></a>
                </li>
				<li class="tj_info_list_on hover">
                    <a href="/user/totalrevenuedetails" class="tj_info_list tj_info_ico_3"><span></span><b>收益明细</b></a>
                </li>
			</ul>
		</div> 
			<!--投资统计 end-->

		<div class="break_30"></div>
		<div class="user_plate_box">
			<h2 class="user_plate_title_min">详细数据</h2>
			<div class="ztj_data_info"> 
				<div class="break_5"></div>	
				<div class="fl">
					<span>投资总金额:<em class="user_table_red"><%=datas[0].Rows[0][1].ToDecimal().ToMoney(2)%>元</em></span>
                    <span>投资总产品:<em class="user_table_red"><%=datas[0].Rows[0][0].ToInt()%>种</em></span>
					<span>投资总份数:<em class="user_table_red"><%=datas[0].Rows[0][2].ToInt()%>份</em></span>
				</div>
				<div class="break_10"></div> 
			</div>

			<div class="break_20"></div>
			<table class="tb" border="0" cellpadding="0" cellspacing="0">
				<thead class="user_table_title">    
					<tr>
						<th width="100">投资日期</th>
						<th width="120">产品名称</th>
						<th width="100">投资份数</th>
						<th width="100">投资金额</th>
						<th width="100">状态</th>
					</tr>
				</thead>				
				<tbody>
                    <%if (datas[1].Rows.Count <= 0){%>
                        <tr>
                            <td colspan="5"><font class="font_red font_max">暂无数据</font></td>
                        </tr>
                        <%}else{foreach (System.Data.DataRow item in datas[1].Rows){%>
                        <tr>
					        <td class="user_table_gray"><%=item[0]%></td>
					        <td class="user_table_gray"><%=item[1]%></td>
					        <td class="user_table_gray"><%=item[2]%>份</td>
					        <td class="user_table_red"><%=Convert.ToDecimal(item[3]).ToMoney(2)%>元</td>
					        <td class="user_table_gray"><%=item[4]%></td>  
                        </tr>
                        <%}}%>	
			    </tbody>
			</table>
		</div>
	</div>
        </div>
</asp:Content>
