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
    public partial class AE_rangoreporteriesgos : PB_PaginaBase
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

                    if (Request.QueryString["IdRangoRiesgos"] != null)
                    {
                        this.LoadRangoRiesgos(Convert.ToInt32(Request.QueryString["IdRangoRiesgos"]));
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
                string script;
                if (Request.QueryString["IdRangoRiesgos"] != null)
                {
                    this.UpdateRango(Convert.ToInt32(Request.QueryString["IdRangoRiesgos"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarRango, Convert.ToInt32(Request.QueryString["IdRangoRiesgos"]), "Modificación rango " + this.txtNombre.Text);
                    script = "<script language='javascript'>alert('El rango se modificó exitosamente'); location.href='LIS_rangoreporteriesgos.aspx';</script>";
                }
                else
                {
                    int idRango = this.InsertRango();
                    this.RegisterLog(Log.EnumActionsLog.IngresarRango, idRango, "Ingreso rango " + this.txtNombre.Text);
                    script = "<script language='javascript'>alert('El rango se ingresó exitosamente'); location.href='LIS_rangoreporteriesgos.aspx';</script>";
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
        /// Evento, realiza la eliminación del rango
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            this.DeleteRango(Convert.ToInt32(Request.QueryString["IdRangoRiesgos"]));
            this.RegisterLog(Log.EnumActionsLog.EliminarRango, Convert.ToInt32(Request.QueryString["IdRangoRiesgos"]), "Eliminación rango " + this.txtNombre.Text);
            
            string script = "<script language='javascript'>alert('El rango se eliminó exitosamente'); location.href='LIS_rangoreporteriesgos.aspx';</script>";

            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Mensaje", script);
            }
        }

        /// <summary>
        /// Evento, se redirecciona al listado de rangos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("LIS_rangoreporteriesgos.aspx");
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Método, Realiza la carga de controles iniciales
        /// </summary>
        public void loadControls()
        {
            this.FillList(this.ddlReporte, "ReporteRangoRiesgos", "--Seleccione--", 0);
        }

        /// <summary>
        /// Método, Realiza la carga de los controles a partir del objeto
        /// </summary>
        /// <param name="p_IdRangoRiesgo"></param>
        public void LoadRangoRiesgos(int p_IdRangoRiesgo)
        {
            RangoRiesgos objRango = new RangoRiesgos();
            objRango.IdRangoRiesgos = p_IdRangoRiesgo;
            objRango.GetRangoRiesgos();

            this.txtNombre.Text = objRango.RangoRiegosNombre;
            this.ddlReporte.SelectedValue = objRango.ReporteRiesgo;
            this.txtLimiteInferior.Text = string.Format("{0:N2}", objRango.LimiteInferior);
            this.txtLimiteSuperior.Text = string.Format("{0:N2}", objRango.LimiteSuperior);
            this.txtPuntuacion.Text = objRango.Puntuacion.ToString();
            this.txtOrden.Text = objRango.Orden.ToString();

        }

        /// <summary>
        /// Método, realiza la carga del objeto a partir de los controles
        /// </summary>
        /// <param name="objRango"></param>
        public void LoadObjectRangoRiesgos(RangoRiesgos objRango)
        {
            objRango.RangoRiegosNombre = this.txtNombre.Text;
            objRango.ReporteRiesgo = this.ddlReporte.SelectedValue;
            objRango.LimiteInferior = Convert.ToDecimal(this.txtLimiteInferior.Text);
            objRango.LimiteSuperior = Convert.ToDecimal(this.txtLimiteSuperior.Text);
            objRango.Puntuacion = Convert.ToInt32(this.txtPuntuacion.Text);
            objRango.Orden = Convert.ToInt32(this.txtOrden.Text);

        }

        /// <summary>
        /// Método, modifica los datos del medicamento
        /// </summary>
        private void UpdateRango(int p_idRango)
        {
            RangoRiesgos objRango = new RangoRiesgos();
            objRango.IdRangoRiesgos = p_idRango;
            this.LoadObjectRangoRiesgos(objRango);
            objRango.UpdateRangoRiesgos();
        }

        /// <summary>
        /// Método, inserta un nuevo medicamento
        /// </summary>
        private int InsertRango()
        {

            RangoRiesgos objRango = new RangoRiesgos();
            this.LoadObjectRangoRiesgos(objRango);
            int idRango = objRango.InsertRangoRiesgos();
            return idRango;
        }

        /// <summary>
        /// Método, modifica los datos del medicamento
        /// </summary>
        private void DeleteRango(int p_idRango)
        {
            RangoRiesgos objRango = new RangoRiesgos();
            objRango.IdRangoRiesgos = p_idRango;
            objRango.DeleteRangoRiesgos();
        }


        #endregion

        

    }
}
