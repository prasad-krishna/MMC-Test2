using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.Admin
{
    public partial class AdminAgendaInicio : PaginaBaseAgenda
    {
        private AgendaDataRepository _agendaRep = new AgendaDataRepository();

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
                if (!IsPostBack)
                {
                    CargarParametrosAgenda();
                }
            }
            catch (Exception ex)
            {
                
                ctrError.MostrarError(ex);
            }


        }

        /// <summary>
        /// Recarga los valores de los controles a partir de la base de datos.
        /// </summary>
        private void CargarParametrosAgenda()
        {
            var parametros = _agendaRep.GetConfiguracionAgenda(SessionManager.IdEmpresa);
            if(parametros!=null)
            {
                txtHorasLimite.Text = parametros.NumHorasLimiteModificacionCitas.ToString();
            }
        }


        protected void btnGuardarParametros_Click(object sender, EventArgs e)
        {
            try
            {
                bool resultado = GuardarParametros();
                CargarParametrosAgenda();
                if(resultado)
                    ctrMensaje.MostrarMensaje("Parámetros actualizados", EnumUserMessage.Notificacion);
                else
                {
                    ctrMensaje.Visible = false;
                }
            }
            catch (Exception exception)
            {
                ctrError.MostrarError(exception);
            }
        }

        /// <summary>
        /// Guardar parámetros de la agenda
        /// </summary>
        private bool GuardarParametros()
        {
            int numHoras;
            var parametros = new ParametrosConfiguracionAgenda();
            if(string.IsNullOrEmpty(txtHorasLimite.Text))
            {
                ctrError.MostrarError("Número de horas limite requerido. Escriba 0 si no quiere establecer un límite");
                ctrMensaje.Visible = false;
                return false;
            }
            if(!int.TryParse(txtHorasLimite.Text,out numHoras))
            {
                ctrError.MostrarError("Formato incorrecto de horas límite");
                ctrMensaje.Visible = false;
                return false;
            }
            if(numHoras<0)
            {
                ctrError.MostrarError("El número de horas debe ser mayor o igual a cero");
                ctrMensaje.Visible = false;
                return false;
            }
            parametros.IdEmpresa = SessionManager.IdEmpresa;
            parametros.NumHorasLimiteModificacionCitas = numHoras;
            _agendaRep.GuardarConfiguracionAgenda(parametros);
            return true;
        }
    }
}
