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
    public partial class WC_EncabezadoFormatoIncapacidad : WC_Base
    {

        #region Atributos

        
        #endregion

        #region Properties

        /// <summary>Propiedad, Indica si despliega la tabla con datos del prestador</summary>
        public string TipoOrden
        {
            get { return (string)ViewState["TipoOrden"]; }
            set { ViewState["TipoOrden"] = value; }
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
                if (Request.QueryString["IdSolicitud"] != null)
                {

                    this.LoadFormSolicitud(Convert.ToInt64(Request.QueryString["IdSolicitud"]), Convert.ToInt64(Request.QueryString["IdSolicitudTipoServicio"]), Convert.ToInt32(Request.QueryString["IdProveedor"]));
                    this.LoadLogo();
                    this.LoadStrings();
                }
            }
        }

        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Oculta las etiquetas que no deben mostrarse para México
        /// </summary>
        private void LoadStrings()
        {
            int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

            if (intPais == 1)
            {
                lblEPSTitulo.Visible = false;
                lblEPS.Visible = false;
                lblLugarResidencia.Visible = false;
                lblLugarResidenciaTitulo.Visible = false;   
                lblBaseTitulo.Visible = false;
                lblBase.Visible = false;
                lblTelefonoPacienteTitulo.Visible = false;
                lblTelefonoPaciente.Visible = false;
                lblTituloIdentificación.Text = "NO. DE EMPLEADO";
                lblTituloAseguradora.Visible = true;
                lblAseguradora.Visible = true;
                lblTituloSede.Visible = true;
                lblSede.Visible = true;
            }
        }

        #endregion

        #region Métodos

        public void LoadFormSolicitud(long p_idSolicitud, long p_idSolicitudTipoServicio, int p_idProveedor)
        {
            this.lblTipoOrden.Text = this.TipoOrden;

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

            /*MAHG Se oculta la ciudad para México*/
            if (int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString()) != 1)
            {
                this.lblExpedidaPor.Text += "<br/>" + objGeneral.Nombre;
            }

            objTipoServicio.IdSolicitud = p_idSolicitud;
            objTipoServicio.IdSolicitudTipoServicio = p_idSolicitudTipoServicio;
            objTipoServicio.GetSolicitudTipoServicio();

            objEmpresaTipoServicio.IdTipoServicio = Convert.ToInt32(Servicios.EnumTiposServicio.Medicamentos);
            objEmpresaTipoServicio.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaTipoServicio.GetEmpresaTipoServicios();

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
                this.lblSede.Text = objEmpleado.locDescripcion;
            }

            this.lblFecha.Text = objSolicitud.Fecha.ToString("dd/MMMM/yyyy hh:mm tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));

            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objConsulta.GetConsulta();
            this.txtNoSolicitud.Text = objConsulta.ConsecutivoNombre;

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
