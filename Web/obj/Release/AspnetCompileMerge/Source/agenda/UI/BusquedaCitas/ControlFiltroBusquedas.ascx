<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlFiltroBusquedas.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.BusquedaCitas.ControlFiltroBusquedas" %>
<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
<div class="contenedorFiltros">
    <div class="titleBackBlue">
        Filtros de citas</div>
    <div class="columna25">
        <div class="campoFormulario">
            <label class="labelFiltro">
                Identificación empleado</label>
            <asp:TextBox ID="txtEmpleadoId" runat="server" CssClass="textBox"></asp:TextBox>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Paciente(Identificación)</label>
            <asp:TextBox ID="txtPacienteId" runat="server" CssClass="textBox"></asp:TextBox>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Paciente (Nombre)</label>
            <asp:TextBox ID="txtPacienteNombre" runat="server" CssClass="textBox"></asp:TextBox>
        </div>
    </div>
    <div class="columna25">
        <div class="campoFormulario">
            <label class="labelFiltro">
                Sede</label>
            <asp:DropDownList ID="dbcSedes" runat="server" OnDataBound="dbcSedes_DataBound" CssClass="textBox">
            </asp:DropDownList>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Estado</label>
            <asp:DropDownList ID="dbcEstadoCita" runat="server" CssClass="textBox">
                <asp:ListItem Selected="True">Todas</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Recordatorio</label>
            <asp:DropDownList ID="dbcRecordatorio" runat="server" CssClass="textBox">
                <asp:ListItem Value="-1" Selected="True">Todas</asp:ListItem>
                <asp:ListItem Value="0">Pendientes por recordatorio</asp:ListItem>
                <asp:ListItem Value="1">Recordatorio realizado</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="columna25">
        <div class="campoFormulario">
            <label class="labelFiltro">
                Especialidad</label>
            <asp:DropDownList ID="dbcEspecialidad" runat="server" OnDataBound="dbcEspecialidad_DataBound"
                OnSelectedIndexChanged="dbcEspecialidad_SelectedIndexChanged" AutoPostBack="True"
                CssClass="textBox">
            </asp:DropDownList>
        </div>
        <div class="campoFormulario">
            <label class="labelFiltro">
                Médicos</label>
            <asp:DropDownList ID="dbcMedicos" runat="server" OnDataBound="dbcMedicos_DataBound"
                CssClass="textBox">
                <asp:ListItem Value="0">Todos Los Medicos</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="campoComboFechas" class="campoFormulario">
            <label class="labelFiltro">
                Rango de fechas</label>
            <asp:DropDownList ID="dbcRangoFechas" runat="server" CssClass="textBox selectRangoFechas">
                <asp:ListItem Selected="True">Hoy</asp:ListItem>
                <asp:ListItem Value="EstaSemana">Esta Semana</asp:ListItem>
                <asp:ListItem Value="SiguienteSemana">Próxima Semana</asp:ListItem>
                <asp:ListItem Value="EsteMes">Este Mes</asp:ListItem>
                <asp:ListItem Value="EsteAño">Este Año</asp:ListItem>
                <asp:ListItem Value="Otro">Otro</asp:ListItem>
            </asp:DropDownList>
            <div id="selectorRangoFechas" style="display: none">
                <label class="labelFiltro">
                    Fecha Inicial</label>
                <asp:TextBox ID="txtRangoFechaInicio" CssClass="rangoFechaInicio textBox" onkeydown="return (event.keyCode!=13);"
                    runat="server"></asp:TextBox>
                <label class="labelFiltro">
                    Fecha Final</label>
                <asp:TextBox ID="txtRangoFechaFin" CssClass="rangoFechaFin textBox" onkeydown="return (event.keyCode!=13);"
                    runat="server"></asp:TextBox>
            </div>
        </div>
        <div id="campoHorarioEspecificoCheck" class="campoFormulario">
            <label class="labelFiltro">
                <asp:CheckBox ID="chkHorarioEspecifico" CssClass="horarioEspecificoCheck check" runat="server" />Rango
                de horas
            </label>
        </div>
    </div>
    <div class="columna25">
        <div class="campoFormulario">
            <label class="labelFiltro">
                Empresa</label>
            <asp:DropDownList ID="DropEmpresas" runat="server"
                CssClass="textBox" OnDataBound="DropEmpresas_DataBound">
            </asp:DropDownList>
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
                                <asp:TextBox ID="txtHoraInicio" CssClass="txtHoraInicio textBox" onkeydown="return (event.keyCode!=13);"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td style="padding: 0 5px 0 5px">
                                <asp:TextBox ID="txtHoraFin" CssClass="txtHoraFin textBox" onkeydown="return (event.keyCode!=13);"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <input type="hidden" id="horaInicio" name="horaInicio" />
                <input type="hidden" id="horaFin" name="horaFin" />
            </div>
        </div>
        <div class="campoFormulario">
            <asp:Button ID="btnBuscarCitas" runat="server" Text="Buscar Citas" OnClick="btnBuscarCitas_Click"
                CssClass="button" OnClientClick="return validarParametrosBusqueda();" />
        </div>
    </div>
    <div class="limpiarFloats">
    </div>
</div>
<uc1:ControlMensajeError ID="ctrError" runat="server" />
