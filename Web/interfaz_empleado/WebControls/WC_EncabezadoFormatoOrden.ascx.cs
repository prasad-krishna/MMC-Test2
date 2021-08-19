namespace TPA.interfaz_empleado.WebControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Mercer.Medicines.Logic;
    using Web.interfaz_empleado.WebControls;

    /// <summary>
    ///	Contiene los datos principales de los formatos
    /// </summary>
    public partial class WC_EncabezadoFormatoOrden : WC_Base
    {

        #region Atributos

        

        #endregion

        #region Properties

        /// <summary>Propiedad, Indica si despliega la tabla con datos del prestador</summary>
        public bool DesplegarPrestador
        {
            get { return (bool)ViewState["DesplegarPrestador"]; }
            set { ViewState["DesplegarPrestador"] = value; }
        }

        /// <summary>Propiedad, Indica si despliega el servicio</summary>
        public bool DesplegarServicios
        {
            get { return (bool)ViewState["DesplegarServicios"]; }
            set { ViewState["DesplegarServicios"] = value; }
        }

        #endregion

        #region Inicialización

        /// <summary>
        /// Inicializa la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            if (!this.Page.IsPostBack)
            {
                /* Inicio MAHG
                * Requerimiento: Ocultar etiquetas de Colombia para México
                */

                int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

                if (intPais == 1)
                {
                    LoadStrings(false);
                    lblTituloAutorizacion.Text = "Autorización  No.";
                    lblTituloIdentificacion.Text = "NO. EMPLEADO";
                    lblTituloAseguradora.Visible = true;
                    lblAseguradora.Visible = true;
                    lblTituloSede.Visible = true;
                    lblSede.Visible = true;
                }
                else
                {
                    LoadStrings(true);
                }
                if (Request.QueryString["IdSolicitud"] != null)
                {
                    if (ViewState["DesplegarPrestador"] != null && !this.DesplegarPrestador)
                        this.tblPrestador.Style["display"] = "none";
                    else
                        this.tblPrestador.Style["display"] = "";

                    if (ViewState["DesplegarServicios"] != null && this.DesplegarServicios)
                        LoadServicios(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]));


                    this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
                    this.LoadLogo();
                }
            }
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Oculta las etiquetas que no deben mostrarse para México
        /// </summary>
        private void LoadStrings(bool bolHabilitar)
        {

            lblTituloResidencia.Visible = bolHabilitar;
            lblLugarResidencia.Visible = bolHabilitar;
            lblTituloEPS.Visible = bolHabilitar;
            lblEPS.Visible = bolHabilitar;
            lblTituloTelefono.Visible = bolHabilitar;
            lblTelefono.Visible = bolHabilitar;
            lblTituloBase.Visible = bolHabilitar;
            lblTituloBase.Visible = bolHabilitar;
            lblBase.Visible = bolHabilitar;



        }

        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio, int p_idProveedor)
        {
            DataTable dtBeneficiarios;
            Solicitud objSolicitud = new Solicitud();
            SolicitudTipoServicio objTipoServicio = new SolicitudTipoServicio();
            EmpresaTipoServicios objEmpresaTipoServicio = new EmpresaTipoServicios();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
            Prestadores objPrestador = new Prestadores();
            SolicitudTipoServicioProveedores objSolProveedor = new SolicitudTipoServicioProveedores();
            GeneralTable objGeneral = new GeneralTable();

            objSolicitud.IdSolicitud = p_idSolicitud;
            objSolicitud.GetSolicitud();

            if (objSolicitud.Usuario_idCreacion != 0)
            {
                SIC_USUARIO objUsuario = new SIC_USUARIO();
                objUsuario.Usuario_id = objSolicitud.Usuario_idCreacion;
                DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
                this.lblExpedidaPor.Text = dtUsuario.Rows[0]["nombre"].ToString();
            }
            else
            {
                this.lblExpedidaPor.Text = objSolicitud.NameUser;

            }

            objGeneral.Id = objSolicitud.IdCiudad;
            objGeneral.TableName = "Ciudades";
            objGeneral.ColumnName = "Ciudad";
            objGeneral.GetGeneralTable();
            this.lblExpedidaPor.Text += "<br/>" + objGeneral.Nombre;

            objTipoServicio.IdSolicitud = p_idSolicitud;
            objTipoServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            objTipoServicio.GetSolicitudTipoServicio();

            //Cargar el solicitante, si el tipo de servicio tiene uno específico lo carga sino carga el general de la solicitud
            if (objTipoServicio.IdPrestador != 0)
                objPrestador.IdPrestador = objTipoServicio.IdPrestador;
            else
                objPrestador.IdPrestador = objSolicitud.IdPrestador;
            objPrestador.GetPrestadores();
            this.lblMedicoTra.Text = objPrestador.NombrePrestador;
            this.lblEspecialidad.Text = objPrestador.NombreEspecialidad;
            this.lblTelefonoTra.Text = objPrestador.Telefonos;

            objGeneral.Id = Convert.ToInt32(objTipoServicio.IdTipoServicio);
            objGeneral.TableName = "TipoServicios";
            objGeneral.ColumnName = "TipoServicio";
            objGeneral.GetGeneralTable();
            this.lblOrden.Text = objGeneral.Nombre;

            if (p_idProveedor != 0)
            {
                Proveedores objProveedor = new Proveedores();
                objProveedor.IdProveedor = p_idProveedor;
                objProveedor.GetProveedores();
                this.lblPrestador.Text = objProveedor.NombreProveedor; ;
                this.lblDireccion.Text = objProveedor.Direcciones;
                this.lblTelefono.Text = objProveedor.Telefonos;
                this.lblEspecialidadPres.Text = objProveedor.NombreEspecialidad;
            }
            else
            {
                if (objTipoServicio.IdEspecialidad != 0)
                {
                    objGeneral.Id = Convert.ToInt32(objTipoServicio.IdEspecialidad);
                    objGeneral.TableName = "Especialidades";
                    objGeneral.ColumnName = "Especialidad";
                    objGeneral.GetGeneralTable();
                    this.lblEspecialidadPres.Text = objGeneral.Nombre;
                }

            }



            objEmpresaTipoServicio.IdTipoServicio = objTipoServicio.IdTipoServicio;
            objEmpresaTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaTipoServicio.GetEmpresaTipoServicios();
            DateTime fechaLimite = Convert.ToDateTime(objSolicitud.FechaCreacion).AddDays(Convert.ToDouble(objEmpresaTipoServicio.DiasVigencia));
            this.lblFechaLimite.Text = fechaLimite.ToString("dd/MMMM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));

            this.lblTitulo.Text = objEmpresaTipoServicio.TituloFormato;

            objEmpleado.Id_empleado = objSolicitud.Id_empleado;
            objEmpleado.GetSIC_EMPLEADO();
            this.lblAseguradora.Text = objEmpleado.Nombre_centro_costo;
            

            if (objSolicitud.Beneficiario_id != 0)
            {
                objBeneficiario.Opcion = 2;
                objBeneficiario.Beneficiario_id = objSolicitud.Beneficiario_id;
                dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                this.lblNombrePaciente.Text = dtBeneficiarios.Rows[0]["nombre"].ToString();
                this.lblTipoIdentificacion.Text = dtBeneficiarios.Rows[0]["Tipo_doc"].ToString();
                this.lblNumero.Text = dtBeneficiarios.Rows[0]["identificacion"].ToString();
                this.lblEdad.Text = dtBeneficiarios.Rows[0]["edad"].ToString();
                this.lblGenero.Text = dtBeneficiarios.Rows[0]["genero"].ToString() == "1" ? "M" : "F";
                this.lblLugarResidencia.Text = dtBeneficiarios.Rows[0]["NombreCiudad"].ToString();
                this.lblTelefonoPaciente.Text = dtBeneficiarios.Rows[0]["Telefono"].ToString();
                this.lblSede.Text = dtBeneficiarios.Rows[0]["locDescripcion"].ToString();
                

            }
            else
            {
                this.lblNombrePaciente.Text = objEmpleado.Nombre_completo;
                this.lblTipoIdentificacion.Text = objEmpleado.Tipo_documento;
                this.lblNumero.Text = objEmpleado.Identificacion;
                this.lblEdad.Text = objEmpleado.Edad.ToString();
                this.lblGenero.Text = objEmpleado.Sexo.ToString() == "1" ? "M" : "F";
                this.lblLugarResidencia.Text = objEmpleado.NombreCiudad;
                this.lblTelefonoPaciente.Text = objEmpleado.Telefono;
                this.lblEPS.Text = objEmpleado.EPS;
                this.lblBase.Text = objEmpleado.IPS;
                this.lblSede.Text = objEmpleado.locDescripcion;
            }

            this.lblFecha.Text = objSolicitud.Fecha.ToString("dd/MMMM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));

            if (objTipoServicio.ConsecutivoNombre.EndsWith("-"))
            {
                objSolProveedor = new SolicitudTipoServicioProveedores();
                objSolProveedor.IdProveedor = p_idProveedor;
                objSolProveedor.IdSolicitud = p_idSolicitud;
                objSolProveedor.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
                objSolProveedor.GetSolicitudTipoServicioProveedores();
                this.txtNoSolicitud.Text = objTipoServicio.ConsecutivoNombre + objSolProveedor.Consecutivo;

            }

            else
            {
                this.txtNoSolicitud.Text = objTipoServicio.ConsecutivoNombre;
            }



        }

        /// <summary>
        /// Método, realiza la carga del logotipo
        /// </summary>
        public void LoadLogo()
        {
            this.imgLogo.ImageUrl = "../../logos/logo_" + Session["Company"] + ".gif";
        }

        /// <summary>
        /// Método, carga el listado de servicios
        /// </summary>
        public void LoadServicios(long p_idSolicitud, long p_idSolicitudTipoServicio)
        {
            SolicitudServicio objServicios = new SolicitudServicio();
            objServicios.IdSolicitud = p_idSolicitud;
            objServicios.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;

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
        ///		Método necesario para admitir el Diseñador. No se puede modificar
        ///		el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #endregion


    }
}
