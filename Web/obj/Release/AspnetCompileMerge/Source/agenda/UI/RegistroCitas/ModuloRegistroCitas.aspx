<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuloRegistroCitas.aspx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.RegistroCitas.ModuloRegistroCitas" %>
<%@ Import Namespace="Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda"%>

<%@ Register Src="ControlFiltroDisponibilidad.ascx" TagName="ControlFiltro" TagPrefix="controlFiltro" %>
<%@ Register Src="../CitasMedico/ControlInfoCitas.ascx" TagName="ControlInfoCitas"
    TagPrefix="controlInfoCitas" %>
<%@ Register Src="../Pacientes/ControlInfoPaciente.ascx" TagName="ControlInfoPaciente"
    TagPrefix="controlInfoPaciente" %>
<%@ Register Src="../Utils/UserControlMessage.ascx" TagName="UserControlMessage"
    TagPrefix="uc1" %>
<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Content/jquery.ui.all.css" type="text/css" rel="Stylesheet" />
    <link href="../../Content/jquery-ui-1.8rc3.custom.css" type="text/css" rel="Stylesheet" />
    <link href="../../Scripts/jquery.timeentry.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../../css/admon.css" />
    <link href="../../Content/estiloscomunes.css" type="text/css" rel="Stylesheet" />
    <link href="../../Content/registroCitas.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.custom.min.js"></script>

     <script type="text/javascript" src="../../Scripts/jquery.ui.datepicker-es.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.bgiframe.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.timeentry.min.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.timeentry-es.js"></script>

    <script type="text/javascript" src="../../Scripts/common.js"></script>

    <script type="text/javascript" src="../../Scripts/ModuloRegistroCitas.js"></script>

    <title>Módulo de búsqueda de disponibilidad</title>
