<%@ Page Language="c#" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.reportes.reporte" CodeBehind="reporte.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>Reportes</title>		
		<link href="../../css/admon.css" rel="stylesheet" type="text/css">
			<script src="../../scripts/utilReportes.js"></script>
			<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<form id="form1" runat="server">
		</form>
		<div>
		<%--
                //Auto:Diego Montejano Avila
                //Proyecto: Auditoria 2014
                //Fecha: 2014/09/17
                //Observaciones: Se utiliza la dll de Microsoft para eliminar el XSS
		 --%>
			<div id="contenido" runat="server" Style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW-Y: auto; OVERFLOW-X: auto;;Z-INDEX: 1100; BORDER-LEFT: dimgray 1px outset; WIDTH: 100%; BORDER-BOTTOM: dimgray 1px outset; POSITION: absolute; HEIGHT: 450px; BACKGROUND-COLOR: white">
				<span><a href="principalreportes.aspx" class="textogris" target="_self">Volver a reportes</a> | 
				<a id="lnkExcel" runat="server" href="javascript:document.formularioReporte.target='_blank';document.formularioReporte.action ='reporteExcel.aspx';document.formularioReporte.submit()" class="textogris">Generar Excel</a></span>
				<%--<span><a href="formaReporte.aspx?xml=<% Response.Write(Microsoft.Security.Application.Encoder.HtmlEncode(Request["xml"].ToString())); %>" class="textogris" target="_self">Volver a reportes</a> | <a href="javascript:document.formularioReporte.target='_blank';document.formularioReporte.action ='reporteExcel.aspx';document.formularioReporte.submit()" class="textogris">Generar Excel</a></span>--%>
        <label runat=server id=contenidoReporte></label>
			</div>
			<br>
		</div>
	</body>
</HTML>
