<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlMensajeError.ascx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Utils.ControlMensajeError" %>
<div class="contenedorError">
    <asp:Label ID="LblError" runat="server" Text=""></asp:Label>
    <a href="#" class="linkDetallesError">(Detalles)</a>
    <div class="detallesError" style="display:none">
        <asp:Label ID="lblDetallesError" runat="server" Text=""></asp:Label>
    </div>
</div>