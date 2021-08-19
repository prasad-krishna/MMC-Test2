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
using System.Configuration;
using Mercer.Medicines.Logic;
using System.Web.Security;

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Inserta o modifica una consulta médica
    /// </summary>
    public partial class AE_registroconsulta : PB_PaginaBase
    {
        #region Atributos
        public int tipoConsulta = 0;
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
                if (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty)
                {
                }
                //Fin - Emilio Bueno 20/11/2012

                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10


                Session["idTipoConsulta"] = "";

                //Inicio MAHG 21/07/2010 Se verifica si la solicitud es Asincrona
                string strScript = "var tipoConsulta = '';";
                strScript += "var radio = document.getElementsByName('rblTipoConsulta');";
                strScript += " for (var j = 0; j < radio.length; j++)";
                strScript += " { ";
                strScript += " if (radio[j].checked)";
                strScript += " tipoConsulta = radio[j].value;";
                strScript += " }";

                strScript += "if(tipoConsulta != '') {";
                strScript += "if (tipoConsulta == 3) {";
                strScript += "var lblIdObligatorioDiagnostico = '" + (WC_AdicionarDiagnosticoConsulta1.FindControl("lblObligatorioDiagnostico")).ClientID + "';";
                strScript += "document.getElementById(lblIdObligatorioDiagnostico).style.display = 'none';";
                strScript += "}else {";
                strScript += "var lblIdObligatorioDiagnostico =  '" + (WC_AdicionarDiagnosticoConsulta1.FindControl("lblObligatorioDiagnostico")).ClientID + "';";
                strScript += "document.getElementById(lblIdObligatorioDiagnostico).style.display = '';";
                strScript += "}}";


                if (System.Web.UI.ScriptManager.GetCurrent(this.Page) != null && System.Web.UI.ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "error", strScript, true);
                }

                if (!this.Page.IsPostBack)
                {
                    this.LoadControls();

                    if (Request.QueryString["IdConsulta"] != null)
                    {
                        this.LoadFormConsulta(Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"])));

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
        /// Método, Carga los controles iniciales
        /// </summary>
        public void LoadControls()
        {
            EmpresaParentescos objEmpresaParentescos = new EmpresaParentescos();
            SIC_BENEFICIARIO objBeneficiario = new SIC_BENEFICIARIO();
            SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();
            DataTable dtParentescos;
            DataTable dtBeneficiarios;
            int IdSede = 0;

            //Inicio MAHG 12/01/10
            //Se agrega el atributo readonly 
            txtPrestador.Attributes.Add("ReadOnly", "ReadOnly");
            txtSolicitanteTranscripcion.Attributes.Add("ReadOnly", "ReadOnly");
            txtIMC.Attributes.Add("ReadOnly", "ReadOnly");
            txtTensionMedia.Attributes.Add("ReadOnly", "ReadOnly");
            //Inicio PETF 14/01/10
            txtFechaControl.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaPapanicolauMicro.Attributes.Add("ReadOnly", "ReadOnly");
            //Fin PETF 14/01/10
            //Fin MAHG 12/01/10

            ViewState["FechaInicioConsulta"] = DateTime.Now;
            //HC-New functionalities-004, PORTOMX, RAM, 15/09/2015
            this.FillList("TipoConsultas", "TipoConsulta", "Orden", "Activo = 1", this.rblTipoConsulta);
            this.FillList("TipoEnfermedades", "TipoEnfermedad", this.rblTipoEnfermedad);
            this.FillListActivos("Prestadores", "Prestador", Convert.ToInt32(Session["Company"]), this.ddlPrestador, "--Solicitante--");
            this.FillListActivos("Proveedores", "Proveedor", Convert.ToInt32(Session["Company"]), this.ddlSolicitanteTranscripcion, "--Solicitante Transcripción--");
            this.FillList(this.ddlSede, "SedeEmpresa", Convert.ToInt32(Session["Company"]), true);
            this.FillList(this.ddlLineaNegocio, "LineasNegocioEmpresa", Convert.ToInt32(Session["Company"]), true);
            this.ddlSede.Items.Insert(0, new ListItem("--Sedes--", "0"));
            this.rblTipoConsulta.Attributes.Add("OnClick", "HabilitarValidadores(this);");
            this.ddlLineaNegocio.Items.Insert(0, new ListItem("--Lineas Negocio--", "0"));

            if (this.ddlLineaNegocio.Items.Count > 1)
            {
                this.lblLineaNegocio.Visible = true;
                this.ddlLineaNegocio.Visible = true;
            }

            //Cargar Sede de la Cita en caso que venga de la agenda
            if (Request.QueryString["cita_id"] != null && Request.QueryString["cita_id"] != string.Empty)
            {
                Consulta objConsultaCita = new Consulta();
                objConsultaCita.cita_id = Convert.ToInt32(Request.QueryString["cita_id"]);
                IdSede = objConsultaCita.GetIdSedeCita();

                if (IdSede > 0)
                {
                    this.ddlSede.SelectedValue = IdSede.ToString();
                }
            }

            if (this.ddlPrestador.Items.Count > 20)
            {
                this.txtPrestador.Visible = true;
                this.btnBuscarPrestador.Style["display"] = "";
                this.ddlPrestador.Visible = false;
                this.lblValidadorMedico.Visible = true;
                this.rfvSolicitante.Enabled = true;
            }
            else
            {
                this.txtPrestador.Visible = false;
                this.btnBuscarPrestador.Style["display"] = "none";
                this.ddlPrestador.Visible = true;
                this.lblValidadorMedico.Visible = true;
                this.cmvSolicitante.Enabled = true;
            }
            if (this.ddlSolicitanteTranscripcion.Items.Count > 20)
            {
                this.txtSolicitanteTranscripcion.Visible = true;
                this.btnBuscarSolicitanteTranscripcion.Style["display"] = "";
                this.ddlSolicitanteTranscripcion.Visible = false;
                this.rfvSolicitanteTranscripcion.Enabled = true;
                this.btnBuscarSolicitanteTranscripcion.Attributes.Add("OnClick", "javascript:ShowProveedor(this,'" + this.txtIdSolicitanteTranscripcion.ClientID + "',0,'" + this.txtSolicitanteTranscripcion.ClientID + "');");
            }
            else
            {
                this.txtSolicitanteTranscripcion.Visible = false;
                this.btnBuscarSolicitanteTranscripcion.Style["display"] = "none";
                this.ddlSolicitanteTranscripcion.Visible = true;
                this.cmvSolicitanteTranscripcion.Enabled = true;
            }
            if (Session["IdPrestador"] != null && Convert.ToInt32(Session["IdPrestador"]) != 0)
            {
                this.txtPrestador.Visible = false;
                this.btnBuscarPrestador.Style["display"] = "none";
                this.ddlPrestador.Visible = false;
                this.lblValidadorMedico.Visible = false;
                GeneralTable objGeneral = new GeneralTable();
                objGeneral.TableName = "Prestadores";
                objGeneral.ColumnName = "Prestador";
                objGeneral.Id = Convert.ToInt32(Session["IdPrestador"]);
                objGeneral.GetGeneralTable();
                this.lblSolicitante.Text = objGeneral.Nombre;
                this.rfvSolicitante.Enabled = false;
                this.cmvSolicitante.Enabled = false;
            }

            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];

            EmpresaDatos objEmpresaDatos = new EmpresaDatos();
            objEmpresaDatos.Empresa_id = Convert.ToInt32(Session["Company"]);
            objEmpresaDatos.GetEmpresaDatos();

            this.ddlUsuario.Items.Add(new ListItem("--Paciente--", "-1"));

            foreach (DataRow row in dtParentescos.Rows)
            {
                if (row["IdParentesco"].ToString() == "0")
                {
                    objEmpleado.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
                    objEmpleado.IdPlanMedicamentos = objEmpresaDatos.IdPlanMedicamentos;
                    objEmpleado.GetSIC_EMPLEADO();
                    this.ddlUsuario.Items.Add(new ListItem("TITU-" + objEmpleado.Nombre_completo, "0"));

                    if (objEmpleado.Sexo == 2)
                        this.txtGenero.Text = "1";
                    else
                        this.txtGenero.Text = "";
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

                        if (Request.QueryString["beneficiario_id"] != null && Request.QueryString["beneficiario_id"] != string.Empty && Request.QueryString["beneficiario_id"] == rowBeneficiario["beneficiario_id"].ToString())
                        {
                            if (rowBeneficiario["Genero"].ToString() == "2")
                                this.txtGenero.Text = "1";
                            else
                                this.txtGenero.Text = "";

                        }
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
            //Cargar controles de pruebas biométricas JOHN PORTELA 25/02/2010.
            this.FillList(this.ddlResultadoMorfologico, "PreguntaRespuesta", "--Resultado--", 1);
            this.FillList(this.ddlAnormalCelulasEpi, "PreguntaRespuesta", "--Resultado--", 3);
            this.FillList(this.ddlCelulasEscamosas, "PreguntaRespuesta", "--Resultado--", 4);
            this.FillList(this.ddlMamografia, "PreguntaRespuesta", "--Resultado--", 11);
            //Cargar divisiones del formulario JOHN PORTELA 04/03/2010.
            this.CargarDivisiones();


            //Realizar la carga de la consulta anterior para algunos campos

            Consulta objConsulta = new Consulta();
            if (Request.QueryString["beneficiario_id"] != null && Request.QueryString["beneficiario_id"] != string.Empty)
                objConsulta.GetUltimaConsulta(Convert.ToInt64(Request.QueryString["employee_id"]), Convert.ToInt64(Request.QueryString["beneficiario_id"]));
            else
                objConsulta.GetUltimaConsulta(Convert.ToInt64(Request.QueryString["employee_id"]), 0);

            /// <summary>
            /// Cambio: Realiza la carga de campos de la última consulta, si se obtiene una consulta anterior se carga la información
            /// Autor: Adriana Diazgranados
            /// Fecha: 10/07/2012
            /// </summary>            
            if (objConsulta.IdConsulta != 0)
            {
                this.lblUltimaConsulta.Text = "Última Consulta - Fecha:" + objConsulta.FechaCreacion.ToShortDateString() + "  Médico Atención:" + objConsulta.NombrePrestador;
                this.lblUltimaConsulta1.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta2.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta3.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta4.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta5.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta6.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta7.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta8.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta9.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta10.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta11.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta12.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta13.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta14.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();
                this.lblUltimaConsulta15.Text = "Última consulta:" + objConsulta.FechaCreacion.ToShortDateString();

                if (objConsulta.Medicos != string.Empty)
                    this.txtAnteMedicos.Text = objConsulta.Medicos + " // ";
                if (objConsulta.Quirurgicos != string.Empty)
                    this.txtAnteQuirurgicos.Text = objConsulta.Quirurgicos + " // ";
                if (objConsulta.Motivo != string.Empty)
                    this.txtMotivo.Text = objConsulta.Motivo + " // ";
                if (objConsulta.ToxicoAlergicos != string.Empty)
                    this.txtAnteToxico.Text = objConsulta.ToxicoAlergicos + " // ";
                if (objConsulta.Ginecobstetricos != string.Empty)
                    this.txtAnteGineco.Text = objConsulta.Ginecobstetricos + " // ";
                if (objConsulta.Transfusionales != string.Empty)
                    this.txtAnteTransfusionales.Text = objConsulta.Transfusionales + " // ";
                if (objConsulta.Farmacologicos != string.Empty)
                    this.txtAnteFarmacologicos.Text = objConsulta.Farmacologicos + " // ";
                if (objConsulta.OtrosAntecedentes != string.Empty)
                    this.txtAnteOtros.Text = objConsulta.OtrosAntecedentes + " // ";
                if (objConsulta.Familiares != string.Empty)
                    this.txtAnteFamiliares.Text = objConsulta.Familiares + " // ";
                if (objConsulta.PlanTratamiento != string.Empty)
                    this.txtPlan.Text = objConsulta.PlanTratamiento + " // ";
                if (objConsulta.Peso != 0)
                    this.txtPeso.Text = string.Format("{0:N2}", objConsulta.Peso);
                if (objConsulta.Talla != 0)
                    this.txtTalla.Text = string.Format("{0:N2}", objConsulta.Talla);
                if (objConsulta.IndiceMasaCorporal != 0)
                    this.txtIMC.Text = string.Format("{0:N2}", objConsulta.IndiceMasaCorporal);
                this.txtSistolica.Text = objConsulta.TensionArterialSistolica;
                this.txtDiastolica.Text = objConsulta.TensionArterialDiastolica;

                /// <summary>
                /// Cambio: Nuevos campos con precarga de última consulta
                /// Autor: Adriana Diazgranados
                /// Fecha: 20/09/2012
                /// </summary>  
                this.txtMenarquia.Text = objConsulta.Menarquia;
                this.txtFechaUltimaMestruacion.Text = objConsulta.FechaUltimaMestruacion;
                if (objConsulta.Gestaciones != -1)
                    this.txtGestaciones.Text = objConsulta.Gestaciones.ToString();
                if (objConsulta.Partos != -1)
                    this.txtPartos.Text = objConsulta.Partos.ToString();
                if (objConsulta.Cesareas != -1)
                    this.txtCesareas.Text = objConsulta.Cesareas.ToString();
                if (objConsulta.Abortos != -1)
                    this.txtAbortos.Text = objConsulta.Abortos.ToString();
                if (objConsulta.Vivos != -1)
                    this.txtVivos.Text = objConsulta.Vivos.ToString();

                //Cargar los diagnosticos de la ultima consulta
                this.WC_AdicionarDiagnosticoConsulta1.LoadControlDiagnosticos(objConsulta.IdConsulta);
            }
            if (objConsulta != null && objConsulta.IdTipoConsulta > 0)
                Session["idTipoConsulta"] = objConsulta.IdTipoConsulta.ToString();

            //Prototipo0-DMA-10/09/2018-Historia Laboral - Ini
            this.FillList(this.ddlGirosEmpresa1, "GirosEmpresas", "--Resultado--", null);
            this.FillList(this.ddlGirosEmpresa2, "GirosEmpresas", "--Resultado--", null);
            this.FillList(this.ddlGirosEmpresa3, "GirosEmpresas", "--Resultado--", null);
            this.FillList(this.ddlGirosEmpresa4, "GirosEmpresas", "--Resultado--", null);
            this.FillList(this.ddlGirosEmpresa5, "GirosEmpresas", "--Resultado--", null);
            //Prototipo0-DMA-10/09/2018-Historia Laboral - Ini

        }

        /// <summary>
        /// Método, Carga una consulta en el forma
        /// </summary>
        public void LoadFormConsulta(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.GetConsulta();
            Session["idTipoConsulta"] = objConsulta.IdTipoConsulta.ToString();
            this.tblDatosConsulta.Style["display"] = "";
            ViewState["IdSolicitud"] = objConsulta.IdSolicitud;
            this.lblNoConsulta.Text = objConsulta.ConsecutivoNombre;
            this.lblFechaCreacion.Text = objConsulta.FechaCreacion.ToShortDateString();
            if (objConsulta.Usuario_idCreacion != 0)
            {
                SIC_USUARIO objUsuario = new SIC_USUARIO();
                objUsuario.Usuario_id = objConsulta.Usuario_idCreacion;
                DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
                this.lblUsuarioCreacion.Text = dtUsuario.Rows[0]["nombre"].ToString();
            }
            else
            {
                this.lblUsuarioCreacion.Text = objConsulta.NameUser;
            }

            if (this.ddlPrestador.Visible)
            {
                this.ddlPrestador.SelectedValue = objConsulta.IdPrestador.ToString();
            }
            if (this.txtPrestador.Visible)
            {
                this.txtIdPrestador.Text = objConsulta.IdPrestador.ToString();
                GeneralTable objGeneral = new GeneralTable();
                objGeneral.TableName = "Prestadores";
                objGeneral.ColumnName = "Prestador";
                objGeneral.Id = objConsulta.IdPrestador;
                objGeneral.GetGeneralTable();
                this.txtPrestador.Text = objGeneral.Nombre;
            }
            if (Session["IdPrestador"] != null && Convert.ToInt32(Session["IdPrestador"]) != 0)
            {
                GeneralTable objGeneral = new GeneralTable();
                objGeneral.TableName = "Prestadores";
                objGeneral.ColumnName = "Prestador";
                objGeneral.Id = objConsulta.IdPrestador;
                objGeneral.GetGeneralTable();
                this.lblSolicitante.Text = objGeneral.Nombre;
            }
            if (objConsulta.IdTipoConsulta == Convert.ToInt32(Consulta.EnumTiposConsulta.Transcripcion))
            {
                this.trSolicitanteTranscripcion.Style["display"] = "";
                this.trComentariosTranscripcion.Style["display"] = "";
                this.rfvSolicitanteTranscripcion.Enabled = true;
                this.cmvSolicitanteTranscripcion.Enabled = true;

                if (this.ddlSolicitanteTranscripcion.Visible)
                {
                    this.ddlSolicitanteTranscripcion.SelectedValue = objConsulta.IdProveedorTranscripcion.ToString();
                }
                if (this.txtSolicitanteTranscripcion.Visible)
                {
                    this.txtIdSolicitanteTranscripcion.Text = objConsulta.IdProveedorTranscripcion.ToString();
                    GeneralTable objGeneral = new GeneralTable();
                    objGeneral.TableName = "Proveedores";
                    objGeneral.ColumnName = "Proveedor";
                    objGeneral.Id = objConsulta.IdProveedorTranscripcion;
                    objGeneral.GetGeneralTable();
                    this.txtSolicitanteTranscripcion.Text = objGeneral.Nombre;
                }
            }
            else
            {
                this.trSolicitanteTranscripcion.Style["display"] = "none";
                this.trComentariosTranscripcion.Style["display"] = "none";
                this.rfvSolicitanteTranscripcion.Enabled = false;
                this.cmvSolicitanteTranscripcion.Enabled = false;
            }
            if (objConsulta.Beneficiario_id != 0)
                this.ddlUsuario.SelectedValue = objConsulta.Beneficiario_id.ToString();
            else
                this.ddlUsuario.SelectedValue = "0";
            this.ddlSede.SelectedValue = objConsulta.sede_id.ToString();
            this.ddlLineaNegocio.SelectedValue = objConsulta.IdLineaNegocio.ToString();
            this.rblTipoConsulta.SelectedValue = objConsulta.IdTipoConsulta.ToString();
            this.rblTipoEnfermedad.SelectedValue = objConsulta.IdTipoEnfermedad.ToString();
            this.txtMotivo.Text = objConsulta.Motivo;
            this.txtContrareferencia.Text = objConsulta.Contrarreferencia;
            this.txtEnfermedad.Text = objConsulta.EnfermedadActual;
            this.txtObservaciones.Text = objConsulta.ObservacionesGenerales;
            this.txtPlan.Text = objConsulta.PlanTratamiento;
            this.txtComentariosTranscripcion.Text = objConsulta.ComentariosTranscripcion;
            this.txtExamenesLaboratorio.Text = objConsulta.ExamenesLaboratorio;

            if (objConsulta.CitaControl.ToShortDateString() != "01/01/0001")
                this.txtFechaControl.Text = objConsulta.CitaControl.ToShortDateString();

            //Antecedentes
            this.txtAnteMedicos.Text = objConsulta.Medicos;
            this.txtAnteQuirurgicos.Text = objConsulta.Quirurgicos;
            this.txtAnteGineco.Text = objConsulta.Ginecobstetricos;
            this.txtAnteTransfusionales.Text = objConsulta.Transfusionales;
            this.txtAnteToxico.Text = objConsulta.ToxicoAlergicos;
            this.txtAnteFarmacologicos.Text = objConsulta.Farmacologicos;
            this.txtAnteOtros.Text = objConsulta.OtrosAntecedentes;
            this.txtAnteFamiliares.Text = objConsulta.Familiares;
            this.txtMenarquia.Text = objConsulta.Menarquia;
            this.txtFechaUltimaMestruacion.Text = objConsulta.FechaUltimaMestruacion;
            if (objConsulta.Gestaciones != -1)
                this.txtGestaciones.Text = objConsulta.Gestaciones.ToString();
            if (objConsulta.Partos != -1)
                this.txtPartos.Text = objConsulta.Partos.ToString();
            if (objConsulta.Cesareas != -1)
                this.txtCesareas.Text = objConsulta.Cesareas.ToString();
            if (objConsulta.Abortos != -1)
                this.txtAbortos.Text = objConsulta.Abortos.ToString();
            if (objConsulta.Vivos != -1)
                this.txtVivos.Text = objConsulta.Vivos.ToString();

            this.chkAnteMedicos.Checked = objConsulta.NormalMedicos;
            this.chkAnteQuirurgicos.Checked = objConsulta.NormalQuirurgicos;
            this.chkAnteGineco.Checked = objConsulta.NormalGinecobstetricos;
            this.chkAnteTransfusionales.Checked = objConsulta.NormalTransfusionales;
            this.chkAnteToxico.Checked = objConsulta.NormalToxicoAlergicos;
            this.chkAnteFarmacologicos.Checked = objConsulta.NormalFarmacologicos;
            this.chkRiesgoCardiovascular.Checked = objConsulta.RiesgoCardiovascular;
            this.chkAnteOtros.Checked = objConsulta.NormalOtrosAntecedentes;
            this.chkAnteFamiliares.Checked = objConsulta.NormalFamiliares;

            //Revisión por sistemas
            this.txtSisGeneral.Text = objConsulta.AspectoGeneral;
            this.txtSisCabeza.Text = objConsulta.Cabeza;
            this.txtSisCuello.Text = objConsulta.Cuello;
            this.txtSisTorax.Text = objConsulta.Torax;
            this.txtSisAbdomen.Text = objConsulta.Abdomen;
            this.txtSisOtros.Text = objConsulta.Otros;
            this.chkSisGeneral.Checked = objConsulta.NormalAspectoGeneral;
            this.chkSisCabeza.Checked = objConsulta.NormalCabeza;
            this.chkSisCuello.Checked = objConsulta.NormalCuello;
            this.chkSisTorax.Checked = objConsulta.NormalTorax;
            this.chkSisAbdomen.Checked = objConsulta.NormalAbdomen;
            this.chkSisOtros.Checked = objConsulta.NormalOtros;

            //Examenen físico
            this.txtComentariosFisico.Text = objConsulta.ComentariosExamenFisico;
            this.txtTension.Text = objConsulta.TensionArterial;
            this.txtTensionMedia.Text = objConsulta.TensionArterial;
            this.txtSistolica.Text = objConsulta.TensionArterialSistolica;
            this.txtDiastolica.Text = objConsulta.TensionArterialDiastolica;
            if (objConsulta.Peso != 0)
                this.txtPeso.Text = string.Format("{0:N2}", objConsulta.Peso);
            if (objConsulta.Talla != 0)
                this.txtTalla.Text = string.Format("{0:N2}", objConsulta.Talla);
            if (objConsulta.IndiceMasaCorporal != 0)
                this.txtIMC.Text = string.Format("{0:N2}", objConsulta.IndiceMasaCorporal);
            if (objConsulta.FrecuenciaCardiaca != 0)
                this.txtFrecuenciaCar.Text = objConsulta.FrecuenciaCardiaca.ToString();
            if (objConsulta.FrecuenciaRespiratoria != 0)
                this.txtFrecuenciaRes.Text = objConsulta.FrecuenciaRespiratoria.ToString();
            if (objConsulta.PerimetroAbdominal != 0)
                this.txtPerimetroAbdominal.Text = string.Format("{0:N2}", objConsulta.PerimetroAbdominal);
            if (objConsulta.Temperatura != 0)
                this.txtTemperatura.Text = string.Format("{0:N2}", objConsulta.Temperatura);
            this.txtFisGeneral.Text = objConsulta.ExamenAspectoGeneral;
            this.txtFisCabeza.Text = objConsulta.ExamenCabeza;
            this.txtFisCuello.Text = objConsulta.ExamenCuello;
            this.txtFisTorax.Text = objConsulta.ExamenTorax;
            this.txtFisAbdomen.Text = objConsulta.ExamenAbdomen;
            this.txtFisOtros.Text = objConsulta.ExamenOtros;
            this.txtFisPielFanelas.Text = objConsulta.ExamenPielFanelas;
            this.txtFisConjuntiva.Text = objConsulta.ExamenConjuntivaOcular;
            this.txtFisReflejo.Text = objConsulta.ExamenReflejoCorneal;
            this.txtFisPupilas.Text = objConsulta.ExamenPupilas;
            this.txtFisOidos.Text = objConsulta.ExamenOidos;
            this.txtFisOtoscopia.Text = objConsulta.ExamenOtoscopia;
            this.txtFisRinoscopia.Text = objConsulta.ExamenRinoscopia;
            this.txtFisBocaFaringe.Text = objConsulta.ExamenBocaFaringe;
            this.txtFisAmigdalas.Text = objConsulta.ExamenAmigdalas;
            this.txtFisTiroides.Text = objConsulta.ExamenTiroides;
            this.txtFisAdenopatias.Text = objConsulta.ExamenAdenopatias;
            this.txtFisRuidosCardiacos.Text = objConsulta.ExamenRuidosCardiacos;
            this.txtFisRuidosRespiratorios.Text = objConsulta.ExamenRuidosRespiratorios;
            this.txtFisPalpacionAbdomen.Text = objConsulta.ExamenPalpacionAbdomen;
            this.txtFisGenitales.Text = objConsulta.ExamenGenitalesExternos;
            this.txtFisHernias.Text = objConsulta.ExamenHernias;
            this.txtFisColumna.Text = objConsulta.ExamenColumnaVertebral;
            this.txtFisExtremidadesSuperiores.Text = objConsulta.ExamenExtremidadesSuperiores;
            this.txtFisExtremidadesInferiores.Text = objConsulta.ExamenExtremidadesInferiores;
            this.txtFisVarices.Text = objConsulta.ExamenVarices;
            this.txtFisNeurologico.Text = objConsulta.ExamenNeurologico;
            this.chkFisGeneral.Checked = objConsulta.ExamenNormalAspectoGeneral;
            this.chkFisCabeza.Checked = objConsulta.ExamenNormalCabeza;
            this.chkFisCuello.Checked = objConsulta.ExamenNormalCuello;
            this.chkFisTorax.Checked = objConsulta.ExamenNormalTorax;
            this.chkFisAbdomen.Checked = objConsulta.ExamenNormalAbdomen;
            this.chkFisOtros.Checked = objConsulta.ExamenNormalOtros;
            this.chkFisPielFanelas.Checked = objConsulta.ExamenNormalPielFanelas;
            this.chkFisConjuntiva.Checked = objConsulta.ExamenNormalConjuntivaOcular;
            this.chkFisReflejo.Checked = objConsulta.ExamenNormalReflejoCorneal;
            this.chkFisPupilas.Checked = objConsulta.ExamenNormalPupilas;
            this.chkFisOidos.Checked = objConsulta.ExamenNormalOidos;
            this.chkFisOtoscopia.Checked = objConsulta.ExamenNormalOtoscopia;
            this.chkFisRinoscopia.Checked = objConsulta.ExamenNormalRinoscopia;
            this.chkFisBocaFaringe.Checked = objConsulta.ExamenNormalBocaFaringe;
            this.chkFisAmigdalas.Checked = objConsulta.ExamenNormalAmigdalas;
            this.chkFisTiroides.Checked = objConsulta.ExamenNormalTiroides;
            this.chkFisAdenopatias.Checked = objConsulta.ExamenNormalAdenopatias;
            this.chkFisRuidosCardiacos.Checked = objConsulta.ExamenNormalRuidosCardiacos;
            this.chkFisRuidosRespiratorios.Checked = objConsulta.ExamenNormalRuidosRespiratorios;
            this.chkFisPalpacionAbdomen.Checked = objConsulta.ExamenNormalPalpacionAbdomen;
            this.chkFisGenitales.Checked = objConsulta.ExamenNormalGenitalesExternos;
            this.chkFisHernias.Checked = objConsulta.ExamenNormalHernias;
            this.chkFisColumna.Checked = objConsulta.ExamenNormalColumnaVertebral;
            this.chkFisExtremidadesSuperiores.Checked = objConsulta.ExamenNormalExtremidadesSuperiores;
            this.chkFisExtremidadesInferiores.Checked = objConsulta.ExamenNormalExtremidadesInferiores;
            this.chkFisVarices.Checked = objConsulta.ExamenNormalVarices;
            this.chkFisNeurologico.Checked = objConsulta.ExamenNormalNeurologico;

            //Habitos
            this.txtFrecuenciaAlcohol.Text = objConsulta.FrecuenciaConsumo;
            this.txtFrecuenciaTabaquismo.Text = objConsulta.FrecuenciaTabaquismo;
            this.txtVacunacion.Text = objConsulta.Vacunacion;
            if (objConsulta.Tabaquismo != -1)
                this.rblTabaquismo.SelectedValue = objConsulta.Tabaquismo.ToString();
            if (objConsulta.ConsumoAlcohol != -1)
                this.rblAlcohol.SelectedValue = objConsulta.ConsumoAlcohol.ToString();
            if (objConsulta.ActividadDeportiva != -1)
                this.rblDeportiva.SelectedValue = objConsulta.ActividadDeportiva.ToString();

            //Diagnosticos
            this.WC_AdicionarDiagnosticoConsulta1.LoadControlDiagnosticos(p_idConsulta);

            if (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty)
            {
                DisableTextBoxesRecursive(Page);

                this.imgCalendario.Style["display"] = "none";
                this.imgPapanicolau.Style["display"] = "none";
                this.txtObservaciones.ReadOnly = false;
                this.txtObservaciones.CssClass = "textBox";
                this.btnCancelar.Visible = true;
                this.btnGuardar.Visible = true;
            }
            //PRUEBAS BIOMÉTRICAS 09/03/2010
            if (objConsulta.ColesterolTotal != 0)
                txtColesterolTotal.Text = objConsulta.ColesterolTotal.ToString();
            if (objConsulta.ColesterolHDL != 0)
                txtColesterolHDL.Text = objConsulta.ColesterolHDL.ToString();
            if (objConsulta.ColesterolHDLmmol != 0)
                txtColesterolHDLmmol.Text = string.Format("{0:N2}", objConsulta.ColesterolHDLmmol);
            if (objConsulta.ColesterolLDL != 0)
                txtColesterolLDL.Text = objConsulta.ColesterolLDL.ToString();
            if (objConsulta.Trigliceridos != 0)
                txtTrigliceridos.Text = objConsulta.Trigliceridos.ToString();
            if (objConsulta.IndiceAterogenico != 0)
                txtIndiceAterogenico.Text = string.Format("{0:N1}", objConsulta.IndiceAterogenico);
            if (objConsulta.AntigenoProstata != 0)
                txtAntigenoProstata.Text = string.Format("{0:N2}", objConsulta.AntigenoProstata); ;
            if (objConsulta.GlucemiaAyunas != 0)
                txtGlucemiaAyunas.Text = objConsulta.GlucemiaAyunas.ToString();
            /// <summary>
            /// Cambio: Permite cargar los resultados de la glicemia sin ayunas
            /// Autor: Ricardo Silva
            /// Fecha: 11/07/2012
            /// </summary>
            if (objConsulta.GlucemiaSinAyunas != 0)
                txtGlicemiaSinAyunas.Text = objConsulta.GlucemiaSinAyunas.ToString();
            if (objConsulta.HemoglobinaGlucosilada != 0)
                txtHemoglobinaGlucosilada.Text = string.Format("{0:N1}", objConsulta.HemoglobinaGlucosilada);
            if (objConsulta.Homocisteina != 0)
                txtHomocisteina.Text = string.Format("{0:N2}", objConsulta.Homocisteina);
            if (objConsulta.PresenciaMicroorganismos != -1)
                rblPresenciaMicroorganismos.SelectedValue = objConsulta.PresenciaMicroorganismos.ToString();
            if (objConsulta.FechaPapanicolauMicro.ToShortDateString() != "01/01/0001")
                txtFechaPapanicolauMicro.Text = objConsulta.FechaPapanicolauMicro.ToShortDateString();
            if (objConsulta.ObservacionesPresenciaMicro != string.Empty)
                txtObservacionesPresenciaMicro.Text = objConsulta.ObservacionesPresenciaMicro;

            if (objConsulta.ResultadoMorfologico != 0)
                ddlResultadoMorfologico.SelectedValue = objConsulta.ResultadoMorfologico.ToString();
            if (objConsulta.AnormalidadCelulasEpiteliales != 0)
            {
                ddlAnormalCelulasEpi.Visible = true;
                ddlAnormalCelulasEpi.SelectedValue = objConsulta.AnormalidadCelulasEpiteliales.ToString();
            }
            else
            {
                ddlAnormalCelulasEpi.Visible = false;
            }
            if (objConsulta.CelulasEscamosasAtipicas != 0)
            {
                ddlCelulasEscamosas.Visible = true;
                ddlCelulasEscamosas.SelectedValue = objConsulta.CelulasEscamosasAtipicas.ToString();
            }
            else
            {
                ddlCelulasEscamosas.Visible = false;
            }
            if (objConsulta.Mamografia != 0)
                ddlMamografia.SelectedValue = objConsulta.Mamografia.ToString();
            if (objConsulta.MamografiaObservaciones != string.Empty)
                txtMamografiaObservaciones.Text = objConsulta.MamografiaObservaciones;
            if (objConsulta.Audiometria != -1)
                rblAudiometria.SelectedValue = objConsulta.Audiometria.ToString();
            if (objConsulta.AudiometriaObservaciones != string.Empty)
                txtAudiometriaObservaciones.Text = objConsulta.AudiometriaObservaciones;
            if (objConsulta.RayosX != -1)
                rblRayosX.SelectedValue = objConsulta.RayosX.ToString();
            if (objConsulta.RayosXObservaciones != string.Empty)
                txtRayosXObservaciones.Text = objConsulta.RayosXObservaciones;
            chkMiopia.Checked = objConsulta.Miopia;
            if (objConsulta.MiopiaValor != 0)
                txtMiopiaValor.Text = string.Format("{0:N2}", objConsulta.MiopiaValor);
            if (objConsulta.MiopiaValorOI != 0)
                txtMiopiaValorOI.Text = string.Format("{0:N2}", objConsulta.MiopiaValorOI);
            if (objConsulta.MiopiaObservaciones != string.Empty)
                txtMiopiaObservaciones.Text = objConsulta.MiopiaObservaciones;
            chkAstigmatismo.Checked = objConsulta.Astigmatismo;
            if (objConsulta.AstigmatismoValor != 0)
                txtAstigmatismoValor.Text = string.Format("{0:N2}", objConsulta.AstigmatismoValor);
            if (objConsulta.AstigmatismoValorOI != 0)
                txtAstigmatismoValorOI.Text = string.Format("{0:N2}", objConsulta.AstigmatismoValorOI);
            if (objConsulta.AstigmatismoObservaciones != string.Empty)
                txtAstigmatismoObservaciones.Text = objConsulta.AstigmatismoObservaciones;
            chkHipermetropia.Checked = objConsulta.Hipermetropia;
            if (objConsulta.HipermetropiaValor != 0)
                txtHipermetropiaValor.Text = string.Format("{0:N2}", objConsulta.HipermetropiaValor);
            if (objConsulta.HipermetropiaValorOI != 0)
                txtHipermetropiaValorOI.Text = string.Format("{0:N2}", objConsulta.HipermetropiaValorOI);
            if (objConsulta.HipermetropiaObservaciones != string.Empty)
                txtHipermetropiaObservaciones.Text = objConsulta.HipermetropiaObservaciones;
            chkPresbicia.Checked = objConsulta.Presbicia;
            if (objConsulta.PresbiciaValor != 0)
                txtPresbiciaValor.Text = string.Format("{0:N2}", objConsulta.PresbiciaValor);
            if (objConsulta.PresbiciaValorOI != 0)
                txtPresbiciaValorOI.Text = string.Format("{0:N2}", objConsulta.PresbiciaValorOI);
            if (objConsulta.PresbiciaObservaciones != string.Empty)
                txtPresbiciaObservaciones.Text = objConsulta.PresbiciaObservaciones;
            chkOtros.Checked = objConsulta.OtrosExamenVisual;
            this.WC_AdicionarDiagnosticoConsultaCIE102.LoadControlDiagnosticos(objConsulta.IdDiagnosticoExamenVisual);


            /// Prototipo0-RHT-03/10/2018 Historia Laboral 
            /// Empresa
            if (objConsulta.HistLabIdGirosEmpresa1 != 0)
                ddlGirosEmpresa1.SelectedValue = objConsulta.HistLabIdGirosEmpresa1.ToString();
            if (objConsulta.HistLabAniosEmpresa1 != 0)
                this.txtAniosEmpresa1.Text = (objConsulta.HistLabAniosEmpresa1).ToString();
            if (objConsulta.HistLabMesesEmpresa1 != 0)
                this.txtMesesEmpresa1.Text = (objConsulta.HistLabMesesEmpresa1).ToString();
            if (objConsulta.HistLabPuestoEmpresa1 != string.Empty)
                this.txtPuestoEmpresa1.Text = objConsulta.HistLabPuestoEmpresa1;
            if (objConsulta.HistLabIdGirosEmpresa2 != 0)
                ddlGirosEmpresa2.SelectedValue = objConsulta.HistLabIdGirosEmpresa2.ToString();
            if (objConsulta.HistLabAniosEmpresa2 != 0)
                this.txtAniosEmpresa2.Text = (objConsulta.HistLabAniosEmpresa2).ToString();
            if (objConsulta.HistLabMesesEmpresa2 != 0)
                this.txtMesesEmpresa2.Text = (objConsulta.HistLabMesesEmpresa2).ToString();
            if (objConsulta.HistLabPuestoEmpresa2 != string.Empty)
                this.txtPuestoEmpresa2.Text = objConsulta.HistLabPuestoEmpresa2;
            if (objConsulta.HistLabIdGirosEmpresa3 != 0)
                ddlGirosEmpresa3.SelectedValue = objConsulta.HistLabIdGirosEmpresa3.ToString();
            if (objConsulta.HistLabAniosEmpresa3 != 0)
                this.txtAniosEmpresa3.Text = (objConsulta.HistLabAniosEmpresa3).ToString();
            if (objConsulta.HistLabMesesEmpresa3 != 0)
                this.txtMesesEmpresa3.Text = (objConsulta.HistLabMesesEmpresa3).ToString();
            if (objConsulta.HistLabPuestoEmpresa3 != string.Empty)
                this.txtPuestoEmpresa3.Text = objConsulta.HistLabPuestoEmpresa3;
            if (objConsulta.HistLabIdGirosEmpresa4 != 0)
                ddlGirosEmpresa4.SelectedValue = objConsulta.HistLabIdGirosEmpresa4.ToString();
            if (objConsulta.HistLabAniosEmpresa4 != 0)
                this.txtAniosEmpresa4.Text = (objConsulta.HistLabAniosEmpresa4).ToString();
            if (objConsulta.HistLabMesesEmpresa4 != 0)
                this.txtMesesEmpresa4.Text = (objConsulta.HistLabMesesEmpresa4).ToString();
            if (objConsulta.HistLabPuestoEmpresa4 != string.Empty)
                this.txtPuestoEmpresa4.Text = objConsulta.HistLabPuestoEmpresa4;
            if (objConsulta.HistLabIdGirosEmpresa5 != 0)
                ddlGirosEmpresa5.SelectedValue = objConsulta.HistLabIdGirosEmpresa5.ToString();
            if (objConsulta.HistLabAniosEmpresa5 != 0)
                this.txtAniosEmpresa5.Text = (objConsulta.HistLabAniosEmpresa5).ToString();
            if (objConsulta.HistLabMesesEmpresa5 != 0)
                this.txtMesesEmpresa5.Text = (objConsulta.HistLabMesesEmpresa5).ToString();
            if (objConsulta.HistLabPuestoEmpresa5 != string.Empty)
                this.txtPuestoEmpresa5.Text = objConsulta.HistLabPuestoEmpresa5;


            /// Fisico 03/10/2018
            chkHistLabFisicoRuido.Checked = objConsulta.HistLabFisicoRuido;
            if (objConsulta.HistLabAniosFisicoRuido != 0)
                txtHistLabAniosFisicoRuido.Text = objConsulta.HistLabAniosFisicoRuido.ToString();
            if (objConsulta.HistLabMesesFisicoRuido != 0)
                txtHistLabMesesFisicoRuido.Text = objConsulta.HistLabMesesFisicoRuido.ToString();
            if (objConsulta.HistLabComentariosFisicoRuido != string.Empty)
                txtHistLabComentariosFisicoRuido.Text = objConsulta.HistLabComentariosFisicoRuido;
            chkHistLabFisicoIluminacion.Checked = objConsulta.HistLabFisicoIluminacion;
            if (objConsulta.HistLabAniosFisicoIluminacion != 0)
                txtHistLabAniosFisicoIluminacion.Text = objConsulta.HistLabAniosFisicoIluminacion.ToString();
            if (objConsulta.HistLabMesesFisicoIluminacion != 0)
                txtHistLabMesesFisicoIluminacion.Text = objConsulta.HistLabMesesFisicoIluminacion.ToString();
            if (objConsulta.HistLabComentariosFisicoIluminacion != string.Empty)
                txtHistLabComentariosFisicoIluminacion.Text = objConsulta.HistLabComentariosFisicoIluminacion;
            chkHistLabFisicoVibraciones.Checked = objConsulta.HistLabFisicoVibraciones;
            if (objConsulta.HistLabAniosFisicoVibraciones != 0)
                txtHistLabAniosFisicoVibraciones.Text = objConsulta.HistLabAniosFisicoVibraciones.ToString();
            if (objConsulta.HistLabMesesFisicoVibraciones != 0)
                txtHistLabMesesFisicoVibraciones.Text = objConsulta.HistLabMesesFisicoVibraciones.ToString();
            if (objConsulta.HistLabComentariosFisicoVibraciones != string.Empty)
                txtHistLabComentariosFisicoVibraciones.Text = objConsulta.HistLabComentariosFisicoVibraciones;
            chkHistLabFisicoRadiacion.Checked = objConsulta.HistLabFisicoRadiacion;
            if (objConsulta.HistLabAniosFisicoRadiacion != 0)
                txtHistLabAniosFisicoRadiacion.Text = objConsulta.HistLabAniosFisicoRadiacion.ToString();
            if (objConsulta.HistLabMesesFisicoRadiacion != 0)
                txtHistLabMesesFisicoRadiacion.Text = objConsulta.HistLabMesesFisicoRadiacion.ToString();
            if (objConsulta.HistLabComentariosFisicoRadiacion != string.Empty)
                txtHistLabComentariosFisicoRadiacion.Text = objConsulta.HistLabComentariosFisicoRadiacion;
            chkHistLabFisicoTempExtremas.Checked = objConsulta.HistLabFisicoTempExtremas;
            if (objConsulta.HistLabAniosFisicoTempExtremas != 0)
                txtHistLabAniosFisicoTempExtremas.Text = objConsulta.HistLabAniosFisicoTempExtremas.ToString();
            if (objConsulta.HistLabMesesFisicoTempExtremas != 0)
                txtHistLabMesesFisicoTempExtremas.Text = objConsulta.HistLabMesesFisicoTempExtremas.ToString();
            if (objConsulta.HistLabComentariosFisicoTempExtremas != string.Empty)
                txtHistLabComentariosFisicoTempExtremas.Text = objConsulta.HistLabComentariosFisicoTempExtremas;
            chkHistLabFisicoOtro1.Checked = objConsulta.HistLabFisicoOtro1;
            if (objConsulta.HistLabAniosFisicoOtro1 != 0)
                txtHistLabAniosFisicoOtro1.Text = objConsulta.HistLabAniosFisicoOtro1.ToString();
            if (objConsulta.HistLabMesesFisicoOtro1 != 0)
                txtHistLabMesesFisicoOtro1.Text = objConsulta.HistLabMesesFisicoOtro1.ToString();
            if (objConsulta.HistLabComentariosFisicoOtro1 != string.Empty)
                txtHistLabComentariosFisicoOtro1.Text = objConsulta.HistLabComentariosFisicoOtro1;
            chkHistLabFisicoOtro2.Checked = objConsulta.HistLabFisicoOtro2;
            if (objConsulta.HistLabAniosFisicoOtro2 != 0)
                txtHistLabAniosFisicoOtro2.Text = objConsulta.HistLabAniosFisicoOtro2.ToString();
            if (objConsulta.HistLabMesesFisicoOtro2 != 0)
                txtHistLabMesesFisicoOtro2.Text = objConsulta.HistLabMesesFisicoOtro2.ToString();
            if (objConsulta.HistLabComentariosFisicoOtro2 != string.Empty)
                txtHistLabComentariosFisicoOtro2.Text = objConsulta.HistLabComentariosFisicoOtro2;
            chkHistLabFisicoOtro3.Checked = objConsulta.HistLabFisicoOtro3;
            if (objConsulta.HistLabAniosFisicoOtro3 != 0)
                txtHistLabAniosFisicoOtro3.Text = objConsulta.HistLabAniosFisicoOtro3.ToString();
            if (objConsulta.HistLabMesesFisicoOtro3 != 0)
                txtHistLabMesesFisicoOtro3.Text = objConsulta.HistLabMesesFisicoOtro3.ToString();
            if (objConsulta.HistLabComentariosFisicoOtro3 != string.Empty)
                txtHistLabComentariosFisicoOtro3.Text = objConsulta.HistLabComentariosFisicoOtro3;
            chkHistLabFisicoOtro4.Checked = objConsulta.HistLabFisicoOtro4;
            if (objConsulta.HistLabAniosFisicoOtro4 != 0)
                txtHistLabAniosFisicoOtro4.Text = objConsulta.HistLabAniosFisicoOtro4.ToString();
            if (objConsulta.HistLabMesesFisicoOtro4 != 0)
                txtHistLabMesesFisicoOtro4.Text = objConsulta.HistLabMesesFisicoOtro4.ToString();
            if (objConsulta.HistLabComentariosFisicoOtro4 != string.Empty)
                txtHistLabComentariosFisicoOtro4.Text = objConsulta.HistLabComentariosFisicoOtro4;
            chkHistLabFisicoOtro5.Checked = objConsulta.HistLabFisicoOtro5;
            if (objConsulta.HistLabAniosFisicoOtro5 != 0)
                txtHistLabAniosFisicoOtro5.Text = objConsulta.HistLabAniosFisicoOtro5.ToString();
            if (objConsulta.HistLabMesesFisicoOtro5 != 0)
                txtHistLabMesesFisicoOtro5.Text = objConsulta.HistLabMesesFisicoOtro5.ToString();
            if (objConsulta.HistLabComentariosFisicoOtro5 != string.Empty)
                txtHistLabComentariosFisicoOtro5.Text = objConsulta.HistLabComentariosFisicoOtro5;


            /// Quimicos 03/10/2018
            chkHistLabQuimicoPolvos.Checked = objConsulta.HistLabQuimicoPolvos;
            if (objConsulta.HistLabAniosQuimicoPolvos != 0)
                txtHistLabAniosQuimicoPolvos.Text = objConsulta.HistLabAniosQuimicoPolvos.ToString();
            if (objConsulta.HistLabMesesQuimicoPolvos != 0)
                txtHistLabMesesQuimicoPolvos.Text = objConsulta.HistLabMesesQuimicoPolvos.ToString();
            if (objConsulta.HistLabComentariosQuimicoPolvos != string.Empty)
                txtHistLabComentariosQuimicoPolvos.Text = objConsulta.HistLabComentariosQuimicoPolvos;
            chkHistLabQuimicoHumos.Checked = objConsulta.HistLabQuimicoHumos;
            if (objConsulta.HistLabAniosQuimicoHumos != 0)
                txtHistLabAniosQuimicoHumos.Text = objConsulta.HistLabAniosQuimicoHumos.ToString();
            if (objConsulta.HistLabMesesQuimicoHumos != 0)
                txtHistLabMesesQuimicoHumos.Text = objConsulta.HistLabMesesQuimicoHumos.ToString();
            if (objConsulta.HistLabComentariosQuimicoHumos != string.Empty)
                txtHistLabComentariosQuimicoHumos.Text = objConsulta.HistLabComentariosQuimicoHumos;
            chkHistLabQuimicoRociosNeblina.Checked = objConsulta.HistLabQuimicoRociosNeblina;
            if (objConsulta.HistLabAniosQuimicoRociosNeblina != 0)
                txtHistLabAniosQuimicoRociosNeblina.Text = objConsulta.HistLabAniosQuimicoRociosNeblina.ToString();
            if (objConsulta.HistLabMesesQuimicoRociosNeblina != 0)
                txtHistLabMesesQuimicoRociosNeblina.Text = objConsulta.HistLabMesesQuimicoRociosNeblina.ToString();
            if (objConsulta.HistLabComentariosQuimicoRociosNeblina != string.Empty)
                txtHistLabComentariosQuimicoRociosNeblina.Text = objConsulta.HistLabComentariosQuimicoRociosNeblina;
            chkHistLabQuimicoVapores.Checked = objConsulta.HistLabQuimicoVapores;
            if (objConsulta.HistLabAniosQuimicoVapores != 0)
                txtHistLabAniosQuimicoVapores.Text = objConsulta.HistLabAniosQuimicoVapores.ToString();
            if (objConsulta.HistLabMesesQuimicoVapores != 0)
                txtHistLabMesesQuimicoVapores.Text = objConsulta.HistLabMesesQuimicoVapores.ToString();
            if (objConsulta.HistLabComentariosQuimicoVapores != string.Empty)
                txtHistLabComentariosQuimicoVapores.Text = objConsulta.HistLabComentariosQuimicoVapores;
            chkHistLabQuimicoGases.Checked = objConsulta.HistLabQuimicoGases;
            if (objConsulta.HistLabAniosQuimicoGases != 0)
                txtHistLabAniosQuimicoGases.Text = objConsulta.HistLabAniosQuimicoGases.ToString();
            if (objConsulta.HistLabMesesQuimicoGases != 0)
                txtHistLabMesesQuimicoGases.Text = objConsulta.HistLabMesesQuimicoGases.ToString();
            if (objConsulta.HistLabComentariosQuimicoGases != string.Empty)
                txtHistLabComentariosQuimicoGases.Text = objConsulta.HistLabComentariosQuimicoGases;
            chkHistLabQuimicoOtro1.Checked = objConsulta.HistLabQuimicoOtro1;
            if (objConsulta.HistLabAniosQuimicoOtro1 != 0)
                txtHistLabAniosQuimicoOtro1.Text = objConsulta.HistLabAniosQuimicoOtro1.ToString();
            if (objConsulta.HistLabMesesQuimicoOtro1 != 0)
                txtHistLabMesesQuimicoOtro1.Text = objConsulta.HistLabMesesQuimicoOtro1.ToString();
            if (objConsulta.HistLabComentariosQuimicoOtro1 != string.Empty)
                txtHistLabComentariosQuimicoOtro1.Text = objConsulta.HistLabComentariosQuimicoOtro1;
            chkHistLabQuimicoOtro2.Checked = objConsulta.HistLabQuimicoOtro2;
            if (objConsulta.HistLabAniosQuimicoOtro2 != 0)
                txtHistLabAniosQuimicoOtro2.Text = objConsulta.HistLabAniosQuimicoOtro2.ToString();
            if (objConsulta.HistLabMesesQuimicoOtro2 != 0)
                txtHistLabMesesQuimicoOtro2.Text = objConsulta.HistLabMesesQuimicoOtro2.ToString();
            if (objConsulta.HistLabComentariosQuimicoOtro2 != string.Empty)
                txtHistLabComentariosQuimicoOtro2.Text = objConsulta.HistLabComentariosQuimicoOtro2;
            chkHistLabQuimicoOtro3.Checked = objConsulta.HistLabQuimicoOtro3;
            if (objConsulta.HistLabAniosQuimicoOtro3 != 0)
                txtHistLabAniosQuimicoOtro3.Text = objConsulta.HistLabAniosQuimicoOtro3.ToString();
            if (objConsulta.HistLabMesesQuimicoOtro3 != 0)
                txtHistLabMesesQuimicoOtro3.Text = objConsulta.HistLabMesesQuimicoOtro3.ToString();
            if (objConsulta.HistLabComentariosQuimicoOtro3 != string.Empty)
                txtHistLabComentariosQuimicoOtro3.Text = objConsulta.HistLabComentariosQuimicoOtro3;
            chkHistLabQuimicoOtro4.Checked = objConsulta.HistLabQuimicoOtro4;
            if (objConsulta.HistLabAniosQuimicoOtro4 != 0)
                txtHistLabAniosQuimicoOtro4.Text = objConsulta.HistLabAniosQuimicoOtro4.ToString();
            if (objConsulta.HistLabMesesQuimicoOtro4 != 0)
                txtHistLabMesesQuimicoOtro4.Text = objConsulta.HistLabMesesQuimicoOtro4.ToString();
            if (objConsulta.HistLabComentariosQuimicoOtro4 != string.Empty)
                txtHistLabComentariosQuimicoOtro4.Text = objConsulta.HistLabComentariosQuimicoOtro4;
            chkHistLabQuimicoOtro5.Checked = objConsulta.HistLabQuimicoOtro5;
            if (objConsulta.HistLabAniosQuimicoOtro5 != 0)
                txtHistLabAniosQuimicoOtro5.Text = objConsulta.HistLabAniosQuimicoOtro5.ToString();
            if (objConsulta.HistLabMesesQuimicoOtro5 != 0)
                txtHistLabMesesQuimicoOtro5.Text = objConsulta.HistLabMesesQuimicoOtro5.ToString();
            if (objConsulta.HistLabComentariosQuimicoOtro5 != string.Empty)
                txtHistLabComentariosQuimicoOtro5.Text = objConsulta.HistLabComentariosQuimicoOtro5;


            /// Biologicos 03/10/2018
            chkHistLabBiologicosBacteria.Checked = objConsulta.HistLabBiologicosBacteria;
            if (objConsulta.HistLabAniosBiologicosBacteria != 0)
                txtHistLabAniosBiologicosBacteria.Text = objConsulta.HistLabAniosBiologicosBacteria.ToString();
            if (objConsulta.HistLabMesesBiologicosBacteria != 0)
                txtHistLabMesesBiologicosBacteria.Text = objConsulta.HistLabMesesBiologicosBacteria.ToString();
            if (objConsulta.HistLabComentariosBiologicosBacteria != string.Empty)
                txtHistLabComentariosBiologicosBacteria.Text = objConsulta.HistLabComentariosBiologicosBacteria;
            chkHistLabBiologicosVirus.Checked = objConsulta.HistLabBiologicosVirus;
            if (objConsulta.HistLabAniosBiologicosVirus != 0)
                txtHistLabAniosBiologicosVirus.Text = objConsulta.HistLabAniosBiologicosVirus.ToString();
            if (objConsulta.HistLabMesesBiologicosVirus != 0)
                txtHistLabMesesBiologicosVirus.Text = objConsulta.HistLabMesesBiologicosVirus.ToString();
            if (objConsulta.HistLabComentariosBiologicosVirus != string.Empty)
                txtHistLabComentariosBiologicosVirus.Text = objConsulta.HistLabComentariosBiologicosVirus;
            chkHistLabBiologicosParasitos.Checked = objConsulta.HistLabBiologicosParasitos;
            if (objConsulta.HistLabAniosBiologicosParasitos != 0)
                txtHistLabAniosBiologicosParasitos.Text = objConsulta.HistLabAniosBiologicosParasitos.ToString();
            if (objConsulta.HistLabMesesBiologicosParasitos != 0)
                txtHistLabMesesBiologicosParasitos.Text = objConsulta.HistLabMesesBiologicosParasitos.ToString();
            if (objConsulta.HistLabComentariosBiologicosParasitos != string.Empty)
                txtHistLabComentariosBiologicosParasitos.Text = objConsulta.HistLabComentariosBiologicosParasitos;
            chkHistLabBiologicosOtro1.Checked = objConsulta.HistLabBiologicosOtro1;
            if (objConsulta.HistLabAniosBiologicosOtro1 != 0)
                txtHistLabAniosBiologicosOtro1.Text = objConsulta.HistLabAniosBiologicosOtro1.ToString();
            if (objConsulta.HistLabMesesBiologicosOtro1 != 0)
                txtHistLabMesesBiologicosOtro1.Text = objConsulta.HistLabMesesBiologicosOtro1.ToString();
            if (objConsulta.HistLabComentariosBiologicosOtro1 != string.Empty)
                txtHistLabComentariosBiologicosOtro1.Text = objConsulta.HistLabComentariosBiologicosOtro1;
            chkHistLabBiologicosOtro2.Checked = objConsulta.HistLabBiologicosOtro2;
            if (objConsulta.HistLabAniosBiologicosOtro2 != 0)
                txtHistLabAniosBiologicosOtro2.Text = objConsulta.HistLabAniosBiologicosOtro2.ToString();
            if (objConsulta.HistLabMesesBiologicosOtro2 != 0)
                txtHistLabMesesBiologicosOtro2.Text = objConsulta.HistLabMesesBiologicosOtro2.ToString();
            if (objConsulta.HistLabComentariosBiologicosOtro2 != string.Empty)
                txtHistLabComentariosBiologicosOtro2.Text = objConsulta.HistLabComentariosBiologicosOtro2;
            chkHistLabBiologicosOtro3.Checked = objConsulta.HistLabBiologicosOtro3;
            if (objConsulta.HistLabAniosBiologicosOtro3 != 0)
                txtHistLabAniosBiologicosOtro3.Text = objConsulta.HistLabAniosBiologicosOtro3.ToString();
            if (objConsulta.HistLabMesesBiologicosOtro3 != 0)
                txtHistLabMesesBiologicosOtro3.Text = objConsulta.HistLabMesesBiologicosOtro3.ToString();
            if (objConsulta.HistLabComentariosBiologicosOtro3 != string.Empty)
                txtHistLabComentariosBiologicosOtro3.Text = objConsulta.HistLabComentariosBiologicosOtro3;
            chkHistLabBiologicosOtro4.Checked = objConsulta.HistLabBiologicosOtro4;
            if (objConsulta.HistLabAniosBiologicosOtro4 != 0)
                txtHistLabAniosBiologicosOtro4.Text = objConsulta.HistLabAniosBiologicosOtro4.ToString();
            if (objConsulta.HistLabMesesBiologicosOtro4 != 0)
                txtHistLabMesesBiologicosOtro4.Text = objConsulta.HistLabMesesBiologicosOtro4.ToString();
            if (objConsulta.HistLabComentariosBiologicosOtro4 != string.Empty)
                txtHistLabComentariosBiologicosOtro4.Text = objConsulta.HistLabComentariosBiologicosOtro4;
            chkHistLabBiologicosOtro5.Checked = objConsulta.HistLabBiologicosOtro5;
            if (objConsulta.HistLabAniosBiologicosOtro5 != 0)
                txtHistLabAniosBiologicosOtro5.Text = objConsulta.HistLabAniosBiologicosOtro5.ToString();
            if (objConsulta.HistLabMesesBiologicosOtro5 != 0)
                txtHistLabMesesBiologicosOtro5.Text = objConsulta.HistLabMesesBiologicosOtro5.ToString();
            if (objConsulta.HistLabComentariosBiologicosOtro5 != string.Empty)
                txtHistLabComentariosBiologicosOtro5.Text = objConsulta.HistLabComentariosBiologicosOtro5;


            /// Ergonómicos 03/10/2018
            chkHistLabErgonomicosMovsRepetitivos.Checked = objConsulta.HistLabErgonomicosMovsRepetitivos;
            if (objConsulta.HistLabAniosErgonomicosMovsRepetitivos != 0)
                txtHistLabAniosErgonomicosMovsRepetitivos.Text = objConsulta.HistLabAniosErgonomicosMovsRepetitivos.ToString();
            if (objConsulta.HistLabMesesErgonomicosMovsRepetitivos != 0)
                txtHistLabMesesErgonomicosMovsRepetitivos.Text = objConsulta.HistLabMesesErgonomicosMovsRepetitivos.ToString();
            if (objConsulta.HistLabComentariosErgonomicosMovsRepetitivos != string.Empty)
                txtHistLabComentariosErgonomicosMovsRepetitivos.Text = objConsulta.HistLabComentariosErgonomicosMovsRepetitivos;
            chkHistLabErgonomicosPosturasForzadas.Checked = objConsulta.HistLabErgonomicosPosturasForzadas;
            if (objConsulta.HistLabAniosErgonomicosPosturasForzadas != 0)
                txtHistLabAniosErgonomicosPosturasForzadas.Text = objConsulta.HistLabAniosErgonomicosPosturasForzadas.ToString();
            if (objConsulta.HistLabMesesErgonomicosPosturasForzadas != 0)
                txtHistLabMesesErgonomicosPosturasForzadas.Text = objConsulta.HistLabMesesErgonomicosPosturasForzadas.ToString();
            if (objConsulta.HistLabComentariosErgonomicosPosturasForzadas != string.Empty)
                txtHistLabComentariosErgonomicosPosturasForzadas.Text = objConsulta.HistLabComentariosErgonomicosPosturasForzadas;
            chkHistLabErgonomicosManejoManCajas.Checked = objConsulta.HistLabErgonomicosManejoManCajas;
            if (objConsulta.HistLabAniosErgonomicosManejoManCajas != 0)
                txtHistLabAniosErgonomicosManejoManCajas.Text = objConsulta.HistLabAniosErgonomicosManejoManCajas.ToString();
            if (objConsulta.HistLabMesesErgonomicosManejoManCajas != 0)
                txtHistLabMesesErgonomicosManejoManCajas.Text = objConsulta.HistLabMesesErgonomicosManejoManCajas.ToString();
            if (objConsulta.HistLabComentariosErgonomicosManejoManCajas != string.Empty)
                txtHistLabComentariosErgonomicosManejoManCajas.Text = objConsulta.HistLabComentariosErgonomicosManejoManCajas;
            chkHistLabErgonomicosBidepestacionProlongada.Checked = objConsulta.HistLabErgonomicosBidepestacionProlongada;
            if (objConsulta.HistLabAniosErgonomicosBidepestacionProlongada != 0)
                txtHistLabAniosErgonomicosBidepestacionProlongada.Text = objConsulta.HistLabAniosErgonomicosBidepestacionProlongada.ToString();
            if (objConsulta.HistLabMesesErgonomicosBidepestacionProlongada != 0)
                txtHistLabMesesErgonomicosBidepestacionProlongada.Text = objConsulta.HistLabMesesErgonomicosBidepestacionProlongada.ToString();
            if (objConsulta.HistLabComentariosErgonomicosBidepestacionProlongada != string.Empty)
                txtHistLabComentariosErgonomicosBidepestacionProlongada.Text = objConsulta.HistLabComentariosErgonomicosBidepestacionProlongada;
            chkHistLabErgonomicosSedestacionProlongada.Checked = objConsulta.HistLabErgonomicosSedestacionProlongada;
            if (objConsulta.HistLabAniosErgonomicosSedestacionProlongada != 0)
                txtHistLabAniosErgonomicosSedestacionProlongada.Text = objConsulta.HistLabAniosErgonomicosSedestacionProlongada.ToString();
            if (objConsulta.HistLabMesesErgonomicosSedestacionProlongada != 0)
                txtHistLabMesesErgonomicosSedestacionProlongada.Text = objConsulta.HistLabMesesErgonomicosSedestacionProlongada.ToString();
            if (objConsulta.HistLabComentariosErgonomicosSedestacionProlongada != string.Empty)
                txtHistLabComentariosErgonomicosSedestacionProlongada.Text = objConsulta.HistLabComentariosErgonomicosSedestacionProlongada;
            chkHistLabErgonomicosOtro1.Checked = objConsulta.HistLabErgonomicosOtro1;
            if (objConsulta.HistLabAniosErgonomicosOtro1 != 0)
                txtHistLabAniosErgonomicosOtro1.Text = objConsulta.HistLabAniosErgonomicosOtro1.ToString();
            if (objConsulta.HistLabMesesErgonomicosOtro1 != 0)
                txtHistLabMesesErgonomicosOtro1.Text = objConsulta.HistLabMesesErgonomicosOtro1.ToString();
            if (objConsulta.HistLabComentariosErgonomicosOtro1 != string.Empty)
                txtHistLabComentariosErgonomicosOtro1.Text = objConsulta.HistLabComentariosErgonomicosOtro1;
            chkHistLabErgonomicosOtro2.Checked = objConsulta.HistLabErgonomicosOtro2;
            if (objConsulta.HistLabAniosErgonomicosOtro2 != 0)
                txtHistLabAniosErgonomicosOtro2.Text = objConsulta.HistLabAniosErgonomicosOtro2.ToString();
            if (objConsulta.HistLabMesesErgonomicosOtro2 != 0)
                txtHistLabMesesErgonomicosOtro2.Text = objConsulta.HistLabMesesErgonomicosOtro2.ToString();
            if (objConsulta.HistLabComentariosErgonomicosOtro2 != string.Empty)
                txtHistLabComentariosErgonomicosOtro2.Text = objConsulta.HistLabComentariosErgonomicosOtro2;
            chkHistLabErgonomicosOtro3.Checked = objConsulta.HistLabErgonomicosOtro3;
            if (objConsulta.HistLabAniosErgonomicosOtro3 != 0)
                txtHistLabAniosErgonomicosOtro3.Text = objConsulta.HistLabAniosErgonomicosOtro3.ToString();
            if (objConsulta.HistLabMesesErgonomicosOtro3 != 0)
                txtHistLabMesesErgonomicosOtro3.Text = objConsulta.HistLabMesesErgonomicosOtro3.ToString();
            if (objConsulta.HistLabComentariosErgonomicosOtro3 != string.Empty)
                txtHistLabComentariosErgonomicosOtro3.Text = objConsulta.HistLabComentariosErgonomicosOtro3;
            chkHistLabErgonomicosOtro4.Checked = objConsulta.HistLabErgonomicosOtro4;
            if (objConsulta.HistLabAniosErgonomicosOtro4 != 0)
                txtHistLabAniosErgonomicosOtro4.Text = objConsulta.HistLabAniosErgonomicosOtro4.ToString();
            if (objConsulta.HistLabMesesErgonomicosOtro4 != 0)
                txtHistLabMesesErgonomicosOtro4.Text = objConsulta.HistLabMesesErgonomicosOtro4.ToString();
            if (objConsulta.HistLabComentariosErgonomicosOtro4 != string.Empty)
                txtHistLabComentariosErgonomicosOtro4.Text = objConsulta.HistLabComentariosErgonomicosOtro4;
            chkHistLabErgonomicosOtro5.Checked = objConsulta.HistLabErgonomicosOtro5;
            if (objConsulta.HistLabAniosErgonomicosOtro5 != 0)
                txtHistLabAniosErgonomicosOtro5.Text = objConsulta.HistLabAniosErgonomicosOtro5.ToString();
            if (objConsulta.HistLabMesesErgonomicosOtro5 != 0)
                txtHistLabMesesErgonomicosOtro5.Text = objConsulta.HistLabMesesErgonomicosOtro5.ToString();
            if (objConsulta.HistLabComentariosErgonomicosOtro5 != string.Empty)
                txtHistLabComentariosErgonomicosOtro5.Text = objConsulta.HistLabComentariosErgonomicosOtro5;


            /// Riesgos psicosociales 03/10/2018
            chkHistLabPsicosocialEstres.Checked = objConsulta.HistLabPsicosocialEstres;
            if (objConsulta.HistLabAniosPsicosocialEstres != 0)
                txtHistLabAniosPsicosocialEstres.Text = objConsulta.HistLabAniosPsicosocialEstres.ToString();
            if (objConsulta.HistLabMesesPsicosocialEstres != 0)
                txtHistLabMesesPsicosocialEstres.Text = objConsulta.HistLabMesesPsicosocialEstres.ToString();
            if (objConsulta.HistLabComentariosPsicosocialEstres != string.Empty)
                txtHistLabComentariosPsicosocialEstres.Text = objConsulta.HistLabComentariosPsicosocialEstres;
            chkHistLabPsicosocialBurnot.Checked = objConsulta.HistLabPsicosocialBurnot;
            if (objConsulta.HistLabAniosPsicosocialBurnot != 0)
                txtHistLabAniosPsicosocialBurnot.Text = objConsulta.HistLabAniosPsicosocialBurnot.ToString();
            if (objConsulta.HistLabMesesPsicosocialBurnot != 0)
                txtHistLabMesesPsicosocialBurnot.Text = objConsulta.HistLabMesesPsicosocialBurnot.ToString();
            if (objConsulta.HistLabComentariosPsicosocialBurnot != string.Empty)
                txtHistLabComentariosPsicosocialBurnot.Text = objConsulta.HistLabComentariosPsicosocialBurnot;
            chkHistLabPsicosocialMobbing.Checked = objConsulta.HistLabPsicosocialMobbing;
            if (objConsulta.HistLabAniosPsicosocialMobbing != 0)
                txtHistLabAniosPsicosocialMobbing.Text = objConsulta.HistLabAniosPsicosocialMobbing.ToString();
            if (objConsulta.HistLabMesesPsicosocialMobbing != 0)
                txtHistLabMesesPsicosocialMobbing.Text = objConsulta.HistLabMesesPsicosocialMobbing.ToString();
            if (objConsulta.HistLabComentariosPsicosocialMobbing != string.Empty)
                txtHistLabComentariosPsicosocialMobbing.Text = objConsulta.HistLabComentariosPsicosocialMobbing;
            chkHistLabPsicosocialTrabajoxTurnos.Checked = objConsulta.HistLabPsicosocialTrabajoxTurnos;
            if (objConsulta.HistLabAniosPsicosocialTrabajoxTurnos != 0)
                txtHistLabAniosPsicosocialTrabajoxTurnos.Text = objConsulta.HistLabAniosPsicosocialTrabajoxTurnos.ToString();
            if (objConsulta.HistLabMesesPsicosocialTrabajoxTurnos != 0)
                txtHistLabMesesPsicosocialTrabajoxTurnos.Text = objConsulta.HistLabMesesPsicosocialTrabajoxTurnos.ToString();
            if (objConsulta.HistLabComentariosPsicosocialTrabajoxTurnos != string.Empty)
                txtHistLabComentariosPsicosocialTrabajoxTurnos.Text = objConsulta.HistLabComentariosPsicosocialTrabajoxTurnos;
            chkHistLabPsicosocialOtro1.Checked = objConsulta.HistLabPsicosocialOtro1;
            if (objConsulta.HistLabAniosPsicosocialOtro1 != 0)
                txtHistLabAniosPsicosocialOtro1.Text = objConsulta.HistLabAniosPsicosocialOtro1.ToString();
            if (objConsulta.HistLabMesesPsicosocialOtro1 != 0)
                txtHistLabMesesPsicosocialOtro1.Text = objConsulta.HistLabMesesPsicosocialOtro1.ToString();
            if (objConsulta.HistLabComentariosPsicosocialOtro1 != string.Empty)
                txtHistLabComentariosPsicosocialOtro1.Text = objConsulta.HistLabComentariosPsicosocialOtro1;
            chkHistLabPsicosocialOtro2.Checked = objConsulta.HistLabPsicosocialOtro2;
            if (objConsulta.HistLabAniosPsicosocialOtro2 != 0)
                txtHistLabAniosPsicosocialOtro2.Text = objConsulta.HistLabAniosPsicosocialOtro2.ToString();
            if (objConsulta.HistLabMesesPsicosocialOtro2 != 0)
                txtHistLabMesesPsicosocialOtro2.Text = objConsulta.HistLabMesesPsicosocialOtro2.ToString();
            if (objConsulta.HistLabComentariosPsicosocialOtro2 != string.Empty)
                txtHistLabComentariosPsicosocialOtro2.Text = objConsulta.HistLabComentariosPsicosocialOtro2;
            chkHistLabPsicosocialOtro3.Checked = objConsulta.HistLabPsicosocialOtro3;
            if (objConsulta.HistLabAniosPsicosocialOtro3 != 0)
                txtHistLabAniosPsicosocialOtro3.Text = objConsulta.HistLabAniosPsicosocialOtro3.ToString();
            if (objConsulta.HistLabMesesPsicosocialOtro3 != 0)
                txtHistLabMesesPsicosocialOtro3.Text = objConsulta.HistLabMesesPsicosocialOtro3.ToString();
            if (objConsulta.HistLabComentariosPsicosocialOtro3 != string.Empty)
                txtHistLabComentariosPsicosocialOtro3.Text = objConsulta.HistLabComentariosPsicosocialOtro3;
            chkHistLabPsicosocialOtro4.Checked = objConsulta.HistLabPsicosocialOtro4;
            if (objConsulta.HistLabAniosPsicosocialOtro4 != 0)
                txtHistLabAniosPsicosocialOtro4.Text = objConsulta.HistLabAniosPsicosocialOtro4.ToString();
            if (objConsulta.HistLabMesesPsicosocialOtro4 != 0)
                txtHistLabMesesPsicosocialOtro4.Text = objConsulta.HistLabMesesPsicosocialOtro4.ToString();
            if (objConsulta.HistLabComentariosPsicosocialOtro4 != string.Empty)
                txtHistLabComentariosPsicosocialOtro4.Text = objConsulta.HistLabComentariosPsicosocialOtro4;
            chkHistLabPsicosocialOtro5.Checked = objConsulta.HistLabPsicosocialOtro5;
            if (objConsulta.HistLabAniosPsicosocialOtro5 != 0)
                txtHistLabAniosPsicosocialOtro5.Text = objConsulta.HistLabAniosPsicosocialOtro5.ToString();
            if (objConsulta.HistLabMesesPsicosocialOtro5 != 0)
                txtHistLabMesesPsicosocialOtro5.Text = objConsulta.HistLabMesesPsicosocialOtro5.ToString();
            if (objConsulta.HistLabComentariosPsicosocialOtro5 != string.Empty)
                txtHistLabComentariosPsicosocialOtro5.Text = objConsulta.HistLabComentariosPsicosocialOtro5;


            /// Laboratorios Estres 03/10/2018
            chkBiometriaHematica.Checked = objConsulta.HistLabCheckBiometriaHematica;
            chkGrupoSanguineo.Checked = objConsulta.HistLabCheckGrupoSanguineo;
            chkQuimicaSanguinea.Checked = objConsulta.HistLabCheckQuimicaSanguinea;
            chkCoproparasitoscopio.Checked = objConsulta.HistLabCheckCoproparasitoscopio;
            chkEgo.Checked = objConsulta.HistLabCheckEgo;
            chkExudadoFaringeo.Checked = objConsulta.HistLabCheckExudadoFaringeo;
            chkReaccionesFebriles.Checked = objConsulta.HistLabCheckReaccionesFebriles;
            chkTeleTorax.Checked = objConsulta.HistLabCheckTeleTorax;
            chkRxColumnaLumbar.Checked = objConsulta.HistLabCheckRxColumnaLumbar;
            chkAudiometria.Checked = objConsulta.HistLabCheckAudiometria;
            chkEspirometria.Checked = objConsulta.HistLabCheckEspirometria;
            chkElectrocardiograma.Checked = objConsulta.HistLabCheckElectrocardiograma;
            chkPruebaEsfuerzo.Checked = objConsulta.HistLabCheckPruebaEsfuerzo;
            chkAgudezaVisual.Checked = objConsulta.HistLabCheckAgudezaVisual;
            chkToxicologia.Checked = objConsulta.HistLabCheckToxicologia;
            chkPerfilDrogas.Checked = objConsulta.HistLabCheckPerfilDrogas;
            chkDesintometriaOsea.Checked = objConsulta.HistLabCheckDesintometriaOsea;
            chkEcografia.Checked = objConsulta.HistLabCheckEcografia;
            chkPruebasEgonometricas.Checked = objConsulta.HistLabCheckPruebasEgonometricas;
            chkEvaluacionPsicologica.Checked = objConsulta.HistLabCheckEvaluacionPsicologica;
            chkOtro1.Checked = objConsulta.HistLabCheckOtro1;
            chkOtro2.Checked = objConsulta.HistLabCheckOtro2;
            chkOtro3.Checked = objConsulta.HistLabCheckOtro3;


        }
        /// <summary>
        /// Método, carga la divisiones que la empresa tiene permitidos.
        /// </summary>
        public void CargarDivisiones()
        {
            if (Session["Company"] != null)
            {
                EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones();
                int numIdEmpresa = int.Parse(Session["Company"].ToString());
                if (numIdEmpresa != 0)
                {
                    objEmpresaDivisiones.Empresa_id = numIdEmpresa;
                    objEmpresaDivisiones.GetEmpresaDivisiones();

                    if (objEmpresaDivisiones.DivHabitos)
                    {
                        divHabitos.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivColesterolGlicemia)
                    {
                        divColesterolGlicemia.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivExamenesLaboratorio)
                    {
                        divExamenesLaboratorio.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivMujer)
                    {
                        divMujer.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivAudiometria)
                    {
                        divAudiometria.Visible = true;
                    }
                    if (!objEmpresaDivisiones.DivColesterolGlicemia && !objEmpresaDivisiones.DivExamenesLaboratorio &&
                        !objEmpresaDivisiones.DivMujer && !objEmpresaDivisiones.DivAudiometria)
                    {
                        divPruebasBiometricas.Visible = false;
                    }
                    else
                    {
                        divPruebasBiometricas.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivDiastolicaSisTolica)
                    {
                        divDiastolicaSisTolica.Visible = true;
                        this.lblMedia.Text = "Media";
                        this.txtTensionMedia.Visible = true;
                        this.txtTension.Visible = false;
                    }
                    else
                    {
                        this.lblMedia.Text = "Normal";
                        this.txtTensionMedia.Visible = false;
                        this.txtTension.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivPerimetroAbdominal)
                    {
                        divPerimetroAbdominal.Visible = true;
                    }

                }

            }
        }

        /// <summary>
        /// Método, coloca en enable los controles de manera recursiva
        /// </summary>
        /// <param name="root"></param>
        private void DisableTextBoxesRecursive(Control root)
        {

            if (root is TextBox)
            {
                ((TextBox)root).CssClass = "LabelNoModifyBottom";
                ((TextBox)root).ReadOnly = true;
                ((TextBox)root).TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
                ((TextBox)root).Attributes["onkeypress"] = "return;";
            }
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

            foreach (Control child in root.Controls)
            {
                DisableTextBoxesRecursive(child);
            }
        }

        /// <summary>
        /// Método, modifica una consulta
        /// </summary>
        /// <param name="p_idConsulta"></param>
        public void UpdateConsulta(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.ObservacionesGenerales = this.txtObservaciones.Text;
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.UpdateConsultaObservaciones();
        }


        /// <summary>
        /// Método, modifica una consulta
        /// </summary>
        /// <param name="p_idConsulta"></param>
        public void UpdateConsultaCompleta(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            this.LoadObjectConsulta(objConsulta);
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.UpdateConsulta();
        }

        /// <summary>
        /// Método, inserta una nueva consulta
        /// </summary>
        /// <returns></returns>
        public long InsertConsulta()
        {
            long idConsulta;
            Consulta objConsulta = new Consulta();
            this.LoadObjectConsulta(objConsulta);
            idConsulta = objConsulta.InsertConsulta();
            return idConsulta;
        }

        /// <summary>
        /// Método, carga un objeto consulta con los datos de la forma
        /// </summary>
        public void LoadObjectConsulta(Consulta objConsulta)
        {
            string strSeparadorMiles = ConfigurationManager.AppSettings["SeparadorMiles"].ToString().Trim();

            objConsulta.Empresa_id = Convert.ToInt32(Session["Company"]);
            if (Request.QueryString["cita_id"] != null && Request.QueryString["cita_id"] != string.Empty)
                objConsulta.cita_id = Convert.ToInt32(Request.QueryString["cita_id"]);
            objConsulta.sede_id = Convert.ToInt32(this.ddlSede.SelectedValue);
            objConsulta.IdLineaNegocio = Convert.ToInt32(this.ddlLineaNegocio.SelectedValue);
            objConsulta.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
            objConsulta.Beneficiario_id = Convert.ToInt32(this.ddlUsuario.SelectedValue);
            if (Request.QueryString["SICAU_solicitud_id"] != null)
                objConsulta.Id_solicitud_SICAU = Convert.ToInt32(Request.QueryString["SICAU_solicitud_id"]);
            if (Session["SICAU"] != null)
                objConsulta.Usuario_idCreacion = Convert.ToInt32(Session["IdUser"]);
            else
                objConsulta.IdUserCreacion = Convert.ToInt32(Session["IdUser"]);
            if (Session["IdPrestador"] != null && Convert.ToInt32(Session["IdPrestador"]) != 0)
                objConsulta.IdPrestador = Convert.ToInt32(Session["IdPrestador"]);
            else
            {
                if (this.ddlPrestador.Visible)
                    objConsulta.IdPrestador = Convert.ToInt32(this.ddlPrestador.SelectedValue);
                if (this.txtPrestador.Visible)
                    objConsulta.IdPrestador = Convert.ToInt32(this.txtIdPrestador.Text);
            }
            objConsulta.FechaInicioCreacion = (DateTime)ViewState["FechaInicioConsulta"];
            objConsulta.IdTipoConsulta = Convert.ToInt16(this.rblTipoConsulta.SelectedValue);
            objConsulta.IdTipoEnfermedad = Convert.ToInt16(this.rblTipoEnfermedad.SelectedValue);
            if (this.txtMotivo.Text.EndsWith(" // "))
                objConsulta.Motivo = this.txtMotivo.Text.Remove(this.txtMotivo.Text.Length - 4);
            else
                objConsulta.Motivo = this.txtMotivo.Text;
            objConsulta.Contrarreferencia = this.txtContrareferencia.Text;
            objConsulta.EnfermedadActual = this.txtEnfermedad.Text;
            objConsulta.ObservacionesGenerales = this.txtObservaciones.Text;
            if (this.txtPlan.Text.EndsWith(" // "))
                objConsulta.PlanTratamiento = this.txtPlan.Text.Remove(this.txtPlan.Text.Length - 4);
            else
                objConsulta.PlanTratamiento = this.txtPlan.Text;
            objConsulta.ComentariosTranscripcion = this.txtComentariosTranscripcion.Text;
            if (this.txtIdSolicitanteTranscripcion.Text != "")
                objConsulta.IdProveedorTranscripcion = Convert.ToInt32(this.txtIdSolicitanteTranscripcion.Text);
            objConsulta.ExamenesLaboratorio = this.txtExamenesLaboratorio.Text;

            if (this.txtFechaControl.Text != string.Empty)
                objConsulta.CitaControl = Convert.ToDateTime(this.txtFechaControl.Text);
            else
                objConsulta.CitaControl = new DateTime(1900, 1, 1);

            //Antecedentes

            /// <summary>
            /// Cambio: Validación previa para eliminar división entre nuevos y antecedentes anteriores en caso que no haya sido diligenciada información adicional
            /// Autor: Adriana Diazgranados
            /// Fecha: 10/07/2012
            /// </summary>  
            if (this.txtAnteMedicos.Text.EndsWith(" // "))
                objConsulta.Medicos = this.txtAnteMedicos.Text.Remove(this.txtAnteMedicos.Text.Length - 4);
            else
                objConsulta.Medicos = this.txtAnteMedicos.Text;
            if (this.txtAnteQuirurgicos.Text.EndsWith(" // "))
                objConsulta.Quirurgicos = this.txtAnteQuirurgicos.Text.Remove(this.txtAnteQuirurgicos.Text.Length - 4);
            else
                objConsulta.Quirurgicos = this.txtAnteQuirurgicos.Text;
            if (this.txtAnteGineco.Text.EndsWith(" // "))
                objConsulta.Ginecobstetricos = this.txtAnteGineco.Text.Remove(this.txtAnteGineco.Text.Length - 4);
            else
                objConsulta.Ginecobstetricos = this.txtAnteGineco.Text;
            if (this.txtAnteTransfusionales.Text.EndsWith(" // "))
                objConsulta.Transfusionales = this.txtAnteTransfusionales.Text.Remove(this.txtAnteTransfusionales.Text.Length - 4);
            else
                objConsulta.Transfusionales = this.txtAnteTransfusionales.Text;
            if (this.txtAnteToxico.Text.EndsWith(" // "))
                objConsulta.ToxicoAlergicos = this.txtAnteToxico.Text.Remove(this.txtAnteToxico.Text.Length - 4);
            else
                objConsulta.ToxicoAlergicos = this.txtAnteToxico.Text;
            if (this.txtAnteFarmacologicos.Text.EndsWith(" // "))
                objConsulta.Farmacologicos = this.txtAnteFarmacologicos.Text.Remove(this.txtAnteFarmacologicos.Text.Length - 4);
            else
                objConsulta.Farmacologicos = this.txtAnteFarmacologicos.Text;
            if (this.txtAnteOtros.Text.EndsWith(" // "))
                objConsulta.OtrosAntecedentes = this.txtAnteOtros.Text.Remove(this.txtAnteOtros.Text.Length - 4);
            else
                objConsulta.OtrosAntecedentes = this.txtAnteOtros.Text;
            if (this.txtAnteFamiliares.Text.EndsWith(" // "))
                objConsulta.Familiares = this.txtAnteFamiliares.Text.Remove(this.txtAnteFamiliares.Text.Length - 4);
            else
                objConsulta.Familiares = this.txtAnteFamiliares.Text;
            objConsulta.Menarquia = this.txtMenarquia.Text.Trim();
            objConsulta.FechaUltimaMestruacion = this.txtFechaUltimaMestruacion.Text.Trim();
            if (this.txtGestaciones.Text != string.Empty)
                objConsulta.Gestaciones = Convert.ToInt16(this.txtGestaciones.Text);
            if (this.txtPartos.Text != string.Empty)
                objConsulta.Partos = Convert.ToInt16(this.txtPartos.Text);
            if (this.txtCesareas.Text != string.Empty)
                objConsulta.Cesareas = Convert.ToInt16(this.txtCesareas.Text);
            if (this.txtAbortos.Text != string.Empty)
                objConsulta.Abortos = Convert.ToInt16(this.txtAbortos.Text);
            if (this.txtVivos.Text != string.Empty)
                objConsulta.Vivos = Convert.ToInt16(this.txtVivos.Text);
            objConsulta.NormalMedicos = this.chkAnteMedicos.Checked;
            objConsulta.NormalQuirurgicos = this.chkAnteQuirurgicos.Checked;
            objConsulta.NormalGinecobstetricos = this.chkAnteGineco.Checked;
            objConsulta.NormalTransfusionales = this.chkAnteTransfusionales.Checked;
            objConsulta.NormalToxicoAlergicos = this.chkAnteToxico.Checked;
            objConsulta.NormalFarmacologicos = this.chkAnteFarmacologicos.Checked;
            objConsulta.RiesgoCardiovascular = this.chkRiesgoCardiovascular.Checked;
            objConsulta.NormalOtrosAntecedentes = this.chkAnteOtros.Checked;
            objConsulta.NormalFamiliares = this.chkAnteFamiliares.Checked;

            //Revisión por sistemas
            objConsulta.AspectoGeneral = this.txtSisGeneral.Text;
            objConsulta.Cabeza = this.txtSisCabeza.Text;
            objConsulta.Cuello = this.txtSisCuello.Text;
            objConsulta.Torax = this.txtSisTorax.Text;
            objConsulta.Abdomen = this.txtSisAbdomen.Text;
            objConsulta.Otros = this.txtSisOtros.Text;
            objConsulta.NormalAspectoGeneral = this.chkSisGeneral.Checked;
            objConsulta.NormalCabeza = this.chkSisCabeza.Checked;
            objConsulta.NormalCuello = this.chkSisCuello.Checked;
            objConsulta.NormalTorax = this.chkSisTorax.Checked;
            objConsulta.NormalAbdomen = this.chkSisAbdomen.Checked;
            objConsulta.NormalOtros = this.chkSisOtros.Checked;

            //Exámen físico
            if (this.txtTension.Visible)
                objConsulta.TensionArterial = this.txtTension.Text;
            if (this.txtTensionMedia.Visible && txtDiastolica.Text != string.Empty && txtSistolica.Text != string.Empty)
                objConsulta.TensionArterial = Math.Round((decimal.Parse(txtDiastolica.Text.Replace(strSeparadorMiles, "")) + (decimal.Parse(txtSistolica.Text.Replace(strSeparadorMiles, "")) - decimal.Parse(txtDiastolica.Text.Replace(strSeparadorMiles, ""))) / 3), 0).ToString();
            objConsulta.TensionArterialDiastolica = this.txtDiastolica.Text;
            objConsulta.TensionArterialSistolica = this.txtSistolica.Text;
            objConsulta.ComentariosExamenFisico = this.txtComentariosFisico.Text;

            this.txtPeso.Text = this.txtPeso.Text.Replace(strSeparadorMiles, "");
            this.txtTalla.Text = this.txtTalla.Text.Replace(strSeparadorMiles, "");
            this.txtPerimetroAbdominal.Text = this.txtPerimetroAbdominal.Text.Replace(strSeparadorMiles, "");
            this.txtTemperatura.Text = this.txtTemperatura.Text.Replace(strSeparadorMiles, "");
            this.txtFrecuenciaCar.Text = this.txtFrecuenciaCar.Text.Replace(strSeparadorMiles, "");
            this.txtFrecuenciaRes.Text = this.txtFrecuenciaRes.Text.Replace(strSeparadorMiles, "");


            if (this.txtPeso.Text != string.Empty)
                objConsulta.Peso = Convert.ToDecimal(this.txtPeso.Text);
            if (this.txtTalla.Text != string.Empty)
                objConsulta.Talla = Convert.ToDecimal(this.txtTalla.Text);
            if (this.txtTalla.Text != string.Empty && this.txtPeso.Text != string.Empty)
                objConsulta.IndiceMasaCorporal = Convert.ToDecimal(this.txtPeso.Text) / (Convert.ToDecimal(this.txtTalla.Text) * Convert.ToDecimal(this.txtTalla.Text));
            if (this.txtFrecuenciaCar.Text != string.Empty)
                objConsulta.FrecuenciaCardiaca = Convert.ToInt32(this.txtFrecuenciaCar.Text);
            if (this.txtFrecuenciaRes.Text != string.Empty)
                objConsulta.FrecuenciaRespiratoria = Convert.ToInt32(this.txtFrecuenciaRes.Text);
            if (this.txtPerimetroAbdominal.Text != string.Empty)
                objConsulta.PerimetroAbdominal = Convert.ToDecimal(this.txtPerimetroAbdominal.Text);
            if (this.txtTemperatura.Text != string.Empty)
                objConsulta.Temperatura = Convert.ToDecimal(this.txtTemperatura.Text);

            objConsulta.ExamenAspectoGeneral = this.txtFisGeneral.Text;
            objConsulta.ExamenCabeza = this.txtFisCabeza.Text;
            objConsulta.ExamenCuello = this.txtFisCuello.Text;
            objConsulta.ExamenTorax = this.txtFisTorax.Text;
            objConsulta.ExamenAbdomen = this.txtFisAbdomen.Text;
            objConsulta.ExamenOtros = this.txtFisOtros.Text;
            objConsulta.ExamenPielFanelas = this.txtFisPielFanelas.Text;
            objConsulta.ExamenConjuntivaOcular = this.txtFisConjuntiva.Text;
            objConsulta.ExamenReflejoCorneal = this.txtFisReflejo.Text;
            objConsulta.ExamenPupilas = this.txtFisPupilas.Text;
            objConsulta.ExamenOidos = this.txtFisOidos.Text;
            objConsulta.ExamenOtoscopia = this.txtFisOtoscopia.Text;
            objConsulta.ExamenRinoscopia = this.txtFisRinoscopia.Text;
            objConsulta.ExamenBocaFaringe = this.txtFisBocaFaringe.Text;
            objConsulta.ExamenAmigdalas = this.txtFisAmigdalas.Text;
            objConsulta.ExamenTiroides = this.txtFisTiroides.Text;
            objConsulta.ExamenAdenopatias = this.txtFisAdenopatias.Text;
            objConsulta.ExamenRuidosCardiacos = this.txtFisRuidosCardiacos.Text;
            objConsulta.ExamenRuidosRespiratorios = this.txtFisRuidosRespiratorios.Text;
            objConsulta.ExamenPalpacionAbdomen = this.txtFisPalpacionAbdomen.Text;
            objConsulta.ExamenGenitalesExternos = this.txtFisGenitales.Text;
            objConsulta.ExamenHernias = this.txtFisHernias.Text;
            objConsulta.ExamenColumnaVertebral = this.txtFisColumna.Text;
            objConsulta.ExamenExtremidadesSuperiores = this.txtFisExtremidadesSuperiores.Text;
            objConsulta.ExamenExtremidadesInferiores = this.txtFisExtremidadesInferiores.Text;
            objConsulta.ExamenVarices = this.txtFisVarices.Text;
            objConsulta.ExamenNeurologico = this.txtFisNeurologico.Text;
            objConsulta.ExamenNormalAspectoGeneral = this.chkFisGeneral.Checked;
            objConsulta.ExamenNormalCabeza = this.chkFisCabeza.Checked;
            objConsulta.ExamenNormalCuello = this.chkFisCuello.Checked;
            objConsulta.ExamenNormalTorax = this.chkFisTorax.Checked;
            objConsulta.ExamenNormalAbdomen = this.chkFisAbdomen.Checked;
            objConsulta.ExamenNormalOtros = this.chkFisOtros.Checked;
            objConsulta.ExamenNormalPielFanelas = this.chkFisPielFanelas.Checked;
            objConsulta.ExamenNormalConjuntivaOcular = this.chkFisConjuntiva.Checked;
            objConsulta.ExamenNormalReflejoCorneal = this.chkFisReflejo.Checked;
            objConsulta.ExamenNormalPupilas = this.chkFisPupilas.Checked;
            objConsulta.ExamenNormalOidos = this.chkFisOidos.Checked;
            objConsulta.ExamenNormalOtoscopia = this.chkFisOtoscopia.Checked;
            objConsulta.ExamenNormalRinoscopia = this.chkFisRinoscopia.Checked;
            objConsulta.ExamenNormalBocaFaringe = this.chkFisBocaFaringe.Checked;
            objConsulta.ExamenNormalAmigdalas = this.chkFisAmigdalas.Checked;
            objConsulta.ExamenNormalTiroides = this.chkFisTiroides.Checked;
            objConsulta.ExamenNormalAdenopatias = this.chkFisAdenopatias.Checked;
            objConsulta.ExamenNormalRuidosCardiacos = this.chkFisRuidosCardiacos.Checked;
            objConsulta.ExamenNormalRuidosRespiratorios = this.chkFisRuidosRespiratorios.Checked;
            objConsulta.ExamenNormalPalpacionAbdomen = this.chkFisPalpacionAbdomen.Checked;
            objConsulta.ExamenNormalGenitalesExternos = this.chkFisGenitales.Checked;
            objConsulta.ExamenNormalHernias = this.chkFisHernias.Checked;
            objConsulta.ExamenNormalColumnaVertebral = this.chkFisColumna.Checked;
            objConsulta.ExamenNormalExtremidadesSuperiores = this.chkFisExtremidadesSuperiores.Checked;
            objConsulta.ExamenNormalExtremidadesInferiores = this.chkFisExtremidadesInferiores.Checked;
            objConsulta.ExamenNormalVarices = this.chkFisVarices.Checked;
            objConsulta.ExamenNormalNeurologico = this.chkFisNeurologico.Checked;

            //Habitos
            if (this.rblTabaquismo.SelectedIndex > -1)
                objConsulta.Tabaquismo = Convert.ToInt32(this.rblTabaquismo.SelectedValue);
            else
                objConsulta.Tabaquismo = -1;
            if (this.rblAlcohol.SelectedIndex > -1)
                objConsulta.ConsumoAlcohol = Convert.ToInt32(this.rblAlcohol.SelectedValue);
            else
                objConsulta.ConsumoAlcohol = -1;
            if (this.rblDeportiva.SelectedIndex > -1)
                objConsulta.ActividadDeportiva = Convert.ToInt32(this.rblDeportiva.SelectedValue);
            else
                objConsulta.ActividadDeportiva = -1;
            objConsulta.FrecuenciaConsumo = this.txtFrecuenciaAlcohol.Text;
            objConsulta.FrecuenciaTabaquismo = this.txtFrecuenciaTabaquismo.Text;
            objConsulta.Vacunacion = this.txtVacunacion.Text;


            //Carga diagnósticos
            this.WC_AdicionarDiagnosticoConsulta1.LoadDiagnosticos(objConsulta);

            //PRUEBAS BIOMÉTRICAS 09/03/2010
            if (this.txtColesterolTotal.Text != string.Empty)
                objConsulta.ColesterolTotal = int.Parse(txtColesterolTotal.Text.Replace(strSeparadorMiles, ""));
            if (this.txtColesterolHDL.Text != string.Empty)
                objConsulta.ColesterolHDL = int.Parse(txtColesterolHDL.Text.Replace(strSeparadorMiles, ""));
            if (this.txtColesterolHDLmmol.Text != string.Empty)
                objConsulta.ColesterolHDLmmol = decimal.Parse(txtColesterolHDLmmol.Text.Replace(strSeparadorMiles, ""));
            if (this.txtColesterolLDL.Text != string.Empty)
                objConsulta.ColesterolLDL = int.Parse(txtColesterolLDL.Text.Replace(strSeparadorMiles, ""));
            if (this.txtTrigliceridos.Text != string.Empty)
                objConsulta.Trigliceridos = int.Parse(txtTrigliceridos.Text.Replace(strSeparadorMiles, ""));
            if (this.txtIndiceAterogenico.Text != string.Empty)
                objConsulta.IndiceAterogenico = decimal.Parse(txtIndiceAterogenico.Text.Replace(strSeparadorMiles, ""));
            if (this.txtAntigenoProstata.Text != string.Empty)
                objConsulta.AntigenoProstata = decimal.Parse(txtAntigenoProstata.Text.Replace(strSeparadorMiles, ""));
            if (this.txtGlucemiaAyunas.Text != string.Empty)
                objConsulta.GlucemiaAyunas = int.Parse(txtGlucemiaAyunas.Text.Replace(strSeparadorMiles, ""));
            /// <summary>
            /// Cambio: Permite guardar los resultados de la glicemia sin ayunas
            /// Autor: Ricardo Silva
            /// Fecha: 11/07/2012
            /// </summary>
            if (this.txtGlicemiaSinAyunas.Text != string.Empty)
                objConsulta.GlucemiaSinAyunas = int.Parse(txtGlicemiaSinAyunas.Text.Replace(strSeparadorMiles, ""));
            if (this.txtHemoglobinaGlucosilada.Text != string.Empty)
                objConsulta.HemoglobinaGlucosilada = decimal.Parse(txtHemoglobinaGlucosilada.Text.Replace(strSeparadorMiles, ""));
            if (this.txtHomocisteina.Text != string.Empty)
                objConsulta.Homocisteina = decimal.Parse(txtHomocisteina.Text.Replace(strSeparadorMiles, ""));
            if (this.rblPresenciaMicroorganismos.SelectedIndex > -1)
                objConsulta.PresenciaMicroorganismos = Convert.ToInt32(this.rblPresenciaMicroorganismos.SelectedValue);
            else
                objConsulta.PresenciaMicroorganismos = -1;
            if (this.txtFechaPapanicolauMicro.Text != string.Empty)
                objConsulta.FechaPapanicolauMicro = Convert.ToDateTime(this.txtFechaPapanicolauMicro.Text);
            else
                objConsulta.FechaPapanicolauMicro = new DateTime(1900, 1, 1);
            if (this.txtObservacionesPresenciaMicro.Text != string.Empty)
                objConsulta.ObservacionesPresenciaMicro = txtObservacionesPresenciaMicro.Text.Trim();
            if (this.ddlResultadoMorfologico.SelectedValue != "0" && this.ddlResultadoMorfologico.SelectedValue != string.Empty)
                objConsulta.ResultadoMorfologico = int.Parse(ddlResultadoMorfologico.SelectedValue);
            if (this.ddlAnormalCelulasEpi.SelectedValue != "0" && this.ddlAnormalCelulasEpi.SelectedValue != string.Empty)
                objConsulta.AnormalidadCelulasEpiteliales = int.Parse(ddlAnormalCelulasEpi.SelectedValue);
            if (this.ddlCelulasEscamosas.SelectedValue != "0" && this.ddlCelulasEscamosas.SelectedValue != string.Empty)
                objConsulta.CelulasEscamosasAtipicas = int.Parse(ddlCelulasEscamosas.SelectedValue);
            if (this.ddlMamografia.SelectedValue != "0" && this.ddlMamografia.SelectedValue != string.Empty)
                objConsulta.Mamografia = int.Parse(ddlMamografia.SelectedValue);
            if (this.txtMamografiaObservaciones.Text != string.Empty)
                objConsulta.MamografiaObservaciones = txtMamografiaObservaciones.Text.Trim();
            if (this.rblAudiometria.SelectedIndex > -1)
                objConsulta.Audiometria = Convert.ToInt32(this.rblAudiometria.SelectedValue);
            else
                objConsulta.Audiometria = -1;
            if (this.txtAudiometriaObservaciones.Text != string.Empty)
                objConsulta.AudiometriaObservaciones = txtAudiometriaObservaciones.Text.Trim();
            if (this.rblRayosX.SelectedIndex > -1)
                objConsulta.RayosX = Convert.ToInt32(this.rblRayosX.SelectedValue);
            else
                objConsulta.RayosX = -1;
            if (this.txtRayosXObservaciones.Text != string.Empty)
                objConsulta.RayosXObservaciones = txtRayosXObservaciones.Text.Trim();
            objConsulta.Miopia = chkMiopia.Checked;
            if (this.txtMiopiaValor.Text != string.Empty)
                objConsulta.MiopiaValor = decimal.Parse(txtMiopiaValor.Text.Trim());
            if (this.txtMiopiaValorOI.Text != string.Empty)
                objConsulta.MiopiaValorOI = decimal.Parse(txtMiopiaValorOI.Text.Trim());
            if (this.txtMiopiaObservaciones.Text != string.Empty)
                objConsulta.MiopiaObservaciones = txtMiopiaObservaciones.Text.Trim();
            objConsulta.Astigmatismo = chkAstigmatismo.Checked;
            if (this.txtAstigmatismoValor.Text != string.Empty)
                objConsulta.AstigmatismoValor = decimal.Parse(txtAstigmatismoValor.Text.Trim());
            if (this.txtAstigmatismoValorOI.Text != string.Empty)
                objConsulta.AstigmatismoValorOI = decimal.Parse(txtAstigmatismoValorOI.Text.Trim());
            if (this.txtAstigmatismoObservaciones.Text != string.Empty)
                objConsulta.AstigmatismoObservaciones = txtAstigmatismoObservaciones.Text.Trim();
            objConsulta.Hipermetropia = chkHipermetropia.Checked;
            if (this.txtHipermetropiaValor.Text != string.Empty)
                objConsulta.HipermetropiaValor = decimal.Parse(txtHipermetropiaValor.Text.Trim());
            if (this.txtHipermetropiaValorOI.Text != string.Empty)
                objConsulta.HipermetropiaValorOI = decimal.Parse(txtHipermetropiaValorOI.Text.Trim());
            if (this.txtHipermetropiaObservaciones.Text != string.Empty)
                objConsulta.HipermetropiaObservaciones = txtHipermetropiaObservaciones.Text.Trim();
            objConsulta.Presbicia = chkPresbicia.Checked;
            if (this.txtPresbiciaValor.Text != string.Empty)
                objConsulta.PresbiciaValor = decimal.Parse(txtPresbiciaValor.Text.Trim());
            if (this.txtPresbiciaValorOI.Text != string.Empty)
                objConsulta.PresbiciaValorOI = decimal.Parse(txtPresbiciaValorOI.Text.Trim());
            if (this.txtPresbiciaObservaciones.Text != string.Empty)
                objConsulta.PresbiciaObservaciones = txtPresbiciaObservaciones.Text.Trim();
            objConsulta.OtrosExamenVisual = chkOtros.Checked;
            this.WC_AdicionarDiagnosticoConsultaCIE102.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoExamenVisual);

            //Prototipo0-DMA-12/09/2018-Historia laboral-Ini

            objConsulta.HistLabIdGirosEmpresa1 = ConvierteEntero(ddlGirosEmpresa1.SelectedIndex == 0 ? "" : ddlGirosEmpresa1.SelectedValue.ToString());
            objConsulta.HistLabAniosEmpresa1  = ConvierteEntero(txtAniosEmpresa1.Text);
            objConsulta.HistLabMesesEmpresa1 = ConvierteEntero(txtMesesEmpresa1.Text);
            objConsulta.HistLabPuestoEmpresa1 = txtPuestoEmpresa1.Text;

            objConsulta.HistLabIdGirosEmpresa2 = ConvierteEntero(ddlGirosEmpresa2.SelectedIndex == 0 ? "" : ddlGirosEmpresa2.SelectedValue.ToString());
            objConsulta.HistLabAniosEmpresa2 = ConvierteEntero(txtAniosEmpresa2.Text);
            objConsulta.HistLabMesesEmpresa2 = ConvierteEntero(txtMesesEmpresa2.Text);
            objConsulta.HistLabPuestoEmpresa2 = txtPuestoEmpresa2.Text;

            objConsulta.HistLabIdGirosEmpresa3 = ConvierteEntero(ddlGirosEmpresa3.SelectedIndex == 0 ? "" : ddlGirosEmpresa3.SelectedValue.ToString());
            objConsulta.HistLabAniosEmpresa3 = ConvierteEntero(txtAniosEmpresa3.Text);
            objConsulta.HistLabMesesEmpresa3 = ConvierteEntero(txtMesesEmpresa3.Text);
            objConsulta.HistLabPuestoEmpresa3 = txtPuestoEmpresa3.Text;

            objConsulta.HistLabIdGirosEmpresa4 = ConvierteEntero(ddlGirosEmpresa4.SelectedIndex == 0 ? "" : ddlGirosEmpresa4.SelectedValue.ToString());
            objConsulta.HistLabAniosEmpresa4 = ConvierteEntero(txtAniosEmpresa4.Text);
            objConsulta.HistLabMesesEmpresa4 = ConvierteEntero(txtMesesEmpresa4.Text);
            objConsulta.HistLabPuestoEmpresa4 = txtPuestoEmpresa4.Text;

            objConsulta.HistLabIdGirosEmpresa5 = ConvierteEntero(ddlGirosEmpresa5.SelectedIndex == 0 ? "" : ddlGirosEmpresa5.SelectedValue.ToString());
            objConsulta.HistLabAniosEmpresa5 = ConvierteEntero(txtAniosEmpresa5.Text);
            objConsulta.HistLabMesesEmpresa5 = ConvierteEntero(txtMesesEmpresa5.Text);
            objConsulta.HistLabPuestoEmpresa5 = txtPuestoEmpresa5.Text;

            objConsulta.HistLabFisicoRuido = chkHistLabFisicoRuido.Checked; objConsulta.HistLabAniosFisicoRuido = ConvierteEntero(txtHistLabAniosFisicoRuido.Text); objConsulta.HistLabMesesFisicoRuido = ConvierteEntero(txtHistLabMesesFisicoRuido.Text); objConsulta.HistLabComentariosFisicoRuido = txtHistLabComentariosFisicoRuido.Text;
            objConsulta.HistLabFisicoIluminacion = chkHistLabFisicoIluminacion.Checked; objConsulta.HistLabAniosFisicoIluminacion = ConvierteEntero(txtHistLabAniosFisicoIluminacion.Text); objConsulta.HistLabMesesFisicoIluminacion = ConvierteEntero(txtHistLabMesesFisicoIluminacion.Text); objConsulta.HistLabComentariosFisicoIluminacion = txtHistLabComentariosFisicoIluminacion.Text;
            objConsulta.HistLabFisicoVibraciones = chkHistLabFisicoVibraciones.Checked; objConsulta.HistLabAniosFisicoVibraciones = ConvierteEntero(txtHistLabAniosFisicoVibraciones.Text); objConsulta.HistLabMesesFisicoVibraciones = ConvierteEntero(txtHistLabMesesFisicoVibraciones.Text); objConsulta.HistLabComentariosFisicoVibraciones = txtHistLabComentariosFisicoVibraciones.Text;
            objConsulta.HistLabFisicoRadiacion = chkHistLabFisicoRadiacion.Checked; objConsulta.HistLabAniosFisicoRadiacion = ConvierteEntero(txtHistLabAniosFisicoRadiacion.Text); objConsulta.HistLabMesesFisicoRadiacion = ConvierteEntero(txtHistLabMesesFisicoRadiacion.Text); objConsulta.HistLabComentariosFisicoRadiacion = txtHistLabComentariosFisicoRadiacion.Text;
            objConsulta.HistLabFisicoTempExtremas = chkHistLabFisicoTempExtremas.Checked; objConsulta.HistLabAniosFisicoTempExtremas = ConvierteEntero(txtHistLabAniosFisicoTempExtremas.Text); objConsulta.HistLabMesesFisicoTempExtremas = ConvierteEntero(txtHistLabMesesFisicoTempExtremas.Text); objConsulta.HistLabComentariosFisicoTempExtremas = txtHistLabComentariosFisicoTempExtremas.Text;
            objConsulta.HistLabFisicoOtro1 = chkHistLabFisicoOtro1.Checked; objConsulta.HistLabAniosFisicoOtro1 = ConvierteEntero(txtHistLabAniosFisicoOtro1.Text); objConsulta.HistLabMesesFisicoOtro1 = ConvierteEntero(txtHistLabMesesFisicoOtro1.Text); objConsulta.HistLabComentariosFisicoOtro1 = txtHistLabComentariosFisicoOtro1.Text;
            objConsulta.HistLabFisicoOtro2 = chkHistLabFisicoOtro2.Checked; objConsulta.HistLabAniosFisicoOtro2 = ConvierteEntero(txtHistLabAniosFisicoOtro2.Text); objConsulta.HistLabMesesFisicoOtro2 = ConvierteEntero(txtHistLabMesesFisicoOtro2.Text); objConsulta.HistLabComentariosFisicoOtro2 = txtHistLabComentariosFisicoOtro2.Text;
            objConsulta.HistLabFisicoOtro3 = chkHistLabFisicoOtro3.Checked; objConsulta.HistLabAniosFisicoOtro3 = ConvierteEntero(txtHistLabAniosFisicoOtro3.Text); objConsulta.HistLabMesesFisicoOtro3 = ConvierteEntero(txtHistLabMesesFisicoOtro3.Text); objConsulta.HistLabComentariosFisicoOtro3 = txtHistLabComentariosFisicoOtro3.Text;
            objConsulta.HistLabFisicoOtro4 = chkHistLabFisicoOtro4.Checked; objConsulta.HistLabAniosFisicoOtro4 = ConvierteEntero(txtHistLabAniosFisicoOtro4.Text); objConsulta.HistLabMesesFisicoOtro4 = ConvierteEntero(txtHistLabMesesFisicoOtro4.Text); objConsulta.HistLabComentariosFisicoOtro4 = txtHistLabComentariosFisicoOtro4.Text;
            objConsulta.HistLabFisicoOtro5 = chkHistLabFisicoOtro5.Checked; objConsulta.HistLabAniosFisicoOtro5 = ConvierteEntero(txtHistLabAniosFisicoOtro5.Text); objConsulta.HistLabMesesFisicoOtro5 = ConvierteEntero(txtHistLabMesesFisicoOtro5.Text); objConsulta.HistLabComentariosFisicoOtro5 = txtHistLabComentariosFisicoOtro5.Text;
            objConsulta.HistLabQuimicoPolvos = chkHistLabQuimicoPolvos.Checked; objConsulta.HistLabAniosQuimicoPolvos = ConvierteEntero(txtHistLabAniosQuimicoPolvos.Text); objConsulta.HistLabMesesQuimicoPolvos = ConvierteEntero(txtHistLabMesesQuimicoPolvos.Text); objConsulta.HistLabComentariosQuimicoPolvos = txtHistLabComentariosQuimicoPolvos.Text;
            objConsulta.HistLabQuimicoHumos = chkHistLabQuimicoHumos.Checked; objConsulta.HistLabAniosQuimicoHumos = ConvierteEntero(txtHistLabAniosQuimicoHumos.Text); objConsulta.HistLabMesesQuimicoHumos = ConvierteEntero(txtHistLabMesesQuimicoHumos.Text); objConsulta.HistLabComentariosQuimicoHumos = txtHistLabComentariosQuimicoHumos.Text;
            objConsulta.HistLabQuimicoRociosNeblina = chkHistLabQuimicoRociosNeblina.Checked; objConsulta.HistLabAniosQuimicoRociosNeblina = ConvierteEntero(txtHistLabAniosQuimicoRociosNeblina.Text); objConsulta.HistLabMesesQuimicoRociosNeblina = ConvierteEntero(txtHistLabMesesQuimicoRociosNeblina.Text); objConsulta.HistLabComentariosQuimicoRociosNeblina = txtHistLabComentariosQuimicoRociosNeblina.Text;
            objConsulta.HistLabQuimicoVapores = chkHistLabQuimicoVapores.Checked; objConsulta.HistLabAniosQuimicoVapores = ConvierteEntero(txtHistLabAniosQuimicoVapores.Text); objConsulta.HistLabMesesQuimicoVapores = ConvierteEntero(txtHistLabMesesQuimicoVapores.Text); objConsulta.HistLabComentariosQuimicoVapores = txtHistLabComentariosQuimicoVapores.Text;
            objConsulta.HistLabQuimicoGases = chkHistLabQuimicoGases.Checked; objConsulta.HistLabAniosQuimicoGases = ConvierteEntero(txtHistLabAniosQuimicoGases.Text); objConsulta.HistLabMesesQuimicoGases = ConvierteEntero(txtHistLabMesesQuimicoGases.Text); objConsulta.HistLabComentariosQuimicoGases = txtHistLabComentariosQuimicoGases.Text;
            objConsulta.HistLabQuimicoOtro1 = chkHistLabQuimicoOtro1.Checked; objConsulta.HistLabAniosQuimicoOtro1 = ConvierteEntero(txtHistLabAniosQuimicoOtro1.Text); objConsulta.HistLabMesesQuimicoOtro1 = ConvierteEntero(txtHistLabMesesQuimicoOtro1.Text); objConsulta.HistLabComentariosQuimicoOtro1 = txtHistLabComentariosQuimicoOtro1.Text;
            objConsulta.HistLabQuimicoOtro2 = chkHistLabQuimicoOtro2.Checked; objConsulta.HistLabAniosQuimicoOtro2 = ConvierteEntero(txtHistLabAniosQuimicoOtro2.Text); objConsulta.HistLabMesesQuimicoOtro2 = ConvierteEntero(txtHistLabMesesQuimicoOtro2.Text); objConsulta.HistLabComentariosQuimicoOtro2 = txtHistLabComentariosQuimicoOtro2.Text;
            objConsulta.HistLabQuimicoOtro3 = chkHistLabQuimicoOtro3.Checked; objConsulta.HistLabAniosQuimicoOtro3 = ConvierteEntero(txtHistLabAniosQuimicoOtro3.Text); objConsulta.HistLabMesesQuimicoOtro3 = ConvierteEntero(txtHistLabMesesQuimicoOtro3.Text); objConsulta.HistLabComentariosQuimicoOtro3 = txtHistLabComentariosQuimicoOtro3.Text;
            objConsulta.HistLabQuimicoOtro4 = chkHistLabQuimicoOtro4.Checked; objConsulta.HistLabAniosQuimicoOtro4 = ConvierteEntero(txtHistLabAniosQuimicoOtro4.Text); objConsulta.HistLabMesesQuimicoOtro4 = ConvierteEntero(txtHistLabMesesQuimicoOtro4.Text); objConsulta.HistLabComentariosQuimicoOtro4 = txtHistLabComentariosQuimicoOtro4.Text;
            objConsulta.HistLabQuimicoOtro5 = chkHistLabQuimicoOtro5.Checked; objConsulta.HistLabAniosQuimicoOtro5 = ConvierteEntero(txtHistLabAniosQuimicoOtro5.Text); objConsulta.HistLabMesesQuimicoOtro5 = ConvierteEntero(txtHistLabMesesQuimicoOtro5.Text); objConsulta.HistLabComentariosQuimicoOtro5 = txtHistLabComentariosQuimicoOtro5.Text;
            objConsulta.HistLabBiologicosBacteria = chkHistLabBiologicosBacteria.Checked; objConsulta.HistLabAniosBiologicosBacteria = ConvierteEntero(txtHistLabAniosBiologicosBacteria.Text); objConsulta.HistLabMesesBiologicosBacteria = ConvierteEntero(txtHistLabMesesBiologicosBacteria.Text); objConsulta.HistLabComentariosBiologicosBacteria = txtHistLabComentariosBiologicosBacteria.Text;
            objConsulta.HistLabBiologicosVirus = chkHistLabBiologicosVirus.Checked; objConsulta.HistLabAniosBiologicosVirus = ConvierteEntero(txtHistLabAniosBiologicosVirus.Text); objConsulta.HistLabMesesBiologicosVirus = ConvierteEntero(txtHistLabMesesBiologicosVirus.Text); objConsulta.HistLabComentariosBiologicosVirus = txtHistLabComentariosBiologicosVirus.Text;
            objConsulta.HistLabBiologicosParasitos = chkHistLabBiologicosParasitos.Checked; objConsulta.HistLabAniosBiologicosParasitos = ConvierteEntero(txtHistLabAniosBiologicosParasitos.Text); objConsulta.HistLabMesesBiologicosParasitos = ConvierteEntero(txtHistLabMesesBiologicosParasitos.Text); objConsulta.HistLabComentariosBiologicosParasitos = txtHistLabComentariosBiologicosParasitos.Text;
            objConsulta.HistLabBiologicosOtro1 = chkHistLabBiologicosOtro1.Checked; objConsulta.HistLabAniosBiologicosOtro1 = ConvierteEntero(txtHistLabAniosBiologicosOtro1.Text); objConsulta.HistLabMesesBiologicosOtro1 = ConvierteEntero(txtHistLabMesesBiologicosOtro1.Text); objConsulta.HistLabComentariosBiologicosOtro1 = txtHistLabComentariosBiologicosOtro1.Text;
            objConsulta.HistLabBiologicosOtro2 = chkHistLabBiologicosOtro2.Checked; objConsulta.HistLabAniosBiologicosOtro2 = ConvierteEntero(txtHistLabAniosBiologicosOtro2.Text); objConsulta.HistLabMesesBiologicosOtro2 = ConvierteEntero(txtHistLabMesesBiologicosOtro2.Text); objConsulta.HistLabComentariosBiologicosOtro2 = txtHistLabComentariosBiologicosOtro2.Text;
            objConsulta.HistLabBiologicosOtro3 = chkHistLabBiologicosOtro3.Checked; objConsulta.HistLabAniosBiologicosOtro3 = ConvierteEntero(txtHistLabAniosBiologicosOtro3.Text); objConsulta.HistLabMesesBiologicosOtro3 = ConvierteEntero(txtHistLabMesesBiologicosOtro3.Text); objConsulta.HistLabComentariosBiologicosOtro3 = txtHistLabComentariosBiologicosOtro3.Text;
            objConsulta.HistLabBiologicosOtro4 = chkHistLabBiologicosOtro4.Checked; objConsulta.HistLabAniosBiologicosOtro4 = ConvierteEntero(txtHistLabAniosBiologicosOtro4.Text); objConsulta.HistLabMesesBiologicosOtro4 = ConvierteEntero(txtHistLabMesesBiologicosOtro4.Text); objConsulta.HistLabComentariosBiologicosOtro4 = txtHistLabComentariosBiologicosOtro4.Text;
            objConsulta.HistLabBiologicosOtro5 = chkHistLabBiologicosOtro5.Checked; objConsulta.HistLabAniosBiologicosOtro5 = ConvierteEntero(txtHistLabAniosBiologicosOtro5.Text); objConsulta.HistLabMesesBiologicosOtro5 = ConvierteEntero(txtHistLabMesesBiologicosOtro5.Text); objConsulta.HistLabComentariosBiologicosOtro5 = txtHistLabComentariosBiologicosOtro5.Text;
            objConsulta.HistLabErgonomicosMovsRepetitivos = chkHistLabErgonomicosMovsRepetitivos.Checked; objConsulta.HistLabAniosErgonomicosMovsRepetitivos = ConvierteEntero(txtHistLabAniosErgonomicosMovsRepetitivos.Text); objConsulta.HistLabMesesErgonomicosMovsRepetitivos = ConvierteEntero(txtHistLabMesesErgonomicosMovsRepetitivos.Text); objConsulta.HistLabComentariosErgonomicosMovsRepetitivos = txtHistLabComentariosErgonomicosMovsRepetitivos.Text;
            objConsulta.HistLabErgonomicosPosturasForzadas = chkHistLabErgonomicosPosturasForzadas.Checked; objConsulta.HistLabAniosErgonomicosPosturasForzadas = ConvierteEntero(txtHistLabAniosErgonomicosPosturasForzadas.Text); objConsulta.HistLabMesesErgonomicosPosturasForzadas = ConvierteEntero(txtHistLabMesesErgonomicosPosturasForzadas.Text); objConsulta.HistLabComentariosErgonomicosPosturasForzadas = txtHistLabComentariosErgonomicosPosturasForzadas.Text;
            objConsulta.HistLabErgonomicosManejoManCajas = chkHistLabErgonomicosManejoManCajas.Checked; objConsulta.HistLabAniosErgonomicosManejoManCajas = ConvierteEntero(txtHistLabAniosErgonomicosManejoManCajas.Text); objConsulta.HistLabMesesErgonomicosManejoManCajas = ConvierteEntero(txtHistLabMesesErgonomicosManejoManCajas.Text); objConsulta.HistLabComentariosErgonomicosManejoManCajas = txtHistLabComentariosErgonomicosManejoManCajas.Text;
            objConsulta.HistLabErgonomicosBidepestacionProlongada = chkHistLabErgonomicosBidepestacionProlongada.Checked; objConsulta.HistLabAniosErgonomicosBidepestacionProlongada = ConvierteEntero(txtHistLabAniosErgonomicosBidepestacionProlongada.Text); objConsulta.HistLabMesesErgonomicosBidepestacionProlongada = ConvierteEntero(txtHistLabMesesErgonomicosBidepestacionProlongada.Text); objConsulta.HistLabComentariosErgonomicosBidepestacionProlongada = txtHistLabComentariosErgonomicosBidepestacionProlongada.Text;
            objConsulta.HistLabErgonomicosSedestacionProlongada = chkHistLabErgonomicosSedestacionProlongada.Checked; objConsulta.HistLabAniosErgonomicosSedestacionProlongada = ConvierteEntero(txtHistLabAniosErgonomicosSedestacionProlongada.Text); objConsulta.HistLabMesesErgonomicosSedestacionProlongada = ConvierteEntero(txtHistLabMesesErgonomicosSedestacionProlongada.Text); objConsulta.HistLabComentariosErgonomicosSedestacionProlongada = txtHistLabComentariosErgonomicosSedestacionProlongada.Text;
            objConsulta.HistLabErgonomicosOtro1 = chkHistLabErgonomicosOtro1.Checked; objConsulta.HistLabAniosErgonomicosOtro1 = ConvierteEntero(txtHistLabAniosErgonomicosOtro1.Text); objConsulta.HistLabMesesErgonomicosOtro1 = ConvierteEntero(txtHistLabMesesErgonomicosOtro1.Text); objConsulta.HistLabComentariosErgonomicosOtro1 = txtHistLabComentariosErgonomicosOtro1.Text;
            objConsulta.HistLabErgonomicosOtro2 = chkHistLabErgonomicosOtro2.Checked; objConsulta.HistLabAniosErgonomicosOtro2 = ConvierteEntero(txtHistLabAniosErgonomicosOtro2.Text); objConsulta.HistLabMesesErgonomicosOtro2 = ConvierteEntero(txtHistLabMesesErgonomicosOtro2.Text); objConsulta.HistLabComentariosErgonomicosOtro2 = txtHistLabComentariosErgonomicosOtro2.Text;
            objConsulta.HistLabErgonomicosOtro3 = chkHistLabErgonomicosOtro3.Checked; objConsulta.HistLabAniosErgonomicosOtro3 = ConvierteEntero(txtHistLabAniosErgonomicosOtro3.Text); objConsulta.HistLabMesesErgonomicosOtro3 = ConvierteEntero(txtHistLabMesesErgonomicosOtro3.Text); objConsulta.HistLabComentariosErgonomicosOtro3 = txtHistLabComentariosErgonomicosOtro3.Text;
            objConsulta.HistLabErgonomicosOtro4 = chkHistLabErgonomicosOtro4.Checked; objConsulta.HistLabAniosErgonomicosOtro4 = ConvierteEntero(txtHistLabAniosErgonomicosOtro4.Text); objConsulta.HistLabMesesErgonomicosOtro4 = ConvierteEntero(txtHistLabMesesErgonomicosOtro4.Text); objConsulta.HistLabComentariosErgonomicosOtro4 = txtHistLabComentariosErgonomicosOtro4.Text;
            objConsulta.HistLabErgonomicosOtro5 = chkHistLabErgonomicosOtro5.Checked; objConsulta.HistLabAniosErgonomicosOtro5 = ConvierteEntero(txtHistLabAniosErgonomicosOtro5.Text); objConsulta.HistLabMesesErgonomicosOtro5 = ConvierteEntero(txtHistLabMesesErgonomicosOtro5.Text); objConsulta.HistLabComentariosErgonomicosOtro5 = txtHistLabComentariosErgonomicosOtro5.Text;
            objConsulta.HistLabPsicosocialEstres = chkHistLabPsicosocialEstres.Checked; objConsulta.HistLabAniosPsicosocialEstres = ConvierteEntero(txtHistLabAniosPsicosocialEstres.Text); objConsulta.HistLabMesesPsicosocialEstres = ConvierteEntero(txtHistLabMesesPsicosocialEstres.Text); objConsulta.HistLabComentariosPsicosocialEstres = txtHistLabComentariosPsicosocialEstres.Text;
            objConsulta.HistLabPsicosocialBurnot = chkHistLabPsicosocialBurnot.Checked; objConsulta.HistLabAniosPsicosocialBurnot = ConvierteEntero(txtHistLabAniosPsicosocialBurnot.Text); objConsulta.HistLabMesesPsicosocialBurnot = ConvierteEntero(txtHistLabMesesPsicosocialBurnot.Text); objConsulta.HistLabComentariosPsicosocialBurnot = txtHistLabComentariosPsicosocialBurnot.Text;
            objConsulta.HistLabPsicosocialMobbing = chkHistLabPsicosocialMobbing.Checked; objConsulta.HistLabAniosPsicosocialMobbing = ConvierteEntero(txtHistLabAniosPsicosocialMobbing.Text); objConsulta.HistLabMesesPsicosocialMobbing = ConvierteEntero(txtHistLabMesesPsicosocialMobbing.Text); objConsulta.HistLabComentariosPsicosocialMobbing = txtHistLabComentariosPsicosocialMobbing.Text;
            objConsulta.HistLabPsicosocialTrabajoxTurnos = chkHistLabPsicosocialTrabajoxTurnos.Checked; objConsulta.HistLabAniosPsicosocialTrabajoxTurnos = ConvierteEntero(txtHistLabAniosPsicosocialTrabajoxTurnos.Text); objConsulta.HistLabMesesPsicosocialTrabajoxTurnos = ConvierteEntero(txtHistLabMesesPsicosocialTrabajoxTurnos.Text); objConsulta.HistLabComentariosPsicosocialTrabajoxTurnos = txtHistLabComentariosPsicosocialTrabajoxTurnos.Text;
            objConsulta.HistLabPsicosocialOtro1 = chkHistLabPsicosocialOtro1.Checked; objConsulta.HistLabAniosPsicosocialOtro1 = ConvierteEntero(txtHistLabAniosPsicosocialOtro1.Text); objConsulta.HistLabMesesPsicosocialOtro1 = ConvierteEntero(txtHistLabMesesPsicosocialOtro1.Text); objConsulta.HistLabComentariosPsicosocialOtro1 = txtHistLabComentariosPsicosocialOtro1.Text;
            objConsulta.HistLabPsicosocialOtro2 = chkHistLabPsicosocialOtro2.Checked; objConsulta.HistLabAniosPsicosocialOtro2 = ConvierteEntero(txtHistLabAniosPsicosocialOtro2.Text); objConsulta.HistLabMesesPsicosocialOtro2 = ConvierteEntero(txtHistLabMesesPsicosocialOtro2.Text); objConsulta.HistLabComentariosPsicosocialOtro2 = txtHistLabComentariosPsicosocialOtro2.Text;
            objConsulta.HistLabPsicosocialOtro3 = chkHistLabPsicosocialOtro3.Checked; objConsulta.HistLabAniosPsicosocialOtro3 = ConvierteEntero(txtHistLabAniosPsicosocialOtro3.Text); objConsulta.HistLabMesesPsicosocialOtro3 = ConvierteEntero(txtHistLabMesesPsicosocialOtro3.Text); objConsulta.HistLabComentariosPsicosocialOtro3 = txtHistLabComentariosPsicosocialOtro3.Text;
            objConsulta.HistLabPsicosocialOtro4 = chkHistLabPsicosocialOtro4.Checked; objConsulta.HistLabAniosPsicosocialOtro4 = ConvierteEntero(txtHistLabAniosPsicosocialOtro4.Text); objConsulta.HistLabMesesPsicosocialOtro4 = ConvierteEntero(txtHistLabMesesPsicosocialOtro4.Text); objConsulta.HistLabComentariosPsicosocialOtro4 = txtHistLabComentariosPsicosocialOtro4.Text;
            objConsulta.HistLabPsicosocialOtro5 = chkHistLabPsicosocialOtro5.Checked; objConsulta.HistLabAniosPsicosocialOtro5 = ConvierteEntero(txtHistLabAniosPsicosocialOtro5.Text); objConsulta.HistLabMesesPsicosocialOtro5 = ConvierteEntero(txtHistLabMesesPsicosocialOtro5.Text); objConsulta.HistLabComentariosPsicosocialOtro5 = txtHistLabComentariosPsicosocialOtro5.Text;


            objConsulta.HistLabCheckBiometriaHematica = chkBiometriaHematica.Checked;
            objConsulta.HistLabCheckGrupoSanguineo = chkGrupoSanguineo.Checked;
            objConsulta.HistLabCheckQuimicaSanguinea = chkQuimicaSanguinea.Checked;
            objConsulta.HistLabCheckCoproparasitoscopio = chkCoproparasitoscopio.Checked;
            objConsulta.HistLabCheckEgo = chkEgo.Checked;
            objConsulta.HistLabCheckExudadoFaringeo = chkExudadoFaringeo.Checked;
            objConsulta.HistLabCheckReaccionesFebriles = chkReaccionesFebriles.Checked;
            objConsulta.HistLabCheckTeleTorax = chkTeleTorax.Checked;
            objConsulta.HistLabCheckRxColumnaLumbar = chkRxColumnaLumbar.Checked;
            objConsulta.HistLabCheckAudiometria = chkAudiometria.Checked;
            objConsulta.HistLabCheckEspirometria = chkEspirometria.Checked;
            objConsulta.HistLabCheckElectrocardiograma = chkElectrocardiograma.Checked;
            objConsulta.HistLabCheckPruebaEsfuerzo = chkPruebaEsfuerzo.Checked;
            objConsulta.HistLabCheckAgudezaVisual = chkAgudezaVisual.Checked;
            objConsulta.HistLabCheckToxicologia = chkToxicologia.Checked;
            objConsulta.HistLabCheckPerfilDrogas = chkPerfilDrogas.Checked;
            objConsulta.HistLabCheckDesintometriaOsea = chkDesintometriaOsea.Checked;
            objConsulta.HistLabCheckEcografia = chkEcografia.Checked;
            objConsulta.HistLabCheckPruebasEgonometricas = chkPruebasEgonometricas.Checked;
            objConsulta.HistLabCheckEvaluacionPsicologica = chkEvaluacionPsicologica.Checked;
            objConsulta.HistLabCheckOtro1 = chkOtro1.Checked;
            objConsulta.HistLabCheckOtro2 = chkOtro2.Checked;
            objConsulta.HistLabCheckOtro3 = chkOtro3.Checked;



            //Prototipo0-DMA-12/09/2018-Historia laboral-Fin


        }

        #endregion

        #region Eventos

        private int ConvierteEntero(string cadena)
        {
            int resp = 0;
            if (string.IsNullOrEmpty(cadena))
                return 0;

            if (int.TryParse(cadena, out resp))
                return resp;

            return 0;



        }

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
            this.imbHistorial.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorial_Click);
            this.lnkVerHistorico.Click += new System.EventHandler(this.lnkVerHistorico_Click);
            this.imbVerHistorialOrdenes.Click += new System.Web.UI.ImageClickEventHandler(this.imbVerHistorialOrdenes_Click);
            this.lnkVerHistorialOrdenes.Click += new System.EventHandler(this.lnkVerHistorialOrdenes_Click);
            this.imblnkVerHistoria.Click += new System.Web.UI.ImageClickEventHandler(this.imbVerHistoria_Click);
            this.lnkVerHistoria.Click += new System.EventHandler(this.lnkVerHistoria_Click);
            this.imbHistorialAntecedentes.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorialAntecedentes_Click);
            this.imbHistorialRevision.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorialRevision_Click);
            this.imbHistorialHabitos.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorialHabitos_Click);
            this.imbHistorialExamen.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorialExamen_Click);
            this.imbHistorialDiagnosticos.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorialDiagnosticos_Click);
            //DMA-Prototipo0-27/09/2018-Historia laboral
            this.imbHistorialLaboral.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorialLaboral_Click);

            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        /// <summary>
        /// Evento, realiza el llamado para guarda la consulta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, System.EventArgs e)
        {

            try
            {
                btnGuardar.Visible = false;
                EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones(int.Parse(Session["Company"].ToString()));
                bool esMedico = false, esNutriologo = false, esTodo = false;
                string[] medico = new string[] { "1", "2", "7", "8", "9", "10", "11", "12", "13", "14", "17" };
                ArrayList ArrayMedico = new ArrayList();
                ArrayMedico.AddRange(medico);

                string[] nutriologo = new string[] { "15", "16" };
                ArrayList ArrayNutriologo = new ArrayList();
                ArrayNutriologo.AddRange(nutriologo);

                string[] todo = new string[] { "3", "4" };
                ArrayList ArrayTodo = new ArrayList();
                ArrayTodo.AddRange(todo);

                //DMA-Prototipo0-29/09/2018
                bool esLaboral = false;


                if (ArrayMedico.Contains(rblTipoConsulta.SelectedValue.ToString()))
                    esMedico = true;
                if (ArrayNutriologo.Contains(rblTipoConsulta.SelectedValue.ToString()))
                    esNutriologo = true;
                if (ArrayTodo.Contains(rblTipoConsulta.SelectedValue.ToString()))
                    esTodo = true;

                //DMA-Prototipo0-29/09/2018
                if (rblTipoConsulta.SelectedValue.ToString() == "18" || rblTipoConsulta.SelectedValue.ToString() == "19" )
                    esLaboral = true;

                Session["idTipoConsulta"] = rblTipoConsulta.SelectedValue.ToString();

                if (Request.QueryString["IdConsulta"] != null)
                {
                    if (Request.QueryString["Editar"] == null || Request.QueryString["Editar"] == string.Empty)
                    {
                        this.UpdateConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsulta, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta observaciones " + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                        if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisiones())

                            if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString())) + "&employee_id=" + Request.QueryString["employee_id"], false);
                            else
                                Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString())) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + ViewState["IdSolicitud"], false);


                        else
                        {
                            if (esMedico)
                            {
                                if (rblTipoConsulta.SelectedValue.ToString() == "11" || rblTipoConsulta.SelectedValue.ToString() == "12")
                                    Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString()) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString())) + "&employee_id=" + Request.QueryString["employee_id"], false);//MAHG 19/07/2010 Se agrega el parámetro Finalizada
                            }
                            else if(esLaboral)
                            {
                                Response.Redirect("AE_CertificadoAptitud.aspx?employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=1");
                            }
                            else
                                Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString())) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + ViewState["IdSolicitud"] + "&TipoConsulta=" + Convert.ToInt32(rblTipoConsulta.SelectedValue), false);
                        }

                    }
                    else
                    {
                        this.UpdateConsultaCompleta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsulta, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta completa edición " + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                        if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisiones())

                            if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString()).ToString())) + "&employee_id=" + Request.QueryString["employee_id"] + "&Editar=1", false);//MAHG 19/07/2010 Se agrega el parámetro Finalizada
                            else
                                Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString())) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + ViewState["IdSolicitud"] + "&Editar=1", false);//MAHG 19/07/2010 Se agrega el parámetro Finalizada


                        else
                        {
                            if (esMedico)
                            {
                                if (rblTipoConsulta.SelectedValue.ToString() == "11" || rblTipoConsulta.SelectedValue.ToString() == "12")
                                    Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString()) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                                else
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString())) + "&employee_id=" + Request.QueryString["employee_id"], false);//MAHG 19/07/2010 Se agrega el parámetro Finalizada
                            }
                            else if (esLaboral)
                            {
                                Response.Redirect("AE_CertificadoAptitud.aspx?employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Request.QueryString["IdConsulta"] + "&editar=1");
                            }
                            else
                                Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Convert.ToInt64(Microsoft.Security.Application.Encoder.HtmlEncode(Request.QueryString["IdConsulta"].ToString())) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + ViewState["IdSolicitud"] + "&Editar=1" + "&TipoConsulta=" + Convert.ToInt32(rblTipoConsulta.SelectedValue), false);//MAHG 19/07/2010 Se agrega el parámetro Finalizada
                        }
                    }

                }
                else
                {
                    long idConsulta = this.InsertConsulta();
                    this.RegisterLog(Log.EnumActionsLog.IngresarConsulta, idConsulta, "Ingreso consulta " + idConsulta);

                    if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisiones())
                        if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                        {
                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&employee_id=" + Request.QueryString["employee_id"]);
                        }
                        else
                            Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&employee_id=" + Request.QueryString["employee_id"], false);
                    else
                    {
                        if (esMedico)
                        {
                            if (rblTipoConsulta.SelectedValue.ToString() == "11" || rblTipoConsulta.SelectedValue.ToString() == "12")
                                Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                            else
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&employee_id=" + Request.QueryString["employee_id"], false);//MAHG 19/07/2010 Se agrega el parámetro Finalizada
                        }
                        else if (esLaboral)
                        {
                            Response.Redirect("AE_CertificadoAptitud.aspx?employee_id=" + Request.QueryString["employee_id"] + "&IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&editar=1" );
                        }
                        else
                            Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(rblTipoConsulta.SelectedValue) + "&IdSolicitud=" + ViewState["IdSolicitud"], false);
                    }

                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Evento, cancela el ingreso o modificación de la orden y retorna de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, System.EventArgs e)
        {
            //if (Request.QueryString["IdConsulta"] == null)
            //    Response.Redirect("LIS_empleado.aspx");
            //else
            //{
            //    if (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty)
            //        Response.Redirect("LIS_consulta.aspx");
            //    else
            //        Response.Redirect("LIS_empleado.aspx");
            //}
            Response.Redirect("LIS_consulta.aspx");//Marsh - JFEE - 2014/11/26 - Correcciones generales, al cancelar una consulta siempre debe direccionar a la página de consultas
        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de la hisotia clínica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbVerHistoria_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=todos", 850, 750, 1);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de la hisotia clínica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkVerHistoria_Click(object sender, System.EventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=todos", 850, 750, 1);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de las consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorial_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=todos", 850, 750, 1);
        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de las consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerHistorico_Click(object sender, System.EventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 2);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=todos", 850, 750, 2);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de antecedentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialAntecedentes_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=antecedentes", 850, 750, 3);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=antecendentes", 850, 750, 3);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de revisión por sistemas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialRevision_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=revision", 850, 750, 4);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=revision", 850, 750, 4);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de exámen físico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialExamen_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=examen", 850, 750, 5);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=examen", 850, 750, 5);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de diagnósticos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialDiagnosticos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=diagnosticos", 850, 750, 6);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=diagnosticos", 850, 750, 6);


        }
        /// <summary>
        /// Evento, abre la ventana para ver historial de diagnósticos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>DMA-Prototipo0-27/09/2018</remarks>
        private void imbHistorialLaboral_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 6);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=todos", 850, 750, 6);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de habitos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialHabitos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=habitos", 850, 750, 6);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + this.ddlUsuario.SelectedValue + "&Modulo=habitos", 850, 750, 6);


        }

        /// <summary>
        /// Evento, llama la ventana para consultar histórico de órdenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbVerHistorialOrdenes_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"], 850, 750);

        }

        /// <summary>
        /// Evento, llama la ventana para consultar histórico de órdenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerHistorialOrdenes_Click(object sender, System.EventArgs e)
        {
            if (this.ddlUsuario.SelectedValue == "0")
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"], 850, 750);

        }
        /// <summary>
        /// Evento, que llena el combo box de Anormailidades en Células epiteliales. Modificacion John Portela 01/03/2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlResultadoMorfologico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlResultadoMorfologico.SelectedValue == "3")
            {
                ddlAnormalCelulasEpi.Visible = true;
                lblAnormalCelulasEpi.Visible = true;
                //this.FillList(this.ddlAnormalCelulasEpi, "PreguntaRespuesta", "--Resultado--", 3);
            }
            else
            {
                ddlAnormalCelulasEpi.Visible = false;
                lblAnormalCelulasEpi.Visible = false;
                ddlCelulasEscamosas.Visible = false;
                lblCelulasEscamosas.Visible = false;
            }
        }
        /// <summary>
        /// Evento, que llena el combo box de Celulas escamosas atipicas. Modificacion John Portela 01/03/2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlAnormalCelulasEpi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAnormalCelulasEpi.SelectedValue == "4")
            {
                lblCelulasEscamosas.Visible = true;
                ddlCelulasEscamosas.Visible = true;
                //this.FillList(this.ddlCelulasEscamosas, "PreguntaRespuesta", "--Resultado--", 4);
            }
            else
            {
                ddlCelulasEscamosas.Visible = false;
                lblCelulasEscamosas.Visible = false;
            }
        }

        /// <summary>
        /// Guarda la consulta temporalmente por medio de la imagen guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Inicio - Emilio Bueno 11/Dic/2012
                // Verifica un conjunto de campos obligatorios
                // para que se pueda realizar el guardado automático
                cmvUsuario0.Validate();
                //cmvLineaNegocio.Validate();
                cmvUsuario.Validate();
                rfvSolicitante.Validate();
                rfvTipoConsulta.Validate();
                rfvTipoEnfermedad.Validate();
                if (rblTipoConsulta.Text != "3")
                {
                    rfvMotivo.Validate();
                    rfvEnfermedadActual.Validate();
                }
                if (!(cmvUsuario0.IsValid &&
                    cmvLineaNegocio.IsValid &&
                    cmvUsuario.IsValid &&
                    rfvSolicitante.IsValid &&
                    rfvTipoConsulta.IsValid &&
                    rfvTipoEnfermedad.IsValid &&
                    rfvMotivo.IsValid &&
                    rfvEnfermedadActual.IsValid))
                {
                    this.DisplayMessage("Debe ingresar los campos requeridos de la sección Datos de la Consulta antes de intentar guardar temporalmente!");
                    return;
                }
                WC_AdicionarDiagnosticoConsulta1.DiagnosticoEsRequerido = false;
                // Fin - Emilio Bueno 11/Dic/2012

                EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones(int.Parse(Session["Company"].ToString()));
                if (Request.QueryString["IdConsulta"] != null)
                {
                    if (Request.QueryString["Editar"] == null || Request.QueryString["Editar"] == string.Empty)
                    {
                        this.UpdateConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsulta, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta " + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                    }
                    else
                    {
                        this.UpdateConsultaCompleta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsulta, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta " + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                    }
                }
                else
                {
                    long idConsulta = this.InsertConsulta();
                    Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1");
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }
        /// <summary>
        /// Guarda la consulta temporalmente por medio del link guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Inicio - Emilio Bueno 11/Dic/2012
                // Verifica un conjunto de campos obligatorios
                // para que se pueda realizar el guardado automático
                cmvUsuario0.Validate();
                //cmvLineaNegocio.Validate();
                cmvUsuario.Validate();
                rfvSolicitante.Validate();
                rfvTipoConsulta.Validate();
                rfvTipoEnfermedad.Validate();
                if (rblTipoConsulta.Text != "3")
                {
                    rfvMotivo.Validate();
                    rfvEnfermedadActual.Validate();
                }
                if (!(cmvUsuario0.IsValid &&
                    cmvLineaNegocio.IsValid &&
                    cmvUsuario.IsValid &&
                    rfvSolicitante.IsValid &&
                    rfvTipoConsulta.IsValid &&
                    rfvTipoEnfermedad.IsValid &&
                    rfvMotivo.IsValid &&
                    rfvEnfermedadActual.IsValid))
                {
                    this.DisplayMessage("Debe ingresar los campos requeridos de la sección Datos de la Consulta antes de intentar guardar temporalmente!");
                    return;
                }
                WC_AdicionarDiagnosticoConsulta1.DiagnosticoEsRequerido = false;
                // Fin - Emilio Bueno 11/Dic/2012

                EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones(int.Parse(Session["Company"].ToString()));
                if (Request.QueryString["IdConsulta"] != null)
                {
                    if (Request.QueryString["Editar"] == null || Request.QueryString["Editar"] == string.Empty)
                    {
                        this.UpdateConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsulta, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta " + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                    }
                    else
                    {
                        this.UpdateConsultaCompleta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsulta, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta " + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                    }
                }
                else
                {
                    long idConsulta = this.InsertConsulta();
                    Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Microsoft.Security.Application.Encoder.HtmlEncode(idConsulta.ToString()) + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1");
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }
        #endregion

    }
}
