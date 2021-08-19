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
    /// Inserta o modifica una consulta médica
    /// </summary>
    public partial class AE_registronutricion : PB_PaginaBase
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

                if (Session["idTipoConsulta"] != null && Session["idTipoConsulta"].ToString() == "3")
                {
                    lnkMostrar.Visible = false;
                }

                if (!this.Page.IsPostBack)
                {
                    Response.Write("<script>window.parent.scrollTo(0,0);</script>");

                    this.LoadControls();
                    
                    if (Request.QueryString["IdConsulta"] != null)
                    {
                        Consulta objConsulta = new Consulta();
                        objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                        if (objConsulta.ExisteConsultaNutricion())
                            this.LoadFormConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
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



            ViewState["FechaInicioConsulta"] = DateTime.Now;

            if (Session["IdPrestador"] != null && Convert.ToInt32(Session["IdPrestador"]) != 0)
            {
                GeneralTable objGeneral = new GeneralTable();
                objGeneral.TableName = "Prestadores";
                objGeneral.ColumnName = "Prestador";
                objGeneral.Id = Convert.ToInt32(Session["IdPrestador"]);
                objGeneral.GetGeneralTable();
            }

            objEmpresaParentescos.Empresa_id = Convert.ToInt32(Session["Company"]);
            dtParentescos = objEmpresaParentescos.ConsultEmpresaParentescos().Tables[0];


            foreach (DataRow row in dtParentescos.Rows)
            {
                if (row["IdParentesco"].ToString() == "0")
                {
                    objEmpleado.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
                    objEmpleado.GetSIC_EMPLEADO();
                }
                else
                {
                    objBeneficiario.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);
                    objBeneficiario.IdParentesco = Convert.ToInt32(row["IdParentesco"].ToString());
                    objBeneficiario.Opcion = 1;
                    dtBeneficiarios = objBeneficiario.ConsultSIC_BENEFICIARIO().Tables[0];
                    foreach (DataRow rowBeneficiario in dtBeneficiarios.Rows)
                    {
                    }
                }
            }
            this.FillList(this.rblAlimentacionSaludable, "PreguntaRespuesta", 152);
            this.FillList(this.ddlDiagnosticoNutricional, "PreguntaRespuesta", "--Seleccione--", 181);
            this.FillList(this.ddlConsideracionApetito, "PreguntaRespuesta", "--Seleccione--", 200);
            this.FillList(this.ddlEliminacionIntestinal, "PreguntaRespuesta", "--Seleccione--", 204);
            this.FillList(this.cblCompraAlimentos, "PreguntaRespuesta", 209);
            this.FillList(this.cblPreparaAlimentos, "PreguntaRespuesta", 214);
            this.FillList(this.ddlDesayunoLugar, "PreguntaRespuesta", "--Seleccione--", 219);
            this.FillList(this.ddlAlmuerzoLugar, "PreguntaRespuesta", "--Seleccione--", 224);
            this.FillList(this.ddlComidaLugar, "PreguntaRespuesta", "--Seleccione--", 225);
            this.FillList(this.ddlEntremesLugar, "PreguntaRespuesta", "--Seleccione--", 226);
            this.FillList(this.ddlCenaLugar, "PreguntaRespuesta", "--Seleccione--", 227);
            this.FillList(this.cblAlimentosAgrado, "PreguntaRespuesta", 228);
            this.FillList(this.cblAlimentosDisguntan, "PreguntaRespuesta", 244);
            this.FillList(this.ddlEstarSatisfecho, "PreguntaRespuesta", "--Seleccione--", 245);
            this.FillList(this.ddlSatisfaccionFacilidad, "PreguntaRespuesta", "--Seleccione--", 250);
            this.FillList(this.ddlReconocerHambre, "PreguntaRespuesta", "--Seleccione--", 251);
            this.FillList(this.ddlComerDespacio, "PreguntaRespuesta", "--Seleccione--", 252);
            this.FillList(this.ddlQuienPrescribe, "PreguntaRespuesta", "--Seleccione--", 253);
            this.FillList(this.ddlMotivoIniciarDieta, "PreguntaRespuesta", "--Seleccione--", 256);
            this.FillList(this.ddlIngestionAlimentos, "PreguntaRespuesta", "--Seleccione--", 260);
            this.FillList(this.ddlBajarPesoPrescrito, "PreguntaRespuesta", "--Seleccione--", 264);
            this.FillList(this.ddlPadecerTrastorno, "PreguntaRespuesta", "--Seleccione--", 268);
            this.FillList(this.ddlComidaRapida, "PreguntaRespuesta", "--Seleccione--", 273);
            this.FillList(this.ddlVasosAgua, "PreguntaRespuesta", "--Seleccione--", 278);

            //Carga las divisiones que la empresa tiene permisos.
            this.CargarDivisiones();

            /// <summary>
            /// Proyecto: JJ
            /// Autor: Ricardo Silva
            /// Funcionalidad: Realizar la carga de la consulta anterior para algunos campos
            /// Fecha: 30/07/2012
            /// </summary>

            Consulta objUltimaConsulta = new Consulta();
            if (Request.QueryString["beneficiario_id"] != null && Request.QueryString["beneficiario_id"] != string.Empty)
                objUltimaConsulta.GetUltimaConsultaNuticion(Convert.ToInt64(Request.QueryString["employee_id"]), Convert.ToInt64(Request.QueryString["beneficiario_id"]));
            else
                objUltimaConsulta.GetUltimaConsultaNuticion(Convert.ToInt64(Request.QueryString["employee_id"]), 0);

            if (objUltimaConsulta.PorcionesFrutas > 0)
            {
                int opPorcionesFrutas = objUltimaConsulta.PorcionesFrutas; 
                lblPorcionesFrutas.Text = objUltimaConsulta.GetEscalarAlimentacionInadecuada(opPorcionesFrutas);               
            }

            if (objUltimaConsulta.PorcionesVegetales > 0)
            {
                int opPorcionesVegetales = objUltimaConsulta.PorcionesVegetales;
                lblPorcionesVegetales.Text = objUltimaConsulta.GetEscalarAlimentacionInadecuada(opPorcionesVegetales);
            }

            if (objUltimaConsulta.FrecuenciaCarne > 0)
            {
                int opFrecuenciaCarne = objUltimaConsulta.FrecuenciaCarne;
                lblFrecuenciaCarne.Text = objUltimaConsulta.GetEscalarAlimentacionInadecuada(opFrecuenciaCarne);
            }

            if (objUltimaConsulta.FrecuenciaSano > 0)
            {
                int opFrecuenciaSano = objUltimaConsulta.FrecuenciaSano;
                lblFrecuenciaSano.Text = objUltimaConsulta.GetEscalarAlimentacionInadecuada(opFrecuenciaSano);
            }

            if (objUltimaConsulta.FrecuenciaGranos > 0)
            {
                int opFrecuenciaGranos = objUltimaConsulta.FrecuenciaGranos;
                lblFrecuenciaGranos.Text = objUltimaConsulta.GetEscalarAlimentacionInadecuada(opFrecuenciaGranos);
            }

            if (objUltimaConsulta.FrecuenciaAzucar > 0)
            {
                int opFrecuenciaAzucar = objUltimaConsulta.FrecuenciaAzucar;
                lblFrecuenciaAzucar.Text = objUltimaConsulta.GetEscalarAlimentacionInadecuada(opFrecuenciaAzucar);
            }

            if (objUltimaConsulta.FrecuenciaSano > 0)
            {
                int opFrecuenciaSodio = objUltimaConsulta.FrecuenciaSodio;
                lblFrecuenciaSodio.Text = objUltimaConsulta.GetEscalarAlimentacionInadecuada(opFrecuenciaSodio);
            }

          
        }
           
        

        /// <summary>
        /// Proyecto: Wellness
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Se cargan los subProgramas en los que se inscribio al empleado en SIAU
        /// Fecha: 02-05-2010
        /// </summary>
        private void CargarRecomendaciones()
        {
            ConsultaOpcion objOpcion = new ConsultaOpcion();
            objOpcion.IdConsulta = int.Parse(Request.QueryString["IdConsulta"]);

            DataSet dsOpciones = objOpcion.ConsultConsultaSeleccionWellness();
            
        }

        /// <summary>
        /// Método, Carga una consulta en el forma
        /// </summary>
        public void LoadFormConsulta(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.GetConsulta();
            objConsulta.GetConsultaNutricion();
            ViewState["IdSolicitud"] = objConsulta.IdSolicitud;
           if (objConsulta.Usuario_idCreacion != 0)
            {
                SIC_USUARIO objUsuario = new SIC_USUARIO();
                objUsuario.Usuario_id = objConsulta.Usuario_idCreacion;
                DataTable dtUsuario = objUsuario.ConsultSIC_USUARIO(2).Tables[0];
            }
            
            if (Session["IdPrestador"] != null && Convert.ToInt32(Session["IdPrestador"]) != 0)
            {
                GeneralTable objGeneral = new GeneralTable();
                objGeneral.TableName = "Prestadores";
                objGeneral.ColumnName = "Prestador";
                objGeneral.Id = objConsulta.IdPrestador;
                objGeneral.GetGeneralTable();
            }

            if (objConsulta.ExisteConsultaNutricion() && (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty))
            {
                DisableTextBoxesRecursive(Page);                
                this.btnCancelar.Visible = true;
                this.btnGuardar.Visible = true;
                this.cblAlimentosAgrado.Enabled = false;
                this.cblAlimentosDisguntan.Enabled = false;
                this.cblCompraAlimentos.Enabled = false;
                this.cblPreparaAlimentos.Enabled = false;              

            }
            
             //NUTRICIÓN
            if (objConsulta.Desayuno != -1)
                this.rblDesayuno.SelectedValue = objConsulta.Desayuno.ToString();
            if (objConsulta.DesayunoHora != 0)
                this.ddlDesayunoHora.SelectedValue = objConsulta.DesayunoHora.ToString();
            if (objConsulta.Almuerzo != -1)
                this.rblAlmuerzo.SelectedValue= objConsulta.Almuerzo.ToString();
            if (objConsulta.AlmuerzoHora  != 0)
                this.ddlAlmuerzoHora.SelectedValue= objConsulta.AlmuerzoHora.ToString() ;
            if (objConsulta.Comida != -1)
                this.rblComida.SelectedValue= objConsulta.Comida.ToString();
            if (objConsulta.ComidaHora != 0)
                this.ddlComidaHora.SelectedValue = objConsulta.ComidaHora.ToString();
            if (objConsulta.Entremes != -1)
                this.rblEntremes.SelectedValue = objConsulta.Entremes.ToString();
            if (objConsulta.EntremesHora != 0)
                this.ddlEntremesHora.SelectedValue = objConsulta.EntremesHora.ToString();
            if (objConsulta.Cena != -1)
                this.rblCena.SelectedValue = objConsulta.Cena.ToString();
            if (objConsulta.CenaHora != 0)
                this.ddlCenaHora.SelectedValue = objConsulta.CenaHora.ToString();
            if (objConsulta.DiametroCintura != 0)
                txtDiametroCintura.Text =  string.Format("{0:N2}",objConsulta.DiametroCintura) ;
            if (objConsulta.DiametroCadera != 0)
                txtDiametroCadera.Text= string.Format("{0:N2}",objConsulta.DiametroCadera) ;
            if (objConsulta.RelacionCinturaCadera != 0)
                txtRelacionCinturaCadera.Text = string.Format("{0:N2}", objConsulta.RelacionCinturaCadera);
            if (objConsulta.DescripcionRelacion != -1)
                this.rblDescripcionRelacion.SelectedValue= objConsulta.DescripcionRelacion.ToString();
            if (objConsulta.MasaGrasa != 0)
                txtMasaGrasa.Text = string.Format("{0:N2}",objConsulta.MasaGrasa);
            if (objConsulta.MasaGrama != 0)
                txtMasaGrama.Text = string.Format("{0:N2}",objConsulta.MasaGrama);
            if (objConsulta.PesoRecomendable != 0)
                txtPesoRecomendable.Text = string.Format("{0:N2}",objConsulta.PesoRecomendable);
            if (objConsulta.ExcedenteGrasa != 0)
                txtExcedenteGrasa.Text = string.Format("{0:N2}",objConsulta.ExcedenteGrasa) ;
            if (objConsulta.DiagnosticoNutricional != 0)
                ddlDiagnosticoNutricional.SelectedValue = objConsulta.DiagnosticoNutricional.ToString();
            this.WC_AdicionarDiagnosticoConsultaCIE101.LoadControlDiagnosticos(objConsulta.IdDiagnosticoNutricional);
            if (objConsulta.RecomendacionesNutricionales != string.Empty)
                txtRecomendacionesNutricionales.Text= objConsulta.RecomendacionesNutricionales ;
            if (objConsulta.AlimentacionSaludable != -1)
                this.rblAlimentacionSaludable.SelectedValue = objConsulta.AlimentacionSaludable.ToString();
            //Fase 2
            if (objConsulta.PesoHace6Meses != 0)
                txtPesoHace6Meses.Text = string.Format("{0:N2}", objConsulta.PesoHace6Meses);
            if (objConsulta.PesoFluctuacion != -1)
                this.rblPesoFluctuacion.SelectedValue = objConsulta.PesoFluctuacion.ToString();
            if (objConsulta.ConsideracionApetito != 0)
                this.ddlConsideracionApetito.SelectedValue = objConsulta.ConsideracionApetito.ToString();
            if (objConsulta.EliminacionIntestinal != 0)
                this.ddlEliminacionIntestinal.SelectedValue = objConsulta.EliminacionIntestinal.ToString();
            if (objConsulta.IntoranciaAlimento != -1)
                this.rblIntoranciaAlimento.SelectedValue = objConsulta.IntoranciaAlimento.ToString();
            if (objConsulta.IntoranciaAlimentoEspecificacion != string.Empty)
                txtIntoranciaAlimentoEspecificacion.Text = objConsulta.IntoranciaAlimentoEspecificacion;
            if (objConsulta.AlergiaAlimento != -1)
                this.rblAlergiaAlimento.SelectedValue = objConsulta.AlergiaAlimento.ToString();
            if (objConsulta.AlergiaAlimentoEspecificacion != string.Empty)
                txtAlergiaAlimentoEspecificacion.Text = objConsulta.AlergiaAlimentoEspecificacion;
            if (objConsulta.DesayunoLugar != 0)
                this.ddlDesayunoLugar.SelectedValue = objConsulta.DesayunoLugar.ToString();
            if (objConsulta.AlmuerzoLugar != 0)
                this.ddlAlmuerzoLugar.SelectedValue = objConsulta.AlmuerzoLugar.ToString();
            if (objConsulta.ComidaLugar != 0)
                this.ddlComidaLugar.SelectedValue = objConsulta.ComidaLugar.ToString();
            if (objConsulta.EntremesLugar != 0)
                this.ddlEntremesLugar.SelectedValue = objConsulta.EntremesLugar.ToString();
            if (objConsulta.CenaLugar != 0)
                this.ddlCenaLugar.SelectedValue = objConsulta.CenaLugar.ToString();
            if (objConsulta.EstarSatisfecho != 0)
                this.ddlEstarSatisfecho.SelectedValue = objConsulta.EstarSatisfecho.ToString();
            if (objConsulta.SatisfaccionFacilidad != 0)
                this.ddlSatisfaccionFacilidad.SelectedValue = objConsulta.SatisfaccionFacilidad.ToString();
            if (objConsulta.ReconocerHambre != 0)
                this.ddlReconocerHambre.SelectedValue = objConsulta.ReconocerHambre.ToString();
            if (objConsulta.ComerDespacio != 0)
                this.ddlComerDespacio.SelectedValue = objConsulta.ComerDespacio.ToString();
            if (objConsulta.MayorApetitoHora != 0)
                this.ddlMayorApetitoHora.SelectedValue = objConsulta.MayorApetitoHora.ToString();
            if (objConsulta.AntojosHora != 0)
                this.ddlAntojosHora.SelectedValue = objConsulta.AntojosHora.ToString();
            if (objConsulta.SometidoDieta != -1)
                this.rblSometidoDieta.SelectedValue = objConsulta.SometidoDieta.ToString();
            if (objConsulta.LlevasDieta != -1)
                this.rblLlevasDieta.SelectedValue = objConsulta.LlevasDieta.ToString();
            if (objConsulta.QuienPrescribe != 0)
                this.ddlQuienPrescribe.SelectedValue = objConsulta.QuienPrescribe.ToString();
            if (objConsulta.MotivoIniciarDieta != 0)
                this.ddlMotivoIniciarDieta.SelectedValue = objConsulta.MotivoIniciarDieta.ToString();
            if (objConsulta.IngestionAlimentos != 0)
                this.ddlIngestionAlimentos.SelectedValue = objConsulta.IngestionAlimentos.ToString();
            if (objConsulta.BajarPesoPrescrito != 0)
                this.ddlBajarPesoPrescrito.SelectedValue = objConsulta.BajarPesoPrescrito.ToString();
            if (objConsulta.BajarPesoPrescritoEspecificacion != string.Empty)
                txtBajarPesoPrescritoEspecificacion.Text = objConsulta.BajarPesoPrescritoEspecificacion;
            if (objConsulta.TrastornoAlimentacion != -1)
                this.rblTrastornoAlimentacion.SelectedValue = objConsulta.TrastornoAlimentacion.ToString();
            this.WC_AdicionarDiagnosticoConsultaCIE1.LoadControlDiagnosticos(objConsulta.IdDiagnosticoTrastorno);
            if (objConsulta.PadecerTrastorno != 0)
                this.ddlPadecerTrastorno.SelectedValue = objConsulta.PadecerTrastorno.ToString();
            if (objConsulta.LevantarseEntreSemana != 0)
                this.ddlLevantarseEntreSemana.SelectedValue = objConsulta.LevantarseEntreSemana.ToString();
            if (objConsulta.LevantarseFinDeSemana != 0)
                this.ddlLevantarseFinDeSemana.SelectedValue = objConsulta.LevantarseFinDeSemana.ToString();
            if (objConsulta.SalirCasaEntreSemana != 0)
                this.ddlSalirCasaEntreSemana.SelectedValue = objConsulta.SalirCasaEntreSemana.ToString();
            if (objConsulta.SalirCasaFinDeSemana != 0)
                this.ddlSalirCasaFinDeSemana.SelectedValue = objConsulta.SalirCasaFinDeSemana.ToString();
            if (objConsulta.AcostarseEntreSemana != 0)
                this.ddlAcostarseEntreSemana.SelectedValue = objConsulta.AcostarseEntreSemana.ToString();
            if (objConsulta.AcostarseFinDeSemana != 0)
                this.ddlAcostarseFinDeSemana.SelectedValue = objConsulta.AcostarseFinDeSemana.ToString();
            if (objConsulta.ComidaRapida != 0)
                this.ddlComidaRapida.SelectedValue = objConsulta.ComidaRapida.ToString();
            if (objConsulta.VasosAgua != 0)
                this.ddlVasosAgua.SelectedValue = objConsulta.VasosAgua.ToString();
            if (objConsulta.ConsultaOpcion != null)
            {
                foreach (ConsultaOpcion objConsultaOpcion in objConsulta.ConsultaOpcion)
                {
                    if (objConsultaOpcion.IdPreguntaRespuestaPadre == 209)
                    {
                        for (int i = 0; i < cblCompraAlimentos.Items.Count; i++)
                        {
                            if (cblCompraAlimentos.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                            {
                                cblCompraAlimentos.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                    if (objConsultaOpcion.IdPreguntaRespuestaPadre == 214)
                    {
                        for (int i = 0; i < cblPreparaAlimentos.Items.Count; i++)
                        {
                            if (cblPreparaAlimentos.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                            {
                                cblPreparaAlimentos.Items[i].Selected = true;
                                break;
                            }
                        }

                    }
                    if (objConsultaOpcion.IdPreguntaRespuestaPadre == 228)
                    {
                        for (int i = 0; i < cblAlimentosAgrado.Items.Count; i++)
                        {
                            if (cblAlimentosAgrado.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                            {
                                cblAlimentosAgrado.Items[i].Selected = true;
                                break;
                            }
                        }

                    }
                    if (objConsultaOpcion.IdPreguntaRespuestaPadre == 244)
                    {
                        for (int i = 0; i < cblAlimentosDisguntan.Items.Count; i++)
                        {
                            if (cblAlimentosDisguntan.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                            {
                                cblAlimentosDisguntan.Items[i].Selected = true;
                                break;
                            }
                        }

                    }
                }
            }

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

                   
                    if (objEmpresaDivisiones.DivNutricion)
                    {
                        divNutricion.Visible = true;
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
        public void UpdateConsultaCompletaNutricion(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            this.LoadObjectConsulta(objConsulta);
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.UpdateConsultaNutricion();
        }

        /// <summary>
        /// Método, inserta una nueva consulta
        /// </summary>
        /// <returns></returns>
        public long InsertConsultaNutricion(long p_idConsulta)
        {
            long idConsulta;
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            this.LoadObjectConsulta(objConsulta);
            idConsulta = objConsulta.InsertConsultaNutricion();
            return idConsulta;
        }

        /// <summary>
        /// Método, carga un objeto consulta con los datos de la forma
        /// </summary>
        public void LoadObjectConsulta(Consulta objConsulta)
        {
            string strSeparadorMiles = ConfigurationManager.AppSettings["SeparadorMiles"].ToString().Trim();
           
            //NUTRICIÓN
            if (this.rblDesayuno.SelectedIndex > -1)
                objConsulta.Desayuno = Convert.ToInt32(this.rblDesayuno.SelectedValue);
            else
                objConsulta.Desayuno = -1;
            if (this.ddlDesayunoHora.SelectedValue != "0" && this.ddlDesayunoHora.SelectedValue != string.Empty)
                objConsulta.DesayunoHora = short.Parse(ddlDesayunoHora.SelectedValue);
            if (this.rblAlmuerzo.SelectedIndex > -1)
                objConsulta.Almuerzo = Convert.ToInt32(this.rblAlmuerzo.SelectedValue);
            else
                objConsulta.Almuerzo = -1;
            if (this.ddlAlmuerzoHora.SelectedValue != "0" && this.ddlAlmuerzoHora.SelectedValue != string.Empty)
                objConsulta.AlmuerzoHora = short.Parse(ddlAlmuerzoHora.SelectedValue);
            if (this.rblComida.SelectedIndex > -1)
                objConsulta.Comida = Convert.ToInt32(this.rblComida.SelectedValue);
            else
                objConsulta.Comida = -1;
            if (this.ddlComidaHora.SelectedValue != "0" && this.ddlComidaHora.SelectedValue != string.Empty)
                objConsulta.ComidaHora = short.Parse(ddlComidaHora.SelectedValue);
            if (this.rblEntremes.SelectedIndex > -1)
                objConsulta.Entremes = Convert.ToInt32(this.rblEntremes.SelectedValue);
            else
                objConsulta.Entremes = -1;
            if (this.ddlEntremesHora.SelectedValue != "0" && this.ddlEntremesHora.SelectedValue != string.Empty)
                objConsulta.EntremesHora = short.Parse(ddlEntremesHora.SelectedValue);
            if (this.rblCena.SelectedIndex > -1)
                objConsulta.Cena = Convert.ToInt32(this.rblCena.SelectedValue);
            else
                objConsulta.Cena = -1;
            if (this.ddlCenaHora.SelectedValue != "0" && this.ddlCenaHora.SelectedValue != string.Empty)
                objConsulta.CenaHora = short.Parse(ddlCenaHora.SelectedValue);
            if (this.txtDiametroCintura.Text != string.Empty)
                objConsulta.DiametroCintura = decimal.Parse(txtDiametroCintura.Text.Replace(strSeparadorMiles, ""));
            if (this.txtDiametroCadera.Text != string.Empty)
                objConsulta.DiametroCadera = decimal.Parse(txtDiametroCadera.Text.Replace(strSeparadorMiles, ""));
            if (this.txtDiametroCintura.Text != string.Empty && this.txtDiametroCadera.Text != string.Empty)
                objConsulta.RelacionCinturaCadera = decimal.Parse(txtDiametroCintura.Text.Replace(strSeparadorMiles, "")) / decimal.Parse(txtDiametroCadera.Text.Replace(strSeparadorMiles, ""));
            if (this.rblDescripcionRelacion.SelectedIndex > -1)
                objConsulta.DescripcionRelacion = Convert.ToInt32(this.rblDescripcionRelacion.SelectedValue);
            else
                 objConsulta.DescripcionRelacion = -1;
            if (this.txtMasaGrasa.Text != string.Empty)
                objConsulta.MasaGrasa = decimal.Parse(txtMasaGrasa.Text.Replace(strSeparadorMiles, ""));
            if (this.txtMasaGrama.Text != string.Empty)
                objConsulta.MasaGrama = decimal.Parse(txtMasaGrama.Text.Replace(strSeparadorMiles, ""));
            if (this.txtPesoRecomendable.Text != string.Empty)
                objConsulta.PesoRecomendable = decimal.Parse(txtPesoRecomendable.Text.Replace(strSeparadorMiles, ""));
            if (this.txtExcedenteGrasa.Text != string.Empty)
                objConsulta.ExcedenteGrasa = decimal.Parse(txtExcedenteGrasa.Text.Replace(strSeparadorMiles, ""));
            if (this.ddlDiagnosticoNutricional.SelectedValue != "0" && this.ddlDiagnosticoNutricional.SelectedValue != string.Empty)
                objConsulta.DiagnosticoNutricional = int.Parse(ddlDiagnosticoNutricional.SelectedValue);
            this.WC_AdicionarDiagnosticoConsultaCIE101.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoNutricional);
            if (this.txtRecomendacionesNutricionales.Text != string.Empty)
                objConsulta.RecomendacionesNutricionales = txtRecomendacionesNutricionales.Text.Trim();
            if (this.rblAlimentacionSaludable.SelectedIndex > -1)
                objConsulta.AlimentacionSaludable = Convert.ToInt32(this.rblAlimentacionSaludable.SelectedValue);
            else
                objConsulta.AlimentacionSaludable = -1;
            //Fase 2
            if (this.txtPesoHace6Meses.Text != string.Empty)
                objConsulta.PesoHace6Meses = decimal.Parse(txtPesoHace6Meses.Text.Replace(strSeparadorMiles, ""));
            if (this.rblPesoFluctuacion.SelectedIndex > -1)
                objConsulta.PesoFluctuacion = Convert.ToInt32(this.rblPesoFluctuacion.SelectedValue);
            else
                objConsulta.PesoFluctuacion = -1;
            if (this.ddlConsideracionApetito.SelectedValue != "0" && this.ddlConsideracionApetito.SelectedValue != string.Empty)
                objConsulta.ConsideracionApetito = int.Parse(ddlConsideracionApetito.SelectedValue);
            if (this.ddlEliminacionIntestinal.SelectedValue != "0" && this.ddlEliminacionIntestinal.SelectedValue != string.Empty)
                objConsulta.EliminacionIntestinal = int.Parse(ddlEliminacionIntestinal.SelectedValue);
            if (this.rblIntoranciaAlimento.SelectedIndex > -1)
                objConsulta.IntoranciaAlimento = Convert.ToInt32(this.rblIntoranciaAlimento.SelectedValue);
            else
                objConsulta.IntoranciaAlimento = -1;
            if (this.txtIntoranciaAlimentoEspecificacion.Text != string.Empty)
                objConsulta.IntoranciaAlimentoEspecificacion = txtIntoranciaAlimentoEspecificacion.Text.Trim();
            if (this.rblAlergiaAlimento.SelectedIndex > -1)
                objConsulta.AlergiaAlimento= Convert.ToInt32(this.rblAlergiaAlimento.SelectedValue);
            else
                objConsulta.AlergiaAlimento = -1;
            if (this.txtAlergiaAlimentoEspecificacion.Text != string.Empty)
                objConsulta.AlergiaAlimentoEspecificacion = txtAlergiaAlimentoEspecificacion.Text.Trim();
            
            ArrayList arrOpciones = new ArrayList();

            for (int i = 0; i < this.cblCompraAlimentos.Items.Count; i++)
            {
                if (cblCompraAlimentos.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 209;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblCompraAlimentos.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                }
            }
            for (int i = 0; i < this.cblPreparaAlimentos.Items.Count; i++)
            {
                if (cblPreparaAlimentos.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 214;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblPreparaAlimentos.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                }
            }
            if (this.ddlDesayunoLugar.SelectedValue != "0" && this.ddlDesayunoLugar.SelectedValue != string.Empty)
                objConsulta.DesayunoLugar = int.Parse(ddlDesayunoLugar.SelectedValue);
            if (this.ddlAlmuerzoLugar.SelectedValue != "0" && this.ddlAlmuerzoLugar.SelectedValue != string.Empty)
                objConsulta.AlmuerzoLugar = int.Parse(ddlAlmuerzoLugar.SelectedValue);
            if (this.ddlComidaLugar.SelectedValue != "0" && this.ddlComidaLugar.SelectedValue != string.Empty)
                objConsulta.ComidaLugar = int.Parse(ddlComidaLugar.SelectedValue);
            if (this.ddlEntremesLugar.SelectedValue != "0" && this.ddlEntremesLugar.SelectedValue != string.Empty)
                objConsulta.EntremesLugar = int.Parse(ddlEntremesLugar.SelectedValue);
            if (this.ddlCenaLugar.SelectedValue != "0" && this.ddlCenaLugar.SelectedValue != string.Empty)
                objConsulta.CenaLugar = int.Parse(ddlCenaLugar.SelectedValue);
            if (this.ddlEstarSatisfecho.SelectedValue != "0" && this.ddlEstarSatisfecho.SelectedValue != string.Empty)
                objConsulta.EstarSatisfecho = int.Parse(ddlEstarSatisfecho.SelectedValue);
            if (this.ddlSatisfaccionFacilidad.SelectedValue != "0" && this.ddlSatisfaccionFacilidad.SelectedValue != string.Empty)
                objConsulta.SatisfaccionFacilidad = int.Parse(ddlSatisfaccionFacilidad.SelectedValue);
            if (this.ddlReconocerHambre.SelectedValue != "0" && this.ddlReconocerHambre.SelectedValue != string.Empty)
                objConsulta.ReconocerHambre = int.Parse(ddlReconocerHambre.SelectedValue);
            if (this.ddlComerDespacio.SelectedValue != "0" && this.ddlComerDespacio.SelectedValue != string.Empty)
                objConsulta.ComerDespacio = int.Parse(ddlComerDespacio.SelectedValue);
            for (int i = 0; i < this.cblAlimentosAgrado.Items.Count; i++)
            {
                if (cblAlimentosAgrado.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 228;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblAlimentosAgrado.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                }
            }
            for (int i = 0; i < this.cblAlimentosDisguntan.Items.Count; i++)
            {
                if (cblAlimentosDisguntan.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 244;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblAlimentosDisguntan.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                }
            }
            if (this.ddlMayorApetitoHora.SelectedValue != "0" && this.ddlMayorApetitoHora.SelectedValue != string.Empty)
                objConsulta.MayorApetitoHora = short.Parse(ddlMayorApetitoHora.SelectedValue);
            if (this.ddlAntojosHora.SelectedValue != "0" && this.ddlAntojosHora.SelectedValue != string.Empty)
                objConsulta.AntojosHora = short.Parse(ddlAntojosHora.SelectedValue);
            if (this.rblSometidoDieta.SelectedIndex > -1)
                objConsulta.SometidoDieta = Convert.ToInt32(this.rblSometidoDieta.SelectedValue);
            else
                objConsulta.SometidoDieta = -1;
            if (this.rblLlevasDieta.SelectedIndex > -1)
                objConsulta.LlevasDieta = Convert.ToInt32(this.rblLlevasDieta.SelectedValue);
            else
                objConsulta.LlevasDieta = -1;
            if (this.ddlQuienPrescribe.SelectedValue != "0" && this.ddlQuienPrescribe.SelectedValue != string.Empty)
                objConsulta.QuienPrescribe = int.Parse(ddlQuienPrescribe.SelectedValue);
            if (this.ddlMotivoIniciarDieta.SelectedValue != "0" && this.ddlMotivoIniciarDieta.SelectedValue != string.Empty)
                objConsulta.MotivoIniciarDieta = int.Parse(ddlMotivoIniciarDieta.SelectedValue);
            if (this.ddlIngestionAlimentos.SelectedValue != "0" && this.ddlIngestionAlimentos.SelectedValue != string.Empty)
                objConsulta.IngestionAlimentos = int.Parse(ddlIngestionAlimentos.SelectedValue);
            if (this.ddlBajarPesoPrescrito.SelectedValue != "0" && this.ddlBajarPesoPrescrito.SelectedValue != string.Empty)
                objConsulta.BajarPesoPrescrito = int.Parse(ddlBajarPesoPrescrito.SelectedValue);
            if (this.txtBajarPesoPrescritoEspecificacion.Text != string.Empty)
                objConsulta.BajarPesoPrescritoEspecificacion = txtBajarPesoPrescritoEspecificacion.Text.Trim();
            if (this.rblTrastornoAlimentacion.SelectedIndex > -1)
                objConsulta.TrastornoAlimentacion = Convert.ToInt32(this.rblTrastornoAlimentacion.SelectedValue);
            else
                objConsulta.TrastornoAlimentacion = -1;
            this.WC_AdicionarDiagnosticoConsultaCIE1.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoTrastorno);
            if (this.ddlPadecerTrastorno.SelectedValue != "0" && this.ddlPadecerTrastorno.SelectedValue != string.Empty)
                objConsulta.PadecerTrastorno = int.Parse(ddlPadecerTrastorno.SelectedValue);
            if (this.ddlLevantarseEntreSemana.SelectedValue != "0" && this.ddlLevantarseEntreSemana.SelectedValue != string.Empty)
                objConsulta.LevantarseEntreSemana = short.Parse(ddlLevantarseEntreSemana.SelectedValue);
            if (this.ddlLevantarseFinDeSemana.SelectedValue != "0" && this.ddlLevantarseFinDeSemana.SelectedValue != string.Empty)
                objConsulta.LevantarseFinDeSemana = short.Parse(ddlLevantarseFinDeSemana.SelectedValue);
            if (this.ddlSalirCasaEntreSemana.SelectedValue != "0" && this.ddlSalirCasaEntreSemana.SelectedValue != string.Empty)
                objConsulta.SalirCasaEntreSemana = short.Parse(ddlSalirCasaEntreSemana.SelectedValue);
            if (this.ddlSalirCasaFinDeSemana.SelectedValue != "0" && this.ddlSalirCasaFinDeSemana.SelectedValue != string.Empty)
                objConsulta.SalirCasaFinDeSemana = short.Parse(ddlSalirCasaFinDeSemana.SelectedValue);
            if (this.ddlAcostarseEntreSemana.SelectedValue != "0" && this.ddlAcostarseEntreSemana.SelectedValue != string.Empty)
                objConsulta.AcostarseEntreSemana = short.Parse(ddlAcostarseEntreSemana.SelectedValue);
            if (this.ddlAcostarseFinDeSemana.SelectedValue != "0" && this.ddlAcostarseFinDeSemana.SelectedValue != string.Empty)
                objConsulta.AcostarseFinDeSemana = short.Parse(ddlAcostarseFinDeSemana.SelectedValue);
            if (this.ddlComidaRapida.SelectedValue != "0" && this.ddlComidaRapida.SelectedValue != string.Empty)
                objConsulta.ComidaRapida = int.Parse(ddlComidaRapida.SelectedValue);
            if (this.ddlVasosAgua.SelectedValue != "0" && this.ddlVasosAgua.SelectedValue != string.Empty)
                objConsulta.VasosAgua = int.Parse(ddlVasosAgua.SelectedValue);
            objConsulta.ConsultaOpcion = arrOpciones;

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
            this.imbHistorial.Click += new System.Web.UI.ImageClickEventHandler(this.imbHistorial_Click);
            this.lnkVerHistorico.Click += new System.EventHandler(this.lnkVerHistorico_Click);
            this.imbVerHistorialOrdenes.Click += new System.Web.UI.ImageClickEventHandler(this.imbVerHistorialOrdenes_Click);
            this.lnkVerHistorialOrdenes.Click += new System.EventHandler(this.lnkVerHistorialOrdenes_Click);
            this.imblnkVerHistoria.Click += new System.Web.UI.ImageClickEventHandler(this.imbVerHistoria_Click);
            this.lnkVerHistoria.Click += new System.EventHandler(this.lnkVerHistoria_Click);  
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

                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaNutricion())
                    {
                        long idConsulta = this.InsertConsultaNutricion(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaNutricion, idConsulta, "Ingreso consulta nutrición" + idConsulta);

                        if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));                         
                        else
                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] );
                    }
                    else
                    {
                        this.UpdateConsultaCompletaNutricion(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaNutricion, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  nutrición" + Convert.ToInt32(Request.QueryString["IdConsulta"]));

                        if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                        {

                            if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]), false);
                            else
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"], false);
                        }

                        else
                        {
                            if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]), false);
                            else
                                Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"], false);

                            
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
        /// Evento, cancela el ingreso o modificación de la orden y retorna de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, System.EventArgs e)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objConsulta.GetConsulta();
            objConsulta.GetConsultaNutricion();
            if (objConsulta.ExisteConsultaNutricion() && (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty))
                Response.Redirect("LIS_empleado.aspx");
            else
                Response.Redirect("LIS_empleado.aspx");

        }

        /// <summary>bool
        /// Evento, abre la ventana para ver historial de toda la información de la hisotia clínica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbVerHistoria_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 1);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de la hisotia clínica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkVerHistoria_Click(object sender, System.EventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 1);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de las consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorial_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 1);
        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de las consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerHistorico_Click(object sender, System.EventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 2);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 2);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de antecedentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialAntecedentes_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=antecedentes", 850, 750, 3);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=antecendentes", 850, 750, 3);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de revisión por sistemas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialRevision_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=revision", 850, 750, 4);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=revision", 850, 750, 4);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de exámen físico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialExamen_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=examen", 850, 750, 5);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=examen", 850, 750, 5);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de diagnósticos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialDiagnosticos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=diagnosticos", 850, 750, 6);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=diagnosticos", 850, 750, 6);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de habitos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialHabitos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=habitos", 850, 750, 6);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"].ToString() + "&Modulo=habitos", 850, 750, 6);


        }

        /// <summary>
        /// Evento, llama la ventana para consultar histórico de órdenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbVerHistorialOrdenes_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
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
            if (Request.QueryString["beneficiario_id"] == null || Request.QueryString["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["beneficiario_id"], 850, 750);

        }
        
        /// <summary>
        /// Evento, para volver a la página anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnterior_Click1(object sender, EventArgs e)
        {
            EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones(int.Parse(Session["Company"].ToString()));
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
            if (!objConsulta.ExisteConsultaNutricion())
            {
                long idConsulta = this.InsertConsultaNutricion(objConsulta.IdConsulta);
                this.RegisterLog(Log.EnumActionsLog.IngresarConsultaNutricion, idConsulta, "Ingreso consulta nutrición" + idConsulta);

                if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisiones())
                    Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                else
                {
                    if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                        Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                    else
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                }
            }
            else
            {
                this.UpdateConsultaCompletaNutricion(objConsulta.IdConsulta);
                this.RegisterLog(Log.EnumActionsLog.ModificarConsultaNutricion, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  nutrición" + Convert.ToInt32(Request.QueryString["IdConsulta"]));

                if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisiones())
                    Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                else
                {
                    if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                        Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                    else
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);

                    
                }


            }      

        }
        
        
        /// <summary>
        /// Guardar la consulta temporalmente por medio de la imagen guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaNutricion())
                    {
                        long idConsulta = this.InsertConsultaNutricion(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaNutricion, idConsulta, "Ingreso consulta nutrición" + idConsulta);
                        Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );
                    }
                    else
                    {
                        this.UpdateConsultaCompletaNutricion(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaNutricion, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  nutrición" + Convert.ToInt32(Request.QueryString["IdConsulta"] ));
                        Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );
                    
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }
        /// <summary>
        /// Guardar la consulta temporalmente por medio del link guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaNutricion())
                    {
                        long idConsulta = this.InsertConsultaNutricion(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaNutricion, idConsulta, "Ingreso consulta nutrición" + idConsulta);
                        Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );
                    }
                    else
                    {
                        this.UpdateConsultaCompletaNutricion(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaNutricion, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  nutrición" + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                        Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );//MAHG 19/07/2010 Se agrega el parámetro Finalizada
                    }
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
