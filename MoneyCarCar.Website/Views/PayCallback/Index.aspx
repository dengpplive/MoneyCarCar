<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--header end-->
	<div class="wrap_1200_content">   
    	<div class="Success_content">
        	<div class="<%=ViewBag.ResultStatu?"Success_icon":"Error_icon"%>"></div>
            <div class="Success_info">
            	<p><span></span></p>
                <h2><%=ViewBag.Message%></h2>
                <p class="s_mt"><a href="/" class="S_index_btn" title="返回首页">返回首页</a> </p>
            </div>
        </div> 	
	</div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript">
        $(document).ready(
        function () {
            var doc = document, inputs = doc.getElementsByTagName('input'), supportPlaceholder = 'placeholder' in doc.createElement('input'), placeholder = function (input) {
                var text = input.getAttribute('placeholder'), defaultValue = input.defaultValue;
                if (defaultValue == '') { input.value = text } input.onfocus = function () { if (input.value === text) { this.value = '' } }; input.onblur = function () { if (input.value === '') { this.value = text } }
            };
            if (!supportPlaceholder) { for (var i = 0, len = inputs.length; i < len; i++) { var input = inputs[i], text = input.getAttribute('placeholder'); if (input.type === 'text' && text) { placeholder(input) } } }
        });
    </script>
</asp:Content>
