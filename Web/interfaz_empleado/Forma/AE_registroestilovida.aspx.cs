using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class AE_registroestilovida : PB_PaginaBase
    {
        #region Atributos
        bool _esNutriologo, _esMedico, _esTodo = false;
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
                    
                    if (Request.QueryString["IdConsulta"] != null)
                    {
                        Consulta objConsulta = new Consulta();
                        objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                        if(objConsulta.ExisteConsultaEstiloVida())
                            this.LoadFormConsulta(Convert.ToInt64(Request.QueryString["IdConsulta"]));
                    }
                }

  
                    string idConsulta = Session["idTipoConsulta"].ToString();

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

                    if (_esNutriologo)
                    {
                        divWellness.Visible = false;
                        divAntecedentesAusentismo.Visible = false;
                        divEstadoSalud.Visible = false;
                        divAccidentalidad.Visible = false;
                        divEmocional.Visible = false;
                        divEstres.Visible = false;
                        divSaludOral.Visible = false;
                        divSedentarismo.Visible = false;
                        divRutinaEjercicioGrande.Visible = false;
                        divRutinaEjercicio.Visible = false;
                        divVacunacion.Visible = false;
                        divConsumoAlcohol.Visible = false;
                        divHabitoFumar.Visible = false;
                        divRecomendaciones.Visible = false;
                        divAlimentacionInadecuada.Visible = true;
                    }
                    else if (_esMedico)
                    {
                        divWellness.Visible = true;
                        divAntecedentesAusentismo.Visible = true;
                        divEstadoSalud.Visible = true;
                        divAccidentalidad.Visible = true;
                        divEmocional.Visible = true;
                        divEstres.Visible = true;
                        divSaludOral.Visible = true;
                        divSedentarismo.Visible = true;
                        divRutinaEjercicioGrande.Visible = true;
                        divRutinaEjercicio.Visible = true;
                        divVacunacion.Visible = true;
                        divConsumoAlcohol.Visible = true;
                        divHabitoFumar.Visible = true;
                        divRecomendaciones.Visible = true;
                        divAlimentacionInadecuada.Visible = false;
                    }
                    else if (_esTodo)
                    {
                        divWellness.Visible = true;
                        divAntecedentesAusentismo.Visible = true;
                        divEstadoSalud.Visible = true;
                        divAccidentalidad.Visible = true;
                        divEmocional.Visible = true;
                        divEstres.Visible = true;
                        divSaludOral.Visible = true;
                        divSedentarismo.Visible = true;
                        divRutinaEjercicioGrande.Visible = true;
                        divRutinaEjercicio.Visible = true;
                        divVacunacion.Visible = true;
                        divConsumoAlcohol.Visible = true;
                        divHabitoFumar.Visible = true;
                        divRecomendaciones.Visible = true;
                        divAlimentacionInadecuada.Visible = true;
                    }
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "_ExitoGuardarFlex", "HabilitarValidadores(" + idConsulta + ");", true);
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

            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            objConsulta.GetConsulta();           
            ViewState["beneficiario_id"] = Convert.ToInt32(objConsulta.Beneficiario_id);

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
                        
            txtFechaFiebreAmarilla.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaHepatitisViral.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaHospitalizacion1.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaHospitalizacion2.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaHospitalizacion3.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaHospitalizacion4.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInfluenciaH1N1.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInfluenzaEstacional.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaToxoideTetanico.Attributes.Add("ReadOnly", "ReadOnly");
         
            //Carga de controles 25/02/2010
            this.FillList(this.rblConductaCigarrillo, "PreguntaRespuesta", 283);
            this.FillList(this.rblTiempoPrimerCigarrillo, "PreguntaRespuesta", 287);
            this.FillList(this.rblCigarrilloSuprimir, "PreguntaRespuesta", 290);
            this.FillList(this.rblCategoriaCigarrillos, "PreguntaRespuesta", 293);
            this.FillList(this.rblAspiraHumo, "PreguntaRespuesta", 297);
            this.FillList(this.rblSedaDental, "PreguntaRespuesta", 301);
            this.FillList(this.ddlCopasSemana, "PreguntaRespuesta", "--Copas--", 18);
            this.FillList(this.ddlPracticaDeporteSemana, "PreguntaRespuesta", "--Veces--", 24);
            this.FillList(this.ddlPromedioTiempoMinutos, "PreguntaRespuesta", "--Minutos--", 29);
            this.FillList(this.ddlHorasDiariasTV, "PreguntaRespuesta", "--Horas--", 34);
            this.FillList(this.ddlNivelEstres, "PreguntaRespuesta", "--Nivel--", 39);
            this.FillList(this.rblSentidoEstresado, "PreguntaRespuesta", 422);
            this.FillList(this.cblFuentesEstres, "PreguntaRespuesta", 50);
            this.FillList(this.rblControlarEstres, "PreguntaRespuesta", 74);
            this.FillList(this.rblCalificacionSueno, "PreguntaRespuesta", 79);
            this.FillList(this.ddlEstadoLevantarse, "PreguntaRespuesta", "--Estado--", 88);
            this.FillList(this.ddlDormidoSuficiente, "PreguntaRespuesta", "--Estado--", 434);
            this.FillList(this.ddlHorasDuermeRegular, "PreguntaRespuesta", "--Horas--", 92);
            this.FillList(this.ddlEstadoAnimoEmocional, "PreguntaRespuesta", "--Nivel--", 96);
            this.FillList(this.ddlCinturonSeguridad, "PreguntaRespuesta", "--Frecuencia--", 107);
            this.FillList(this.ddlCocheCelular, "PreguntaRespuesta", "--Frecuencia--", 112);
            this.FillList(this.ddlLimiteVelocidad, "PreguntaRespuesta", "--Seleccione--", 117);
            this.FillList(this.ddlConductorBebido, "PreguntaRespuesta", "--Frecuencia--", 121);
            this.FillList(this.ddlCasco, "PreguntaRespuesta", "--Frecuencia--", 125);
            this.FillList(this.ddlFiltroSolar, "PreguntaRespuesta", "--Frecuencia--", 130);
            this.FillList(this.ddlEstadoSalud, "PreguntaRespuesta", "--Estado--", 135);
            this.FillList(this.ddlHabitosVida, "PreguntaRespuesta", "--Estado--", 146);
            this.FillList(this.ddlLavaDientes, "PreguntaRespuesta", "--Veces--", 158);
            this.FillList(this.rblTipoActividadFisica, "PreguntaRespuesta", 197);
            this.FillList(this.ddlPorcionesFrutas, "PreguntaRespuesta","--Porciones--", 440);
            this.FillList(this.ddlPorcionesVegetales, "PreguntaRespuesta", "--Porciones--", 447);
            this.FillList(this.ddlFrecuenciaCarne, "PreguntaRespuesta","--Frecuencia--", 455);
            this.FillList(this.ddlFrecuenciaSano, "PreguntaRespuesta", "--Frecuencia--", 462);
            this.FillList(this.ddlFrecuenciaGranos, "PreguntaRespuesta", "--Porciones--", 469);
            this.FillList(this.ddlFrecuenciaAzucar, "PreguntaRespuesta","--Porciones--", 477);
            this.FillList(this.ddlFrecuenciaSodio, "PreguntaRespuesta","--Frecuencia--", 484);
            this.FillList(this.ddlPocoInteres, "PreguntaRespuesta", "--Seleccione--", 491);
            this.FillList(this.ddlSinEsperanza, "PreguntaRespuesta", "--Seleccione--", 496);
            this.FillList(this.ddlConviveFumador, "PreguntaRespuesta", "--Seleccione--", 501);
            this.FillList(this.cblQueFuma, "PreguntaRespuesta", 506);
            this.FillList(this.ddlNoRutinaEjercicio, "PreguntaRespuesta", "--Seleccione--", 571);


            /*Inicio MAHG 30/04/2010*/
            /*Se cargan las recomendaciones que tiene el empleado en SICAM*/
            //this.FillList(this.cblRecomendacion, "PreguntaRespuesta", 168);
            
            this.FillList(this.cblRecomendacion, "PreguntaRespuestaWellness", int.Parse(Request.QueryString["IdConsulta"]) );            

            /*Fin MAHG 30/04/2010*/
            this.FillList(this.ddlTrasmisionSexual, "PreguntaRespuesta", "--Veces--", 190);
                       
            //Carga las divisiones que la empresa tiene permisos.

            if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
            {
                this.divRutinaEjercicioGrande.Visible = true;
                this.divRutinaEjercicio.Visible = true;
                this.CargarDivisionesEstamosContigo();
            }
            else
            {
                this.CargarDivisiones();
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
            foreach(ListItem item in cblRecomendacion.Items)
            {                
                foreach(DataRow row in dsOpciones.Tables[0].Rows)
                {
                    if (item.Value == row["IdPreguntaRespuesta"].ToString() && Convert.ToBoolean(row["Estado"].ToString()))
                    {
                        item.Selected = true;
                        item.Enabled = false;

                        item.Text += " (Inscrito)";
                    }
                }
            }
        }

        /// <summary>
        /// Método, Carga una consulta en el forma
        /// </summary>
        public void LoadFormConsulta(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.GetConsulta();
            objConsulta.GetConsultaEstiloVida();
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
           
            if (objConsulta.ExisteConsultaEstiloVida() && (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty))
            {
                DisableTextBoxesRecursive(Page);                
                this.btnCancelar.Visible = true;
                this.btnGuardar.Visible = true;
                this.cblRecomendacion.Enabled = false;
                this.cblFuentesEstres.Enabled = false;
                this.cblQueFuma.Enabled = false;
                this.imgCalAntecendentes1.Style["display"] = "none";
                this.imgCalAntecendentes2.Style["display"] = "none";
                this.imgCalAntecendentes3.Style["display"] = "none";
                this.imgCalAntecendentes4.Style["display"] = "none";
                this.imgCalFiebre.Style["display"] = "none";
                this.imgCalH1N1.Style["display"] = "none";
                this.imgCalHepatitis.Style["display"] = "none";
                this.imgCalInfluenza.Style["display"] = "none";
                this.imgCalTetanos.Style["display"] = "none";

            }
            //WELLNESS
            if (objConsulta.ProgramaWellness != -1)
                this.rblwellness.SelectedValue = objConsulta.ProgramaWellness.ToString();
            if (objConsulta.FirmaWellness != -1)
                this.rblFirmaWellness.SelectedValue = objConsulta.FirmaWellness.ToString();

            //HABITO DE FUMAR
            if (objConsulta.ConductaCigarrillo != -1)
            {
                this.rblConductaCigarrillo.SelectedValue = objConsulta.ConductaCigarrillo.ToString();
                this.MostrarDivsCigarrillo();
            }
            if (objConsulta.TiempoPrimerCigarrillo != -1)
                this.rblTiempoPrimerCigarrillo.SelectedValue = objConsulta.TiempoPrimerCigarrillo.ToString();
            if (objConsulta.DificultadFumar != -1)
                this.rblDificultadFumar.SelectedValue = objConsulta.DificultadFumar.ToString();
            if (objConsulta.CigarrilloSuprimir != -1)
                this.rblCigarrilloSuprimir.SelectedValue = objConsulta.CigarrilloSuprimir.ToString();
            if (objConsulta.CigarrillosalDia != 0)
                txtCigarrillosalDia.Text = objConsulta.CigarrillosalDia.ToString();
            if (objConsulta.FrecuenciaPrimerasHorasDia != - 1)
                this.rblFrecuenciaPrimerasHorasDia.SelectedValue= objConsulta.FrecuenciaPrimerasHorasDia.ToString();
            if (objConsulta.FumaEnfermedad != -1)
                this.rblFumaEnfermedad.SelectedValue = objConsulta.FumaEnfermedad.ToString();
            if (objConsulta.CategoriaCigarrillos != -1)
                this.rblCategoriaCigarrillos.SelectedValue = objConsulta.CategoriaCigarrillos.ToString();
            if (objConsulta.AspiraHumo != -1)
                this.rblAspiraHumo.SelectedValue=objConsulta.AspiraHumo.ToString();
            if (objConsulta.AnosDejoFumar != 0)
                txtAnosDejoFumar.Text = objConsulta.AnosDejoFumar.ToString();
            if (objConsulta.PromedioDiarioX2Anos  != 0)
                txtPromedioDiarioX2Anos.Text = objConsulta.PromedioDiarioX2Anos.ToString();
            if (objConsulta.ConviveFumador != 0)
                this.ddlConviveFumador.SelectedValue = objConsulta.ConviveFumador.ToString();
            


            /// <summary>
            /// Cambio: Permite cargar los resultados antiguos del consumo de alcohol
            /// Autor: Ricardo Silva
            /// Fecha: 09/07/2012
            /// </summary>
            //CONSUMO ALCOHOL
            if (objConsulta.CopasSemana != 0)   
                if (ddlCopasSemana.Items.FindByValue(Convert.ToString(objConsulta.CopasSemana)) == null)
	            {
                    DataTable dtAntiguos = objConsulta.ConsultAntiguoPregunta(Convert.ToInt32(objConsulta.CopasSemana)).Tables[0];
                    DataRow drAntiguos = dtAntiguos.Rows[0];
                    ddlCopasSemana.Items.Add(new ListItem(drAntiguos["Nombre"].ToString(), drAntiguos["Id"].ToString()));
                    ddlCopasSemana.SelectedValue = objConsulta.CopasSemana.ToString();
	            }
                else
                {
                    ddlCopasSemana.SelectedValue = objConsulta.CopasSemana.ToString();
                }
                
            if (objConsulta.CriticaAlcohol != - 1)
                this.rblCriticaAlcohol.SelectedValue = objConsulta.CriticaAlcohol.ToString();
            if (objConsulta.ArrepentidoAlcohol != -1)
                this.rblArrepentidoAlcohol.SelectedValue = objConsulta.ArrepentidoAlcohol.ToString();
            if (objConsulta.LagunaAlcohol != -1)
                this.rblLagunaAlcohol.SelectedValue = objConsulta.LagunaAlcohol.ToString();
            if (objConsulta.MananaAlcohol != - 1)
                this.rblMananaAlcohol.SelectedValue = objConsulta.MananaAlcohol.ToString();

            // VACUNACION
            if (objConsulta.InfluenciaEstacional != - 1)
                this.rblInfluenciaEstacional.SelectedValue = objConsulta.InfluenciaEstacional.ToString();
            if (objConsulta.FechaInfluenzaEstacional.ToShortDateString() != "01/01/0001")
                txtFechaInfluenzaEstacional.Text = objConsulta.FechaInfluenzaEstacional.ToShortDateString();
            txtMarcaLoteInfluenciaEstacional.Text = Convert.ToString(objConsulta.MarcaLoteInfluenciaEstacional);
            if (objConsulta.InfluenciaH1N1 != -1)
                this.rblInfluenciaH1N1.SelectedValue = objConsulta.InfluenciaH1N1.ToString();
            if (objConsulta.FechaInfluenciaH1N1.ToShortDateString() != "01/01/0001")
                this.txtFechaInfluenciaH1N1.Text = objConsulta.FechaInfluenciaH1N1.ToShortDateString();
            txtMarcaLoteInfluenciaH1N1.Text = Convert.ToString(objConsulta.MarcaLoteInfluenciaH1N1);
            if (objConsulta.FiebreAmarilla != -1)
                this.rblFiebreAmarilla.SelectedValue = objConsulta.FiebreAmarilla.ToString();
            if (objConsulta.FechaFiebreAmarilla.ToShortDateString() != "01/01/0001")
                this.txtFechaFiebreAmarilla.Text = objConsulta.FechaFiebreAmarilla.ToShortDateString();
            txtMarcaLoteFiebreAmarilla.Text = Convert.ToString(objConsulta.MarcaLoteFiebreAmarilla);
            if (objConsulta.HepatitisViral != -1)
                this.rblHepatitisViral.SelectedValue = objConsulta.HepatitisViral.ToString();
            if (objConsulta.FechaHepatitisViral.ToShortDateString() != "01/01/0001")
                this.txtFechaHepatitisViral.Text = objConsulta.FechaHepatitisViral.ToShortDateString();
            txtMarcaLoteHepatitisViral.Text = Convert.ToString(objConsulta.MarcaLoteHepatitisViral);
            if (objConsulta.ToxoideTetanico != -1)
                this.rblToxoideTetanico.SelectedValue = objConsulta.ToxoideTetanico.ToString();
            if (objConsulta.FechaToxoideTetanico.ToShortDateString() != "01/01/0001")
                this.txtFechaToxoideTetanico.Text = objConsulta.FechaToxoideTetanico.ToShortDateString();
            txtMarcaLoteToxoideTetanico.Text = Convert.ToString(objConsulta.MarcaLoteToxoideTetanico);

            if (objConsulta.ToxoideTetanico != -1)
                this.rblToxoideTetanico.SelectedValue = objConsulta.ToxoideTetanico.ToString();
            if (objConsulta.FechaToxoideTetanico.ToShortDateString() != "01/01/0001")
                this.txtFechaToxoideTetanico.Text = objConsulta.FechaToxoideTetanico.ToShortDateString();
            txtMarcaLoteToxoideTetanico.Text = Convert.ToString(objConsulta.MarcaLoteToxoideTetanico);

            if (objConsulta.HepatitisViralB != -1)
                this.rblHepatitisViralB.SelectedValue = objConsulta.HepatitisViralB.ToString();
            if (objConsulta.FechaHepatitisViralB.ToShortDateString() != "01/01/0001")
                this.txtFechaHepatitisViralB.Text = objConsulta.FechaHepatitisViralB.ToShortDateString();
            txtMarcaLoteHepatitisViralB.Text = Convert.ToString(objConsulta.MarcaLoteHepatitisViralB);

            if (objConsulta.Meningococo != -1)
                this.rblMeningococo.SelectedValue = objConsulta.Meningococo.ToString();
            if (objConsulta.FechaMeningococo.ToShortDateString() != "01/01/0001")
                this.txtFechaMeningococo.Text = objConsulta.FechaMeningococo.ToShortDateString();
            txtMarcaLoteMeningococo.Text = Convert.ToString(objConsulta.MarcaLoteMeningococo);

            if (objConsulta.FiebreTifoidea != -1)
                this.rblFiebreTifoidea.SelectedValue = objConsulta.FiebreTifoidea.ToString();
            if (objConsulta.FechaFiebreTifoidea.ToShortDateString() != "01/01/0001")
                this.txtFechaFiebreTifoidea.Text = objConsulta.FechaFiebreTifoidea.ToShortDateString();
            txtMarcaLoteFiebreTifoidea.Text = Convert.ToString(objConsulta.MarcaLoteFiebreTifoidea);

            if (objConsulta.FiebreVPH != -1)
                this.rblVPH.SelectedValue = objConsulta.FiebreVPH.ToString();
            if (objConsulta.FechaVPH.ToShortDateString() != "01/01/0001")
                this.txtFechaVPH.Text = objConsulta.FechaVPH.ToShortDateString();
            txtMarcaLoteVPH.Text = Convert.ToString(objConsulta.MarcaLoteVPH);            

            /// <summary>
            /// Cambio: Permite cargar los resultados antiguos de sedentarismo
            /// Autor: Ricardo Silva
            /// Fecha: 09/07/2012
            /// </summary>
            // SEDENTARISMO
            if (objConsulta.PracticaDeporte != -1)
                this.rblPracticaDeporte.SelectedValue = objConsulta.PracticaDeporte.ToString();
            if (objConsulta.PracticaDeporteSemana != 0)
                if (ddlPracticaDeporteSemana.Items.FindByValue(Convert.ToString(objConsulta.PracticaDeporteSemana)) == null)
                {
                    DataTable dtAntiguos = objConsulta.ConsultAntiguoPregunta(Convert.ToInt32(objConsulta.PracticaDeporteSemana)).Tables[0];
                    DataRow drAntiguos = dtAntiguos.Rows[0];
                    ddlPracticaDeporteSemana.Items.Add(new ListItem(drAntiguos["Nombre"].ToString(), drAntiguos["Id"].ToString()));
                    ddlPracticaDeporteSemana.SelectedValue = objConsulta.PracticaDeporteSemana.ToString();
                }
                else
                {
                    ddlPracticaDeporteSemana.SelectedValue = objConsulta.PracticaDeporteSemana.ToString();
                }
                
            if (objConsulta.PromedioTiempoMinutos != 0)
                if (ddlPromedioTiempoMinutos.Items.FindByValue(Convert.ToString(objConsulta.PromedioTiempoMinutos)) == null)
                {
                    DataTable dtAntiguos = objConsulta.ConsultAntiguoPregunta(Convert.ToInt32(objConsulta.PromedioTiempoMinutos)).Tables[0];
                    DataRow drAntiguos = dtAntiguos.Rows[0];
                    ddlPromedioTiempoMinutos.Items.Add(new ListItem(drAntiguos["Nombre"].ToString(), drAntiguos["Id"].ToString()));
                    ddlPromedioTiempoMinutos.SelectedValue = objConsulta.PromedioTiempoMinutos.ToString();
                }
                else
                {
                    ddlPromedioTiempoMinutos.SelectedValue = objConsulta.PromedioTiempoMinutos.ToString();
                }
                
            if (objConsulta.TipoActividadFisica != -1)
                this.rblTipoActividadFisica.SelectedValue = objConsulta.TipoActividadFisica.ToString();
            if (objConsulta.HorasDiariasTV != 0)
                ddlHorasDiariasTV.SelectedValue = objConsulta.HorasDiariasTV.ToString();

            if (objConsulta.rutinaEjercicioUltimoMes != -1)
            {
                this.rblrutinaUltimoMes.SelectedValue = objConsulta.rutinaEjercicioUltimoMes.ToString();
                if (this.rblrutinaUltimoMes.SelectedIndex == 0)
                {
                    this.DivNoRutinaEjercicio.Visible = false;
                    this.divSedentarismo.Visible = true;
                }
                if (this.rblrutinaUltimoMes.SelectedIndex == 1)
                {
                    this.DivNoRutinaEjercicio.Visible = true;
                    this.divSedentarismo.Visible = false;
                }
            }
            if (objConsulta.NoRutinaEjercicio != 0)
                ddlNoRutinaEjercicio.SelectedValue = objConsulta.NoRutinaEjercicio.ToString();

            txtOtroMotivo.Text = Convert.ToString(objConsulta.OtroMotivo);

            // SALUD ORAL
            if (objConsulta.ConsultaOdontologica != -1)
                this.rblConsultaOdontologica.SelectedValue = objConsulta.ConsultaOdontologica.ToString();
            if (objConsulta.LavaDientes != 0)
                ddlLavaDientes.SelectedValue = objConsulta.LavaDientes.ToString();
            if (objConsulta.SedaDental != -1)
                this.rblSedaDental.SelectedValue = objConsulta.SedaDental.ToString();

            // ESTRES
            if (objConsulta.SentidoDecaido != -1)
                this.rblSentidoDecaido.SelectedValue = objConsulta.SentidoDecaido.ToString();
            /// <summary>
            /// Descripción: Permite cargar los datos del campo sentido cansado
            /// Autor: Ricardo Silva
            /// Fecha: 11/07/2012
            if (objConsulta.SentidoEstresado != -1)
                this.rblSentidoEstresado.SelectedValue = objConsulta.SentidoEstresado.ToString();
            if (objConsulta.InteresPlacer != -1)
                this.rblInteresPlacer.SelectedValue = objConsulta.InteresPlacer.ToString();
            if (objConsulta.NivelEstres != 0)
                this.ddlNivelEstres.SelectedValue = objConsulta.NivelEstres.ToString();
            if (objConsulta.ControlarEstres != -1)
                this.rblControlarEstres.SelectedValue= objConsulta.ControlarEstres.ToString();
            if (objConsulta.PocoInteres != -1)
                this.ddlPocoInteres.SelectedValue = objConsulta.PocoInteres.ToString();
            if (objConsulta.SinEsperanza != -1)
                this.ddlSinEsperanza.SelectedValue = objConsulta.SinEsperanza.ToString();


             
            // EMOCIONAL
            if (objConsulta.CalificacionSueno != -1)
                 this.rblCalificacionSueno.SelectedValue = objConsulta.CalificacionSueno.ToString();
            if (objConsulta.EstadoLevantarse != 0)
                 this.ddlEstadoLevantarse.SelectedValue= objConsulta.EstadoLevantarse.ToString();
            /// <summary>
            /// Descripción: Permite cargar los datos del campo dormido suficiente
            /// Autor: Ricardo Silva
            /// Fecha: 11/07/2012
            if (objConsulta.DormidoSuficiente != 0)
                this.ddlDormidoSuficiente.SelectedValue = objConsulta.DormidoSuficiente.ToString();
            if (objConsulta.HorasDuermeRegular != 0)
                 this.ddlHorasDuermeRegular.SelectedValue= objConsulta.HorasDuermeRegular.ToString();
            if (objConsulta.EstadoAnimoEmocional != 0)
                 this.ddlEstadoAnimoEmocional.SelectedValue= objConsulta.EstadoAnimoEmocional.ToString() ;

            /// <summary>
            /// Descripción: Permite cargar los resultados de la seccion ALIMENTACION INADECUADA
            /// Autor: Ricardo Silva
            /// Fecha: 13/07/2012
            /// </summary>
            //ALIMENTACION INADECUADA
            if (objConsulta.PorcionesFrutas != 0)
                this.ddlPorcionesFrutas.SelectedValue = objConsulta.PorcionesFrutas.ToString();
            if (objConsulta.PorcionesVegetales != 0)
                this.ddlPorcionesVegetales.SelectedValue = objConsulta.PorcionesVegetales.ToString();
            if (objConsulta.FrecuenciaCarne != 0)
                this.ddlFrecuenciaCarne.SelectedValue = objConsulta.FrecuenciaCarne.ToString();
            if (objConsulta.FrecuenciaSano != 0)
                this.ddlFrecuenciaSano.SelectedValue = objConsulta.FrecuenciaSano.ToString();
            if (objConsulta.FrecuenciaGranos != 0)
                this.ddlFrecuenciaGranos.SelectedValue = objConsulta.FrecuenciaGranos.ToString();
            if (objConsulta.FrecuenciaAzucar != 0)
                this.ddlFrecuenciaAzucar.SelectedValue = objConsulta.FrecuenciaAzucar.ToString();
            if (objConsulta.FrecuenciaSodio != 0)
                this.ddlFrecuenciaSodio.SelectedValue = objConsulta.FrecuenciaSodio.ToString();


            //COMPORTAMIENTOS DE RIESGO Y ACCIDENTALIDAD
            if (objConsulta.CinturonSeguridad  != 0)
                this.ddlCinturonSeguridad.SelectedValue= objConsulta.CinturonSeguridad.ToString() ;
            if (objConsulta.CocheCelular  != 0 )
                this.ddlCocheCelular.SelectedValue = objConsulta.CocheCelular.ToString();
            if (objConsulta.LimiteVelocidad != 0)
                this.ddlLimiteVelocidad.SelectedValue= objConsulta.LimiteVelocidad.ToString();
            if (objConsulta.ConductorBebido  != 0)
                this.ddlConductorBebido.SelectedValue= objConsulta.ConductorBebido.ToString() ;
            if (objConsulta.Casco != 0)
                this.ddlCasco.SelectedValue= objConsulta.Casco.ToString();
            if (objConsulta.FiltroSolar != 0)
                this.ddlFiltroSolar.SelectedValue= objConsulta.FiltroSolar.ToString();
            if (objConsulta.SeguridadDomestica != -1)
                this.rblSeguridadDomestica.SelectedValue = objConsulta.SeguridadDomestica.ToString();
            if (objConsulta.TrasmisionSexual != 0)
                this.ddlTrasmisionSexual.SelectedValue = objConsulta.TrasmisionSexual.ToString();
            
            //PERCEPCIÓN DEL ESTADO DE SALUD
            /// <summary>
            /// Cambio: Permite cargar los resultados antiguos del estado de salud
            /// Autor: Ricardo Silva
            /// Fecha: 11/07/2012
            /// </summary>
            if (objConsulta.EstadoSalud != 0)
                if (ddlEstadoSalud.Items.FindByValue(Convert.ToString(objConsulta.EstadoSalud)) == null)
                {
                    DataTable dtAntiguos = objConsulta.ConsultAntiguoPregunta(Convert.ToInt32(objConsulta.EstadoSalud)).Tables[0];
                    DataRow drAntiguos = dtAntiguos.Rows[0];
                    ddlEstadoSalud.Items.Add(new ListItem(drAntiguos["Nombre"].ToString(), drAntiguos["Id"].ToString()));
                    ddlEstadoSalud.SelectedValue = objConsulta.EstadoSalud.ToString();
                }
                else
                {
                    this.ddlEstadoSalud.SelectedValue = objConsulta.EstadoSalud.ToString();
                }

            if (objConsulta.HabitosVida != 0)
                this.ddlHabitosVida.SelectedValue= objConsulta.HabitosVida.ToString();
            
            //ANTECEDENTES AUSENTISMO
            if (objConsulta.Incapacitado != -1)
                this.rblIncapacitado.SelectedValue = objConsulta.Incapacitado.ToString() ;
            this.WC_AdicionarDiagnosticoConsultaCIE1.LoadControlDiagnosticos(objConsulta.IdDiagnosticoIncapacidad);
            if (objConsulta.DiasIncapacidad != 0)
                txtDiasIncapacidad.Text = objConsulta.DiasIncapacidad.ToString();
            this.WC_AdicionarDiagnosticoConsultaCIE2.LoadControlDiagnosticos(objConsulta.IdDiagnosticoHospitalizacion1);
            if (objConsulta.FechaHospitalizacion1.ToShortDateString() != "01/01/0001")
                txtFechaHospitalizacion1.Text = objConsulta.FechaHospitalizacion1.ToShortDateString();
            if (objConsulta.DiasHospitalizacion1 != 0)
                txtDiasHospitalizacion1.Text = objConsulta.DiasHospitalizacion1.ToString() ;
            this.WC_AdicionarDiagnosticoConsultaCIE3.LoadControlDiagnosticos(objConsulta.IdDiagnosticoHospitalizacion2);
            if (objConsulta.FechaHospitalizacion2.ToShortDateString() != "01/01/0001")
                txtFechaHospitalizacion2.Text = objConsulta.FechaHospitalizacion2.ToShortDateString();
            if (objConsulta.DiasHospitalizacion2  != 0)
                txtDiasHospitalizacion2.Text = objConsulta.DiasHospitalizacion2.ToString();
            this.WC_AdicionarDiagnosticoConsultaCIE4.LoadControlDiagnosticos(objConsulta.IdDiagnosticoHospitalizacion3);
            if (objConsulta.FechaHospitalizacion3.ToShortDateString() != "01/01/0001")
                txtFechaHospitalizacion3.Text = objConsulta.FechaHospitalizacion3.ToShortDateString();
            if (objConsulta.DiasHospitalizacion3  != 0)
                txtDiasHospitalizacion3.Text = objConsulta.DiasHospitalizacion3.ToString();
            this.WC_AdicionarDiagnosticoConsultaCIE5.LoadControlDiagnosticos(objConsulta.IdDiagnosticoHospitalizacion4);
            if (objConsulta.FechaHospitalizacion4.ToShortDateString() != "01/01/0001")
                txtFechaHospitalizacion4.Text = objConsulta.FechaHospitalizacion4.ToShortDateString();
            if (objConsulta.DiasHospitalizacion4 != 0)
                txtDiasHospitalizacion4.Text = objConsulta.DiasHospitalizacion4.ToString();
            if (objConsulta.ConsultaOpcion != null)
            {
                /// <summary>
                /// Cambio: Permite cargar los resultados antiguos de la lista de fuentes de estrés
                /// Autor: Ricardo Silva
                /// Fecha: 10/07/2012
                /// </summary>
                foreach (ConsultaOpcion objConsultaOpcion in objConsulta.ConsultaOpcion)
                {
                    if (objConsultaOpcion.IdPreguntaRespuestaPadre == 50)
                    {
                        int bandera;
                        bandera = 0;
                        for (int i = 0; i < cblFuentesEstres.Items.Count; i++)
                        {
                            if (cblFuentesEstres.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                            {
                                cblFuentesEstres.Items[i].Selected = true;
                                bandera = 1;
                                break;
                            }                        
                        }
                        if (bandera == 0)
                        {
                            DataTable dtAntiguos = objConsulta.ConsultAntiguoPregunta(Convert.ToInt32(objConsultaOpcion.IdPreguntaRespuesta)).Tables[0];
                            DataRow drAntiguos = dtAntiguos.Rows[0];
                            cblFuentesEstres.Items.Add(new ListItem(drAntiguos["Nombre"].ToString(), drAntiguos["Id"].ToString()));
                            cblFuentesEstres.Items[cblFuentesEstres.Items.Count - 1].Selected = true;
                        }

                    }
                    if (objConsultaOpcion.IdPreguntaRespuestaPadre == 168)
                    {
                        for (int i = 0; i < cblRecomendacion.Items.Count; i++)
                        {
                            if (cblRecomendacion.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                            {
                                cblRecomendacion.Items[i].Selected = true;
                                break;
                            }
                        }
                        
                    }

                    if (objConsultaOpcion.IdPreguntaRespuestaPadre == 506)
                    {
                        for (int i = 0; i < cblQueFuma.Items.Count; i++)
                        {
                            if (cblQueFuma.Items[i].Value == objConsultaOpcion.IdPreguntaRespuesta.ToString())
                            {
                                cblQueFuma.Items[i].Selected = true;
                                break;
                            }
                        }

                    }
                }
            }

        }

        /// <summary>
        /// Método, carga la divisiones que la empresa tiene permitidos.
        /// Autor: Ricardo Silva
        /// Fecha: 06/07/2012
        /// </summary>
        public void CargarDivisionesEstamosContigo()
        {
            if (Session["Company"] != null)
            {
                EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones();
                int numIdEmpresa = int.Parse(Session["Company"].ToString());
                if (numIdEmpresa != 0)
                {
                    objEmpresaDivisiones.Empresa_id = numIdEmpresa;
                    objEmpresaDivisiones.GetEmpresaDivisiones();

                                        
                    if (objEmpresaDivisiones.DivHabitoFumar)
                    {
                        divHabitoFumar.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivConsumoAlcohol)
                    {
                        divConsumoAlcohol.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivWellness)
                    {
                        divWellness.Visible = true;
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

                    if (objEmpresaDivisiones.DivWellness)
                    {
                        divWellness.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivHabitoFumar)
                    {
                        divHabitoFumar.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivConsumoAlcohol)
                    {
                        divConsumoAlcohol.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivVacunacion)
                    {
                        divVacunacion.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivSedentarismo)
                    {
                        divSedentarismo.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivSaludOral)
                    {
                        divSaludOral.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivEstres)
                    {
                        divEstres.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivEmocional)
                    {
                        divEmocional.Visible = true;                        
                    }
                    if (objEmpresaDivisiones.DivAccidentalidad)
                    {
                        //divAlimentacionInadecuada.Visible = true;
                        divAccidentalidad.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivEstadoSalud)
                    {
                        divEstadoSalud.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivAntecedentesAusentismo)
                    {
                        divAntecedentesAusentismo.Visible = true;
                    }
                    if (objEmpresaDivisiones.DivRecomendaciones)
                    {
                        divRecomendaciones.Visible = true;

                        //Inicio MAHG 
                        //Proyecto: Wellness
                        //Fecha: 02-05-2010
                        //Se cargan las recomendaciones que se seleccionaron en SIAU para el asegurado

                        int intPais = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Pais"].ToString());

                        if (intPais == 1)
                        {
                            this.CargarRecomendaciones();
                        }                                               


                        //Fin MAHG
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
        public void UpdateConsultaCompletaEstiloVida(long p_idConsulta)
        {
            Consulta objConsulta = new Consulta();
            this.LoadObjectConsulta(objConsulta);
            objConsulta.IdConsulta = p_idConsulta;
            objConsulta.UpdateConsultaEstiloVida();
        }

        /// <summary>
        /// Método, inserta una nueva consulta
        /// </summary>
        /// <returns></returns>
        public long InsertConsultaEstiloVida(long p_idConsulta)
        {
            long idConsulta;
            Consulta objConsulta = new Consulta();
            objConsulta.IdConsulta = p_idConsulta;
            this.LoadObjectConsulta(objConsulta);
            idConsulta = objConsulta.InsertConsultaEstiloVida();
            return idConsulta;
        }

        /// <summary>
        /// Método, carga un objeto consulta con los datos de la forma
        /// </summary>
        public void LoadObjectConsulta(Consulta objConsulta)
        {
            string strSeparadorMiles = ConfigurationManager.AppSettings["SeparadorMiles"].ToString().Trim();
            //WELLNESS
            if (this.rblwellness.SelectedIndex > -1)
                 objConsulta.ProgramaWellness = Convert.ToInt32(this.rblwellness.SelectedValue);
            else
                 objConsulta.ProgramaWellness = -1;
            if (this.rblFirmaWellness.SelectedIndex > -1)
                objConsulta.FirmaWellness = Convert.ToInt32(this.rblFirmaWellness.SelectedValue);
            else
                objConsulta.FirmaWellness = -1;
            //HABITO DE FUMAR
            if (this.rblConductaCigarrillo.SelectedIndex > -1)
                objConsulta.ConductaCigarrillo = Convert.ToInt32(this.rblConductaCigarrillo.SelectedValue);
            else
                objConsulta.ConductaCigarrillo = -1;
            if (this.rblTiempoPrimerCigarrillo.SelectedIndex > -1)
                objConsulta.TiempoPrimerCigarrillo = Convert.ToInt32(this.rblTiempoPrimerCigarrillo.SelectedValue);
            else
                objConsulta.TiempoPrimerCigarrillo = -1;
            if (this.rblDificultadFumar.SelectedIndex > -1)
                objConsulta.DificultadFumar = Convert.ToInt32(this.rblDificultadFumar.SelectedValue);
            else
                objConsulta.DificultadFumar = -1;
            if (this.rblCigarrilloSuprimir.SelectedIndex > -1)
                objConsulta.CigarrilloSuprimir = Convert.ToInt32(this.rblCigarrilloSuprimir.SelectedValue);
            else
                objConsulta.CigarrilloSuprimir = -1;
            if (this.txtCigarrillosalDia.Text != string.Empty)
                objConsulta.CigarrillosalDia = int.Parse(txtCigarrillosalDia.Text.Replace(strSeparadorMiles, ""));
            if (this.rblFrecuenciaPrimerasHorasDia.SelectedIndex > -1)
                objConsulta.FrecuenciaPrimerasHorasDia = Convert.ToInt32(this.rblFrecuenciaPrimerasHorasDia.SelectedValue);
            else
                objConsulta.FrecuenciaPrimerasHorasDia = -1;
            if (this.rblFumaEnfermedad.SelectedIndex > -1)
                objConsulta.FumaEnfermedad = Convert.ToInt32(this.rblFumaEnfermedad.SelectedValue);
            else
                objConsulta.FumaEnfermedad = -1;
            if (this.rblCategoriaCigarrillos.SelectedIndex > -1)
                objConsulta.CategoriaCigarrillos = Convert.ToInt32(this.rblCategoriaCigarrillos.SelectedValue);
            else
                objConsulta.CategoriaCigarrillos = -1;
            if (this.rblAspiraHumo.SelectedIndex > -1)
                objConsulta.AspiraHumo = Convert.ToInt32(this.rblAspiraHumo.SelectedValue);
            else
                objConsulta.AspiraHumo = -1;
            if (this.txtAnosDejoFumar.Text != string.Empty)
                objConsulta.AnosDejoFumar = decimal.Parse(txtAnosDejoFumar.Text);
            if (this.txtPromedioDiarioX2Anos.Text != string.Empty)
                objConsulta.PromedioDiarioX2Anos = int.Parse(txtPromedioDiarioX2Anos.Text.Replace(strSeparadorMiles, ""));
            if (this.ddlConviveFumador.SelectedValue != "0" && this.ddlConviveFumador.SelectedValue != string.Empty)
                objConsulta.ConviveFumador = int.Parse(ddlConviveFumador.SelectedValue);
            

            //CONSUMO ALCOHOL
            if (this.ddlCopasSemana.SelectedValue != "0" && this.ddlCopasSemana.SelectedValue != string.Empty)
                objConsulta.CopasSemana = int.Parse(ddlCopasSemana.SelectedValue);
            if (this.rblCriticaAlcohol.SelectedIndex > -1)
                objConsulta.CriticaAlcohol = Convert.ToInt32(this.rblCriticaAlcohol.SelectedValue);
            else
                objConsulta.CriticaAlcohol = -1;
            if (this.rblArrepentidoAlcohol.SelectedIndex > -1)
                objConsulta.ArrepentidoAlcohol = Convert.ToInt32(this.rblArrepentidoAlcohol.SelectedValue);
            else
                objConsulta.ArrepentidoAlcohol = -1;
            if (this.rblLagunaAlcohol.SelectedIndex > -1)
                objConsulta.LagunaAlcohol = Convert.ToInt32(this.rblLagunaAlcohol.SelectedValue);
            else
                objConsulta.LagunaAlcohol = -1;
            if (this.rblMananaAlcohol.SelectedIndex > -1)
                objConsulta.MananaAlcohol = Convert.ToInt32(this.rblMananaAlcohol.SelectedValue);
            else
                objConsulta.MananaAlcohol = -1;
            // VACUNACION
            if (this.rblInfluenciaEstacional.SelectedIndex > -1)
                objConsulta.InfluenciaEstacional = Convert.ToInt32(this.rblInfluenciaEstacional.SelectedValue);
            else
                objConsulta.InfluenciaEstacional = -1;
            if (this.txtFechaInfluenzaEstacional.Text != string.Empty)
                objConsulta.FechaInfluenzaEstacional = Convert.ToDateTime(this.txtFechaInfluenzaEstacional.Text);
            else
                objConsulta.FechaInfluenzaEstacional = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteInfluenciaEstacional.Text != string.Empty)
            {
                objConsulta.MarcaLoteInfluenciaEstacional = Convert.ToString(this.txtMarcaLoteInfluenciaEstacional.Text);
            }
            if (this.rblInfluenciaH1N1.SelectedIndex > -1)
                objConsulta.InfluenciaH1N1 = Convert.ToInt32(this.rblInfluenciaH1N1.SelectedValue);
            else
                objConsulta.InfluenciaH1N1 = -1;
            if (this.txtFechaInfluenciaH1N1.Text != string.Empty)
                objConsulta.FechaInfluenciaH1N1 = Convert.ToDateTime(this.txtFechaInfluenciaH1N1.Text);
            else
                objConsulta.FechaInfluenciaH1N1 = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteInfluenciaH1N1.Text != string.Empty)
            {
                objConsulta.MarcaLoteInfluenciaH1N1 = Convert.ToString(this.txtMarcaLoteInfluenciaH1N1.Text);
            }
            if (this.rblFiebreAmarilla.SelectedIndex > -1)
                objConsulta.FiebreAmarilla = Convert.ToInt32(this.rblFiebreAmarilla.SelectedValue);
            else
                objConsulta.FiebreAmarilla = -1;
            if (this.txtFechaFiebreAmarilla.Text != string.Empty)
                objConsulta.FechaFiebreAmarilla = Convert.ToDateTime(this.txtFechaFiebreAmarilla.Text);
            else
                objConsulta.FechaFiebreAmarilla = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteFiebreAmarilla.Text != string.Empty)
            {
                objConsulta.MarcaLoteFiebreAmarilla = Convert.ToString(this.txtMarcaLoteFiebreAmarilla.Text);
            }
            if (this.rblHepatitisViral.SelectedIndex > -1)
                objConsulta.HepatitisViral = Convert.ToInt32(this.rblHepatitisViral.SelectedValue);
            else
                objConsulta.HepatitisViral = -1;
            if (this.txtFechaHepatitisViral.Text != string.Empty)
                objConsulta.FechaHepatitisViral = Convert.ToDateTime(this.txtFechaHepatitisViral.Text);
            else
                objConsulta.FechaHepatitisViral = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteHepatitisViral.Text != string.Empty)
            {
                objConsulta.MarcaLoteHepatitisViral = Convert.ToString(this.txtMarcaLoteHepatitisViral.Text);
            }
            if (this.rblToxoideTetanico.SelectedIndex > -1)
                objConsulta.ToxoideTetanico = Convert.ToInt32(this.rblToxoideTetanico.SelectedValue);
            else
                objConsulta.ToxoideTetanico = -1;
            if (this.txtFechaToxoideTetanico.Text != string.Empty)
                objConsulta.FechaToxoideTetanico = Convert.ToDateTime(this.txtFechaToxoideTetanico.Text);
            else
                objConsulta.FechaToxoideTetanico = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteToxoideTetanico.Text != string.Empty)
            {
                objConsulta.MarcaLoteToxoideTetanico = Convert.ToString(this.txtMarcaLoteToxoideTetanico.Text);
            }

            if (this.rblHepatitisViralB.SelectedIndex > -1)
                objConsulta.HepatitisViralB = Convert.ToInt32(this.rblHepatitisViralB.SelectedValue);
            else
                objConsulta.HepatitisViralB = -1;
            if (this.txtFechaHepatitisViralB.Text != string.Empty)
                objConsulta.FechaHepatitisViralB = Convert.ToDateTime(this.txtFechaHepatitisViralB.Text);
            else
                objConsulta.FechaHepatitisViralB = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteHepatitisViralB.Text != string.Empty)
            {
                objConsulta.MarcaLoteHepatitisViralB = Convert.ToString(this.txtMarcaLoteHepatitisViralB.Text);
            }

            if (this.rblMeningococo.SelectedIndex > -1)
                objConsulta.Meningococo = Convert.ToInt32(this.rblMeningococo.SelectedValue);
            else
                objConsulta.Meningococo = -1;
            if (this.txtFechaMeningococo.Text != string.Empty)
                objConsulta.FechaMeningococo = Convert.ToDateTime(this.txtFechaMeningococo.Text);
            else
                objConsulta.FechaMeningococo = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteMeningococo.Text != string.Empty)
            {
                objConsulta.MarcaLoteMeningococo = Convert.ToString(this.txtMarcaLoteMeningococo.Text);
            }

            if (this.rblFiebreTifoidea.SelectedIndex > -1)
                objConsulta.FiebreTifoidea = Convert.ToInt32(this.rblFiebreTifoidea.SelectedValue);
            else
                objConsulta.FiebreTifoidea = -1;
            if (this.txtFechaFiebreTifoidea.Text != string.Empty)
                objConsulta.FechaFiebreTifoidea = Convert.ToDateTime(this.txtFechaFiebreTifoidea.Text);
            else
                objConsulta.FechaFiebreTifoidea = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteFiebreTifoidea.Text != string.Empty)
            {
                objConsulta.MarcaLoteFiebreTifoidea = Convert.ToString(this.txtMarcaLoteFiebreTifoidea.Text);
            }

            if (this.rblVPH.SelectedIndex > -1)
                objConsulta.FiebreVPH = Convert.ToInt32(this.rblVPH.SelectedValue);
            else
                objConsulta.FiebreVPH = -1;
            if (this.txtFechaVPH.Text != string.Empty)
                objConsulta.FechaVPH = Convert.ToDateTime(this.txtFechaVPH.Text);
            else
                objConsulta.FechaVPH = new DateTime(1900, 1, 1);
            if (this.txtMarcaLoteVPH.Text != string.Empty)
            {
                objConsulta.MarcaLoteVPH = Convert.ToString(this.txtMarcaLoteVPH.Text);
            }

            // SEDENTARISMO
            if (this.rblPracticaDeporte.SelectedIndex > -1)
                objConsulta.PracticaDeporte = Convert.ToInt32(this.rblPracticaDeporte.SelectedValue);
            else
                objConsulta.PracticaDeporte = -1;
            if (this.ddlPracticaDeporteSemana.SelectedValue != "0" && this.ddlPracticaDeporteSemana.SelectedValue != string.Empty)
                objConsulta.PracticaDeporteSemana = int.Parse(ddlPracticaDeporteSemana.SelectedValue);
            if (this.ddlPromedioTiempoMinutos.SelectedValue != "0" && this.ddlPromedioTiempoMinutos.SelectedValue != string.Empty)
                objConsulta.PromedioTiempoMinutos = int.Parse(ddlPromedioTiempoMinutos.SelectedValue);
            if (this.rblTipoActividadFisica.SelectedIndex > -1)
                objConsulta.TipoActividadFisica = Convert.ToInt32(this.rblTipoActividadFisica.SelectedValue);
            else
                objConsulta.TipoActividadFisica = -1;
            if (this.ddlHorasDiariasTV.SelectedValue != "0" && this.ddlHorasDiariasTV.SelectedValue != string.Empty)
                objConsulta.HorasDiariasTV = int.Parse(ddlHorasDiariasTV.SelectedValue);
            // SALUD ORAL
            if (this.rblConsultaOdontologica.SelectedIndex > -1)
                objConsulta.ConsultaOdontologica = Convert.ToInt32(this.rblConsultaOdontologica.SelectedValue);
            else
                objConsulta.ConsultaOdontologica = -1;
            if (this.ddlLavaDientes.SelectedValue != "0" && this.ddlLavaDientes.SelectedValue != string.Empty)
                objConsulta.LavaDientes = int.Parse(ddlLavaDientes.SelectedValue);
            if (this.rblSedaDental.SelectedIndex > -1)
                objConsulta.SedaDental = Convert.ToInt32(this.rblSedaDental.SelectedValue);
            else
                objConsulta.SedaDental = -1;

            if (this.rblrutinaUltimoMes.SelectedIndex > -1)
                objConsulta.rutinaEjercicioUltimoMes = Convert.ToInt32(this.rblrutinaUltimoMes.SelectedValue);
            else
                objConsulta.rutinaEjercicioUltimoMes = -1;
            if (this.ddlNoRutinaEjercicio.SelectedValue != "0" && this.ddlNoRutinaEjercicio.SelectedValue != string.Empty)
                objConsulta.NoRutinaEjercicio = int.Parse(ddlNoRutinaEjercicio.SelectedValue);
            if (this.txtOtroMotivo.Text != string.Empty)
                objConsulta.OtroMotivo = this.txtOtroMotivo.Text;

            // ESTRES
            if (this.rblSentidoDecaido.SelectedIndex > -1)
                objConsulta.SentidoDecaido = Convert.ToInt32(this.rblSentidoDecaido.SelectedValue);
            else
                objConsulta.SentidoDecaido = -1;

            /// <summary>
            /// Descripción: Permite Guardar los datos del campo sentido cansado
            /// Autor: Ricardo Silva
            /// Fecha: 11/07/2012
            if (this.rblSentidoEstresado.SelectedIndex > -1)
                objConsulta.SentidoEstresado = Convert.ToInt32(this.rblSentidoEstresado.SelectedValue);
            else
                objConsulta.SentidoEstresado = -1;

            if (this.rblInteresPlacer.SelectedIndex > -1)
                objConsulta.InteresPlacer = Convert.ToInt32(this.rblInteresPlacer.SelectedValue);
            else
                objConsulta.InteresPlacer = -1;
            if (this.ddlNivelEstres.SelectedValue != "0" && this.ddlNivelEstres.SelectedValue != string.Empty)
                objConsulta.NivelEstres = int.Parse(ddlNivelEstres.SelectedValue);
            if (this.rblControlarEstres.SelectedIndex > -1)
                objConsulta.ControlarEstres = Convert.ToInt32(this.rblControlarEstres.SelectedValue);
            else
                objConsulta.ControlarEstres = -1;
            if (this.ddlPocoInteres.SelectedValue != "0" && this.ddlPocoInteres.SelectedValue != string.Empty)
                objConsulta.PocoInteres = int.Parse(ddlPocoInteres.SelectedValue);
            if (this.ddlSinEsperanza.SelectedValue != "0" && this.ddlSinEsperanza.SelectedValue != string.Empty)
                objConsulta.SinEsperanza = int.Parse(ddlSinEsperanza.SelectedValue);

            // EMOCIONAL

            if (this.rblCalificacionSueno.SelectedIndex > -1)
                objConsulta.CalificacionSueno = Convert.ToInt32(this.rblCalificacionSueno.SelectedValue);
            else
                objConsulta.CalificacionSueno = -1;
            if (this.ddlEstadoLevantarse.SelectedValue != "0" && this.ddlEstadoLevantarse.SelectedValue != string.Empty)
                objConsulta.EstadoLevantarse = int.Parse(ddlEstadoLevantarse.SelectedValue);
            /// <summary>
            /// Descripción: Permite cargar los datos del campo dormido suficiente
            /// Autor: Ricardo Silva
            /// Fecha: 11/07/2012
            if (this.ddlDormidoSuficiente.SelectedValue != "0" && this.ddlDormidoSuficiente.SelectedValue != string.Empty)
                objConsulta.DormidoSuficiente = int.Parse(ddlDormidoSuficiente.SelectedValue);
            if (this.ddlHorasDuermeRegular.SelectedValue != "0" && this.ddlHorasDuermeRegular.SelectedValue != string.Empty)
                objConsulta.HorasDuermeRegular = int.Parse(ddlHorasDuermeRegular.SelectedValue);
            if (this.ddlEstadoAnimoEmocional.SelectedValue != "0" && this.ddlEstadoAnimoEmocional.SelectedValue != string.Empty)
                objConsulta.EstadoAnimoEmocional = int.Parse(ddlEstadoAnimoEmocional.SelectedValue);

            /// <summary>
            /// Descripción: Permite guardar en el objeto los resultados de la seccion ALIMENTACION INADECUADA
            /// Autor: Ricardo Silva
            /// Fecha: 13/07/2012
            /// </summary>
            //ALIMENTACION INADECUADA
            if (this.ddlPorcionesFrutas.SelectedValue != "0" && this.ddlPorcionesFrutas.SelectedValue != string.Empty)
                objConsulta.PorcionesFrutas = int.Parse(ddlPorcionesFrutas.SelectedValue);
            if (this.ddlPorcionesVegetales.SelectedValue != "0" && this.ddlPorcionesVegetales.SelectedValue != string.Empty)
                objConsulta.PorcionesVegetales = int.Parse(ddlPorcionesVegetales.SelectedValue);
            if (this.ddlFrecuenciaCarne.SelectedValue != "0" && this.ddlFrecuenciaCarne.SelectedValue != string.Empty)
                objConsulta.FrecuenciaCarne = int.Parse(ddlFrecuenciaCarne.SelectedValue);
            if (this.ddlPorcionesFrutas.SelectedValue != "0" && this.ddlPorcionesFrutas.SelectedValue != string.Empty)
                objConsulta.FrecuenciaSano = int.Parse(ddlFrecuenciaSano.SelectedValue);
            if (this.ddlFrecuenciaSano.SelectedValue != "0" && this.ddlFrecuenciaSano.SelectedValue != string.Empty)
                objConsulta.FrecuenciaGranos = int.Parse(ddlFrecuenciaGranos.SelectedValue);
            if (this.ddlFrecuenciaGranos.SelectedValue != "0" && this.ddlFrecuenciaGranos.SelectedValue != string.Empty)
                objConsulta.PorcionesFrutas = int.Parse(ddlPorcionesFrutas.SelectedValue);
            if (this.ddlFrecuenciaAzucar.SelectedValue != "0" && this.ddlFrecuenciaAzucar.SelectedValue != string.Empty)
                objConsulta.FrecuenciaAzucar = int.Parse(ddlFrecuenciaAzucar.SelectedValue);
            if (this.ddlFrecuenciaSodio.SelectedValue != "0" && this.ddlFrecuenciaSodio.SelectedValue != string.Empty)
                objConsulta.FrecuenciaSodio = int.Parse(ddlFrecuenciaSodio.SelectedValue);

            //COMPORTAMIENTOS DE RIESGO Y ACCIDENTALIDAD
            if (this.ddlCinturonSeguridad.SelectedValue != "0" && this.ddlCinturonSeguridad.SelectedValue != string.Empty)
                objConsulta.CinturonSeguridad = int.Parse(ddlCinturonSeguridad.SelectedValue);
            if (this.ddlCocheCelular.SelectedValue != "0" && this.ddlCocheCelular.SelectedValue != string.Empty)
                objConsulta.CocheCelular = int.Parse(ddlCocheCelular.SelectedValue);
            if (this.ddlLimiteVelocidad.SelectedValue != "0" && this.ddlLimiteVelocidad.SelectedValue != string.Empty)
                objConsulta.LimiteVelocidad = int.Parse(ddlLimiteVelocidad.SelectedValue);
            if (this.ddlConductorBebido.SelectedValue != "0" && this.ddlConductorBebido.SelectedValue != string.Empty)
                objConsulta.ConductorBebido = int.Parse(ddlConductorBebido.SelectedValue);
            if (this.ddlCasco.SelectedValue != "0" && this.ddlCasco.SelectedValue != string.Empty)
                objConsulta.Casco = int.Parse(ddlCasco.SelectedValue);
            if (this.ddlFiltroSolar.SelectedValue != "0" && this.ddlFiltroSolar.SelectedValue != string.Empty)
                objConsulta.FiltroSolar = int.Parse(ddlFiltroSolar.SelectedValue);
            if (this.rblSeguridadDomestica.SelectedIndex > -1)
                objConsulta.SeguridadDomestica = Convert.ToInt32(this.rblSeguridadDomestica.SelectedValue);
            else
                objConsulta.SeguridadDomestica = -1;
            if (this.ddlTrasmisionSexual.SelectedValue != "0" && this.ddlTrasmisionSexual.SelectedValue != string.Empty)
                objConsulta.TrasmisionSexual = int.Parse(ddlTrasmisionSexual.SelectedValue);
            
            //PERCEPCIÓN DEL ESTADO DE SALUD
            if (this.ddlEstadoSalud.SelectedValue != "0" && this.ddlEstadoSalud.SelectedValue != string.Empty)
                objConsulta.EstadoSalud = int.Parse(ddlEstadoSalud.SelectedValue);
            if (this.ddlHabitosVida.SelectedValue != "0" && this.ddlHabitosVida.SelectedValue != string.Empty)
                objConsulta.HabitosVida = int.Parse(ddlHabitosVida.SelectedValue);
            
            //ANTECEDENTES AUSENTISMO
            if (this.rblIncapacitado.SelectedIndex > -1)
                objConsulta.Incapacitado = Convert.ToInt32(this.rblIncapacitado.SelectedValue);
            else
                objConsulta.Incapacitado = -1;
            this.WC_AdicionarDiagnosticoConsultaCIE1.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoIncapacidad);
            if (this.txtDiasIncapacidad.Text != string.Empty)
                objConsulta.DiasIncapacidad = int.Parse(txtDiasIncapacidad.Text.Replace(strSeparadorMiles, ""));

            this.WC_AdicionarDiagnosticoConsultaCIE2.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion1);
            if (this.txtFechaHospitalizacion1.Text != string.Empty)
                objConsulta.FechaHospitalizacion1 = Convert.ToDateTime(this.txtFechaHospitalizacion1.Text);
            else
                objConsulta.FechaHospitalizacion1 = new DateTime(1900, 1, 1);
            if (this.txtDiasHospitalizacion1.Text != string.Empty)
                objConsulta.DiasHospitalizacion1 = int.Parse(txtDiasHospitalizacion1.Text.Replace(strSeparadorMiles, ""));
            this.WC_AdicionarDiagnosticoConsultaCIE3.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion2);
            if (this.txtFechaHospitalizacion2.Text != string.Empty)
                objConsulta.FechaHospitalizacion2 = Convert.ToDateTime(this.txtFechaHospitalizacion2.Text);
            else
                objConsulta.FechaHospitalizacion2 = new DateTime(1900, 1, 1);
            if (this.txtDiasHospitalizacion2.Text != string.Empty)
                objConsulta.DiasHospitalizacion2 = int.Parse(txtDiasHospitalizacion2.Text.Replace(strSeparadorMiles, ""));
            this.WC_AdicionarDiagnosticoConsultaCIE4.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion3);
            if (this.txtFechaHospitalizacion3.Text != string.Empty)
                objConsulta.FechaHospitalizacion3 = Convert.ToDateTime(this.txtFechaHospitalizacion3.Text);
            else
                objConsulta.FechaHospitalizacion3 = new DateTime(1900, 1, 1);
            if (this.txtDiasHospitalizacion3.Text != string.Empty)
                objConsulta.DiasHospitalizacion3 = int.Parse(txtDiasHospitalizacion3.Text.Replace(strSeparadorMiles, ""));
            this.WC_AdicionarDiagnosticoConsultaCIE5.LoadDiagnosticos(objConsulta, Mercer.Medicines.Logic.Consulta.EnumTiposDiagnosticos.IdDiagnosticoHospitalizacion4);
            if (this.txtFechaHospitalizacion4.Text != string.Empty)
                objConsulta.FechaHospitalizacion4 = Convert.ToDateTime(this.txtFechaHospitalizacion4.Text);
            else
                objConsulta.FechaHospitalizacion4 = new DateTime(1900, 1, 1);
            if (this.txtDiasHospitalizacion4.Text != string.Empty)
                objConsulta.DiasHospitalizacion4 = int.Parse(txtDiasHospitalizacion4.Text.Replace(strSeparadorMiles, ""));
            //OPCIONES LLENA ARREGLO DE OPCINES POR CADA CONTROL
            ArrayList arrOpciones = new ArrayList();

            for (int i = 0; i < this.cblFuentesEstres.Items.Count; i++)
            {
                if (cblFuentesEstres.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 50; 
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblFuentesEstres.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                }
            }
            for (int i = 0; i < this.cblRecomendacion.Items.Count; i++)
            {
                if (cblRecomendacion.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 168;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblRecomendacion.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                }
            }

            for (int i = 0; i < this.cblQueFuma.Items.Count; i++)
            {
                if (cblQueFuma.Items[i].Selected)
                {
                    ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                    objConsultaOpcion.IdPreguntaRespuestaPadre = 506;
                    objConsultaOpcion.IdPreguntaRespuesta = int.Parse(cblQueFuma.Items[i].Value);
                    arrOpciones.Add(objConsultaOpcion);
                }
            }
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
                EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones(int.Parse(Session["Company"].ToString()));

                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaEstiloVida())
                    {
                        long idConsulta = this.InsertConsultaEstiloVida(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaEstiloVida, idConsulta, "Ingreso consulta estilo de vida" + idConsulta);

                        if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                        {
                            Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]));
                        }

                        if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]);
                        else
                        {
                            if (_esNutriologo)
                                Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"], false);
                            else
                            {
                                if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"]), false);
                                else
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"], false);
                            }
                        }
                        
                    }
                    else
                    {
                        this.UpdateConsultaCompletaEstiloVida(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaEstiloVida, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  estilo de vida" + Convert.ToInt32(Request.QueryString["IdConsulta"]));

                        if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                        {
                            if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                                Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]), false);
                            else
                                Response.Redirect("AE_registroestamoscontigo.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]) + "&Editar=" + Request.QueryString["Editar"]);
                        }
                        else
                        {

                            if (!objEmpresaDivisiones.ConsultExisteEmpresaDivisionesNutricion())
                            {
                                if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"], false);
                                else
                                    Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"], false);
                            }
                            else
                            {
                                if (Request.QueryString["IdSolicitud"] != null && Request.QueryString["IdSolicitud"] != string.Empty && Request.QueryString["IdSolicitud"] != "0")
                                {
                                    if (_esNutriologo)
                                        Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] + "&Editar=" + Request.QueryString["Editar"], false);
                                    else
                                    {
                                        if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"]), false);
                                        else
                                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"], false);
                                    }
                                }
                                else
                                {
                                    if (_esNutriologo)
                                        Response.Redirect("AE_registronutricion.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&Editar=" + Request.QueryString["Editar"], false);
                                    else
                                    {
                                        if (Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 11 || Convert.ToInt32(Request.QueryString["TipoConsulta"]) == 12)
                                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"] + "&TipoConsulta=" + Convert.ToInt32(Request.QueryString["TipoConsulta"] ), false);
                                        else
                                            Response.Redirect("AE_solicitudorden.aspx?IdConsulta=" + Convert.ToInt64(Request.QueryString["IdConsulta"]) + "&employee_id=" + Request.QueryString["employee_id"], false);
                                    }
                                }
                            }
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
            //Consulta objConsulta = new Consulta();
            //objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"]);
            //objConsulta.GetConsulta();
            //objConsulta.GetConsultaEstiloVida();
            //if (objConsulta.ExisteConsultaEstiloVida() && (Request.QueryString["editar"] == null || Request.QueryString["editar"] == string.Empty))
            //    Response.Redirect("LIS_consulta.aspx");               
            //else
            //    Response.Redirect("LIS_empleado.aspx");
            Response.Redirect("LIS_consulta.aspx");//Marsh - JFEE - 2014/11/26 - Correcciones generales, al cancelar una consulta siempre debe direccionar a la página de consultas
        }

        /// <summary>bool
        /// Evento, abre la ventana para ver historial de toda la información de la hisotia clínica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbVerHistoria_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 1);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de la hisotia clínica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkVerHistoria_Click(object sender, System.EventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicohistoriaclinica.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 1);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de las consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorial_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 1);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 1);
        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de toda la información de las consultas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerHistorico_Click(object sender, System.EventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=todos", 850, 750, 2);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=todos", 850, 750, 2);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de antecedentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialAntecedentes_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=antecedentes", 850, 750, 3);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=antecendentes", 850, 750, 3);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de revisión por sistemas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialRevision_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=revision", 850, 750, 4);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=revision", 850, 750, 4);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de exámen físico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialExamen_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=examen", 850, 750, 5);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=examen", 850, 750, 5);

        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de diagnósticos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialDiagnosticos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=diagnosticos", 850, 750, 6);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=diagnosticos", 850, 750, 6);


        }

        /// <summary>
        /// Evento, abre la ventana para ver historial de habitos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbHistorialHabitos_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s&Modulo=habitos", 850, 750, 6);
            else
                this.OpenWindow("LIS_historicoconsultas.aspx?beneficiario_id=" + ViewState["beneficiario_id"].ToString() + "&Modulo=habitos", 850, 750, 6);


        }

        /// <summary>
        /// Evento, llama la ventana para consultar histórico de órdenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imbVerHistorialOrdenes_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + ViewState["beneficiario_id"], 850, 750);

        }

        /// <summary>
        /// Evento, llama la ventana para consultar histórico de órdenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerHistorialOrdenes_Click(object sender, System.EventArgs e)
        {
            if (ViewState["beneficiario_id"] == null || ViewState["beneficiario_id"].ToString() == "0")
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + Request.QueryString["employee_id"] + "&empleado=s", 850, 750);
            else
                this.OpenWindow("LIS_historicosolicitudservicios.aspx?beneficiario_id=" + ViewState["beneficiario_id"], 850, 750);

        }
        /// <summary>
        /// Evento que habilita las divisiones según conducto del cigarrillo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblConductaCigarrillo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MostrarDivsCigarrillo();
        }
        /// <summary>
        /// Evento, para volver a la página anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnterior_Click1(object sender, EventArgs e)
        {
            if (Request.QueryString["IdConsulta"] != null)
            {
                Consulta objConsulta = new Consulta();
                objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                if (!objConsulta.ExisteConsultaEstiloVida())
                {
                    long idConsulta = this.InsertConsultaEstiloVida(objConsulta.IdConsulta);
                    this.RegisterLog(Log.EnumActionsLog.IngresarConsultaEstiloVida, idConsulta, "Ingreso consulta estilo de vida" + idConsulta);
                }
                else
                {
                    this.UpdateConsultaCompletaEstiloVida(objConsulta.IdConsulta);
                    this.RegisterLog(Log.EnumActionsLog.ModificarConsultaEstiloVida, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  estilo de vida" + Convert.ToInt32(Request.QueryString["IdConsulta"] ));

                }
            }
            Response.Redirect("AE_registroconsulta.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );

        }
        //uncion que muestra divs y inicia controles contenidos en ellos
        /// </summary>
        public void MostrarDivsCigarrillo()
        {
            if (this.rblConductaCigarrillo.SelectedValue == "284")
            {
                this.divFumaActualmente.Visible = false;
                this.divFumaba.Visible = false;
                this.rblTiempoPrimerCigarrillo.SelectedIndex = -1;
                this.rblDificultadFumar.SelectedIndex = -1;
                this.rblCigarrilloSuprimir.SelectedIndex = -1;
                this.txtCigarrillosalDia.Text = "";
                this.rblFrecuenciaPrimerasHorasDia.SelectedIndex = -1;
                this.rblFumaEnfermedad.SelectedIndex = -1;
                this.rblCategoriaCigarrillos.SelectedIndex = -1;
                this.rblAspiraHumo.SelectedIndex = -1;
                this.txtAnosDejoFumar.Text = "";
                this.txtPromedioDiarioX2Anos.Text = "";
               
            


            }
            if (this.rblConductaCigarrillo.SelectedValue == "285")
            {
                this.divFumaActualmente.Visible = false;
                this.divFumaba.Visible = true;
                this.rblTiempoPrimerCigarrillo.SelectedIndex = -1;
                this.rblDificultadFumar.SelectedIndex = -1;
                this.rblCigarrilloSuprimir.SelectedIndex = -1;
                this.txtCigarrillosalDia.Text = "";
                this.rblFrecuenciaPrimerasHorasDia.SelectedIndex = -1;
                this.rblFumaEnfermedad.SelectedIndex = -1;
                this.rblCategoriaCigarrillos.SelectedIndex = -1;
                this.rblAspiraHumo.SelectedIndex = -1;
            }
            if (this.rblConductaCigarrillo.SelectedValue == "286")
            {
                this.divFumaActualmente.Visible = true;
                this.divFumaba.Visible = false;
                this.txtAnosDejoFumar.Text = "";
                this.txtPromedioDiarioX2Anos.Text = "";
            }

            this.ResizePage(this.rblConductaCigarrillo.ClientID);
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
                // Inicio - Emilio Bueno 11/Dic/2012
                // Verifica un conjunto de campos obligatorios
                // para que se pueda realizar el guardado automático
                //rfvwellness.Validate();
                //rfvFirmaWellness.Validate();
                //if (!(rfvwellness.IsValid &&
                //    rfvFirmaWellness.IsValid))
                //{
                //    this.DisplayMessage("Debe ingresar los campos requeridos de la sección Wellness antes de intentar guardar temporalmente!");
                //    return;
                //}
                // Fin - Emilio Bueno 11/Dic/2012

                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaEstiloVida())
                    {
                        long idConsulta = this.InsertConsultaEstiloVida(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaEstiloVida, idConsulta, "Ingreso consulta estilo de vida" + idConsulta);
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );
                    }
                    else
                    {                       
                        this.UpdateConsultaCompletaEstiloVida(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaEstiloVida, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  estilo de vida" + Convert.ToInt32(Request.QueryString["IdConsulta"]));
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );//MAHG 19/07/2010 Se agrega el parámetro Finalizada
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
                // Inicio - Emilio Bueno 11/Dic/2012
                // Verifica un conjunto de campos obligatorios
                // para que se pueda realizar el guardado automático
                //rfvwellness.Validate();
                //rfvFirmaWellness.Validate();
                //if (!(rfvwellness.IsValid &&
                //    rfvFirmaWellness.IsValid))
                //{
                //    this.DisplayMessage("Debe ingresar los campos requeridos de la sección Wellness antes de intentar guardar temporalmente!");
                //    return;
                //}
                // Fin - Emilio Bueno 11/Dic/2012

                if (Request.QueryString["IdConsulta"] != null)
                {
                    Consulta objConsulta = new Consulta();
                    objConsulta.IdConsulta = Convert.ToInt64(Request.QueryString["IdConsulta"].ToString());
                    if (!objConsulta.ExisteConsultaEstiloVida())
                    {
                        long idConsulta = this.InsertConsultaEstiloVida(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.IngresarConsultaEstiloVida, idConsulta, "Ingreso consulta estilo de vida" + idConsulta);
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + idConsulta + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );
                    }
                    else
                    {
                        this.UpdateConsultaCompletaEstiloVida(objConsulta.IdConsulta);
                        this.RegisterLog(Log.EnumActionsLog.ModificarConsultaEstiloVida, Convert.ToInt64(Request.QueryString["IdConsulta"]), "Modificación consulta  estilo de vida" + Convert.ToInt32(Request.QueryString["IdConsulta"] ));
                        Response.Redirect("AE_registroestilovida.aspx?IdConsulta=" + Request.QueryString["IdConsulta"] + "&employee_id=" + Request.QueryString["employee_id"] + "&editar=1" + "&IdSolicitud=" + Request.QueryString["IdSolicitud"] );
                        
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        protected void rblrutinaUltimoMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblrutinaUltimoMes.SelectedIndex == 0)
            {
                this.divSedentarismo.Visible = true;
                this.DivNoRutinaEjercicio.Visible = false;
            }
            if (this.rblrutinaUltimoMes.SelectedIndex == 1)
            {
                this.divSedentarismo.Visible = false;
                this.DivNoRutinaEjercicio.Visible = true;
            }
            
        }

        #endregion        


    }
}
