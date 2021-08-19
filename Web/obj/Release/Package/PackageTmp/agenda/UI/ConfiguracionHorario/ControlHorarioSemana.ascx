<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlHorarioSemana.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario.ControlHorarioSemana" %>
<%@ Register src="ControlHorarioDia.ascx" tagname="ControlHorarioDia" tagprefix="uc1" %>

<%@ Register src="SelectorSemana.ascx" tagname="SelectorSemana" tagprefix="uc2" %>
<table id="tblHorarioMedico" class="tblHorarioMedico" style="width:400px;">
    <tr>
        <td class="contenedorHorarioDia">            
            <uc1:ControlHorarioDia ID="HorarioLunes" runat="server" />            
        </td>
        <td class="contenedorHorarioDia">
            <uc1:ControlHorarioDia ID="HorarioMartes" runat="server" />
        </td>
        <td class="contenedorHorarioDia">
            <uc1:ControlHorarioDia ID="HorarioMiercoles" runat="server" />
        </td>
        <td class="contenedorHorarioDia">
            <uc1:ControlHorarioDia ID="HorarioJueves" runat="server" />
        </td>
        <td class="contenedorHorarioDia">
            <uc1:ControlHorarioDia ID="HorarioViernes" runat="server" />
        </td>
        <td class="contenedorHorarioDia">
            <uc1:ControlHorarioDia ID="HorarioSabado" runat="server" EnableTheming="True" />
        </td>
        <td class="contenedorHorarioDia">
            <uc1:ControlHorarioDia ID="HorarioDomingo" runat="server" />
        </td>
    </tr>
</table>
