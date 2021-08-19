using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;

namespace Mercer.Tpa.Agenda.Web.UI
{
    public partial class MiAgendaMain : System.Web.UI.Page
    {
        ASPxSchedulerStorage Storage { get { return ASPxScheduler1.Storage; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupMappings();
            //AptFiller.FillResources(this.ASPxScheduler1.Storage, 2);
            ConfigurarRecursos();
            ASPxScheduler1.AppointmentDataSource = appointmentDataSource;
            ASPxScheduler1.Views.DayView.ShowWorkTimeOnly = true;
            ASPxScheduler1.Views.WorkWeekView.ShowWorkTimeOnly = true;
            ASPxScheduler1.OptionsToolTips.AppointmentDragToolTipUrl = "CustomSchedulerTemplates/AppointmentDragTooltip.ascx";
            ASPxScheduler1.DataBind();
            ConfigurarCitaUrgencia();
            ConfigurarAccionSeleccionarCita();

        }

        /// <summary>
        /// Configura la funcionalidad que abre una cita pasada por parametro
        /// </summary>
        private void ConfigurarAccionSeleccionarCita()
        {
            //TODO: Usar enumeraciones
            if(Request.Params["action"]!=null && Request.Params["action"]=="selectAppointment")
            {
                //Se requiere el parametro idCita
                if(Request.Params["idCita"]==null)
                {
                    throw new ApplicationException("Se esperaba parámetro idCita");
                }
                var idCita = Request.Params["idCita"];
                //Registrar un script blog que llame la funcion en js encargada
                var strScript = "$(function(){cargarCitaYMostrarDetalles('" + idCita + "')});";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(),"scriptOpenAppointment",strScript,true);
            }
        }

        private void ConfigurarCitaUrgencia()
        {
            if(Request.Params["action"]!=null && Request.Params["action"]=="createUrgencyAppointment")
            {
                var strscript = "$(function(){mostrarDialogoUrgencia();});";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(),"scriptCitaUrgencia",strscript,true);
            }
        }

        void ConfigurarRecursos()
        {
            ResourceCollection resources = Storage.Resources.Items;
            Storage.BeginUpdate();
            try
            {

                    resources.Add(new Resource(GetIdMedico(),GetNombreMedico()));
            }
            finally
            {
                Storage.EndUpdate();
            }
        }

        private string GetNombreMedico()
        {
            return "nombre prestador del servicio";
        }

        void SetupMappings()
        {
            ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;
            Storage.BeginUpdate();
            try
            {
                mappings.AppointmentId = "Id";
                mappings.Start = "StartDate";
                mappings.End = "EndDate";
                mappings.Subject = "NombrePaciente";
                mappings.AllDay = "AllDay";
                mappings.Description = "NotasAdicionales";
                mappings.Label = "Label";
                mappings.Location = "NombreSede";
                mappings.RecurrenceInfo = "RecurrenceInfo";
                mappings.ReminderInfo = "ReminderInfo";
                mappings.ResourceId = "IdPrestador";
                mappings.Status = "Status";
                mappings.Type = "Type";
            }
            finally
            {
                Storage.EndUpdate();
            }
        }
        protected void ASPxScheduler1_PreparePopupMenu(object sender, DevExpress.Web.ASPxScheduler.PreparePopupMenuEventArgs e)
        {
            ASPxSchedulerPopupMenu menu = e.Menu;
            DevExpress.Web.ASPxMenu.MenuItemCollection menuItems = menu.Items;
            if (menu.Id.Equals(SchedulerMenuItemId.DefaultMenu))
            {
                menu.Items.Clear();
                //DevExpress.Web.ASPxMenu.MenuItem defaultItemOpentAppointment = menuItems.FindByText("New Appointment");
                ClearUnusedDefaultMenuItems(menu);
                //menuItems.Add(defaultItemOpentAppointment);
                menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultAppointmentMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName);
                menuItems.Insert(0,new DevExpress.Web.ASPxMenu.MenuItem("Registrar Cita", "RegistrarCita"));
                //menuItems.Insert(1,new DevExpress.Web.ASPxMenu.MenuItem("Registrar Cita De Urgencia", "RegistrarCitaUrgencia"));
              //  menuItems.Insert(2,new DevExpress.Web.ASPxMenu.MenuItem("Ver Citas Pendientes", "CitasPendientes"));
                
            }
            else if (menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu))
            {
                menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultAppointmentMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName);
                menu.Items.Clear();
                AddNestedMenu(menu, "Marcar Cita Como...");
                menuItems.Insert(1,new DevExpress.Web.ASPxMenu.MenuItem("Abrir", "Abrir"));
                menuItems.Insert(2,new DevExpress.Web.ASPxMenu.MenuItem("Registrar Nueva Cita", "RegistrarCita"));
                menuItems.Insert(3,new DevExpress.Web.ASPxMenu.MenuItem("Registrar Nueva Cita De Urgencia", "RegistrarCitaUrgencia"));
                //menuItems.Insert(4,new DevExpress.Web.ASPxMenu.MenuItem("Ver Citas Pendientes", "CitasPendientes"));
                menuItems.Insert(4,new DevExpress.Web.ASPxMenu.MenuItem("Cancelar Cita", "Cancelar"));
            }
        }
        #region UTILS

        protected void AddNestedMenu(ASPxSchedulerPopupMenu menu, string caption)
        {
            var newWithTemplateItem = new DevExpress.Web.ASPxMenu.MenuItem(caption, "MarcarComo")
                                          {BeginGroup = true};
            menu.Items.Insert(0, newWithTemplateItem);
            AddTemplatesSubMenuItems(newWithTemplateItem);
        }
        private static void AddTemplatesSubMenuItems(DevExpress.Web.ASPxMenu.MenuItem parentMenuItem)
        {
            parentMenuItem.Items.Add(new DevExpress.Web.ASPxMenu.MenuItem("En Espera", "EnEspera"));
            //parentMenuItem.Items.Add(new DevExpress.Web.ASPxMenu.MenuItem("Cancelada", "Cancelada"));
            parentMenuItem.Items.Add(new DevExpress.Web.ASPxMenu.MenuItem("Completada", "Completada"));

        }
        protected void ClearUnusedDefaultMenuItems(ASPxSchedulerPopupMenu menu)
        {
            RemoveMenuItem(menu, "NewAppointment");
            RemoveMenuItem(menu, "NewAllDayEvent");
            RemoveMenuItem(menu, "NewRecurringAppointment");
            RemoveMenuItem(menu, "NewRecurringEvent");
            RemoveMenuItem(menu, "GotoToday");
            RemoveMenuItem(menu, "GotoDate");
            RemoveMenuItem(menu, "ChangeViewTo");
        }
        protected void RemoveMenuItem(ASPxSchedulerPopupMenu menu, string menuItemName)
        {
            DevExpress.Web.ASPxMenu.MenuItem item = menu.Items.FindByName(menuItemName);
            if (item != null)
                menu.Items.Remove(item);
        } 
        #endregion

        protected void appointmentDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["idMedico"] = GetIdMedico();
        }

        private int GetIdMedico()
        {
            return 1;
        }
    }
}
