(function(){
  function trim(el) {
    return (''.trim) ? el.val().trim() : $.trim(el.val());
  }
  $.srw= function (config) {
    var fields = [], item;
	var funs = false;
	for (item in config) {
      if(!processField(config[item], item)){
		funs =false;
		break;
	  }
	  funs = true;
    }
	function processField(opts, selector) {
		//radio灞炴€х壒娈婏紝鎻愬墠鍒ゆ柇
		var R_D = selector.split('#');
		if(R_D['0']!=''){
			if(R_D['0']=='radio'){
				selector = 'input:radio[name="'+R_D['1']+'"]:checked';
			}else if(R_D['0']=='checkbox'){
				selector = 'input:checkbox[name="'+R_D['1']+'"]:checked';
			}
		}else{
			selector = '#'+R_D['1'];
		}
		 if($(selector) && opts.nl==true){	//鏄惁鍏佽涓虹┖
			  if($(selector).val()=='' || typeof($(selector).val())=='undefined'){
				  art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				  $(selector).focus();
				  return false;
		 	 }
		}
		
		 if($(selector) && opts.eq && opts.eq!=""){	//涓ゆ鍊兼槸鍚︿竴鑷�
			  if($(selector).val()!=$(opts.eq).val()){
				  art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				  $(selector).focus();
				  return false;
		 	 }
		}
		 
		if($(selector) && opts.len){	//闀垮害
			var tlen = opts.len.split(',');
			if(tlen['0'] && tlen['1'] && $(selector).val().length<tlen['0'] && $(selector).val().length>tlen['1']){
				art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}else if(tlen['0'] && $(selector).val().length<tlen['0']){
				art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}else if(tlen['1'] && $(selector).val().length>tlen['1']){
				art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}
		}
		
		if($(selector) && opts.daxiao){	//骞呭害 澶т簬鎴栬€呭皬浜�
			var tdaxiao = opts.daxiao.split(',');
			var myval = parseFloat($(selector).val());
			if(isNaN(myval)){
				art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}
			if(tdaxiao['0'] && tdaxiao['1'] && myval<tdaxiao['0'] && myval>tdaxiao['1']){
				art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}else if(tdaxiao['0'] && myval<tdaxiao['0']){
				art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}else if(tdaxiao['1'] && myval>tdaxiao['1']){
				art.dialog({ content: opts.nl_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}
		}
		
		if($(selector) && opts.reg){	//姝ｅ垯楠岃瘉
			var tlen = opts.reg.split(',');
			var regR = false;
			for (regd in tlen) {
				if(tlen[regd] && regexEnum && regexEnum[tlen[regd]]){
					regstr = eval("regexEnum."+tlen[regd]);
					if(regstr==undefined || regstr==""){
						art.dialog({content: '鏍￠獙閰嶇疆閿欒',ok:function(){this.close();return false;}});
						return false;
					}
					if(new RegExp(regstr).test($(selector).val())){	//鏈変竴娆￠獙璇侀€氳繃鍒欎负閫氳繃;
						regR=true;
					}
				}	
			}
			if(!regR){
				art.dialog({content: opts.reg_err, lock: true,ok:function(){this.close();return false;}});
				$(selector).focus();
				return false;
			}
		}

		if($(selector) && opts.fun){	//鑷畾涔夊嚱鏁伴獙璇�
			if(!eval(opts.fun+'()')){
				 art.dialog({content: opts.fun_err, lock: true,ok:function(){this.close();return false;}});
				 $(selector).focus();
				 return false;
		 	 }
		}

		return true;
	}
	return funs;
 };
})(this.jQuery);

//鍊掕鏃禰鍙戦€佺煭淇★紝鍙戦€侀偖浠禲
var djs = {
node:null,
count:30,
start:function(){
if(this.count > 0){
this.node.val(this.count--+'绉掑悗閲嶆柊鍙戦€�');
var _this = this;
setTimeout(function(){
_this.start();
},1000);
}else{
this.node.prop({disabled:false});
this.node.val("楠岃瘉鍐嶆鍙戦€�");
this.count = 30;
}
},
init:function(noded,count){
		var node = $('#'+noded);
   this.count = count;
this.node = node;
this.node.prop({disabled:true});
this.start();
}
}

function code(Did,codeurl){
	var SID = Math.round(Math.random()*(9999-1000) + 1000);
	document.getElementById(Did).src = codeurl+'&fp='+SID;
		
}