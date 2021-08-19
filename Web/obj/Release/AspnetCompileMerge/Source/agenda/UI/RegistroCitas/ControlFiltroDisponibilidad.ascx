<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlFiltroDisponibilidad.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.RegistroCitas.ControlFiltroDisponibilidad" %>
<%@ Register src="../Utils/ControlMensajeError.ascx" tagname="ControlMensajeError" tagprefix="uc1" %>
<div id="containerFiltro">
    <div class="">
        <div class="columna30">
            <div class="campoFormulario">
                <label class="labelFiltro">
                    Sede</label>
                <asp:DropDownList ID="dbcSedes" runat="server" OnDataBound="dbcSedes_DataBound" CssClass="textBox comboSedes">
                </asp:DropDownList>
            </div>
            <div class="campoFormulario">
                <label class="labelFiltro">
                    Tipo Cita<span class="textRed">*</span></label>
                <asp:DropDownList ID="dbcTipoCita" runat="server" OnDataBound="dbcTipoCita_DataBound"
                    AutoPostBack="True" OnSelectedIndexChanged="dbcTipoCita_SelectedIndexChanged"
                    CssClass="textBox listaTiposCita">
                    <asp:ListItem Selected="True">Todas</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="campoFormulario">
                <label class="labelFiltro">
                    Duración (Minutos)<span class="textRed">*</span></label>
                <asp:TextBox ID="txtDuracion" MaxLength="3" runat="server" CssClass="duracionCita textBox"  onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
            </div>
        </div>
        <div class="columna30">
            <div class="campoFormulario">
                <label class="labelFiltro">
                    Especialidad</label>
                <asp:DropDownList ID="dbcEspecialidades" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dbcEspecialidades_SelectedIndexChanged"
                    CssClass="textBox" OnDataBound="dbcEspecialidades_DataBound">
                </asp:DropDownList>
            </div>
            <div class="campoFormulario">
                <label class="labelFiltro">
                    Médico</label>
                <asp:DropDownList ID="dbcMedicos" runat="server" CssClass="textBox" OnDataBound="dbcMedicos_DataBound">
                </asp:DropDownList>
            </div>
        </div>
        <div class="columna30">
            <div id="campoSelectRangoFechas" class="campoFormulario">
                <label class="labelFiltro">
                    Rango de fechas</label>
                <asp:DropDownList ID="dbcRangoFechas" runat="server" CssClass="textBox selectRangoFechas">
                    <asp:ListItem Selected="True">Hoy</asp:ListItem>
                    <asp:ListItem Value="EstaSemana">Esta Semana</asp:ListItem>
                    <asp:ListItem Value="SiguienteSemana">Próxima Semana</asp:ListItem>
                    <asp:ListItem Value="EsteMes">Este Mes</asp:ListItem>
                    <asp:ListItem Value="Otro">Otro</asp:ListItem>
                </asp:DropDownList>
                <div id="selectorRangoFechas" style="display: none">
                    <label class="labelFiltro">
                        Fecha Inicial</label>
                    <asp:TextBox ID="txtRangoFechaInicio" CssClass="rangoFechaInicio textBox"  onkeydown = "return (event.keyCode!=13);" runat="server"></asp:TextBox>
                    <label class="labelFiltro">
                        Fecha Final</label>
                     <asp:TextBox ID="txtRangoFechaFin" CssClass="rangoFechaFin textBox"  onkeydown = "return (event.keyCode!=13);"  runat="server"></asp:TextBox>
                </div>
            </div>
            <div id="campoCheckHorarioEspecifico" class="campoFormulario">
                <label class="labelFiltro">
                    <asp:CheckBox ID="chkHorarioEspecifico" CssClass="horarioEspecificoCheck check" runat="server" />Rango
                    de horas
                </label>
            </div>
            <div class="campoFormulario">
                <div id="contenedorRangoHoras" style="display: none">
                    <label class="labelFiltro">
                        Hora Inicio - Hora Fin</label>
                    <table style="width: 100%">
                        <tbody>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0 5px 0 5px">                                    
                                    <asp:TextBox ID="txtHoraInicio" CssClass="txtHoraInicio textBox"  onkeydown = "return (event.keyCode!=13);" runat="server"></asp:TextBox>
                                </td>
                                <td style="padding: 0 5px 0 5px">
                                   <asp:TextBox ID="txtHoraFin" CssClass="txtHoraFin textBox"  onkeydown = "return (event.keyCode!=13);" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <input type="hidden" id="horaInicio" name="horaInicio" />
                    <input type="hidden" id="horaFin" name="horaFin" />
                </div>
                
            </div>
        </div>
    </div>
    <div style="clear: both">
    </div>
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
</div>
