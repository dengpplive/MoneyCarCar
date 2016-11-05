<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YeePayControllerTest.aspx.cs" Inherits="MoneyCarCar.DataApi.YeePayControllerTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        唯一商户号：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        &nbsp;

        请求流水号：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

        <asp:Button ID="Button2" runat="server" Text="请求到易宝" OnClick="Button2_Click" />
    
        <asp:Button ID="Button3" runat="server" Text="Notify 测试" OnClick="Button3_Click" />

         <input type="button" value="清空" onclick="document.getElementById('txtReturnValue').value = ''">
    
        <br />

        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="4" Width="100%">
        <asp:ListItem Selected="True" Text="无" Value="0"></asp:ListItem>
        <asp:ListItem Text="转款记录" Value="1"></asp:ListItem>    
        <asp:ListItem Text="提现记录 " Value="2"></asp:ListItem>
        <asp:ListItem Text="充值记录" Value="3"></asp:ListItem>

        </asp:RadioButtonList>

        方法：<asp:RadioButtonList ID="rblPayType" runat="server" RepeatColumns="4" Width="100%">
        <asp:ListItem Selected="True" Text="无" Value="0"></asp:ListItem>
        <asp:ListItem Text="21注册o" Value="21"></asp:ListItem>    
        <asp:ListItem Text="22充值o" Value="22"></asp:ListItem>
        <asp:ListItem Text="23提现o" Value="23"></asp:ListItem>
        <asp:ListItem Text="24绑卡o" Value="24"></asp:ListItem>
        <asp:ListItem Text="25取消绑卡o" Value="25"></asp:ListItem>
        <asp:ListItem Text="26企业用户注册o" Value="26"></asp:ListItem>
        <asp:ListItem Text="27(1)转账[TRANSFER]o" Value="271"></asp:ListItem>
        <asp:ListItem Text="27(2)投标[TENDER]o" Value="272"></asp:ListItem>
        <asp:ListItem Text="27(3)还款[REPAYMENT]o" Value="273"></asp:ListItem>
        <asp:ListItem Text="27(4)债权转让[CREDIT_ASSIGNMENT]o" Value="274"></asp:ListItem>
        <asp:ListItem Text="28自动投标授权o" Value="28"></asp:ListItem>
        <asp:ListItem Text="29自动还款授权o" Value="29"></asp:ListItem>
        <asp:ListItem Text="31账户查询(及时返回)o" Value="31"></asp:ListItem>
        <asp:ListItem Text="32资金冻结(及时返回)o" Value="32"></asp:ListItem>
        <asp:ListItem Text="33资金解冻(及时返回)o" Value="33"></asp:ListItem>
        <asp:ListItem Text="34直接转账o" Value="34"></asp:ListItem>
        <asp:ListItem Text="35自动转账授权o" Value="35"></asp:ListItem>
        <asp:ListItem Text="36单笔业务查询(及时返回)o" Value="36"></asp:ListItem>
        <asp:ListItem Text="37 转账确认o" Value="37"></asp:ListItem>
              <asp:ListItem Text="310 对账o" Value="310"></asp:ListItem>

        </asp:RadioButtonList>
        请求易宝的表单数据：<br />
        <asp:TextBox ID="txtReturnValue" runat="server" BackColor="Black" ForeColor="Lime" Height="500px" Rows="50" TextMode="MultiLine" Width="100%"></asp:TextBox>
        <br />
        <br />
        &nbsp;<br />
    
        <br />
    
    </div>
    </form>
</body>
</html>
