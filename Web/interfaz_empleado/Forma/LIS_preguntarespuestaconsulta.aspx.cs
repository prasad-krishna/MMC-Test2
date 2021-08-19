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
    /// Realiza la consulta de preguntas respuestas de consulta
    /// </summary>
    public partial class LIS_preguntarespuestaconsulta : PB_PaginaBase
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
              
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        #endregion


        #region Eventos

        /// <summary>
        /// Eventos, realiza la búsqueda de los rangos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.findPreguntasRespuesta();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Métodos


        /// <summary>
        /// Método, realiza la búsqueda
        /// </summary>
        public void findPreguntasRespuesta()
        {
            PreguntaRespuesta objPregunta = new PreguntaRespuesta();
            objPregunta.Descripcion = this.txtRespuesta.Text;
            objPregunta.DescripcionPregunta = this.txtPregunta.Text;

            this.grvPregunta.DataSource = objPregunta.ConsultPreguntaRespuestaBusqueda();
            this.grvPregunta.DataBind();
        }

        #endregion


        protected void grvPregunta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
                Response.Redirect("AE_preguntarespuestaconsulta.aspx?IdPreguntaRespuesta=" + e.CommandArgument.ToString());

        }


    }
}
