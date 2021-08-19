using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Mercer.Medicines.Logic;

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Descripción breve de LIS_servicio.
    /// </summary>
    public partial class LIS_servicio : PB_PaginaBase
    {

        #region Atributos

        

        #endregion

        #region Initializing

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            //Inicio MAHG 22/01/10
            //Se carga el load de la página base

            base.Page_Load(sender, e);

            //Fin MAHG 22/01/10


            this.FillList("TipoServicios", "TipoServicio", this.ddlTipoServicio, "---Cualquiera----");

            if (this.IsPostBack)
            {
                // Si viene de una petición anterior se marca el valor correspondiente
                this.SelectList(this.ddlTipoServicio, "ddlTipoServicio");

            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, realiza la consulta de Servicios
        /// </summary>
        private void FindService()
        {
            Servicios objService = new Servicios();
            objService.NombreServicio = this.txtNombre.Text.Trim();

            objService.IdTipoServicio = Convert.ToInt16(this.ddlTipoServicio.SelectedValue);
            objService.empresa_id = Convert.ToInt32(Session["Company"]);
            objService.CodigoServicio = this.txtCodigoServicio.Text.Trim();

            DataTable dtServicios = objService.ConsultServicios().Tables[0];
            this.dtgServicios.DataSource = dtServicios;
            this.dtgServicios.DataBind();
            this.Fcount.InnerHtml = ("<br/><br/>" + dtServicios.Rows.Count + " servicios encontrados.");
            if (this.dtgServicios.Items.Count > 0)
            {
                this.dtgServicios.Visible = true;
            }
            else
            {
                this.dtgServicios.Visible = false;
            }


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
            //base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.imbAdicionar.Click += new System.Web.UI.ImageClickEventHandler(this.imbAdicionar_Click);
            this.lnkAdicionar.Click += new System.EventHandler(this.lnkAdicionar_Click);
            this.dtgServicios.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgServicios_ItemCommand);
            this.dtgServicios.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgServicios_PageIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza la búsqueda de servicios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            this.dtgServicios.CurrentPageIndex = 0;
            this.FindService();
        }

        /// <summary>
        /// Evento, realiza la paginación de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgServicios_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgServicios.CurrentPageIndex = e.NewPageIndex;
            this.FindService();
        }
        /// <summary>
        /// Evento, redirecciona a la página para adición de servicios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdicionar_Click(object sender, System.EventArgs e)
        {
            this.OpenWindow("AE_servicio.aspx", 800, 570);

        }

        /// <summary>
        /// Evento, redirecciona a la página para adición de servicios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            this.OpenWindow("AE_servicio.aspx", 800, 570);
        }

        /// <summary>
        /// Evento, redirecciona a la página para edición del servicio
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgServicios_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.OpenWindow("AE_servicio.aspx?IdServicio=" + e.CommandArgument.ToString(), 800, 570);

            }
        }

        #endregion

    }
}
