using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.UI
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Página inicial Mi Agenda 
    /// </summary>
    public partial class AgendaInicio : PaginaBaseAgenda
    {
        
        #region Propiedades

        /// <summary>
        /// Sobrescribimos el path relativo del formulario de login para cuando la sesión expira
        /// </summary>
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
            /*
             * Si el usuario actual es un medico llevarlo a la agenda del medico
             * De lo contrario mostrar las opciones actuales que corresponden a la agenda del asistente
             */
            if (SessionManager.IdPrestador > 0)
            {
                Response.Redirect("AgendaMedico.aspx");
            }
        }

        #endregion

    }
}
