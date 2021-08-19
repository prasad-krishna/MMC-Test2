using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mercer.Medicines.Logic;

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Realiza la edición y adición del rango para los cálculos del reporte de riesgos
    /// </summary>
    public partial class AE_preguntarespuestaconsulta : PB_PaginaBase
    {
        #region Inicializacion

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);

                if (!this.Page.IsPostBack)
                {

                    if (Request.QueryString["IdPreguntaRespuesta"] != null)
                    {
                        this.LoadPreguntaRespuesta(Convert.ToInt32(Request.QueryString["IdPreguntaRespuesta"]));
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento, realiza el guardado o edición del rango
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string script = "";
                if (Request.QueryString["IdPreguntaRespuesta"] != null)
                {
                    this.UpdatePreguntaRespuesta(Convert.ToInt32(Request.QueryString["IdPreguntaRespuesta"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarPreguntaRespuesta, Convert.ToInt32(Request.QueryString["IdPreguntaRespuesta"]), "Modificación pregunta " + this.lblPregunta.Text + " respuesta " + this.lblRespuesta.Text);
                    script = "<script language='javascript'>alert('La respuesta se modificó exitosamente'); location.href='LIS_preguntarespuestaconsulta.aspx';</script>";
                }                

                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", script, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Mensaje", script);
                }

            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, se redirecciona al listado de rangos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("LIS_preguntarespuestaconsulta.aspx");
        }

        #endregion


        #region Métodos


        /// <summary>
        /// Método, Realiza la carga de los controles a partir del objeto
        /// </summary>
        /// <param name="p_IdRangoRiesgo"></param>
        public void LoadPreguntaRespuesta(int p_IdPreguntaRespuesta)
        {
            PreguntaRespuesta objPreguntaRespuesta = new PreguntaRespuesta();
            objPreguntaRespuesta.IdPreguntaRespuesta = p_IdPreguntaRespuesta;
            objPreguntaRespuesta.GetPreguntaRespuesta();

            this.lblPregunta.Text = objPreguntaRespuesta.DescripcionPregunta;
            this.lblRespuesta.Text = objPreguntaRespuesta.Descripcion;
            if(objPreguntaRespuesta.Puntuacion != -999)
                this.txtPuntuacion.Text = objPreguntaRespuesta.Puntuacion.ToString();
            this.rblActiva.SelectedValue = Convert.ToInt16(objPreguntaRespuesta.Activa).ToString();

        }

        /// <summary>
        /// Método, realiza la carga del objeto a partir de los controles
        /// </summary>
        /// <param name="objRango"></param>
        public void LoadObjectPreguntaRespuesta(PreguntaRespuesta objPreguntaRespuesta)
        {
            if (this.txtPuntuacion.Text != string.Empty)
                objPreguntaRespuesta.Puntuacion = Convert.ToInt32(this.txtPuntuacion.Text);
            else
                objPreguntaRespuesta.Puntuacion = -999;
            objPreguntaRespuesta.Activa = Convert.ToBoolean(Convert.ToInt32(this.rblActiva.SelectedValue));            

        }

        /// <summary>
        /// Método, modifica los datos del medicamento
        /// </summary>
        private void UpdatePreguntaRespuesta(int p_idPreguntaRespuesta)
        {
            PreguntaRespuesta objPregunta = new PreguntaRespuesta();
            objPregunta.IdPreguntaRespuesta = p_idPreguntaRespuesta;
            this.LoadObjectPreguntaRespuesta(objPregunta);
            objPregunta.UpdatePreguntaRespuesta();
        }

        #endregion



    }
}
