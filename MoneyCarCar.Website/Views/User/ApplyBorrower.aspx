<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Users.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Scription" ContentPlaceHolderID="ScriptionContent" runat="server">
    <script src="../../Contents/js/js_form.js"></script>
    <script type="text/javascript">
        $(function () {
            var postLastTime = null;
            $("#btnSubmit").click(function () {
                try {
                    //alert(new Date().getTime());
                    //return;
                    var form = getFormData('tixian', true);
                    var errMsg = "";
                    if ($.trim(form.BorrowerName) == "") {
                        errMsg = "借款人姓名不能为空";
                    }
                    else if ($.trim(form.LoanAmount) == "") {
                        errMsg = "借款金额不能为空！";
                    } else if ($.trim(form.BorrowerReason) == "") {
                        errMsg = "借款原因不能为空！";
                    } else if ($.trim(form.CollateralDesc) == "") {
                        errMsg = "抵押物描述不能为空！";
                    }
                    if (errMsg == "" && parseInt(form.LoanAmount, 10) % 100 != 0) {
                        errMsg = "借款金额需为100的整数倍！";
                    }
                    var b1 = (/^(([1-9]\d*)|\d)(\.\d{1,2}(0{0,2})?)?$/).test(parseInt(form.LoanAmount, 10)) && parseInt(form.LoanAmount, 10) > 0;
                    if (!b1) {
                        errMsg = "输入借款金额错误";
                    }
                    if (errMsg != "") {
                        alert(errMsg);
                        return;
                    }
                    var url = '/Submit/ApplyBorrower';

                    //限制点击过快
                    //if (postLastTime != null) {
                    //    if ((new Date().getTime() - postLastTime) < 1000) {
                    //        return;
                    //    }
                    //}
                    postLastTime = new Date().getTime();
                    $.post(url, form, function (data) {
                        data = JSON.parse(data);
                        alert(data.message);
                    });
                } catch (e) {
                    alert(e.message);
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="right">
        <div class="user_right_content">
            <h2 class="user_plate_title">申请债权</h2>
            <form method="post" id="tixian" class="tixian_form">
                <table border="1" cellpadding="0" cellspacing="0" class="user_table_tixian">
                    <tbody>
                        <tr>
                            <td class="user_table_text_right" style="width: 150px;"><strong>*</strong>借款人姓名:</td>
                            <td style="padding: 10px">
                                <input name="BorrowerName" type="text" class="input" datatype="n10" placeholder="借款人姓名"></td>
                        </tr>
                        <tr>
                            <td class="user_table_text_right"><strong>*</strong>借款金额:</td>
                            <td style="padding: 10px">
                                <input name="LoanAmount" type="text" value="" class="input" placeholder="借款金额">
                            </td>
                        </tr>
                        <tr>
                            <td class="user_table_text_right"><strong>*</strong>借款原因:</td>
                            <td style="padding: 10px">
                                <textarea name="BorrowerReason" name="" rows="3" class="input" style="width: 400px;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="user_table_text_right"><strong>*</strong>抵押物描述:</td>
                            <td style="padding: 10px">
                                <textarea name="CollateralDesc" rows="3" class="input" style="width: 400px;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 25px 14px; text-align: center!important; background: #f9f9f9"><a href="javascript:void(0);" id="btnSubmit" class="button_blue" style="padding: 12px 152px; font-size: 14px">申请债权</a></td>
                        </tr>
                    </tbody>
                </table>
            </form>
        </div>
    </div>
</asp:Content>


