using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.CitasMedico;

namespace Mercer.Tpa.Agenda.Web.UI.Admin.CitasMedico
{
    public partial class CitasMedicoScheduler : System.Web.UI.UserControl
    {
        ASPxSchedulerStorage Storage { get { return ASPxScheduler1.Storage; } }
        private int _selectedMedico = -1;
        private int _altoDia = 400;
        /// <summary>
        /// Evento lanzado cuando se actualiza el periodo visible del scheduler.
        /// </summary>
        public event EventHandler CambioPeriodoSeleccionado;

        public ASPxScheduler ControlAgenda
        {
            get
            {
                return ASPxScheduler1;
            }
        }

        public DateTime FechaInicio
        {
            get { return ASPxScheduler1.ActiveView.GetVisibleIntervals().Start; }
        }  
        
        public DateTime FechaFin
        {
            get { return ASPxScheduler1.ActiveView.GetVisibleIntervals().End; }
        }

        /// <summary>
        /// Tamaño de la vista Dia y SemanaLaboral de la agenda
        /// </summary>
        public int AltoVistaDia
        {
            get { return _altoDia; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SetupMappings();
                ConfigurarRecursos();
                ConfigurarUI();
                ASPxScheduler1.DataBind();
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }
        }

        private void ConfigurarUI()
        {
            ASPxScheduler1.DayView.WorkTime = new TimeOfDayInterval(new TimeSpan(6,0,0),new TimeSpan(23,59,59) );
            ASPxScheduler1.WorkWeekView.WorkTime = new TimeOfDayInterval(new TimeSpan(6,0,0),new TimeSpan(23,59,59) );
            ASPxScheduler1.DayView.ShowWorkTimeOnly = true;
            ASPxScheduler1.WorkWeekView.ShowWorkTimeOnly = true;
            
      
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                _selectedMedico = SessionManager.IdPrestador;
                ASPxScheduler1.AppointmentDataSource = ObjectDataSourceScheduler;
                ASPxScheduler1.Views.DayView.Styles.ScrollAreaHeight = AltoVistaDia;
                ASPxScheduler1.Views.WorkWeekView.Styles.ScrollAreaHeight = AltoVistaDia;
                ASPxScheduler1.Views.MonthView.Styles.DateCellBody.Height = 100;
                ASPxScheduler1.ClientSideEvents.AppointmentsSelectionChanged = String.Format("function(s, e) {{ schedulerCambioCitaSeleccionada({0}, s, e); }}", ASPxScheduler1.ClientInstanceName);

                //ASPxScheduler1.Views.DayView.TopRowTime = new TimeSpan(5, 0, 0);
               // ASPxScheduler1.Views.WorkWeekView.ShowWorkTimeOnly = false;
                ASPxScheduler1.OptionsToolTips.AppointmentDragToolTipUrl = "CustomSchedulerTemplates/AppointmentDragTooltip.ascx";
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
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
                mappings.Status = "EstadoCita";
                mappings.Type = "Tipo";
            }
            finally
            {
                Storage.EndUpdate();
            }
        }
        void ConfigurarRecursos()
        {
            ResourceCollection resources = Storage.Resources.Items;
            Storage.BeginUpdate();
            try
            {
                resources.Add(new Resource(SessionManager.IdPrestador, "nombre prueba medico"));
            }
            finally
            {
                Storage.EndUpdate();
            }
        }

        protected void ASPxScheduler1_PreparePopupMenu(object sender, DevExpress.Web.ASPxScheduler.PreparePopupMenuEventArgs e)
        {
            try
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
                    //menuItems.Insert(0,new DevExpress.Web.ASPxMenu.MenuItem("Registrar Consulta", "RegistrarConsulta"));
                    //menuItems.Insert(1,new DevExpress.Web.ASPxMenu.MenuItem("Registrar Cita De Urgencia", "RegistrarCitaUrgencia"));
                    //  menuItems.Insert(2,new DevExpress.Web.ASPxMenu.MenuItem("Ver Citas Pendientes", "CitasPendientes"));

                }
                else if (menu.Id.Equals(SchedulerMenuItemId.AppointmentMenu))
                {
                    menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultAppointmentMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientInstanceName);
                    menu.Items.Clear();
                    //AddNestedMenu(menu, "Marcar Cita Como...");
                    menuItems.Insert(0, new DevExpress.Web.ASPxMenu.MenuItem("Registrar Consulta", "RegistrarConsulta"));
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }
        }

        #region Utilidades Agenda

        protected void AddNestedMenu(ASPxSchedulerPopupMenu menu, string caption)
        {
            var newWithTemplateItem = new DevExpress.Web.ASPxMenu.MenuItem(caption, "MarcarComo") { BeginGroup = true };
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

        protected void ObjectDataSourceScheduler_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["idMedico"] = _selectedMedico;
                e.InputParameters["startDate"] = ASPxScheduler1.ActiveView.GetVisibleIntervals().Start.Date;
                //Asignar la fecha final como el ultimo momento (un segundo antes del dia siguiente) para que se incluyan todas las del día.
                e.InputParameters["endDate"] = ASPxScheduler1.ActiveView.GetVisibleIntervals().End.Date.AddDays(1).AddSeconds(-1);
                e.InputParameters["idEmpresa"] = SessionManager.IdEmpresa;
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }
        }

        protected void ASPxScheduler1_VisibleIntervalChanged(object sender, EventArgs e)
        {
            try
            {
                ASPxScheduler1.DataBind();
                if (CambioPeriodoSeleccionado != null)
                {
                    CambioPeriodoSeleccionado(this, null);
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }
        }

        protected void ASPxScheduler1_FetchAppointments(object sender, FetchAppointmentsEventArgs e)
        {
            
        }

        protected void ASPxScheduler1_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
        {
            try
            {
                /*Establecer el color dependiendo del estado*/
                var tipo = (EnumEstadoCita)e.ViewInfo.Appointment.StatusId;
                var estilo = new Style();
                estilo.CssClass = "citaEstado" + tipo.ToString();
                e.ViewInfo.AppointmentStyle.MergeWith(estilo);
                e.ViewInfo.AppointmentStyle.BackColor = GetColorCita(tipo);
                switch (tipo)
                {
                    case EnumEstadoCita.Finalizada:
                        e.ViewInfo.AppointmentStyle.Font.Strikeout = true;

                        break;
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }
        }

        private Color GetColorCita(EnumEstadoCita estado)
        {
            switch (estado)
            {
                case EnumEstadoCita.Pendiente:
                    return ColorTranslator.FromHtml("#C1E9F4");
                    break;
                case EnumEstadoCita.Espera:
                    return ColorTranslator.FromHtml("#DE8703");
                    break;
                case EnumEstadoCita.Inasistida:
                    return ColorTranslator.FromHtml("#DEDCD9");
                    break;
                case EnumEstadoCita.Cancelada:
                    return ColorTranslator.FromHtml("#D78D96");
                    break;
                case EnumEstadoCita.Finalizada:
                    return ColorTranslator.FromHtml("#00FF99"); ;
                    break;
                default:
                    throw new ApplicationException("Estado de cita no soportado:"+estado.ToString());

            }
        }

        protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e)
        {
            /*Redirigir si la sesión expiró*/
            if (ASPxScheduler1.IsCallback && SessionManager.IdUser == -1)
            {

                /*Redirigir a formulario de consulta*/
                this.Response.RedirectLocation = "../../AE_login_admin.aspx";
                return;
            }
           
            
            if (e.CommandId == "USRAPTMENU")
                e.Command = new CustomMenuAppointmentCallbackCommand((ASPxScheduler)sender);

        }
    }
}