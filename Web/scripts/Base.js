var IE = document.all?true:false;

function WindowParent()
{
    var win = window;
    while(win != win.parent && null != win)
        win = win.parent;
    return win;
}

function frameBack(num)
{
	alert("otra");
	//WindowParent().history.back(num);
	
}

function CargarConfiguracion()
{     
   ResizePage(true);  
}
    
function ResizePage()
{
     if(null != window.frameElement)
     {
         var docHt = 0, sh, oh;
         if (document.body.scrollHeight) docHt = sh = document.body.scrollHeight;
         if (document.body.offsetHeight) docHt = oh = document.body.offsetHeight;
         if (sh && oh) docHt = Math.max(sh, oh);
         window.frameElement.style.height = docHt + "px";
      }       
}  

function keyDown(fld, e)
{
	if (e.keyCode == 8)
	{
		event.keyCode=0; 		
		fld.value = "";
		return event.keyCode;

	}
}

function currencyFormatOneDecimal(fld, e, miles, decimales,cantidadDecimales)
{
    var decSep = '';
    var decNum = 0;
    var textSelected =  document.selection.createRange().text;
   
	if(fld.value.length == textSelected.length)
	{
		fld.value = "";		
		document.selection.empty();
	}
	if (e.keyCode == 8)
	{
		event.keyCode=0; 		
		fld.value = "";
		return event.keyCode;
	}
    
    if(decimales == true){
	
        if(WindowParent().document.getElementById('hdnSeparadorDecimales') != null)
        {
			decSep =  WindowParent().document.getElementById('hdnSeparadorDecimales').value;
			decNum = parseInt(cantidadDecimales);
        }
        else
        {
			decSep =  window.opener.WindowParent().document.getElementById('hdnSeparadorDecimales').value;   
			decNum = parseInt(cantidadDecimales);		  
        } 
        
    }     
      
    if(miles == true)
    {
		
	  if(WindowParent().document.getElementById('hdnSeparadorMiles') != null)
		var milSep =  WindowParent().document.getElementById('hdnSeparadorMiles').value; 
      else
		milSep = window.opener.WindowParent().document.getElementById('hdnSeparadorMiles').value;     
    }  
    
    else
    {   
		  
          return blockNonNumbers(fld, e, decimales, true, decSep);
          return;
    }
  
    

	var sep = 0;
	var key = '';
	var i = j = 0;
	var len = len2 = 0;
	var strCheck = '0123456789';
	var aux = aux2 = '';
	var whichCode = (window.Event) ? e.which : e.keyCode;
	
	var bInteger = false;
	var decCount = 0;

	if (whichCode == 13) return true;  // Enter
	if (whichCode == 8) return true;  // Backspace (Bug fixed)
	if (whichCode == 127) return true; // Delete
	key = String.fromCharCode(whichCode);  // Get key value from key code
	if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
	len = fld.value.length;
	for(i = 0; i < len; i++)
	if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
	aux = '';
	for(; i < len; i++)
		if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
		aux += key;
		len = aux.length;
		if (len == 0) fld.value = '';
		if (decSep != '')
		{
		
		
			decCount = decNum;
			if (len == 1) fld.value = '0'+ decSep + aux;
			//if (len == 2) fld.value = '0'+ decSep + aux;
			if (len > 1)
				bInteger = true;
		}
		else
		{
			decCount = 0
			bInteger = true;
		}
		if (bInteger)
		{
			aux2 = '';
			for (j = 0, i = len - (decCount + 1); i >= 0; i--)
			{
				if (j == 3)
				{
					aux2 += milSep;
					j = 0;
				}
				aux2 += aux.charAt(i);
				j++;
			}
			fld.value = '';
			len2 = aux2.length;
			for (i = len2 - 1; i >= 0; i--)
				fld.value += aux2.charAt(i);
				fld.value += decSep + aux.substr(len - decCount, len);
		}
	return false;
}
function currencyFormatWithNegative(fld, e, miles, decimales)
{
    var decSep = '';
    var decNum = 0;
   
	var textSelected =  document.selection.createRange().text;
   
	if(fld.value.length == textSelected.length)
	{
		fld.value = "";		
		document.selection.empty();
	}
	if (e.keyCode == 8)
	{
		event.keyCode=0; 		
		fld.value = "";
		return event.keyCode;
	}
    
    if(decimales == true){
	
        if(WindowParent().document.getElementById('hdnSeparadorDecimales') != null)
        {
			decSep =  WindowParent().document.getElementById('hdnSeparadorDecimales').value;
			decNum = parseInt( WindowParent().document.getElementById('hdnNumeroDecimales').value);
        }
        else
        {
			decSep =  window.opener.WindowParent().document.getElementById('hdnSeparadorDecimales').value;   
			decNum = parseInt( window.opener.WindowParent().document.getElementById('hdnNumeroDecimales').value);		  
        } 
    }     
      
    if(miles == true)
    {
		
	  if(WindowParent().document.getElementById('hdnSeparadorMiles') != null)
		var milSep =  WindowParent().document.getElementById('hdnSeparadorMiles').value; 
      else
		milSep = window.opener.WindowParent().document.getElementById('hdnSeparadorMiles').value;     
    }  
    
    else
    {   
		  
          return blockNonNumbers(fld, e, decimales, false, decSep);
          
          return;
    }
  
    

	var sep = 0;
	var key = '';
	var i = j = 0;
	var len = len2 = 0;
	var strCheck = '-0123456789';
	var aux = aux2 = '';
	var whichCode = (window.Event) ? e.which : e.keyCode;
	
	var bInteger = false;
	var decCount = 0;

	if (whichCode == 13) return true;  // Enter
	if (whichCode == 8) return true;  // Backspace (Bug fixed)
	if (whichCode == 127) return true; // Delete
	key = String.fromCharCode(whichCode);  // Get key value from key code
	if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
	
	len = fld.value.length;
	for(i = 0; i < len; i++)
	if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
	aux = '';
	for(; i < len; i++)
		if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
		aux += key;
		len = aux.length;
		if (len == 0) fld.value = '';
		if (decSep != '')
		{
			decCount = decNum;
			if (len == 1) fld.value = '0'+ decSep + '0' + aux;
			if (len == 2) fld.value = '0'+ decSep + aux;
			if (len > 2)
				bInteger = true;
		}
		else
		{
			decCount = 0
			bInteger = true;
		}
		if (bInteger)
		{
			aux2 = '';
			for (j = 0, i = len - (decCount + 1); i >= 0; i--)
			{
				if (j == 3)
				{
					aux2 += milSep;
					j = 0;
				}
				aux2 += aux.charAt(i);
				j++;
			}
			fld.value = '';
			len2 = aux2.length;
			for (i = len2 - 1; i >= 0; i--)
				fld.value += aux2.charAt(i);
				fld.value += decSep + aux.substr(len - decCount, len);
		}
	return false;
}
function SoloNumero(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    if (key == 8) return true
    if (key > 47 && key < 58) {
        if (field.value == "") return true
        regexp = /.[0-9]{2}$/
        return !(regexp.test(field.value))
    }

    if (key == 46) {
        if (field.value == "") return false
        regexp = /^[0-9]+$/
        return regexp.test(field.value)
    }
    return false
}
function currencyFormat(fld, e, miles, decimales)
{
    var decSep = '';
    var decNum = 0;
   
	var textSelected =  document.selection.createRange().text;

	if(fld.value.length == textSelected.length)
	{
		fld.value = "";		
		document.selection.empty();
	}
	if (e.keyCode == 8)
	{
		event.keyCode=0; 		
		fld.value = "";
		return event.keyCode;
	}
    
    if(decimales == true){
	
        if(WindowParent().document.getElementById('hdnSeparadorDecimales') != null)
        {
			decSep =  WindowParent().document.getElementById('hdnSeparadorDecimales').value;
			decNum = parseInt( WindowParent().document.getElementById('hdnNumeroDecimales').value);
        }
        else
        {
			decSep =  window.opener.WindowParent().document.getElementById('hdnSeparadorDecimales').value;   
			decNum = parseInt( window.opener.WindowParent().document.getElementById('hdnNumeroDecimales').value);		  
        } 
    }     
      
    if(miles == true)
    {
		
	  if(WindowParent().document.getElementById('hdnSeparadorMiles') != null)
		var milSep =  WindowParent().document.getElementById('hdnSeparadorMiles').value; 
      else
		milSep = window.opener.WindowParent().document.getElementById('hdnSeparadorMiles').value;     
    }  
    
    else
    {   
		  
          return blockNonNumbers(fld, e, decimales, true, decSep);
          return;
    }
  
    

	var sep = 0;
	var key = '';
	var i = j = 0;
	var len = len2 = 0;
	var strCheck = '0123456789';
	var aux = aux2 = '';
	var whichCode = (window.Event) ? e.which : e.keyCode;
	
	var bInteger = false;
	var decCount = 0;

	if (whichCode == 13) return true;  // Enter
	if (whichCode == 8) return true;  // Backspace (Bug fixed)
	if (whichCode == 127) return true; // Delete
	key = String.fromCharCode(whichCode);  // Get key value from key code
	if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
	len = fld.value.length;
	for(i = 0; i < len; i++)
	if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
	aux = '';
	for(; i < len; i++)
		if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
		aux += key;
		len = aux.length;
		if (len == 0) fld.value = '';
		if (decSep != '')
		{
			decCount = decNum;
			if (len == 1) fld.value = '0'+ decSep + '0' + aux;
			if (len == 2) fld.value = '0'+ decSep + aux;
			if (len > 2)
				bInteger = true;
		}
		else
		{
			decCount = 0
			bInteger = true;
		}
		if (bInteger)
		{
			aux2 = '';
			for (j = 0, i = len - (decCount + 1); i >= 0; i--)
			{
				if (j == 3)
				{
					aux2 += milSep;
					j = 0;
				}
				aux2 += aux.charAt(i);
				j++;
			}
			fld.value = '';
			len2 = aux2.length;
			for (i = len2 - 1; i >= 0; i--)
				fld.value += aux2.charAt(i);
				fld.value += decSep + aux.substr(len - decCount, len);
		}
	return false;
}



