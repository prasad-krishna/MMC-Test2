using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.UI.Pacientes
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Despliega los detalles de un paciente
    /// </summary>
    public partial class ControlInfoPaciente : System.Web.UI.UserControl
    {

        private InfoPaciente _paciente;

        public InfoPaciente Paciente
        {
            get { return _paciente; }
            set
            {
                _paciente = value;
                ActualizarControles();
            }
        }

        private void ActualizarControles()
        {
            if(Paciente !=null)
            {
                valNombre.Text =  Server.HtmlEncode(Paciente.Nombre);
                valIdentificacion.Text = Server.HtmlEncode( Paciente.Identificacion);
                valTelefonos.Text = Server.HtmlEncode(Paciente.Telefono);
                valEdad.Text = DateUtils.CalculateAge(Paciente.FechaNacimiento).ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}