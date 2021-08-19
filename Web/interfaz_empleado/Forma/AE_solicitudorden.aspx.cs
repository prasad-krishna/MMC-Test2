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
using System.Configuration;

namespace TPA.interfaz_empleado.forma
{

    /// <summary>
    /// Inserta o modifica una solicitud de una orden médica
    /// </summary>
    public partial class AE_solicitudorden : PB_PaginaBase
    {
        #region Atributos
        bool _esNutriologo, _esMedico, _esTodo = false;
        string idConsulta = "";
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
                //Inicio - Emilio Bueno 20/11/2012
                //Se cargan los objetos con el tiempo límite de sesión
                hdnTimeout.Value = HttpContext.Current.Session.Timeout.ToString();
                hdnSesion.Value = HttpContext.Current.Session.Timeout.ToString();
                //Se cargan los objetos con los valores del Web.Config
                hdnTiempoMostrarAlerta.Value = ConfigurationManager.AppSettings["TiempoMostrarAlerta"].ToString();
                hdnTiempoGuardarTemporal.Value = ConfigurationManager.AppSettings["TiempoGuardarTemporal"].ToString();
                //Fin - Emilio Bueno 20/11/2012

                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10

                if (!this.Page.IsPostBack)
                {
                    Response.Write("<script>window.parent.scrollTo(0,0);</script>");
                    this.LoadControls();

                    if (Request.QueryString["IdSolicitud"] != null)
                    {
                        if (Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != "0")
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
                            loadDataList();
                            this.ResizePage(this.dtlTipoServicio.Items.Count.ToString());
                        }
                    }
                }

                //idConsulta = Session["idTipoConsulta"].ToString();

                //GAMM 
                if (Session["idTipoConsulta"] != null)
                {
                    idConsulta = Session["idTipoConsulta"].ToString();
                }
                else {
                    Session["idTipoConsulta"] = "";
                }
                

                string[] medico = new string[] { "1", "2", "7", "8", "9", "10", "11", "12", "13", "14", "17" };
                ArrayList ArrayMedico = new ArrayList();
                ArrayMedico.AddRange(medico);

                string[] nutriologo = new string[] { "15", "16" };
                ArrayList ArrayNutriologo = new ArrayList();
                ArrayNutriologo.AddRange(nutriologo);

                string[] todo = new string[] { "3", "4" };
                ArrayList ArrayTodo = new ArrayList();
                ArrayTodo.AddRange(todo);

