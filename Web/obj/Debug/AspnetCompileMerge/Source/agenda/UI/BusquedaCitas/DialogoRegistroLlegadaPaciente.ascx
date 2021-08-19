<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DialogoRegistroLlegadaPaciente.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.BusquedaCitas.DialogoRegistroLlegadaPaciente" %>
<div id="dialogoRegistrarLlegadaPaciente" title="Registrar llegada de paciente" style="display:none">
    <div>
        <p class="tip">
            Registro de llegada de paciente a la cita
        </p>
        <div class="campoFormulario">
            <label class="labelFiltro">Paciente</label>
            <label id="lblNombrePacienteLlegada">.</label>
        </div>
        <input type="hidden" name="idCitaLlegada" id="idCitaLlegada" />
        <asp:Button ID="BtnOkLlegadaPaciente" runat="server" Text="Ok" OnClick="BtnOkLlegadaPaciente_Click"
            CssClass="button" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClientClick="return cerrarDialogoLlegada();" />
    </div>
</div>
