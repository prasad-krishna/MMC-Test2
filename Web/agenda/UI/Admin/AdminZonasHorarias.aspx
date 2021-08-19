<%@ Page Title="" Language="C#" MasterPageFile="AdminAgenda.Master" AutoEventWireup="true"
    CodeBehind="AdminZonasHorarias.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.AdminZonasHorarias" %>

<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<%@ Register Src="../Utils/UserControlMessage.ascx" TagName="UserControlMessage"
    TagPrefix="uc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
    <div class="contenedorContenidoAgenda">
        <fieldset class="panelConfiguracionAgenda" style="width: 450px">
            <legend>Zona horaria</legend>
            <div>
                <asp:DropDownList ID="dbcZonasHorarias" runat="server" DataSourceID="ObjectDataSourceZonasHorarias"
                    DataTextField="Nombre" DataValueField="Id" OnDataBound="dbcZonasHorarias_DataBound">
                </asp:DropDownList>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSourceZonasHorarias" runat="server" SelectMethod="GetZonasHorarias"
                TypeName="Mercer.Tpa.Agenda.Web.DataAcess.HorarioRepository" OnSelecting="ObjectDataSourceZonasHorarias_Selecting">
            </asp:ObjectDataSource>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Zona" CssClass="bigButton"
                OnClick="btnGuardar_Click" />
            <asp:Label ID="LblZonaHorariaActualizada" runat="server" Text="Zona horaria actualizada"
                Visible="false"></asp:Label>
        </fieldset>
        <uc1:UserControlMessage ID="ctrMensaje" runat="server" />
    </div>
</asp:Content>
