<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notificador.ascx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.Notificaciones.Notificador" %>

<div class="contenedorDataListNotificacion">

<asp:DataList ID="dlNotificaciones" runat="server" CssClass="contenedorNotificacion" 
    onitemdatabound="DataList1_ItemDataBound" RepeatDirection="Horizontal">
    <HeaderTemplate>
        Novedades:
    </HeaderTemplate>
    <ItemTemplate>
    <div class="notificationGroup">       
        
        <asp:Label ID="LabelTitulo" runat="server" CssClass="labelDescripcion" Text='<%#Eval("Titulo")%>'></asp:Label>
        <asp:Label ID="LabelInfo" runat="server" CssClass="LabelInfo" Text='<%#Eval("Info")%>'></asp:Label>
    </div>
    </ItemTemplate>
   
</asp:DataList>
</div>