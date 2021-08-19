<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosSedes.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.PrestadoresSedes.PrestadoresSedes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../../Content/estiloscomunes.css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
                Administración de sedes para usuarios</h1>
    <div class="agenda_menu">
       
    </div>
    <div>
        <asp:ObjectDataSource ID="ObjectDataSourceMedicos" runat="server" 
            onselecting="ObjectDataSourceMedicos_Selecting" 
            SelectMethod="GetPrestadoresPorEspecialidad" 
            TypeName="Mercer.Tpa.Agenda.Web.DataAcess.PrestadoresDataRepository">
            <SelectParameters>
                <asp:Parameter DefaultValue="" Name="idEmpresa" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="idEspecialidad" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceSedes" runat="server" 
            onselected="ObjectDataSourceSedes_Selected" 
            onselecting="ObjectDataSourceSedes_Selecting" SelectMethod="GetSedes" 
            TypeName="Mercer.Tpa.Agenda.Web.DataAcess.SedesDataRepository">
            <SelectParameters>
                <asp:Parameter Name="idEmpresa" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:DropDownList ID="DropDownListMedicos" runat="server" AutoPostBack="True" 
            DataSourceID="ObjectDataSourceMedicos" DataTextField="Name" DataValueField="Id" 
            ondatabound="DropDownListMedicos_DataBound" 
            onselectedindexchanged="DropDownListMedicos_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:CheckBoxList ID="CheckBoxListSedes" runat="server" 
            DataSourceID="ObjectDataSourceSedes" DataTextField="Nombre" DataValueField="Id" 
            ondatabound="CheckBoxListSedes_DataBound">
        </asp:CheckBoxList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Guardar Cambios" />
    
    </div>
    </form>
</body>
</html>
