/**
 * @deplicate 20171112
 * The default template 'iHello' is depended by this js file.(scroll)
 * @todo Remove this fiel safety.
 * @author lee
 */

//Use internal $.serializeArray to get list of form elements which is
//consistent with $.serialize

//From version 2.0.0, $.serializeObject will stop converting [name] values
//to camelCase format. This is *consistent* with other serialize methods:

//- $.serialize
//- $.serializeArray

//If you require camel casing, you can either download version 1.0.4 or map
//them yourself.



var ISJ = {

	ajaxPostForm:function(form, onload){
		//onload = function(data, textStatus)
		$.post($(form).attr('action'), $(form).serializeObject(), onload);
	}

};


var is_regexp = (window.RegExp) ? true : false;
var sAgent = navigator.userAgent.toLowerCase() ;

var BrowserInfo = new Object() ;
BrowserInfo.IsIE			= ( sAgent.indexOf("msie") != -1 ) ;
BrowserInfo.IsGecko		= !BrowserInfo.IsIE ;
BrowserInfo.IsSafari		= ( sAgent.indexOf("safari") != -1 );
BrowserInfo.IsNetscape	= ( sAgent.indexOf("netscape") != -1 ) ;

var ISS = new Object();

ISS.F = function() {
	var elements = new Array();

	for (var i = 0; i < arguments.length; i++) {
		var element = arguments[i];
		if (typeof element == 'string')
			element = document.getElementById(element);

		if (arguments.length == 1)
			return element;

		elements.push(element);
	}

	return elements;
}


ISS.$ = function(n, d) { //v4.01
	var p,i,x;
	if(!d) d=document;
	if((p=n.indexOf("?"))>0&&parent.frames.length) {
		d=parent.frames[n.substring(p+1)].document;
		n=n.substring(0,p);
	}
	if(!(x=d[n])&&d.all) x=d.all[n];
	for (i=0;!x&&i<d.forms.length;i++)
		x=d.forms[i][n];
	for(i=0;!x&&d.layers&&i<d.layers.length;i++)
		x=ISS.$(n,d.layers[i].document);
	if(!x && d.getElementById) x=d.getElementById(n);
	return x;
}

ISS.addClassName = function(element, className){
	if (!(element = this.F(element))) return;
	pattern = new RegExp('\\b'+ className+'\\b','i');
	if(element.className.search(pattern) == -1){
		element.className += ' '+className;
	}
}

ISS.removeClassName = function(element, className){
	if (!(element = ISS.F(element))) return;
	pattern = new RegExp('\\b'+ className+'\\b','i');
	if(element.className.search(pattern) != -1){
		element.className = element.className.replace(pattern,' ');
	}
}


ISS.openWindow = function(sUrl,sName,iWidth,iHeight,scroolbars){

	iLeft=(screen.height-iHeight)/2;
	iTop=(screen.width-iWidth)/2;

	var sOptions = "toolbar=no,status=no,resizable=yes,dependent=yes,scrollbars=1" ;
	sOptions += ",width=" + iWidth;
	sOptions += ",height=" + iHeight;
	sOptions += ",left=" + iLeft;
	sOptions += ",top=" + iTop;

	var oWindow = window.open( sUrl,sName, sOptions ) ;

	if (BrowserInfo.IsIE)
	{
		// The following change has been made otherwise IE will open the file
		// browser on a different server session (on some cases):
		// http://support.microsoft.com/default.aspx?scid=kb;en-us;831678
		// by Simone Chiaretta.
		oWindow.opener = window ;
	}

	return oWindow;
}

/**
 * listbox redirection
 */
ISS.jumpSelectMenu = function(targ,selObj){
	if(selObj.options[selObj.selectedIndex].value!='')
		eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");

}

ISS.submitForm = function(form,paramName,paramValue){
	oF = ISS.$(form);

	if(oF.tagName=='FORM'){
		if(paramName){
			oTag = document.createElement('input');
			oF.appendChild(oTag);
			oTag.name = paramName;
			oTag.value = paramValue;
			oF.submit();
			oF.removeChild(oTag);
		}else{
			oF.submit();
		}
	}else{

	}
}

