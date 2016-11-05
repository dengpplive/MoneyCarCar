<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Scription" ContentPlaceHolderID="ScriptionContent" runat="server">
	<!-- 动画控制 -->
	<script type="text/javascript" src="/Contents/js/wow.min.js"></script>
	<script type="text/javascript" src="/Contents/js/iframeTools.js"></script>
	<!--表单验证插件-->
	<script type="text/javascript" src="/Contents/js/Validform_v5.3.2_min.js"></script> 
    <!-- 验证码和短信倒计时 重要-->
    <script type="text/javascript" src="/Contents/js/jquery.form.js"></script>
	<!-- 其他效果 函数 重要-->
	<script type="text/javascript" src="/Contents/js/other.js"></script> 
	<!--tips 提示框 重要-->
	<script type="text/javascript" src="/Contents/js/jquery.poshytip.min.js"></script>
    <!--图片懒加载-->
    <script type="text/javascript" src="/Contents/js/jquery.lazyload.js"></script>
    <!--倒计时-->
    <script type="text/javascript" src="/Contents/js/Countdown.js"></script>
    <script type="text/javascript">
        //返回顶部
        $(function () {
            $(window).scroll(function () {
                if ($(window).scrollTop() > 400) {
                    $("#back_top").fadeIn(400);
                }
                else {
                    $("#back_top").fadeOut(400);
                }
            });
            $("#back_top").click(function () {
                $('body,html').animate({ scrollTop: 0 }, 500);
                return false;
            });
        });
        function login() {
            var datas = { UserNameOrPhone: $("#UserNameOrPhone").val(), UserPassword: $("#UserPassword").val(), UserIP: "", UserType: 1, Vcode: $("#VcodeEl").val() };
            $.post("/Submit/Login/", datas, function (result) {
                if (result == 1) {
                    location.href = "/";
                }
                else if (result == -1) {
                    alert("验证码错误");
                    code('js-mail_vcode_img', '/Home/ValidateCode');
                    VcodeEl.value = "";
                    VcodeEl.focus();
                    return false;
                }
                else {
                    alert(result);
                    return false;
                }
            });
        }
        //容错处理代码
        window.onerror = function () { return true; }
    </script>