function currencyFormatPopUp(fld, e, miles, decimales)
{
    var decSep = '';
    var decNum = 0;
    
    
	var textSelected =  document.selection.createRange().text;
   
	if(fld.value.length == textSelected.length)
	{
		fld.value = "";		
		document.selection.empty();
	}
    
    if(decimales == true){
        decSep =  window.opener.WindowParent().document.getElementById('hdnSeparadorDecimales').value;   
        decNum = parseInt( window.opener.document.getElementById('hdnNumeroDecimales').value);
    } 
    
    if(miles == true)
    {
      var milSep =  window.opener.WindowParent().document.getElementById('hdnSeparadorMiles').value; 
    
    }
    else
    {   
          return blockNonNumbers(fld, e, decimales, true, decSep);
          return;
    }
  
    

	var sep = 0;
	var key = '';
	var i = j = 0;
	var len = len2 = 0;
	var strCheck = '0123456789';
	var aux = aux2 = '';
	var whichCode = (window.Event) ? e.which : e.keyCode;
	
	var bInteger = false;
	var decCount = 0;

	if (whichCode == 13) return true;  // Enter
	if (whichCode == 8) return true;  // Backspace (Bug fixed)
	if (whichCode == 127) return true; // Delete
	key = String.fromCharCode(whichCode);  // Get key value from key code
	if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
	len = fld.value.length;
	for(i = 0; i < len; i++)
	if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
	aux = '';
	for(; i < len; i++)
		if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
		aux += key;
		len = aux.length;
		if (len == 0) fld.value = '';
		if (decSep != '')
		{
			decCount = decNum;
			if (len == 1) fld.value = '0'+ decSep + '0' + aux;
			if (len == 2) fld.value = '0'+ decSep + aux;
			if (len > 2)
				bInteger = true;
		}
		else
		{
			decCount = 0
			bInteger = true;
		}
		if (bInteger)
		{
			aux2 = '';
			for (j = 0, i = len - (decCount + 1); i >= 0; i--)
			{
				if (j == 3)
				{
					aux2 += milSep;
					j = 0;
				}
				aux2 += aux.charAt(i);
				j++;
			}
			fld.value = '';
			len2 = aux2.length;
			for (i = len2 - 1; i >= 0; i--)
				fld.value += aux2.charAt(i);
				fld.value += decSep + aux.substr(len - decCount, len);
		}
	return false;
}

