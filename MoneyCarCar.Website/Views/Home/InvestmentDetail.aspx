<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!--banner start-->
	<div class="banner">
	<ul class="list">
		<li class="bg1" style="display:block" >
			<a href="###" target="_blank"><img src="/Contents/images/banner/banner.jpg" alt="" /></a>
		</li>
		<li class="bg2">
        	<a href="###" target="_blank"><img src="/Contents/images/banner/banner.jpg" alt="" /></a>
		</li>
		<li class="bg3">
			<a href="###" target="_blank"><img src="/Contents/images/banner/banner.jpg" alt="" /></a>
		</li>
	</ul>
	<ul class="btn hover"></ul>
</div>
	<!--banner end-->
    <%MoneyCarCar.Models.SystemClaims model = ViewBag.Model; double days = ViewBag.Days;%>
    <div class="path_link" style="margin-bottom:-10px;"><a href="/" title="首页">首页</a><span>/</span><a href="/Home/Investment" title="投资">投资</a><span>/</span><%=model.Title %></div>
	<div class="wrap_1200">
		<div class="products_content">	
			<div class="invest_left fl">
				<div class="wrap_white products_show">
					<div class="products_show_left fl">
						<div class="products_img ">
							<img src="<%=model.TitleImagePath %>"  class="fl" width="240" height="276"/>
							<div class="products_img_tag_box">
								<span class="imt_tag_bao">担保物</span>
								<%--<span class="imt_tag_shou no_hover_right_tips" title="可获得销售奖励">可售46万</span>--%>
							</div>
						</div>
                        <%if (ViewBag.IsFull)
                          {
                        %>
						<ul class="end_time_over">
							<b>投资期已结束</b>
						</ul>
                        <%}
                          else
                          {
                        %>
                        <ul class="end_time_box" data-seconds="<%=((int)(Convert.ToDateTime(model.EarningsStartTime)-DateTime.Now).TotalSeconds)%>">
							<b>可投时间:</b>
						    <span>-</span>天
						    <span>-</span>时
						    <span>-</span>分
						    <span>-</span>秒
						</ul>
                        <%
                          }
                        %>
					</div>

                   	<div class="products_title fl">
						<a class="products_link fl" title="<%=model.Title %>" style="font-size:19px"><%=model.Title %></a>
						<div class="products_title_tag">
                            
                            <%
                                List<MoneyCarCar.Models.IconModel> icons = MoneyCarCar.Commons.ExpandHelper.ToModel<List<MoneyCarCar.Models.IconModel>>(model.Icons);
                                foreach (MoneyCarCar.Models.IconModel icos in icons)
                                {
                            %>
                            <span class="tag_ico tag_<%=icos.StyleName%> fr no_hover_top_tips" title="<%=icos.Description%>"><%=icos.Title%></span>
                            <%
                                }
                            %>
						</div>
						<div class="break_10"></div>
					</div>

					<div class="products_data">
						<div class="products_money_info">
							<p class="fl" style="width:40%;border-right:2px dashed #ddd">
								<span>借款金额</span>
								<b><font>¥</font><%=model.LoanAmount.ToString("#,##0.00") %></b>
							</p>
							<p class="fl" style="width:20%;padding-left:10%;border-right:2px dashed #ddd">
								<span>年化利率</span>
								<b><%=model.APR %>%</b>
							</p>
							<p class="fr" style="width:26%">
								<span>借款期限</span>
								<b><%=model.LoanPeriod %>个月</b>
							</p>
							<div class="break_15"></div>
						</div>
						<div class="products_data_list">
								<span class="fl">投资总分数: <b><%=model.LoanAmount/model.SingleAmount %>份</b></span>
								<span class="fr">单份投资金额: <b><%=model.SingleAmount.ToString("#,##0.00")%>元</b></span>
								<div class="border_line"></div>
								<span class="fl">担保方式: <font class="font_red">第三方本息担保+货物抵押反担保</font></span>
								<span class="fr">还款方式: <font class="font_red">每日付息,销售或到期还本金</font></span>
								<div class="border_line"></div>
								<%--<span class="fl">可售价格: <b>46万</b></span>
								<span class="fr">销售提成: <b class="font_red">2000元</b></span>
								<div class="border_line"></div>--%>
								<span class="fl">投资结束日期: <font><%=model.EarningsStartTime %></font></span>
								<span class="fr">收益起计日期: <font><%=model.EarningsStartTime %><%=ViewBag.IsFull?"":" 或 <font class=\"font_red no_hover_top_tips\" title=\"一旦投满,立即发息(工作时间),不用等待投资期结束\">满标即发息</font>" %></font></span>
								<div class="border_line"></div>
								<span class="fl">借款公司: <font><%=model.Borrower %></font></span>
								<span class="fr">单份质押物规格: <font><%=model.PawnSpec%></font></span>
							</div>
					</div>

					<div class="clear"></div>
				</div>
				<div class="break_20"></div>
				<div class="content_max">
                	 <ul class="touzi_nav_box">
                    	<li><a class="current2">担保物详情</a></li>
                        <li><a href="###">借款方介绍</a></li>
                        <li><a href="###">库房展示</a></li>
                        <li><a href="###">相关资料</a></li>
                        <li><a href="###">担保方</a></li>
                    </ul>
                    <div class="touzhi_content"><%=model.DetailsImages1%></div>
					<div class="touzhi_content undis"><%=model.DetailsImages2%></div>
                    <div class="touzhi_content undis"><%=model.DetailsImages3%></div>
                    <div class="touzhi_content undis"><%=model.DetailsImages4%></div>
                    <div class="touzhi_content undis"><%=model.DetailsImages5%></div>
				</div>
                
			</div>
			<!--投资 left end-->

			<!--投资 right start-->
			<div class="invest_right fr" id="invest_right">
                <div class="invest_form">
                    <h1 class="invest_name" title="<%=model.Title %>"><%=model.Title %></h1>
                        <div class="invest_form_content box_shadow">
                            <P><span class="fl">投资进度:</span><span class="fr"><%=ViewBag.Already%>%</span><div class="break_5"></div></P>
                            <div class="products_step_box">
                                <div class="products_step_loading"style="width:<%=ViewBag.Already%>%"></div>
                            </div>
                            <div class="break_15"></div>
                            <p class="invest_num_button"><span>可投份数</span><b><%=ViewBag.AllCanBuy%>/<strong class="font_red"><%=ViewBag.CanBuy%></strong></b>份</p>
                            
                            <ul class="xnbj">
                            	<li>账户余额<span><%=ViewBag.HaveMoney.ToString("#,##0.00")%>元<a href="###" target="_blank" class="account_prepaid">充值</a></span> </li>
                                <li class="no_b">虚拟本金<span><em>¥</em><%=ViewBag.HaveVirtualMoney.ToString("#,##0.00")%></span></li>
                            </ul>
                            <div class="break_5"></div>
                            
                            <p class="yqsy">预期收益: <em id="daymoney">0.000</em>元<span class="question_ico hover no_hover_tips" title="预期收益计算方式:
投资金额 * <%=model.APR%>%（结果取小数点后2位）/ 365天（结果取小数点后2位）* 投资期限<%=days%>天（结果取小数点后2位）">?</span></p>
                            
                            <form name="transactionact" id="transactionact" method="post" action="/Home/TransactionAct" onsubmit="return form_invest(); ">		
                                <%if(!ViewBag.IsFull){%>
                                <p class="yp"><input id="IsUserBounty" name="IsUserBounty" type="checkbox" value="1" class="va"/><label for="IsUserBounty">使用虚拟本金</label></p>
                                <div class="break_10"></div>
                                <div class="clear"></div>
                                <div class="invest_number fl">
                                    <label>数量<input type="text" id="J_input" class="J_input" value="0" autocomplete="off" name="BuyCount"></label>
                                    <span class="J_minus" title="数量减1"></span>
                                    <span class="J_add" title="数量加1"></span>
							    </div>
                          	    <span class="investmentmoney  font_red fr">投资总金额: <span id="investmentmoney">0.00</span>元</span>
								<div class="break_10"></div>
                                <%}%>
								<div class="border_line"></div>
                                <div class="break_10"></div>
                                <%if(!ViewBag.IsFull){%>
                                <input class="button_blue hover" type="submit" name="submit" value="立即投资" />
                                <input type="hidden" value="<%=model.SingleAmount%>" id="sharemoney" name="sharemoney" />
                                <input type="hidden" value="<%=model.APR%>" id="investmentinterest" name="investmentinterest" />
                                <input type="hidden" value="<%=days%>" id="days" name="days" />
                                <input type="hidden" value="<%=model.ID%>" id="InvestorsID" name="InvestorsID" />
                                <input type="hidden" value="-1" id="BountyCount" name="BountyCount" />
                                <%}else{%>
                                <span class="button_false">日息发放中</span>
                                <%}%>
						    </form>
                        </div>
                    </div>
                <div class="invest_right_content box_shadow" style="border-width:0 1px 1px 1px;display:none">
                    <div class="break_10"></div>
                    <div class="invest_right_content_border">
                        <!--产品销售情况 start-->
                        <div clas="clear"></div>
                        <h1 class="invest_sale_title">合作渠道</h1>
                        <ul class="invest_sale">
                            <div class="clear"></div>
                        </ul>
                        <!--产品销售情况 end-->
                        <div class="break_20"></div>
                    </div>
                </div>
			</div>
			<!--投资 right end-->
		</div>
	</div>
    <%if(!ViewBag.IsLogin){%>
    <!--login 弹窗 start-->
    <div class="login_box fr" id="login_form" style="display:none">
	    <div class="login_box_content ">
		    <div class="login_title"><b class="fl">登录</b><span class="fr">没有账号? 点击进行<a href="/Home/Reg">注册</a></span></div>
		    <div class="clear"></div>
		    <div class="border_line"></div>
		    <div class="break_10"></div>
		    <form class="registerform" id="js-login_form" onsubmit="login(); return false;">
                <input type="hidden" name="token" id="token" value="98bb152174fc3bc2">
			    <p class="input_lable">账户名</p>
			    <p class="input_and_button">
                    <input value="" id="UserNameOrPhone" type="text" class="input_text hover" value="用户名/手机号" onfocus="if(this.value=='用户名/手机号'){this.value=''}" onblur="if(this.value==''){this.value='用户名/手机号'}" datatype="e6-18" errormsg="6到18个字符,只能是字母,数字,下滑线!" sucmsg="&nbsp;" nullmsg="请填写账户名!"/>
                </p>

			    <p class="input_lable">密码</p>
			    <p class="input_and_button">
                    <input type="password" class="input_text hover" id="UserPassword" datatype="*6-18" errormsg="密码为6到18个字符!" nullmsg="请填写密码"  sucmsg="&nbsp;"/>
                </p>

			    <p class="input_lable">验证码</p>
			    <p class="input_code input_and_button Validform_right_3">
				    <input type="text" class="input_text hover" datatype="e5-5" errormsg="验证码位数不正确,为5位!" sucmsg="&nbsp;" id="VcodeEl" nullmsg="请填写验证码!" name="code"/><img src="/Home/ValidateCode" height="34" onclick="return code('js-mail_vcode_img','/Home/ValidateCode')" title="点击换一张" id="js-mail_vcode_img"/>
			        <a href="javascript:code('js-mail_vcode_img','/Home/ValidateCode')">
                    <span class="refresh_ico"></span>换一张</a>
			    </p>

			    <div class="break_20"></div>
			    <input type="button" name="submit" class="button_blue hover" value="登录" onclick="login()">
			    <div class="break_10"></div>
			    <p><a href="/index.php?c=login&a=findpwd" class="fr">忘记密码?</a></p>
		    </form>
		    <div class="break_10"></div>
	    </div>
    </div>
	<!--login 弹窗 end-->
    <%}%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptionContent" runat="server">
    <%MoneyCarCar.Models.SystemClaims model = ViewBag.Model; %>
    <!--倒计时-->
    <script type="text/javascript" src="/Contents/js/Countdown.js"></script>
    <script type="text/javascript" src="/Contents/js/jquery.poshytip.min.js"></script>
    <script type="text/javascript" src="/Contents/js/other.js"></script>	<!-- 弹窗组件-->
	<script type="text/javascript" src="/Contents/js/artDialog.js?skin=opera"></script>
    <script type="text/javascript" src="/Contents/js/jquery.form.js"></script>
    <script type="text/javascript">
        var singleAmount = parseFloat('<%=model.SingleAmount%>');
        var canBuy = parseInt('<%=ViewBag.CanBuy%>');
        var apr = parseInt('<%=model.APR%>') / 100.00;

        //tab 切换
        var tabs = function (e1, e2, e3) {
            var e1 = $('a', e1);
            var e2 = $(e2);
            e1.mouseover(function () {
                if (!$(this).hasClass('current2')) {
                    e1.removeClass('current2');
                    $(this).addClass('current2');
                    var idx = e1.index(this);
                    e2.hide();
                    $(e2[idx]).show();
                    $(e3).attr('href', $(this).attr('href'));
                }
            });
            e1.click(function () {
                return false;
            })
        }

        $(function () {
            $('.end_time_box').countdown(function (s, d) {
                var items = $(this).find('span');
                //items.eq(0).text(d.total);
                items.eq(0).text(d.day);
                items.eq(1).text(d.hour);
                items.eq(2).text(d.minute);
                items.eq(3).text(d.second);
            });
            tabs('.content_max .touzi_nav_box', '.touzhi_content');
            //投资数量加减  Max:为最大数值
            $('.invest_number').iVaryVal({ Max: canBuy }, function (value, index) { });

        });
        
        <%if(ViewBag.IsLogin){%>
        //投资表单验证
        function getObj(id) {
            var Obj = document.getElementById(id).value;
            return Obj;
        }
        function form_invest() {
            if (getObj("J_input") == "0") {
                //$.sticky('您还没有填写投资数量', {
                //    speed: 'fast', autoclose: 5000
                //});
                alert("您还没有填写投资数量");
                return false;
            }
        }
        <%}else{%>
        //异步登录
        function login() {
            var datas = { UserNameOrPhone: $("#UserNameOrPhone").val(), UserPassword: $("#UserPassword").val(), UserIP: "", UserType: 1, Vcode: $("#VcodeEl").val() };
            $.post("/Submit/Login/", datas, function (result) {
                if (result == 1) {
                    location.reload();
                }
                else if (result == -1) {
                    alert("验证码错误");
                    VcodeEl.value = "";
                    VcodeEl.focus();
                    return false;
                }
                else {
                    $.sticky(result, { speed: 'fast', autoclose: 5000 });
                    return false;
                }
            });
        }
        //弹出登录框
        function loginform() {
            art.dialog({
                title: "",
                lock: true,
                opacity: 0.2,
                fixed: true,
                padding: 0,
                content: document.getElementById('login_form')
            });
        }
        //登录验证
        function form_invest() {
            loginform();
            return false;
        }
        <%}%>
    </script>
</asp:Content>
