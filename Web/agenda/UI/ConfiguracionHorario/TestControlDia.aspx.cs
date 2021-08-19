using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;

namespace Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario
{
    public partial class TestControlDia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarHorariosdeBaseDeDatos();
            }
        }

        private void CargarHorariosdeBaseDeDatos()
        {
            var intervalos = new List<IntervaloHorarioSede>();
            for (int i = 0; i < 10; i++)
            {
                intervalos.Add(new IntervaloHorarioSede()
                                   {
                                       HoraInicio = DateTime.Now.AddHours(i),
                                       HoraFin = DateTime.Now.AddHours(i + 1),
                                       Sede = new InfoSede(){Nombre = "Sede 1"}

                                   });

            }

            ControlHorarioDia1.Intervalos = intervalos;
        }

        protected void Mostrar_Click(object sender, EventArgs e)
        {
            var intervalos = ControlHorarioDia1.Intervalos;
            LabelNumIntervalos.Text = intervalos.Count.ToString();
        }
    }
}