ISS.getBackGroudIframe = function(){
	if(oIframe = window.frames["__backgroud__"]){
		return oIframe;
	}else{
		if(BrowserInfo.IsIE){
			oIframe = document.createElement("<iframe name='__backgroud__'>");
		}else{
			oIframe = document.createElement("iframe");
			oIframe.name = "__backgroud__";
		}
		document.body.appendChild(oIframe);
		oIframe.width = '0px';
		oIframe.height = '0px';
		oIframe.frameBorder = 0;
		oIframe.src = 'about:blank';
		return oIframe;
	}
}

ISS.submitForm_background = function(formName,param){
	oIframe = ISS.getBackGroudIframe();
	if(document.attachEvent)
		oIframe.attachEvent("onload",this.onLoadBackgroudSubmit);
	if(document.addEventListener)
		oIframe.addEventListener("load",this.onLoadBackgroudSubmit,false);

	oF = ISS.$(formName);
	target = oF.target;
	oF.target = oIframe.name;
	oTag = document.createElement('input');
	oF.appendChild(oTag);
	oTag.name = "__backgroud_tag_";
	oTag.value = "";
	//oTag.type = "hidden";
	oF.submit();
	oF.removeChild(oTag);
	oF.target = target;
}

ISS.onLoadBackgroudSubmit = function(){
	var oF = window.frames["__backgroud__"];
	if(BrowserInfo.IsIE){
		oF.detachEvent("onload",ISS.onLoadBackgroudSubmit);
		alert(oF.document.body.innerHTML);
	}else{
		try{
			oF.removeEventListener("load",this.onLoadBackgroudSubmit,false);
		}finally{
			alert(oF.document.body.innerHTML);
		}
	}
}

ISS.showOrHidden = function(sObjectID,show){
	obj = ISS.$(sObjectID);
	if(obj){
		if(show){
			obj.style.display = "";
		}else{
			obj.style.display = "none";
		}
	}
}

ISS.CheckAll = function(sAttributeName,bChecked){
	oCols = document.getElementsByTagName('input');
	for(i=0;i<oCols.length;i++){
		if(oCols[i].type == 'checkbox' && oCols[i].getAttribute(sAttributeName)){
			try{
				oCols[i].checked = bChecked;
			}catch(e){
			}
		}
	}
}



ISS.autoScroll_objs = new Object();

