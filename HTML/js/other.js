////////////////////////////////////////涓婁笅宸﹀彸婊戝姩
		function getOffset(e) {
		    var target = e.target;
		    if (target.offsetLeft == undefined) {
		        target = target.parentNode;
		    }
		    var pageCoord = getPageCoord(target);
		    var eventCoord = {
		        x: window.pageXOffset + e.clientX,
		        y: window.pageYOffset + e.clientY
		    };
		    var offset = {
		        offsetX: eventCoord.x - pageCoord.x,
		        offsetY: eventCoord.y - pageCoord.y
		    };
		    return offset;
		}

		function getPageCoord(element) {
		    var coord = {
		        x: 0,
		        y: 0
		    };
		    while (element) {
		        coord.x += element.offsetLeft;
		        coord.y += element.offsetTop;
		        element = element.offsetParent;
		    }
		    return coord;
		}

		$(document).ready(function(){
			$(".products_items .products_index_list").hover(function(e){
				var _this  = $(this), //闂寘
				_desc  = _this.find(".products_index_list_info").stop(true),
				width  = _this.width(), //鍙栧緱鍏冪礌瀹�
				height = _this.height(), //鍙栧緱鍏冪礌楂�
				left   = (e.offsetX == undefined) ? getOffset(e).offsetX : e.offsetX, //浠庨紶鏍囦綅缃紝寰楀埌宸﹁竟鐣岋紝鍒╃敤淇ff鍏煎鐨勬柟娉�
				top    = (e.offsetY == undefined) ? getOffset(e).offsetY : e.offsetY, //寰楀埌涓婅竟鐣�
				right  = width - left, //璁＄畻鍑哄彸杈圭晫
				bottom = height - top, //璁＄畻鍑轰笅杈圭晫
				rect   = {}, //鍧愭爣瀵硅薄锛岀敤浜庢墽琛屽搴旀柟娉曘€�
				_min   = Math.min(left, top, right, bottom), //寰楀埌鏈€灏忓€�
				_out   = e.type == "mouseleave", //鏄惁鏄寮€浜嬩欢
				spos   = {}; //璧峰浣嶇疆
			
				rect[left] = function (epos){ //榧犱粠鏍囧乏渚ц繘鍏ュ拰绂诲紑浜嬩欢
					spos = {"left": -width, "top": 0};
					if(_out){
						_desc.animate(spos, "fast"); //浠庡乏渚х寮€
					}else{
						_desc.css(spos).animate(epos, "fast"); //浠庡乏渚ц繘鍏�
					}
				};
			
				rect[top] = function (epos) { //榧犱粠鏍囦笂杈圭晫杩涘叆鍜岀寮€浜嬩欢
					spos = {"top": -height, "left": 0};
					if(_out){
						_desc.animate(spos, "fast"); //浠庝笂闈㈢寮€
					}else{
						_desc.css(spos).animate(epos, "fast"); //浠庝笂闈㈣繘鍏�
					}
				};
			
				rect[right] = function (epos){ //榧犱粠鏍囧彸渚ц繘鍏ュ拰绂诲紑浜嬩欢
					spos = {"left": left,"top": 0};
					if(_out){
						_desc.animate(spos, "fast"); //浠庡彸渚ф垚绂诲紑
					}else{
						_desc.css(spos).animate(epos, "fast"); //浠庡彸渚ц繘鍏�
					}
				};
			
				rect[bottom] = function (epos){ //榧犱粠鏍囦笅杈圭晫杩涘叆鍜岀寮€浜嬩欢
					spos = {"top": height, "left": 0};
					if(_out){
						_desc.animate(spos, "fast"); //浠庡簳閮ㄧ寮€
					}else{
						_desc.css(spos).animate(epos, "fast"); //浠庡簳閮ㄨ繘鍏�
					}
				};
			
				rect[_min]({"left":0, "top":0}); // 鎵ц瀵瑰簲杈圭晫 杩涘叆/绂诲紑 鐨勬柟娉�
			
			});

		});


////////////////////////////////////////鍒嗕韩鏃跺埢
 
 

