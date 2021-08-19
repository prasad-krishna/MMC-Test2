<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="AdminAgenda.Master"
    CodeBehind="AdminFestivos.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.AdminFestivos" %>

<%@ Register src="../Utils/ControlMensajeError.ascx" tagname="ControlMensajeError" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Días festivos
    </h1>
    <asp:ObjectDataSource ID="ObjDataSourceFestivos" runat="server" SelectMethod="GetFestivosPorMes"
        TypeName="Mercer.Tpa.Agenda.Web.DataAcess.DiasFestivosDataRepository" OnSelecting="ObjDataSourceFestivos_Selecting"
        OnSelected="ObjDataSourceFestivos_Selected">
        <SelectParameters>
            <asp:Parameter Name="idEmpresa" Type="Int32" DefaultValue="1" />
            <asp:Parameter DefaultValue="" Name="year" Type="Int32" />
            <asp:Parameter Name="month" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
    
    
    <div id="contenedorDiasFestivos">
        <asp:Label ID="lblAño" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="dbcAño" runat="server" Width="101px" AutoPostBack="True"
            OnSelectedIndexChanged="dbcAño_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="lblMes" runat="server" Text="Mes"></asp:Label>
        <asp:DropDownList ID="dbcMes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dbcMes_SelectedIndexChanged"
            Width="101px">
            <asp:ListItem Value="1">Enero</asp:ListItem>
            <asp:ListItem Value="2">Febrero</asp:ListItem>
            <asp:ListItem Value="3">Marzo</asp:ListItem>
            <asp:ListItem Value="4">Abril</asp:ListItem>
            <asp:ListItem Value="5">Mayo</asp:ListItem>
            <asp:ListItem Value="6">Junio</asp:ListItem>
            <asp:ListItem Value="7">Julio</asp:ListItem>
            <asp:ListItem Value="8">Agosto</asp:ListItem>
            <asp:ListItem Value="9">Septiembre</asp:ListItem>
            <asp:ListItem Value="10">Octubre</asp:ListItem>
            <asp:ListItem Value="11">Noviembre</asp:ListItem>
            <asp:ListItem Value="12">Diciembre</asp:ListItem>
        </asp:DropDownList>
        <div>
         <asp:Label ID="lblDiasFestivos" runat="server" Text="Días Festivos"></asp:Label>
        <asp:CheckBoxList ID="dblstDias" runat="server" DataSourceID="ObjDataSourceFestivos"
            DataTextField="dia" DataValueField="IsFestivo" Height="80px"
            OnDataBound="dblstDiasMes_DataBound" Width="171px" RepeatColumns="11" 
            RepeatDirection="Horizontal">
        </asp:CheckBoxList>
        </div>

    </div>
    <p>
        <asp:Button ID="btnGuardarCambios" CssClass="button" runat="server" Text="Guardar" 
            OnClick="BtnGuardar_Click" />
    </p>
</asp:Content>
