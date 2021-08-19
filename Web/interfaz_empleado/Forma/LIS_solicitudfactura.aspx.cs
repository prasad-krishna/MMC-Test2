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
    /// Realiza la búsqueda de solicitud por factura para poder realizar reversión
    /// </summary>
    public partial class LIS_solicitudfactura : PB_PaginaBase
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
            //Inicio MAHG 22/01/10
            //Se carga el load de la página base

            base.Page_Load(sender, e);

            //Fin MAHG 22/01/10

            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.LoadControls();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        #endregion

        #region Métodos

        /// <summary>
        /// Método, Realiza la carga inicial de controles
        /// </summary>
        public void LoadControls()
        {
            this.lblMensaje.Text = "";
            this.FillList("Proveedores", "Proveedor", Convert.ToInt32(Session["Company"]), this.ddlProveedorBus, "--Todos--");

        }

        /// <summary>
        /// Método, Realiza la búsqueda de solicitudes
        /// </summary>
        public void FindSolicitudes()
        {
            ViewState["BusquedaEspecial"] = true;
            object dateFrom = null;
            object dateUntil = null;
            object dateCreateFrom = null;
            object dateCreateUntil = null;
            int idProveedor = 0;
            int mesLiquidacion = 0;
            int anoLiquidacion = 0;
            int idSolicitudEstado1 = 0;
            int idSolicitudEstado2 = 0;
            int idSolicitudEstado3 = 0;
            long id_empleado = 0;
            long id_beneficiario = 0;
            ArrayList arrEstados = new ArrayList();
            SolicitudTipoServicio objSolicitud = new SolicitudTipoServicio();
            objSolicitud.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (this.txtFactura.Text != string.Empty)
                objSolicitud.NumeroFactura = this.txtFactura.Text;
            else
                throw new Exception("Ingrese la factura");
            idProveedor = Convert.ToInt32(this.ddlProveedorBus.SelectedValue);
            if (idProveedor == 0)
                throw new Exception("Seleccione el prestador de la factura");

            objSolicitud.IdTipoSolicitud = Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion);
            idSolicitudEstado1 = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado);

            this.dtgSolicitudes.DataSource = objSolicitud.ConsultSolicitudTipoServicioBusqueda(dateFrom, dateUntil, dateCreateFrom, dateCreateUntil, idProveedor, mesLiquidacion, anoLiquidacion, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3, id_empleado, id_beneficiario, 0, 0, 0, 1, null);
            this.dtgSolicitudes.DataBind();
            this.dtgSolicitudes.CurrentPageIndex = 0;
            this.btnReversar.Visible = true;

        }

        public void ReversarFactura()
        {
            SolicitudTipoServicio objSolicitudTipoServicio = new SolicitudTipoServicio();
            objSolicitudTipoServicio.NumeroFactura = this.txtFactura.Text;
            objSolicitudTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);
            objSolicitudTipoServicio.IdProveedor = Convert.ToInt32(this.ddlProveedorBus.SelectedValue);
            objSolicitudTipoServicio.ReversarFactura();
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
            this.dtgSolicitudes.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgSolicitudes_ItemCommand);
            this.dtgSolicitudes.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgSolicitudes_PageIndexChanged);
            this.dtgSolicitudes.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgSolicitudes_ItemDataBound);
            this.btnReversar.Click += new System.EventHandler(this.btnReversar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza la búsqueda de la solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.lblMensaje.Text = "";
                this.dtgSolicitudes.CurrentPageIndex = 0;
                this.FindSolicitudes();

            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Realiza la paginación
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.lblMensaje.Text = "";
            this.dtgSolicitudes.CurrentPageIndex = e.NewPageIndex;
            this.FindSolicitudes();
        }

        /// <summary>
        /// Evento, permite ver el detalle de la orden
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ver")
                {
                    if (ViewState["Ver"] != null)
                        ViewState["Ver"] = Convert.ToInt32(ViewState["Ver"]) + 1;
                    else
                        ViewState["Ver"] = 0;
                    this.OpenWindow("AE_SolicitudAutorizacion.aspx?IdSolicitud=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[2].Text + "&popup=1", 950, 850, Convert.ToInt32(ViewState["Ver"]));


                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, carga valores en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblPaciente = (Label)e.Item.FindControl("lblPaciente");

                    if (e.Item.Cells[4].Text == string.Empty || e.Item.Cells[4].Text == "&nbsp;")
                    {
                        SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                        objEmpleado.Id_empleado = Convert.ToInt32(e.Item.Cells[2].Text);
                        objEmpleado.GetSIC_EMPLEADO();
                        lblPaciente.Text = objEmpleado.Nombre_completo;
                    }
                    else
                    {
                        SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
                        DataTable dtBeneficiarios;
                        objBeneficiario.Opcion = 2;
                        objBeneficiario.Beneficiario_id = Convert.ToInt32(e.Item.Cells[4].Text);
                        dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                        lblPaciente.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
                    }

                    if (e.Item.ItemType == ListItemType.Item)
                    {
                        if (e.Item.CssClass != "norItemsColor")
                        {
                            e.Item.Attributes.Add("onmouseover", "SelectItemGrid(this)");
                            e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'norItems')");
                        }

                    }
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        if (e.Item.CssClass != "norItemsColor")
                        {
                            e.Item.Attributes.Add("onmouseenter", "SelectItemGrid(this)");
                            e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'altItems')");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Evento, reversa la factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReversar_Click(object sender, System.EventArgs e)
        {
            this.ReversarFactura();
            foreach (DataGridItem item in this.dtgSolicitudes.Items)
            {
                this.RegisterLog(Log.EnumActionsLog.ReversarFactura, Convert.ToInt64(item.Cells[0].Text), "Reversión factura numero:" + this.txtFactura.Text + " id solicitud:" + item.Cells[0].Text + " id solicitud tipo servicio:" + item.Cells[3].Text);
            }
            string script = "<script language='javascript'>alert('La factura se reversó exitosamente'); location.href='LIS_solicitudfactura.aspx';</script>";

            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Mensaje", script);
            }
            //Fin      
           


        }


        #endregion





    }
}