function blockNonNumbers(obj, e, allowDecimal, allowNegative, charDecimal)
{
	var key;
	var isCtrl = false;
	var keychar;
	var reg;
		
	if(window.event) {
		key = e.keyCode;
		isCtrl = window.event.ctrlKey
	}
	else if(e.which) {
		key = e.which;
		isCtrl = e.ctrlKey;
	}
	
	if (isNaN(key)) return true;
	
	keychar = String.fromCharCode(key);
	
	// check for backspace or delete, or if Ctrl was pressed
	if (key == 8 || isCtrl)
	{
		return true;
	}

	reg = /\d/;
	var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
	alert (isFirstN);
	var isFirstD = allowDecimal ? keychar == charDecimal && obj.value.indexOf(charDecimal) == -1 : false;
	
	return isFirstN || isFirstD || reg.test(keychar);
}

//Coloca en seleccionada la fila en la grilla para realizar el rollover
function SelectItemGrid(objRow)
{
   objRow.className="selItems";    
} 

//Coloca en su estado normal la fila en la grilla para realizar el rollover        
function NoSelectItemGrid(objRow, className)
{   
   objRow.className=className
 
}  

function closeLayer(idLayer)
{
	var divLayer = document.getElementById("dv"+ idLayer);
	var ifrLayer = document.getElementById("ifr" + idLayer);
	if(null != divLayer && null != ifrLayer)
	{
	    divLayer.style.display = "none";
	    ifrLayer.style.display = "none";
	}
	__doPostBack('idLayer', '');
}
		