                if (ArrayMedico.Contains(idConsulta))
                    _esMedico = true;
                if (ArrayNutriologo.Contains(idConsulta))
                    _esNutriologo = true;
                if (ArrayTodo.Contains(idConsulta))
                    _esTodo = true;

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
            //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 
            txtDias.Attributes.Add("ReadOnly", "ReadOnly");
            //Inicio PETF 14/01/10
            txtInicioIncapacidad.Attributes.Add("ReadOnly", "ReadOnly");
            txtFinIncapacidad.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10
            //txtProductoServicio
            //Fin MAHG 12/01/10

            
            DataTable dtParentescos;
            EmpresaParentescos objEmpresaParentescos = new EmpresaParentescos();
            EmpresaDatos objEmpresaDatos = new EmpresaDatos();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();

            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();
            ViewState["IdEstadoDefecto"] = objEmpresaDatos.IdSolicitudEstadoDefecto;
            ViewState["FechaInicio"] = DateTime.Now;

            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];

            this.FillListActivos("PlanesSolicitud", "PlanSolicitud", Convert.ToInt32(Session["Company"]), this.ddlPlanesSolicitud, "--Plan--");

            if (this.ddlPlanesSolicitud.Items.Count > 1)
            {
                this.ddlPlanesSolicitud.Visible = true;
                this.lblPlanesSolicitud.Visible = true;
            }

            for (int i = 1; i < 11; i++)
                this.ddlAdicionarTipoServicio.Items.Add(new ListItem(i.ToString(), i.ToString()));

            this.WC_SeleccionarDiagnostico1.LoadControls();

            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objConsulta.GetConsulta();
            ViewState["IdTipoConsulta"] = objConsulta.IdTipoConsulta;
            ViewState["Finalizada"] = objConsulta.Finalizada;
            this.txtInicioIncapacidad.Text = DateTime.Now.ToShortDateString();
            if(objConsulta.Beneficiario_id != 0)
                ViewState["beneficiario_id"] = Convert.ToInt32(objConsulta.Beneficiario_id);

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
            long idSolicitud;
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
            objPresupuestoEmpresa.IdTipoProceso = Convert.ToInt16(PresupuestosIndividuo.EnumTipoProceso.Ordenes);
            objPresupuestoEmpresa.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            objPresupuestoEmpresa.GetPresupuestosActual(DateTime.Now);
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
            if (ViewState["beneficiario_id"] != null && ViewState["beneficiario_id"] != string.Empty)
                objPresupuestoIndividuo.Beneficiario_id = Convert.ToInt32(ViewState["beneficiario_id"]);
            objPresupuestoIndividuo.IdTipoProceso = Convert.ToInt16(PresupuestosIndividuo.EnumTipoProceso.Ordenes);
            objPresupuestoIndividuo.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            objPresupuestoIndividuo.GetPresupuestosIndividuoActual(DateTime.Now);
            if (Convert.IsDBNull(objPresupuestoIndividuo.IdPresupuestoIndividuo) || objPresupuestoIndividuo.IdPresupuestoIndividuo == 0)
            {
                if (!Convert.IsDBNull(objPresupuestoEmpresa.IdTipoPresupuestoTodos))
                {
                    objPresupuestoIndividuo.InsertPresupuestosIndividuoAutomatico(objPresupuestoEmpresa.IdPresupuestoEmpresa);
                    objPresupuestoIndividuo.GetPresupuestosIndividuoActual(DateTime.Now);
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
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objConsulta.GetConsulta();     
            objSolicitud.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objSolicitud.Empresa_id = Convert.ToInt32(Session["Company"]);
            objSolicitud.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
            objSolicitud.Beneficiario_id = Convert.ToInt32(objConsulta.Beneficiario_id);
            objSolicitud.IdTipoSolicitud = Convert.ToInt16(Solicitud.EnumTipoSolicitud.Orden);
            objSolicitud.FechaInicioCreacion = (DateTime)(ViewState["FechaInicio"]);
            objSolicitud.Fecha = objConsulta.FechaCreacion;
            objSolicitud.SolicitudEmpleado = false;
            objSolicitud.IdPlanSolicitud = Convert.ToInt32(this.ddlPlanesSolicitud.SelectedValue);
            objSolicitud.IdPrestador = objConsulta.IdPrestador;
            objSolicitud.Observaciones = this.txtRecomendaciones.Text.Trim();
            objSolicitud.AnotacionesFijas = new ArrayList();
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

            this.LoadObjectIncapacidad(objSolicitud);

        }

        /// <summary>
        /// Método, Carga los objetos de tipos de servicios ingresados en la forma
        /// </summary>
        /// <param name="objSolicitud"></param>
        public void LoadSolicitudTipoServicios(Solicitud objSolicitud)
        {

            SolicitudTipoServicio objTipoServicio;
            ArrayList arrTiposServicio = new ArrayList();


            foreach (DataListItem item in this.dtlTipoServicio.Items)
            {
                DropDownList ddlTipoServicio = (DropDownList)item.FindControl("ddlTipoServicio");

                if (ddlTipoServicio.SelectedValue != "0")
                {
                    ArrayList arrProveedoresCarga = this.LoadProveedores(item, Convert.ToInt32(ddlTipoServicio.SelectedValue));

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
                            DropDownList ddlEspecialidad = (DropDownList)item.FindControl("ddlEspecialidad");
                            DataGrid dtgProductoServicio = (DataGrid)item.FindControl("dtgProductoServicio");
                            DropDownList ddlUnidadAprueba = (DropDownList)item.FindControl("ddlUnidadAprueba");

                            if (Request.QueryString["IdSolicitudCopia"] == null)
                            {
                                if (txtConsecutivoNombre.Text != "")
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
                            objTipoServicio.FechaFactura = new DateTime(1900, 1, 1);
                            objTipoServicio.Comentarios = txtComentariosTipoServicio.Text;
                            objTipoServicio.IdEspecialidad = Convert.ToInt32(ddlEspecialidad.SelectedValue);
                            if (txtIdSolicitudTipoServicio.Text != string.Empty)
                                objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64(txtIdSolicitudTipoServicio.Text);
                            ViewState["TotalValorFactura"] = (decimal)ViewState["TotalValorFactura"] + objTipoServicio.ValorFactura;

                            this.LoadSolicitudServicios(objTipoServicio, dtgProductoServicio, Convert.ToInt32(ddlTipoServicio.SelectedValue));
                            this.LoadDiagnosticos(objTipoServicio, item);
                            ArrayList arrProveedores = new ArrayList();
                            arrProveedores.Add(objProveedores);
                            objTipoServicio.SolicitudTipoServicioProveedores = (ArrayList)arrProveedores.Clone();
                            objTipoServicio.IdTipoConsecutivo = Convert.ToInt16(SolicitudTipoServicio.EnumTipoConsecutivos.Nuevo);

                            if (objTipoServicio.SolicitudServicios.Count > 0)
                                arrTiposServicio.Add(objTipoServicio);
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
        public void LoadSolicitudServicios(SolicitudTipoServicio p_objTipoServicio, DataGrid p_dtgProductoServicio, int p_idTipoServicio)
        {
            SolicitudServicio objServicio;
            ArrayList arrServicios = new ArrayList();

            foreach (DataGridItem item in p_dtgProductoServicio.Items)
            {
                TextBox txtProductoServicio = (TextBox)item.FindControl("txtProductoServicio");

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
                    TextBox txtViaAdministracion = (TextBox)item.FindControl("txtViaAdministracion");
                    TextBox txtDuracion = (TextBox)item.FindControl("txtDuracion");
                    
                    TextBox txtFechaPrestacion = (TextBox)item.FindControl("txtFechaPrestacion");
                    txtFechaPrestacion.Attributes.Add("ReadOnly", "ReadOnly"); //PETF 15/01/10 ReadOnly

                    CheckBox chkPrestado = (CheckBox)item.FindControl("chkPrestado");

                    if (txtIdServicioProducto.Text.Trim() != string.Empty && (ddlEstadoServicio.SelectedValue == "0" || ddlMotivo.SelectedValue == "0"))
                    {
                        throw new Exception("Debe ingresar por cada servicio el estado y el motivo");
                    }
                    /// <summary>
                    /// Cambio para validación cuando se realiza cambio de motivo
                    /// Autor: Adriana Diazgranados
                    /// Fecha: 19 de Junio 2012
                    /// </summary>
                    if (txtComentarioServicioProducto.Text.EndsWith(" por: "))
                    {
                        throw new Exception("Debe registrar el motivo por el cual modifico el medicamento en el campo comentarios");
                    }
                    objServicio.IdSolicitudEstado = Convert.ToInt16(ddlEstadoServicio.SelectedValue);
                    objServicio.IdSolicitudMotivo = Convert.ToInt16(ddlMotivo.SelectedValue);
                    objServicio.Comentarios = HttpUtility.HtmlDecode(txtComentarioServicioProducto.Text.Trim());

                    if (Request.QueryString["liquidacion"] != null && Request.QueryString["liquidacion"] != string.Empty && Convert.ToInt64(Request.QueryString["liquidacion"]) == p_objTipoServicio.IdSolicitudTipoServicio)
                    {
                        if (txtFechaPrestacion.Text.Trim() == string.Empty)
                            throw new Exception("Debe seleccionar la fecha de prestación para todos los servicios");
                    }
                    if (txtFechaPrestacion.Text.Trim() != string.Empty)
                        objServicio.FechaPrestacion = Convert.ToDateTime(txtFechaPrestacion.Text);
                    else
                        objServicio.FechaPrestacion = new DateTime(1900, 1, 1);
                    objServicio.Prestado = chkPrestado.Checked;


                    if (txtDosis.Visible)
                        objServicio.Dosis = HttpUtility.HtmlDecode(txtDosis.Text.Trim());

                    if (txtViaAdministracion.Visible)
                        objServicio.ViaAdministracion = HttpUtility.HtmlDecode(txtViaAdministracion.Text.Trim());

                    if (txtDuracion.Visible)
                        objServicio.Duracion = HttpUtility.HtmlDecode(txtDuracion.Text.Trim());

                    if (p_idTipoServicio == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos) || p_idTipoServicio == Convert.ToInt32(Servicios.EnumTiposServicio.Vacunas))
                        objServicio.IdMedicamento = Convert.ToInt32(txtIdServicioProducto.Text);

                    else
                        objServicio.IdServicio = Convert.ToInt32(txtIdServicioProducto.Text);

                    if (txtCantidad.Text.Trim() != string.Empty)
                        objServicio.Cantidad = HttpUtility.HtmlDecode(txtCantidad.Text);

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

                    if (txtValorAprobado.Text.Trim() != string.Empty)
                    {
                        objServicio.ValorAprobado = Convert.ToDecimal(txtValorAprobado.Text);
                        ViewState["TotalValorAprobado"] = (decimal)ViewState["TotalValorAprobado"] + objServicio.ValorAprobado;
                    }
                    if (txtDescuento.Text.Trim() != string.Empty)
                    {
                        objServicio.Descuento = Convert.ToDecimal(txtDescuento.Text);
                    }
                    if (txtUVR.Text.Trim() != string.Empty)
                    {
                        objServicio.UVR = Convert.ToDecimal(txtUVR.Text);
                    }
                    if (txtUVRSolicitado.Text.Trim() != string.Empty)
                    {
                        objServicio.UVRConvenioSolicitado = Convert.ToDecimal(txtUVRSolicitado.Text);
                    }
                    if (txtValorConvenioSolicitado.Text.Trim() != string.Empty)
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
            objSolicitud.ObservacionAnulacion = this.txtAnulacion.Text.Trim();
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
                        objTipoServicioCarga.ObservacionAnulacion = this.txtAnulacion.Text;

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
                    objTipoServicioCarga.ObservacionAnulacion = this.txtAnulacion.Text;
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
            TPA.interfaz_empleado.WebControls.WC_SeleccionarDiagnostico WC_SeleccionarDiagnostico2 = (TPA.interfaz_empleado.WebControls.WC_SeleccionarDiagnostico)p_item.FindControl("WC_SeleccionarDiagnostico2");
            WC_SeleccionarDiagnostico2.LoadDiagnosticos(p_objTipoServicio);
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
            return WC_AdicionarPrestador1.LoadProveedores(p_tipoServicio, Convert.ToInt16(Solicitud.EnumTipoSolicitud.Orden));
        }

        /// <summary>
        /// Método, Carga los controles del listado de diagnósticos del tipo de solicitud
        /// </summary>
        /// <param name="WC_AdicionarDiagnostico1"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadControlDiagnosticos(TPA.interfaz_empleado.WebControls.WC_SeleccionarDiagnostico WC_SeleccionarDiagnostico2, long p_idSolicitudTipoServicio)
        {
            WC_SeleccionarDiagnostico2.LoadControlDiagnosticos(p_idSolicitudTipoServicio);
        }

        /// <summary>
        /// Método, Carga los controles del listado de proveedores del tipo de solicitud
        /// </summary>
        /// <param name="WC_AdicionarPrestador1"></param>
        /// <param name="p_idSolicitudTipoServicio"></param>
        public void LoadControlProveedores(TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador WC_AdicionarPrestador1, long p_idSolicitudTipoServicio)
        {
            WC_AdicionarPrestador1.LoadControlProveedores(p_idSolicitudTipoServicio);

        }


        /// <summary>
        /// Método, Carga los datos de la solicitud en la forma
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void LoadFormSolicitud(long p_idSolicitud)
        {
            if (Request.QueryString["adicion"] != null && Request.QueryString["adicion"] != string.Empty)
            {
                this.btnAnterior.Visible = false;
                this.btnGuardar.Text = "Aceptar";
            }

            else
                if (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty)
                    this.btnAnterior.Visible = false;
                else
                    this.btnAnterior.Visible = true;
            

            Solicitud objSolicitud = new Solicitud();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();
            ViewState["IdUserCreacion"] = objSolicitud.IdUserCreacion;

            if ((objSolicitud.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) || objSolicitud.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado)) && Request.QueryString["IdSolicitudCopia"] == null)
            {
                Response.Redirect("AE_solicitudordenresumen.aspx?IdSolicitud=" + p_idSolicitud + "&employee_id=" + Convert.ToInt32(Request.QueryString["employee_id"]) + "&IdConsulta=" + Request.QueryString["IdConsulta"]);
            }
            else
            {
                this.txtRecomendaciones.Text = objSolicitud.Observaciones;
                this.ddlPlanesSolicitud.SelectedValue = objSolicitud.IdPlanSolicitud.ToString();
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

                if (Request.QueryString["Anular"] != null && Request.QueryString["Anular"] != string.Empty)
                {
                    //Cargar, desplegar y seleccionar estado				
                    this.btnAnular.Visible = true;
                    this.txtAnulacion.Visible = true;
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
                {
                    this.tblDatosSolicitud.Style["display"] = "";
                    this.imbCopiar.Visible = false;
                    this.lnkCopiar.Visible = false;
                }
                else
                {
                    this.imbCopiar.Visible = true;
                    this.lnkCopiar.Visible = true;
                }

                if (Request.QueryString["IdSolicitud"] != null)
                    this.tblDatosSolicitud.Style["display"] = "";

                //Cargar los tipos de servicio
                objTipoServicio.IdSolicitud = p_idSolicitud;
                //Si viene el id tipo servicio para copiar, se filtra por ese tipo de servicio
                if (Request.QueryString["IdSolicituTipoServicioCopia"] != null && Request.QueryString["IdSolicituTipoServicioCopia"] != string.Empty)
                    objTipoServicio.IdSolicitudTipoServicio = Convert.ToInt64("IdSolicituTipoServicioCopia");

                this.dtlTipoServicio.DataSource = objTipoServicio.ConsultSolicitudTipoServicio().Tables[0];
                this.dtlTipoServicio.DataBind();

                LoadIncapacidad(p_idSolicitud);
            }
        }

        /// <summary>
        /// Método, carga la información de la incapacidad
        /// </summary>
        /// <param name="p_idSolicitud"></param>
        public void LoadIncapacidad(long p_idSolicitud)
        {
            Incapacidad objIncapacidad = new Incapacidad();
            objIncapacidad.IdSolicitud = p_idSolicitud;
            objIncapacidad.GetIncapacidad();

            if (objIncapacidad.IdIncapacidad != 0)
            {
                this.txtInicioIncapacidad.Text = objIncapacidad.FechaInicio.ToShortDateString();
                this.txtFinIncapacidad.Text = objIncapacidad.FechaFin.ToShortDateString();
                this.txtDias.Text = (objIncapacidad.FechaFin.Subtract(objIncapacidad.FechaInicio).Days + 1).ToString();
                this.chkContinuacion.Checked = objIncapacidad.Continuacion;
                this.chkTranscripcion.Checked = objIncapacidad.Transcripcion;
                this.txtObservacionesIncapacidad.Text = objIncapacidad.Observaciones;
                if (Request.QueryString["IdSolicitudCopia"] == null)
                {
                    this.WC_SeleccionarDiagnostico1.LoadControlDiagnosticosIncapacidad(objIncapacidad.IdIncapacidad);
                }
                ViewState["IdIncapacidad"] = objIncapacidad.IdIncapacidad;
            }

        }

        /// <summary>
        /// Método, carga el objeto incapacidad
        /// </summary>
        public void LoadObjectIncapacidad(Solicitud objSolicitud)
        {
            if (this.txtFinIncapacidad.Text != string.Empty)
            {
                if (this.txtFinIncapacidad.Text == string.Empty || this.txtInicioIncapacidad.Text == string.Empty)
                    throw new Exception("Debe ingresar la fecha de inicio y de fin de la incapacidad");
                Incapacidad objIncapacidad = new Incapacidad();

                try
                {
                    objIncapacidad.FechaInicio = Convert.ToDateTime(this.txtInicioIncapacidad.Text);
                    objIncapacidad.FechaFin = Convert.ToDateTime(this.txtFinIncapacidad.Text);
                }
                catch
                {
                    throw new Exception("El formato de la fecha de inicio y fin de la incapacidad es incorrecto");
                }

                objIncapacidad.Continuacion = this.chkContinuacion.Checked;
                objIncapacidad.Transcripcion = this.chkTranscripcion.Checked;
                objIncapacidad.Observaciones = this.txtObservacionesIncapacidad.Text;
                objIncapacidad.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);

                if (Request.QueryString["IdSolicitudCopia"] == null)
                {
                    this.WC_SeleccionarDiagnostico1.LoadDiagnosticosIncapacidad(objIncapacidad);
                }

                if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty)
                    objIncapacidad.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);

                if (ViewState["IdIncapacidad"] != null)
                    objIncapacidad.IdIncapacidad = Convert.ToInt64(ViewState["IdIncapacidad"]);

                objSolicitud.objIncapacidad = objIncapacidad;
            }

        }

        /// <summary>
        /// Método, Adiciona un nuevo bloque para adicionar un nuevo tipo de servicio
        /// </summary>
        public void addFacturaDataList(bool p_nueva, int cantidad)
        {
            object[] lstObjetos = new object[0];
            int i = 0;
            DataTable dtTiposServicio = new DataTable();

            if (Request.QueryString["IdSolicitud"] == "0" || Request.QueryString["IdSolicitud"] == "")
            {
                for (i = 0; i < this.dtlTipoServicio.Items.Count + cantidad; i++)
                {
                    dtTiposServicio.Rows.Add(lstObjetos);
                }

                this.dtlTipoServicio.DataSource = dtTiposServicio;
                this.dtlTipoServicio.DataBind();

            }
            else
            {

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
                    }

                    this.dtlTipoServicio.DataSource = dtTiposServicio;
                    this.dtlTipoServicio.DataBind();
                }
                else
                {
                    GeneralTable objGeneralTable = new GeneralTable();
                    objGeneralTable.TableName = "TipoServicios";
                    objGeneralTable.ColumnName = "TipoServicio";
                    objGeneralTable.empresa_id = Convert.ToInt32(Session["Company"]);

                    if (Session["SICAU"] != null)
                        objGeneralTable.Usuario_id = Convert.ToInt32(Session["IdUser"]);
                    else
                        objGeneralTable.IdUser = Convert.ToInt32(Session["IdUser"]);


                    DataTable dtTipoServicios = objGeneralTable.ConsultGeneralTableUsuario().Tables[0];

                    foreach (DataRow row in dtTipoServicios.Rows)
                    {
                        dtTiposServicio.Rows.Add(lstObjetos);
                    }

                    for (i = dtTipoServicios.Rows.Count; i < this.dtlTipoServicio.Items.Count + cantidad; i++)
                    {
                        dtTiposServicio.Rows.Add(lstObjetos);

                    }

                    this.dtlTipoServicio.DataSource = dtTiposServicio;
                    this.dtlTipoServicio.DataBind();
                    DropDownList ddlTipoServicio;

                    foreach (DataListItem dataItem in dtlTipoServicio.Items)
                    {
                        ddlTipoServicio = (DropDownList)dataItem.FindControl("ddlTipoServicio");
                        if (dataItem.ItemIndex < dtTipoServicios.Rows.Count)
                        {
                            ddlTipoServicio.SelectedValue = dtTipoServicios.Rows[dataItem.ItemIndex]["Id"].ToString();
                            ddlTipoServicio.Enabled = false;
                        }
                        DataGrid dtgProductoServicio = (DataGrid)dataItem.FindControl("dtgProductoServicio");

                        DataTable dtProductos = new DataTable();
                        int cantidadSuma = 5;
                        if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos).ToString())
                            cantidadSuma = 10;
                        if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.Remisiones).ToString())
                            cantidadSuma = 1;
                        if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.Procedimientos).ToString())
                            cantidadSuma = 2;
                        if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.ExamenesDiagnosticos).ToString())
                            cantidadSuma = 10;

                        for (i = 0; i < cantidadSuma; i++)
                        {
                            dtProductos.Rows.Add(lstObjetos);
                        }

                        dtgProductoServicio.DataSource = dtProductos;
                        dtgProductoServicio.DataBind();

                        foreach (DataGridItem item in dtgProductoServicio.Items)
                        {
                            TextBox txtIdTipoServicio = (TextBox)item.FindControl("txtIdTipoServicio");
                            txtIdTipoServicio.Text = ddlTipoServicio.SelectedValue;
                            TextBox txtIdServicioProducto = (TextBox)item.FindControl("txtIdServicioProducto");
                            TextBox txtProductoServicio = (TextBox)item.FindControl("txtProductoServicio");
                            TextBox txtValorConvenioSolicitado = (TextBox)item.FindControl("txtValorConvenioSolicitado");
                            TextBox txtComentarioServicioProducto = (TextBox)item.FindControl("txtComentarioServicioProducto");
                            TextBox txtValorAprobado = (TextBox)item.FindControl("txtValorAprobado");
                            TextBox txtDescuento = (TextBox)item.FindControl("txtDescuento");
                            TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                            DropDownList ddlEstadoServicio = (DropDownList)item.FindControl("ddlEstadoServicio");
                            DropDownList ddlMotivo = (DropDownList)item.FindControl("ddlMotivo");
                            Button btnBuscarProductoServicio = (Button)item.FindControl("btnBuscarProductoServicio");
                            TextBox txtDosis = (TextBox)item.FindControl("txtDosis");
                            TextBox txtViaAdministracion = (TextBox)item.FindControl("txtViaAdministracion");
                            TextBox txtDuracion = (TextBox)item.FindControl("txtDuracion");
                            Label lblDosis = (Label)item.FindControl("lblDosis");


                            txtIdServicioProducto.Text = "";
                            txtProductoServicio.Text = "";
                            txtValorConvenioSolicitado.Text = "";
                            txtDosis.Text = "";
                            txtViaAdministracion.Text = "";
                            txtDuracion.Text = "";
                            txtCantidad.Text = "";
                            ddlEstadoServicio.SelectedIndex = 0;
                            txtValorAprobado.Text = "";
                            txtDescuento.Text = "";
                            ddlMotivo.SelectedIndex = 0;
                            txtComentarioServicioProducto.Text = "";

                            btnBuscarProductoServicio.Attributes.Add("OnClick", "javascript:ShowServicioProducto(this,'" + txtIdServicioProducto.ClientID + "','" + txtIdTipoServicio.Text + "','" + txtProductoServicio.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "','" + txtCantidad.ClientID + "','"  + txtComentarioServicioProducto.ClientID + "');");

                            if (Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos) || Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Vacunas))
                            {
                                txtDosis.Visible = true;
                                txtViaAdministracion.Visible = true;
                                txtDuracion.Visible = true;
                                lblDosis.Visible = true;
                                dtgProductoServicio.Columns[2].Visible = true;
                                dtgProductoServicio.Columns[3].Visible = true;
                                dtgProductoServicio.Columns[4].Visible = true;
                            }
                            else
                            {
                                txtDosis.Visible = false;
                                txtViaAdministracion.Visible = false;
                                lblDosis.Visible = false;
                                txtDuracion.Visible = false;
                                dtgProductoServicio.Columns[2].Visible = false;
                                dtgProductoServicio.Columns[3].Visible = false;
                                dtgProductoServicio.Columns[4].Visible = false;
                            }
                            if (ViewState["IdEstadoDefecto"] != null && Convert.ToInt16(ViewState["IdEstadoDefecto"]) != 0)
                            {
                                ddlEstadoServicio.SelectedValue = Convert.ToInt16(ViewState["IdEstadoDefecto"]).ToString();
                                this.FillList(ddlMotivo, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(ViewState["IdEstadoDefecto"]));
                            }
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Método, Carga la grilla con los tipo de servicio por defecto
        /// </summary>
        public void loadDataList()
        {
            object[] lstObjetos = new object[0];
            DataTable dtTiposServicio = new DataTable();
            GeneralTable objGeneralTable = new GeneralTable();
            objGeneralTable.TableName = "TipoServicios";
            objGeneralTable.ColumnName = "TipoServicio";
            objGeneralTable.empresa_id = Convert.ToInt32(Session["Company"]);

            if (Session["SICAU"] != null)
                objGeneralTable.Usuario_id = Convert.ToInt32(Session["IdUser"]);
            else
                objGeneralTable.IdUser = Convert.ToInt32(Session["IdUser"]);


            DataTable dtTipoServicios = objGeneralTable.ConsultGeneralTableUsuario().Tables[0];

            foreach (DataRow row in dtTipoServicios.Rows)
            {
                dtTiposServicio.Rows.Add(lstObjetos);
            }

            this.dtlTipoServicio.DataSource = dtTiposServicio;
            this.dtlTipoServicio.DataBind();

            DropDownList ddlTipoServicio;

            foreach (DataListItem dataItem in dtlTipoServicio.Items)
            {
                ddlTipoServicio = (DropDownList)dataItem.FindControl("ddlTipoServicio");
                ddlTipoServicio.SelectedValue = dtTipoServicios.Rows[dataItem.ItemIndex]["Id"].ToString();
                ddlTipoServicio.Enabled = false;
                DataGrid dtgProductoServicio = (DataGrid)dataItem.FindControl("dtgProductoServicio");

                DataTable dtProductos = new DataTable();
                int cantidadSuma = 5;
                if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos).ToString())
                    cantidadSuma = 1;
                if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.Remisiones).ToString())
                    cantidadSuma = 1;
                if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.Procedimientos).ToString())
                    cantidadSuma = 2;
                if (ddlTipoServicio.SelectedValue == Convert.ToInt32(Servicios.EnumTiposServicio.ExamenesDiagnosticos).ToString())
                    cantidadSuma = 10;

                for (int i = 0; i < cantidadSuma; i++)
                {
                    dtProductos.Rows.Add(lstObjetos);
                }

                dtgProductoServicio.DataSource = dtProductos;
                dtgProductoServicio.DataBind();

                foreach (DataGridItem item in dtgProductoServicio.Items)
                {
                    TextBox txtIdTipoServicio = (TextBox)item.FindControl("txtIdTipoServicio");
                    txtIdTipoServicio.Text = ddlTipoServicio.SelectedValue;
                    TextBox txtIdServicioProducto = (TextBox)item.FindControl("txtIdServicioProducto");
                    TextBox txtProductoServicio = (TextBox)item.FindControl("txtProductoServicio");
                    TextBox txtValorConvenioSolicitado = (TextBox)item.FindControl("txtValorConvenioSolicitado");
                    TextBox txtComentarioServicioProducto = (TextBox)item.FindControl("txtComentarioServicioProducto");
                    TextBox txtValorAprobado = (TextBox)item.FindControl("txtValorAprobado");
                    TextBox txtDescuento = (TextBox)item.FindControl("txtDescuento");
                    TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                    DropDownList ddlEstadoServicio = (DropDownList)item.FindControl("ddlEstadoServicio");
                    DropDownList ddlMotivo = (DropDownList)item.FindControl("ddlMotivo");
                    Button btnBuscarProductoServicio = (Button)item.FindControl("btnBuscarProductoServicio");
                    TextBox txtDosis = (TextBox)item.FindControl("txtDosis");
                    TextBox txtViaAdministracion = (TextBox)item.FindControl("txtViaAdministracion");
                    TextBox txtDuracion = (TextBox)item.FindControl("txtDuracion");
                    Label lblDosis = (Label)item.FindControl("lblDosis");


                    txtIdServicioProducto.Text = "";
                    txtProductoServicio.Text = "";
                    txtValorConvenioSolicitado.Text = "";
                    txtDosis.Text = "";
                    txtViaAdministracion.Text = "";
                    txtDuracion.Text = "";
                    txtCantidad.Text = "";
                    ddlEstadoServicio.SelectedIndex = 0;
                    txtValorAprobado.Text = "";
                    txtDescuento.Text = "";
                    ddlMotivo.SelectedIndex = 0;
                    txtComentarioServicioProducto.Text = "";

                    btnBuscarProductoServicio.Attributes.Add("OnClick", "javascript:ShowServicioProducto(this,'" + txtIdServicioProducto.ClientID + "','" + txtIdTipoServicio.Text + "','" + txtProductoServicio.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "','" + txtCantidad.ClientID + "','" + txtComentarioServicioProducto.ClientID + "');");

                    if (Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos) || Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Vacunas))
                    {
                        txtDosis.Visible = true;
                        txtViaAdministracion.Visible = true;
                        txtDuracion.Visible = true;
                        lblDosis.Visible = true;
                        dtgProductoServicio.Columns[2].Visible = true;
                        dtgProductoServicio.Columns[3].Visible = true;
                        dtgProductoServicio.Columns[4].Visible = true;

                    }
                    else
                    {
                        txtDosis.Visible = false;
                        lblDosis.Visible = false;
                        txtViaAdministracion.Visible = false;
                        txtDuracion.Visible = false;
                        dtgProductoServicio.Columns[2].Visible = false;
                        dtgProductoServicio.Columns[3].Visible = false;
                        dtgProductoServicio.Columns[4].Visible = false;
                    }
                    if (ViewState["IdEstadoDefecto"] != null && Convert.ToInt16(ViewState["IdEstadoDefecto"]) != 0)
                    {
                        ddlEstadoServicio.SelectedValue = Convert.ToInt16(ViewState["IdEstadoDefecto"]).ToString();
                        this.FillList(ddlMotivo, "SolicitudMotivos", "--Motivo--", Convert.ToInt16(ViewState["IdEstadoDefecto"]));
                    }
                }
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
            //base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.imbAdicionarSolicitud.Click += new System.Web.UI.ImageClickEventHandler(this.imbAdicionarSolicitud_Click);
            this.lnkAdicionarSolicitud.Click += new System.EventHandler(this.lnkAdicionarSolicitud_Click);
            this.ddlAdicionarTipoServicio.SelectedIndexChanged += new System.EventHandler(this.ddlAdicionarTipoServicio_SelectedIndexChanged);
            this.imbAdicionarTipoServicio.Click += new System.Web.UI.ImageClickEventHandler(this.imbAdicionarTipoServicio_Click);
            this.lnkAdicionarTipoServicio.Click += new System.EventHandler(this.lnkAdicionarTipoServicio_Click);
            this.imbVerHistorial.Click += new System.Web.UI.ImageClickEventHandler(this.imbVerHistorial_Click);
            this.lnkVerHistorico.Click += new System.EventHandler(this.lnkVerHistorico_Click);
            this.imbGuardar.Click += new System.Web.UI.ImageClickEventHandler(this.imbGuardar_Click);
            this.lnkGuardar.Click += new System.EventHandler(this.lnkGuardar_Click);
            this.imbCopiar.Click += new System.Web.UI.ImageClickEventHandler(this.imbCopiar_Click);
            this.lnkCopiar.Click += new System.EventHandler(this.lnkCopiar_Click);
            this.dtlTipoServicio.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtlTipoServicio_ItemCommand);
            this.dtlTipoServicio.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlTipoServicio_ItemDataBound);
            this.imbHistorialIncapacidades.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorialIncapacidades_Click);
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
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
            Button lnkRegistrarUVR = (Button)ddlEstadoServicio.Parent.FindControl("lnkRegistrarUVR");
            Label lblValorAprobado = (Label)ddlEstadoServicio.Parent.FindControl("lblValorAprobado");
            Label lblDescuento = (Label)ddlEstadoServicio.Parent.FindControl("lblDescuento");
            TextBox txtUVR = (TextBox)ddlEstadoServicio.Parent.FindControl("txtUVR");

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
                TextBox txtValorConvenioSolicitado = (TextBox)item.FindControl("txtValorConvenioSolicitado");
                TextBox txtComentarioServicioProducto = (TextBox)item.FindControl("txtComentarioServicioProducto");
                TextBox txtValorAprobado = (TextBox)item.FindControl("txtValorAprobado");
                TextBox txtDescuento = (TextBox)item.FindControl("txtDescuento");
                TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                DropDownList ddlEstadoServicio = (DropDownList)item.FindControl("ddlEstadoServicio");
                DropDownList ddlMotivo = (DropDownList)item.FindControl("ddlMotivo");
                Button btnBuscarProductoServicio = (Button)item.FindControl("btnBuscarProductoServicio");
                TextBox txtDosis = (TextBox)item.FindControl("txtDosis");
                TextBox txtViaAdministracion = (TextBox)item.FindControl("txtViaAdministracion");
                TextBox txtDuracion = (TextBox)item.FindControl("txtDuracion");
                Label lblDosis = (Label)item.FindControl("lblDosis");
                Button lnkRegistrarUVR = (Button)item.FindControl("lnkRegistrarUVR");
                Button lnkUVRSolicitado = (Button)item.FindControl("lnkUVRSolicitado");
                TextBox txtUVR = (TextBox)item.FindControl("txtUVR");
                TextBox txtUVRSolicitado = (TextBox)item.FindControl("txtUVRSolicitado");

                txtIdServicioProducto.Text = "";
                txtProductoServicio.Text = "";
                txtValorConvenioSolicitado.Text = "";
                txtDosis.Text = "";
                txtViaAdministracion.Text = "";
                txtDuracion.Text = "";
                txtCantidad.Text = "";
                ddlEstadoServicio.SelectedIndex = 0;
                txtValorAprobado.Text = "";
                txtDescuento.Text = "";
                ddlMotivo.SelectedIndex = 0;
                txtComentarioServicioProducto.Text = "";

                lnkRegistrarUVR.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVR.ClientID + "','" + txtValorAprobado.ClientID + "');");
                lnkUVRSolicitado.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVRSolicitado.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "');");
                btnBuscarProductoServicio.Attributes.Add("OnClick", "javascript:ShowServicioProducto(this,'" + txtIdServicioProducto.ClientID + "','" + txtIdTipoServicio.Text + "','" + txtProductoServicio.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "','" + txtCantidad.ClientID + "','" + txtComentarioServicioProducto.ClientID + "');");

                if (Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos) || Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Vacunas))
                {
                    txtDosis.Visible = true;
                    txtViaAdministracion.Visible = true;
                    txtDuracion.Visible = true;
                    lblDosis.Visible = true;
                    dtgProductoServicio.Columns[2].Visible = true;
                    dtgProductoServicio.Columns[3].Visible = true;
                    dtgProductoServicio.Columns[4].Visible = true;

                }
                else
                {
                    txtDosis.Visible = false;
                    txtViaAdministracion.Visible = false;
                    lblDosis.Visible = false;
                    txtDuracion.Visible = false;
                    dtgProductoServicio.Columns[2].Visible = false;
                    dtgProductoServicio.Columns[3].Visible = false;
                    dtgProductoServicio.Columns[4].Visible = false;
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
                    CheckBox chkPrestado = (CheckBox)e.Item.FindControl("chkPrestado");
                    TextBox txtValorAprobado = (TextBox)e.Item.FindControl("txtValorAprobado");
                    System.Web.UI.WebControls.Image btnFechaPrestacion = (System.Web.UI.WebControls.Image)e.Item.FindControl("btnFechaPrestacion");

                    btnFechaPrestacion.Attributes.Add("OnClick", "javascript:MostrarCalendario(Form1." + txtFechaPrestacion.ClientID + ",Form1." + txtFechaPrestacion.ClientID + ",'dd/mm/yyyy');");

                    //Desplegar fecha de prestación si se está liquidando
                    if ((Request.QueryString["liquidacion"] != null && Request.QueryString["liquidacion"] != string.Empty) || (Request.QueryString["liquidacionConfirmacion"] != null && Request.QueryString["liquidacionConfirmacion"] != string.Empty))
                    {
                        chkPrestado.Attributes.Add("OnClick", "javascript:BloquearValorAprobado(Form1." + txtValorAprobado.ClientID + ",Form1." + chkPrestado.ClientID + ");");
                        chkPrestado.Visible = true;
                        chkPrestado.Checked = true;
                    }

                    //Mostrar controles si el estado por defecto es aprobado
                    if (Convert.ToInt16(ViewState["IdEstadoDefecto"]) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado))
                    {

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
                        TextBox txtCantidad = (TextBox)e.Item.FindControl("txtCantidad");
                        TextBox txtValorConvenioSolicitado = (TextBox)e.Item.FindControl("txtValorConvenioSolicitado");
                        TextBox txtDescuento = (TextBox)e.Item.FindControl("txtDescuento");
                        Button btnBuscarProductoServicio = (Button)e.Item.FindControl("btnBuscarProductoServicio");
                        Button lnkRegistrarUVR = (Button)e.Item.FindControl("lnkRegistrarUVR");
                        Button lnkUVRSolicitado = (Button)e.Item.FindControl("lnkUVRSolicitado");
                        TextBox txtUVR = (TextBox)e.Item.FindControl("txtUVR");
                        TextBox txtUVRSolicitado = (TextBox)e.Item.FindControl("txtUVRSolicitado");
                        TextBox txtDosis = (TextBox)e.Item.FindControl("txtDosis");
                        TextBox txtViaAdministracion = (TextBox)e.Item.FindControl("txtViaAdministracion");
                        TextBox txtDuracion = (TextBox)e.Item.FindControl("txtDuracion");
                        TextBox txtComentarioServicioProducto = (TextBox)e.Item.FindControl("txtComentarioServicioProducto");

                        lnkUVRSolicitado.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVRSolicitado.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "');");
                        lnkRegistrarUVR.Attributes.Add("OnClick", "javascript:ShowUVR(this,'" + txtUVR.ClientID + "','" + txtValorAprobado.ClientID + "');");
                        btnBuscarProductoServicio.Attributes.Add("OnClick", "javascript:ShowServicioProducto(this,'" + txtIdServicioProducto.ClientID + "','" + txtIdTipoServicio.Text + "','" + txtProductoServicio.ClientID + "','" + txtValorConvenioSolicitado.ClientID + "','" + txtCantidad.ClientID + "','" + txtComentarioServicioProducto.ClientID + "');");

                        if (Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos) || Convert.ToInt32(ddlTipoServicio.SelectedValue) == Convert.ToInt32(Servicios.EnumTiposServicio.Vacunas))
                        {
                            txtDosis.Visible = true;
                            txtViaAdministracion.Visible = true;
                            txtDuracion.Visible = true;
                            dtgProductoServicio.Columns[2].Visible = true;
                            dtgProductoServicio.Columns[3].Visible = true;
                            dtgProductoServicio.Columns[4].Visible = true;
                        }
                    }

                    //Cargar los datos si viene el id de la solicitud
                    if ((Request.QueryString["IdSolicitud"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudServicio"]) && (long)rowItem["IdSolicitudServicio"] != 0)
                        || (Request.QueryString["IdSolicitudCopia"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudServicio"]) && (long)rowItem["IdSolicitudServicio"] != 0))
                    {
                        TextBox txtIdSolicitudServicio = (TextBox)e.Item.FindControl("txtIdSolicitudServicio");
                        TextBox txtProductoServicio = (TextBox)e.Item.FindControl("txtProductoServicio");
                        TextBox txtIdServicioProducto = (TextBox)e.Item.FindControl("txtIdServicioProducto");
                        TextBox txtCantidad = (TextBox)e.Item.FindControl("txtCantidad");
                        TextBox txtValorConvenioSolicitado = (TextBox)e.Item.FindControl("txtValorConvenioSolicitado");
                        TextBox txtDosis = (TextBox)e.Item.FindControl("txtDosis");
                        TextBox txtViaAdministracion = (TextBox)e.Item.FindControl("txtViaAdministracion");
                        TextBox txtDuracion = (TextBox)e.Item.FindControl("txtDuracion");
                        TextBox txtDescuento = (TextBox)e.Item.FindControl("txtDescuento");
                        TextBox txtUVR = (TextBox)e.Item.FindControl("txtUVR");
                        TextBox txtUVRSolicitado = (TextBox)e.Item.FindControl("txtUVRSolicitado");
                        TextBox txtComentarioServicioProducto = (TextBox)e.Item.FindControl("txtComentarioServicioProducto");

                        Button lnkRegistrarUVR = (Button)e.Item.FindControl("lnkRegistrarUVR");                        
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
                            if (!Convert.IsDBNull(rowItem["Prestado"]))
                            {
                                chkPrestado.Checked = (bool)rowItem["Prestado"];
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
                            dtgProductoServicio.Columns[2].Visible = true;

                        }
                        if (!Convert.IsDBNull(rowItem["ViaAdministracion"]))
                        {
                            txtViaAdministracion.Text = (string)rowItem["ViaAdministracion"];
                            txtViaAdministracion.Visible = true;
                            dtgProductoServicio.Columns[3].Visible = true;

                        }
                        if (!Convert.IsDBNull(rowItem["Duracion"]))
                        {
                            txtDuracion.Text = (string)rowItem["Duracion"];
                            txtDuracion.Visible = true;
                            dtgProductoServicio.Columns[4].Visible = true;

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
                    DropDownList ddlUnidadAprueba = (DropDownList)e.Item.FindControl("ddlUnidadAprueba");
                    DropDownList ddlClaseAtencion = (DropDownList)e.Item.FindControl("ddlClaseAtencion");
                    DropDownList ddlTipoAtencion = (DropDownList)e.Item.FindControl("ddlTipoAtencion");
                    DropDownList ddlContingencia = (DropDownList)e.Item.FindControl("ddlContingencia");
                    DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidad");
                    TPA.interfaz_empleado.WebControls.WC_SeleccionarDiagnostico WC_SeleccionarDiagnostico2 = (TPA.interfaz_empleado.WebControls.WC_SeleccionarDiagnostico)e.Item.FindControl("WC_SeleccionarDiagnostico2");
                    TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador WC_AdicionarPrestador1 = (TPA.interfaz_empleado.WebControls.WC_AdicionarPrestador)e.Item.FindControl("WC_AdicionarPrestador1");
                    DataGrid dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");

                    WC_SeleccionarDiagnostico2.LoadControls();
                    WC_AdicionarPrestador1.LoadControlsOrden("0");


                    for (int k = 1; k < 16; k++)
                        ddlAdicionarServicios.Items.Add(new ListItem(k.ToString(), k.ToString()));

                    this.FillListUser("TipoServicios", "TipoServicio", Convert.ToInt32(Session["IdUser"]), Session["SICAU"], Convert.ToInt32(Session["Company"]), ddlTipoServicio, "--Tipo de Servicio--");
                    if (ddlTipoServicio.Items.Count == 1)
                    {
                        ddlTipoServicio.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Tipo de Servicio--", "0"));
                        ddlTipoServicio.SelectedIndex = 0;
                    }
                    this.FillList("ClaseAtenciones", "ClaseAtencion", ddlClaseAtencion, "--Clase Atención--");
                    this.FillList("TipoAtenciones", "TipoAtencion", ddlTipoAtencion, "--Tipo Atención--");
                    this.FillList("Contingencias", "Contingencia", ddlContingencia, "--Contingencia--");
                    this.FillList("Especialidades", "Especialidad", ddlEspecialidad, "--Especialidad--");

                    //Desplegar fecha de prestación si se está liquidando
                    if ((Request.QueryString["liquidacion"] != null && Request.QueryString["liquidacion"] != string.Empty) || (Request.QueryString["liquidacionConfirmacion"] != null && Request.QueryString["liquidacionConfirmacion"] != string.Empty))
                    {
                        HtmlTableRow trFechaPrestacion = (HtmlTableRow)e.Item.FindControl("trFechaPrestacion");
                        System.Web.UI.WebControls.Image btnFechaPrestacionGeneral = (System.Web.UI.WebControls.Image)e.Item.FindControl("btnFechaPrestacionGeneral");
                        System.Web.UI.WebControls.Image btnDescuento = (System.Web.UI.WebControls.Image)e.Item.FindControl("btnDescuento");
                        
                        TextBox txtFechaPrestacionGeneral = (TextBox)e.Item.FindControl("txtFechaPrestacionGeneral");
                        txtFechaPrestacionGeneral.Attributes.Add("ReadOnly", "ReadOnly"); // PETF 15/01/10 ReadOnly

                        TextBox txtDescuentoGeneral = (TextBox)e.Item.FindControl("txtDescuentoGeneral");
                        trFechaPrestacion.Style["display"] = "";
                        btnFechaPrestacionGeneral.Attributes.Add("OnClick", "javascript:MostrarCalendario(Form1." + txtFechaPrestacionGeneral.ClientID + ",Form1." + txtFechaPrestacionGeneral.ClientID + ",'dd/mm/yyyy','" + dtgProductoServicio.ClientID + "');");
                        btnDescuento.Attributes.Add("OnClick", "javascript:setValueDataGrid(Form1." + txtDescuentoGeneral.ClientID + ",'" + dtgProductoServicio.ClientID + "','txtDescuento');");

                    }

                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                    //Cargar los datos si viene el id de la solicitud
                    if ((Request.QueryString["IdSolicitud"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudTipoServicio"]) && (long)rowItem["IdSolicitudTipoServicio"] != 0)
                        || (Request.QueryString["IdSolicitudCopia"] != null && rowItem.ItemArray.Length > 0 && !Convert.IsDBNull(rowItem["IdSolicitudTipoServicio"]) && (long)rowItem["IdSolicitudTipoServicio"] != 0))
                    {
                        ImageButton imbBorrar = (ImageButton)e.Item.FindControl("imbBorrar");
                        TextBox txtIdSolicitudTipoServicio = (TextBox)e.Item.FindControl("txtIdSolicitudTipoServicio");
                        TextBox txtConsecutivo = (TextBox)e.Item.FindControl("txtConsecutivo");
                        TextBox txtIdSolicitudEstado = (TextBox)e.Item.FindControl("txtIdSolicitudEstado");
                        TextBox txtImpresiones = (TextBox)e.Item.FindControl("txtImpresiones");
                        TextBox txtConsecutivoNombre = (TextBox)e.Item.FindControl("txtConsecutivoNombre");
                        TextBox txtComentariosTipoServicio = (TextBox)e.Item.FindControl("txtComentariosTipoServicio");

                        ddlTipoServicio.Enabled = false;
                        if (Request.QueryString["IdSolicitudCopia"] == null)
                        {
                            imbBorrar.Visible = true;
                            imbBorrar.ToolTip = "Eliminar";
                        }
                        if (!Convert.IsDBNull(rowItem["IdTipoServicio"]))
                            ddlTipoServicio.SelectedValue = ((int)rowItem["IdTipoServicio"]).ToString();
                        if (!Convert.IsDBNull(rowItem["IdTipoAtencion"]))
                            ddlTipoAtencion.SelectedValue = ((short)rowItem["IdTipoAtencion"]).ToString();
                        if (!Convert.IsDBNull(rowItem["IdClaseAtencion"]))
                            ddlClaseAtencion.SelectedValue = ((short)rowItem["IdClaseAtencion"]).ToString();
                        if (!Convert.IsDBNull(rowItem["IdEspecialidad"]))
                            ddlEspecialidad.SelectedValue = ((int)rowItem["IdEspecialidad"]).ToString();
                        if (!Convert.IsDBNull(rowItem["IdContingencia"]))
                            ddlContingencia.SelectedValue = ((short)rowItem["IdContingencia"]).ToString();
                        if (!Convert.IsDBNull(rowItem["UnidadAprobacion"]))
                            ddlUnidadAprueba.SelectedValue = (string)rowItem["UnidadAprobacion"];
                        if (!Convert.IsDBNull(rowItem["Comentarios"]))
                            txtComentariosTipoServicio.Text = rowItem["Comentarios"].ToString();
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

                        LoadControlProveedores(WC_AdicionarPrestador1, ((long)rowItem["IdSolicitudTipoServicio"]));

                        if (Request.QueryString["IdSolicitudCopia"] == null)
                        {
                            //Cargar diagnosticos
                            LoadControlDiagnosticos(WC_SeleccionarDiagnostico2, ((long)rowItem["IdSolicitudTipoServicio"]));
                        }

                        //Colocar inhabilitados los controles para estados liquidados y anulados y no copias
                        if ((Convert.ToInt16((rowItem["IdSolicitudEstado"])) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado) || Convert.ToInt16((rowItem["IdSolicitudEstado"])) == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado))
                            && (Request.QueryString["IdSolicitudCopia"] == null || Request.QueryString["IdSolicitudCopia"] == string.Empty))
                        {
                            DisableRecursive(e.Item);
                        }


                        //Cargar los servicio						
                        SolicitudServicio objSolicitudServicio = new SolicitudServicio();
                        if (Request.QueryString["IdSolicitud"] != null)
                            objSolicitudServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitud"]);
                        if (Request.QueryString["IdSolicitudCopia"] != null)
                            objSolicitudServicio.IdSolicitud = Convert.ToInt64(Request.QueryString["IdSolicitudCopia"]);
                        objSolicitudServicio.IdSolicitudTipoServicio = ((long)rowItem["IdSolicitudTipoServicio"]);
                        dtgProductoServicio.DataSource = objSolicitudServicio.ConsultSolicitudServicio();
                        dtgProductoServicio.DataBind();

                        //Si viene de liquidación solamente lista el tipo de servicio a liquidar
                        if (Request.QueryString["liquidacion"] != null && Request.QueryString["liquidacion"] != string.Empty)
                        {
                            HtmlGenericControl divDataList = (HtmlGenericControl)e.Item.FindControl("divDataList");

                            if (((long)rowItem["IdSolicitudTipoServicio"]) != Convert.ToInt64(Request.QueryString["liquidacion"]))
                            {
                                divDataList.Style["display"] = "none";
                            }
                        }

                    }
                    //Si no es edición, se carga la grilla con la cantidad de items adicionados	
                    else
                    {

                        object[] lstObjetos = new object[0];
                        int cantidadSuma = 1;
                        dtgProductoServicio = (DataGrid)e.Item.FindControl("dtgProductoServicio");
                        DataTable dtProductos = new DataTable();

                        if (dtgProductoServicio.Items.Count == 0)
                            cantidadSuma = 5;
                        for (int i = 0; i < dtgProductoServicio.Items.Count + cantidadSuma; i++)
                        {
                            dtProductos.Rows.Add(lstObjetos);
                        }

                        dtgProductoServicio.DataSource = dtProductos;
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

                    if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"] == string.Empty)
                        this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&idTipoServicio=" + ddlTipoServicio.SelectedValue, 850, 750, e.Item.ItemIndex);
                    else
                        this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + ViewState["beneficiario_id"] + "&idTipoServicio=" + ddlTipoServicio.SelectedValue, 850, 750, e.Item.ItemIndex);



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
                if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != "0" && Request.QueryString["IdSolicitud"] !="")
                {
                    if (Convert.ToInt32(Session["IdUser"]) != (int)ViewState["IdUserCreacion"])
                    {
                        if (ViewState["IdTipoConsulta"].ToString() != "3" || Convert.ToBoolean(ViewState["Finalizada"]) == true)
                            throw new Exception("Solamente el usuario que creo las órdenes puede modificarlas");
                    }

                    this.UpdateSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarSolicitudOrden, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " Estado Actual " + this.ddlEstadoSoli.SelectedItem.Text);

                    Response.Redirect("AE_solicitudordenresumen.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&popup=" + Request.QueryString["popup"] + "&liquidacionConfirmacion=" + Request.QueryString["liquidacion"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=" + Request.QueryString["editar"] + "&adicion=" + Request.QueryString["adicion"] );
                }
                else
                {
                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolicitudOrden, idSolicitud, "Ingreso solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudordenresumen.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&liquidacionConfirmacion=" + Request.QueryString["liquidacionConfirmacion"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=" + Request.QueryString["editar"] + "&adicion=" + Request.QueryString["adicion"] );
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, Cancela y retorna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            //if (Request.QueryString["adicion"] != null && Request.QueryString["adicion"] != string.Empty)
            //    Response.Redirect("LIS_consulta.aspx"); 
            //else
            //    Response.Redirect("LIS_empleado.aspx");           
            Response.Redirect("LIS_consulta.aspx");//Marsh - JFEE - 2014/11/26 - Correcciones generales, al cancelar una consulta siempre debe direccionar a la página de consultas
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
                    if (this.txtAnulacion.Text == string.Empty)
                        throw new Exception("Debe ingresar las Observaciones de Anulación");

                    short idEstado = Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Anulado);
                    short idMotivo = Convert.ToInt16(Solicitud.EnumMotivosEstadoSolicitud.Anulado);
                    this.UpdateEstadoSolicitud(idEstado, idMotivo);
                    this.RegisterLog(Log.EnumActionsLog.ModificarEstadoSolicitud, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación estado solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " A Estado Anulado");

                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolictudAutorizacion, idSolicitud, "Ingreso solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudordenresumen.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&liquidacionConfirmacion=" + Request.QueryString["liquidacionConfirmacion"]);

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
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"] == string.Empty)
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + ViewState["beneficiario_id"], 850, 750);
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
                if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != "0")
                {
                    if (Convert.ToInt32(Session["IdUser"]) != (int)ViewState["IdUserCreacion"])
                    {
                        if (ViewState["IdTipoConsulta"].ToString() != "3" || Convert.ToBoolean(ViewState["Finalizada"]) == true)
                            throw new Exception("Solamente el usuario que creo las órdenes puede modificarlas");
                    }
                    this.UpdateSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarSolicitudOrden, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " Estado Actual " + this.ddlEstadoSoli.SelectedItem.Text);
                    Response.Redirect("AE_solicitudorden.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdConsulta=" + Request.QueryString["IdConsulta"] );
                }
                else
                {
                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolicitudOrden, idSolicitud, "Modificación solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudorden.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdConsulta=" + Request.QueryString["IdConsulta"] );
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
                if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != "0")
                {
                    if (Convert.ToInt32(Session["IdUser"]) != (int)ViewState["IdUserCreacion"])
                    {
                        if (ViewState["IdTipoConsulta"].ToString() != "3" || Convert.ToBoolean(ViewState["Finalizada"]) == true)
                            throw new Exception("Solamente el usuario que creo las órdenes puede modificarlas");
                    }

                    this.UpdateSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarSolicitudOrden, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " Estado Actual " + this.ddlEstadoSoli.SelectedItem.Text);
                    Response.Redirect("AE_solicitudorden.aspx?IdSolicitud=" + Convert.ToInt64(Request.QueryString["IdSolicitud"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=1" );//MAHG 19/07/2010 Se agrega el parámetro Finalizada
                }
                else
                {
                    long idSolicitud = this.InsertSolicitud();
                    this.RegisterLog(Log.EnumActionsLog.IngresarSolicitudOrden, idSolicitud, "Modificación solicitud " + idSolicitud);
                    Response.Redirect("AE_solicitudorden.aspx?IdSolicitud=" + idSolicitud + "&employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=1" );//MAHG 19/07/2010 Se agrega el parámetro Finalizada);
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
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"] == string.Empty)
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + ViewState["beneficiario_id"], 850, 750);
        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de antecedentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialIncapacidades_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

            if (ViewState["beneficiario_id"] != null && ViewState["beneficiario_id"] != string.Empty)
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"] + "&Modulo=incapacidad", 850, 750, 3);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=incapacidad", 850, 750, 3);



        }

        /// <summary>
        /// Evento, realiza el llamado para la copia de órdenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbCopiar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"] == string.Empty)
                this.OpenWindow("LIS_historicosolicitudes.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s" + "&IdConsulta=" + Request.QueryString["IdConsulta"], 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudes.aspx?beneficiario_id=" + ViewState["beneficiario_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"], 850, 750);
        }

        /// <summary>
        /// Evento, realiza el llamado para la copia de órdenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkCopiar_Click(object sender, System.EventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"] == string.Empty)
                this.OpenWindow("LIS_historicosolicitudes.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s" + "&IdConsulta=" + Request.QueryString["IdConsulta"], 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudes.aspx?beneficiario_id=" + ViewState["beneficiario_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"], 850, 750);
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

        /// <summary>
        /// Evento, Retorna al paso anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnterior_Click(object sender, System.EventArgs e)
        {
            EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones(int.Parse(Session["Company"].ToString()));

            if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != "0" && Request.QueryString["IdSolicitud"] != "")
            {
                if (Convert.ToInt32(Session["IdUser"]) != (int)ViewState["IdUserCreacion"])
                {
                    if (ViewState["IdTipoConsulta"].ToString() != "3" || Convert.ToBoolean(ViewState["Finalizada"]) == true)
                        throw new Exception("Solamente el usuario que creo las órdenes puede modificarlas");
                }

                this.UpdateSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]));
                this.RegisterLog(Log.EnumActionsLog.ModificarSolicitudOrden, Convert.ToInt64(Request.QueryString["IdSolicitud"]), "Modificación solicitud " + Convert.ToInt32(Request.QueryString["IdSolicitud"]) + " Estado Actual " + this.ddlEstadoSoli.SelectedItem.Text);

                Consulta objConsulta = new Consulta();
                objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());

                if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                {
                    if (_esNutriologo)
                    {
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + ViewState["IdSolicitud"] + "&TipoConsulta=" + Session["idTipoConsulta"].ToString(), false);
                    }
                    else
                    {
                        if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisiones())
                        {
                            if (idConsulta == "11" || idConsulta == "12")
                                Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            else
                                Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                        }
                        else
                        {
                            if (_esMedico)
                            {
                                if (idConsulta == "11" || idConsulta == "12")
                                    Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                            }
                            else
                                Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                        }
                    }
                }    
                else
                {
                    Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );
                }                                
            }
            else
            {
                long idSolicitud = this.InsertSolicitud();
                this.RegisterLog(Log.EnumActionsLog.IngresarSolicitudOrden, idSolicitud, "Modificación solicitud " + idSolicitud);
                Consulta objConsulta = new Consulta();
                objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());

                if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                {
                    if (_esNutriologo)
                    {
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + ViewState["IdSolicitud"] + "&TipoConsulta=" + Session["idTipoConsulta"].ToString(), false);
                    }
                    else
                    {
                        if (!objConsulta.ExisteConsultaEstiloVida())
                        {
                            if (idConsulta == "11" || idConsulta == "12")
                                Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            else
                                Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + idSolicitud);
                        }
                        else
                        {
                            if (_esMedico)
                            {
                                if (idConsulta == "11" || idConsulta == "12")
                                    Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                            }
                            else
                                Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + idSolicitud);
                        }
                    }
                }
                else
                {
                    if (_esNutriologo)
                        Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + idSolicitud);
                    else
                    {
                        if (_esMedico)
                        {
                            if (idConsulta == "11" || idConsulta == "12")
                                Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            else
                                Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                        }
                        else
                            Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + ViewState["IdSolicitud"] + "&TipoConsulta=" + Session["idTipoConsulta"].ToString(), false);
                    }
                }
            }        

        }

        #endregion

    }
}