</asp:Content> 
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <%
        List<SystemNews> news = (List<SystemNews>)ViewBag.News;
        List<SystemHelp> helpes = (List<SystemHelp>)ViewBag.Helpers;
        List<SystemClaims> claims = (List<SystemClaims>)ViewBag.Claims;
        List<SystemNotice> notices = (List<SystemNotice>)ViewBag.Notices;
    %>
    <div class="banner">
	    <div class="login_box">
            <%if (null == Session["UserInfo"]){%>
    	    <div class="login">
        	    <h2 class="login_title">登录财富车车</h2>
                <ul class="login_list">
            	    <li><span>账&nbsp;&nbsp;&nbsp;号：</span><input id="UserNameOrPhone" name="UserNameOrPhone" type="text" value="" placeholder="用户名/手机号" class="login_input" /></li>
                    <li><span>密&nbsp;&nbsp;&nbsp;码：</span><input id="UserPassword" name="UserPassword"type="password" value="" placeholder="" class="login_input" /></li>
                    <li><span>验证码：</span><input id="VcodeEl" name="VcodeEl" type="text" value="" placeholder="" class="l_yzm_input" /><img src="/Home/ValidateCode" height="34" title="点击换一张" onclick="code('js-mail_vcode_img','/Home/ValidateCode')"  id="js-mail_vcode_img" class="l_yzm_img"> <a href="javascript:code('js-mail_vcode_img','/Home/ValidateCode')" class="l_re_a">换一张</a></li>
                </ul>
                <p class="login_position"><input type="button" onclick="return login()" value="登录" class="l_btn" /></p>
                <p class="reg_info"><span class="fl">没有账号?点击<a href="/Home/Reg" class="reg_btn">免费注册</a></span> <a href="###" class="f_pwd fr">忘记密码?</a></p>
            </div>
            <%
                }else{
                    SystemUsers userInfo = (SystemUsers)Session["UserInfo"];  
            %>
            <div class="login_success">
        	    <h2 class="login_title">欢迎使用财富车车！</h2>
                <p>您当前正在使用的财富账户是:</p>
                <h3><%=userInfo.UserName%></h3>
                <p><a href="/User/" class="enter_btn">进入财富管理</a></p>
            </div>
            <%}%>
        </div>
	    <ul class="list">
		    <li class="bg1" style="display:block" >
			    <a href="###" target="_blank"><img src="/Contents/images/banner/banner.jpg" alt="" /></a>
		    </li>
		    <li class="bg2">
        	    <a href="###" target="_blank"><img src="/Contents/images/banner/banner1.jpg" alt="" /></a>
		    </li>
		    <li class="bg3">
			    <a href="###" target="_blank"><img src="/Contents/images/banner/banner2.jpg" alt="" /></a>
		    </li>
	    </ul>
	    <ul class="btn hover"></ul>
    </div>
	<!--banner end-->

	<!--投资 start-->
	<div class="wrap_1200 wrap_white index_step_wrap">
		<div class="wrap_1200_content">
			<ul class="index_step">
				<div class="clear"></div>
				<li class="index_step_1" style="width:27%">
					<a href="/Iod/Iod" target="_blank" class="wow FadeInL" data-wow-delay="0.2s" >
						<p class="fl"><img class="hover" src="/Contents/images/step_ico_0.png" width="86" height="86"></p>
						<b class="hover" style="color:#2A95DD">IOD模式</b>
						<p class="hover">投资后获得了担保物的处置权，<br/>并且可以随时行使自己的合法权利！</p>
					</a>
				</li>
				<li class="index_step_2" style="width:25%">
					<a href="/Iod/Iod#page4" target="_blank" class=" wow FadeInL" data-wow-delay="0.4s">
						<p class="fl"><img class="hover" src="/Contents/images/step_ico_6.png" width="86" height="86"></p>
						<b class="hover" style="color:#E24F30">整车抵押</b>
						<P class="hover">更安全的抵押物,银行和担保公司<br/>最爱的抵押担保物</P>
					</a>
				</li>
				<li class="index_step_3" style="width:23%">
					<a href="/Iod/Iod#page5" target="_blank" class=" wow FadeInL" data-wow-delay="0.6s">
						<p class="fl"><img class="hover" src="/Contents/images/step_ico_2.png" width="86" height="86"></p>
						<b class="hover" style="color:#2E4654">实地审核</b>
						<P class="hover">专业环节交由专业机构处理，<br/>做好借款企业实地认证工作。</P>
					</a>
				</li>
				<li class="index_step_4" style="width:17%;padding:0;margin:0;float:right!important">
					<a href="/Home/Safe" target="_blank" class=" index_step_right wow FadeInL" data-wow-delay="0.8s">
						<p class="fl"><img class="hover" src="/Contents/images/step_ico_5.png" width="86" height="86"></p>
						<b class="hover" style="color:#41C877">风险保障</b>
						<p class="hover">第三方本息担保,<br/>车辆过户反担保！</p>
					</a>
				</li>
				<div class="clear"></div>
			</ul>
		</div>
	</div>
    <!--投资动态 start-->
    <div class="wrap_1200">
   		<div class="wrap_1200_content touzi_upate">
   			<span class="fl touzi_notice_ico style_ico">最新公告:</span>
			<div class="txtScroll_touzi fl">
				<div class="hd">
					<a class="next style_ico"></a>
					<a class="prev style_ico"></a>
					<span class="pageState fr"></span>
				</div>
				<div class="bd">
					<ul class="infoList">
                        <%foreach (SystemNotice item in notices){%>
                        <li><%=item.NoticeContent%></li>
                        <%}%>
					</ul>
				</div>
			</div>
			<div class="clear"></div>
   		</div>
		<div class="break_5"></div>
   	</div>
	<!--投资动态 end-->

	<!--投资 start-->
	<div class="wrap_1200 products_wrap">
		<div class="products_content">
        	<div class="pd_title"><h2>Investment | 投资</h2><a href="/Home/Investment" class="fr more">更多></a></div>
			<ul class="products_box">
                <%foreach (SystemClaims item in claims){%>
                 <li>
                     <%=item.AlreadyAmount>=item.LoanAmount?"<a href=\"#\" class=\"products_mask\"></a>":""%>
					<a target="_blank" href="/Home/InvestmentDetail/<%=item.ID%>" class="products_img fl">
						<img class="img_loading hover_img hover" src="<%=item.TitleImagePath %>" width="168" height="168">
						<div class="products_img_tag_box">
							<span class="imt_tag_bao">担保物</span>
							<%--<span class="imt_tag_shou no_hover_right_tips" title="可获得销售奖励">可售48万</span>--%>
						</div>
					</a>
					<div class="products_title fl">
						<a target="_blank" href="/Home/InvestmentDetail/<%=item.ID%>" class="products_link text fl" title="<%=item.Title %>"><%=item.Title %></a>
						    <%
                                List<MoneyCarCar.Models.IconModel>  icons = MoneyCarCar.Commons.ExpandHelper.ToModel<List<MoneyCarCar.Models.IconModel>>(item.Icons);
                                foreach (MoneyCarCar.Models.IconModel icos in icons)
                                {
                            %>
                            <span class="tag_ico tag_<%=icos.StyleName%> fr no_hover_top_tips" title="<%=icos.Description%>"><%=icos.Title%></span>
                            <%
                                }
                            %>
					</div>
					<div class="products_money fl">
						<div class="products_money_info">
							<p class="fl">
								<span>借款金额</span>
								<b><font>¥</font><%=item.LoanAmount.ToString("#,##0.00") %></b>
							</p>
							<p class="fr">
								<span>年化利率</span>
								<b><%=item.APR %>%</b>
							</p>
							<div class="clear"></div>
						</div>
                        <%if (item.AlreadyAmount >= item.LoanAmount){%>
						<ul class="end_time_over">
							<b>投资期已于 <%=item.InvestmentEndTime.ToDateTime().ToString("yyyy-MM-dd")%> 日 结束</b>
						</ul>
                        <%}else{%>
                        <ul class="end_time_box" data-seconds="<%=((int)(Convert.ToDateTime(item.EarningsStartTime)-DateTime.Now).TotalSeconds)%>">
							<b>可投时间:</b>
						    <span>-</span>天
						    <span>-</span>时
						    <span>-</span>分
						    <span>-</span>秒
						</ul>
                        <%}%>
					</div>
					<div class="products_info fl">
						<p class="text">借款期限: <span class="font_red font"><%=item.LoanPeriod %>个月</span></p>
						<p class="text">还款方式: <span class="font_red"><%=item.RepaymentWat%></span></p>
						<p class="text">担保方式: <span class="font_red"><%=item.GuaranteeWay%></span></p>
						<p class="text">收益起计日期: <%=item.EarningsStartTime.ToDateTime().ToString("yyyy-MM-dd") %> 或 <font class="font_red no_hover_top_tips" title="一旦投满,立即发息(工作时间),不用等待投资期结束">满标即发息</font></p>
						<p class="text">借款公司: <%=item.Borrower %></p>
					</div>
					<div class="products_step fr">
						<p class="text">剩余份数: <font class="font_red"><%=((item.LoanAmount-item.AlreadyAmount)/item.SingleAmount).ToString("0.00") %>份</font></p>
						<P><span class="fl">投资进度:</span><span class="fr"><%=100-(item.LoanAmount-item.AlreadyAmount)*100/item.LoanAmount%>%</span><div class="break_5"></div></P>
						<div class="products_step_box">
							<div class="products_step_loading" style="width:<%=100-(item.LoanAmount-item.AlreadyAmount)*100/item.LoanAmount%>%"></div>
						</div>
						<div class="break_10"></div>
						<a target="_blank" href="/Home/InvestmentDetail/<%=item.ID%>" class="button_blue"><%=(item.AlreadyAmount >= item.LoanAmount)?"还款中":"立即投资"%></a>
					</div>
				</li>
                <%}%>		
			</ul>
			<div class="button_more_box">
				<div class="break_10"></div>
				<a href="/Home/Investment" class="gray_blue_1" style="padding:10px 150px">查看更多</a>
				<div class="break_10"></div>
			</div>
		</div>
	</div>
	<!--投资 end-->	 
    
    <!--新闻 start-->
    <div class="wrap_1200">
    	<div class="products_content">
			<!--footer news-->
			<div class="footer_box fl">
				<div class="footer_title"><h2>新闻动态 | <span>News</span></h2><a href="#" class="fr more">更多></a></div>
				<ul class="footr_news_list">
                    <%foreach (SystemNews item in news){%>
                    <li><a href="#" title="<%=item.NewsTitle%>"><%=item.NewsTitle%></a></li> 
                    <%}%>
				</ul>
			</div>
			<!--footer 联系方式-->
			<div class="footer_box fl">
				<div class="footer_title"><h2>帮助中心 | <span>About us</span></h2><a href="#" class="fr more">更多></a></div>
				<div class="footer_box_content">
				<ul class="help_list_min">
					<%foreach (SystemHelp item in helpes){%>
                    <li><a href="#" title="<%=item.AskContent%>"><%=item.AskContent%></a></li> 
                    <%}%>
				</ul>
				</div>
			</div>

			<!--客服服务-->
			<div class="footer_box nb_r fr">
				<div class="footer_title"><h2>客服服务 | <span>Customer service</span></h2></div>
				<div class="footer_box_content">
					<img src="/Contents/images/tel.png" title="客服电话" alt="客服电话">
					<p>工作时间 9:00-12:00AM & 13:30-18:00PM</p>
					<p>客服邮箱：4008888888@foxmail.com</p>
					<p>客服将在上班后第一时间为您处理，并通过电话或邮件的形式回复您！</p>
					<p>公司：成都CF网络科技有限公司</p>
					<p>地址：成都市天晖路808号</p>
					<p>邮编：610000  电话：400-888-8888</p>
				</div>
			</div>
        </div>
    </div>    
    <!--新闻 end-->
    
    <!--合作伙伴 start-->
    <div class="partner_box">
    	<div class="partner_content">
        	<div class="pd_title"><h2>合作伙伴</h2><a href="###" class="fr more">更多></a></div>
            <ul class="partner_list">
            	<li><a href="###" title=""><img src="/Contents/images/partner/1.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/2.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/3.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/4.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/5.jpg" alt="" /></a></li>
                <li><a href="###" title=""><img src="/Contents/images/partner/6.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/7.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/8.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/9.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/10.jpg" alt="" /></a></li>
                <li><a href="###" title=""><img src="/Contents/images/partner/11.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/12.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/13.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/14.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/15.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/16.jpg" alt="" /></a></li>
            </ul>
            <div class="pd_title"><h2>合作机构</h2><a href="###" class="fr more">更多></a></div>
            <ul class="partner_list">
            	<li><a href="###" title=""><img src="/Contents/images/partner/01.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/02.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/03.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/04.jpg" alt="" /></a></li>
            	<li><a href="###" title=""><img src="/Contents/images/partner/05.jpg" alt="" /></a></li>
            </ul>
        </div>
    </div>
    <!--悬浮工具条 start-->
	<div class="tools_fixed">
		<a href="http://crm2.qq.com/page/portalpage/wpa.php?uin=4006976662&aty=0&a=0&curl=&ty=1" target="_blank" class="tools_link" title="QQ客服">
			<span class="style_ico ico_kf"></span>
			<div class="tools_content_box">
				<div class="tools_content">
					<img src="images/phone_min.png"/><p><img class="hover hover_img" src="images/qq_button.png"/></p>
				</div>
			</div>
		</a>
		<a href="javascript:void(0)" class="tools_link"  title="关注微信" target="_blank" >
			<span class="style_ico ico_code"></span>
			<div class="tools_content_box"><div class="tools_content"><img src="http://www.hellomoney.com/public/images/weixin_code.jpg"/><p>关注 财富车车 微信</p></div></div>
		</a>
		<a href="http://weibo.com/mangning" target="_blank" class="tools_link" title="关注 新浪微博">
			<span class="style_ico ico_weibo"></span>
			<div class="tools_content_box"><div class="tools_content"><img src="http://www.hellomoney.com/public/images/weibo_code.png"/><p>关注 财富车车 新浪微博</p></div></div>
		</a>
		<a href="#" class="tools_link" id="back_top" title="返回顶部" style="display:none"><span class="style_ico ico_top"></span></a>
	</div>
	<!--悬浮工具条 end-->
    <!--合作伙伴 end-->
	<script type="text/javascript">
	    //首页banner
	    $(function () {
	        function banner() {
	            var index = 1;
	            var len = $(".banner .list li").length;
	            var time;

	            for (i = 1; i <= len; i++) {
	                var Obtn = $("<li>" + i + "</li>").appendTo($(".banner .btn"));
	            }
	            $(".banner .btn li").eq(0).addClass('hover');
	            $(".banner .list li").eq(0).css("z-index", "1");
	            $(".banner .btn li").mouseover(function () {
	                index = $(".banner .btn li").index(this);
	                show(index);
	            })
	            time = setInterval(function () {
	                show(index);
	                index++;
	                if (index == len) { index = 0 }
	            }, 10000);

	            function show(index) {
	                $(".banner .list li p").removeClass('banner_hover');
	                $(".banner .list li").eq(index).find("p").addClass('banner_hover');

	                $(".banner .btn li").eq(index).addClass('hover').siblings("li").removeClass('hover');
	                $(".banner .list li").eq(index).fadeIn(900).siblings("li").fadeOut(600);
	            }
	        }
	        banner();
	    })

	    //滚动公告
	    jQuery(".txtScroll_touzi").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "top", autoPlay: true, interTime: 3500 });
 	</script>
</asp:Content>
