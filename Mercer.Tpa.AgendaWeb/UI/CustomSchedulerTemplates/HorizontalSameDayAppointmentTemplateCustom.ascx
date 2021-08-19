﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="HorizontalSameDayAppointmentTemplateCustom" Codebehind="HorizontalSameDayAppointmentTemplateCustom.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<div id="appointmentDiv" runat="server" class='<%#((HorizontalAppointmentTemplateContainer)Container).Items.AppointmentStyle.CssClass %>'>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td runat="server" id="statusContainer" valign="top">    
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="1" cellspacing="0" width="100%">
                    <tr valign="middle" align="left">
                        <td runat="server" id="startTimeClockContainer"> 
                        </td>
                        <td>
                            <dxe:ASPxLabel runat="server" CssClass="labelCitaScheduler"  EnableViewState="false" EncodeHtml="true" ID="lblStartTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Visible%>'></dxe:ASPxLabel>            
                        </td>
                        <td runat="server" id="endTimeClockContainer">
                        </td>
                        <td>
                            <dxe:ASPxLabel runat="server" CssClass="labelCitaScheduler"  EnableViewState="false" EncodeHtml="true" ID="lblEndTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Visible%>'></dxe:ASPxLabel>
                        </td>
                        <td>
                            <table id="imageContainer" runat="server" cellpadding="1" cellspacing="0" style="vertical-align: middle;">                            
                            </table>
                        </td>
                        <td style="width: 100%">
                            <dxe:ASPxLabel runat="server" CssClass="labelCitaScheduler"  EnableViewState="false" EncodeHtml="true" ID="lblTitle" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.Title.Text%>'> </dxe:ASPxLabel>            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>