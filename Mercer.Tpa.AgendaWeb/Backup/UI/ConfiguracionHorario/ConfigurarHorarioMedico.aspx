<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigurarHorarioMedico.aspx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario.ConfigurarHorarioSede" %>

<%@ Register Src="ControlHorarioSemana.ascx" TagName="ControlHorarioSemana" TagPrefix="uc1" %>
<%@ Register Src="SelectorSemana.ascx" TagName="SelectorSemana" TagPrefix="uc2" %>
<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc3" %>
<%@ Register Src="../Utils/UserControlMessage.ascx" TagName="UserControlMessage"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Content/jquery.ui.all.css" type="text/css" rel="Stylesheet" />
    <link href="../../Content/jquery-ui-1.8rc3.custom.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../../css/admon.css" />
    <link href="../../Content/estiloscomunes.css" type="text/css" rel="Stylesheet" />
    <link href="../../Content/configuracionHorarios.css" type="text/css" rel="Stylesheet" />
    <link href="../../Scripts/jquery.timeentry.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.custom.min.js"></script>
    
    <script type="text/javascript" src="../../Scripts/jquery.ui.datepicker-es.js"></script>
    
    <script type="text/javascript" src="../../Scripts/jquery.timeentry.min.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.timeentry-es.js"></script>
       <script type="text/javascript" src="../../Scripts/common.js"></script>
    <script type="text/javascript" src="../../Scripts/ConfiguracionHorarioMedico.js"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Configuración de horario</h1>
            
            <asp:Label ID="lblMedico" runat="server" Text=""></asp:Label>
            <div>
                <asp:Label ID="lblEspecialidad" runat="server" Text=""></asp:Label>
            </div>
            
            
        <uc4:UserControlMessage ID="ctrMensaje" runat="server" />
        
        <uc3:ControlMensajeError ID="ctrError" runat="server" />
        <div class="opcionesHorario">
            <ul>
                <li><a href="#" id="linkAgregarIntervalo">Agregar intervalo</a> </li>
            </ul>
        </div>
        <div class="contenedorControlHorarioSemana">
            <a href="#" id="selectorFecha">Ir a otra semana</a>
            <div id="contenedorSeleccionFecha" style="display: none">
                <label>
                    Fecha:</label>
                <input type="text" name="cambioFecha" id="cambioFecha" class="textBox" />
            </div>
            <input type="hidden" id="fechaActual" name="fechaActual" />
            <uc1:ControlHorarioSemana ID="ctrHorarioSemana" runat="server" />
        </div>
        <div id="dialogoAgregarIntervalo" title="Nuevo intervalo de trabajo">
            <div id="contenedorCamposIntervalo">
                <table style="width: 400px; vertical-align: top;">
                    <tbody>
                        <tr>
                            <td style="width: 50%">
                                <fieldset class="FieldSet">
                                    <legend>Día de la semana<span class="textRed">*</span></legend>
                                    <asp:CheckBoxList ID="ListaDias" CssClass="listaChecksDias" runat="server">
                                        <asp:ListItem Value="Monday">Lunes</asp:ListItem>
                                        <asp:ListItem Value="Tuesday">Martes</asp:ListItem>
                                        <asp:ListItem Value="Wednesday">Miércoles</asp:ListItem>
                                        <asp:ListItem Value="Thursday">Jueves</asp:ListItem>
                                        <asp:ListItem Value="Friday">Viernes</asp:ListItem>
                                        <asp:ListItem Value="Saturday">Sábado</asp:ListItem>
                                        <asp:ListItem Value="Sunday">Domingo</asp:ListItem>
                                    </asp:CheckBoxList>
                                </fieldset>
                            </td>
                            <td style="width: 50%">
                                <fieldset class="FieldSet">
                                    <legend>Sede<span class="textRed">*</span></legend>
                                    <asp:ListBox ID="ListaSedes" CssClass="listaSedes" runat="server" Width="200px"></asp:ListBox>
                                </fieldset>
                                <fieldset class="FieldSet">
                                    <legend>Horario</legend>
                                    <div>
                                        Hora Inicio<span class="textRed">*</span></div>
                                    <input type="text" id="txtHoraInicio" class="textBox" />
                                    <div>
                                        Hora fin<span class="textRed">*</span></div>
                                    <input type="text" id="txtHoraFin"  class="textBox" />
                                    <input type="hidden" id="horaInicio" name="horaInicio" />
                                    <input type="hidden" id="horaFin" name="horaFin" />
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <fieldset id="panelVigenciaHorario">
                                    <legend>Vigencia de horario</legend><legend>Vigente desde<span class="textRed">*</span></legend>
                                    <input id="fechaVigenciaDesde" name="fechaVigenciaDesde" type="text" class="textBox"/>
                                    <legend>Vigente hasta</legend>
                                    <input id="radioVigenciaSinLimite" type="radio" name="vigenciaLimite" value="1" checked="checked" />Siempre
                                    vigente
                                    <input id="radioVigenciaConLimite" type="radio" name="vigenciaLimite" value="2" />Con
                                    limite de vigencia
                                    <div id="panelVigencia" style="display:none">
                                        <label>
                                            Fecha limite</label>
                                        <input id="fechaVigenciaLimite" name="fechaVigenciaLimite" type="text" class="textBox" />
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:Button ID="btnGuardar" CssClass="button" runat="server" Text="Aceptar" OnClientClick="return ValidarIntervalo();"
                    OnClick="btnGuardar_Click" />
                <input type="button" id="linkCancelarCreacionIntervalo" class="button" value="Cancelar" />
            </div>
        </div>
        <div id="dialogoEliminarIntervalo" title="Eliminar intervalo">
            <label id="labelFechaEliminacion">
                [Fecha]</label>
            <input id="fechaEliminacion" name="fechaEliminacion" type="hidden" />
            <input id="idIntervaloToDelete" name="idIntervaloToDelete" type="hidden" />
            <fieldset>
                <legend>Opciones</legend>
                <div>
                    <input id="radioEliminarSoloDia" name="radioEliminar" type="radio" checked="checked"
                        value="1" />
                    <label for="radioEliminarSoloDia">
                        Eliminar SOLO para este día</label>
                </div>
                <div>
                    <input id="radioEliminarEnAdelante" name="radioEliminar" type="radio" value="2" />
                    <label for="radioEliminarEnAdelante">
                        Eliminar de aquí en adelante
                    </label>
                </div>
            </fieldset>
            <asp:Button ID="btnBorrarIntervalo" CssClass="button" runat="server" Text="Borrar" OnClick="BtnDeleteIntervalo_Click" />
            <input type="submit" id="btnCancelarEliminacion" class="button" value="Cancelar" />
        </div>
    </div>
    </form>
</body>
</html>