function showLayer(idLayer, x, y, width, height)
{
	var divLayer = document.getElementById("dv"+ idLayer);
	var ifrLayer = document.getElementById("ifr" + idLayer);
	if(null != divLayer && null != ifrLayer && StateLayer(idLayer) == true)
	{
		if(StateLayer(idLayer) == true)
		{
	    
			if(x == undefined || null == x)
				x = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft - width;
			if(y == undefined || null == y)
				y = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop;
    		if(width == undefined || null == width )
    			width = parseInt(divLayer.style.width);
    		if(height == undefined || null == height )
    			height = parseInt(divLayer.style.height);
	    	
    		divLayer.style.left = x + "px";
			divLayer.style.top = y + "px";
			divLayer.style.display = "inline";
			ifrLayer.style.left = x + "px";
			ifrLayer.style.top = y + "px";
			ifrLayer.style.display = "inline";				
	    }
	    else
	    {			
			divLayer.style.display = "none";
			ifrLayer.style.display = "none";
	    }
	    
	}
	
}

function StateLayer(idLayer)
{
    var divLayer = document.getElementById("dv"+ idLayer);
    if(null != divLayer && divLayer.style.display == "none")
        return true;
    return false;
}

function Narg_GetPosX(obj)
{
	var curleft = 0;
	if (obj.offsetParent)
	{
		while (obj.offsetParent)
		{
			curleft += obj.offsetLeft
			obj = obj.offsetParent;
		}
	}
	else if (obj.x)
		curleft += obj.x;
	return curleft;
}

function Narg_GetPosY(obj)
{
	var curtop = 0;
	if (obj.offsetParent)
	{
		while (obj.offsetParent)
		{
			curtop += obj.offsetTop
			obj = obj.offsetParent;
		}
	}
	else if (obj.y)
		curtop += obj.y;
	return curtop;
}

