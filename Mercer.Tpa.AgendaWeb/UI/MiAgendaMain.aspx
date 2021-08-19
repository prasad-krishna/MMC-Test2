﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiAgendaMain.aspx.cs" Inherits="Mercer.Tpa.Agenda.Web.UI.MiAgendaMain" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v10.1.Core, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/jquery.ui.all.css" type="text/css" rel="Stylesheet" />
    <link href="../Content/jquery-ui-1.8rc3.custom.css" type="text/css" rel="Stylesheet" />
    <link href="../Content/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="../../css/admon.css" />
    <link href="../Content/agenda.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.min.js"></script>

    <!-- Includes necesarios para grid jqGrid -->

    <script src="../Scripts/jqGrid/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Fin dependencias jqGrid -->

    <script type="text/javascript" src="../Scripts/json2.js"></script>

    <script type="text/javascript" src="../Scripts/MiAgenda.js"></script>

    <title></title>
    <style type="text/css">
        .style1
        {
            width: 199px;
        }
        .style2
        {
            width: 108px;
        }
        .style3
        {
            width: 130px;
        }
    </style>
</head>
<body class="agenda">
    <form id="form1" runat="server">
    <h1>
        Agenda</h1>
    <div id="seleccion_medico">
        <asp:Label ID="Label1" runat="server" Text="Médico" CssClass="alignedLabel"></asp:Label>
        <asp:TextBox ID="TextBoxNombreMedico" CssClass="textBox" runat="server" Text="Adriana Diazgranados"></asp:TextBox>
        <a id="linkCambiarMedico" href="#">(Cambiar)</a>
        <asp:ObjectDataSource ID="appointmentDataSource" runat="server" 
            DataObjectTypeName="Mercer.Tpa.Agenda.Web.Logic.Cita" 
            DeleteMethod="CancelarCita" InsertMethod="RegistrarCita" 
            onselecting="appointmentDataSource_Selecting" SelectMethod="GetCitasMedico" 
            TypeName="Mercer.Tpa.Agenda.Web.DataAcess.CitasDataRepository" 
            UpdateMethod="ActualizarCita">
            <SelectParameters>
                <asp:Parameter Name="idMedico" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="mainwrapper">
        <div class="agenda_menu1">
            <ul class="agenda_menu2">
            </ul>
        </div>
        <div id="appointmentForm" style="display:none" title="Detalles cita">
            <div id="appointmentDetails">
                <label>
                    Paciente</label>
                <input type="text" id="nombrePaciente" value="Nombre del paciente"/>
                <label>
                    Tipo de Cita</label>
                <asp:DropDownList ID="DropDownTipoCita" runat="server" Width="99%">
                    <asp:ListItem Value="tipo1">Tipo de cita 1</asp:ListItem>
                    <asp:ListItem>Urgencia</asp:ListItem>
                </asp:DropDownList>
                <label>
                    Sede</label>
                <asp:DropDownList ID="DropDownSede" runat="server" Height="16px" Width="99%">
                    <asp:ListItem>Sede1</asp:ListItem>
                    <asp:ListItem>Sede2</asp:ListItem>
                    <asp:ListItem>Sede3</asp:ListItem>
                </asp:DropDownList>
                <label>
                    Fecha</label>
                <input type="text" class="textBox" id="textFechaCita" value="2010/03/18"/>
                <label>
                    Duración (Minutos)</label>
                <input type="text" class="textBox" id="duracion" style="width:99%" />
                <label>
                    Notas Adicionales</label>
                <textarea class="textBox" id="notasAdicionales" rows="4" style="width:200px"></textarea>
                
            </div>
        </div>
        
        <div id="schedulerContainer">
            <div style="width:99%;height:25px;position:relative"><a href="#" id="linkMostrarNavigator" style="">Mostrar Calendario</a></div>
            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" CssClass="schedulerControl"
                Start="2010-03-16" Width="99%" ClientInstanceName="scheduler" 
                onpreparepopupmenu="ASPxScheduler1_PreparePopupMenu" 
                ActiveViewType="WorkWeek">
                <Styles SelectionColor="#669999">
                </Styles>
                <OptionsLoadingPanel Text="Cargando&amp;hellip;" />
                <Views>
                    <DayView ShortDisplayName="Dia">
                        <TimeRulers>
                            <dx:TimeRuler>
                            </dx:TimeRuler>
                        </TimeRulers>
                        <DayViewStyles ScrollAreaHeight="400px" />
                    </DayView>
                    <WorkWeekView ShortDisplayName="Semana laboral">
                        <WorkWeekViewStyles ScrollAreaHeight="400px" />
                        <TimeRulers>
                            <dx:TimeRuler>
                            </dx:TimeRuler>
                        </TimeRulers>
                        <TimeSlots>
                            <dx:TimeSlot DisplayName="60 Minutos" MenuCaption="6&amp;0 Minutes" 
                                Value="01:00:00" />
                            <dx:TimeSlot DisplayName="30 Minutes" MenuCaption="&amp;30 Minutes" 
                                Value="00:30:00" />
                            <dx:TimeSlot DisplayName="15 Minutes" MenuCaption="&amp;15 Minutes" 
                                Value="00:15:00" />
                            <dx:TimeSlot DisplayName="10 Minutes" MenuCaption="10 &amp;Minutes" 
                                Value="00:10:00" />
                            <dx:TimeSlot DisplayName="6 Minutes" MenuCaption="&amp;6 Minutes" 
                                Value="00:06:00" />
                            <dx:TimeSlot DisplayName="5 Minutes" MenuCaption="&amp;5 Minutes" />
                        </TimeSlots>
                    </WorkWeekView>
                    <WeekView ShortDisplayName="Semana">
                    </WeekView>
                    <MonthView ShortDisplayName="Mes">
                    </MonthView>
                    <TimelineView ShortDisplayName="Linea de tiempo">
                    </TimelineView>
                </Views>
                <Views>
                    <DayView>
                        <TimeRulers>
                        </TimeRulers>
                    </DayView>
                    <WorkWeekView>
                        <TimeRulers>
                        </TimeRulers>
                    </WorkWeekView>
                </Views>
            </dxwschs:ASPxScheduler>
            <div id="dialogoNavigator" title="Calendario">
                <dxwschs:ASPxDateNavigator ID="ASPxDateNavigator1" runat="server" 
                    MasterControlID="ASPxScheduler1">
                </dxwschs:ASPxDateNavigator>
            </div>
        </div>
        <!--Resetear el float (compatibilidad browsers) -->
        <div style="clear: both">
        </div>
    </div>
    <div id="dialogoSeleccionMedico" title="Selección de médico">
        <div class="busquedaRapidaMedico">
            <label>
                Busqueda rápida</label>
            <input type="text" class="textBox" id="txtBusquedaMedico" />
            <a href="#" id="linkBusquedaRapida">Buscar</a>
        </div>
        <div class="resultadosMedico">
            <table id="gridSeleccionMedicos" class="scroll" cellpadding="0" cellspacing="0">
            </table>
            <div style="overflow: scroll; height: 300px;width:450px;margin:auto;">
                <table id="prototipoListaMedicos" style="overflow: scroll">
                
                    <thead>
                        <tr>
                            <td class="style2">
                                Médico
                            </td>
                            <td class="style3">
                                Especialidad
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        
                    </tbody>
                </table>
            </div>
            <div id="pagerMedicos" class="scroll" style="text-align: center;">
            </div>
        </div>
    </div>
    
    </form>
    
</body>
</html>
