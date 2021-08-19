	var fixedX = -1 // posición x (-1 Si aparecerá debajo del control)
	var fixedY = -1 // posición y (-1 Si aparecerá debajo del control)
	var startAt = 0 // 0 - Domingo ; 1 - Lunes
	var showWeekNumber = 0	// 0 - no mostrar; 1 - mostrar
	var showToday = 1		// 0 - no mostrar ; 1 - mostrar
	var imgDir="../../Images/"	// directorio de imágenes ... p.e. var imgDir="../images/"
	//var imgDir="./images/"	// directorio de imágenes ... p.e. var imgDir="../images/"

	var gotoString = "Ir al mes actual"
	var todayString = "Hoy es"
	var weekString = "Semana"
	var scrollLeftMessage = "Click para ir al mes anterior. Mantenga pulsado el botón del ratón para desplazarse automáticamente."
	var scrollRightMessage = "Click para ir al mes siguiente. Mantenga pulsado el botón del ratón para desplazarse automáticamente."
	var selectMonthMessage = "Click para seleccionar un mes."
	var selectYearMessage = "Click para seleccionar un anio."
	var selectDateMessage = "Seleccionar [date] como la fecha." // no remplace [date], sera reemplazada por la fecha en la barra de status.

	var crossobj, crossMonthObj, crossYearObj, monthSelected, yearSelected, dateSelected, omonthSelected, oyearSelected, odateSelected, monthConstructed, yearConstructed, intervalID1, intervalID2, timeoutID1, timeoutID2, ctlToPlaceValue, ctlNow, dateFormat, nStartingYear, ctlGrid

	var bPageLoaded = false
	var ie = document.all
	var dom = document.getElementById

	var ns4 = document.layers
	var today =	new Date()
	var dateNow	 = today.getDate()
	var monthNow = today.getMonth()
	var yearNow	 = today.getYear()
	var imgsrc = new Array("calDrop1.gif","calDrop2.gif","calLeft1.gif","calLeft2.gif","calRight1.gif","calRight2.gif")
	var img	= new Array()

	var bShow = false;

    /* oculta objetos <select> y <applet> (para IE solamente) */
    function hideElement( elmID, overDiv )
    {
      if( ie )
      {
        for( i = 0; i < document.all.tags( elmID ).length; i++ )
        {
          obj = document.all.tags( elmID )[i];
          if( !obj || !obj.offsetParent )
          {
            continue;
          }
      
          // Busca el desplazamiento superior e izquierdo del elemento relativo a el BODY tag.
          objLeft   = obj.offsetLeft;
          objTop    = obj.offsetTop;
          objParent = obj.offsetParent;
          
          while( objParent.tagName.toUpperCase() != "BODY" )
          {
            objLeft  += objParent.offsetLeft;
            objTop   += objParent.offsetTop;
            objParent = objParent.offsetParent;
          }
      
          objHeight = obj.offsetHeight;
          objWidth = obj.offsetWidth;
      
          if(( overDiv.offsetLeft + overDiv.offsetWidth ) <= objLeft );
          else if(( overDiv.offsetTop + overDiv.offsetHeight ) <= objTop );
          else if( overDiv.offsetTop >= ( objTop + objHeight ));
          else if( overDiv.offsetLeft >= ( objLeft + objWidth ));
          else
          {
            obj.style.visibility = "hidden";
          }
        }
      }
    }
     
    /*
    * muestra objetos <select> y <applet> (para IE solamente)
    */
    function showElement( elmID )
    {
      if( ie )
      {
        for( i = 0; i < document.all.tags( elmID ).length; i++ )
        {
          obj = document.all.tags( elmID )[i];
          
          if( !obj || !obj.offsetParent )
          {
            continue;
          }
        
          obj.style.visibility = "";
        }
      }
    }

	function HolidayRec (d, m, y, desc)
	{
		this.d = d
		this.m = m
		this.y = y
		this.desc = desc
	}

	var HolidaysCounter = 0
	var Holidays = new Array()

	function addHoliday (d, m, y, desc)
	{		
		Holidays[HolidaysCounter++] = new HolidayRec ( d, m, y, desc )
	}
	addHoliday(1, 1, 2009, '');
	addHoliday(12, 1, 2009, '');
	addHoliday(23, 3, 2009, '');
	addHoliday(9, 4, 2009, '');
	addHoliday(1, 4, 2009, '');
	addHoliday(25, 4, 2009, '');	
	addHoliday(15, 6, 2009, '');
	addHoliday(22, 6, 2009, '');
	addHoliday(29, 6, 2009, '');
	addHoliday(20, 7, 2009, '');
	addHoliday(7, 8, 2009, '');
	addHoliday(17, 8, 2009, '');
	addHoliday(12, 10, 2009, '');
	addHoliday(2, 11, 2009, '');
	addHoliday(16, 11, 2009, '');
	addHoliday(8, 12, 2009, '');
	addHoliday(25, 12, 2009, '');
	addHoliday(11, 1, 2010, '');
	addHoliday(22, 3, 2010, '');
	addHoliday(1, 4, 2010, '');
	addHoliday(2, 4, 2010, '');
	addHoliday(17, 5, 2010, '');
	addHoliday(7, 6, 2010, '');
	addHoliday(14, 6, 2010, '');
	addHoliday(5, 7, 2010, '');
	addHoliday(20, 7, 2010, '');
	addHoliday(16, 7, 2010, '');
	addHoliday(18, 10, 2010, '');
	addHoliday(1, 11, 2010, '');
	addHoliday(15, 11, 2010, '');
	addHoliday(8, 12, 2010, '');
	addHoliday(25, 12, 2010, '');
	
	if (dom)
	{
		for	(i=0;i<imgsrc.length;i++)
		{
			img[i] = new Image
			img[i].src= img + imgsrc[i]
		}
		document.write ("<div onclick='bShow=true' id='calendar'	class='div-style'><table width="+((showWeekNumber==1)?250:220)+" class='table-style'><tr class='title-background-style' ><td><table width='"+((showWeekNumber==1)?248:218)+"'><tr><td class='title-style'><B><span id='caption'></span></B></td><td align=right><a href='javascript:hideCalendar()'><IMG SRC='"+imgDir+"calClose.gif' WIDTH='15' HEIGHT='13' BORDER='0' ALT='Cerrar calendario'></a></td></tr></table></td></tr><tr><td class='body-style'><span id='content'></span></td></tr>")
			
		if (showToday==1)
		{
			document.write ("<tr class='today-style'><td><span id='lblToday'></span></td></tr>")
		}
			
		document.write ("</table></div><div id='selectMonth' class='div-style'></div><div id='selectYear' class='div-style'></div>");
	}

	var	monthName =	new	Array("Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre")
	if (startAt==0)
	{
		dayName = new Array	("Dom","Lun","Mar","Mie","Jue","Vie","Sab")
	}
	else
	{
		dayName = new Array	("Lun","Mar","Mie","Jue","Vie","Sab","Dom")
	}

	function swapImage(srcImg, destImg){
		if (ie)	{ document.getElementById(srcImg).setAttribute("src",imgDir + destImg) }
	}

	function init()	{
		if (!ns4)
		{
			if (!ie) { yearNow += 1900	}

			crossobj=(dom)?document.getElementById("calendar").style : ie? document.all.calendar : document.calendar
			hideCalendar()

			crossMonthObj=(dom)?document.getElementById("selectMonth").style : ie? document.all.selectMonth	: document.selectMonth

			crossYearObj=(dom)?document.getElementById("selectYear").style : ie? document.all.selectYear : document.selectYear

			monthConstructed=false;
			yearConstructed=false;

			if (showToday==1)
			{
				document.getElementById("lblToday").innerHTML =	todayString + " <a class='today-style' onmousemove='window.status=\""+gotoString+"\"' onmouseout='window.status=\"\"' title='"+gotoString+"' href='javascript:monthSelected=monthNow;yearSelected=yearNow;constructCalendar();'>"+dayName[(today.getDay()-startAt==-1)?6:(today.getDay()-startAt)]+", " + dateNow + " " + monthName[monthNow].substring(0,3)	+ "	" +	yearNow	+ "</a>"
			}

			sHTML1= "<span id='spanLeft'  class='title-control-normal-style' onmouseover='swapImage(\"changeLeft\",\"calLeft2.gif\");this.className=\"title-control-select-style\";window.status=\""+scrollLeftMessage+"\"' onclick='javascript:decMonth()' onmouseout='clearInterval(intervalID1);swapImage(\"changeLeft\",\"calLeft1.gif\");this.className=\"title-control-normal-style\";window.status=\"\"' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartDecMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='changeLeft' SRC='"+imgDir+"calLeft1.gif' BORDER=0>&nbsp</span>&nbsp;"
			sHTML1+="<span id='spanRight' class='title-control-normal-style' onmouseover='swapImage(\"changeRight\",\"calRight2.gif\");this.className=\"title-control-select-style\";window.status=\""+scrollRightMessage+"\"' onmouseout='clearInterval(intervalID1);swapImage(\"changeRight\",\"calRight1.gif\");this.className=\"title-control-normal-style\";window.status=\"\"' onclick='incMonth()' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartIncMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='changeRight' SRC='"+imgDir+"calRight1.gif' BORDER=0>&nbsp</span>&nbsp"
			sHTML1+="<span id='spanMonth' class='title-control-normal-style' onmouseover='swapImage(\"changeMonth\",\"calDrop2.gif\");this.className=\"title-control-select-style\";window.status=\""+selectMonthMessage+"\"' onmouseout='swapImage(\"changeMonth\",\"calDrop1.gif\");this.className=\"title-control-normal-style\";window.status=\"\"' onclick='popUpMonth()'></span>&nbsp;"
			sHTML1+="<span id='spanYear'  class='title-control-normal-style' onmouseover='swapImage(\"changeYear\",\"calDrop2.gif\");this.className=\"title-control-select-style\";window.status=\""+selectYearMessage+"\"'	onmouseout='swapImage(\"changeYear\",\"calDrop1.gif\");this.className=\"title-control-normal-style\";window.status=\"\"'	onclick='popUpYear()'></span>&nbsp;"
			
			document.getElementById("caption").innerHTML  =	sHTML1

			bPageLoaded=true
		}
	}

	function hideCalendar()	{
		if (crossobj) {
			crossobj.visibility="hidden"
			if (crossMonthObj != null){crossMonthObj.visibility="hidden"}
			if (crossYearObj !=	null){crossYearObj.visibility="hidden"}

			showElement( 'SELECT' );
			showElement( 'APPLET' );
		}
	}

	function padZero(num) {
		return (num	< 10)? '0' + num : num ;
	}

	function constructDate(d,m,y)
	{
		sTmp = dateFormat
		sTmp = sTmp.replace	("dd","<e>")
		sTmp = sTmp.replace	("d","<d>")
		sTmp = sTmp.replace	("<e>",padZero(d))
		sTmp = sTmp.replace	("<d>",d)
		sTmp = sTmp.replace	("mmm","<o>")
		sTmp = sTmp.replace	("mm","<n>")
		sTmp = sTmp.replace	("m","<m>")
		sTmp = sTmp.replace	("<m>",m+1)
		sTmp = sTmp.replace	("<n>",padZero(m+1))
		sTmp = sTmp.replace	("<o>",monthName[m])
		return sTmp.replace ("yyyy",y)
	}

	function closeCalendar() {
		var	sTmp

		hideCalendar();
		ctlToPlaceValue.value =	constructDate(dateSelected,monthSelected,yearSelected)
		var txtInicioIncapacidad = document.getElementById('txtInicioIncapacidad')
		if(txtInicioIncapacidad != null)
			CalcularDias();
		 
		if(ctlGrid!= null && ctlGrid!= "" )
		{
			var idRoot    = ctlGrid + "__ctl"; 
			var idtxtTail = '_txtFechaPrestacion';
			var count   = 2;
			var txtctrl = document.getElementById(idRoot + count + idtxtTail);
				
			while (txtctrl != null)
			{
				txtctrl.value = ctlToPlaceValue.value;		
				count++;
				txtctrl = document.getElementById(idRoot + count + idtxtTail);
			}  
		}		
		ctlGrid = null;
	}

	/*** Ventana de despliegue de los Meses ***/

	function StartDecMonth()
	{
		intervalID1=setInterval("decMonth()",80)
	}

	function StartIncMonth()
	{
		intervalID1=setInterval("incMonth()",80)
	}

	function incMonth () {
		monthSelected++
		if (monthSelected>11) {
			monthSelected=0
			yearSelected++
		}
		constructCalendar()
	}

	function decMonth () {
		monthSelected--
		if (monthSelected<0) {
			monthSelected=11
			yearSelected--
		}
		constructCalendar()
	}

	function constructMonth() {
		popDownYear()
		if (!monthConstructed) {
			sHTML =	""
			for	(i=0; i<12;	i++) {
				sName =	monthName[i];
				if (i==monthSelected){
					sName =	"<B>" +	sName +	"</B>"
				}
				sHTML += "<tr><td id='m" + i + "' onmouseover='this.className=\"dropdown-select-style\"' onmouseout='this.className=\"dropdown-normal-style\"' onclick='monthConstructed=false;monthSelected=" + i + ";constructCalendar();popDownMonth();event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
			}

			document.getElementById("selectMonth").innerHTML = "<table width=70	class='dropdown-style' cellspacing=0 onmouseover='clearTimeout(timeoutID1)'	onmouseout='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"popDownMonth()\",100);event.cancelBubble=true'>" +	sHTML +	"</table>"

			monthConstructed=true
		}
	}

	function popUpMonth() {
		constructMonth()
		crossMonthObj.visibility = (dom||ie)? "visible"	: "show"
		crossMonthObj.left = parseInt(crossobj.left) + 50
		crossMonthObj.top =	parseInt(crossobj.top) + 26
	}

	function popDownMonth()	{
		crossMonthObj.visibility= "hidden"
	}

	/*** Ventana de despliegue de los años ***/

	function incYear() {
		for	(i=0; i<7; i++){
			newYear	= (i+nStartingYear)+1
			if (newYear==yearSelected)
			{ txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;" }
			else
			{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
			document.getElementById("y"+i).innerHTML = txtYear
		}
		nStartingYear ++;
		bShow=true
	}

	function decYear() {
		for	(i=0; i<7; i++){
			newYear	= (i+nStartingYear)-1
			if (newYear==yearSelected)
			{ txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;" }
			else
			{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
			document.getElementById("y"+i).innerHTML = txtYear
		}
		nStartingYear --;
		bShow=true
	}

	function selectYear(nYear) {
		yearSelected=parseInt(nYear+nStartingYear);
		yearConstructed=false;
		constructCalendar();
		popDownYear();
	}

	function constructYear() {
		popDownMonth()
		sHTML =	""
		if (!yearConstructed) {

			sHTML =	"<tr><td align='center'	onmouseover='this.className=\"dropdown-select-style\"' onmouseout='clearInterval(intervalID1);this.className=\"dropdown-normal-style\"' onmousedown='clearInterval(intervalID1);intervalID1=setInterval(\"decYear()\",30)' onmouseup='clearInterval(intervalID1)'>-</td></tr>"
			j =	0
			nStartingYear =	yearSelected-3
			for	(i=(yearSelected-3); i<=(yearSelected+3); i++) {
				sName =	i;
				if (i==yearSelected){
					sName =	"<B>" +	sName +	"</B>"
				}

				sHTML += "<tr><td id='y" + j + "' onmouseover='this.className=\"dropdown-select-style\"' onmouseout='this.className=\"dropdown-normal-style\"' onclick='selectYear("+j+");event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
				j ++;
			}

			sHTML += "<tr><td align='center' onmouseover='this.className=\"dropdown-select-style\"' onmouseout='clearInterval(intervalID2);this.className=\"dropdown-normal-style\"' onmousedown='clearInterval(intervalID2);intervalID2=setInterval(\"incYear()\",30)'	onmouseup='clearInterval(intervalID2)'>+</td></tr>"

			document.getElementById("selectYear").innerHTML	= "<table width=44 class='dropdown-style' onmouseover='clearTimeout(timeoutID2)' onmouseout='clearTimeout(timeoutID2);timeoutID2=setTimeout(\"popDownYear()\",100)' cellspacing=0>"	+ sHTML	+ "</table>"

			yearConstructed	= true
		}
	}

	function popDownYear() {
		clearInterval(intervalID1)
		clearTimeout(timeoutID1)
		clearInterval(intervalID2)
		clearTimeout(timeoutID2)
		crossYearObj.visibility= "hidden"
	}

	function popUpYear() {
		var	leftOffset

		constructYear()
		crossYearObj.visibility	= (dom||ie)? "visible" : "show"
		leftOffset = parseInt(crossobj.left) + document.getElementById("spanYear").offsetLeft
		if (ie)
		{
			leftOffset += 6
		}
		crossYearObj.left =	leftOffset
		crossYearObj.top = parseInt(crossobj.top) +	26
	}

	/*** Calendario ***/

	function WeekNbr(today)
    {
		Year = takeYear(today);
		Month = today.getMonth();
		Day = today.getDate();
		now = Date.UTC(Year,Month,Day+1,0,0,0);
		var Firstday = new Date();
		Firstday.setYear(Year);
		Firstday.setMonth(0);
		Firstday.setDate(1);
		then = Date.UTC(Year,0,1,0,0,0);
		var Compensation = Firstday.getDay();
		if (Compensation > 3) Compensation -= 4;
		else Compensation += 3;
		NumberOfWeek =  Math.round((((now-then)/86400000)+Compensation)/7);
		return NumberOfWeek;
	}

	function takeYear(theDate)
	{
		x = theDate.getYear();
		var y = x % 100;
		y += (y < 38) ? 2000 : 1900;
		return y;
	}

	function constructCalendar () {
		var dateMessage
		var	startDate =	new	Date (yearSelected,monthSelected,1)
		var	endDate	= new Date (yearSelected,monthSelected+1,1);
		endDate	= new Date (endDate	- (24*60*60*1000));
		numDaysInMonth = endDate.getDate()
		

		datePointer	= 0
		dayPointer = startDate.getDay() - startAt
		
		if (dayPointer<0)
		{
			dayPointer = 6
		}

		sHTML =	"<table	border=0 class='body-style'><tr>"

		if (showWeekNumber==1)
		{
			sHTML += "<td width=27><b>" + weekString + "</b></td><td width=1 rowspan=7 class='weeknumber-div-style'><img src='"+imgDir+"calDivider.gif' width=1></td>"
		}

		for	(i=0; i<7; i++)	{
			sHTML += "<td width='27' align='right'><B>"+ dayName[i]+"</B></td>"
		}
		sHTML +="</tr><tr>"
		
		if (showWeekNumber==1)
		{
			sHTML += "<td align=right>" + WeekNbr(startDate) + "&nbsp;</td>"
		}

		for	( var i=1; i<=dayPointer;i++ )
		{
			sHTML += "<td>&nbsp;</td>"
		}
	
		for	( datePointer=1; datePointer<=numDaysInMonth; datePointer++ )
		{
			dayPointer++;
			sHTML += "<td align=right>"

			var sStyle="normal-day-style"; //día laborable

			if ((datePointer==dateNow)&&(monthSelected==monthNow)&&(yearSelected==yearNow)) //today
			{ sStyle = "current-day-style"; } 
			else if	(dayPointer % 7 == (startAt * -1) +1) //fin del día de la semana
			{ sStyle = "end-of-weekday-style"; }

			// Día seleccionado
			if ((datePointer==odateSelected) &&	(monthSelected==omonthSelected)	&& (yearSelected==oyearSelected))
			{ sStyle += " selected-day-style"; }

			sHint = ""
			for (k=0;k<HolidaysCounter;k++)
			{
				if ((parseInt(Holidays[k].d)==datePointer)&&(parseInt(Holidays[k].m)==(monthSelected+1)))
				{
					if ((parseInt(Holidays[k].y)==0)||((parseInt(Holidays[k].y)==yearSelected)&&(parseInt(Holidays[k].y)!=0)))
					{
						sStyle += " holiday-style";
						sHint+=sHint==""?Holidays[k].desc:"\n"+Holidays[k].desc
					}
				}
			}

			var regexp= /\"/g
			sHint=sHint.replace(regexp,"&quot;")

			dateMessage = "onmousemove='window.status=\""+selectDateMessage.replace("[date]",constructDate(datePointer,monthSelected,yearSelected))+"\"' onmouseout='window.status=\"\"' "

			sHTML += "<a class='"+sStyle+"' "+dateMessage+" title=\"" + sHint + "\" href='javascript:dateSelected="+datePointer+";closeCalendar();'>&nbsp;" + datePointer + "&nbsp;</a>"

			sHTML += ""
			if ((dayPointer+startAt) % 7 == startAt) { 
				sHTML += "</tr><tr>" 
				if ((showWeekNumber==1)&&(datePointer<numDaysInMonth))
				{
					sHTML += "<td align=right>" + (WeekNbr(new Date(yearSelected,monthSelected,datePointer+1))) + "&nbsp;</td>"
				}
			}
		}

		document.getElementById("content").innerHTML   = sHTML
		document.getElementById("spanMonth").innerHTML = "&nbsp;" +	monthName[monthSelected] + "&nbsp;<IMG id='changeMonth' SRC='"+imgDir+"calDrop1.gif' BORDER=0>"
		document.getElementById("spanYear").innerHTML =	"&nbsp;" + yearSelected	+ "&nbsp;<IMG id='changeYear' SRC='"+imgDir+"calDrop1.gif'  BORDER=0>"
	}

	function MostrarCalendario(ctl, ctl2, format, grid) {
		var leftpos=0
		var toppos=0
		if(grid != null)
			ctlGrid = grid
		
		if (bPageLoaded)
		{
			if ( crossobj.visibility == "hidden" ) {
				ctlToPlaceValue	= ctl2
				dateFormat=format;
				

				formatChar = " "
				aFormat	= dateFormat.split(formatChar)
				if (aFormat.length<3)
				{
					formatChar = "/"
					aFormat	= dateFormat.split(formatChar)
					if (aFormat.length<3)
					{
						formatChar = "."
						aFormat	= dateFormat.split(formatChar)
						if (aFormat.length<3)
						{
							formatChar = "-"
							aFormat	= dateFormat.split(formatChar)
							if (aFormat.length<3)
							{
								// Formato de fecha inválido
								formatChar=""
							}
						}
					}
				}

				tokensChanged =	0
				if ( formatChar	!= "" )
				{
					// usar la fecha del usuario
					aData =	ctl2.value.split(formatChar)

					for	(i=0;i<3;i++)
					{
						if ((aFormat[i]=="d") || (aFormat[i]=="dd"))
						{
							dateSelected = parseInt(aData[i], 10)
							tokensChanged ++
						}
						else if	((aFormat[i]=="m") || (aFormat[i]=="mm"))
						{
							monthSelected =	parseInt(aData[i], 10) - 1
							tokensChanged ++
						}
						else if	(aFormat[i]=="yyyy")
						{
							yearSelected = parseInt(aData[i], 10)
							tokensChanged ++
						}
						else if	(aFormat[i]=="mmm")
						{
							for	(j=0; j<12;	j++)
							{
								if (aData[i]==monthName[j])
								{
									monthSelected=j
									tokensChanged ++
								}
							}
						}
					}
				}

				if ((tokensChanged!=3)||isNaN(dateSelected)||isNaN(monthSelected)||isNaN(yearSelected))
				{
					dateSelected = dateNow
					monthSelected = monthNow
					yearSelected = yearNow
				}

				odateSelected=dateSelected
				omonthSelected=monthSelected
				oyearSelected=yearSelected

				aTag = ctl
				do {
					aTag = aTag.offsetParent;
					leftpos	+= aTag.offsetLeft;
					toppos += aTag.offsetTop;
				} while(aTag.tagName!="BODY");

				
				//crossobj.left = fixedX==-1 ? ctl.offsetLeft	+ leftpos  :	fixedX
				crossobj.left = fixedX==-1 ? ctl.offsetLeft	+ leftpos - 50 :	fixedX  // Centrarlo en el control

				crossobj.top = fixedY==-1 ? ctl.offsetTop +	toppos + ctl.offsetHeight +	2 :	fixedY
				constructCalendar (1, monthSelected, yearSelected);
				crossobj.visibility=(dom||ie)? "visible" : "show"

				hideElement( 'SELECT', document.getElementById("calendar") );
				hideElement( 'APPLET', document.getElementById("calendar") );

				bShow = true;
			}
		}
		else
		{
			init()
			MostrarCalendario(ctl, ctl2, format)
		}
	}
/*	document.onkeypress = function hidecal1 () {
		if (event.keyCode==27)
		{
			hideCalendar()
		}

	}     */
	document.onclick = function hidecal2 () { 		
		if (!bShow)
		{
			hideCalendar()
		}
		bShow = false
	}	


