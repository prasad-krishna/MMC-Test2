﻿using System;
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
    /// Lista el histórico de consultas recibidas del empleado
    /// </summary>
    public partial class LIS_historicoconsultas : PB_PaginaBase
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
                    //Inicio MAHG 12/01/10
                    //Se agrega el atributo readonly 
                    //txtSolicitante.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtTipoConsulta.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtTipoEnfermedad.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtCitaControl.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalMedicos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalQuirurgicos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalGineco.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtMenarquia.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtFechaUltimaMestruacion.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtPartos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtCesareas.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtAbortos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtVivos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalTransfusionales.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalToxicoAlergicos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFarmacologicos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalOtrosAntecedentes.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFamiliares.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtTabaquismo.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtFrecuenciaTabaquismo.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtActividadDeportiva.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtAlcohol.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtFrecuenciaAlcohol.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalCabeza.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalCuello.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalTorax.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalAbdomen.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalOtros.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtPeso.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtTalla.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtIMC.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtTension.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtFrecuenciaCardiaca.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtFrecuenciaRespiratoria.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtTemperatura.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtPerimetroAbdominal.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisGeneral.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisPielFanelas.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisCabeza.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisConjuntiva.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisReflejo.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisPupilas.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisOidos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisOtoscopia.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisRinoscopia.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisBocaFaringe.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisAmigdalas.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisCuello.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisTiroides.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisAdenopatias.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisTorax.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisRuidosCardiacos.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisRuidosRespiratorios.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisAbdomen.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisPalpacionAbdomen.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisGenitales.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisHernias.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisColumna.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisExtremidadesSuperiores.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisExtremidadesInferiores.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisVarices.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisNeurologico.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtNormalFisOtros.Attributes.Add("ReadOnly", "ReadOnly");

                    //Fin MAHG 12/01/10

                    this.FindConsulta();
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
        /// Método, Carga los controles de listados
        /// </summary>
        public void LoadControls()
        {
            //Inicio PETF 14/01/10
            //Se agrega el atributo readonly 
            txtFechaFin.Attributes.Add("ReadOnly", "ReadOnly");
            txtFechaInicio.Attributes.Add("ReadOnly", "ReadOnly");
            
            foreach (DataListItem item in dtlConsultas.Items)
            {
                ((TextBox)item.FindControl("txtSolicitante")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTipoConsulta")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTipoEnfermedad")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCitaControl")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalMedicos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalQuirurgicos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalGineco")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMenarquia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaUltimaMestruacion")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPartos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCesareas")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAbortos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtVivos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalTransfusionales")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalToxicoAlergicos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFarmacologicos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalOtrosAntecedentes")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFamiliares")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTabaquismo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFrecuenciaTabaquismo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtActividadDeportiva")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAlcohol")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFrecuenciaAlcohol")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalCabeza")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalCuello")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalTorax")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalAbdomen")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalOtros")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPeso")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTalla")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIMC")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTension")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFrecuenciaCardiaca")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFrecuenciaRespiratoria")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTemperatura")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPerimetroAbdominal")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisGeneral")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisPielFanelas")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisCabeza")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisConjuntiva")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisReflejo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisPupilas")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisOidos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisOtoscopia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisRinoscopia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisBocaFaringe")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisAmigdalas")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisCuello")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisTiroides")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisAdenopatias")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisTorax")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisRuidosCardiacos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisRuidosRespiratorios")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisAbdomen")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisPalpacionAbdomen")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisGenitales")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisHernias")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisColumna")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisExtremidadesSuperiores")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisExtremidadesInferiores")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisVarices")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisNeurologico")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNormalFisOtros")).Attributes.Add("ReadOnly", "ReadOnly");
                //John Portela 16/03/2010 PRUEBAS BIOMÉTRICAS
                ((TextBox)item.FindControl("txtColesterolTotal")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtColesterolHDL")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtColesterolHDLmmol")).Attributes.Add("ReadOnly", "ReadOnly"); 
                ((TextBox)item.FindControl("txtColesterolLDL")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTrigliceridos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIndiceAterogenico")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAntigenoProstata")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtGlucemiaAyunas")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHemoglobinaGlucosilada")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHomocisteina")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPresenciaMicroorganismos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaPapanicolauMicro")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtObservacionesPresenciaMicro")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtResultadoMorfologico")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAnormalCelulasEpi")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCelulasEscamosas")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMamografia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMamografiaObservaciones")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAudiometria")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAudiometriaObservaciones")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtRayosX")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtRayosXObservaciones")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMiopia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMiopiaValor")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMiopiaValorOI")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMiopiaObservaciones")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAstigmatismo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAstigmatismoValor")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAstigmatismoValorOI")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAstigmatismoObservaciones")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHipermetropia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHipermetropiaValor")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHipermetropiaValorOI")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHipermetropiaObservaciones")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPresbicia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPresbiciaValor")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPresbiciaValorOI")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPresbiciaObservaciones")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtOtrosExamenVisual")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtOtrosDiagnostico")).Attributes.Add("ReadOnly", "ReadOnly");
                //John Portela 16/03/2010 ESTILO DE VIDA
                ((TextBox)item.FindControl("txtWellness")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFirmaWellness")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtConductaCigarrillo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTiempoPrimerCigarrillo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDificultadFumar")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCigarrilloSuprimir")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCigarrillosalDia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFrecuenciaPrimerasHorasDia")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFumaEnfermedad")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCategoriaCigarrillos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAspiraHumo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAnosDejoFumar")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPromedioDiarioX2Anos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCopasSemana")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCriticaAlcohol")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtArrepentidoAlcohol")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtLagunaAlcohol")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMananaAlcohol")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtInfluenciaEstacional")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtInfluenciaH1N1")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFiebreAmarilla")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHepatitisViral")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtToxoideTetanico")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaInfluenciaEstacional")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaInfluenciaH1N1")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaFiebreAmarilla")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaHepatitisViral")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaToxoideTetanico")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPracticaDeporte")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPracticaDeporteSemana")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPromedioTiempoMinutos")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTipoActividadFisica")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHorasDiariasTV")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtConsultaOdontologica")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtLavaDientes")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtSedaDental")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtSentidoDecaido")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtInteresPlacer")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtNivelEstres")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtControlarEstres")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCalificacionSueno")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtEstadoLevantarse")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHorasDuermeRegular")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtEstadoAnimoEmocional")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCinturonSeguridad")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCocheCelular")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtLimiteVelocidad")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtConductorBebido")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCasco")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFiltroSolar")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtSeguridadDomestica")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtEstadoSalud")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtHabitosVida")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIncapacitado")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIdDiagnosticoIncapacidad")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiasIncapacidad")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIdDiagnosticoHospitalizacion1")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaHospitalizacion1")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiasHospitalizacion1")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIdDiagnosticoHospitalizacion2")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaHospitalizacion2")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiasHospitalizacion2")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIdDiagnosticoHospitalizacion3")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaHospitalizacion3")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiasHospitalizacion3")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIdDiagnosticoHospitalizacion4")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtFechaHospitalizacion4")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiasHospitalizacion4")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtTrasmisionSexual")).Attributes.Add("ReadOnly", "ReadOnly");
                //NUTRICION
                ((TextBox)item.FindControl("txtDesayuno")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDesayunoHora")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAlmuerzo")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAlmuerzoHora")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtComida")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtComidaHora")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtEntremes")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtEntremesHora")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCena")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtCenaHora")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiametroCintura")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiametroCadera")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtRelacionCinturaCadera")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDescripcionRelacion")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMasaGrasa")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtMasaGrama")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtPesoRecomendable")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtExcedenteGrasa")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtDiagnosticoNutricional")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtIdDiagnosticoNutricional")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtRecomendacionesNutricionales")).Attributes.Add("ReadOnly", "ReadOnly");
                ((TextBox)item.FindControl("txtAlimentacionSaludable")).Attributes.Add("ReadOnly", "ReadOnly");
                
            }
            //Fin PETF 14/01/10            
        }
        
        /// <summary>
        /// Métoco, realiza la búsqueda de consultas
        /// </summary>
        public void FindConsulta()
        {
            Consulta objConsulta = new Consulta();
            DateTime fechaInicio, fechaFin;

            if (this.txtFechaInicio.Text.Trim() != string.Empty)
                fechaInicio = Convert.ToDateTime(this.txtFechaInicio.Text);
            else
                fechaInicio = DateTime.Now.AddDays(-360);
            if (this.txtFechaFin.Text.Trim() != string.Empty)
                fechaFin = Convert.ToDateTime(this.txtFechaFin.Text);
            else
                fechaFin = DateTime.Now;
            if (Request.QueryString["empleado"] != null && Request.QueryString["empleado"] == "s")
                this.dtlConsultas.DataSource = objConsulta.ConsultConsultaCompleta(0, Convert.ToInt32(Request.QueryString["beneficiario_id"]), fechaInicio, fechaFin);
            else
                this.dtlConsultas.DataSource = objConsulta.ConsultConsultaCompleta(Convert.ToInt32(Request.QueryString["beneficiario_id"]), 0, fechaInicio, fechaFin);
            
            this.dtlConsultas.DataBind();
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
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.dtlConsultas.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtlConsultas_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCerrar_Click(object sender, System.EventArgs e)
        {
            Response.Write("<script>top.close();</script>");
        }


        /// <summary>
        /// Evento, realiza la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            this.FindConsulta();

        }

        /// <summary>
        /// Evento, Realiza la carga de las tablas que deben desplegarse dependiendo del histórico que se quiera desplegar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtlConsultas_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Request.QueryString["Modulo"] == "todos")
                {
                    HtmlTable tblDatosConsulta = (HtmlTable)e.Item.FindControl("tblDatosConsulta");
                    tblDatosConsulta.Style["display"] = "";
                }
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "diagnosticos")
                {
                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                    ConsultaDiagnosticos objConsultaDiagnosticos = new ConsultaDiagnosticos();
                    objConsultaDiagnosticos.IdConsulta = Convert.ToInt64(rowItem["IdConsulta"]);
                    DataGrid dtgDiagnosticos = (DataGrid)e.Item.FindControl("dtgDiagnosticos");
                    dtgDiagnosticos.DataSource = objConsultaDiagnosticos.ConsultConsultaDiagnosticos();
                    dtgDiagnosticos.DataBind();

                    HtmlTable tblImpresionDiagnostica = (HtmlTable)e.Item.FindControl("tblImpresionDiagnostica");
                    tblImpresionDiagnostica.Style["display"] = "";


                }
                
               
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "antecedentes")
                {
                    HtmlTable tblAntecendentes = (HtmlTable)e.Item.FindControl("tblAntecendentes");
                    tblAntecendentes.Style["display"] = "";
                }
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "revision")
                {
                    HtmlTable tblRevisionSistemas = (HtmlTable)e.Item.FindControl("tblRevisionSistemas");
                    tblRevisionSistemas.Style["display"] = "";
                }
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "examen")
                {
                    HtmlTable tblExamenFisico = (HtmlTable)e.Item.FindControl("tblExamenFisico");
                    tblExamenFisico.Style["display"] = "";
                }
                
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "incapacidad")
                {
                    HtmlTable tblIncapacidad = (HtmlTable)e.Item.FindControl("tblIncapacidad");

                    DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;

                    if (!Convert.IsDBNull(rowItem["FechaInicio"]))
                        tblIncapacidad.Style["display"] = "";
                    else
                    {
                        if (Request.QueryString["Modulo"] == "incapacidad")
                        {
                            e.Item.CssClass = "hidden";
                        }
                    }

                }
                if (Request.QueryString["Modulo"] == "todos" || Request.QueryString["Modulo"] == "saludOcupacional")
                {
                    HtmlTable tblSaludOcupacional = (HtmlTable)e.Item.FindControl("tblSaludOcupacional");
                    tblSaludOcupacional.Style["display"] = "";
                }


                
                //John Portela 16/03/2010 
                if (Session["Company"] != null)
                {
                    EmpresaDivisiones objEmpresaDivisiones = new EmpresaDivisiones();
                    int numIdEmpresa = int.Parse(Session["Company"].ToString());
                    if (numIdEmpresa != 0)
                    {
                        objEmpresaDivisiones.Empresa_id = numIdEmpresa;
                        objEmpresaDivisiones.GetEmpresaDivisiones();
                        //HABITOS
                        if (objEmpresaDivisiones.DivHabitos)
                        {
                            HtmlTable tblHabitos = (HtmlTable)e.Item.FindControl("tblHabitos");
                            tblHabitos.Style["display"] = "";
                        }
                        //PRUEBAS BIOMÉTRICAS
                        if (objEmpresaDivisiones.DivColesterolGlicemia && objEmpresaDivisiones.DivExamenesLaboratorio 
                            && objEmpresaDivisiones.DivMujer && objEmpresaDivisiones.DivAudiometria)
                        {
                            HtmlTable tblPruebasBiometricas = (HtmlTable)e.Item.FindControl("tblPruebasBiometricas");
                            tblPruebasBiometricas.Style["display"] = "";
                        }
                         //ESTILO DE VIDA
                        if (objEmpresaDivisiones.DivWellness)
                        {
                            HtmlTable tblWellness = (HtmlTable)e.Item.FindControl("tblWellness");
                            tblWellness.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivHabitoFumar)
                        {
                            HtmlTable tblHabitoFumar = (HtmlTable)e.Item.FindControl("tblHabitoFumar");
                            tblHabitoFumar.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivConsumoAlcohol)
                        {
                            HtmlTable tblConsumoAlcohol = (HtmlTable)e.Item.FindControl("tblConsumoAlcohol");
                            tblConsumoAlcohol.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivVacunacion)
                        {
                            HtmlTable tblVacunacion = (HtmlTable)e.Item.FindControl("tblVacunacion");
                            tblVacunacion.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivSedentarismo)
                        {
                            HtmlTable tblSedentarismo = (HtmlTable)e.Item.FindControl("tblSedentarismo");
                            tblSedentarismo.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivSaludOral)
                        {
                            HtmlTable tblSaludOral = (HtmlTable)e.Item.FindControl("tblSaludOral");
                            tblSaludOral.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivEstres)
                        {
                            HtmlTable tblEstres = (HtmlTable)e.Item.FindControl("tblEstres");
                            tblEstres.Style["display"] = "";
                            DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                            ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                            objConsultaOpcion.IdConsulta = Convert.ToInt64(rowItem["IdConsulta"]);
                            BulletedList btlFuentesEstres = (BulletedList)e.Item.FindControl("btlFuentesEstres");
                            objConsultaOpcion.IdPreguntaRespuestaPadre = 50;
                            btlFuentesEstres.DataSource = objConsultaOpcion.ConsultConsultaOpcionPadre();
                            btlFuentesEstres.DataBind();
                        }
                        if (objEmpresaDivisiones.DivEmocional)
                        {
                            HtmlTable tblEmocional = (HtmlTable)e.Item.FindControl("tblEmocional");
                            tblEmocional.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivAccidentalidad)
                        {
                            HtmlTable tblAccidentalidad = (HtmlTable)e.Item.FindControl("tblAccidentalidad");
                            tblAccidentalidad.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivEstadoSalud)
                        {
                            HtmlTable tblEstadoSalud = (HtmlTable)e.Item.FindControl("tblEstadoSalud");
                            tblEstadoSalud.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivNutricion)
                        {
                            HtmlTable tblNutricion = (HtmlTable)e.Item.FindControl("tblNutricion");
                            tblNutricion.Style["display"] = "";
                            DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                            ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                            objConsultaOpcion.IdConsulta = Convert.ToInt64(rowItem["IdConsulta"]);
                            BulletedList btlCompraAlimentos = (BulletedList)e.Item.FindControl("btlCompraAlimentos");
                            objConsultaOpcion.IdPreguntaRespuestaPadre = 209;
                            btlCompraAlimentos.DataSource = objConsultaOpcion.ConsultConsultaOpcionPadre();
                            btlCompraAlimentos.DataBind();

                            DataRow rowItem1 = ((DataRowView)e.Item.DataItem).Row;
                            ConsultaOpcion objConsultaOpcion1 = new ConsultaOpcion();
                            objConsultaOpcion1.IdConsulta = Convert.ToInt64(rowItem1["IdConsulta"]);
                            BulletedList btlPreparaAlimentos = (BulletedList)e.Item.FindControl("btlPreparaAlimentos");
                            objConsultaOpcion1.IdPreguntaRespuestaPadre = 214;
                            btlPreparaAlimentos.DataSource = objConsultaOpcion1.ConsultConsultaOpcionPadre();
                            btlPreparaAlimentos.DataBind();

                            DataRow rowItem2 = ((DataRowView)e.Item.DataItem).Row;
                            ConsultaOpcion objConsultaOpcion2 = new ConsultaOpcion();
                            objConsultaOpcion2.IdConsulta = Convert.ToInt64(rowItem2["IdConsulta"]);
                            BulletedList btlAlimentosAgrado = (BulletedList)e.Item.FindControl("btlAlimentosAgrado");
                            objConsultaOpcion2.IdPreguntaRespuestaPadre = 228;
                            btlAlimentosAgrado.DataSource = objConsultaOpcion2.ConsultConsultaOpcionPadre();
                            btlAlimentosAgrado.DataBind();

                            DataRow rowItem3 = ((DataRowView)e.Item.DataItem).Row;
                            ConsultaOpcion objConsultaOpcion3 = new ConsultaOpcion();
                            objConsultaOpcion3.IdConsulta = Convert.ToInt64(rowItem3["IdConsulta"]);
                            BulletedList btlAlimentosDisguntan = (BulletedList)e.Item.FindControl("btlAlimentosDisguntan");
                            objConsultaOpcion3.IdPreguntaRespuestaPadre = 244;
                            btlAlimentosDisguntan.DataSource = objConsultaOpcion3.ConsultConsultaOpcionPadre();
                            btlAlimentosDisguntan.DataBind();
                        }
                        if (objEmpresaDivisiones.DivAntecedentesAusentismo)
                        {
                            HtmlTable tblAntecedentesAusentismo = (HtmlTable)e.Item.FindControl("tblAntecedentesAusentismo");
                            tblAntecedentesAusentismo.Style["display"] = "";
                        }
                        if (objEmpresaDivisiones.DivRecomendaciones)
                        {
                            HtmlTable tblRecomendaciones = (HtmlTable)e.Item.FindControl("tblRecomendaciones");
                            tblRecomendaciones.Style["display"] = "";
                            DataRow rowItem = ((DataRowView)e.Item.DataItem).Row;
                            ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                            objConsultaOpcion.IdConsulta = Convert.ToInt64(rowItem["IdConsulta"]);
                            BulletedList btlRecomendaciones = (BulletedList)e.Item.FindControl("btlRecomendaciones");
                            objConsultaOpcion.IdPreguntaRespuestaPadre = 168;
                            btlRecomendaciones.DataSource = objConsultaOpcion.ConsultConsultaOpcionPadre();
                            btlRecomendaciones.DataBind();
                        }

                    }
                }
                TextBox txtAnormalCelulasEpi = (TextBox)e.Item.FindControl("txtAnormalCelulasEpi");
                TextBox txtCelulasEscamosas = (TextBox)e.Item.FindControl("txtCelulasEscamosas");
                Label lblAnormalCelulasEpi = (Label)e.Item.FindControl("lblAnormalCelulasEpi");
                Label lblCelulasEscamosas = (Label)e.Item.FindControl("lblCelulasEscamosas");
                if (txtAnormalCelulasEpi.Text == string.Empty)
                {
                    txtAnormalCelulasEpi.Visible = false;
                    lblAnormalCelulasEpi.Visible = false;
                }
                else
                {
                    txtAnormalCelulasEpi.Visible = true;
                    lblAnormalCelulasEpi.Visible = true;
                }
                if (txtCelulasEscamosas.Text == string.Empty)
                {
                    txtCelulasEscamosas.Visible = false;
                    lblCelulasEscamosas.Visible = false;
                }
                else
                {
                    txtCelulasEscamosas.Visible = true;
                    lblCelulasEscamosas.Visible = true;
                }
                TextBox txtIdConductaCigarrillo = (TextBox)e.Item.FindControl("txtIdConductaCigarrillo");
                if (txtIdConductaCigarrillo.Text == "284")
                {
                    //Fuma Actualmente
                    HtmlTableRow trTiempoPrimerCigarrillo = (HtmlTableRow)e.Item.FindControl("trTiempoPrimerCigarrillo");
                    trTiempoPrimerCigarrillo.Visible = false;
                    HtmlTableRow trDificultadFumar = (HtmlTableRow)e.Item.FindControl("trDificultadFumar");
                    trDificultadFumar.Visible = false;
                    HtmlTableRow trCigarrilloSuprimir = (HtmlTableRow)e.Item.FindControl("trCigarrilloSuprimir");
                    trCigarrilloSuprimir.Visible = false;
                    HtmlTableRow trCigarrillosalDia = (HtmlTableRow)e.Item.FindControl("trCigarrillosalDia");
                    trCigarrillosalDia.Visible = false;
                    HtmlTableRow trFrecuenciaPrimerasHorasDia = (HtmlTableRow)e.Item.FindControl("trFrecuenciaPrimerasHorasDia");
                    trFrecuenciaPrimerasHorasDia.Visible = false;
                    HtmlTableRow trFumaEnfermedad = (HtmlTableRow)e.Item.FindControl("trFumaEnfermedad");
                    trFumaEnfermedad.Visible = false;
                    HtmlTableRow trCategoriaCigarrillos = (HtmlTableRow)e.Item.FindControl("trCategoriaCigarrillos");
                    trCategoriaCigarrillos.Visible = false;
                    HtmlTableRow trAspiraHumo = (HtmlTableRow)e.Item.FindControl("trAspiraHumo");
                    trAspiraHumo.Visible = false;
                    //Fumaba
                    HtmlTableRow trPromedioDiarioX2Anos = (HtmlTableRow)e.Item.FindControl("trPromedioDiarioX2Anos");
                    trPromedioDiarioX2Anos.Visible = false;
                    HtmlTableRow trAnosDejoFumar = (HtmlTableRow)e.Item.FindControl("trAnosDejoFumar");
                    trAnosDejoFumar.Visible = false;



                }
                if (txtIdConductaCigarrillo.Text == "285")
                {
                    //Fuma Actualmente
                    HtmlTableRow trTiempoPrimerCigarrillo = (HtmlTableRow)e.Item.FindControl("trTiempoPrimerCigarrillo");
                    trTiempoPrimerCigarrillo.Visible = false;
                    HtmlTableRow trDificultadFumar = (HtmlTableRow)e.Item.FindControl("trDificultadFumar");
                    trDificultadFumar.Visible = false;
                    HtmlTableRow trCigarrilloSuprimir = (HtmlTableRow)e.Item.FindControl("trCigarrilloSuprimir");
                    trCigarrilloSuprimir.Visible = false;
                    HtmlTableRow trCigarrillosalDia = (HtmlTableRow)e.Item.FindControl("trCigarrillosalDia");
                    trCigarrillosalDia.Visible = false;
                    HtmlTableRow trFrecuenciaPrimerasHorasDia = (HtmlTableRow)e.Item.FindControl("trFrecuenciaPrimerasHorasDia");
                    trFrecuenciaPrimerasHorasDia.Visible = false;
                    HtmlTableRow trFumaEnfermedad = (HtmlTableRow)e.Item.FindControl("trFumaEnfermedad");
                    trFumaEnfermedad.Visible = false;
                    HtmlTableRow trCategoriaCigarrillos = (HtmlTableRow)e.Item.FindControl("trCategoriaCigarrillos");
                    trCategoriaCigarrillos.Visible = false;
                    HtmlTableRow trAspiraHumo = (HtmlTableRow)e.Item.FindControl("trAspiraHumo");
                    trAspiraHumo.Visible = false;
                    //Fumaba
                    HtmlTableRow trPromedioDiarioX2Anos = (HtmlTableRow)e.Item.FindControl("trPromedioDiarioX2Anos");
                    trPromedioDiarioX2Anos.Visible = true;
                    HtmlTableRow trAnosDejoFumar = (HtmlTableRow)e.Item.FindControl("trAnosDejoFumar");
                    trAnosDejoFumar.Visible = true;
                }
                if (txtIdConductaCigarrillo.Text == "286")
                {
                    //Fuma Actualmente
                    HtmlTableRow trTiempoPrimerCigarrillo = (HtmlTableRow)e.Item.FindControl("trTiempoPrimerCigarrillo");
                    trTiempoPrimerCigarrillo.Visible = true;
                    HtmlTableRow trDificultadFumar = (HtmlTableRow)e.Item.FindControl("trDificultadFumar");
                    trDificultadFumar.Visible = true;
                    HtmlTableRow trCigarrilloSuprimir = (HtmlTableRow)e.Item.FindControl("trCigarrilloSuprimir");
                    trCigarrilloSuprimir.Visible = true;
                    HtmlTableRow trCigarrillosalDia = (HtmlTableRow)e.Item.FindControl("trCigarrillosalDia");
                    trCigarrillosalDia.Visible = true;
                    HtmlTableRow trFrecuenciaPrimerasHorasDia = (HtmlTableRow)e.Item.FindControl("trFrecuenciaPrimerasHorasDia");
                    trFrecuenciaPrimerasHorasDia.Visible = true;
                    HtmlTableRow trFumaEnfermedad = (HtmlTableRow)e.Item.FindControl("trFumaEnfermedad");
                    trFumaEnfermedad.Visible = true;
                    HtmlTableRow trCategoriaCigarrillos = (HtmlTableRow)e.Item.FindControl("trCategoriaCigarrillos");
                    trCategoriaCigarrillos.Visible = true;
                    HtmlTableRow trAspiraHumo = (HtmlTableRow)e.Item.FindControl("trAspiraHumo");
                    trAspiraHumo.Visible = true;
                    //Fumaba
                    HtmlTableRow trPromedioDiarioX2Anos = (HtmlTableRow)e.Item.FindControl("trPromedioDiarioX2Anos");
                    trPromedioDiarioX2Anos.Visible = false;
                    HtmlTableRow trAnosDejoFumar = (HtmlTableRow)e.Item.FindControl("trAnosDejoFumar");
                    trAnosDejoFumar.Visible = false;
                }
                
               
            }

        }

        #endregion


    }
}
