using System;
using System.Collections.Generic;
using System.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using Mercer.Tpa.Agenda.Web.DataAcess;

namespace Mercer.Tpa.Agenda.Web.UI.CitasMedico
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Callback personalizado para manejar eventos de menú (Ver documentación DevExpress ASPXSchedulers
    /// </summary>
    public class CustomMenuAppointmentCallbackCommand : MenuAppointmentCallbackCommand
    {
        string menuItemId = String.Empty;
        private CitasDataRepository _citasRep = new CitasDataRepository();

        public CustomMenuAppointmentCallbackCommand(ASPxScheduler control) : base(control) { }

        public override string Id { get { return "USRAPTMENU"; } }
        public string MenuItemId { get { return menuItemId; } }

        protected override void ParseParameters(string parameters)
        {
            this.menuItemId = parameters;
            base.ParseParameters(parameters);
        }

        protected override void ExecuteCore()
        {
            Appointment apt = Control.SelectedAppointments[0];
            if (MenuItemId == "RegistrarConsulta")
            {
                if(this.Control.IsCallback)
                {
                    var cita = _citasRep.GetCitaById((int)AppointmentIdHelper.GetAppointmentId(apt));
                    /*Redirigir a formulario de consulta*/
                    this.Control.Response.RedirectLocation = "../../interfaz_empleado/forma/AE_registroconsulta.aspx?employee_id=" + cita.IdEmpleado + "&beneficiario_id=" + cita.IdBeneficiario + "&cita_id=" + cita.Id;
                }
                return;
            }

            base.ExecuteCore();

        }
    }


}
