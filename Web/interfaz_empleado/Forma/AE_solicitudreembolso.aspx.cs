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
    /// Inserta o modifica una solicitud de reembolso, con sus encabezados y detalles
    /// </summary>
    public partial class AE_solicitudreembolso : PB_PaginaBase
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

                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    }
                    else
                    {
                        if (Request.QueryString["IdSolicitudCopia"] != null)
                        {
                            this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]));
                        }
                        else
                        {
                            addFacturaDataList(true, 1);
                            this.ResizePage(this.dtlTipoServicio.Items.Count.ToString());
                        }
                    }
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
        /// Método, Carga los controles de listados
        /// </summary>
        public void LoadControls()
        {
            DataTable dtParentescos;
            DataTable dtBeneficiarios;
            EmpresaParentescos objEmpresaParentescos = new EmpresaParentescos();
            EmpresaDatos objEmpresaDatos = new EmpresaDatos();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();

            //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 
            txtPrestador.Attributes.Add("ReadOnly", "ReadOnly");
            txtDiagnostico.Attributes.Add("ReadOnly", "ReadOnly");
            //Inicio PETF 14/01/10
            txtFecha.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin  PETF 14/01/10
            //Fin MAHG 12/01/10

            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();
            if (objEmpresaDatos.CargaFechaDefecto)
                this.txtFecha.Text = DateTime.Now.ToShortDateString();
            ViewState["IdEstadoDefecto"] = objEmpresaDatos.IdSolicitudEstadoDefecto;
            ViewState["FechaInicio"] = DateTime.Now;

            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];

            this.ddlUsuario.Items.Add(new ListItem("--Paciente--", "-1"));

            foreach (DataRow row in dtParentescos.Rows)
            {
                if (row["IdParentesco"].ToString() == "0")
                {
                    objEmpleado.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
                    objEmpleado.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
                    objEmpleado.GetSIC_EMPLEADO();
                    this.ddlUsuario.Items.Add(new ListItem("TITU-" + objEmpleado.Nombre_completo, "0"));
                }
                else
                {
                    objBeneficiario.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
                    objBeneficiario.IdParentesco = Convert.ToInt32(row["IdParentesco"].ToString());
                    objBeneficiario.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
                    objBeneficiario.Opcion = 1;
                    dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                    foreach (DataRow rowBeneficiario in dtBeneficiarios.Rows)
                    {
                        this.ddlUsuario.Items.Add(new ListItem(rowBeneficiario["Parentesco"].ToString() + "-" + rowBeneficiario["nombre"].ToString(), rowBeneficiario["beneficiario_id"].ToString()));

                    }
                }
            }

            if (Request.QueryString["beneficiario_id"] != null && Request.QueryString["beneficiario_id"] != string.Empty)
            {
                this.ddlUsuario.SelectedValue = Request.QueryString["beneficiario_id"];
            }
            else
            {
                this.ddlUsuario.SelectedValue = "0";
            }

            this.FillListActivos("Prestadores", "Prestador", Convert.ToInt32(Session["Company"]), this.ddlPrestador, "--Solicitante--");
            this.FillList("FormasPago", "FormaPago", Convert.ToInt32(Session["Company"]), this.ddlFormaPago, "--Forma Pago--");
            this.FillListActivos("PlanesSolicitud", "PlanSolicitud", Convert.ToInt32(Session["Company"]), this.ddlPlanesSolicitud, "--Plan--");

            if (this.ddlPlanesSolicitud.Items.Count > 1)
            {
                this.ddlPlanesSolicitud.Visible = true;
                this.lblPlanesSolicitud.Visible = true;
            }
            if (this.ddlPrestador.Items.Count > 40)
            {
                this.txtPrestador.Visible = true;
                this.btnBuscarPrestador.Style["display"] = "";
                this.ddlPrestador.Visible = false;
            }
            else
            {
                this.txtPrestador.Visible = false;
                this.btnBuscarPrestador.Style["display"] = "none";
                this.ddlPrestador.Visible = true;
            }

            this.ddlAno.Items.Add(new ListItem("--Año--", "0"));
            for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 2; i++)
                this.ddlAno.Items.Add(new ListItem(i.ToString(), i.ToString()));

            for (int i = 1; i < 11; i++)
                this.ddlAdicionarTipoServicio.Items.Add(new ListItem(i.ToString(), i.ToString()));

            this.FillList("AnotacionesFijas", "AnotacionFija", this.chkAnotacionesFijas);

        }

        /// <summary>
        /// Método, Carga la grilla con espacios vaciós
        /// </summary>
        public void LoadListEmpty()
        {

            object[] lstObjetos = new object[0];
            DataTable dtTable = new DataTable();
            dtTable.Rows.Add(lstObjetos);

            this.dtlTipoServicio.DataSource = dtTable;
            this.dtlTipoServicio.DataBind();
            Label lblNuevo = (Label)this.dtlTipoServicio.Items[this.dtlTipoServicio.Items.Count - 1].FindControl("lblNuevo");
            lblNuevo.Text = "Nueva Solicitud";

            foreach (DataListItem item in this.dtlTipoServicio.Items)
            {
                DataTable dtProductosServicios = new DataTable();
                dtProductosServicios.Rows.Add(lstObjetos);
                dtProductosServicios.Rows.Add(lstObjetos);

                DataGrid dtgProductoServicio = (DataGrid)item.FindControl("dtgProductoServicio");
                dtgProductoServicio.DataSource = dtProductosServicios;
                dtgProductoServicio.DataBind();
            }
        }

        /// <summary>
        /// Método, Ingresa la solicitud
        /// </summary>
        /// <returns></returns>
        public long InsertSolicitud()
        {
            long idSolicitud = 0;
            Solicitud objSolicitud = new Solicitud();
            this.LoadObjectSolicitud(objSolicitud);
            this.validatePresupuesto(objSolicitud);
            idSolicitud = objSolicitud.InsertSolicitud();
            return idSolicitud;
        }

        /// <summary>
        /// Método, Modifica la solicitud
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void UpdateSolicitud(long p_idSolicitud)
        {
            Solicitud objSolicitud = new Solicitud();
            this.LoadObjectSolicitud(objSolicitud);
            objSolicitud.IdSolicitud = p_idSolicitud;
            this.validatePresupuesto(objSolicitud);
            objSolicitud.UpdateSolicitud();
        }

        /// <summary>
        /// Método, Modifica la solicitud
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void UpdateEstadoSolicitud(short p_idEstado, short p_idMotivo)
        {
            Solicitud objSolicitud = new Solicitud();
            this.LoadObjectSolicitudEstado(objSolicitud, p_idEstado, p_idMotivo);
            objSolicitud.UpdateSolicitudEstado();
        }

        /// <summary>
        /// Método, valida que el presupuesto del empleado y de la empresa exista para la fecha actual y tenga cupo disponible
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void validatePresupuesto(Solicitud objSolicitud)
        {
            //Validar prespuesto de la empresa
            PresupuestosEmpresa objPresupuestoEmpresa = new PresupuestosEmpresa();
            objPresupuestoEmpresa.Empresa_id = Convert.ToInt32(Session["Company"]);
            objPresupuestoEmpresa.IdTipoProceso = Convert.ToInt16(PresupuestosIndividuo.EnumTipoProceso.Reembolso);
            objPresupuestoEmpresa.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            objPresupuestoEmpresa.GetPresupuestosActual(Convert.ToDateTime(this.txtFecha.Text));
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
            objPresupuestoIndividuo.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
            objPresupuestoIndividuo.Beneficiario_id = Convert.ToInt32(this.ddlUsuario.SelectedValue);
            objPresupuestoIndividuo.IdTipoProceso = Convert.ToInt16(PresupuestosIndividuo.EnumTipoProceso.Reembolso);
            objPresupuestoIndividuo.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            objPresupuestoIndividuo.GetPresupuestosIndividuoActual(Convert.ToDateTime(this.txtFecha.Text));
            if (Convert.IsDBNull(objPresupuestoIndividuo.IdPresupuestoIndividuo) || objPresupuestoIndividuo.IdPresupuestoIndividuo == 0)
            {
                if (!Convert.IsDBNull(objPresupuestoEmpresa.IdTipoPresupuestoTodos))
                {
                    objPresupuestoIndividuo.InsertPresupuestosIndividuoAutomatico(objPresupuestoEmpresa.IdPresupuestoEmpresa);
                    objPresupuestoIndividuo.GetPresupuestosIndividuoActual(Convert.ToDateTime(this.txtFecha.Text));
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

        /// <summary>
        /// Método, carga el objeto solicitud con los datos básicos de la solicitud ingresados en la forma
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void LoadObjectSolicitud(Solicitud objSolicitud)
        {
            objSolicitud.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (this.txtOtroEmpleado.Text != "")
                objSolicitud.Id_empleado = Convert.ToInt32(this.txtOtroEmpleado.Text);
            else
                objSolicitud.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
            objSolicitud.Beneficiario_id = Convert.ToInt32(this.ddlUsuario.SelectedValue);
            objSolicitud.IdTipoSolicitud = Convert.ToInt16(Solicitud.EnumTipoSolicitud.Reembolso);
            objSolicitud.FechaInicioCreacion = (DateTime)(ViewState["FechaInicio"]);
            objSolicitud.SolicitudEmpleado = false;
            if (this.txtFecha.Text == string.Empty)
                throw new Exception("Debe ingresar la fecha de la solicitud");
            if (Convert.ToDateTime(this.txtFecha.Text).Date.CompareTo(DateTime.Now.Date) > 0)
                throw new Exception("La fecha de la solicitud no puede ser posterior a la fecha de hoy");

            objSolicitud.Fecha = Convert.ToDateTime(this.txtFecha.Text);
            objSolicitud.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            if (this.txtIdDiagnostico.Text.Trim() != string.Empty)
                objSolicitud.IdDiagnostico = Convert.ToInt32(this.txtIdDiagnostico.Text);
            if (this.ddlPrestador.Visible)
            {
                if (this.ddlPrestador.SelectedValue != "0")
                    objSolicitud.IdPrestador = Convert.ToInt32(this.ddlPrestador.SelectedValue);
            }
            else
            {
                if (this.txtIdPrestador.Text.Trim() != string.Empty)
                    objSolicitud.IdPrestador = Convert.ToInt32(this.txtIdPrestador.Text);
            }
            objSolicitud.Observaciones = this.txtComentarioSolicitud.Text.Trim();
            objSolicitud.Documentos = this.txtDocumentos.Text.Trim();
            objSolicitud.AnotacionesFijas = new ArrayList();

            foreach (ListItem itemAnotacion in this.chkAnotacionesFijas.Items)
            {
                if (itemAnotacion.Selected)
                    objSolicitud.AnotacionesFijas.Add(Convert.ToInt16(itemAnotacion.Value));
            }

            objSolicitud.MesLiquidacion = Convert.ToInt16(this.ddlMes.SelectedValue);
            objSolicitud.AnoLiquidacion = Convert.ToInt16(this.ddlAno.SelectedValue);
            objSolicitud.IdFormaPago = Convert.ToInt16(this.ddlFormaPago.SelectedValue);
            objSolicitud.IdSolicitudEstado = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Pendiente);
            if (Request.QueryString["SICAU_solicitud_id"] != null)
                objSolicitud.Id_solicitud_SICAU = Convert.ToInt32(Request.QueryString["SICAU_solicitud_id"]);
            if (Session["SICAU"] != null)
                objSolicitud.Usuario_idCreacion = Convert.ToInt32(Session["IdUser"]);
            else
                objSolicitud.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);
            ViewState["TotalValorFactura"] = Convert.ToDecimal(0);
            ViewState["TotalValorConvenioSolicitado"] = Convert.ToDecimal(0);
            ViewState["TotalValorAprobado"] = Convert.ToDecimal(0);
            this.LoadSolicitudTipoServicios(objSolicitud);
            objSolicitud.ValorTotalConvenioSolicitado = (decimal)ViewState["TotalValorConvenioSolicitado"];
            objSolicitud.ValorTotalFactura = (decimal)ViewState["TotalValorFactura"];
            objSolicitud.ValorTotalAprobado = (decimal)ViewState["TotalValorAprobado"];
        }

        /// <summary>
        /// Método, Carga los objetos de tipos de servicios ingresados en la forma
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void LoadSolicitudTipoServicios(Solicitud objSolicitud)
        {
            SolicitudTipoServicio objTipoServicio;
            ArrayList arrTiposServicio = new ArrayList();
            int numProveedor = 0;

            foreach (DataListItem item in this.dtlTipoServicio.Items)
            {
                DropDownList ddlTipoServicio = (DropDownList)item.FindControl("ddlTipoServicio");
                numProveedor = 0;

                if (ddlTipoServicio.SelectedValue != "0")
                {
                    ArrayList arrProveedoresCarga = this.LoadProveedores(item, Convert.ToInt32(ddlTipoServicio.SelectedValue));
                    TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador WC_AdicionarPrestador1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador)item.FindControl("WC_AdicionarPrestador1");

                    foreach (SolicitudTipoServicioProveedores objProveedores in arrProveedoresCarga)
                    {
                        TextBox txtIdSolicitudEstado = (TextBox)item.FindControl("txtIdSolicitudEstado");

                        if (txtIdSolicitudEstado.Text == string.Empty || (Convert.ToInt16(txtIdSolicitudEstado.Text) != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) && Convert.ToInt16(txtIdSolicitudEstado.Text) != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado)))
                        {
                            objTipoServicio = new SolicitudTipoServicio();
                            TextBox txtConsecutivo = (TextBox)item.FindControl("txtConsecutivo");
                            TextBox txtImpresiones = (TextBox)item.FindControl("txtImpresiones");
                            TextBox txtConsecutivoNombre = (TextBox)item.FindControl("txtConsecutivoNombre");
                            TextBox txtComentariosTipoServicio = (TextBox)item.FindControl("txtComentariosTipoServicio");
                            TextBox txtIdSolicitudTipoServicio = (TextBox)item.FindControl("txtIdSolicitudTipoServicio");
                            DropDownList ddlClaseAtencion = (DropDownList)item.FindControl("ddlClaseAtencion");
                            DropDownList ddlTipoAtencion = (DropDownList)item.FindControl("ddlTipoAtencion");
                            DropDownList ddlContingencia = (DropDownList)item.FindControl("ddlContingencia");
                            DropDownList ddlUnidadAprueba = (DropDownList)item.FindControl("ddlUnidadAprueba");
                            TPA.interfaz_empleado.WebControls.WC_AdicionarSolicitante WC_AdicionarSolicitante1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarSolicitante)ddlTipoServicio.Parent.FindControl("WC_AdicionarSolicitante1");
                            TextBox txtNumFactura = (TextBox)item.FindControl("txtNumFactura");
                            TextBox txtCuentaCobro = (TextBox)item.FindControl("txtCuentaCobro");
                            
                            TextBox txtFechaFactura = (TextBox)item.FindControl("txtFechaFactura");
                            txtFechaFactura.Attributes.Add("ReadOnly", "ReadOnly"); //PETF 14/01/10 ReadOnly

                            TextBox txtValorFactura = (TextBox)item.FindControl("txtValorFactura");
                            DataGrid dtgProductoServicio = (DataGrid)item.FindControl("dtgProductoServicio");

                            if (ddlTipoServicio.SelectedValue != "0" && (txtNumFactura.Text.Trim() == string.Empty || txtFechaFactura.Text.Trim() == string.Empty || txtValorFactura.Text.Trim() == string.Empty))
                            {
                                throw new Exception("Debe ingresar por cada tipo de servicio el número, fecha y valor de la factura");
                            }

                            if (Request.QueryString["IdSolicitudCopia"] == null)
                            {
                                if (txtConsecutivoNombre.Text != "" && numProveedor == 0)
                                {
                                    objTipoServicio.Consecutivo = Convert.ToInt64(txtConsecutivo.Text);
                                    objTipoServicio.ConsecutivoNombre = txtConsecutivoNombre.Text;
                                }
                            }
                            if (txtImpresiones.Text != "")
                            {
                                objTipoServicio.Impresiones = Convert.ToInt16(txtImpresiones.Text);
                            }
                            if (ddlUnidadAprueba.SelectedValue != string.Empty)
                            {
                                objTipoServicio.UnidadAprobacion = ddlUnidadAprueba.SelectedValue;
                            }
                            objTipoServicio.IdTipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue);

                            objTipoServicio.IdTipoAtencion = Convert.ToInt16(ddlTipoAtencion.SelectedValue);
                            objTipoServicio.IdClaseAtencion = Convert.ToInt16(ddlClaseAtencion.SelectedValue);
                            objTipoServicio.IdContingencia = Convert.ToInt16(ddlContingencia.SelectedValue);
                            objTipoServicio.NumeroFactura = txtNumFactura.Text.Trim();
                            objTipoServicio.NumeroCuentaCobro = txtCuentaCobro.Text.Trim();
                            objTipoServicio.FechaFactura = Convert.ToDateTime(txtFechaFactura.Text);
                            objTipoServicio.ValorFactura = Convert.ToDecimal(txtValorFactura.Text);
                            objTipoServicio.Comentarios = txtComentariosTipoServicio.Text;
                            objTipoServicio.IdPrestador = WC_AdicionarSolicitante1.GetSolicitante();
                            if (txtIdSolicitudTipoServicio.Text != string.Empty && numProveedor == 0)
                                objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(txtIdSolicitudTipoServicio.Text);
                            ViewState["TotalValorFactura"] = (decimal)ViewState["TotalValorFactura"] + objTipoServicio.ValorFactura;

                            bool despliegaUVR = true;
                            if (WC_AdicionarPrestador1.tieneUVR && !objProveedores.DespliegaUVR)
                                despliegaUVR = false;

                            this.LoadSolicitudServicios(objTipoServicio, dtgProductoServicio, Convert.ToInt32(ddlTipoServicio.SelectedValue), despliegaUVR);
                            this.LoadDiagnosticos(objTipoServicio, item);
                            ArrayList arrProveedores = new ArrayList();
                            arrProveedores.Add(objProveedores);
                            objTipoServicio.SolicitudTipoServicioProveedores = (ArrayList)arrProveedores.Clone();

                            Label lblNuevo = (Label)item.FindControl("lblNuevo");

                            if (Request.QueryString["IdSolicitudCopia"] != null)
                            {
                                objTipoServicio.IdTipoConsecutivo = Convert.ToInt16(SolicitudTipoServicio.EnumTipoConsecutivos.Nuevo);
                            }
                            else
                            {
                                if (lblNuevo.Text != string.Empty && arrProveedoresCarga.Count == 1)

                                    objTipoServicio.IdTipoConsecutivo = Convert.ToInt16(SolicitudTipoServicio.EnumTipoConsecutivos.Nuevo);
                                else
                                    objTipoServicio.IdTipoConsecutivo = Convert.ToInt16(SolicitudTipoServicio.EnumTipoConsecutivos.Asociado);

                            }

                            if (objTipoServicio.SolicitudServicios.Count == 0)
                                throw new Exception("Ingrese el detalle de los servicios para todas las solicitudes");
                            arrTiposServicio.Add(objTipoServicio);
                            numProveedor++;
                        }
                    }
                }
            }

            objSolicitud.SolicitudTipoServicios = arrTiposServicio;
        }

        /// <summary>
        /// Método, Carga los objetos de de servicios ingresados en la forma
        /// </summary>
        /// <param name="p_objTipoServicio"></param>
        /// <param name="p_dtgProductoServicio"></param>
        /// <param name="p_idTipoServicio"></param>
        public void LoadSolicitudServicios(SolicitudTipoServicio p_objTipoServicio, DataGrid p_dtgProductoServicio, int p_idTipoServicio, bool despliegaUVR)
        {
            SolicitudServicio objServicio;
            ArrayList arrServicios = new ArrayList();

            foreach (DataGridItem item in p_dtgProductoServicio.Items)
            {
                TextBox txtProductoServicio = (TextBox)item.FindControl("txtProductoServicio");
                txtProductoServicio.Attributes.Add("ReadOnly", "ReadOnly"); // PETF 14/01/10 Readonly

                if (txtProductoServicio.Text.Trim() != string.Empty)
                {
                    objServicio = new SolicitudServicio();

                    DropDownList ddlEstadoServicio = (DropDownList)item.FindControl("ddlEstadoServicio");
                    DropDownList ddlMotivo = (DropDownList)item.FindControl("ddlMotivo");
                    TextBox txtIdServicioProducto = (TextBox)item.FindControl("txtIdServicioProducto");
                    TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                    TextBox txtValorConvenioSolicitado = (TextBox)item.FindControl("txtValorConvenioSolicitado");
                    TextBox txtComentarioServicioProducto = (TextBox)item.FindControl("txtComentarioServicioProducto");
                    TextBox txtValorAprobado = (TextBox)item.FindControl("txtValorAprobado");
                    TextBox txtDescuento = (TextBox)item.FindControl("txtDescuento");
                    TextBox txtUVR = (TextBox)item.FindControl("txtUVR");
                    TextBox txtUVRSolicitado = (TextBox)item.FindControl("txtUVRSolicitado");
                    TextBox txtDosis = (TextBox)item.FindControl("txtDosis");
                    
                    TextBox txtFechaPrestacion = (TextBox)item.FindControl("txtFechaPrestacion");
                    txtFechaPrestacion.Attributes.Add("ReadOnly", "ReadOnly"); // PETF 14/01/10 Readonly

                    if (txtIdServicioProducto.Text.Trim() != string.Empty && (ddlEstadoServicio.SelectedValue == "0" || ddlMotivo.SelectedValue == "0"))
                    {
                        throw new Exception("Debe ingresar por cada servicio el estado y el motivo");
                    }

                    objServicio.IdSolicitudEstado = Convert.ToInt16(ddlEstadoServicio.SelectedValue);
                    objServicio.IdSolicitudMotivo = Convert.ToInt16(ddlMotivo.SelectedValue);
                    objServicio.Comentarios = txtComentarioServicioProducto.Text.Trim();

                    if (txtFechaPrestacion.Text.Trim() != string.Empty)
                        objServicio.FechaPrestacion = Convert.ToDateTime(txtFechaPrestacion.Text);
                    else
                        objServicio.FechaPrestacion = new DateTime(1900, 1, 1);

                    if (txtDosis.Visible)
                        objServicio.Dosis = txtDosis.Text.Trim();

                    if (p_idTipoServicio == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos) || p_idTipoServicio == Convert.ToInt32(Servicios.EnumTiposServicio.Vacunas))
                        objServicio.IdMedicamento = Convert.ToInt32(txtIdServicioProducto.Text);

                    else
                        objServicio.IdServicio = Convert.ToInt32(txtIdServicioProducto.Text);

                    if (txtCantidad.Text.Trim() != string.Empty)
                        objServicio.Cantidad = txtCantidad.Text;

                    try
                    {
                        if (p_objTipoServicio.IdTipoServicio == Convert.ToInt16(Servicios.EnumTiposServicio.Vacunas) && txtCantidad.Text.Trim() == string.Empty)
                            throw new Exception("Debe ingresar el número de vacunas en el campo cantidad");
                        if (p_objTipoServicio.IdTipoServicio == Convert.ToInt16(Servicios.EnumTiposServicio.Vacunas) && txtCantidad.Text.Trim() != string.Empty)
                            Convert.ToInt32(txtCantidad.Text);
                    }
                    catch
                    {
                        throw new Exception("Debe ingresar un número en el campo cantidad");
                    }

                    if (txtValorAprobado.Text.Trim() != string.Empty && despliegaUVR)
                    {
                        objServicio.ValorAprobado = Convert.ToDecimal(txtValorAprobado.Text);
                        ViewState["TotalValorAprobado"] = (decimal)ViewState["TotalValorAprobado"] + objServicio.ValorAprobado;
                    }
                    if (txtDescuento.Text.Trim() != string.Empty && despliegaUVR)
                    {
                        objServicio.Descuento = Convert.ToDecimal(txtDescuento.Text);
                    }
                    if (txtUVR.Text.Trim() != string.Empty && despliegaUVR)
                    {
                        objServicio.UVR = Convert.ToDecimal(txtUVR.Text);
                    }
                    if (txtUVRSolicitado.Text.Trim() != string.Empty && despliegaUVR)
                    {
                        objServicio.UVRConvenioSolicitado = Convert.ToDecimal(txtUVRSolicitado.Text);
                    }
                    if (txtValorConvenioSolicitado.Text.Trim() != string.Empty && despliegaUVR)
                    {
                        objServicio.ValorConvenioSolicitado = Convert.ToDecimal(txtValorConvenioSolicitado.Text);
                        ViewState["TotalValorConvenioSolicitado"] = (decimal)ViewState["TotalValorConvenioSolicitado"] + objServicio.ValorConvenioSolicitado;
                    }

                    arrServicios.Add(objServicio);
                }
                p_objTipoServicio.SolicitudServicios = arrServicios;
            }
        }

        /// <summary>
        /// Método, Carga el objeto solicitud para modificación estado
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void LoadObjectSolicitudEstado(Solicitud objSolicitud, short p_idEstado, short p_idMotivo)
        {
            objSolicitud.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            objSolicitud.GetSolicitud();
            objSolicitud.ObservacionAnulacion = this.ddlMotivosAnulacion.SelectedItem.Text.Trim();	
            objSolicitud.IdSolicitudEstado = p_idEstado;

            this.LoadSolicitudTipoServiciosEstado(objSolicitud, p_idEstado, p_idMotivo);
        }


        /// <summary>
        /// Método, Carga los objetos de tipos de servicios para modificación estado
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void LoadSolicitudTipoServiciosEstado(Solicitud objSolicitud, short p_idEstado, short p_idMotivo)
        {
            if (Request.QueryString["IdSolicitudTipoServicioCopia"] == null || Request.QueryString["IdSolicitudTipoServicioCopia"] == string.Empty)
            {
                SolicitudTipoServicio objTipoServicio;
                SolicitudTipoServicio objTipoServicioCarga;
                ArrayList arrTiposServicio = new ArrayList();

                objTipoServicio = new SolicitudTipoServicio();
                objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);
                DataTable dtTiposServicios = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];


                foreach (DataRow item in dtTiposServicios.Rows)
                {
                    if (Convert.ToInt16(item["IdSolicitudEstado"]) != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) && Convert.ToInt16(item["IdSolicitudEstado"]) != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                    {

                        objTipoServicioCarga = new SolicitudTipoServicio();
                        objTipoServicioCarga.IdSolicitudTipoServicio = Convert.ToInt64(item["IdSolicitudTipoServicio"]);
                        objTipoServicioCarga.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);
                        objTipoServicioCarga.ObservacionAnulacion = this.ddlMotivosAnulacion.SelectedItem.Text;

                        this.LoadSolicitudServiciosEstado(objTipoServicioCarga, objTipoServicioCarga.IdSolicitudTipoServicio, p_idEstado, p_idMotivo);

                        arrTiposServicio.Add(objTipoServicioCarga);
                    }
                }
                objSolicitud.SolicitudTipoServicios = arrTiposServicio;
            }
            else
            {
                SolicitudTipoServicio objTipoServicioCarga;
                SolicitudTipoServicio objTipoServicio;
                ArrayList arrTiposServicio = new ArrayList();

                objTipoServicio = new SolicitudTipoServicio();

                objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);
                objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicioCopia"]);
                objTipoServicio.GetSolicitudTipoServicio();

                if (objTipoServicio.IdSolicitudEstado != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) && objTipoServicio.IdSolicitudEstado != Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                {
                    objTipoServicioCarga = new SolicitudTipoServicio();
                    objTipoServicioCarga.IdSolicitudTipoServicio = Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicioCopia"]);
                    objTipoServicioCarga.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);
                    objTipoServicioCarga.ObservacionAnulacion = this.ddlMotivosAnulacion.SelectedItem.Text;
                    this.LoadSolicitudServiciosEstado(objTipoServicioCarga, objTipoServicioCarga.IdSolicitudTipoServicio, p_idEstado, p_idMotivo);
                    arrTiposServicio.Add(objTipoServicioCarga);
                }
                objSolicitud.SolicitudTipoServicios = arrTiposServicio;
            }
        }

        /// <summary>
        /// Método, Carga los objetos de servicios para modificación estado
        /// </summary>
        /// <param name="p_objTipoServicio"></param>
        /// <param name="p_dtgProductoServicio"></param>
        /// <param name="p_idTipoServicio"></param>
        public void LoadSolicitudServiciosEstado(SolicitudTipoServicio p_objTipoServicio, long p_idSolicitudTipoServicio, short p_idEstado, short p_idMotivo)
        {
            SolicitudServicio objServicio;
            SolicitudServicio objServicioCarga;
            ArrayList arrServicios = new ArrayList();

            objServicio = new SolicitudServicio();
            objServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
            objServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            DataTable dtServicios = objServicio.ConsultSolicitudServicio().Tables[0];

            foreach (DataRow item in dtServicios.Rows)
            {
                objServicioCarga = new SolicitudServicio();
                objServicioCarga.IdSolicitudServicio = Convert.ToInt64(item["IdSolicitudServicio"]);
                objServicioCarga.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
                objServicioCarga.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                objServicioCarga.IdSolicitudEstado = p_idEstado;
                objServicioCarga.IdSolicitudMotivo = p_idMotivo;

                arrServicios.Add(objServicioCarga);
            }
            p_objTipoServicio.SolicitudServicios = arrServicios;
        }

        /// <summary>
        /// Método, Carga los objetos de de diagnosticos del tipo de servicio de la solicitud
        /// </summary>
        /// <param name="p_objTipoServicio"></param>
        /// <param name="p_dtgProductoServicio"></param>
        /// <param name="p_idTipoServicio"></param>
        public void LoadDiagnosticos(SolicitudTipoServicio p_objTipoServicio, DataListItem p_item)
        {
            TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnostico WC_AdicionarDiagnostico1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnostico)p_item.FindControl("WC_AdicionarDiagnostico1");
            WC_AdicionarDiagnostico1.LoadDiagnosticos(p_objTipoServicio, Convert.ToInt16(Solicitud.EnumTipoSolicitud.Reembolso));
        }


        /// <summary>
        /// Método, Carga los objetos de de diagnosticos del tipo de servicio de la solicitud
        /// </summary>
        /// <param name="p_objTipoServicio"></param>
        /// <param name="p_dtgProductoServicio"></param>
        /// <param name="p_idTipoServicio"></param>
        public ArrayList LoadProveedores(DataListItem p_item, int p_tipoServicio)
        {
            TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador WC_AdicionarPrestador1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador)p_item.FindControl("WC_AdicionarPrestador1");
            return WC_AdicionarPrestador1.LoadProveedores(p_tipoServicio, Convert.ToInt16(Solicitud.EnumTipoSolicitud.Reembolso));
        }

        /// <summary>
        /// Carga el listado de diagnósticos
        /// </summary>
        /// <param name="p_lblIdDiagnosticos"></param>
        /// <param name="p_lblDiagnostico"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadControlDiagnosticos(TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnostico WC_AdicionarDiagnostico1, long p_idSolicitudTipoServicio)
        {
            WC_AdicionarDiagnostico1.LoadControlDiagnosticos(p_idSolicitudTipoServicio);
        }


        /// <summary>
        /// Método, Carga los controles del listado de proveedores del tipo de solicitud
        /// </summary>
        /// <param name="WC_AdicionarPrestador1"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadLabelProveedores(TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador WC_AdicionarPrestador1, long p_idSolicitudTipoServicio)
        {
            WC_AdicionarPrestador1.LoadControlProveedores(p_idSolicitudTipoServicio);

        }

        /// <summary>
        /// Método, Carga los datos de la solicitud en la forma
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void LoadFormSolicitud(long p_idSolicitud)
        {

            Solicitud objSolicitud = new Solicitud();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();
            this.lnkCambiarPaciente.Visible = true;

            if ((objSolicitud.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) || objSolicitud.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado)) && Request.QueryString["IdSolicitudCopia"] == null)
            {
                Response.Redirect("AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Convert.ToInt32(Request.QueryString["employee_id"]));
            }
            else
            {
                if (Request.QueryString["IdSolicitudCopia"] != null)
                    this.txtFecha.Text = DateTime.Now.ToShortDateString();
                else
                    this.txtFecha.Text = objSolicitud.Fecha.ToShortDateString();

                this.ddlUsuario.SelectedValue = objSolicitud.Beneficiario_id.ToString();
                this.txtComentarioSolicitud.Text = objSolicitud.Observaciones;
                this.ddlPlanesSolicitud.SelectedValue = objSolicitud.IdPlanSolicitud.ToString();
                this.txtDocumentos.Text = objSolicitud.Documentos;

                if (Request.QueryString["IdSolicituTipoServicioCopia"] == null || Request.QueryString["IdSolicituTipoServicioCopia"] == string.Empty)
                {

                    this.lblNoSolicitud.Text = objSolicitud.ConsecutivoNombre;
                    this.lblFechaCreacion.Text = objSolicitud.FechaCreacion.ToShortDateString();
                    this.ddlPlanesSolicitud.Enabled = false;
                    if (objSolicitud.Usuario_idCreacion != 0)
                    {
                        SIC_USUARIO objUsuario = new SIC_USUARIO();
                        objUsuario.Usuario_id = objSolicitud.Usuario_idCreacion;
                        DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
                        this.lblUsuarioCreacion.Text = dtUsuario.Rows[0]["nombre"].ToString();
                    }
                    else
                    {
                        this.lblUsuarioCreacion.Text = objSolicitud.NameUser;
                    }

                    if (objSolicitud.ValorTotalFactura != 0)
                        this.lblTotalFacturas.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalFactura);
                    if (objSolicitud.ValorTotalAprobado != 0)
                        this.lblTotalAprobado.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalAprobado);
                    this.ddlEstadoSoli.Visible = true;
                    this.lblEstado.Visible = true;
                }

                if (objSolicitud.ValorTotalConvenioSolicitado != 0)
                    this.lblTotalProductosServicios.Text = string.Format("{0:0,0}", objSolicitud.ValorTotalConvenioSolicitado);

                if (objSolicitud.IdPrestador != 0)
                {
                    if (this.ddlPrestador.Visible)
                        this.ddlPrestador.SelectedValue = objSolicitud.IdPrestador.ToString();
                    else
                    {
                        GeneralTable objGeneral = new GeneralTable();
                        objGeneral.TableName = "Prestadores";
                        objGeneral.ColumnName = "Prestador";
                        objGeneral.Id = Convert.ToInt32(objSolicitud.IdPrestador);
                        objGeneral.GetGeneralTable();
                        this.txtPrestador.Text = objGeneral.Nombre;
                        this.txtIdPrestador.Text = objSolicitud.IdPrestador.ToString();
                    }
                }
                if (objSolicitud.MesLiquidacion != 0)
                    this.ddlMes.SelectedValue = objSolicitud.MesLiquidacion.ToString();
                if (objSolicitud.AnoLiquidacion != 0)
                {
                    if (this.ddlAno.Items.FindByValue(objSolicitud.AnoLiquidacion.ToString()) == null)
                        this.ddlAno.Items.Add(new ListItem(objSolicitud.AnoLiquidacion.ToString(), objSolicitud.AnoLiquidacion.ToString()));
                    else
                        this.ddlAno.SelectedValue = objSolicitud.AnoLiquidacion.ToString();
                }
                if (objSolicitud.IdFormaPago != 0)
                    this.ddlFormaPago.SelectedValue = objSolicitud.IdFormaPago.ToString();

                if (Request.QueryString["Anular"] != null && Request.QueryString["Anular"] != string.Empty)
                {
                    //Cargar, desplegar y seleccionar estado
                    this.FillList("SolicitudMotivosAnulacion", "SolicitudMotivoAnulacion", this.ddlMotivosAnulacion, "--Motivo Anulación--");
                    this.btnAnular.Visible = true;
                    this.ddlMotivosAnulacion.Visible = true;
                    this.lblAnulacion.Visible = true;
                    this.btnGuardar.Visible = false;
                }

                this.FillList(this.ddlEstadoSoli, "SolicitudEstadosCondiciones", "--Estados--", null, null, true);
                if (Request.QueryString["IdSolicituTipoServicioCopia"] == null || Request.QueryString["IdSolicituTipoServicioCopia"] == string.Empty)
                {
                    if (objSolicitud.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado))
                        this.ddlEstadoSoli.Items.Add(new ListItem("Liquidado", Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado).ToString()));
                    if (objSolicitud.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                        this.ddlEstadoSoli.Items.Add(new ListItem("Anulado", Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado).ToString()));
                }
                this.ddlEstadoSoli.SelectedValue = objSolicitud.IdSolicitudEstado.ToString();

                ViewState["IdPresupuestoIndividuo"] = objSolicitud.IdPresupuestoIndividuo;
                ViewState["IdPresupuestoEmpresa"] = objSolicitud.IdPresupuestoEmpresa;

                if (Request.QueryString["IdSolicitud"] != null)
                    this.tblDatosSolicitud.Style["display"] = "";

                //Cargar los tipos de servicio
                objTipoServicio.IdSolicitud = p_idSolicitud;
                //Si viene el id tipo servicio para copiar, se filtra por ese tipo de servicio
                if (Request.QueryString["IdSolicituTipoServicioCopia"] != null && Request.QueryString["IdSolicituTipoServicioCopia"] != string.Empty)
                    objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64("IdSolicituTipoServicioCopia");

                this.dtlTipoServicio.DataSource = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];
                this.dtlTipoServicio.DataBind();

                //Cargar las anotaciones fijas
                DataTable lstAnotaciones = objSolicitud.ConsultSolicitudAnotacionesFijas().Tables[0];
                foreach (DataRow row in lstAnotaciones.Rows)
                {
                    this.chkAnotacionesFijas.Items.FindByValue(row["IdAnotacionFija"].ToString()).Selected = true;
                }

            }
        }

        /// <summary>
        /// Método, Adiciona un nuevo bloque para adicionar un nuevo tipo de servicio
        /// </summary>
        public void addFacturaDataList(bool p_nueva, int cantidad)
        {
            object[] lstObjetos = new object[0];
            ArrayList arrNuevos = new ArrayList();
            int i = 0;
            DataTable dtTiposServicio;
            Label lblNuevo;

            if (Request.QueryString["IdSolicitud"] != null || Request.QueryString["IdSolicitudCopia"] != null)
            {

                SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
                if (Request.QueryString["IdSolicitud"] != null)
                    objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                if (Request.QueryString["IdSolicitudCopia"] != null)
                    objTipoServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);
                dtTiposServicio = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];

                for (i = dtTiposServicio.Rows.Count; i < this.dtlTipoServicio.Items.Count + cantidad; i++)
                {
                    dtTiposServicio.Rows.Add(lstObjetos);

                    if (cantidad > 1)
                    {
                        arrNuevos.Add(i);
                    }
                }
            }
            else
            {
                dtTiposServicio = new DataTable();

                for (i = 0; i < this.dtlTipoServicio.Items.Count + cantidad; i++)
                {
                    dtTiposServicio.Rows.Add(lstObjetos);

                    if (this.dtlTipoServicio.Items.Count > i)
                    {
                        Label lblNuevoAnt = (Label)this.dtlTipoServicio.Items[i].FindControl("lblNuevo");
                        if (lblNuevoAnt.Text != string.Empty)
                            arrNuevos.Add(i);

                    }
                    if (cantidad > 1)
                    {
                        arrNuevos.Add(i);
                    }
                }
            }
            this.dtlTipoServicio.DataSource = dtTiposServicio;
            this.dtlTipoServicio.DataBind();

            foreach (int indexNuevo in arrNuevos)
            {
                lblNuevo = (Label)this.dtlTipoServicio.Items[indexNuevo].FindControl("lblNuevo");
                lblNuevo.Text = "Nueva Solicitud";
            }
            if (p_nueva)
            {
                lblNuevo = (Label)this.dtlTipoServicio.Items[this.dtlTipoServicio.Items.Count - 1].FindControl("lblNuevo");
                lblNuevo.Text = "Nueva Solicitud";
                lblNuevo = (Label)this.dtlTipoServicio.Items[0].FindControl("lblNuevo");
                lblNuevo.Text = "Nueva Solicitud";
            }

        }

        /// <summary>
        /// Método, Elimina un tipo de servicio de la solicitud y sus servicios
        /// </summary>
        /// <param name="p_idSolicitudTipoServicio"></param>
        /// <param name="p_idSolicitud"></param>
        public void DeleteSolicitudTipoServicio(long p_idSolicitudTipoServicio, long p_idSolicitud)
        {
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objTipoServicio.IdSolicitud = p_idSolicitud;
            objTipoServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            objTipoServicio.DeleteSolicitudTipoServicio();

            //Cargar nuevamente los datos del datalist
            objTipoServicio = new SolicitudTipoServicio();
            objTipoServicio.IdSolicitud = p_idSolicitud;
            this.dtlTipoServicio.DataSource = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];
            this.dtlTipoServicio.DataBind();
        }

        /// <summary>
        /// Método, Elimina un servicio de la solicitud
        /// </summary>
        /// <param name="p_idSolicitudServicio"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        /// <param name="p_idSolicitud"></param>
        /// <param name="dtgProductoServicio"></param>
        public void DeleteSolicitudServicio(long p_idSolicitudServicio, long p_idSolicitudTipoServicio, long p_idSolicitud, DataGrid dtgProductoServicio)
        {
            SolicitudServicio objServicio = new SolicitudServicio();
            objServicio.IdSolicitud = p_idSolicitud;
            objServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            objServicio.IdSolicitudServicio = p_idSolicitudServicio;
            objServicio.DeleteSolicitudServicio();

            //Cargar nuevamente los datos de la datagrid
            objServicio = new SolicitudServicio();
            objServicio.IdSolicitud = p_idSolicitud;
            objServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            dtgProductoServicio.DataSource = objServicio.ConsultSolicitudServicio();
            dtgProductoServicio.DataBind();
            dtgProductoServicio.Columns[dtgProductoServicio.Columns.Count - 1].Visible = true;
        }

        /// <summary>
        /// Método, inhabilita los controles de manera recursiva
        /// </summary>
        /// <param name="root"></param>
        private void DisableRecursive(Control root)
        {
            if (root is TextBox)
                ((TextBox)root).Enabled = false;

            if (root is CheckBox)
                ((CheckBox)root).Enabled = false;

            if (root is RadioButton)
                ((RadioButton)root).Enabled = false;

            if (root is RadioButtonList)
                ((RadioButtonList)root).Enabled = false;

            if (root is DropDownList)
                ((DropDownList)root).Enabled = false;

            if (root is Button)
                ((Button)root).Visible = false;

            if (root is System.Web.UI.HtmlControls.HtmlControl)
            {
                ((System.Web.UI.HtmlControls.HtmlControl)root).Disabled = true;
                ((System.Web.UI.HtmlControls.HtmlControl)root).Attributes.Remove("OnClick");
                ((System.Web.UI.HtmlControls.HtmlControl)root).Attributes.Remove("onclick");
            }

            foreach (Control child in root.Controls)
            {
                DisableRecursive(child);
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
            this.lnkCambiarPaciente.Click += new System.EventHandler(this.lnkCambiarPaciente_Click);
            this.lnkCargarNuevoPaciente.Click += new System.EventHandler(this.lnkCargarNuevoPaciente_Click);
            this.imbAdicionarSolicitud.Click += new System.Web.UI.ImageClickEventHandler(this.imbAdicionarSolicitud_Click);
            this.lnkAdicionarSolicitud.Click += new System.EventHandler(this.lnkAdicionarSolicitud_Click);
            this.ddlAdicionarTipoServicio.SelectedIndexChanged += new System.EventHandler(this.ddlAdicionarTipoServicio_SelectedIndexChanged);
            this.imbAdicionarTipoServicio.Click += new System.Web.UI.ImageClickEventHandler(this.imbAdicionarTipoServicio_Click);
            this.lnkAdicionarTipoServicio.Click += new System.EventHandler(this.lnkAdicionarTipoServicio_Click);
            this.imbVerHistorial.Click += new System.Web.UI.ImageClickEventHandler(this.imbVerHistorial_Click);
            this.lnkVerHistorico.Click += new System.EventHandler(this.lnkVerHistorico_Click);
            this.imbGuardar.Click += new System.Web.UI.ImageClickEventHandler(this.imbGuardar_Click);
            this.lnkGuardar.Click += new System.EventHandler(this.lnkGuardar_Click);
            this.dtlTipoServicio.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtlTipoServicio_ItemCommand);
            this.dtlTipoServicio.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlTipoServicio_ItemDataBound);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, Adiciona un bloque de tipo de servicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdicionarTipoServicio_Click(object sender, System.EventArgs e)
        {
            try
            {
                addFacturaDataList(false, 1);
                this.ResizePage(this.dtlTipoServicio.Items.Count.ToString());
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, adiciona un bloque de tipo de servicio creando un nuevo consecutivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAdicionarSolicitud_Click(object sender, System.EventArgs e)
        {
            try
            {
                addFacturaDataList(true, 1);
                this.ResizePage(this.dtlTipoServicio.Items.Count.ToString());
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, Adiciona un bloque de tipo de servicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbAdicionarTipoServicio_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                addFacturaDataList(false, 1);
                this.ResizePage(this.dtlTipoServicio.Items.Count.ToString());
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        /// <summary>
        /// Evento, adiciona un bloque de tipo de servicio creando un nuevo consecutivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbAdicionarSolicitud_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                addFacturaDataList(true, 1);
                this.ResizePage(this.dtlTipoServicio.Items.Count.ToString());
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }



        /// <summary>
        /// Evento, Carga la especialidad del prestador o permite el ingreso de uno nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlPrestador_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Prestadores objPrestador;
            try
            {
                objPrestador = new Prestadores();
                objPrestador.IdPrestador = Convert.ToInt32(this.ddlPrestador.SelectedValue);
                objPrestador.GetPrestadores();

                this.lblEspecialidad.Text = objPrestador.NombreEspecialidad;

                if (this.ddlPrestador.SelectedValue == "-1")
                {
                    this.txtOtroPrestador.Visible = true;
                }
                else
                {
                    this.txtOtroPrestador.Visible = false;
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

        /// <summary>
        /// Evento, Carga los motivos del estado seleccionado y oculta o muestra algunos campos dependiendo del estado seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ddlEstadoServicio_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList ddlEstadoServicio = (DropDownList)sender;
            DropDownList ddlMotivo = (DropDownList)ddlEstadoServicio.Parent.FindControl("ddlMotivo");
            TextBox txtValorAprobado = (TextBox)ddlEstadoServicio.Parent.FindControl("txtValorAprobado");
            TextBox txtDescuento = (TextBox)ddlEstadoServicio.Parent.FindControl("txtDescuento");
            TextBox txtUVR = (TextBox)ddlEstadoServicio.Parent.FindControl("txtUVR");
            Button lnkRegistrarUVR = (Button)ddlEstadoServicio.Parent.FindControl("lnkRegistrarUVR");
            Label lblValorAprobado = (Label)ddlEstadoServicio.Parent.FindControl("lblValorAprobado");
            Label lblDescuento = (Label)ddlEstadoServicio.Parent.FindControl("lblDescuento");

            this.FillList(ddlMotivo, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(ddlEstadoServicio.SelectedValue));

            if (Convert.ToInt16(ddlEstadoServicio.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
            {
                txtValorAprobado.Visible = true;
                txtDescuento.Visible = true;
                lblValorAprobado.Visible = true;
                lblDescuento.Visible = true;
                lnkRegistrarUVR.Visible = true;
                txtUVR.Visible = true;
            }
            else
            {
                txtValorAprobado.Visible = false;
                txtDescuento.Visible = false;
                lblValorAprobado.Visible = false;
                lblDescuento.Visible = false;
                lnkRegistrarUVR.Visible = false;
                txtUVR.Visible = false;

                if (Convert.ToInt16(ddlEstadoServicio.SelectedValue) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Negado))
                {
                    txtValorAprobado.Text = "0";
                    txtDescuento.Text = "0";
                    txtUVR.Text = "0";
                }
            }

            this.ResizePage(ddlEstadoServicio.ClientID);
        }

        /// <summary>
        /// Evento, Carga los proveedores, oculta o despliega controles y cambia etiquetas dependiendo del tipo de servicio seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ddlTipoServicio_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList ddlTipoServicio = (DropDownList)sender;
            DataGrid dtgProductoServicio = (DataGrid)ddlTipoServicio.Parent.FindControl("dtgProductoServicio");
            WC_BuscarServicioProducto1.CleanControl();

            if (ddlTipoServicio.SelectedValue != "0")
            {
                TipoServicios objTipoServicio = new TipoServicios();
                objTipoServicio.IdTipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue);
                objTipoServicio.GetTipoServicios();

                dtgProductoServicio.Columns[0].HeaderText = objTipoServicio.EtiquetaProductoServicio;
                dtgProductoServicio.Columns[1].HeaderText = objTipoServicio.EtiquetaCantidad;
            }

            foreach (DataGridItem item in dtgProductoServicio.Items)
            {
                TextBox txtIdTipoServicio = (TextBox)item.FindControl("txtIdTipoServicio");
                txtIdTipoServicio.Text = ddlTipoServicio.SelectedValue;
                TextBox txtIdServicioProducto = (TextBox)item.FindControl("txtIdServicioProducto");
                
                TextBox txtProductoServicio = (TextBox)item.FindControl("txtProductoServicio");
                txtProductoServicio.Attributes.Add("ReadOnly", "ReadOnly"); // PETF 14/01/10 Readonly

                TextBox txtValorConvenioSolicitado = (TextBox)item.FindControl("txtValorConvenioSolicitado");
                TextBox txtComentarioServicioProducto = (TextBox)item.FindControl("txtComentarioServicioProducto");
                TextBox txtValorAprobado = (TextBox)item.FindControl("txtValorAprobado");
                TextBox txtDescuento = (TextBox)item.FindControl("txtDescuento");
                TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                DropDownList ddlEstadoServicio = (DropDownList)item.FindControl("ddlEstadoServicio");
                DropDownList ddlMotivo = (DropDownList)item.FindControl("ddlMotivo");
                Button btnBuscarProductoServicio = (Button)item.FindControl("btnBuscarProductoServicio");
                TextBox txtDosis = (TextBox)item.FindControl("txtDosis");
                Label lblDosis = (Label)item.FindControl("lblDosis");
                Button lnkRegistrarUVR = (Button)item.FindControl("lnkRegistrarUVR");
                TextBox txtUVR = (TextBox)item.FindControl("txtUVR");
                Button lnkUVRSolicitado = (Button)item.FindControl("lnkUVRSolicitado");
                TextBox txtUVRSolicitado = (TextBox)item.FindControl("txtUVRSolicitado");


                txtIdServicioProducto.Text = "";
                txtProductoServicio.Text = "";
                txtValorConvenioSolicitado.Text = "";
                txtDosis.Text = "";
                txtCantidad.Text = "";
                ddlEstadoServicio.SelectedIndex = 0;
                txtValorAprobado.Text = "";
                txtDescuento.Text = "";
                ddlMotivo.SelectedIndex = 0;
                txtComentarioServicioProducto.Text = "";

                lnkUVRSolicitado.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVRSolicitado.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "');");
                lnkRegistrarUVR.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVR.ClientID + "','" + txtValorAprobado.ClientID + "');");
                btnBuscarProductoServicio.Attributes.Add("OnClick", "javascript:ShowServicioProducto(this,'" + txtIdServicioProducto.ClientID + "','" + txtIdTipoServicio.Text + "','" + txtProductoServicio.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "','" + txtCantidad.ClientID + "');");

                if (Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos) || Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Vacunas))
                {
                    txtDosis.Visible = true;
                    lblDosis.Visible = true;

                }
                else
                {
                    txtDosis.Visible = false;
                    lblDosis.Visible = false;
                }
                if (ViewState["IdEstadoDefecto"] != null && Convert.ToInt16(ViewState["IdEstadoDefecto"]) != 0)
                {
                    ddlEstadoServicio.SelectedValue = Convert.ToInt16(ViewState["IdEstadoDefecto"]).ToString();
                    this.FillList(ddlMotivo, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(ViewState["IdEstadoDefecto"]));
                }

            }


            this.ResizePage(ddlTipoServicio.ClientID);
        }


        /// <summary>
        /// Evento, Realiza la carga para ingreso de varios servicios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ddlAdicionarServicios_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            DropDownList ddlAdicionarServicios = (DropDownList)sender;
            DataGrid dtgProductoServicio = (DataGrid)ddlAdicionarServicios.Parent.FindControl("dtgProductoServicio");
            TextBox txtIdSolicitudTipoServicio = (TextBox)ddlAdicionarServicios.Parent.FindControl("txtIdSolicitudTipoServicio");
            int cantidad = Convert.ToInt32((ddlAdicionarServicios.SelectedValue)) - 1;

            object[] lstObjetos = new object[0];
            int i = 0;
            DataTable dtProductos;

            if ((Request.QueryString["IdSolicitud"] != null || Request.QueryString["IdSolicitudCopia"] != null) && txtIdSolicitudTipoServicio.Text != string.Empty)
            {
                SolicitudServicio objServicio = new SolicitudServicio();
                if (Request.QueryString["IdSolicitud"] != null)
                    objServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                if (Request.QueryString["IdSolicitudCopia"] != null)
                    objServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

                objServicio.IdSolicitudTipoServicio = Convert.ToInt64(txtIdSolicitudTipoServicio.Text);
                dtProductos = objServicio.ConsultSolicitudServicio().Tables[0];

                for (i = dtProductos.Rows.Count; i < dtgProductoServicio.Items.Count + cantidad; i++)
                {
                    dtProductos.Rows.Add(lstObjetos);
                }

            }
            else
            {
                dtProductos = new DataTable();

                for (i = dtgProductoServicio.Items.Count - 1; i < dtgProductoServicio.Items.Count + cantidad; i++)
                {
                    dtProductos.Rows.Add(lstObjetos);
                }
            }

            dtgProductoServicio.DataSource = dtProductos;
            dtgProductoServicio.DataBind();

            this.ResizePage("Adicionar" + cantidad);

        }

        /// <summary>
        /// Método, carga de los datos en la grilla de servicios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dtgProductoServicio_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            try
            {
                //Cargar las etiquetas de los encabezados de la grilla de servicios
                if (e.Item.ItemType == ListItemType.Header)
                {
                    DataGrid dtgProductoServicio = (DataGrid)sender;
                    DropDownList ddlTipoServicio = (DropDownList)dtgProductoServicio.Parent.FindControl("ddlTipoServicio");

                    if (ddlTipoServicio.SelectedValue != "0")
                    {
                        TipoServicios objTipoServicio = new TipoServicios();
                        objTipoServicio.IdTipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue);
                        objTipoServicio.GetTipoServicios();

                        e.Item.Cells[0].Text = objTipoServicio.EtiquetaProductoServicio;
                        e.Item.Cells[1].Text = objTipoServicio.EtiquetaCantidad;
                    }

                }
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Carga los listados
                    DataGrid dtgProductoServicio = (DataGrid)sender;
                    DropDownList ddlEstadoServicio = (DropDownList)e.Item.FindControl("ddlEstadoServicio");
                    DropDownList ddlMotivo = (DropDownList)e.Item.FindControl("ddlMotivo");
                    this.FillList(ddlEstadoServicio, "SolicitudEstadosCondiciones", "--Estados--", null, null, true);
                    ddlMotivo.Items.Add(new ListItem("--Motivo--", "0"));
                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                    DropDownList ddlTipoServicio = (DropDownList)dtgProductoServicio.Parent.FindControl("ddlTipoServicio");
                    
                    TextBox txtFechaPrestacion = (TextBox)e.Item.FindControl("txtFechaPrestacion");
                    txtFechaPrestacion.Attributes.Add("ReadOnly", "ReadOnly"); // PETF 14/01/10 Readonly

                    TextBox txtValorConvenioSolicitado = (TextBox)e.Item.FindControl("txtValorConvenioSolicitado");

                    //Adicionar Eventos
                    System.Web.UI.WebControls.Image btnFechaPrestacion = (System.Web.UI.WebControls.Image)e.Item.FindControl("btnFechaPrestacion");
                    btnFechaPrestacion.Attributes.Add("OnClick", "javascript:MostrarCalendario(Form1." + txtFechaPrestacion.ClientID + ",Form1." + txtFechaPrestacion.ClientID + ",'dd/mm/yyyy');");

                    //Mostrar controles si el estado por defecto es aprobado
                    if (Convert.ToInt16(ViewState["IdEstadoDefecto"]) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
                    {
                        TextBox txtValorAprobado = (TextBox)e.Item.FindControl("txtValorAprobado");
                        TextBox txtDescuento = (TextBox)e.Item.FindControl("txtDescuento");
                        Button lnkRegistrarUVR = (Button)e.Item.FindControl("lnkRegistrarUVR");
                        Label lblValorAprobado = (Label)e.Item.FindControl("lblValorAprobado");
                        Label lblDescuento = (Label)e.Item.FindControl("lblDescuento");
                        TextBox txtUVR = (TextBox)e.Item.FindControl("txtUVR");

                        txtValorAprobado.Visible = true;
                        txtDescuento.Visible = true;
                        lblValorAprobado.Visible = true;
                        lblDescuento.Visible = true;
                        lnkRegistrarUVR.Visible = true;
                        txtUVR.Visible = true;

                        txtValorConvenioSolicitado.Attributes.Add("onmouseout", "javascript:copyValue(this,'" + txtValorAprobado.ClientID + "');");
                    }

                    if (ddlEstadoServicio.SelectedValue == "0")
                    {
                        ddlEstadoServicio.SelectedValue = Convert.ToInt16(ViewState["IdEstadoDefecto"]).ToString();
                        this.FillList(ddlMotivo, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(ViewState["IdEstadoDefecto"]));
                    }
                    if (ddlTipoServicio.SelectedValue != "0")
                    {
                        TextBox txtIdTipoServicio = (TextBox)e.Item.FindControl("txtIdTipoServicio");
                        txtIdTipoServicio.Text = ddlTipoServicio.SelectedValue;
                        TextBox txtIdServicioProducto = (TextBox)e.Item.FindControl("txtIdServicioProducto");
                        
                        TextBox txtProductoServicio = (TextBox)e.Item.FindControl("txtProductoServicio");
                        txtProductoServicio.Attributes.Add("ReadOnly", "ReadOnly"); // PETF 14/01/10 Readonly

                        TextBox txtCantidad = (TextBox)e.Item.FindControl("txtCantidad");
                        TextBox txtValorAprobado = (TextBox)e.Item.FindControl("txtValorAprobado");
                        TextBox txtDescuento = (TextBox)e.Item.FindControl("txtDescuento");
                        Button btnBuscarProductoServicio = (Button)e.Item.FindControl("btnBuscarProductoServicio");
                        Button lnkRegistrarUVR = (Button)e.Item.FindControl("lnkRegistrarUVR");
                        TextBox txtUVR = (TextBox)e.Item.FindControl("txtUVR");
                        Button lnkUVRSolicitado = (Button)e.Item.FindControl("lnkUVRSolicitado");
                        TextBox txtUVRSolicitado = (TextBox)e.Item.FindControl("txtUVRSolicitado");

                        lnkUVRSolicitado.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVRSolicitado.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "');");
                        lnkRegistrarUVR.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVR.ClientID + "','" + txtValorAprobado.ClientID + "');");
                        btnBuscarProductoServicio.Attributes.Add("OnClick", "javascript:ShowServicioProducto(this,'" + txtIdServicioProducto.ClientID + "','" + txtIdTipoServicio.Text + "','" + txtProductoServicio.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "','" + txtCantidad.ClientID + "');");
                    }

                    //Cargar los datos si viene el id de la solicitud
                    if ((Request.QueryString["IdSolicitud"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudServicio"]) && (long)rowItem["IdSolicitudServicio"] != 0)
                        || (Request.QueryString["IdSolicitudCopia"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudServicio"]) && (long)rowItem["IdSolicitudServicio"] != 0))
                    {
                        TextBox txtIdSolicitudServicio = (TextBox)e.Item.FindControl("txtIdSolicitudServicio");
                        
                        TextBox txtProductoServicio = (TextBox)e.Item.FindControl("txtProductoServicio");
                        txtProductoServicio.Attributes.Add("ReadOnly", "ReadOnly"); // PETF 14/01/10 Readonly

                        TextBox txtIdServicioProducto = (TextBox)e.Item.FindControl("txtIdServicioProducto");
                        TextBox txtCantidad = (TextBox)e.Item.FindControl("txtCantidad");
                        TextBox txtDosis = (TextBox)e.Item.FindControl("txtDosis");
                        TextBox txtValorAprobado = (TextBox)e.Item.FindControl("txtValorAprobado");
                        TextBox txtDescuento = (TextBox)e.Item.FindControl("txtDescuento");
                        TextBox txtUVR = (TextBox)e.Item.FindControl("txtUVR");
                        TextBox txtUVRSolicitado = (TextBox)e.Item.FindControl("txtUVRSolicitado");
                        Button lnkRegistrarUVR = (Button)e.Item.FindControl("lnkRegistrarUVR");
                        TextBox txtComentarioServicioProducto = (TextBox)e.Item.FindControl("txtComentarioServicioProducto");
                        Label lblDosis = (Label)e.Item.FindControl("lblDosis");
                        Label lblValorAprobado = (Label)e.Item.FindControl("lblValorAprobado");
                        Label lblDescuento = (Label)e.Item.FindControl("lblDescuento");

                        if ((Convert.ToInt16((rowItem["IdSolicitudEstado"])) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) || Convert.ToInt16((rowItem["IdSolicitudEstado"])) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                            && (Request.QueryString["IdSolicitudCopia"] == null || Request.QueryString["IdSolicitudCopia"] == string.Empty))
                        {

                            if (Convert.ToInt16((rowItem["IdSolicitudEstado"])) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                            {
                                ddlEstadoServicio.Items.Add(new ListItem("Anulado", Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado).ToString()));
                            }
                            else
                            {
                                ddlEstadoServicio.Items.Add(new ListItem("Liquidado", Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado).ToString()));
                            }
                            DisableRecursive(e.Item);

                        }
                        if (Request.QueryString["IdSolicitudCopia"] == null || Request.QueryString["IdSolicitudCopia"] == string.Empty)
                        {
                            ddlEstadoServicio.SelectedValue = ((short)rowItem["IdSolicitudEstado"]).ToString();
                            if (!Convert.IsDBNull(rowItem["FechaPrestacion"]))
                            {
                                txtFechaPrestacion.Text = ((DateTime)rowItem["FechaPrestacion"]).ToShortDateString();
                            }
                            if ((!Convert.IsDBNull(rowItem["ValorAprobado"]) && Convert.ToDecimal(rowItem["ValorAprobado"]) != 0) || (short)rowItem["IdSolicitudEstado"] == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
                            {
                                if (!Convert.IsDBNull(rowItem["ValorAprobado"]) && Convert.ToDecimal(rowItem["ValorAprobado"]) != 0)
                                    txtValorAprobado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorAprobado"]));
                                if (!Convert.IsDBNull(rowItem["Descuento"]) && Convert.ToDecimal(rowItem["Descuento"]) != 0)
                                    txtDescuento.Text = string.Format("{0:N2}", ((decimal)rowItem["Descuento"]));
                                txtValorAprobado.Visible = true;
                                txtDescuento.Visible = true;
                                lblValorAprobado.Visible = true;
                                lblDescuento.Visible = true;
                                lnkRegistrarUVR.Visible = true;
                                txtUVR.Visible = true;
                            }
                            if (!Convert.IsDBNull(rowItem["UVR"]))
                                txtUVR.Text = string.Format("{0:0,0}", ((decimal)rowItem["UVR"]));
                        }
                        txtComentarioServicioProducto.Text = (string)rowItem["Comentarios"];
                        txtIdSolicitudServicio.Text = ((long)rowItem["IdSolicitudServicio"]).ToString();

                        if (!Convert.IsDBNull(rowItem["IdMedicamento"]))
                        {
                            Medicamentos objMedicamento = new Medicamentos();
                            objMedicamento.IdMedicamento = (int)rowItem["IdMedicamento"];
                            objMedicamento.GetMedicamentos();
                            txtIdServicioProducto.Text = ((int)rowItem["IdMedicamento"]).ToString();
                            txtProductoServicio.Text = objMedicamento.NombreCompleto;
                            txtProductoServicio.ToolTip = objMedicamento.NombreCompleto;

                        }
                        if (!Convert.IsDBNull(rowItem["IdServicio"]))
                        {
                            Servicios objServicio = new Servicios();
                            objServicio.IdServicio = (int)rowItem["IdServicio"];
                            objServicio.GetServicios();
                            txtIdServicioProducto.Text = ((int)rowItem["IdServicio"]).ToString();
                            txtProductoServicio.Text = objServicio.NombreCompleto;
                            txtProductoServicio.ToolTip = objServicio.NombreCompleto;

                        }

                        if (!Convert.IsDBNull(rowItem["Cantidad"]))
                            txtCantidad.Text = (rowItem["Cantidad"]).ToString();
                        if (!Convert.IsDBNull(rowItem["Dosis"]))
                        {
                            txtDosis.Text = (string)rowItem["Dosis"];
                            txtDosis.Visible = true;
                            lblDosis.Visible = true;

                        }
                        if (!Convert.IsDBNull(rowItem["UVRConvenioSolicitado"]))
                            txtUVRSolicitado.Text = string.Format("{0:0,0}", ((decimal)rowItem["UVRConvenioSolicitado"]));
                        if (!Convert.IsDBNull(rowItem["ValorConvenioSolicitado"]))
                            txtValorConvenioSolicitado.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorConvenioSolicitado"]));

                        this.FillList(ddlMotivo, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(ddlEstadoServicio.SelectedValue));
                        if (Request.QueryString["IdSolicitudCopia"] == null || Request.QueryString["IdSolicitudCopia"] == string.Empty)
                            ddlMotivo.SelectedValue = ((short)rowItem["IdSolicitudMotivo"]).ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        /// <summary>
        /// Método, carga los datos en el DataList de tipos de servicios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtlTipoServicio_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Carga los listados				
                    DropDownList ddlTipoServicio = (DropDownList)e.Item.FindControl("ddlTipoServicio");
                    DropDownList ddlAdicionarServicios = (DropDownList)e.Item.FindControl("ddlAdicionarServicios");
                    DropDownList ddlClaseAtencion = (DropDownList)e.Item.FindControl("ddlClaseAtencion");
                    DropDownList ddlTipoAtencion = (DropDownList)e.Item.FindControl("ddlTipoAtencion");
                    DropDownList ddlContingencia = (DropDownList)e.Item.FindControl("ddlContingencia");
                    DropDownList ddlUnidadAprueba = (DropDownList)e.Item.FindControl("ddlUnidadAprueba");
                    TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnostico WC_AdicionarDiagnostico1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarDiagnostico)e.Item.FindControl("WC_AdicionarDiagnostico1");
                    TPA.interfaz_empleado.WebControls.WC_AdicionarSolicitante WC_AdicionarSolicitante1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarSolicitante)e.Item.FindControl("WC_AdicionarSolicitante1");
                    TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador WC_AdicionarPrestador1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador)ddlTipoServicio.Parent.FindControl("WC_AdicionarPrestador1");

                    WC_AdicionarPrestador1.LoadControls("0");
                    WC_AdicionarDiagnostico1.LoadControls();
                    WC_AdicionarSolicitante1.LoadControls();

                    for (int k = 1; k < 16; k++)
                        ddlAdicionarServicios.Items.Add(new ListItem(k.ToString(), k.ToString()));

                    this.FillListUser("TipoServicios", "TipoServicio", Convert.ToInt32(Session["IdUser"]), Session["SICAU"], Convert.ToInt32(Session["Company"]), ddlTipoServicio, "--Tipo de Servicio--");
                    if (ddlTipoServicio.Items.Count == 1)
                    {
                        ddlTipoServicio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Tipo de Servicio--", "0"));
                        ddlTipoServicio.SelectedIndex = 0;
                    }
                    this.FillList("ClaseAtenciones", "ClaseAtencion", ddlClaseAtencion, "--Clase Atención--");
                    ddlClaseAtencion.SelectedValue = "1";
                    this.FillList("TipoAtenciones", "TipoAtencion", ddlTipoAtencion, "--Tipo Atención--");
                    ddlTipoAtencion.SelectedValue = "1";
                    this.FillList("Contingencias", "Contingencia", ddlContingencia, "--Contingencia--");
                    ddlContingencia.SelectedValue = "1";

                    //Adicionar evento para el calendario
                    TextBox txtFechaFactura = (TextBox)e.Item.FindControl("txtFechaFactura");
                    txtFechaFactura.Attributes.Add("ReadOnly", "ReadOnly"); //PETF 14/01/10 ReadOnly

                    System.Web.UI.WebControls.Image btnFechaInicio = (System.Web.UI.WebControls.Image)e.Item.FindControl("btnFechaFactura");
                    btnFechaInicio.Attributes.Add("OnClick", "javascript:MostrarCalendario(Form1." + txtFechaFactura.ClientID + ",Form1." + txtFechaFactura.ClientID + ",'dd/mm/yyyy');");

                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                    //Cargar los datos si viene el id de la solicitud
                    if ((Request.QueryString["IdSolicitud"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudTipoServicio"]) && (long)rowItem["IdSolicitudTipoServicio"] != 0)
                        || (Request.QueryString["IdSolicitudCopia"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudTipoServicio"]) && (long)rowItem["IdSolicitudTipoServicio"] != 0))
                    {
                        ImageButton imbBorrar = (ImageButton)e.Item.FindControl("imbBorrar");
                        TextBox txtIdSolicitudTipoServicio = (TextBox)e.Item.FindControl("txtIdSolicitudTipoServicio");
                        TextBox txtConsecutivo = (TextBox)e.Item.FindControl("txtConsecutivo");
                        TextBox txtImpresiones = (TextBox)e.Item.FindControl("txtImpresiones");
                        TextBox txtConsecutivoNombre = (TextBox)e.Item.FindControl("txtConsecutivoNombre");
                        TextBox txtIdSolicitudEstado = (TextBox)e.Item.FindControl("txtIdSolicitudEstado");
                        TextBox txtValorFactura = (TextBox)e.Item.FindControl("txtValorFactura");
                        TextBox txtNumFactura = (TextBox)e.Item.FindControl("txtNumFactura");
                        TextBox txtCuentaCobro = (TextBox)e.Item.FindControl("txtCuentaCobro");
                        TextBox txtComentariosTipoServicio = (TextBox)e.Item.FindControl("txtComentariosTipoServicio");

                        ddlTipoServicio.Enabled = false;
                        imbBorrar.Visible = false;

                        if (!Convert.IsDBNull(rowItem["IdTipoServicio"]))
                            ddlTipoServicio.SelectedValue = ((int)rowItem["IdTipoServicio"]).ToString();
                        if (!Convert.IsDBNull(rowItem["IdTipoAtencion"]))
                            ddlTipoAtencion.SelectedValue = ((short)rowItem["IdTipoAtencion"]).ToString();
                        if (!Convert.IsDBNull(rowItem["IdClaseAtencion"]))
                            ddlClaseAtencion.SelectedValue = ((short)rowItem["IdClaseAtencion"]).ToString();
                        if (!Convert.IsDBNull(rowItem["IdContingencia"]))
                            ddlContingencia.SelectedValue = ((short)rowItem["IdContingencia"]).ToString();
                        if (!Convert.IsDBNull(rowItem["UnidadAprobacion"]))
                            ddlUnidadAprueba.SelectedValue = (string)rowItem["UnidadAprobacion"];
                        if (!Convert.IsDBNull(rowItem["Comentarios"]))
                            txtComentariosTipoServicio.Text = rowItem["Comentarios"].ToString();
                        if (!Convert.IsDBNull(rowItem["IdPrestador"]))
                            WC_AdicionarSolicitante1.SetSolicitante(Convert.ToInt32(rowItem["IdPrestador"]));
                        txtValorFactura.Text = string.Format("{0:0,0}", ((decimal)rowItem["ValorFactura"]));
                        txtNumFactura.Text = ((string)rowItem["NumeroFactura"]);
                        if (!Convert.IsDBNull(rowItem["NumeroCuentaCobro"]))
                            txtCuentaCobro.Text = ((string)rowItem["NumeroCuentaCobro"]);
                        txtFechaFactura.Text = ((DateTime)rowItem["FechaFactura"]).ToShortDateString();
                        txtIdSolicitudTipoServicio.Text = ((long)rowItem["IdSolicitudTipoServicio"]).ToString();
                        if (Request.QueryString["IdSolicitudCopia"] == null || Request.QueryString["IdSolicitudCopia"] == string.Empty)
                        {
                            txtConsecutivo.Text = ((long)rowItem["Consecutivo"]).ToString();
                            txtConsecutivoNombre.Text = rowItem["ConsecutivoNombre"].ToString();
                            txtIdSolicitudEstado.Text = rowItem["IdSolicitudEstado"].ToString();
                            if (!Convert.IsDBNull(rowItem["Impresiones"]))
                                txtImpresiones.Text = ((short)rowItem["Impresiones"]).ToString();

                            if (!Convert.IsDBNull(rowItem["DescripcionDiagnosticos"]) && rowItem["DescripcionDiagnosticos"].ToString() != string.Empty)
                            {
                                Label lblDiagnosticos = (Label)e.Item.FindControl("lblDiagnosticos");
                                HtmlTable tblInternet = (HtmlTable)e.Item.FindControl("tblInternet");
                                tblInternet.Style["display"] = "";
                                lblDiagnosticos.Text = rowItem["DescripcionDiagnosticos"].ToString();
                            }
                            if (!Convert.IsDBNull(rowItem["DetalleServicios"]) && rowItem["DetalleServicios"].ToString() != string.Empty)
                            {
                                Label lblServicios = (Label)e.Item.FindControl("lblServicios");
                                HtmlTable tblInternet = (HtmlTable)e.Item.FindControl("tblInternet");
                                tblInternet.Style["display"] = "";
                                lblServicios.Text = rowItem["DetalleServicios"].ToString();
                            }
                        }

                        //Cargar diagnosticos
                        LoadControlDiagnosticos(WC_AdicionarDiagnostico1, ((long)rowItem["IdSolicitudTipoServicio"]));
                        LoadLabelProveedores(WC_AdicionarPrestador1, ((long)rowItem["IdSolicitudTipoServicio"]));

                        //Colocar inhabilitados los controles para estados liquidados y anulados y no copias
                        if ((Convert.ToInt16((rowItem["IdSolicitudEstado"])) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) || Convert.ToInt16((rowItem["IdSolicitudEstado"])) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                            && (Request.QueryString["IdSolicitudCopia"] == null || Request.QueryString["IdSolicitudCopia"] == string.Empty))
                        {
                            DisableRecursive(e.Item);
                        }

                        //Cargar los servicio
                        DataGrid dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");
                        SolicitudServicio objSolicitudServicio = new SolicitudServicio();
                        if (Request.QueryString["IdSolicitud"] != null)
                            objSolicitudServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                        if (Request.QueryString["IdSolicitudCopia"] != null)
                            objSolicitudServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);
                        objSolicitudServicio.IdSolicitudTipoServicio = ((long)rowItem["IdSolicitudTipoServicio"]);
                        dtgProductoServicio.DataSource = objSolicitudServicio.ConsultSolicitudServicio();
                        dtgProductoServicio.DataBind();

                    }
                    //Si no es edición, se carga la grilla con la cantidad de items adicionados	
                    else
                    {

                        object[] lstObjetos = new object[0];
                        int cantidadSuma = 1;
                        DataGrid dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");
                        DataTable dtProductos = new DataTable();

                        if (dtgProductoServicio.Items.Count == 0)
                            cantidadSuma = 5;

                        for (int i = 0; i < dtgProductoServicio.Items.Count + cantidadSuma; i++)
                        {
                            dtProductos.Rows.Add(lstObjetos);
                        }

                        dtgProductoServicio.DataSource = dtProductos.DefaultView;
                        dtgProductoServicio.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Método, Ejecuta la adición en la grilla o la eliminación en la base de datos de tipos de servicio
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dtlTipoServicio_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Adicionar")
                {
                    object[] lstObjetos = new object[0];
                    int i = 0;
                    DataTable dtProductos;
                    TextBox txtIdSolicitudTipoServicio = (TextBox)e.Item.FindControl("txtIdSolicitudTipoServicio");
                    DataGrid dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");

                    if ((Request.QueryString["IdSolicitud"] != null || Request.QueryString["IdSolicitudCopia"] != null) && txtIdSolicitudTipoServicio.Text != string.Empty)
                    {
                        SolicitudServicio objServicio = new SolicitudServicio();
                        if (Request.QueryString["IdSolicitud"] != null)
                            objServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                        if (Request.QueryString["IdSolicitudCopia"] != null)
                            objServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);

                        objServicio.IdSolicitudTipoServicio = Convert.ToInt64(txtIdSolicitudTipoServicio.Text);
                        dtProductos = objServicio.ConsultSolicitudServicio().Tables[0];

                        for (i = dtProductos.Rows.Count; i < dtgProductoServicio.Items.Count + 1; i++)
                        {
                            dtProductos.Rows.Add(lstObjetos);
                        }

                    }
                    else
                    {
                        int cantidadSuma = 1;
                        dtProductos = new DataTable();

                        if (dtgProductoServicio.Items.Count == 0)
                            cantidadSuma = 5;

                        for (i = 0; i < dtgProductoServicio.Items.Count + cantidadSuma; i++)
                        {
                            dtProductos.Rows.Add(lstObjetos);
                        }
                    }

                    dtgProductoServicio.DataSource = dtProductos;
                    dtgProductoServicio.DataBind();
                }
                if (e.CommandName == "Eliminar")
                {
                    TextBox txtIdSolicitudTipoServicio = (TextBox)e.Item.FindControl("txtIdSolicitudTipoServicio");
                    DropDownList ddlTipoServicio = (DropDownList)e.Item.FindControl("ddlTipoServicio");

                    if (txtIdSolicitudTipoServicio.Text.Trim() != string.Empty && Request.QueryString["IdSolicitud"] != null)
                    {
                        this.RegisterLog(Log.EnumActionsLog.EliminarSolicitudTipoServicio, Convert.ToInt64(Request.QueryString["IdSolicitud"]), " Tipo de servicio eliminado " + ddlTipoServicio.SelectedItem.Text + " de solicitud " + Request.QueryString["IdSolicitud"]);
                        this.DeleteSolicitudTipoServicio(Convert.ToInt64(txtIdSolicitudTipoServicio.Text), Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    }
                }
                if (e.CommandName == "VerHistorico")
                {

                    DropDownList ddlTipoServicio = (DropDownList)e.Item.FindControl("ddlTipoServicio");

                    if (this.ddlUsuario.SelectedValue == "0")
                        this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&idTipoServicio=" + ddlTipoServicio.SelectedValue, 850, 750, e.Item.ItemIndex);
                    else
                        this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&idTipoServicio=" + ddlTipoServicio.SelectedValue, 850, 750, e.Item.ItemIndex);

                }
                this.ResizePage("Command" + e.Item.ItemIndex.ToString());
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Método, Ejecuta la eliminación en la base de datos del servicio
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void dtgProductoServicio_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    DataGrid dtgProductoServicio = (DataGrid)source;
                    TextBox txtIdSolicitudServicio = (TextBox)e.Item.FindControl("txtIdSolicitudServicio");
                    TextBox txtIdSolicitudTipoServicio = (TextBox)dtgProductoServicio.Parent.FindControl("txtIdSolicitudTipoServicio");
                    TextBox txtProductoServicio = (TextBox)e.Item.FindControl("txtProductoServicio");

                    if (txtIdSolicitudTipoServicio.Text.Trim() != string.Empty && txtIdSolicitudServicio.Text.Trim() != string.Empty && Request.QueryString["IdSolicitud"] != null)
                    {
                        this.RegisterLog(Log.EnumActionsLog.EliminarServicioSolicitud, Convert.ToInt64(Request.QueryString["IdSolicitud"]), " Servicio eliminado " + txtProductoServicio.Text + " de solicitud " + Request.QueryString["IdSolicitud"]);
                        this.DeleteSolicitudServicio(Convert.ToInt64(txtIdSolicitudServicio.Text), Convert.ToInt64(txtIdSolicitudTipoServicio.Text), Convert.ToInt64(Request.QueryString["IdSolicitud"]), dtgProductoServicio);

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, realiza el llamado a la adición o edición de toda la solicitud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    this.UpdateSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarSolictudReembolso, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " Estado Actual " + this.ddlEstadoSoli.SelectedItem.Text);
                    Response.Redirect("AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&popup=" + Request.QueryString["popup"] + "&reload=" + Request.QueryString["reload"]);
                }
                else
                {
                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolictudReembolso, idSolicitud, "Ingresar solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudreembolsoresumen.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"]);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, Retorna a la página anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("LIS_solicitudgastos.aspx?employee_id=" + Request.QueryString["employee_id"]);
        }

        /// <summary>
        /// Evento, realiza la anulación de la orden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnular_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Request.QueryString["IdSolicitudCopia"] != null && Request.QueryString["IdSolicitudCopia"] != string.Empty)
                {
                    if (this.ddlMotivosAnulacion.SelectedValue == "0")
                        throw new Exception("Debe ingresar las Observaciones de Anulación");

                    short idEstado = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado);
                    short idMotivo = Convert.ToInt16(Solicitud.EnumMotivosEstadoSolicitud.Anulado);
                    this.UpdateEstadoSolicitud(idEstado, idMotivo);
                    this.RegisterLog(Log.EnumActionsLog.ModificarEstadoSolicitud, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación estado solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " A Estado Anulado");

                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolictudAutorizacion, idSolicitud, "Ingreso solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudautorizacionresumen.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&liquidacionConfirmacion=" + Request.QueryString["liquidacionConfirmacion"]);

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        /// <summary>
        /// Evento, Abre la venta que despliega el histórico de servicios del empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerHistorico_Click(object sender, System.EventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 800, 700);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue, 800, 700);
        }

        /// <summary>
        /// Realiza el guardado temporal de la orden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    this.UpdateSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarSolictudAutorizacion, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " Estado Actual " + this.ddlEstadoSoli.SelectedItem.Text);
                    Response.Redirect("AE_solicitudreembolso.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"]);
                }
                else
                {
                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolictudAutorizacion, idSolicitud, "Modificación solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudreembolso.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"]);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Realiza el guardado temporal de la orden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkGuardar_Click(object sender, System.EventArgs e)
        {

            try
            {
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    this.UpdateSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarSolictudAutorizacion, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " Estado Actual " + this.ddlEstadoSoli.SelectedItem.Text);
                    Response.Redirect("AE_solicitudreembolso.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"]);
                }
                else
                {
                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolictudAutorizacion, idSolicitud, "Modificación solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudreembolso.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"]);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        /// <summary>
        /// Evento, Abre la venta que despliega el histórico de servicios del empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbVerHistorial_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 800, 700);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue, 800, 700);
        }

        /// <summary>
        /// Modificar a un paciente de otro grupo familiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCambiarPaciente_Click(object sender, System.EventArgs e)
        {
            this.txtOtroEmpleado.Text = "";
            this.txtOtroBeneficiario.Text = "";
            this.OpenWindow("LIS_empleado.aspx?cambioPaciente=1", 950, 1000);
        }

        /// <summary>
        /// Carga el nuevo listado del grupo familiar del paciente seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCargarNuevoPaciente_Click(object sender, System.EventArgs e)
        {
            EmpresaParentescos objEmpresaParentescos = new EmpresaParentescos();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            DataTable dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];
            this.ddlUsuario.Items.Clear();
            this.ddlUsuario.Items.Add(new ListItem("--Paciente--", "-1"));

            foreach (DataRow row in dtParentescos.Rows)
            {
                if (row["IdParentesco"].ToString() == "0")
                {
                    objEmpleado.Id_empleado = Convert.ToInt32(this.txtOtroEmpleado.Text);
                    objEmpleado.GetSIC_EMPLEADO();
                    this.ddlUsuario.Items.Add(new ListItem("TITU-" + objEmpleado.Nombre_completo, "0"));
                }
                else
                {
                    objBeneficiario.Id_empleado = Convert.ToInt32(this.txtOtroEmpleado.Text);
                    objBeneficiario.IdParentesco = Convert.ToInt32(row["IdParentesco"].ToString());
                    objBeneficiario.Opcion = 1;
                    DataTable dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                    foreach (DataRow rowBeneficiario in dtBeneficiarios.Rows)
                    {
                        this.ddlUsuario.Items.Add(new ListItem(rowBeneficiario["Parentesco"].ToString() + "-" + rowBeneficiario["nombre"].ToString(), rowBeneficiario["beneficiario_id"].ToString()));
                    }
                }
            }

            if (this.txtOtroBeneficiario.Text != string.Empty)
            {
                this.ddlUsuario.SelectedValue = this.txtOtroBeneficiario.Text;
            }
            else
            {
                this.ddlUsuario.SelectedValue = "0";
            }
        }

        /// <summary>
        /// Evento, adiciona el número de tipos de servicio seleccionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlAdicionarTipoServicio_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                addFacturaDataList(true, Convert.ToInt32(this.ddlAdicionarTipoServicio.SelectedValue) - 1);
                this.ResizePage(this.dtlTipoServicio.Items.Count.ToString());
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }

        }

        #endregion

    }
}
