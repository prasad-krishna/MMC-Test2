using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercer.Tpa.Agenda.Web.UI.Utils
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Control de usuario que muestra los errores en los formularios
    /// Y escribe en el Trace la excepción completa
    /// </summary>
    public partial class ControlMensajeError : System.Web.UI.UserControl
    {
        private bool _errorCargado = false;
        protected void Page_Load(object sender, EventArgs e)
        {
         
            this.Visible = _errorCargado;
        }

        /// <summary>
        /// Muestra la excepción y la escribe en el trace
        /// </summary>
        /// <param name="ex"></param>
        public void MostrarError(Exception ex)
        {
            this.Visible = true;
            LblError.Text = "Ocurrió un error";
            lblDetallesError.Text = ex.ToString();
            Trace.Write(ex.ToString());
            if(ex.InnerException!=null)
            {
                Trace.Write("Inner Exception:"+ex.InnerException.ToString());
                lblDetallesError.Text += Server.HtmlEncode(ex.InnerException.ToString());
            }
            _errorCargado = true;
        }

        /// <summary>
        /// Muestra un mensaje de error personalizado
        /// </summary>
        /// <param name="error"></param>
        public void MostrarError(string error)
        {
            this.Visible = true;
            LblError.Text = error;
            lblDetallesError.Text = "";
            Trace.Write(error);
            _errorCargado = true;
        }


        /// <summary>
        /// Muestra un mensaje de error personalizado y escribe la excepcion
        /// al trace para facilitar la solución del problema.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="customMsg"></param>
        internal void MostrarError(Exception ex, string customMsg)
        {
            this.Visible = true;
            LblError.Text = Server.HtmlEncode(customMsg);
            lblDetallesError.Text = ex.ToString();
            Trace.Write(ex.ToString());
            if (ex.InnerException != null)
            {
                Trace.Write("Inner Exception:" + ex.InnerException.ToString());
                lblDetallesError.Text += Server.HtmlEncode(ex.InnerException.ToString());
            }
            _errorCargado = true;
        }
    }
}