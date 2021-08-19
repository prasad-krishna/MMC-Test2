<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DialogoRegistrarRecordatorioPaciente.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.BusquedaCitas.DialogoRegistrarRecordatorioPaciente" %>
<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<div id="dialogoRegistrarRecordatorio" title="Registrar recordatorio" style="display:none">
    <div class="contenedorFiltros">
        <input type="hidden" name="idCitaRecordatorio" id="idCitaRecordatorio" />
        <div class="campoFormulario">
            <label class="labelFiltro">
                Paciente</label>
            <span id="lblNombrePacienteRecordatorio">nombrepaciente</span>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Medio<span class="textRed">*</span></label>
            <asp:DropDownList ID="dbcMedio" runat="server" CssClass="textBox selectMedioRecordatorio"
                DataTextField="Nombre" DataValueField="Id" OnDataBound="dbcMedio_DataBound">
            </asp:DropDownList>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Información adicional</label>
            <textarea name="descripcionRecordatorio" id="descripcionRecordatorio" rows="2" cols="30" maxlength="1000" 
                cssclass="textBox"></textarea>
        </div>
        <table>
            <tbody>
                <tr>
                    <td>
                        <asp:Button ID="BtnContacto" CssClass="button" runat="server" Text="Contactado"
                            OnClick="BtnContacto_Click" OnClientClick="return validarParametrosRecordatorio();" />
                    </td>
                    <td>
                        <asp:Button ID="BtnNoContacto" runat="server" CssClass="bigButton" Text="No Contactado"
                            OnClick="BtnNoContacto_Click" OnClientClick="return validarParametrosRecordatorio();" />
                    </td>
                    <td>
                        <input type="button" class="button" id="btnCerrarDialogoRecordatorio" value="Cerrar" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
<uc1:ControlMensajeError ID="ctrError" runat="server" />
