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
    /// Modifica las solicitudes a un estado determinado realizando un proceso de confirmación
    /// </summary>
    public partial class AE_liquidarautconfirmacion : PB_PaginaBase
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
                    this.LoadControls();
                    this.loadConfirmados();
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
            //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 
            txtValorTotalAprobado.Attributes.Add("ReadOnly", "ReadOnly");
            txtValorTotalDescuento.Attributes.Add("ReadOnly", "ReadOnly");
            //Se agrega el atributo readonly PETF 14/01/10
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaCreacionInicio.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaCreacionFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaFactura.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaRadicacion.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaConfirmacion.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10
            //Fin MAHG 12/01/10

            this.lblMensaje.Text = "";

            this.FillList(this.ddlProveedor, "ProveedoresEmpresa", "--Prestadores--", Convert.ToInt32(Session["Company"]));
            this.FillList(this.ddlProveedorBus, "ProveedoresEmpresa", "--Prestadores--", Convert.ToInt32(Session["Company"]));
            this.FillListUser("TipoServicios", "TipoServicio", Convert.ToInt32(Session["IdUser"]), Session["SICAU"], Convert.ToInt32(Session["Company"]), this.ddlTipoServicio, "--Tipo de Servicio--");


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
            int idProveedor = 0;
            int mesLiquidacion = 0;
            int anoLiquidacion = 0;
            int idSolicitudEstado1 = 0;
            int idSolicitudEstado2 = 0;
            int idSolicitudEstado3 = 0;
            long id_empleado = 0;
            long id_beneficiario = 0;
            long consecutivoDesde = 0;
            long consecutivoHasta = 0;
            int idTipoServicio = 0;
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
            idProveedor = Convert.ToInt32(this.ddlProveedorBus.SelectedValue);
            objSolicitud.IdTipoSolicitud = Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion);
            idTipoServicio = Convert.ToInt32(this.ddlTipoServicio.SelectedValue);
            arrEstados = this.getEstadoSolicitud();
            if (arrEstados.Count > 0)
                idSolicitudEstado1 = (short)arrEstados[0];
            if (arrEstados.Count > 1)
                idSolicitudEstado2 = (short)arrEstados[1];
            if (arrEstados.Count > 2)
                idSolicitudEstado3 = (short)arrEstados[2];
            if (this.ddlMes.SelectedValue != "0")
                mesLiquidacion = Convert.ToInt16(this.ddlMes.SelectedValue);
            if (this.ddlAno.SelectedValue != "0")
                anoLiquidacion = Convert.ToInt16(this.ddlAno.SelectedValue);
            if (this.txtNoDesde.Text != string.Empty)
                consecutivoDesde = Convert.ToInt64(this.txtNoDesde.Text);
            if (this.txtNoHasta.Text != string.Empty)
                consecutivoHasta = Convert.ToInt64(this.txtNoHasta.Text);

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


            this.dtgSolicitudes.DataSource = objSolicitud.ConsultSolicitudTipoServicioBusqueda(dateFrom, dateUntil, dateCreateFrom, dateCreateUntil, idProveedor, mesLiquidacion, anoLiquidacion, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3, id_empleado, id_beneficiario, consecutivoDesde, consecutivoHasta, idTipoServicio, 1, null);
            this.dtgSolicitudes.DataBind();
            this.dtgSolicitudes.CurrentPageIndex = 0;

            this.loadConfirmados();
        }

        /// <summary>
        /// Realiza la búsqqueda de solicitudes en el estado seleccionado
        /// </summary>
        public void FindSolicitudesEstado()
        {
            ArrayList arrEstados = new ArrayList();
            int idSolicitudEstado1 = 0;
            int idSolicitudEstado2 = 0;
            int idSolicitudEstado3 = 0;
            ViewState["BusquedaEspecial"] = false;
            Solicitud objSolicitud = new Solicitud();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            arrEstados = this.getEstadoSolicitud();
            if (arrEstados.Count > 0)
                idSolicitudEstado1 = (short)arrEstados[0];
            if (arrEstados.Count > 1)
                idSolicitudEstado2 = (short)arrEstados[1];
            if (arrEstados.Count > 2)
                idSolicitudEstado3 = (short)arrEstados[2];
            objSolicitud.IdTipoSolicitud = Convert.ToInt16(Solicitud.EnumTipoSolicitud.Autorizacion);
            objSolicitud.Empresa_id = Convert.ToInt32(Session["Company"]);
            this.dtgSolicitudes.DataSource = objTipoServicio.ConsultSolicitudTipoServicioEstado(objSolicitud, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3);
            this.dtgSolicitudes.DataBind();
        }

        /// <summary>
        /// Método, consulta el estado de las solicitudes que se van a consultar dependiendo del estado seleccionado, oculta o muestra controles dependiendo del estado
        /// </summary>
        /// <returns></returns>
        public ArrayList getEstadoSolicitud()
        {
            ArrayList lstEstados = new ArrayList();

            lstEstados.Add(Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado));
            this.rfvFechaFactura.Enabled = true;
            this.rfvFechaRadicacion.Enabled = true;
            this.rfvFechaConfirmacion.Enabled = true;
            this.rfvNoFactura.Enabled = true;
            this.rfvValorFactura.Enabled = true;
            this.cmvProveedor.Enabled = true;
            this.dtgSolicitudes.Columns[11].Visible = false;
            this.dtgSolicitudes.Columns[12].Visible = false;
            this.dtgSolicitudes.Columns[13].Visible = true;

            return lstEstados;

        }

        /// <summary>
        /// Carga la grilla de solicitudes confirmadas a partir de la sessión
        /// </summary>
        public void loadConfirmados()
        {

            DataTable dtConfirmados;
            decimal valorTotalAprobado = 0;
            decimal valorDescuento = 0;

            SolicitudLiquidacion objLiquidacion = new SolicitudLiquidacion();
            if (Session["SICAU"] != null)
                objLiquidacion.Usuario_idCreacion = Convert.ToInt32(Session["IdUser"]);
            else
                objLiquidacion.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);

            dtConfirmados = objLiquidacion.ConsultSolicitudLiquidacion().Tables[0];

            if (dtConfirmados.Rows.Count > 0)
            {
                this.dtgConfirmados.DataSource = dtConfirmados;
                this.dtgConfirmados.DataBind();
                this.tdFactura.Style["display"] = "";
                this.btnProcesar.Visible = true;
                this.btnCancelar.Visible = true;
                this.dtgConfirmados.Visible = true;

                foreach (DataRow rowItem in dtConfirmados.Rows)
                {
                    if (!Convert.IsDBNull(rowItem["ValorAprobado"]))
                    {
                        valorTotalAprobado += (decimal)rowItem["ValorAprobado"];
                        if (!Convert.IsDBNull(rowItem["Descuento"]))
                            valorDescuento += (decimal)rowItem["ValorAprobado"] - ((decimal)rowItem["ValorAprobado"] * (decimal)rowItem["Descuento"] / 100);
                        if (Convert.IsDBNull(rowItem["Descuento"]))
                            valorDescuento += (decimal)rowItem["ValorAprobado"];
                    }
                }


                this.txtValorTotalAprobado.Text = string.Format("{0:0,0}", valorTotalAprobado);
                this.txtValorTotalDescuento.Text = string.Format("{0:0,0}", valorDescuento);

                //Marcar en la grilla de solicitudes las que están como confirmadas
                /*for(int i= this.dtgSolicitudes.CurrentPageIndex * 20; i < ((this.dtgSolicitudes.CurrentPageIndex + 1) * 20) && i<this.dtgSolicitudes.Items.Count; i++)
                {	
                    foreach(DataGridItem itemConfirmado in this.dtgConfirmados.Items)
                    {
                        if(this.dtgSolicitudes.Items[i].Cells[0].Text == itemConfirmado.Cells[0].Text && this.dtgSolicitudes.Items[i].Cells[3].Text == itemConfirmado.Cells[3].Text)
                        {
                            this.dtgSolicitudes.Items[i].CssClass="norItemsColor";								
                            this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseenter");
                            this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseover");	
                            this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseout");									
                        }
                    }
                }*/

                if (ViewState["Ver"] != null)
                    ViewState["Ver"] = Convert.ToInt32(ViewState["Ver"]) + 1;
                else
                    ViewState["Ver"] = 0;

                this.ResizePage(ViewState["Ver"].ToString());
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
            ////base.OnInit(e);
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
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            this.dtgConfirmados.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgConfirmados_ItemCommand);
            this.dtgConfirmados.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgConfirmados_ItemDataBound);
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
                this.dtgSolicitudes.CurrentPageIndex = 0;
                this.lblMensaje.Text = "";
                this.FindSolicitudes();

                //Marcar en la grilla de solicitudes las que están como confirmadas
                for (int i = this.dtgSolicitudes.CurrentPageIndex * 20; i < ((this.dtgSolicitudes.CurrentPageIndex + 1) * 20) && i < this.dtgSolicitudes.Items.Count; i++)
                {
                    foreach (DataGridItem itemConfirmado in this.dtgConfirmados.Items)
                    {
                        if (this.dtgSolicitudes.Items[i].Cells[0].Text == itemConfirmado.Cells[0].Text && this.dtgSolicitudes.Items[i].Cells[3].Text == itemConfirmado.Cells[3].Text)
                        {
                            this.dtgSolicitudes.Items[i].CssClass = "norItemsColor";
                            this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseenter");
                            this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseover");
                            this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseout");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, realiza el procesamiento de la solicitud seleccionada
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

                    SolicitudLiquidacion objLiquidacion = new SolicitudLiquidacion();
                    objLiquidacion.IdSolicitud = Convert.ToInt64(e.Item.Cells[0].Text);
                    objLiquidacion.IdSolicitudTipoServicio = Convert.ToInt64(e.Item.Cells[3].Text);
                    if (Session["SICAU"] != null)
                        objLiquidacion.Usuario_idCreacion = Convert.ToInt32(Session["IdUser"]);
                    else
                        objLiquidacion.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);
                    objLiquidacion.InsertSolicitudLiquidacion();
                    this.OpenWindow("AE_SolicitudAutorizacion.aspx?IdSolicitud=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[2].Text + "&popup=1&liquidacion=" + e.Item.Cells[3].Text, 1000, 700, Convert.ToInt32(ViewState["Ver"]));


                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, realiza la paginación de la grilla
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.lblMensaje.Text = "";
            this.dtgSolicitudes.CurrentPageIndex = e.NewPageIndex;
            if (ViewState["BusquedaEspecial"] != null && (bool)ViewState["BusquedaEspecial"] == true)
                this.FindSolicitudes();
            else
                this.FindSolicitudesEstado();

            //Marcar en la grilla de solicitudes las que están como confirmadas
            for (int i = this.dtgSolicitudes.CurrentPageIndex * 20; i < ((this.dtgSolicitudes.CurrentPageIndex + 1) * 20) && i < this.dtgSolicitudes.Items.Count; i++)
            {
                foreach (DataGridItem itemConfirmado in this.dtgConfirmados.Items)
                {
                    if (this.dtgSolicitudes.Items[i].Cells[0].Text == itemConfirmado.Cells[0].Text && this.dtgSolicitudes.Items[i].Cells[3].Text == itemConfirmado.Cells[3].Text)
                    {
                        this.dtgSolicitudes.Items[i].CssClass = "norItemsColor";
                        this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseenter");
                        this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseover");
                        this.dtgSolicitudes.Items[i].Attributes.Remove("onmouseout");
                    }
                }
            }

        }

        /// <summary>
        /// Evento, carga valores, oculta o despliega controles de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgSolicitudes_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    TextBox txtValorAprobado = (TextBox)e.Item.FindControl("txtValorAprobado");
                    TextBox txtValorFacturado = (TextBox)e.Item.FindControl("txtValorFacturado");

                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                    if (!Convert.IsDBNull(rowItem["ValorAprobado"]) && (decimal)rowItem["ValorAprobado"] != 0)
                    {
                        txtValorAprobado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorAprobado"]));

                        if (!Convert.IsDBNull(rowItem["ValorFactura"]) && (decimal)rowItem["ValorFactura"] != 0)
                            txtValorFacturado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorFactura"]));
                        else
                            txtValorFacturado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorAprobado"]));

                    }
                    else
                    {
                        if (!Convert.IsDBNull(rowItem["ValorConvenioSolicitado"]))
                            txtValorAprobado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorConvenioSolicitado"]));

                        if (!Convert.IsDBNull(rowItem["ValorFactura"]) && (decimal)rowItem["ValorFactura"] != 0)
                            txtValorFacturado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorFactura"]));
                        else
                            if (!Convert.IsDBNull(rowItem["ValorConvenioSolicitado"]))
                                txtValorFacturado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorConvenioSolicitado"]));
                    }


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
        /// Evento, realiza el llamado para liquidar la orden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProcesar_Click(object sender, System.EventArgs e)
        {
            foreach (DataGridItem item in this.dtgConfirmados.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkProcesar = (CheckBox)item.FindControl("chkProcesar");

                    if (chkProcesar.Checked)
                    {
                        Solicitud objSolicitud = new Solicitud();
                        SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();

                        if (item.Cells[13].Text != string.Empty)
                            objTipoServicio.ValorAprobado = Convert.ToDecimal(item.Cells[13].Text);
                        else
                            objTipoServicio.ValorAprobado = Convert.ToDecimal(0);

                        if (item.Cells[13].Text != string.Empty)
                            objTipoServicio.ValorFactura = Convert.ToDecimal(item.Cells[13].Text);
                        else
                            objTipoServicio.ValorFactura = 0;

                        objTipoServicio.IdSolicitud = Convert.ToInt64(item.Cells[0].Text);
                        objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(item.Cells[3].Text);
                        objSolicitud.IdSolicitud = Convert.ToInt64(item.Cells[0].Text);
                        objTipoServicio.IdSolicitudEstado = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado);


                        //Si se va a liquidar se carga el id de la solicitud de SICAU para cierre
                        if (item.Cells[1].Text != "0" && item.Cells[1].Text != "&nbsp;")
                            objSolicitud.Id_solicitud_SICAU = Convert.ToInt32(item.Cells[1].Text);
                        //Cargar usuario de la liquidación						
                        if (Session["SICAU"] != null)
                            objTipoServicio.Usuario_idLiquidacion = Convert.ToInt32(Session["IdUser"]);
                        else
                            objTipoServicio.IdUserLiquidacion = Convert.ToInt32(Session["IdUser"]);

                        objTipoServicio.IdProveedor = Convert.ToInt32(this.ddlProveedor.SelectedValue);
                        objTipoServicio.NumeroFactura = this.txtNumFactura.Text;
                        objTipoServicio.FechaFactura = Convert.ToDateTime(this.txtFechaFactura.Text);
                        objTipoServicio.FechaRadicacionFactura = Convert.ToDateTime(this.txtFechaRadicacion.Text);
                        objTipoServicio.ValorFactura = Convert.ToDecimal(this.txtValorFactura.Text);
                        objTipoServicio.NumeroCuentaCobro = this.txtCuentaCobro.Text;
                        objTipoServicio.FechaConfirmacion = Convert.ToDateTime(this.txtFechaConfirmacion.Text);


                        //Cargar el servicio
                        SolicitudServicio objSolicitudServicio = new SolicitudServicio();
                        objSolicitudServicio.IdSolicitudEstado = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado);
                        objSolicitudServicio.IdSolicitudMotivo = Convert.ToInt16(Solicitud.EnumMotivosEstadoSolicitud.Liquidado);
                        objSolicitudServicio.IdSolicitud = Convert.ToInt64(item.Cells[0].Text);
                        objSolicitudServicio.IdSolicitudTipoServicio = Convert.ToInt64(item.Cells[3].Text);

                        if (item.Cells[13].Text != string.Empty)
                            objSolicitudServicio.ValorAprobado = Convert.ToDecimal(item.Cells[13].Text);
                        else
                            objSolicitudServicio.ValorAprobado = Convert.ToDecimal(0);
                        if (item.Cells[13].Text != string.Empty)
                            objSolicitudServicio.ValorFactura = Convert.ToDecimal(item.Cells[13].Text);
                        else
                            objSolicitudServicio.ValorFactura = 0;

                        objTipoServicio.SolicitudServicios = new ArrayList();
                        objTipoServicio.SolicitudServicios.Add(objSolicitudServicio);

                        objTipoServicio.UpdateSolicitudTipoServicioEstado(objSolicitud);
                        this.RegisterLog(Log.EnumActionsLog.ModificarEstadoSolicitud, Convert.ToInt64(item.Cells[0].Text), "Cambio solicitud a estado liquidado de solicitud " + item.Cells[0].Text);
                    }
                }
            }

            this.dtgConfirmados.DataSource = new DataTable();
            this.dtgConfirmados.DataBind();
            this.dtgConfirmados.Visible = false;
            this.txtNumFactura.Text = "";
            this.txtCuentaCobro.Text = "";
            this.txtFechaFactura.Text = "";
            this.txtFechaRadicacion.Text = "";
            this.txtFechaConfirmacion.Text = "";
            this.txtValorFactura.Text = "";
            this.ddlProveedor.SelectedValue = "0";
            this.txtValorTotalAprobado.Text = "";
            this.txtValorTotalDescuento.Text = "";
            this.tdFactura.Style["display"] = "none";
            this.btnProcesar.Visible = false;
            this.btnCancelar.Visible = false;

            //Eliminar los registros temporales de la tabla para liquidación
            SolicitudLiquidacion objLiquidacion = new SolicitudLiquidacion();
            if (Session["SICAU"] != null)
                objLiquidacion.Usuario_idCreacion = Convert.ToInt32(Session["IdUser"]);
            else
                objLiquidacion.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);
            objLiquidacion.DeleteSolicitudLiquidacion();
            Session["Confirmados"] = null;

            string script = "<script language='javascript'>alert('La factura fue liquidada exitosamente'); window.parent.scrollTo(0,0);</script>";



            //Inicio 13/01/10 MAHG Se verifica si la solicitud es Asincrona
            if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "error", script, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", script);
            }
            //Fin 


        }

        /// <summary>
        /// Evento, abrir la ventana para registro de nueva solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.OpenWindow("LIS_empleado.aspx?liquidacionConfirmacion=1", 1020, 910, 900);
        }

        /// <summary>
        /// Evento, abrir la ventana para registro de nueva solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdicionar_Click(object sender, System.EventArgs e)
        {
            this.OpenWindow("LIS_empleado.aspx?liquidacionConfirmacion=1", 1020, 910, 901);
        }

        /// <summary>
        /// Evento para recarga de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReload_Click(object sender, System.EventArgs e)
        {
            this.loadConfirmados();
        }

        /// <summary>
        /// Evento, realiza el llamado pora la modificación de la orden
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtgConfirmados_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ver")
                {
                    if (ViewState["Ver"] != null)
                        ViewState["Ver"] = Convert.ToInt32(ViewState["Ver"]) + 1;
                    else
                        ViewState["Ver"] = 0;
                    this.OpenWindow("AE_SolicitudAutorizacion.aspx?IdSolicitud=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[2].Text + "&popup=1&liquidacion=" + e.Item.Cells[3].Text, 1000, 700, Convert.ToInt32(ViewState["Ver"]));
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento carga controles de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgConfirmados_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblPaciente = (Label)e.Item.FindControl("lblPacienteConf");
                    Label lblDescuento = (Label)e.Item.FindControl("lblDescuento");

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


                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message + ex.StackTrace);
            }

        }

        protected void btnCancelar_Click(object sender, System.EventArgs e)
        {
            SolicitudLiquidacion objLiquidacion = new SolicitudLiquidacion();
            if (Session["SICAU"] != null)
                objLiquidacion.Usuario_idCreacion = Convert.ToInt32(Session["IdUser"]);
            else
                objLiquidacion.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);
            objLiquidacion.DeleteSolicitudLiquidacion();
            Session["Confirmados"] = null;
        }

        #endregion







    }
}
