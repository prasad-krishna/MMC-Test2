<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusquedaCitas.aspx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.BusquedaCitas.BusquedaCitas" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="ControlFiltroBusquedas.ascx" TagName="ControlFiltroBusquedas" TagPrefix="uc1" %>
<%@ Register Src="DialogoCancelarCita.ascx" TagName="DialogoCancelarCita" TagPrefix="uc2" %>
<%@ Register Src="DialogoRegistrarRecordatorioPaciente.ascx" TagName="DialogoRegistrarRecordatorioPaciente"
    TagPrefix="uc3" %>
<%@ Register Src="DialogoRegistroLlegadaPaciente.ascx" TagName="DialogoRegistroLlegadaPaciente"
    TagPrefix="uc4" %>
<%@ Register Src="ConvencionesEstados.ascx" TagName="ConvencionesEstados" TagPrefix="uc5" %>
<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc6" %>
<%@ Register Src="../Utils/UserControlMessage.ascx" TagName="UserControlMessage"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<link href="../../Content/jquery.ui.all.css" type="text/css" rel="Stylesheet" />--%>
    <link href="../../Content/jquery-ui-1.8rc3.custom.css" type="text/css" rel="Stylesheet" />
    <link href="../../Scripts/jquery.timeentry.css" type="text/css" rel="Stylesheet" />
    <link type="text/css" rel="Stylesheet" href="../../../css/admon.css" />
    <link href="../../Content/estiloscomunes.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.custom.min.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.ui.datepicker-es.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.bgiframe.js"></script>

    <script type="text/javascript" src="../../Scripts/jquery.timeentry.min.js"></script>

    <script type="text/javascript" src="../../Scripts/common.js"></script>

    <script type="text/javascript" src="../../Scripts/BusquedaCitas.js"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1 class="tituloBuscar">
            Busqueda de citas</h1>
        <div class="agenda_menu">
        </div>
        <div class="limpiarFloats">
        </div>
        <uc1:ControlFiltroBusquedas ID="ctrFiltros" runat="server" />
        <uc7:UserControlMessage ID="ctrMensaje" runat="server" />
        <uc6:ControlMensajeError ID="ctrError" runat="server" />
        <uc5:ConvencionesEstados ID="ctrEstados" runat="server" />
        <fieldset class="FieldSet expandible" style="width:300px">
            <legend><a href="#" class="linkToggle">Cancelar múltiples citas</a> </legend>
            <div class="contenido" style="display: none">
                <div class="infoCancelacionMasiva">
                    <div class="contenedorFiltros">
                        <div class="campoFormulario">
                            <label class="labelFiltro">
                                A partir de<span class="textRed">*</span></label>
                            <asp:DropDownList ID="dbcOrigenCancelacionMasiva" CssClass="selectOrigen" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="campoFormulario">
                            <label class="labelFiltro">
                                Nombre de quien solicita<span class="textRed">*</span></label>
                            <asp:TextBox ID="txtNombreSolicitaCancelacionMasiva" runat="server" CssClass="txtSolicitaCancelacionMasiva textBox"></asp:TextBox>
                        </div>
                        <div class="campoFormulario">
                            <label class="labelFiltro">
                                Medio<span class="textRed">*</span></label>
                            <asp:DropDownList ID="dbcMedioCancelacionMasiva" CssClass="selectMedioCancelacionMasiva" DataTextField="Nombre"
                                DataValueField="Id" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="campoFormulario">
                            <label class="labelFiltro">
                                Información adicional<span class="textRed">*</span></label>
                            <asp:TextBox ID="txtNotasCancelacionMasiva" class="notasCancelacionMasiva" TextMode="MultiLine" Columns="30" Rows="3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <a id="linkSeleccionarTodos" href="#" title="Seleccionar todos los registros de la página actual">
                                        Seleccionar todas</a>
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelarSeleccionadas" CssClass="bigButton" runat="server"
                                        Text="Cancelar citas" OnClick="btnCancelarSeleccionadas_Click" OnClientClick="return validarCancelacionMasiva();" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </fieldset>
        <table>
        </table>
        <div id="contenedorGridResultados" class="gridViewBusqueda">
            <asp:GridView ID="grdResultados" runat="server" Width="99%" AllowPaging="True" DataSourceID="objDataSourceCitas"
                AutoGenerateColumns="False" PageSize="30" AllowSorting="True" CssClass="grid"
                AlternatingRowStyle-CssClass="altItems" RowStyle-CssClass="norItems" EmptyDataText="No se encontraron registros"
                Visible="False" DataKeyNames="Id">
                <RowStyle CssClass="norItems"></RowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSeleccionFila" CssClass="check checkSeleccion" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Menu">
                        <ItemTemplate>
                            <a class="menuGrid" href="#" idcita="<%#Eval("Id")%>" dia="<%#Eval("StartDate")%>"
                                nombrepaciente="<%#HttpUtility.HtmlEncode((string)Eval("NombrePaciente"))%>" idempleado="<%#Eval("IdEmpleado")%>"
                                idbeneficiario="<%#Eval("IdBeneficiario")%>" estado="<%#Eval("EstadoCita")%>"><img style="border:none" src="../../Content/images/icoMenu.gif"/></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FormatoDia" HeaderText="Dia" SortExpression="fechaInicio" />
                    <asp:BoundField DataField="FormatoHoras" HeaderText="Hora" />
                    <asp:BoundField DataField="TotalMinutes" HeaderText="Duración(Min)" />
                    <asp:BoundField DataField="NombreTipoCita" HeaderText="Tipo Cita" ItemStyle-CssClass="columnaGrid"
                        ItemStyle-Wrap="true" SortExpression="NombreTipoCita" />
                    <asp:BoundField DataField="NombrePaciente" HeaderText="Paciente" ItemStyle-CssClass="columnaGrid"
                        ItemStyle-Wrap="true" SortExpression="nombrePaciente" />
                    <%--<asp:BoundField DataField="PacienteIdentificacion" HeaderText="Identificación" />--%>
                    <asp:BoundField DataField="NombrePrestador" HeaderText="Médico" ItemStyle-CssClass="columnaGrid"
                        ItemStyle-Wrap="true" SortExpression="NombrePrestador" />
                    <asp:BoundField DataField="NombreSede" HeaderText="Sede" ItemStyle-CssClass="columnaGrid"
                        ItemStyle-Wrap="true" SortExpression="nombreSede" />
                    <asp:BoundField DataField="RecordatorioLegible" HeaderText="Aviso" />
                    <asp:BoundField DataField="TelefonosContacto" HeaderText="Teléfonos" ItemStyle-CssClass="columnaGrid"
                        ItemStyle-Wrap="true" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>                            
                           <img style="border:none" src="../../Content/images/<%#Eval("EstadoCita")%>.gif"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="#0066FF" Font-Bold="True" ForeColor="White" />
                <HeaderStyle CssClass="headerGrid" />
                <AlternatingRowStyle CssClass="altItems"></AlternatingRowStyle>
            </asp:GridView>
        </div>
        <asp:ObjectDataSource ID="objDataSourceCitas" runat="server" OnSelecting="objDataSourceCitas_Selecting"
            SelectMethod="GetCitasBusqueda" TypeName="Mercer.Tpa.Agenda.Web.DataAcess.CitasDataRepository"
            EnablePaging="True" SelectCountMethod="GetTotalCitasBusqueda" SortParameterName="sortExpression">
            <SelectParameters>
                <asp:Parameter Name="parametros" Type="Object" />
                <asp:Parameter Name="sortExpression" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
        <div id="divAccionesCita" class="menuContextualGrid">
            <a href="#" class="linkCerrarMenu">
                <img style="border:none" src="../../Content/images/imgClose.gif" />
            </a>
            <ul>
                <li id="liregRecordatorio"><a href="#" id="regRecordatorio">Registrar recordatorio</a></li>
                <li id="liregLlegadaPaciente"><a href="#" id="regLlegadaPaciente">Registrar llegada paciente</a></li>
                <li id="lirepCita"><a href="#" id="repCita">Reprogramar cita</a></li>
                <li id="licanCita"><a href="#" id="canCita">Cancelar cita</a></li>
                <li id="lisinAcciones"><span id="sinAcciones">No hay ninguna acción asociada para el estado de la cita</span></li>
            </ul>
        </div>
        <uc2:DialogoCancelarCita ID="dlgCancelarCita" runat="server" />
        <uc3:DialogoRegistrarRecordatorioPaciente ID="dlgRegistrarRecordatorio" runat="server" />
        <uc4:DialogoRegistroLlegadaPaciente ID="dlgRegistrarLlegada" runat="server" />
        <div id="loadingMessage" style="display: none">
            Cargando...</div>
    </div>
    </form>
</body>
</html>