//Abrir ventana Emergente
function openPopUp(url,width,height)
{	
	var params = "resizable=yes,dependent=yes,directories=no,hotkeys=no,menubar=no,personalbar=no,scrollbars=yes,status=yes,titlebar=yes,toolbar=no";
	fullurl = url;
	wcontainer2  = open ( fullurl, "objeto", params); 
	wcontainer2.resizeTo (width,height);
	wcontainer2.moveTo (100,80);
	wcontainer2.focus();
}

function FieldClick(sender)
{
    var field = sender.parentNode;   
    var divs = field.getElementsByTagName("DIV");
    var img = sender.getElementsByTagName("IMG");
  
    if(divs.length > 0)
    {
        var divContent = divs[0];
        if(divContent.style.display == 'none')
        {
            divContent.style.display = 'inline';
            img[0].src = "../../images/icoCollapse.gif";
        }
            
        else
        {
            divContent.style.display = 'none';
            img[0].src = "../../images/icoExpand.gif";
        }
    }
    ResizePage();
}

function imprimir()
{ 
	if ((navigator.appName == "Netscape")) 
	{ 
		window.print() ; 
	} 
	else
	{ 
		var WebBrowser = '&lt;OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"&gt;&lt;/OBJECT&gt;'; 
		document.body.insertAdjacentHTML('beforeEnd', WebBrowser); WebBrowser1.ExecWB(6, -1); WebBrowser1.outerHTML = "";
	}
}

// FUNCIONES AGREGADAS POR KRAM

function dhtmlLoadScript(url){
   var e = document.createElement("script");
   e.src = url;
   e.type="text/javascript";
   document.getElementsByTagName("head")[0].appendChild(e);
}

function editarEntidad(nombreEntidad,id){
        openPopUp('INSERTUPDATE_'+nombreEntidad+'.aspx?Id'+nombreEntidad+'='+id,600,700);   
}

function marcarTodos(){
        var check_items = document.forms[0].items_borrar;
        var indice = 0;
        while(indice < check_items.length){
            check_items[indice].checked = true;
            indice = indice + 1;
        }
}

function desmarcarTodos(){
        var check_items = document.forms[0].items_borrar;
        var indice = 0;
        while(indice < check_items.length){
            check_items[indice].checked = false;
            indice = indice + 1;
        }
}

function invertirSeleccion(){
        var check_items = document.forms[0].items_borrar;
        var indice = 0;
        while(indice < check_items.length){
            if(check_items[indice].checked){
                check_items[indice].checked = false;
            }
            else{
                check_items[indice].checked = true;
            }                
            indice = indice + 1;
        }
}

function eliminarEntidad(nombreEntidad){
        var cadena = "";
        var check_items = document.forms[0].items_borrar;
        var indice = 0;
        if(check_items.length){
            while(indice < check_items.length){
                if(check_items[indice].checked){
                    if(cadena != ""){
                        cadena = cadena + "|";
                    }
                    cadena = cadena + check_items[indice].value;
                }
                indice = indice + 1;
            }
        }
        else{
            if(check_items.checked){
                cadena = cadena + check_items.value;
            }
        }
        //alert(cadena);
        if(cadena == ""){
            alert('Seleccione al menos un registro para eliminar');
        }
        else{
            dhtmlLoadScript('DELETE_'+nombreEntidad+'.aspx?ids='+cadena);
        }
}


function copyValue(sender, nameTextBox)
{ 
	var textBox = document.getElementById(nameTextBox);
	textBox.value = sender.value;
}




function setValueDataGrid(ctrlValue, idGrid, nameTextBox) 
{
							
	if(idGrid!= null && idGrid!= "" )
	{
		var idRoot    = idGrid + "__ctl"; 
		var idtxtTail = "_" + nameTextBox;
		var count   = 2;
		
		var txtctrl = document.getElementById(idRoot + count + idtxtTail);
		
		while (txtctrl != null)
		{
			txtctrl.value = ctrlValue.value;		
			count++;
			txtctrl = document.getElementById(idRoot + count + idtxtTail);
		}  

	}		
	ctlGrid = null;
}






