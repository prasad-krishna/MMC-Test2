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
    ///	Control que despliega los datos principales del empleado
    /// </summary>
    public partial class WC_DatosEmpleado : WC_Base
    {
        #region Atributos

        
        #endregion

        #region Inicialización

        /// <summary>
        /// Evento, inicializa el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 26/01/10*/
            //Se carga el load de la clase base del control
            base.Page_Load(sender, e);
            //Fin MAHG 26/01/10

            if (Request.QueryString["employee_id"] != null)
            {
                this.LoadControlEmp(Convert.ToInt32(Request.QueryString["employee_id"]));
                if (Request.QueryString["beneficiario_id"] != null && Convert.ToInt32(Request.QueryString["beneficiario_id"]) != 0)
                    this.LoadControlUsu(Convert.ToInt32(Request.QueryString["beneficiario_id"]), Convert.ToInt32(Request.QueryString["employee_id"]));
                int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

                if (intPais == 1)
                {
                    LoadStrings(false);
                }
                else
                {
                    LoadStrings(true);
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

            lblTituloEPS.Visible = bolHabilitar;
            lblEPS.Visible = bolHabilitar;
            lblLabelBase.Visible = bolHabilitar;
            lblBase.Visible = bolHabilitar;
            lblTituloTelefonos.Visible = bolHabilitar;
            lblTelefono.Visible = bolHabilitar;
            lblCelular.Visible = bolHabilitar;
            lblTituloPlan.Visible = bolHabilitar;
            lblPlan.Visible = bolHabilitar;
            lblTituloCargo.Visible = bolHabilitar;
            lblCargo.Visible = bolHabilitar;
            lblTituloCiudad.Visible = bolHabilitar;
            lblCiudad.Visible = bolHabilitar;
            lblTituloEmail.Visible = bolHabilitar;
            lblEmail.Visible = bolHabilitar;
            lblTituloCodigo.Visible = bolHabilitar;
            lblCodigo.Visible = bolHabilitar;

            //Datos Beneficiario

            lblTituloEPSUsuario.Visible = bolHabilitar;
            lblEPSUsu.Visible = bolHabilitar;          
            lblTituloTelefonoUsuario.Visible = bolHabilitar;
            lblTelefonoUsu.Visible = bolHabilitar;
            lblTituloPlanUsuario.Visible = bolHabilitar;
            lblPlanUsu.Visible = bolHabilitar;
                       
        }

        /// <summary>
        /// Método, carga la información del empleado
        /// </summary>
        private void LoadControlEmp(int p_idEmpleado)
        {
            SIC_EMPLEADO objSicEmpleado = new SIC_EMPLEADO();
            EmpresaDatos objEmpresaDatos = new EmpresaDatos();            

            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();

            string parentescos = "";
            DataTable dtParentescos;
            EmpresaParentescos objEmpresaParentescos = new EmpresaParentescos();
            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];

            foreach (DataRow row in dtParentescos.Rows)
            {
                if (row["IdParentesco"].ToString() != "0")
                {
                    parentescos += row["IdParentesco"].ToString() + ",";
                }
            }
            if (parentescos != "")
                parentescos = parentescos.Remove(parentescos.Length - 1, 1);


            objSicEmpleado.Id_empleado = p_idEmpleado;
            objSicEmpleado.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
            objSicEmpleado.GetSIC_EMPLEADO();

            //Inicio Ricardo Silva 05/10/2011
            //Se insertan los campos del aviso de privacidad 

            SIC_PRIVACIDAD Privacidad = new SIC_PRIVACIDAD();
            Privacidad.GetSIC_PRIVACIDAD(0, p_idEmpleado);

            if (Privacidad.Firma == true)
            {
                lblFirma.Text = "Si";
                String fechaUltimaFirma;
                fechaUltimaFirma = Privacidad.fechaUltimaFirma.Substring(0, 10);
                lblFechaFirma.Text = fechaUltimaFirma;
            }
            else
            {
               lblFirma.Text = "No";
               lblFechaFirma.Visible = false;
               Label3.Visible = false;
            }

            //Fin Ricardo Silva

            this.lblIdentificacion.Text = objSicEmpleado.Identificacion;
            string nombreCompleto = (objSicEmpleado.Primer_nombre != null && objSicEmpleado.Primer_nombre != string.Empty ? objSicEmpleado.Primer_nombre : "");
            nombreCompleto = nombreCompleto + (objSicEmpleado.Segundo_nombre != null && objSicEmpleado.Segundo_nombre != string.Empty ? " " + objSicEmpleado.Segundo_nombre : "");
            nombreCompleto = nombreCompleto + (objSicEmpleado.Apellido_paterno != null && objSicEmpleado.Apellido_paterno != string.Empty ? " " + objSicEmpleado.Apellido_paterno : "");
            nombreCompleto = nombreCompleto + (objSicEmpleado.Apellido_materno != null && objSicEmpleado.Apellido_materno != string.Empty ? " " + objSicEmpleado.Apellido_materno : "");
            this.lblNombre.Text = nombreCompleto;
            this.lblFechaNac.Text = objSicEmpleado.Fecha_nacimiento.ToShortDateString();
            this.lblEPS.Text = objSicEmpleado.EPS;
            this.lblPlan.Text = objSicEmpleado.PlanComplementario;
            this.lblCentroCosto.Text = objSicEmpleado.Codigo_centro_costo + "-" + objSicEmpleado.Nombre_centro_costo;
            this.lblCodigo.Text = objSicEmpleado.Codigo;
            this.lblEdad.Text = objSicEmpleado.Edad.ToString();
            this.lblTelefono.Text = objSicEmpleado.Telefono;
            this.lblEmail.Text = objSicEmpleado.Correo;
            this.lblEstado.Text = objSicEmpleado.NombreEstado;
            this.lblPlan.Text = objSicEmpleado.PlanComplementario;
            this.lblCelular.Text = "Celular:" + objSicEmpleado.Celular;
            this.lblCargo.Text = objSicEmpleado.Cargo;
            this.lblCiudad.Text = objSicEmpleado.NombreCiudad;
        
            if (objSicEmpleado.Sexo == 2)
            {
                this.lblGenero.Text = "Femenino";
            }
            else
            {
                this.lblGenero.Text = "Masculino";
            }

            if (objSicEmpleado.IPS != null && objSicEmpleado.IPS != string.Empty)
            {
                this.lblLabelBase.Visible = true;
                this.lblBase.Text = objSicEmpleado.IPS;
            }
            if (objSicEmpleado.Profesion != null && objSicEmpleado.Profesion != string.Empty)
                this.lblCentroCosto.Text = this.lblCentroCosto.Text + " - " + objSicEmpleado.Profesion;

            if (objSicEmpleado.fecha_ingreso_salud.ToShortDateString() != "01/01/0001")
                this.lblFechaIngreso.Text = objSicEmpleado.fecha_ingreso_salud.ToShortDateString();
            if (objSicEmpleado.fecha_egreso.ToShortDateString() != "01/01/0001")
                this.lblEstado.Text = this.lblEstado.Text + " Fecha Retiro:" + objSicEmpleado.fecha_egreso.ToShortDateString();

            //Carga de datos de cupo
            Solicitud objSolicitud = new Solicitud();
            objSolicitud.Id_empleado = p_idEmpleado;
            objSolicitud.GetValoresEmpleado();
            this.lblCantidadSolicitudes.Text = objSolicitud.CantidadSolicitudesEmpleado.ToString();

            if (objSolicitud.ValorUtilizadoEmpleado == 0)
                this.lblUtilizado.Text = "0";

            else
                this.lblUtilizado.Text = string.Format("{0:0,0}", objSolicitud.ValorUtilizadoEmpleado);

            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            objBeneficiario.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
            objBeneficiario.Opcion = 3;
            objBeneficiario.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
            objBeneficiario.Parentescos = parentescos;
            this.dtgBeneficiarios.DataSource = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
            this.dtgBeneficiarios.DataBind();

            objBeneficiario.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
            objBeneficiario.Opcion = 4;
            DataTable dtPreexistenciasTitular = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];

            if (dtPreexistenciasTitular.Rows.Count > 0)
            {
                this.dtgPreexistenciasTitual.DataSource = dtPreexistenciasTitular;
                this.dtgPreexistenciasTitual.DataBind();
            }
            else
            {
                this.dtgPreexistenciasTitual.Visible = false;
            }

            //Desplegar toda la información o ocultarla
            if (Request.QueryString["liquidacionConfirmacion"] != null && Request.QueryString["liquidacionConfirmacion"] != string.Empty)
            {
                this.dvEmpleado.Style["display"] = "none";
            }

            //Cargar titulo preexistencias por empresa
           
            this.dtgPreexistenciasTitual.Columns[1].HeaderText = objEmpresaDatos.TituloPreexistencias;
            this.dtgPreexistenciasUsuario.Columns[1].HeaderText = objEmpresaDatos.TituloPreexistencias;           

            //RAM carga historial de consultas
            Consulta cons = new Consulta();
            DataSet ds = new DataSet();
            ds = cons.consultaHistorial(Convert.ToInt32(Request.QueryString["employee_id"]));

            if (ds.Tables.Count > 0)
            {
                gridInformacionHistorica.DataSource = ds;
                gridInformacionHistorica.DataBind();
            }
            else
            {
                noDatos.Visible = true;
                noDatos.Text = "Sin información historica";
            }
        }


        /// <summary>
        /// Método, carga la información del beneficiario en caso que no sea el empleado
        /// </summary>
        private void LoadControlUsu(int p_idBeneficiario, int p_idEmpleado)
        {
            this.fldUsuario.Style["display"] = "";
            this.dtgPreexistenciasTitual.Visible = false;
            EmpresaDatos objEmpresaDatos = new EmpresaDatos();

            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();

            SIC_BENEFICIARIO objSicBeneficiario = new SIC_BENEFICIARIO();
            objSicBeneficiario.Id_empleado = p_idEmpleado;
            objSicBeneficiario.Beneficiario_id = p_idBeneficiario;
            objSicBeneficiario.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
            objSicBeneficiario.Opcion = 2;
            objSicBeneficiario.GetSIC_BENEFICIARIO();

            //Inicio Ricardo Silva 05/10/2011
            //Se insertan los campos del aviso de privacidad 

            SIC_PRIVACIDAD Privacidad = new SIC_PRIVACIDAD();
            Privacidad.GetSIC_PRIVACIDAD(p_idBeneficiario, p_idEmpleado);

            if (Privacidad.Firma == true)
            {
                lblFirmoAvisoUsuario.Text = "Si";
                String fechaUltimaFirma;
                fechaUltimaFirma = Privacidad.fechaUltimaFirma.Substring(0, 10);
                lblFechaAvisoUsuario.Text = fechaUltimaFirma;
            }
            else
            {
                lblFirmoAvisoUsuario.Text = "No";
                lblTextofechaUsuario.Visible = false;
                lblFechaAvisoUsuario.Visible = false;
            }

            //Fin Ricardo Silva
            

            this.lblIdentificacionUsu.Text = objSicBeneficiario.Identificacion;
            this.lblNombreUsu.Text = objSicBeneficiario.Nombre;
            this.lblFechaNacUsu.Text = objSicBeneficiario.Fecha_nacimiento.ToShortDateString();
            this.lblEdadUsu.Text = objSicBeneficiario.Edad.ToString();
            this.lblTelefonoUsu.Text = objSicBeneficiario.Telefono;
            this.lblParentescoUsu.Text = objSicBeneficiario.Parentesco;
            if (objSicBeneficiario.FechaIngresoPlan.ToShortDateString() != "01/01/0001")
                this.lblFechaIngresoUsu.Text = objSicBeneficiario.FechaIngresoPlan.ToShortDateString();
            this.lblGeneroUsu.Text = objSicBeneficiario.sexo_texto;
            ViewState["Genero"] = objSicBeneficiario.Genero;

            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            objBeneficiario.Beneficiario_id = p_idBeneficiario;
            objBeneficiario.Opcion = 2;

            DataTable dtPreexistenciasUsuario = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];

            if (dtPreexistenciasUsuario.Rows.Count > 0)
            {
                this.dtgPreexistenciasUsuario.DataSource = dtPreexistenciasUsuario;
                this.dtgPreexistenciasUsuario.DataBind();
            }
            else
            {
                this.dtgPreexistenciasUsuario.Visible = false;
            }
        }


        #endregion

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

        //RAM* colores
        protected void gridInformacionHistorica_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int par = e.Row.RowIndex;
            if ((par % 2)==0)
                e.Row.BackColor = System.Drawing.Color.FromName("#F7F7F7");
            else
                e.Row.BackColor = System.Drawing.Color.White;

        }
    }
}
