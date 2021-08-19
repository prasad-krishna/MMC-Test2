<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlInfoCitas.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.CitasMedico.ControlInfoCitas" %>
<div class="infoCitas">
    <fieldset class="FieldSet">
        <legend><span class="tituloInfoCita">Información de la Cita</span></legend>
        <table>
            <tbody>
                <tr>
                    <td>
                        <label>
                            Fecha</label>
                    </td>
                    <td>
                        <asp:Label ID="valFecha" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <label>
                            Tipo</label>
                    </td>
                    <td>
                        <asp:Label ID="valTipo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Hora Inicio</label>
                    </td>
                    <td>
                        <asp:Label ID="valHoraInicio" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <label>
                            Duración</label>
                    </td>
                    <td>
                        <asp:Label ID="valDuracion" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Hora Finalización</label>
                    </td>
                    <td>
                        <asp:Label ID="valHoraFin" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <label>
                            Sede</label>
                    </td>
                    <td>
                        <asp:Label ID="valSede" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><label>Paciente</label></td>
                    <td>        <asp:Label ID="valPaciente" runat="server" Text=""></asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </fieldset>

</div>
