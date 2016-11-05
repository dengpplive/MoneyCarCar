$(function () {
    //静态表格分页
    var tabId = "gvflyer";//需要分页的表格ID
    var PagerContainer = "pagerarea";//分页显示的位置            
    var listNum = 10;//分页大小    
    var PageNum = 4;//分页链接接数(5个)
    //加载
    upPage(0);//初始化
    //单击分页显示
    function upPage(nowPage) {
        $.get("/Submit/GetApplyRecord?page=" + (nowPage + 1) + "&rows=" + listNum, function (data, status, res) {
            try {
                //处理数据
                data = JSON.parse(data);
                var total = data.total;
                var rows = data.rows;
                var html = '';
                var trHead = $("#" + tabId + " tr:first");
                var html = '<tr>' + trHead.html() + "</tr>";
                $.each(rows, function (i, d) {
                    var status = "";
                    if (d.BorrowerType == 1) {
                        status = "未审核";
                    } else if (d.BorrowerType == 2) {
                        status = "正在审核";
                    } else if (d.BorrowerType == 3) {
                        status = "未通过";
                    } else if (d.BorrowerType == 4) {
                        status = "已通过";
                    }
                    html += '<tr>' +
                             '<td>' + d.BorrowerName + '</td>' +
                             '<td>' + d.LoanAmount + '</td>' +
                             '<td>' + d.BorrowerTime + '</td>' +
                             '<td>' + status + '</td>' +
                             '<td>' + d.BorrowerReason + '</td>' +
                             '<td>' + d.CollateralDesc + '</td>' +
                    '</tr>';
                });
                //当前页显示的数据
                $("#" + tabId).html(html);
                //总页数           
                var PagesLen = Math.ceil(total / listNum);

                //分页链接变换
                strS = '<a href="###" page="0" >首页</a>&nbsp;&nbsp;';
                var PageNum_2 = PageNum % 2 == 0 ? Math.ceil(PageNum / 2) + 1 : Math.ceil(PageNum / 2);
                var PageNum_3 = PageNum % 2 == 0 ? Math.ceil(PageNum / 2) : Math.ceil(PageNum / 2) + 1;
                var strC = "", startPage, endPage;
                if (PageNum >= PagesLen) { startPage = 0; endPage = PagesLen - 1; }
                else if (nowPage < PageNum_2) { startPage = 0; endPage = PagesLen - 1 > PageNum ? PageNum : PagesLen - 1; } //首页
                else { startPage = nowPage + PageNum_3 >= PagesLen ? PagesLen - PageNum - 1 : nowPage - PageNum_2 + 1; var t = startPage + PageNum; endPage = t > PagesLen ? PagesLen - 1 : t; }
                for (var i = startPage; i <= endPage; i++) {
                    if (i == nowPage) strC += '<a href="###" style="color:red;font-weight:700;" page="' + i + '">' + (i + 1) + '</a>&nbsp;'
                    else strC += '<a href="###" page="' + i + '" >' + (i + 1) + '</a>&nbsp;'
                }
                strE = '&nbsp;<a href="###" page="' + (PagesLen - 1) + '"  >尾页</a>&nbsp;&nbsp;';
                strE2 = parseInt(nowPage + 1) + "/" + PagesLen + "页" + "&nbsp;&nbsp;共" + total + "条";
                var footerHtml = strS + strC + strE + strE2;
                $("#" + PagerContainer).html(footerHtml);
                $("#" + PagerContainer + " a").click(function () {
                    var pindex = parseInt($(this).attr("page"));
                    upPage(pindex);
                });
                trHead.show();
            } catch (e) {
                alert(e.message);
            }
        });
    }
});
