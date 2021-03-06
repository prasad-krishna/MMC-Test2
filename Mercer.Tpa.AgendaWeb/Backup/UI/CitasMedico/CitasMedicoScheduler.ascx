<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CitasMedicoScheduler.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.Admin.CitasMedico.CitasMedicoScheduler" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1" Namespace="DevExpress.Web.ASPxScheduler"
    TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v10.1.Core" Namespace="DevExpress.XtraScheduler"
    TagPrefix="dx" %>
<%@ Register Src="../CustomSchedulerTemplates/PlantillaCitaScheduler.ascx" TagName="VerticalAppointment"
    TagPrefix="va" %>
<%@ Register Src="../CustomSchedulerTemplates/PlantillaCitaSchedulerWorkweek.ascx"
    TagName="VerticalAppointmentWorkweek" TagPrefix="wa" %>
<%@ Register Src="../Utils/ControlMensajeError.ascx" TagName="ControlMensajeError"
    TagPrefix="uc1" %>
    
<%@ Register Src="../CustomSchedulerTemplates/HorizontalAppointmentTemplateCustom.ascx" TagName="HorizontalAppointment" TagPrefix="ha" %>
<%@ Register Src="../CustomSchedulerTemplates/HorizontalSameDayAppointmentTemplateCustom.ascx" TagName="HorizontalSameDayAppointment" TagPrefix="hsda" %>
<div id="referenciaColores">
    <div class="referenciaCitaAgenda referenciaPendiente">
        Pendiente</div>
    <div class="referenciaCitaAgenda referenciaEspera">
        En espera</div>
    <div class="referenciaCitaAgenda referenciaFinalizada">
        Finalizada</div>
    <div class="referenciaCitaAgenda referenciaInasistida">
        Inasistida</div>
    <div style="clear: both">
    </div>
</div>
<div class="contenedorCitasScheduler">
    <uc1:ControlMensajeError ID="ctrError" runat="server" />
    <fieldset class="FieldSet expandible">
        <legend><a class="linkToggle" href="#">Ver otra fecha</a> </legend>
        <div class="contenido" style="display: none">
            <dxwschs:ASPxDateNavigator ID="ASPxDateNavigator1" runat="server" MasterControlID="ASPxScheduler1">
            </dxwschs:ASPxDateNavigator>
        </div>
    </fieldset>
    <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" OnPreparePopupMenu="ASPxScheduler1_PreparePopupMenu"
        ClientInstanceName="scheduler" Width="750px" Font-Size="11px" OnVisibleIntervalChanged="ASPxScheduler1_VisibleIntervalChanged"
        onappointmentviewinfocustomizing="ASPxScheduler1_AppointmentViewInfoCustomizing"
        EnableViewState="True" onbeforeexecutecallbackcommand="ASPxScheduler1_BeforeExecuteCallbackCommand">
        <optionscustomization allowappointmentcreate="None" allowappointmentdelete="None"
            allowappointmentdrag="None" allowappointmentedit="None" />
        <optionsview firstdayofweek="Monday">
        </optionsview>
        <views>
            <DayView TimeScale="00:15:00">
                <TimeRulers>
                    <dx:TimeRuler>
                    </dx:TimeRuler>
                </TimeRulers>
                <Templates>
                    <VerticalAppointmentTemplate>
                        <va:VerticalAppointment runat="server" />
                    </VerticalAppointmentTemplate>
                </Templates>
            </DayView>
            <WorkWeekView>
                <TimeRulers>
                    <dx:TimeRuler>
                    </dx:TimeRuler>
                </TimeRulers>
                 <Templates>
                    <VerticalAppointmentTemplate>
                        <wa:VerticalAppointmentWorkweek  runat="server" />
                    </VerticalAppointmentTemplate>
                </Templates>
            </WorkWeekView>
            <WeekView>
                <AppointmentDisplayOptions  AppointmentAutoHeight="false" AppointmentHeight="40"/>
                <Templates>
                    <HorizontalSameDayAppointmentTemplate>
                        <hsda:HorizontalSameDayAppointment runat="server" />
                    </HorizontalSameDayAppointmentTemplate>
                    <HorizontalAppointmentTemplate>
                        <ha:HorizontalAppointment runat="server" />
                    </HorizontalAppointmentTemplate>
                </Templates>
            </WeekView>
            <MonthView>
                <AppointmentDisplayOptions AppointmentAutoHeight="false" AppointmentHeight="40"/>
                <Templates>
                    <HorizontalAppointmentTemplate>
                        <ha:HorizontalAppointment runat="server" />
                    </HorizontalAppointmentTemplate>
                    <HorizontalSameDayAppointmentTemplate>
                        <hsda:HorizontalSameDayAppointment runat="server" />
                    </HorizontalSameDayAppointmentTemplate>
                </Templates>  
            </MonthView>
       <%--     <TimelineView>
                <AppointmentDisplayOptions  AppointmentAutoHeight="false" AppointmentHeight="35"/>
                <Templates>
                    <HorizontalAppointmentTemplate>
                        <ha:HorizontalAppointment runat="server" />
                    </HorizontalAppointmentTemplate>
                </Templates>  
            </TimelineView>--%>
        </views>
    </dxwschs:ASPxScheduler>
    <asp:ObjectDataSource ID="ObjectDataSourceScheduler" runat="server" OnSelecting="ObjectDataSourceScheduler_Selecting"
        SelectMethod="GetCitasMedico" TypeName="Mercer.Tpa.Agenda.Web.DataAcess.CitasDataRepository">
        <SelectParameters>
            <asp:Parameter Name="idMedico" Type="Int32" />
            <asp:Parameter Name="startDate" Type="DateTime" />
            <asp:Parameter Name="endDate" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
