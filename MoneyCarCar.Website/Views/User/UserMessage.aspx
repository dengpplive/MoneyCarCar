<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UserMessage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="right">
        	<div class="user_right_content">
					<!--信息提醒 操作 strat-->
					<div class="user_msg">
						<h2 class="user_plate_title">系统信息<span class="title_wd_ico">0条未读</span></h2>
						<table border="0" cellpadding="0" cellspacing="0" class="tb">
							<tr>
								<th width="10%">状态</th>
								<th width="60%">信息内容</th>
								<th width="15%">时间</th>
								<th width="15%">操作</th>
							</tr>	
						</table>
        			<div class="break_30"></div>                   
    					</div>
					<!--信息提醒 操作end-->
			</div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SEOContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
</asp:Content>
