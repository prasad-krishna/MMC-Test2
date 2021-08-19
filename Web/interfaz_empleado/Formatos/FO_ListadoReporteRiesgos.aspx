<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FO_ListadoReporteRiesgos.aspx.cs" Inherits="TPA.interfaz_admon.formatos.FO_ListadoReporteRiesgos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">		
	</HEAD>
<body>
    <form id="form1" runat="server">
    <div>    
        <span>LISTADO RIESGOS<br />
        </span>
        &nbsp;<asp:GridView ID="gvDetalle"  runat="server">
            <RowStyle  />
            <HeaderStyle  Font-Bold="true" />
            <AlternatingRowStyle  />
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
