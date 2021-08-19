﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mercer.Tpa.Agenda.Web.UI.CustomSchedulerTemplates.PlantillaCitaSchedulerWorkweek" Codebehind="PlantillaCitaSchedulerWorkweek.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<div id="appointmentDiv" runat="server" class='<%#((VerticalAppointmentTemplateContainer)Container).Items.AppointmentStyle.CssClass %>' >
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top" >
            <td runat="server" id="statusContainer">    
            </td>
            <td style="width:100%">
                <table cellpadding="1" cellspacing="0" width="100%">
                    <tr valign="top">
                        <td>
                            <table id="imageContainer" runat="server" cellpadding="1" cellspacing="0" style="text-align: center">
                                <tr><td></td></tr>
                            </table>
                        </td>
                        <td style="width:100%">                    
                            <table width="100%" cellpadding="1" cellspacing="0" >                        
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel CssClass="schedulerPaciente" runat="server" EnableViewState="false" EncodeHtml="true" ID="lblTitle" Text='<%#((VerticalAppointmentTemplateContainer)Container).AppointmentViewInfo.Appointment.Subject%>'></dxe:ASPxLabel>
                                        <span>(</span>
                                        <dxe:ASPxLabel CssClass="schedulerSede" runat="server" EnableViewState="false" EncodeHtml="true" ID="lblSede" Text='<%#((VerticalAppointmentTemplateContainer)Container).AppointmentViewInfo.Appointment.Location%>'></dxe:ASPxLabel>
                                        <span>)</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>