////////////////////////////////////////琛ㄥ崟鏁伴噺鍔犲噺
$.fn.iVaryVal=function(iSet,CallBack){
	/*
	 * Minus:鐐瑰嚮鍏冪礌--鍑忓皬
	 * Add:鐐瑰嚮鍏冪礌--澧炲姞
	 * Input:琛ㄥ崟鍏冪礌
	 * Min:琛ㄥ崟鐨勬渶灏忓€硷紝闈炶礋鏁存暟
	 * Max:琛ㄥ崟鐨勬渶澶у€硷紝姝ｆ暣鏁�
	 */
	iSet=$.extend({Minus:$('.J_minus'),Add:$('.J_add'),Input:$('.J_input'),Min:0,Max:10000},iSet);
	var C=null,O=null;
	//鎻掍欢杩斿洖鍊�
	var $CB={};
	//澧炲姞
	iSet.Add.each(function(i){
		$(this).click(function(){
			O=parseInt(iSet.Input.eq(i).val());
			(O+1<=iSet.Max) || (iSet.Max==null) ? iSet.Input.eq(i).val(O+1) : iSet.Input.eq(i).val(iSet.Max);
			//杈撳嚭褰撳墠鏀瑰彉鍚庣殑鍊�
			$CB.val=iSet.Input.eq(i).val();
			$CB.index=i;
			
			
			
			//鍥炶皟鍑芥暟
			if (typeof CallBack == 'function') {
                CallBack($CB.val,$CB.index);
				
                if($CB.val==iSet.Max){
                	$.sticky('杈撳叆鐨勬暟閲忚秴杩囨渶澶у€�',{speed:'fast',autoclose:5000});
                }
            }
			//hello mall 浠锋牸璁＄畻
			$('#freighttot').html(($CB.val * $('#freight').val()).toFixed(2))  ;
			$('#investmentmoney').html(($CB.val * $('#sharemoney').val()).toFixed(2))  ;
			$('#investmentinterestmoney').html((($CB.val *$('#sharemoney').val())* ($('#investmentinterest').val()* 0.01)).toFixed(2));
			var daymoney = ($CB.val * $('#sharemoney').val() * 91) * ($('#investmentinterest').val()* 0.01 / 365) + '';
			$('#daymoney').html(daymoney.substring(0,daymoney.indexOf(".") + 4));
			var invest_profit = document.getElementById("invest_profit").value / 100;
			$('#invest_profit_num').html((parseInt(($CB.val * $('#sharemoney').val()).toFixed(2)) * invest_profit).toFixed(2) );
			$('#allmoney').html(($CB.val * $('#sharemoney').val())+parseInt(($CB.val * $('#freight').val()).toFixed(2)));
						//鎻愮ず璐拱鏁伴噺
			if($CB.val>2){
				$('.unit_tips').show();
			}
		});
	});
	//鍑忓皯
	iSet.Minus.each(function(i){
		$(this).click(function(){
			O=parseInt(iSet.Input.eq(i).val());
			O-1<iSet.Min ? iSet.Input.eq(i).val(iSet.Min) : iSet.Input.eq(i).val(O-1);
			$CB.val=iSet.Input.eq(i).val();
			$CB.index=i;
			//鍥炶皟鍑芥暟
			//鍥炶皟鍑芥暟
			if (typeof CallBack == 'function') {
				CallBack($CB.val,$CB.index);
		  	}
			$('#investmentmoney').html(($CB.val * $('#sharemoney').val()).toFixed(2))  ;
			$('#investmentinterestmoney').html((($CB.val *$('#sharemoney').val())* ($('#investmentinterest').val()* 0.01)).toFixed(2));
			var daymoney = ($CB.val * $('#sharemoney').val() * 91) * ($('#investmentinterest').val()* 0.01 / 365) + '';
			$('#daymoney').html(daymoney.substring(0,daymoney.indexOf(".") + 4));
			$('#freighttot').html(($CB.val * $('#freight').val()).toFixed(2))  ;
			$('#investmentmoney').html(($CB.val * $('#sharemoney').val()).toFixed(2))  ;
			$('#investmentinterestmoney').html((($CB.val *$('#sharemoney').val())* ($('#investmentinterest').val()* 0.01)).toFixed(2));
			var invest_profit = document.getElementById("invest_profit").value / 100;
			$('#invest_profit_num').html((parseInt(($CB.val * $('#sharemoney').val()).toFixed(2)) * invest_profit).toFixed(2) );
			$('#allmoney').html(($CB.val * $('#sharemoney').val())+parseInt(($CB.val * $('#freight').val()).toFixed(2)));
			if($CB.val<3){
				$('.unit_tips').hide();
			}
		});
	});
	//鎵嬪姩
	iSet.Input.bind({
		'click':function(){
			O=parseInt($(this).val());
			$(this).select();
		},
		'keyup':function(e){
			if($(this).val()!=''){
				C=parseInt($(this).val());
				//闈炶礋鏁存暟鍒ゆ柇
				if(/^[1-9]\d*|0$/.test(C)){
					$(this).val(C);
					O=C;
				}else{
					$(this).val(O);
				}
			}
			//閿洏鎺у埗锛氫笂鍙�--鍔狅紝涓嬪乏--鍑�
			if(e.keyCode==38 || e.keyCode==39){
				iSet.Add.eq(iSet.Input.index(this)).click();
			}
			if(e.keyCode==37 || e.keyCode==40){
				iSet.Minus.eq(iSet.Input.index(this)).click();
			}
			//杈撳嚭褰撳墠鏀瑰彉鍚庣殑鍊�
			$CB.val=$(this).val();
			$CB.index=iSet.Input.index(this);
			//鍥炶皟鍑芥暟
			if (typeof CallBack == 'function') {
                CallBack($CB.val,$CB.index);
            }
		 //parseInt($CB.val) * (parseInt($('#investmentinterest').val())* 0.01)

			if(O>iSet.Max){
				$(this).val(iSet.Max);
				$.sticky('杈撳叆鐨勬暟閲忚秴杩囨渶澶у€�',{speed:'fast',autoclose:5000})
			}

			$('#investmentmoney').html(($CB.val * $('#sharemoney').val()).toFixed(2))  ;
			$('#investmentinterestmoney').html((($CB.val *$('#sharemoney').val())* ($('#investmentinterest').val()* 0.01)).toFixed(2));
			var daymoney = ($CB.val * $('#sharemoney').val() * 91) * ($('#investmentinterest').val()* 0.01 / 365) + '';
			$('#daymoney').html(daymoney.substring(0,daymoney.indexOf(".") + 4));
			$('#freighttot').html(($CB.val * $('#freight').val()).toFixed(2))  ;
			$('#investmentmoney').html(($CB.val * $('#sharemoney').val()).toFixed(2))  ;
			var invest_profit = document.getElementById("invest_profit").value / 100;
			$('#invest_profit_num').html((parseInt(($CB.val * $('#sharemoney').val()).toFixed(2)) * invest_profit).toFixed(2) );
			$('#allmoney').html(($CB.val * $('#sharemoney').val())+parseInt(($CB.val * $('#freight').val()).toFixed(2)));

		},
		'blur':function(){
			$(this).trigger('keyup');
			if($(this).val()==''){
				$(this).val(O);
				$('.J_minus').addClass("xxx");
			}
			
			//鎻愮ず璐拱鏁伴噺
			if($CB.val > 2){
				$('.unit_tips').show();
			}else{
				$('.unit_tips').hide();
			}

			//鍒ゆ柇杈撳叆鍊兼槸鍚﹁秴鍑烘渶澶ф渶灏忓€�
			if(iSet.Max){
				
				
				if(O>iSet.Max){
					$(this).val(iSet.Max);
					$.sticky('杈撳叆鐨勬暟閲忚秴杩囨渶澶у€�',{speed:'fast',autoclose:5000})
				}
			}
			if(O<iSet.Min){
				$(this).val(iSet.Min);
			}
			//杈撳嚭褰撳墠鏀瑰彉鍚庣殑鍊�
			$CB.val=$(this).val();
			$CB.index=iSet.Input.index(this);
			//鍥炶皟鍑芥暟
			if (typeof CallBack == 'function') {
                CallBack($CB.val,$CB.index);
            }
			$('#investmentmoney').html(($CB.val * $('#sharemoney').val()).toFixed(2))  ;
			$('#investmentinterestmoney').html((($CB.val *$('#sharemoney').val())* ($('#investmentinterest').val()* 0.01)).toFixed(2));
			var daymoney = ($CB.val * $('#sharemoney').val() * 91) * ($('#investmentinterest').val()* 0.01 / 365) + '';
			$('#daymoney').html(daymoney.substring(0,daymoney.indexOf(".") + 4));
		}
	});
}