</head>
<body class="moduloRegistroCitas">
    <form id="form1" runat="server">
    <h1 class="tituloRegistroCitas">
        Búsqueda de disponibilidad</h1>
    <controlInfoCitas:ControlInfoCitas ID="ctrInfoCitas" runat="server" IdCita="2" Visible="false" />
    <controlInfoPaciente:ControlInfoPaciente ID="ctrInfoPaciente" runat="server" IdEmpleado="2"
        IdBeneficiario="0" Visible="false" />
    <asp:ObjectDataSource ID="objDataSourceIntervalos" runat="server" SelectMethod="GetIntervalosDisponibilidad"
        TypeName="Mercer.Tpa.Agenda.Web.Logic.HorarioMedico.HorarioMedicoManager" OnSelecting="objDataSourceIntervalos_Selecting">
        <SelectParameters>
            <asp:Parameter Name="idEmpresa" Type="Int32" />
            <asp:Parameter Name="parametros" Type="Object" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="contenedorFiltros">
        <div class="titleBackBlue">
            Buscar disponibilidad</div>
        <controlFiltro:ControlFiltro ID="ctrFiltros" runat="server" />
        <div class="campoFormulario" style="width: 99%">
            <asp:Button ID="btnBuscarDisponibilidad" CssClass="button" runat="server" Text="Buscar"
                OnClick="BtnBuscarDisponibilidades_Click" OnClientClick="return validarParametros();" />
        </div>
    </div>
    <uc1:UserControlMessage ID="ctrMensaje" runat="server" />
    <uc2:ControlMensajeError ID="ctrError" runat="server" />
    <asp:Label ID="lblNoPuedeReprogramar" CssClass="contenedorError" runat="server" 
        Text="La cita no se puede reprogramar (Está próxima a iniciar o ya debió haber iniciado)" 
         Visible="False"></asp:Label>
    <asp:GridView ID="grdResultados" runat="server" AutoGenerateColumns="False" DataSourceID="objDataSourceIntervalos"
        OnRowDataBound="grdResultados_RowDataBound" CssClass="grid" AllowPaging="True" AllowSorting="True"
        PageSize="30" AlternatingRowStyle-CssClass="altItems" RowStyle-CssClass="norItems"
        Width="99%" EmptyDataText="No se encontraron intervalos de disponibilidad para los parámetros seleccionados"
        Visible="False">
        <RowStyle CssClass="disponibilidadRow" />
        <Columns>
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" HtmlEncode="false" DataFormatString="{0:dddd, MMMM d, yyyy}" />
            <asp:BoundField DataField="FechaInicio" HeaderText="Hora Inicio" HtmlEncode="false" DataFormatString="{0:hh:mm tt}" />
            <asp:BoundField DataField="FechaFin" HeaderText="Hora Fin" HtmlEncode="false" DataFormatString="{0:hh:mm tt}" />
            <asp:TemplateField HeaderText="Médico">
                <ItemTemplate>
                    <asp:Label ID="LabelMedico" runat="server" Text='<%#Server.HtmlEncode(Eval("Prestador.Name") as string) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sede">
                <ItemTemplate>
                    <asp:Label ID="LabelSede" runat="server" Text='<%#Server.HtmlEncode(Eval("Sede.Nombre") as string) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Panel runat="server" ID="PanelLinkRegistroCita"> 
                        <a class="seleccionDisponibilidad esDiaFestivo<%#Eval("EsDiaFestivo")%>" href="#" idmedico="<%#Eval("Prestador.Id")%>"
                            nombremedico="<%#HttpUtility.HtmlEncode((string)Eval("Prestador.Name"))%>" fecha="<%#Eval("Fecha",ConfiguracionAgendaManager.FormatoFechaGrid)%>"
                            horainicio="<%#Eval("HoraInicio")%>" horafin="<%#Eval("HoraFin")%>" horainicioformato="<%#Eval("FechaInicio","{0:hh:mm tt}")%>"
                            horafinformato="<%#Eval("FechaFin","{0:hh:mm tt}")%>" idsede="<%#Eval("Sede.Id")%>"
                            nombresede="<%#HttpUtility.HtmlEncode((string)Eval("Sede.Nombre"))%>" idtipocita="<%#Eval("Tipo.Id")%>" tipocita="<%#HttpUtility.HtmlEncode((string)Eval("Tipo.Name"))%>">
                            Registrar</a>
                    </asp:Panel>
                    <asp:LinkButton ID="LinkReprogramar" runat="server" CssClass="linkReprogramar" Visible="true"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="headerGrid" />
        <AlternatingRowStyle CssClass="altItems"></AlternatingRowStyle>
    </asp:GridView>
    <div id="dialogoConfirmarRegistroCita" title="Confirmar registro de cita" style="display:none">
        <div>
            <fieldset class="FieldSet">
                <legend><span class="seccionDetallesCita">Detalles de la cita</span> </legend>
                <table class="tblDetallesCita">
                    <tbody>
                        <tr>
                            <td>
                                Fecha:
                            </td>
                            <td>
                                <span id="lblFecha"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Hora Inicio:
                            </td>
                            <td>
                                <span id="lblHora"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Hora Fin:
                            </td>
                            <td>
                                <span id="lblHoraFinal"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Médico:
                            </td>
                            <td>
                                <span id="lblMedico"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sede:
                            </td>
                            <td>
                                <span id="lblSede"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo:
                            </td>
                            <td>
                                <span id="lblTipoCita"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </fieldset>
            <input type="hidden" name="idMedico" id="idMedico" />
            <input type="hidden" name="idSede" id="idSede" />
            <input type="hidden" name="idTipoCita" id="idTipoCita" />
            <input type="hidden" name="fecha" id="fecha" />
            <input type="hidden" name="horaInicial" id="horaInicial" />
            <input type="hidden" name="horaFinal" id="horaFinal" />
            <input type="hidden" name="nombreMedico" id="nombreMedico" />
            <input type="hidden" name="nombreSede" id="nombreSede" />
            <fieldset>
                <legend><span class="seccionNotasAdicionales">Información adicional</span> </legend>
                <textarea name="notasRegistro" id="notasRegistro" rows="2" cols="30" maxlength="1000"></textarea>
            </fieldset>
            <fieldset>
                <legend><div class="seccionTelefonosContacto">Teléfonos de contacto<span class="textRed">*</span></div> </legend>
                <textarea name="telefonosRegistro" id="telefonosRegistro" rows="2" cols="30" maxlength="100"></textarea>
            </fieldset>
            <asp:Button ID="BtnOkRegistro" CssClass="BtnOkRegistro button" runat="server" Text="Aceptar"
                OnClick="BtnOkRegistro_Click"  OnClientClick="return validarParametrosRegistro();" />
            <asp:Button ID="BtnCancelarRegistro" CssClass="BtnCancelarRegistro button" runat="server"
                Text="Cancelar" />
        </div>
    </div>
    <div id="dialogoConfirmarReprogramacionCita" title="Confirmar reprogramación de cita"
        style="display: none">
        <div>
            <input type="hidden" name="fechaReprog" id="fechaReprog" />
            <input type="hidden" name="horaInicialReprog" id="horaInicialReprog" />
            <input type="hidden" name="horaFinalReprog" id="horaFinalReprog" />
            <input type="hidden" name="idSedeReprog" id="idSedeReprog" />
            <fieldset class="FieldSet detallesCitaExistente">
                <legend>Detalles de la cita</legend>
                <div class="detalleCita">
                    <label>
                        Fecha original:
                    </label>
                    <label id="fechaOriginal">
                    </label>
                </div>
                <div class="detalleCita">
                    <label>
                        Nueva fecha:
                    </label>
                    <label id="nuevaFecha">
                    </label>
                </div>
                <div class="detalleCita">
                    <label>
                        Médico:</label>
                    <label id="nuevoMedico">
                    </label>
                </div>
                <div class="detalleCita">
                    <label>
                        Sede:</label>
                    <label id="nuevaSede">
                    </label>
                </div>
            </fieldset>
            <fieldset class="FieldSet">
                <legend><span class="seccionNotasAdicionales">Información adicional</span> </legend>
                <div>
                    <label>
                        Origen del cambio</label></div>
                <div>
                    <select style="width: 200px" id="origenReprogramacion" name="origenReprogramacion">
                        <option value="1">Paciente solicita</option>
                        <option value="2">Médico solicita</option>
                        <option value="3">Otra</option>
                    </select>
                </div>
                <div>
                    <label>
                        Nombre de quien solicita el cambio de fecha<span class="textRed">*</span></label>
                </div>
                <div>
                    <input style="width: 200px" type="text" maxlength="250" class="textBox" id="nombreSolicitaReprog"
                        name="nombreSolicitaReprog" />
                </div>
                <div id="contenedorSelectMedios" style="margin-top: 5px">
                    <label style="width: 99%">
                        Medio<span class="textRed">*</span></label>
                    <asp:DropDownList runat="server" ID="DropDownMediosComunicacionReprog" CssClass="selectMedioReprog"
                        DataTextField="Nombre" DataValueField="Id" OnDataBound="DropDownMediosComunicacionReprog_DataBound">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Notas adicionales cambio de fecha</label>
                </div>
                <textarea style="width: 200px" name="notasRegistroReprog" id="notasRegistroReprog"
                    rows="2" cols="30" maxlength="1000"></textarea>
            </fieldset>
            <fieldset>
                <legend><span class="seccionTelefonosContacto">Teléfonos de contacto</span><span class="textRed">*</span> </legend>
                <textarea name="telefonosRegistroReprog" id="telefonosRegistroReprog" rows="2" cols="30" maxlength="1000"></textarea>
            </fieldset>
            <div>
                <asp:Button ID="btnReprogramar" CssClass="BtnOkReprogramacion button" runat="server"
                    Text="Aceptar" OnClick="BtnOkReprogramacion_Click" OnClientClick="return validarParametrosReprog();" />
                <input type="button" class="button" id="cancelarReprogramacion" value="Cancelar" />
            </div>
        </div>
    </div>
    <div id="contenedorUltimoTelefono" style="display:none">
        <asp:TextBox ID="txtUltimoTelefonoPaciente" CssClass="ultimoTelefono" runat="server"></asp:TextBox>
    </div>
    
    </form>
</body>
</html>
