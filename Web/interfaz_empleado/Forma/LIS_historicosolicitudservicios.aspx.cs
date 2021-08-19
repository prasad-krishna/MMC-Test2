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


namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Lista el históricos de servicios del empleado
    /// </summary>
    public partial class LIS_historicosolicitudservicios : PB_PaginaBase
    {

        #region Atributos

       
        #endregion

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
                    this.LoadControls();
                    this.FindSolicitudServicio();
                    
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
        /// Método, Carga los controles de listados
        /// </summary>
        public void LoadControls()
        {
            //Inicio PETF 14/01/10
            //Se agrega el atributo readonly 
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10

            this.FillListActivos("TipoServicios", "TipoServicio", Convert.ToInt32(Session["Company"]), this.ddlTipoServicio, "Todos");
            if (Request.QueryString["idTipoServicio"] != null && Request.QueryString["idTipoServicio"] != "")
            {
                this.ddlTipoServicio.SelectedValue = Request.QueryString["idTipoServicio"];
            }

        }

        /// <summary>
        /// Métoco, realiza la búsqueda de servicios
        /// </summary>
        public void FindSolicitudServicio()
        {

            DateTime fechaInicio, fechaFin;
            SolicitudServicio objSolicitudServicio = new SolicitudServicio();

            if (this.txtFechaInicio.Text.Trim() != string.Empty)
                fechaInicio = Convert.ToDateTime(this.txtFechaInicio.Text);
            else
                fechaInicio = DateTime.Now.AddDays(-360);
            if (this.txtFechaFin.Text.Trim() != string.Empty)
                fechaFin = Convert.ToDateTime(this.txtFechaFin.Text);
            else
                fechaFin = DateTime.Now;
            if (Request.QueryString["empleado"] != null && Request.QueryString["empleado"] == "s")
                this.dtgSolicitudServicios.DataSource = objSolicitudServicio.ConsultSolicitudServicioBeneficiario(0, Convert.ToInt32(Request.QueryString["beneficiario_id"]), Convert.ToInt32(this.ddlTipoServicio.SelectedValue), fechaInicio, fechaFin);
            else
                this.dtgSolicitudServicios.DataSource = objSolicitudServicio.ConsultSolicitudServicioBeneficiario(Convert.ToInt32(Request.QueryString["beneficiario_id"]), 0, Convert.ToInt32(this.ddlTipoServicio.SelectedValue), fechaInicio, fechaFin);
            this.dtgSolicitudServicios.DataBind();
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
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.dtgSolicitudServicios.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgSolicitudServicios_PageIndexChanged);
            this.dtgSolicitudServicios.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgSolicitudServicios_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.dtgSolicitudServicios.CurrentPageIndex = 0;
                this.FindSolicitudServicio();
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        private void dtgSolicitudServicios_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dtgSolicitudServicios.CurrentPageIndex = e.NewPageIndex;
            this.FindSolicitudServicio();

        }

        protected void btnCerrar_Click(object sender, System.EventArgs e)
        {
            Response.Write("<script>top.close();</script>");

        }

        #endregion

        private void dtgSolicitudServicios_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblDiagnosticos = (Label)e.Item.FindControl("lblDiagnosticos");

                SolicitudTipoServicioDiagnosticos objDiagnostico = new SolicitudTipoServicioDiagnosticos();
                objDiagnostico.IdSolicitud = Convert.ToInt64(e.Item.Cells[0].Text);
                objDiagnostico.IdSolicitudTipoServicio = Convert.ToInt64(e.Item.Cells[1].Text);

                DataTable dtDiagnosticos = objDiagnostico.ConsultSolicitudTipoServicioDiagnosticos().Tables[0];

                foreach (DataRow dr in dtDiagnosticos.Rows)
                {
                    lblDiagnosticos.Text += dr["NombreDiagnostico"].ToString() + "</br>";
                }
            }

        }

    }
}
