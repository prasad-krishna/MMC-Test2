<%@ Control Language="C#" AutoEventWireup="true" Inherits="HorizontalAppointmentTemplateCustom" Codebehind="HorizontalAppointmentTemplateCustom.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<div id="appointmentDiv" runat="server" class='<%#((HorizontalAppointmentTemplateContainer)Container).Items.AppointmentStyle.CssClass %>'>
    <table style="width:100%; height:100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td runat="server" id="statusContainer" valign="top">    
            </td>
        </tr>
        <tr>
            <td>
            <table cellpadding="0" cellspacing="0" style="width:100%; padding-bottom:2px; padding-top:2px; padding-left:1px; padding-right:1px;">
                <tr valign="middle" align="left">
                    <td>
                        <asp:Image runat="server" ID="imgStartContinueArrow" Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartContinueArrow.Visible%>'></asp:Image>
                    </td>
                    <td>
                        <dxe:ASPxLabel runat="server" CssClass="labelCitaScheduler"  EnableViewState="false" EncodeHtml="true" ID="lblStartContinueText" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartContinueText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartContinueText.Visible%>'> </dxe:ASPxLabel>
                    </td>
                    <td runat="server" id="startTimeClockContainer"> </td>
                    <td>
                       <dxe:ASPxLabel runat="server" CssClass="labelCitaScheduler"  EnableViewState="false" EncodeHtml="true" ID="lblStartTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Visible%>' ></dxe:ASPxLabel>
                    </td>
                    <td style="width: 100%;" align="center">
                        <table cellpadding="1" cellspacing="1" style="vertical-align: middle;">
                            <tr>
                                <td>
                                    <table id="imageContainer" runat="server" cellpadding="0" cellspacing="0" style="vertical-align: middle;">                                    
                                    </table>
                                </td>
                                <td align="center">                            
                                     <dxe:ASPxLabel CssClass="labelCitaScheduler" runat="server" EnableViewState="false" EncodeHtml="true" ID="lblTitle" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.Title.Text%>'></dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td runat="server" id="endTimeClockContainer"> 
                    </td>
                    <td>
                        <dxe:ASPxLabel runat="server" CssClass="labelCitaScheduler" EnableViewState="false" EncodeHtml="true" ID="lblEndTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Visible%>' ></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel runat="server" CssClass="labelCitaScheduler" EnableViewState="false" EncodeHtml="true" ID="lblEndContinueText" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndContinueText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndContinueText.Visible%>'></dxe:ASPxLabel>
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgEndContinueArrow" Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndContinueArrow.Visible%>'></asp:Image>
                    </td>
                </tr>
            </table>
            </td>
        </tr>
    </table>
</div>