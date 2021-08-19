<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlInfoPaciente.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.Pacientes.ControlInfoPaciente" %>
<fieldset class="infoPaciente">
    <legend><span class="tituloInfoPaciente">Información de paciente</span> </legend>
    <table>
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="valNombre" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEdad" runat="server" Text="Edad: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="valEdad" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="valIdentificacion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTelefonos" runat="server" Text="Teléfonos: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="valTelefonos" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</fieldset>