ISS.autoScroll_contrl = function(sName){
	os = ISS.autoScroll_objs[sName];

	if(os.bPause){
		//alert(os.Block.scrollHeight+'|offset:'+os.Block.offsetHeight);
		return;
	}

	if(os.sDirection == 'up'){
		if(!os.Checked){
			if(os.Content1.scrollHeight < os.Block.offsetHeight){
				//alert('scrollHeight:'+os.Block.scrollHeight+'|offset:'+os.Block.offsetHeight+'|height_sub:'+os.Content1.scrollHeight);
				os.Content1.style.height = os.Block.offsetHeight+'px';
				os.Content2.style.height = os.Block.offsetHeight+'px';
			}
			os.Checked = true;
		}
		if(os.Block.scrollTop + os.Block.offsetHeight >= os.Block.scrollHeight){ //閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熶茎鏂ゆ嫹灏炬椂
			os.Block.scrollTop = 0;	//閿熸枻鎷烽敓鎴鎷烽敓鏂ゆ嫹澶�
		}else if(os.Block.scrollTop >= os.Content1.offsetHeight){	//閿熸枻鎷稢ontent1鍏ㄩ敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷风ず閿熸枻鎷锋椂
			os.Block.scrollTop -= os.Content1.offsetHeight;		//閿熸枻鎷烽敓鏂ゆ嫹涓�閿熸枻鎷稢ontent1閿熶茎楂樿鎷�
		}else{
			os.Block.scrollTop += os.iStep;
		}
	}else if(os.sDirection == 'down'){
		if(!os.Checked){
			if(os.Content1.scrollHeight < os.Block.offsetHeight){
				//alert('scrollHeight:'+os.Block.scrollHeight+'|offset:'+os.Block.offsetHeight+'|height_sub:'+os.Content1.scrollHeight);
				os.Content1.style.height = os.Block.offsetHeight+'px';
				os.Content2.style.height = os.Block.offsetHeight+'px';
			}
			os.Checked = true;
		}
		if(os.Block.scrollTop <= 0 ){ //閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熶茎鏂ゆ嫹灏炬椂
			os.Block.scrollTop = os.Block.scrollHeight;	//閿熸枻鎷烽敓鎴鎷烽敓鏂ゆ嫹澶�
		}else if((os.Block.scrollHeight -(os.Block.scrollTop + os.Block.offsetHeight)) >= os.Content2.offsetHeight){	//閿熸枻鎷稢ontent2鍏ㄩ敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷风ず閿熸枻鎷锋椂
			os.Block.scrollTop += os.Content2.offsetHeight;		//閿熸枻鎷烽敓鏂ゆ嫹涓�閿熸枻鎷稢ontent2閿熶茎楂樿鎷�
		}else{
			os.Block.scrollTop -= os.iStep;
		}
	}else if(os.sDirection == 'left'){
		if(!os.Checked){
			if(os.Content1.scrollWidth < os.Block.offsetWidth){
				os.Content1.style.width = os.Block.offsetWidth+'px';
			}
			if(os.Content2.scrollWidth < os.Block.offsetWidth){
				os.Content2.style.width = os.Block.offsetWidth+'px';
			}
			os.Checked = true;
		}
		if(os.Block.scrollLeft + os.Block.offsetWidth>=os.Block.scrollWidth ){ //閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熶茎鏂ゆ嫹灏炬椂
			os.Block.scrollLeft = 0;	//閿熸枻鎷烽敓鎴鎷烽敓鏂ゆ嫹澶�
		}else if(os.Block.scrollLeft >= os.Content1.offsetWidth){	//閿熸枻鎷稢ontent1鍏ㄩ敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷风ず閿熸枻鎷锋椂
			os.Block.scrollLeft-=os.Content1.offsetWidth;		//閿熸枻鎷烽敓鏂ゆ嫹涓�閿熸枻鎷稢ontent1閿熶茎鍖℃嫹閿燂拷
		}else{
			os.Block.scrollLeft += os.iStep;
		}
	}else if(os.sDirection == 'right'){
		if(!os.Checked){
			if(os.Content1.scrollWidth < os.Block.offsetWidth){
				os.Content1.style.width = os.Block.offsetWidth+'px';
			}
			if(os.Content2.scrollWidth < os.Block.offsetWidth){
				os.Content2.style.width = os.Block.offsetWidth+'px';
			}
			os.Checked = true;
		}
		if(os.Block.scrollLeft <= 0){ //閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熶茎鏂ゆ嫹灏炬椂
			os.Block.scrollLeft = os.Block.scrollWidth;	//閿熸枻鎷烽敓鎴鎷烽敓鏂ゆ嫹澶�
		}else if((os.Block.scrollWidth -(os.Block.scrollLeft+os.Block.offsetWidth)) >= os.Content2.offsetWidth){	//閿熸枻鎷稢ontent2鍏ㄩ敓鏂ゆ嫹閿熸枻鎷烽敓鏂ゆ嫹閿熸枻鎷风ず閿熸枻鎷锋椂
			os.Block.scrollLeft+=os.Content2.offsetWidth;		//閿熸枻鎷烽敓鏂ゆ嫹涓�閿熸枻鎷稢ontent2閿熶茎鍖℃嫹閿燂拷
		}else{
			os.Block.scrollLeft -= os.iStep;
		}
	}else{
		os.bPause = true;
	}
}

ISS.autoScroll = function(sName,sDirection,sStyle,iSpeed,iStep){
	ISS.autoScroll_objs[sName] = new Object();
	os = ISS.autoScroll_objs[sName];


	os.sName = sName;
	os.bPause = false;
	os.iSpeed = iSpeed;
	os.iStep = iStep;
	os.sDirection = sDirection;
	os.sStyle = sStyle;

	os.Block = ISS.$(sName);
	os.Content1 = ISS.$(sName+'_1');
	os.Content2 = ISS.$(sName+'_2');

	os.Block.onmouseover=function(){ISS.autoScroll_objs[sName].bPause = true;}
	os.Block.onmouseout=function() {ISS.autoScroll_objs[sName].bPause = false;}

	os.Content2.innerHTML = 	os.Content1.innerHTML;
	//alert("ISS.autoScroll_contrl('"+sName+")");
	setInterval("ISS.autoScroll_contrl('"+sName+"')",iSpeed);

}

function getUrlVars()
{
	var vars = [], hash;
	var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
	for(var i = 0; i < hashes.length; i++)
	{
		hash = hashes[i].split('=');
		vars.push(hash[0]);
		vars[hash[0]] = hash[1];
	}
	return vars;
}



