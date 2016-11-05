<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Scription" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript" src="../../Contents/js/js_Pagination.js"></script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="right">
        <div class="user_right_content">
            <h2 class="user_plate_title">债权申请记录</h2>
            <div class="break_10"></div>
            <%--数据--%>
            <table border="0" cellpadding="0" cellspacing="0" class="tb" id="gvflyer">
                <tr>
                    <th>借款人姓名</th>
                    <th>借款金额</th>
                    <th>申请时间</th>
                    <th>申请状态</th>
                    <th>借款原因</th>
                    <th>抵押物描述</th>
                </tr>
            </table>
            <div class="break_30"></div>
            <%--分页区域--%>
            <div id="pagerarea">
                <span class="current">1</span><span class="total">共 1 页</span>
            </div>
        </div>
    </div>
</asp:Content>

