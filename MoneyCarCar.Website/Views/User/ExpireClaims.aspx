<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="right">
        <div class="user_right_content">
        <h2 class="user_plate_title">过期产品</h2>
		<%--<h2 class="user_plate_title">已投产品<span class="title_hot_ico">共0款产品</span></h2>--%>
		<%
            System.Data.DataTable datas = ViewBag.Record as System.Data.DataTable;
        %>
		<div class="user_plate_box">
            <div class="tj_data_info"> 
                <div class="break_5"></div> 
                <div class="fl">
                    <%--<span>筛选:</span>
                    <a href="###" class="on">全部产品</a>
                    <a href="###">发息中产品</a>
                    <a href="###">已还本金产品</a>--%>
                </div>
                <%--<a class="button_blue fr" href="javascript:void(0)">利息一键确认</a>--%>
                <%--<div class="clear"></div>--%> 
            </div>
            <div class="clear"></div> 
			<table class="tb" border="0" cellpadding="0" cellspacing="0">
				<thead>
					<tr>
						<th width="156">质押物图片</th>
						<th width="150">产品名称</th>
						<th width="120">到期时间</th>
						<th width="120">投资份数</th>
						<th width="120">剩余份数</th>
						<th width="260">状态</th>
					</tr>
				</thead>
				 <tbody>
                     <%if (datas.Rows.Count <= 0) { %>
                     <tr><td colspan="7"><font class="font_red font_max">暂无数据</font></td></tr>
                     <%}else {foreach (System.Data.DataRow item in datas.Rows){%>
                     <tr>
                        <th width="156"><img src="<%=item[0]%>" width="118" height="118" /></th>
						<th width="150"><%=item[1]%></th>
						<th width="120"><%=item[2]%></th>
						<th width="120"><%=item[3]%></th>
						<th width="120"><%=item[4]%></th>
						<th width="260"><%=item[5]%></th>
                    </tr>
                     <%}}%>
			</tbody></table>
              <div class="break_30"></div>        
            	 
	</div>
 			</div>
        </div>
</asp:Content>
