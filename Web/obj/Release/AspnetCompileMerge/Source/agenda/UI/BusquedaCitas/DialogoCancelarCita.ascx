<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DialogoCancelarCita.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.BusquedaCitas.DialogoCancelarCita" %>
<div id="dialogoCancelarCita" title="Cancelar Cita" style="display:none">
    <div class="contenedorFiltros">
        <input type="hidden" name="idCita" id="idCitaCancelacion" />
        <div class="campoFormulario">
            <label class="labelFiltro">
                Paciente</label>
            <span id="lblNombrePaciente">nombrepaciente</span>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                A partir de<span class="textRed">*</span></label>
            <asp:DropDownList ID="DropDownListOrigen" CssClass="selectOrigen" runat="server">
            </asp:DropDownList>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Nombre de quien solicita<span class="textRed">*</span></label>
            <asp:TextBox ID="solicitante" MaxLength="250" runat="server"  CssClass="txtSolicitaCancelar textBox"></asp:TextBox>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Medio<span class="textRed">*</span></label>
            <asp:DropDownList ID="DropDownListMedio" CssClass="selectMedioCancelar" DataTextField="Nombre" DataValueField="Id"
                runat="server">
            </asp:DropDownList>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Información adicional</label>
            <textarea name="notasCancelacion" id="notasCancelacion" rows="3" cols="30" maxlength="1000"></textarea>
        </div>
        <asp:Button ID="BtnOkCancelacion" CssClass="button bigButton" runat="server" Text="Cancelar cita"
            OnClick="BtnOkCancelacion_Click" OnClientClick="return validarParametrosCancelacion();" />
        <input type="button" class="button" id="btnNoCancelar" value="Cerrar"/>
   
    </div>
</div>
