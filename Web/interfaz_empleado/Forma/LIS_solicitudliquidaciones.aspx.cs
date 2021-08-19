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
    /// Realiza la búsqueda de solicitud tipo servicio liquidados
    /// </summary>
    public partial class LIS_solicitudliquidaciones : PB_PaginaBase
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
            //Inicio PETF 14/01/10
            //Se agrega el atributo readonly 
            txtFechaConfirmacion.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaCreacionFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaCreacionInicio.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");            
            //Fin PETF 14/01/10

            this.lblMensaje.Text = "";

            this.FillList(this.ddlProveedorBus, "ProveedoresEmpresa", "--Prestadores--", Convert.ToInt32(Session["Company"]));


            this.ddlAno.Items.Add(new ListItem("--Año--", "0"));
            for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 2; i++)
            {
                this.ddlAno.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

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
            object dateConfirmacion = null;
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
            if (this.txtNoSolicitud.Text != string.Empty)
                objSolicitud.ConsecutivoNombre = this.txtNoSolicitud.Text;
            if (this.txtFactura.Text != string.Empty)
                objSolicitud.NumeroFactura = this.txtFactura.Text;
            if (this.txtFechaInicio.Text.Trim() != string.Empty)
                dateFrom = Convert.ToDateTime(this.txtFechaInicio.Text);
            if (this.txtFechaFin.Text.Trim() != string.Empty)
                dateUntil = Convert.ToDateTime(this.txtFechaFin.Text);
            if (this.txtFechaCreacionInicio.Text.Trim() != string.Empty)
                dateCreateFrom = Convert.ToDateTime(this.txtFechaCreacionInicio.Text);
            if (this.txtFechaCreacionFin.Text.Trim() != string.Empty)
                dateCreateUntil = Convert.ToDateTime(this.txtFechaCreacionFin.Text);
            if (this.txtFechaConfirmacion.Text.Trim() != string.Empty)
                dateConfirmacion = Convert.ToDateTime(this.txtFechaConfirmacion.Text);
            idProveedor = Convert.ToInt32(this.ddlProveedorBus.SelectedValue);
            objSolicitud.IdTipoSolicitud = Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion);
            idSolicitudEstado1 = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado);
            if (this.ddlMes.SelectedValue != "0")
                mesLiquidacion = Convert.ToInt16(this.ddlMes.SelectedValue);
            if (this.ddlAno.SelectedValue != "0")
                anoLiquidacion = Convert.ToInt16(this.ddlAno.SelectedValue);

            if (this.txtIdEmpleado.Text != string.Empty)
            {
                SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
                objEmpleado.Identificacion = this.txtIdEmpleado.Text;
                objEmpleado.Empresa_id = Convert.ToInt32(Session["Company"]);
                objEmpleado.GetSIC_EMPLEADOByIdentificacion();
                if (objEmpleado.Id_empleado == 0)
                    id_empleado = -1;
                else
                    id_empleado = objEmpleado.Id_empleado;
            }
            if (this.txtIdBeneficiario.Text != string.Empty)
            {
                SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
                objBeneficiario.Identificacion = this.txtIdBeneficiario.Text;
                objBeneficiario.GetSIC_BENEFICIARIOByIdentificacion(Convert.ToInt32(Session["Company"]));
                if (objBeneficiario.Beneficiario_id == 0)
                    id_beneficiario = -1;
                else
                    id_beneficiario = objBeneficiario.Beneficiario_id;
            }

            this.dtgSolicitudes.DataSource = objSolicitud.ConsultSolicitudTipoServicioBusqueda(dateFrom, dateUntil, dateCreateFrom, dateCreateUntil, idProveedor, mesLiquidacion, anoLiquidacion, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3, id_empleado, id_beneficiario, 0, 0, 0, 1, dateConfirmacion);
            this.dtgSolicitudes.DataBind();
            this.dtgSolicitudes.CurrentPageIndex = 0;

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


        #endregion



    }
}
