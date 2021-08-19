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
    /// Realiza la consulta de rangos
    /// </summary>
    public partial class LIS_rangoreporteriesgos : PB_PaginaBase
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
                    this.loadControls();
                    
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
        /// Eventos, realiza la búsqueda de los rangos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.findRangos();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, carga los controles iniciales
        /// </summary>
        public void loadControls()
        {
            this.FillList(this.ddlReporte, "ReporteRangoRiesgos", "--Seleccione--", 0);
        }

        /// <summary>
        /// Método, realiza la búsqueda
        /// </summary>
        public void findRangos()
        {
            RangoRiesgos objRango = new RangoRiesgos();
            objRango.ReporteRiesgo = this.ddlReporte.SelectedValue;

            this.grvRango.DataSource = objRango.ConsultRangoRiesgos();
            this.grvRango.DataBind();
        }

        #endregion

        protected void lnkAdicionar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AE_rangoreporteriesgos.aspx");

        }

        /// <summary>
        /// Evento, redirecciona 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AE_rangoreporteriesgos.aspx");
        }

        /// <summary>
        /// Evento, realiza el llamado a la edición del rango
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvRango_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Edit")
                Response.Redirect("AE_rangoreporteriesgos.aspx?IdRangoRiesgos=" + e.CommandArgument.ToString());

        }

        
    }
}
