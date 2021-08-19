using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Página AgendaMédico (Contiene controles que muestran el control Scheduler y las próximas citas)
    /// </summary>
    public partial class AgendaMedico : PaginaBaseAgenda
    {
        #region Variables privadas

        private int _selectedMedico = -1;
        private int _selectedEstado = -1;

        #endregion

        #region Propiedades

        protected override string PathLoginForm
        {
            get
            {
                return "../../AE_login_admin.aspx";
            }
        }
 

        #endregion

        
        #region Eventos
        protected override void Page_Load(object sender, EventArgs e)
        {

            try
            {
                base.Page_Load(sender,e);
                _selectedMedico = SessionManager.IdUser;
                _selectedEstado = (int)EnumEstadoCita.Pendiente;
             
                ConfigurarAlertasCliente();
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }

        }

        /// <summary>
        /// Dependiendo del valor de la llave de configuración "AlertasAgendaActivas"
        /// se habilitan o deshabilita el pooling frecuente desde el cliente (ajax) al servidor
        /// en busca de alertas
        /// </summary>
        private void ConfigurarAlertasCliente()
        {
            //Solo registrar script que desactiva alertas si no estamos en una llamada AJAX
            if (System.Web.UI.ScriptManager.GetCurrent(base.Page) == null || System.Web.UI.ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack==false)
            {
                bool alertasActivas = false;
                if (ConfigurationManager.AppSettings["AlertasAgendaActivas"] != null)
                {
                    alertasActivas = bool.Parse(ConfigurationManager.AppSettings["AlertasAgendaActivas"]);
                }
                if (!alertasActivas)
                {
                    var desactivarAlertas = "desactivarAlertas();";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "desactivarAlertasScript", desactivarAlertas,true);
                }
                
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                //CtrlCitasScheduler.CambioPeriodoSeleccionado += new EventHandler(CtrlCitasScheduler_CambioPeriodoSeleccionado);
                if (!IsPostBack)
                {
                    /*Mostrar en el grid las citas de esta semana*/
                    CtrlCitasGrid.StartDate = DateTime.Now.Date;
                    CtrlCitasGrid.EndDate = DateTime.Now.AddDays(7);
                }
            }
            catch (Exception ex)
            {

                ctrError.MostrarError(ex);
            }

        }

        void CtrlCitasScheduler_CambioPeriodoSeleccionado(object sender, EventArgs e)
        {
            
        }

        protected void btnProximasCitas_Click(object sender, EventArgs e)
        {
            CtrlCitasScheduler.Visible = false;
            CtrlCitasGrid.Visible = true;
        }

        protected void btnVerAgenda_Click(object sender, EventArgs e)
        {
            CtrlCitasScheduler.Visible = true;
            CtrlCitasGrid.Visible = false;
        }

        #endregion

    }
}
