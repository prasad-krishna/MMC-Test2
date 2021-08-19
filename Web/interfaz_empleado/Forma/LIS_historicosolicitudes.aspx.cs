using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Medicines.Logic;

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Lista el históricos de solicitudes del empleado
    /// </summary>
    public partial class LIS_historicosolicitudes : PB_PaginaBase
    {
        #region Inicialización

        /// <summary>
        /// Inicializa la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10

                if (!this.Page.IsPostBack)
                {
                    this.FindSolicitudesOrden();

                }
            }
            catch (Exception ex)
            {
                string message = "";
                message = "<script language='javascript'>alert('Exception :" + ex.Message + "')</script>";


                //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", message, false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", message);
                }
                //Fin       
            }

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Métoco, realiza la búsqueda de solicitudes
        /// </summary>
        public void FindSolicitudesOrden()
        {

            DateTime fechaInicio, fechaFin;
            Solicitud objSolicitud = new Solicitud();

            if (this.txtFechaInicio.Text.Trim() != string.Empty)
                fechaInicio = Convert.ToDateTime(this.txtFechaInicio.Text);
            else
                fechaInicio = DateTime.Now.AddDays(-120);
            if (this.txtFechaFin.Text.Trim() != string.Empty)
                fechaFin = Convert.ToDateTime(this.txtFechaFin.Text);
            else
                fechaFin = DateTime.Now;
            if (Request.QueryString["empleado"] != null && Request.QueryString["empleado"] == "s")
                this.dtgSolicitud.DataSource = objSolicitud.ConsultSolicitudOrden(0, Convert.ToInt32(Request.QueryString["beneficiario_id"]), fechaInicio, fechaFin);
            else
                this.dtgSolicitud.DataSource = objSolicitud.ConsultSolicitudOrden(Convert.ToInt32(Request.QueryString["beneficiario_id"]), 0, fechaInicio, fechaFin);

            this.dtgSolicitud.DataBind();
        }

        #endregion

        #region Eventos

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.dtgSolicitud.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgSolicitud_ItemCommand);
            this.dtgSolicitud.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgSolicitud_PageIndexChanged);
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void btnCerrar_Click(object sender, System.EventArgs e)
        {
            Response.Write("<script>top.close();</script>");

        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.dtgSolicitud.CurrentPageIndex = 0;
                this.FindSolicitudesOrden();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, realiza la paginación
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        private void dtgSolicitud_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgSolicitud.CurrentPageIndex = e.NewPageIndex;
            this.FindSolicitudesOrden();

        }

        private void dtgSolicitud_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Copiar")
                {
                    Response.Write("<script>opener.location='AE_solicitudorden.aspx?IdSolicitudCopia=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[1].Text + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=1';top.close();</script>");

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        #endregion

       

    }
}
