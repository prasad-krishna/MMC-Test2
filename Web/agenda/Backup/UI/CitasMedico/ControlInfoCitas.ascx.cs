using System;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.UI.CitasMedico
{
    public partial class ControlInfoCitas : System.Web.UI.UserControl
    {
        public int IdCita { get; set; }
        public string Duracion { get { return valDuracion.Text; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            CitasDataRepository cdr = new CitasDataRepository();
            Cita cita = cdr.GetCitaById(IdCita);
            valHoraInicio.Text = DateUtils.FormatSoloTiempo(cita.StartDate);
            valHoraFin.Text = DateUtils.FormatSoloTiempo(cita.EndDate);
            valFecha.Text = DateUtils.FormatSoloFecha(cita.StartDate);
            valPaciente.Text = Server.HtmlEncode( cita.NombrePaciente);
            valSede.Text = Server.HtmlEncode(cita.Sede.Nombre);
            valTipo.Text = Server.HtmlEncode(cita.Tipo.Name);
            valDuracion.Text = cita.TotalMinutes+" Minutos";
        }
    }
}