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
    /// Modifica las solicitudes a un estado determinado
    /// </summary>
    public partial class AE_liquidarautorizaciones : PB_PaginaBase
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
            txtFechaFactura.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaRadicacion.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaConfirmacion.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaCreacionFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaCreacionInicio.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10

            this.lblMensaje.Text = "";
            this.FillList(this.ddlEstados, "SolicitudEstadosCondiciones", "--Estados--", true, null, null);
            this.FillList("Proveedores", "Proveedor", Convert.ToInt32(Session["Company"]), this.ddlProveedor, "--Prestadores--");
            this.FillList("Proveedores", "Proveedor", Convert.ToInt32(Session["Company"]), this.ddlProveedorBus, "--Prestadores--");
            this.FillListActivos("PlanesSolicitud", "PlanSolicitud", Convert.ToInt32(Session["Company"]), this.ddlPlanesSolicitud, "--Plan--");

            if (this.ddlPlanesSolicitud.Items.Count > 1)
            {
                this.ddlPlanesSolicitud.Visible = true;
                this.lblPlanesSolicitud.Visible = true;
            }

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
            long consecutivoDesde = 0;
            long consecutivoHasta = 0;

            ArrayList arrEstados = new ArrayList();

            SolicitudTipoServicio objSolicitud = new SolicitudTipoServicio();

            objSolicitud.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (this.txtNoSolicitud.Text != string.Empty)
                objSolicitud.ConsecutivoNombre = this.txtNoSolicitud.Text;
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

            this.dtgSolicitudes.DataSource = objSolicitud.ConsultSolicitudTipoServicioBusqueda(dateFrom, dateUntil, dateCreateFrom, dateCreateUntil, idProveedor, mesLiquidacion, anoLiquidacion, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3, 0, 0, consecutivoDesde, consecutivoHasta, 0, 1, null);
            this.dtgSolicitudes.DataBind();
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
            if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado))
            {
                this.tdFactura.Style["display"] = "";
                lstEstados.Add(Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado));
                this.rfvFechaFactura.Enabled = true;
                this.rfvFechaRadicacion.Enabled = true;
                this.rfvFechaConfirmacion.Enabled = true;
                this.rfvNoFactura.Enabled = true;
                this.rfvValorFactura.Enabled = true;
                this.cmvProveedor.Enabled = true;
                this.dtgSolicitudes.Columns[14].Visible = false;
                this.dtgSolicitudes.Columns[15].Visible = false;
                this.dtgSolicitudes.Columns[16].Visible = true;

            }
            else
            {
                this.tdFactura.Style["display"] = "none";
                this.rfvFechaFactura.Enabled = false;
                this.rfvFechaRadicacion.Enabled = false;
                this.rfvFechaConfirmacion.Enabled = false;
                this.rfvNoFactura.Enabled = false;
                this.rfvValorFactura.Enabled = false;
                this.cmvProveedor.Enabled = false;
            }
            if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado) || Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Negado))
            {
                lstEstados.Add(Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Pendiente));

                if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
                {
                    this.dtgSolicitudes.Columns[14].Visible = false;
                    this.dtgSolicitudes.Columns[15].Visible = false;
                    this.dtgSolicitudes.Columns[16].Visible = false;
                }
                if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Negado))
                {
                    lstEstados.Add(Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado));
                }

            }
            if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
            {
                lstEstados.Add(Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Pendiente));
                lstEstados.Add(Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado));
                lstEstados.Add(Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Negado));
            }
            if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Negado) || Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
            {
                this.dtgSolicitudes.Columns[14].Visible = false;
                this.dtgSolicitudes.Columns[15].Visible = false;
                this.dtgSolicitudes.Columns[16].Visible = false;
            }

            return lstEstados;

        }


        /// <summary>
        /// Método, valida que el presupuesto del empleado y de la empresa exista para la fecha actual y tenga cupo disponible
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void validatePresupuesto(Solicitud objSolicitud)
        {
            //Validar prespuesto de la empresa
            PresupuestosEmpresa objPresupuestoEmpresa = new PresupuestosEmpresa();
            Solicitud objSolicitudActual = new Solicitud();
            objSolicitudActual.IdSolicitud = objSolicitud.IdSolicitud;
            objSolicitudActual.GetSolicitud();
            objPresupuestoEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
            objPresupuestoEmpresa.IdTipoProceso = Convert.ToInt16(PresupuestosIndividuo.EnumTipoProceso.Autorizacion);
            objPresupuestoEmpresa.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            objPresupuestoEmpresa.GetPresupuestosActual(objSolicitudActual.Fecha);
            if (Convert.IsDBNull(objPresupuestoEmpresa.IdPresupuestoEmpresa) || objPresupuestoEmpresa.IdPresupuestoEmpresa == 0)
            {
                throw new Exception("La empresa no tiene configurado un presupuesto para la fecha actual");
            }
            if ((!Convert.IsDBNull(objPresupuestoEmpresa.Presupuesto)) && objPresupuestoEmpresa.Presupuesto != 0 && (objPresupuestoEmpresa.Utilizado + (decimal)ViewState["TotalValorAprobado"]) > objPresupuestoEmpresa.Presupuesto)
            {
                if (!objPresupuestoEmpresa.IngresoConExceso)
                    throw new Exception("El presupuesto de la empresa ha sido superado, no puede ingresar mas solicitudes");
                else
                    ViewState["IngresoConExcesoEmpresa"] = true;
            }

            //Validar presupuesto del empleado
            PresupuestosIndividuo objPresupuestoIndividuo = new PresupuestosIndividuo();
            objPresupuestoIndividuo.Id_empleado = objSolicitudActual.Id_empleado;
            objPresupuestoIndividuo.Beneficiario_id = objSolicitudActual.Beneficiario_id;
            objPresupuestoIndividuo.IdTipoProceso = Convert.ToInt16(PresupuestosIndividuo.EnumTipoProceso.Autorizacion);
            objPresupuestoIndividuo.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            objPresupuestoIndividuo.GetPresupuestosIndividuoActual(objSolicitudActual.Fecha);
            if (Convert.IsDBNull(objPresupuestoIndividuo.IdPresupuestoIndividuo) || objPresupuestoIndividuo.IdPresupuestoIndividuo == 0)
            {
                if (!Convert.IsDBNull(objPresupuestoEmpresa.PresupuestoTodos) && objPresupuestoEmpresa.PresupuestoTodos != 0)
                {
                    objPresupuestoIndividuo.InsertPresupuestosIndividuoAutomatico(objPresupuestoEmpresa.IdPresupuestoEmpresa);
                    objPresupuestoIndividuo.GetPresupuestosIndividuoActual(objSolicitudActual.Fecha);
                }
                else
                    throw new Exception("El empleado no tiene configurado un presupuesto para la fecha actual");
            }
            if ((!Convert.IsDBNull(objPresupuestoIndividuo.Presupuesto)) && objPresupuestoIndividuo.Presupuesto != 0 && objPresupuestoIndividuo.Utilizado + (decimal)ViewState["TotalValorAprobado"] > objPresupuestoIndividuo.Presupuesto)
            {
                if (!objPresupuestoEmpresa.IngresoConExceso)
                    throw new Exception("El presupuesto del empleado o beneficiario ha sido superado, no puede ingresar mas solicitudes");
                else
                    ViewState["IngresoConExcesoEmpleado"] = true;
            }
            objSolicitud.IdPresupuestoEmpresa = objPresupuestoEmpresa.IdPresupuestoEmpresa;
            objSolicitud.IdPresupuestoIndividuo = objPresupuestoIndividuo.IdPresupuestoIndividuo;
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
            this.ddlEstados.SelectedIndexChanged += new System.EventHandler(this.ddlEstados_SelectedIndexChanged);
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.dtgSolicitudes.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgSolicitudes_ItemCommand);
            this.dtgSolicitudes.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgSolicitudes_PageIndexChanged);
            this.dtgSolicitudes.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgSolicitudes_ItemDataBound);
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
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
        /// Evento, carga las solicitudes que pueden cambiar al estado seleccionado, carga controles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEstados_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.dtgSolicitudes.CurrentPageIndex = 0;
                Solicitud objSolicitud = new Solicitud();
                this.txtNoSolicitud.Text = "";
                this.lblMensaje.Text = "";

                if (Convert.ToInt16(this.ddlEstados.SelectedValue) == 0)
                {
                    this.tblBuscar.Style["display"] = "none";
                    this.tdFactura.Style["display"] = "none";
                    this.dtgSolicitudes.Visible = false;


                }
                else
                {
                    this.dtgSolicitudes.Visible = true;
                    this.tblBuscar.Style["display"] = "";
                    if (Convert.ToInt32(Session["Company"]) != 85)
                        this.FindSolicitudesEstado();

                }

                this.FillList(this.ddlMotivos, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(this.ddlEstados.SelectedValue));

                if (this.ddlMotivos.Items.Count > 1)
                {
                    this.lblMotivo.Visible = true;
                    this.ddlMotivos.Visible = true;

                }
                else
                {
                    this.lblMotivo.Visible = false;
                    this.ddlMotivos.Visible = false;
                    this.ddlMotivos.SelectedIndex = 0;
                }
                if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                    this.dtgSolicitudes.Columns[18].Visible = true;
                else
                    this.dtgSolicitudes.Columns[18].Visible = false;

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
        public void dtgSolicitudes_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Procesar")
                {
                    Solicitud objSolicitud = new Solicitud();
                    SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();

                    TextBox txtValorAprobado = (TextBox)e.Item.FindControl("txtValorAprobado");
                    TextBox txtValorFacturado = (TextBox)e.Item.FindControl("txtValorFacturado");
                    CheckBox chkGlosa = (CheckBox)e.Item.FindControl("chkGlosa");

                    if (txtValorAprobado.Text.Trim() != string.Empty)
                        objTipoServicio.ValorAprobado = Convert.ToDecimal(txtValorAprobado.Text);
                    else
                        objTipoServicio.ValorAprobado = Convert.ToDecimal(0);

                    if (txtValorFacturado.Text.Trim() != string.Empty)
                        objTipoServicio.ValorFactura = Convert.ToDecimal(txtValorFacturado.Text);
                    else
                        objTipoServicio.ValorFactura = 0;

                    objTipoServicio.Glosa = chkGlosa.Checked;
                    objTipoServicio.IdSolicitud = Convert.ToInt64(e.Item.Cells[0].Text);
                    objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(e.Item.Cells[3].Text);
                    objSolicitud.IdSolicitud = Convert.ToInt64(e.Item.Cells[0].Text);
                    objTipoServicio.IdSolicitudEstado = Convert.ToInt16(this.ddlEstados.SelectedValue);
                    objTipoServicio.FechaConfirmacion = new DateTime(1900, 1, 1);

                    if (Session["SICAU"] != null)
                        objTipoServicio.Usuario_idLiquidacion = Convert.ToInt32(Session["IdUser"]);
                    else
                        objTipoServicio.IdUserLiquidacion = Convert.ToInt32(Session["IdUser"]);


                    if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado))
                    {
                        //Si se va a liquidar se carga el id de la solicitud de SICAU para cierre
                        if (e.Item.Cells[1].Text != "0" && e.Item.Cells[1].Text != "&nbsp;")
                            objSolicitud.Id_solicitud_SICAU = Convert.ToInt32(e.Item.Cells[1].Text);
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

                    }
                    //Si se va a liquidar se carga el id de la solicitud de SICAU para cierre
                    if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                    {

                        TextBox txtObservacionesAnulacion = (TextBox)e.Item.FindControl("txtObservacionesAnulacion");
                        if (txtObservacionesAnulacion.Text == string.Empty)
                            throw new Exception("Debe ingresar la observación de la anulación");

                        objTipoServicio.ObservacionAnulacion = txtObservacionesAnulacion.Text;

                    }

                    //Cargar el servicio
                    SolicitudServicio objSolicitudServicio = new SolicitudServicio();
                    objSolicitudServicio.IdSolicitudEstado = Convert.ToInt16(this.ddlEstados.SelectedValue);
                    objSolicitudServicio.IdSolicitudMotivo = Convert.ToInt16(this.ddlMotivos.SelectedValue);
                    objSolicitudServicio.IdSolicitud = Convert.ToInt64(e.Item.Cells[0].Text);
                    objSolicitudServicio.IdSolicitudTipoServicio = Convert.ToInt64(e.Item.Cells[3].Text);

                    if (txtValorAprobado.Text.Trim() != string.Empty)
                        objSolicitudServicio.ValorAprobado = Convert.ToDecimal(txtValorAprobado.Text);
                    else
                        objSolicitudServicio.ValorAprobado = Convert.ToDecimal(0);
                    if (txtValorFacturado.Text.Trim() != string.Empty)
                        objSolicitudServicio.ValorFactura = Convert.ToDecimal(txtValorFacturado.Text);
                    else
                        objSolicitudServicio.ValorFactura = 0;

                    objTipoServicio.SolicitudServicios = new ArrayList();
                    objTipoServicio.SolicitudServicios.Add(objSolicitudServicio);

                    if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
                    {
                        ViewState["TotalValorAprobado"] = objSolicitudServicio.ValorAprobado;
                        this.validatePresupuesto(objSolicitud);
                    }

                    objTipoServicio.UpdateSolicitudTipoServicioEstado(objSolicitud);
                    this.RegisterLog(Log.EnumActionsLog.ModificarEstadoSolicitud, Convert.ToInt64(e.Item.Cells[0].Text), "Cambio solicitud a estado " + this.ddlEstados.SelectedItem.Text + " de solicitud " + e.Item.Cells[0].Text);
                    this.lblMensaje.Text = "La solicitud No. " + e.Item.Cells[7].Text + " fue " + this.ddlEstados.SelectedItem.Text + " exitosamente";
                    this.FindSolicitudesEstado();


                }
                if (e.CommandName == "Ver")
                {
                    if (ViewState["Ver"] != null)
                        ViewState["Ver"] = Convert.ToInt32(ViewState["Ver"]) + 1;
                    else
                        ViewState["Ver"] = 0;
                    this.OpenWindow("AE_SolicitudAutorizacion.aspx?IdSolicitud=" + e.Item.Cells[0].Text + "&employee_id=" + e.Item.Cells[2].Text + "&popup=1&reload=1", 900, 700, Convert.ToInt32(ViewState["Ver"]));
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

                    //Modificar los valores dependiendo del estado seleccionado
                    if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Negado) || Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                    {
                        txtValorFacturado.Text = "";
                        txtValorAprobado.Text = "";

                    }
                    if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
                    {
                        txtValorFacturado.Text = "";
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
                        e.Item.Attributes.Add("onmouseover", "SelectItemGrid(this)");
                        e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'norItems')");

                    }
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        e.Item.Attributes.Add("onmouseenter", "SelectItemGrid(this)");
                        e.Item.Attributes.Add("onmouseout", "NoSelectItemGrid(this,'altItems')");

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message + ex.StackTrace);
            }

        }

        /// <summary>
        /// Evento, carga de nuevo la información cuando se modifica desde un popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReload_Click(object sender, System.EventArgs e)
        {
            this.FindSolicitudes();

        }

        /// <summary>
        /// Evento, procesar varias autorizaciones en un solo paso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcesar_Click(object sender, System.EventArgs e)
        {
            foreach (DataGridItem item in this.dtgSolicitudes.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkProcesar = (CheckBox)item.FindControl("chkProcesar");

                    if (chkProcesar.Checked)
                    {
                        Solicitud objSolicitud = new Solicitud();
                        SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();

                        TextBox txtValorAprobado = (TextBox)item.FindControl("txtValorAprobado");
                        TextBox txtValorFacturado = (TextBox)item.FindControl("txtValorFacturado");
                        CheckBox chkGlosa = (CheckBox)item.FindControl("chkGlosa");

                        if (txtValorAprobado.Text.Trim() != string.Empty)
                            objTipoServicio.ValorAprobado = Convert.ToDecimal(txtValorAprobado.Text);
                        else
                            objTipoServicio.ValorAprobado = Convert.ToDecimal(0);

                        if (txtValorFacturado.Text.Trim() != string.Empty)
                            objTipoServicio.ValorFactura = Convert.ToDecimal(txtValorFacturado.Text);
                        else
                            objTipoServicio.ValorFactura = 0;

                        objTipoServicio.Glosa = chkGlosa.Checked;
                        objTipoServicio.IdSolicitud = Convert.ToInt64(item.Cells[0].Text);
                        objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(item.Cells[3].Text);
                        objSolicitud.IdSolicitud = Convert.ToInt64(item.Cells[0].Text);
                        objTipoServicio.IdSolicitudEstado = Convert.ToInt16(this.ddlEstados.SelectedValue);
                        objTipoServicio.FechaConfirmacion = new DateTime(1900, 1, 1);

                        if (Session["SICAU"] != null)
                            objTipoServicio.Usuario_idLiquidacion = Convert.ToInt32(Session["IdUser"]);
                        else
                            objTipoServicio.IdUserLiquidacion = Convert.ToInt32(Session["IdUser"]);


                        if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado))
                        {
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

                        }
                        //Si se va a liquidar se carga el id de la solicitud de SICAU para cierre
                        if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                        {

                            TextBox txtObservacionesAnulacion = (TextBox)item.FindControl("txtObservacionesAnulacion");
                            if (txtObservacionesAnulacion.Text == string.Empty)
                                throw new Exception("Debe ingresar la observación de la anulación");

                            objTipoServicio.ObservacionAnulacion = txtObservacionesAnulacion.Text;

                        }

                        //Cargar el servicio
                        SolicitudServicio objSolicitudServicio = new SolicitudServicio();
                        objSolicitudServicio.IdSolicitudEstado = Convert.ToInt16(this.ddlEstados.SelectedValue);
                        objSolicitudServicio.IdSolicitudMotivo = Convert.ToInt16(this.ddlMotivos.SelectedValue);
                        objSolicitudServicio.IdSolicitud = Convert.ToInt64(item.Cells[0].Text);
                        objSolicitudServicio.IdSolicitudTipoServicio = Convert.ToInt64(item.Cells[3].Text);

                        if (txtValorAprobado.Text.Trim() != string.Empty)
                            objSolicitudServicio.ValorAprobado = Convert.ToDecimal(txtValorAprobado.Text);
                        else
                            objSolicitudServicio.ValorAprobado = Convert.ToDecimal(0);
                        if (txtValorFacturado.Text.Trim() != string.Empty)
                            objSolicitudServicio.ValorFactura = Convert.ToDecimal(txtValorFacturado.Text);
                        else
                            objSolicitudServicio.ValorFactura = 0;

                        objTipoServicio.SolicitudServicios = new ArrayList();
                        objTipoServicio.SolicitudServicios.Add(objSolicitudServicio);

                        if (Convert.ToInt16(this.ddlEstados.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
                        {
                            ViewState["TotalValorAprobado"] = objSolicitudServicio.ValorAprobado;
                            this.validatePresupuesto(objSolicitud);
                        }

                        objTipoServicio.UpdateSolicitudTipoServicioEstado(objSolicitud);
                        this.RegisterLog(Log.EnumActionsLog.ModificarEstadoSolicitud, Convert.ToInt64(item.Cells[0].Text), "Cambio solicitud a estado " + this.ddlEstados.SelectedItem.Text + " de solicitud " + item.Cells[0].Text);

                    }

                }
            }

            this.lblMensaje.Text = "Las solicitudes fueron modificadas exitosamente";

        }
        #endregion




    }
}
