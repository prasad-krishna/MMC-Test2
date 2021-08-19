<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectorSemana.ascx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario.SelectorSemana" %>

<script type="text/javascript">
    function toggleCalendario() {
        $("#mostrarCalendario").slideToggle();
        return false;
    }
</script>
<asp:Label ID="LabelSemana" runat="server" Text=""></asp:Label>
<div id="contenedorCalendario" >
    <asp:Calendar ID="CalendarioSemana" runat="server" 
        onselectionchanged="CalendarioSemana_SelectionChanged"></asp:Calendar>
</div>

