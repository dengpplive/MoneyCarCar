<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%SystemBankCard bank = (SystemBankCard)ViewBag.Bank; %>
    <div class="right">
        <div class="user_right_content">
		    <h2 class="user_plate_title">银行卡管理
                <%if(bank==null){%>>
			    <span class="title_hot_ico">您还没有绑定银行卡</span>
                <%}else{%>
                <span class="title_wd_ico">您已经绑定了银行卡</span>
                <%}%>
		    </h2>
		
		    <div class="user_plate_box">
                <%if(bank==null){%>
			    <ul class="bank_card_list">
				    <a href="#" class="add_bank_card" onclick="bank_form_read();"><span class="hover">+</span>绑定银行卡</a>
			    </ul>
                <%}else{%>
                <%--<ul class="bank_card_list">
				   <li> <span>卡号：<%=bank.BankCardNumber%></span></li>
                   <li> <span>姓名：<%=bank.OpenAnAccountUser%></span></li>
                   <li> <span>开户行：<%=bank.OpenAnAccountBankCard%></span></li>
                   <li> <span>开户地址：<%=bank.OpenAnAccountAdd%></span></li>
                    <li> <span><a onclick="return unbank_form_read()" href="#">解除绑定</a></span></li>
			    </ul>--%>
                <ul class="bank_card_list">
            	<li>
                	<span class="bank_name"><span><%=bank.OpenAnAccountBankCard%></span></span><a href="#" onclick="return unbank_form_read()" class="bank_clear">解锁</a>
                    <div class="bank_card"><span><%=bank.BankCardNumber%></span></div>
                    <p class="bank_address"><%=bank.OpenAnAccountAdd%></p>                    
                    <p  class="bank_name_r"><%=bank.OpenAnAccountUser%></p>
                </li>
            </ul>

                <%}%>

			    <div class="break_10"></div>
			    <!-- 添加新的收货地址 start-->
			    <div class="clear"></div> 
			    <!-- 添加新的收货地址 end-->
		    </div>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script type="text/javascript">
        function bank_form_read() {
            $.post("/Submit/BindBankCard/", {}, function (data) {
                if (data.IsSeccess) {
                    if (data.ErrorCode == -2) {
                        location.href = "/Home/Login";
                        return false;
                    } else {
                        ToPayForm(data);
                    }
                } else {
                    alert(data.msg);
                }
            }, 'json');
        }

        function unbank_form_read() {
            $.post("/Submit/UnBindBankCard/", {}, function (data) {
                if (data.IsSeccess) {
                    if (data.ErrorCode == -2) {
                        location.href = "/Home/Login";
                        return false;
                    } else {
                        ToPayForm(data);
                    }
                } else {
                    alert(data.msg);
                }
            }, 'json');
        }
    </script>
</asp:Content>
