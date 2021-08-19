<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestControlDia.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario.TestControlDia" %>

<%@ Register src="ControlHorarioDia.ascx" tagname="ControlHorarioDia" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:ControlHorarioDia ID="ControlHorarioDia1" runat="server" />
        <asp:Button ID="Mostrar" runat="server" Text="Ver intervalos dia" 
            onclick="Mostrar_Click" />
        <asp:Label ID="LabelNumIntervalos" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
