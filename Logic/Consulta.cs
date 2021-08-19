/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2008 by Mercer
'===============================================================================
*/

using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;
using System.Collections;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para las acciones de las consulta
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: </remarks>
    public class Consulta : GeneralProcess
    {
        #region Attributes
        #region GENERAL
        /// <summary>Atributo, Id de la consulta</summary>
        private long _IdConsulta;
        /// <summary>Atributo, Id de la empresa en SICAU</summary>
        private int _Empresa_id;
        /// <summary>Atributo, Id del empleado al que se le realiza la consulta</summary>
        private int _Id_empleado;
        /// <summary>Atributo, Id del beneficiario al que se realiza la consulta</summary>
        private int _Beneficiario_id;
        /// <summary>Atributo, Id del prestador o solicitante que realiza la consulta</summary>
        private int _IdPrestador;
        /// <summary>Atributo, Id del tipo de consulta</summary>
        private short _IdTipoConsulta;
        /// <summary>Atributo, Id del tipo de enfermedad</summary>
        private short _IdTipoEnfermedad;
        /// <summary>Atributo, Motivo de la consulta</summary>
        private string _Motivo;
        /// <summary>Atributo, Contrarreferencia de la consulta</summary>
        private string _Contrarreferencia;
        /// <summary>Atributo, Texto de enfermedad actual</summary>
        private string _EnfermedadActual;
        /// <summary>Atributo, </summary>
        private string _ObservacionesGenerales;
        /// <summary>Atributo, </summary>
        private string _PlanTratamiento;
        /// <summary>Atributo, </summary>
        private DateTime _CitaControl;
        /// <summary>Atributo, Fecha de creación</summary>
        private DateTime _FechaCreacion;
        /// <summary>Atributo, Fecha de la última modificación</summary>
        private DateTime _FechaModificacion;
        /// <summary>Atributo, Id del usuario de creación</summary>
        private int _IdUserCreacion;
        /// <summary>Atributo, Id del usuario de creación en SICAU</summary>
        private int _Usuario_idCreacion;
        /// <summary>Atributo, Id de la solicitud en el sistema SICAU para cierre del caso</summary>
        private int _Id_solicitud_SICAU;
        /// <summary>Atributo, No de consecutivo</summary>
        private int _Consecutivo;
        /// <summary>Atributo, Consecutivo de la consulta</summary>
        private string _ConsecutivoNombre;
        /// <summary>Atributo, Arreglo que contiene los diagnosticos de la consulta</summary>
        private ArrayList _ConsultaDiagnosticos;
        /// <summary>Atributo, Id de la solicitud</summary>
        private long _IdSolicitud;
        /// <summary>Atributo, Id de la cita</summary>
        private int _cita_id;
        /// <summary>Atributo, Id del proveedore que solicita la transcripción</summary>
        private int _IdProveedorTranscripcion;
        /// <summary>Atributo, Comentarios si la consulta es una transcripción</summary>
        private string _ComentariosTranscripcion;
        /// <summary>Atributo, Campo para escribir resultados de exámenes de laboratorio</summary>
        private string _ExamenesLaboratorio;
        /// <summary>Atributo, Fecha exacta de cuando se ingresa al primer paso de la consulta</summary>
        private DateTime _FechaInicioCreacion;
        /// <summary>Atributo, Fecha fin exacta de cuando se guarda el último paso de la consulta</summary>
        private DateTime _FechaFinCreacion;
        /// <summary>Atributo, Arreglo que contiene los diagnosticos de la consulta</summary>
        private ArrayList _ConsultaOpcion;
        /// <summary>
        /// Atributo, Indica si el tipo de consulta "Wellness Primera Vez" ha sido cerrada
        /// </summary>
        private bool _Finalizada;
        /// <summary>Atributo, Id de la sede donde se atiende la consulta</summary>
        private int _sede_id;
        /// <summary>Atributo, Nombre del prestador</summary>
        private string _NombrePrestador;
        /// <summary>Atributo, Id de de la linea de negocio del empleado de la consulta</summary>
        private int _IdLineaNegocio;

        #endregion

        #region ANTECEDENTES
        //ANTECEDENTES
        /// <summary>Atributo, Antecedentes médicos</summary>
        private string _Medicos;
        /// <summary>Atributo, Antecedentes Quirúrgicos</summary>
        private string _Quirurgicos;
        /// <summary>Atributo, Antecedentes Ginecobstétricos</summary>
        private string _Ginecobstetricos;
        /// <summary>Atributo, Antecedentes transfusionales</summary>
        private string _Transfusionales;
        /// <summary>Atributo, Antecedentes toxico alérgicos</summary>
        private string _ToxicoAlergicos;
        /// <summary>Atributo, Antecedentes farmacologicos</summary>
        private string _Farmacologicos;
        /// <summary>Atributo, Antecedentes familiares</summary>
        private string _Familiares;
        /// <summary>Atributo, Otros antecedentes</summary>
        private string _OtrosAntecedentes;
        /// <summary>Atributo, Menarquia</summary>
        private string _Menarquia;
        /// <summary>Atributo, </summary>
        private string _FechaUltimaMestruacion;
        /// <summary>Atributo, </summary>
        private short _Gestaciones;
        /// <summary>Atributo, </summary>
        private short _Partos;
        /// <summary>Atributo, </summary>
        private short _Cesareas;
        /// <summary>Atributo, </summary>
        private short _Abortos;
        /// <summary>Atributo, </summary>
        private short _Vivos;
        /// <summary>Atributo, Indica estado normal de antecedentes médicos</summary>
        private bool _NormalMedicos;
        /// <summary>Atributo, Indica estado normal de antecedentes quirúrgicos</summary>
        private bool _NormalQuirurgicos;
        /// <summary>Atributo, Indica estado normal de antecedentes ginecobstetricos</summary>
        private bool _NormalGinecobstetricos;
        /// <summary>Atributo, Indica estado normal de antecedentes transfusionales</summary>
        private bool _NormalTransfusionales;
        /// <summary>Atributo, Indica estado normal de antecedentes toxico alergicos</summary>
        private bool _NormalToxicoAlergicos;
        /// <summary>Atributo, Indica estado normal de antecedentes farmacologicos</summary>
        private bool _NormalFarmacologicos;
        /// <summary>Atributo, Indica estado normal de antecedentes familiares</summary>
        private bool _NormalFamiliares;
        /// <summary>Atributo, Indica estad normal de otros antecedentes</summary>
        private bool _NormalOtrosAntecedentes;
        /// <summary>Atributo, Indica riesgo cardiovascular</summary>
        private bool _RiesgoCardiovascular;

        #endregion

        #region EXAMEN FÍSICO
        //EXAMEN FÍSICO
        /// <summary>Atributo, Peso del paciente</summary>
        private decimal _Peso;
        /// <summary>Atributo, Talla del peciente</summary>
        private decimal _Talla;
        /// <summary>Atributo, Indice de Masa Corporal del paciente</summary>
        private decimal _IndiceMasaCorporal;
        /// <summary>Atributo, Tensión arterial del paciente</summary>
        private string _TensionArterial;
        /// <summary>Atributo, Tensión arterial sistolica del paciente</summary>
        private string _TensionArterialSistolica;
        /// <summary>Atributo, Tensión arterial diastolica del paciente</summary>
        private string _TensionArterialDiastolica;
        /// <summary>Atributo, Frecuencia cardiaca del paciente</summary>
        private int _FrecuenciaCardiaca;
        /// <summary>Atributo, Frecuencia respiratoria del paciente</summary>
        private int _FrecuenciaRespiratoria;
        /// <summary>Atributo, Perimetro abdominal del paciente</summary>
        private decimal _PerimetroAbdominal;
        /// <summary>Atributo, Comentarios generales del exámen físico</summary>
        private string _ComentariosExamenFisico;
        /// <summary>Atributo, Aspecto general</summary>
        private string _ExamenAspectoGeneral;
        /// <summary>Atributo, Cabeza</summary>
        private string _ExamenCabeza;
        /// <summary>Atributo, Cuello</summary>
        private string _ExamenCuello;
        /// <summary>Atributo, Torax</summary>
        private string _ExamenTorax;
        /// <summary>Atributo, Abdomen</summary>
        private string _ExamenAbdomen;
        /// <summary>Atributo, Otros sistemas</summary>
        private string _ExamenOtros;
        /// <summary>Atributo, Indica estado normal de aspecto general</summary>
        private bool _ExamenNormalAspectoGeneral;
        /// <summary>Atributo, Indica estado normal de cabeza</summary>
        private bool _ExamenNormalCabeza;
        /// <summary>Atributo, Indica estado normal de cuello</summary>
        private bool _ExamenNormalCuello;
        /// <summary>Atributo, Indica estado normal de torax</summary>
        private bool _ExamenNormalTorax;
        /// <summary>Atributo, Indica estado normal de abdomen</summary>
        private bool _ExamenNormalAbdomen;
        /// <summary>Atributo, Indica estado normal de otros</summary>
        private bool _ExamenNormalOtros;
        #endregion

        #region HABITOS
        //HABITOS
        /// <summary>Atributo, Indica si consume tabaco</summary>
        private int _Tabaquismo;
        /// <summary>Atributo, Indica si realiza actividad deportiva</summary>
        private int _ActividadDeportiva;
        /// <summary>Atributo, Indica si consume alcohol</summary>
        private int _ConsumoAlcohol;
        /// <summary>Atributo, Frecuencia de consumo alcohol</summary>
        private string _FrecuenciaConsumo;
        /// <summary>Atributo, Frecuencia tabaquismo</summary>
        private string _FrecuenciaTabaquismo;
        /// <summary>Atributo, Texto para vacunación</summary>
        private string _Vacunacion;
        #endregion

        #region REVISIÓN POR SISTEMAS

        //REVISIÓN POR SISTEMAS
        /// <summary>Atributo, Aspecto general</summary>
        private string _AspectoGeneral;
        /// <summary>Atributo, Cabeza</summary>
        private string _Cabeza;
        /// <summary>Atributo, Cuello</summary>
        private string _Cuello;
        /// <summary>Atributo, Torax</summary>
        private string _Torax;
        /// <summary>Atributo, Abdomen</summary>
        private string _Abdomen;
        /// <summary>Atributo, Otros sistemas</summary>
        private string _Otros;
        /// <summary>Atributo, Indica estado normal de aspecto general</summary>
        private bool _NormalAspectoGeneral;
        /// <summary>Atributo, Indica estado normal de cabeza</summary>
        private bool _NormalCabeza;
        /// <summary>Atributo, Indica estado normal de cuello</summary>
        private bool _NormalCuello;
        /// <summary>Atributo, Indica estado normal de torax</summary>
        private bool _NormalTorax;
        /// <summary>Atributo, Indica estado normal de abdomen</summary>
        private bool _NormalAbdomen;
        /// <summary>Atributo, Indica estado normal de otros</summary>
        private bool _NormalOtros;
        /// <summary>Atributo, Temperatura del paciente</summary>
        private decimal _Temperatura;
        /// <summary>Atributo, Examen de piel y fanelas</summary>
        private string _ExamenPielFanelas;
        /// <summary>Atributo, Indica si piel y fanelas es normal</summary>
        private bool _ExamenNormalPielFanelas;
        /// <summary>Atributo, Examen de conjuntiva ocular</summary>
        private string _ExamenConjuntivaOcular;
        /// <summary>Atributo, Indica si conjuntiva ocular es normal</summary>
        private bool _ExamenNormalConjuntivaOcular;
        /// <summary>Atributo, Examen de reflejo corneal</summary>
        private string _ExamenReflejoCorneal;
        /// <summary>Atributo, Indica si reflejo corneal es normal</summary>
        private bool _ExamenNormalReflejoCorneal;
        /// <summary>Atributo, Examen de pupilas</summary>
        private string _ExamenPupilas;
        /// <summary>Atributo, Indica si pupilas es normal</summary>
        private bool _ExamenNormalPupilas;
        /// <summary>Atributo, Examen de oídos</summary>
        private string _ExamenOidos;
        /// <summary>Atributo, Indica si oidos es normal</summary>
        private bool _ExamenNormalOidos;
        /// <summary>Atributo, Examen de otoscopia</summary>
        private string _ExamenOtoscopia;
        /// <summary>Atributo, Indica si otoscopia es normal</summary>
        private bool _ExamenNormalOtoscopia;
        /// <summary>Atributo, Examen de rinoscopia</summary>
        private string _ExamenRinoscopia;
        /// <summary>Atributo, Indica si rinoscopia es normal</summary>
        private bool _ExamenNormalRinoscopia;
        /// <summary>Atributo, Examen de boca y faringe</summary>
        private string _ExamenBocaFaringe;
        /// <summary>Atributo, Indica si boca y faringe es normal</summary>
        private bool _ExamenNormalBocaFaringe;
        /// <summary>Atributo, Examen de amigdalas</summary>
        private string _ExamenAmigdalas;
        /// <summary>Atributo, Indica si amgdalas es normal</summary>
        private bool _ExamenNormalAmigdalas;
        /// <summary>Atributo, Examen de tiroides</summary>
        private string _ExamenTiroides;
        /// <summary>Atributo, Indica si tiroides es normal</summary>
        private bool _ExamenNormalTiroides;
        /// <summary>Atributo, Examen de adenopatias</summary>
        private string _ExamenAdenopatias;
        /// <summary>Atributo, Indica si adenopatias es normal</summary>
        private bool _ExamenNormalAdenopatias;
        /// <summary>Atributo, Examen de ruidos cardiacos</summary>
        private string _ExamenRuidosCardiacos;
        /// <summary>Atributo, Indica si ruidos cardiacos es normal</summary>
        private bool _ExamenNormalRuidosCardiacos;
        /// <summary>Atributo, Examen de ruidos respiratorios</summary>
        private string _ExamenRuidosRespiratorios;
        /// <summary>Atributo, Indica si ruidos respiratorios es normal</summary>
        private bool _ExamenNormalRuidosRespiratorios;
        /// <summary>Atributo, Examen palpación de abdomen</summary>
        private string _ExamenPalpacionAbdomen;
        /// <summary>Atributo, Indica si palpación es normal</summary>
        private bool _ExamenNormalPalpacionAbdomen;
        /// <summary>Atributo, Examen de genitales externos</summary>
        private string _ExamenGenitalesExternos;
        /// <summary>Atributo, Indica si genitales externos es normal</summary>
        private bool _ExamenNormalGenitalesExternos;
        /// <summary>Atributo, Examen de hernias</summary>
        private string _ExamenHernias;
        /// <summary>Atributo, Indica si hermias es normal</summary>
        private bool _ExamenNormalHernias;
        /// <summary>Atributo, Examen de columna vertebral</summary>
        private string _ExamenColumnaVertebral;
        /// <summary>Atributo, Indica si columna vertebral es normal</summary>
        private bool _ExamenNormalColumnaVertebral;
        /// <summary>Atributo, Examen de extremidades superiores</summary>
        private string _ExamenExtremidadesSuperiores;
        /// <summary>Atributo, Indica si extremidades superiores es normal</summary>
        private bool _ExamenNormalExtremidadesSuperiores;
        /// <summary>Atributo, Examen extremidades inferiores</summary>
        private string _ExamenExtremidadesInferiores;
        /// <summary>Atributo, Indica si extremidades inferiores es normal</summary>
        private bool _ExamenNormalExtremidadesInferiores;
        /// <summary>Atributo, Examen de varices</summary>
        private string _ExamenVarices;
        /// <summary>Atributo, Indica si varices es normal</summary>
        private bool _ExamenNormalVarices;
        /// <summary>Atributo, Examen neurológico</summary>
        private string _ExamenNeurologico;
        /// <summary>Atributo, Indica si neurológico es normal</summary>
        private bool _ExamenNormalNeurologico;
        /// <summary>Atributo, Nombre del usuario</summary>
        private string _NameUser;
        #endregion

        #region PRUEBAS BIOMÉTRICAS
        //PRUEBAS BIOMÉTRICAS
        /// <summary>Atributo, Valor total de colesterol</summary>
        private int _ColesterolTotal;
        /// <summary>Atributo, Valor colesterol HDL</summary>
        private int _ColesterolHDL;
        /// <summary>Atributo, Valor colestero LDL</summary>
        private int _ColesterolLDL;
        /// <summary>Atributo, Valor trigliceridos</summary>
        private int _Trigliceridos;
        /// <summary>Atributo, Valor Indice Aterogenico</summary>
        private decimal _IndiceAterogenico;
        /// <summary>Atributo, Valor Antigeno de Prostata</summary>
        private decimal _AntigenoProstata;
        /// <summary>Atributo, Valor Glucemia Ayunas</summary>
        private int _GlucemiaAyunas;
        /// <summary>Atributo, Valor Glicemia sin Ayunas</summary>
        private int _GlucemiaSinAyunas;
        /// <summary>Atributo, Valor Hemoglobina Glucosilada</summary>
        private decimal _HemoglobinaGlucosilada;
        /// <summary>Atributo, Valor Homocisteina</summary>
        private decimal _Homocisteina;
        /// <summary>Atributo, Indica si hay existencia de microorganismos</summary>
        private int _PresenciaMicroorganismos;
        /// <summary>Atributo, Indica la fecha del papanicolau microbiológico</summary>
        private DateTime _FechaPapanicolauMicro;
        /// <summary>Atributo, Observaciones del papanicolau microbiológico</summary>
        private string _ObservacionesPresenciaMicro;
        /// <summary>Atributo, Indica el valor que corresponde al resultado morfológico</summary>
        private int _ResultadoMorfologico;
        /// <summary>Atributo, Indica el valor que corresponde al resultado en anormalidades de células epiteliales</summary>
        private int _AnormalidadCelulasEpiteliales;
        /// <summary>Atributo, Indica el valor que corresponde al resultado en células escamosas atípicas</summary>
        private int _CelulasEscamosasAtipicas;
        /// <summary>Atributo, Indica el valor que corresponde al resultado de la mamográfica</summary>
        private int _Mamografia;
        /// <summary>Atributo, Indica si es normal o anormal el examen de audiometría</summary>
        private int _Audiometria;
        /// <summary>Atributo, Describe las observaciones de examen de audimetría</summary>
        private string _AudiometriaObservaciones;
        /// <summary>Atributo, Indica si es normal o anormal el examen de rayos x</summary>
        private int _RayosX;
        /// <summary>Atributo, Describe las observaciones de examen de rayos X</summary>
        private string _RayosXObservaciones;
        /// <summary>Atributo, Indica si padece de miopía</summary>
        private bool _Miopia;
        /// <summary>Atributo, Valor de la miopía</summary>
        private decimal _MiopiaValor;
        /// <summary>Atributo, Observaciones de la miopia</summary>
        private string _MiopiaObservaciones;
        /// <summary>Atributo, Indica si padece de astigmatísmo</summary>
        private bool _Astigmatismo;
        /// <summary>Atributo, Valor de astigmatismo</summary>
        private decimal _AstigmatismoValor;
        /// <summary>Atributo, Observaciones de astigmatismo</summary>
        private string _AstigmatismoObservaciones;
        /// <summary>Atributo, Indica si padece de hipermetropía</summary>
        private bool _Hipermetropia;
        /// <summary>Atributo, Valor de la hipermetriopía</summary>
        private decimal _HipermetropiaValor;
        /// <summary>Atributo, Observaciones de la hipermetropia</summary>
        private string _HipermetropiaObservaciones;
        /// <summary>Atributo, Indica si padece de presbicia</summary>
        private bool _Presbicia;
        /// <summary>Atributo, Valor de la presbicia</summary>
        private decimal _PresbiciaValor;
        /// <summary>Atributo, Observaciones de la presbicia</summary>
        private string _PresbiciaObservaciones;
        /// <summary>Atributo, Indica si existe otro problema</summary>
        private bool _OtrosExamenVisual;
        /// <summary>Atributo, Id de diagnostico del examen visual</summary>
        private int _IdDiagnosticoExamenVisual;
        /// <summary>Atributo, Guarda el valor de colesterolHDL en mmol</summary>
        private decimal _ColesterolHDLmmol;
        /// <summary>Atributo, Observaciones de la Mamografia</summary>
        private string _MamografiaObservaciones;
        /// <summary>Atributo, Valor de la miopía del ojo izquierdo</summary>
        private decimal _MiopiaValorOI;
        /// <summary>Atributo, Valor de astigmatismo del ojo izquierdo</summary>
        private decimal _AstigmatismoValorOI;
        /// <summary>Atributo, Valor de la hipermetriopía del ojo izquierdo</summary>
        private decimal _HipermetropiaValorOI;
        /// <summary>Atributo, Valor de la presbicia del ojo izquierdo</summary>
        private decimal _PresbiciaValorOI;
        #endregion

        #region WELLNESS
        /// <summary>Atributo, indica si esta afiliado al programa wellness</summary>
        private int _ProgramaWellness;
        /// <summary>Atributo, Indica si esta firmado el acuerdo wellness</summary>
        private int _FirmaWellness;
        #endregion

        #region HABITO DE FUMAR

        /// <summary>Atributo, Indica la conducta frente a el cigarrillo</summary>
        private int _ConductaCigarrillo;
        /// <summary>Atributo, Indica el tiempo que transcurre desde que se levanta hasta encender el primer cigarrillo</summary>
        private int _TiempoPrimerCigarrillo;
        /// <summary>Atributo, Indica las dificultades para no fumar en lugares donde está prohibido </summary>
        private int _DificultadFumar;
        /// <summary>Atributo, Indica el cigarrillo le costaría más suprimir</summary>
        private int _CigarrilloSuprimir;
        /// <summary>Atributo, Indica cuantos cigarrillos fuma al día</summary>
        private int _CigarrillosalDia;
        /// <summary>Atributo, Indica si Fuma más frecuentemente durante las primeras horas del día que durante el resto del día</summary>
        private int _FrecuenciaPrimerasHorasDia;
        /// <summary>Atributo, Indica si Fuma cuándo debe guardar cama por una enfermedad la mayor parte del día</summary>
        private int _FumaEnfermedad;
        /// <summary>Atributo, Indica en que categoría entran la mayoría de cigarrillos que usted fuma</summary>
        private int _CategoriaCigarrillos;
        /// <summary>Atributo, Indica si Aspira el humo cuando fuma</summary>
        private int _AspiraHumo;
        /// <summary>Atributo, Indica hace cuanto dejo de fumar</summary>
        private decimal _AnosDejoFumar;
        /// <summary>Atributo, Indica el promedio diario de cigarrillos que fumaba durante los dos años previos de dejar el hábito</summary>
        private int _PromedioDiarioX2Anos;
        /// <summary>Atributo, Indica si convive habitualmente con un fumador</summary>
        private int _ConviveFumador;


        #endregion

        #region CONSUMO ALCOHOL
        /// <summary>Atributo, Indica las Copas a la semana que consume</summary>
        private int _CopasSemana;
        /// <summary>Atributo, indica si han criticado su consumo de alcoho</summary>
        private int _CriticaAlcohol;
        /// <summary>Atributo, Indica si se ha arrepentido de la cantidad de alcohol que consumió</summary>
        private int _ArrepentidoAlcohol;
        /// <summary>Atributo, indica si ha tenido lagunas por el consumo de alcohol</summary>
        private int _LagunaAlcohol;
        /// <summary>Atributo, Indica si alguna vez lo primero que ha consumido en la mañana ha sido una copa de alcohol</summary>
        private int _MananaAlcohol;
        #endregion

        #region VACUNACION
        /// <summary>Atributo, Indica si se ha aplicado la vacuna contra Influenza Estacional en el último año</summary>
        private int _InfluenciaEstacional;
        /// <summary>Atributo, Guarda fecha de aplicación de la vacuna influencia estacional</summary>
        private DateTime _FechaInfluenzaEstacional;
        /// <summary>Atributo, Indica si se ha aplicado la vacuna contra Influenza H1N1 en el último año</summary>
        private int _InfluenciaH1N1;
        /// <summary>Atributo, Guarda fecha de aplicación de la vacuna influencia H1N1</summary>
        private DateTime _FechaInfluenciaH1N1;
        /// <summary>Atributo, indica si se ha aplicado la vacuna contra Fiebre Amarilla</summary>
        private int _FiebreAmarilla;
        /// <summary>Atributo, Guarda fecha de aplicación de la vacuna contra fiebre amarilla</summary>
        private DateTime _FechaFiebreAmarilla;
        /// <summary>Atributo, </summary>
        private int _HepatitisViral;
        /// <summary>Atributo, Guarda fecha de aplicación de la vacuna contra hepatitis viral</summary>
        private DateTime _FechaHepatitisViral;
        /// <summary>Atributo, indica si se ha aplicado la vacuna contra el Tétanos</summary>
        private int _ToxoideTetanico;
        /// <summary>Atributo, Guarda fecha de aplicación de la vacuna contra en tetanos</summary>
        private DateTime _FechaToxoideTetanico;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteInfluenciaEstacional;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteInfluenciaH1N1;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteFiebreAmarilla;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteHepatitisViral;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteToxoideTetanico;
        /// <summary>Atributo, </summary>
        private int _HepatitisViralB;
        /// <summary>Atributo, </summary>
        private DateTime _FechaHepatitisViralB;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteHepatitisViralB;
        /// <summary>Atributo, </summary>
        private int _Meningococo;
        /// <summary>Atributo, </summary>
        private DateTime _FechaMeningococo;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteMeningococo;
        /// <summary>Atributo, </summary>
        private int _FiebreTifoidea;
        /// <summary>Atributo, </summary>
        private DateTime _FechaFiebreTifoidea;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteFiebreTifoidea;
        /// <summary>Atributo, </summary>
        private int _FiebreVPH;
        /// <summary>Atributo, </summary>
        private DateTime _FechaVPH;
        /// <summary>Atributo, </summary>
        private string _MarcaLoteVPH;

        #endregion

        #region SEDENTARISMO
        /// <summary>Atributo, indica si practica deporte</summary>
        private int _PracticaDeporte;
        /// <summary>Atributo, Indica cuantas veces practica deporte a la semana</summary>
        private int _PracticaDeporteSemana;
        /// <summary>Atributo, Indica el Promedio de tiempo en minutos en cada sesión que practica deporte</summary>
        private int _PromedioTiempoMinutos;
        /// <summary>Atributo, Indica el tipo actividad física</summary>
        private int _TipoActividadFisica;
        /// <summary>Atributo, indica cuantas horas ve diarias en promedio de televisión</summary>
        private int _HorasDiariasTV;
        /// <summary>Atributo, indica cuantas si hizo deporte el ultimo mes</summary>
        private int _rutinaEjercicioUltimoMes;
        /// <summary>Atributo, indica la razon por que no hizo deporte el ultimo mes</summary>
        private int _NoRutinaEjercicio;
        /// <summary>Atributo, indica otra razon por que no hizo deporte el ultimo mes</summary>
        private string _OtroMotivo;


        #endregion

        #region SALUD ORAL
        /// <summary>Atributo, Indica si ha asistido a consulta odontológica en el último año</summary>
        private int _ConsultaOdontologica;
        /// <summary>Atributo, Indica cuantas veces se lava los dientes al día</summary>
        private int _LavaDientes;
        /// <summary>Atributo, Indica el uso de hilo dental todos los días</summary>
        private int _SedaDental;
        #endregion

        #region ESTRES
        /// <summary>Atributo, Indica si se ha sentido decaído (a), deprimido (a) o estresado (a) de manera persistente</summary>
        private int _SentidoDecaido;
        /// <summary>Atributo, Indica si se ha sentido Estresado en los ultimos 3 meses</summary>
        private int _SentidoEstresado;
        /// <summary>Atributo, Indica si has tenido poco interés o placer al hacer las cosas</summary>
        private int _InteresPlacer;
        /// <summary>Atributo, Indica el nivel de estres</summary>
        private int _NivelEstres;
        /// <summary>Atributo, Indica el plan para controlar el estrés</summary>
        private int _ControlarEstres;
        /// <summary>Atributo, Indica el interes por hacer las cosas</summary>
        private int _PocoInteres;
        /// <summary>Atributo, Indica si se ha sentido decaido ultimamente</summary>
        private int _SinEsperanza;


        #endregion

        #region EMOCIONAL
        /// <summary>Atributo, Indica cómo calificarías la calidad general de tu sueño</summary>
        private int _CalificacionSueno;
        /// <summary>Atributo, Indica el estado Después de una noche habitual de sueño</summary>
        private int _EstadoLevantarse;
        /// <summary>Atributo, Indica el estado Después de una noche habitual de sueño en el ultimo mes</summary>
        private int _DormidoSuficiente;
        /// <summary>Atributo, Indica cuantas horas duerme regularmente</summary>
        private int _HorasDuermeRegular;
        /// <summary>Atributo, Indica Cómo califica su estado de ánimo emocional</summary>
        private int _EstadoAnimoEmocional;
        #endregion

        #region ALIMENTACION INADECUADA

        /// <summary>Atributo, Indica las porciones de frutas que consume</summary>
        private int _PorcionesFrutas;
        /// <summary>Atributo, Indica las porciones de vegetales que consume</summary>
        private int _PorcionesVegetales;
        /// <summary>Atributo, Indica la frecuencia que consume alimetos no sanos</summary>
        private int _FrecuenciaCarne;
        /// <summary>Atributo, Indica la frecuencia que consume alimetos sanos</summary>
        private int _FrecuenciaSano;
        /// <summary>Atributo, Indica las porciones de granos que consume</summary>
        private int _FrecuenciaGranos;
        /// <summary>Atributo, Indica las porciones de azucar que consume</summary>
        private int _FrecuenciaAzucar;
        /// <summary>Atributo, Indica las porciones de sodio que consume</summary>
        private int _FrecuenciaSodio;

        #endregion

        #region COMPORTAMIENTOS DE RIESGO Y ACCIDENTALIDAD
        /// <summary>Atributo, Indica si se Utiliza el Cinturón de seguridad</summary>
        private int _CinturonSeguridad;
        /// <summary>Atributo, Indica si Cuándo conduce el coche utiliza el celular con manos libres</summary>
        private int _CocheCelular;
        /// <summary>Atributo, Indica Qué tan cerca del límite de velocidad conduces generalmente</summary>
        private int _LimiteVelocidad;
        /// <summary>Atributo, Indica Con qué frecuencia en el último mes has manejado o viajado en un vehículo en el que posiblemente el conductor había bebido demasiado</summary>
        private int _ConductorBebido;
        /// <summary>Atributo, Indica Con qué frecuencia usas un casco cuando paseas en bicicleta o motocicleta</summary>
        private int _Casco;
        /// <summary>Atributo, Indica Con qué frecuencia usas filtro solar con factor de protección 15 o mayor cuando pasas tiempo al sol</summary>
        private int _FiltroSolar;
        /// <summary>Atributo, Indica si Has realizado alguna revisión de seguridad doméstica en los seis meses anteriores</summary>
        private int _SeguridadDomestica;
        /// <summary>Propiedad, Indica las medidas de protección adecuadas frente al riesgo de contraer enfermedades de transmisión sexual</summary>
        private int _TrasmisionSexual;

        #endregion

        #region PERCEPCIÓN DEL ESTADO DE SALUD
        /// <summary>Atributo, Indica Cómo califica su estado de salud en términos generales</summary>
        private int _EstadoSalud;
        /// <summary>Atributo, Indica En general que tan dispuesto está a modificar sus hábitos de vida como son actividad física, dejar de fumar y un programa de educación en salud</summary>
        private int _HabitosVida;
        #endregion

        #region ANTECEDENTES AUSENTISMO
        /// <summary>Atributo, Indica si ha estado incapacitado en el último año</summary>
        private int _Incapacitado;
        /// <summary>Atributo, Indica el diagnóstico CIE10 de incapacidad</summary>
        private int _IdDiagnosticoIncapacidad;
        /// <summary>Atributo, Cantidad de días de incapacidad</summary>
        private int _DiasIncapacidad;
        /// <summary>Atributo, Indica el dianóstico CIE 10 de hospitalización</summary>
        private int _IdDiagnosticoHospitalizacion1;
        /// <summary>Atributo, Indica la fecha de hospitalización</summary>
        private DateTime _FechaHospitalizacion1;
        /// <summary>Atributo, Indica los días hospitalizados</summary>
        private int _DiasHospitalizacion1;
        /// <summary>Atributo, Indica el dianóstico CIE 10 de hospitalización</summary>
        private int _IdDiagnosticoHospitalizacion2;
        /// <summary>Atributo, Indica la fecha dehospitalización</summary>
        private DateTime _FechaHospitalizacion2;
        /// <summary>Atributo, Indica los días hospitalizados</summary>
        private int _DiasHospitalizacion2;
        /// <summary>Atributo, Indica el dianóstico CIE 10 de hospitalización</summary>
        private int _IdDiagnosticoHospitalizacion3;
        /// <summary>Atributo, Indica la fecha de hospitalización</summary>
        private DateTime _FechaHospitalizacion3;
        /// <summary>Atributo, Indica los días hospitalizados</summary>
        private int _DiasHospitalizacion3;
        /// <summary>Atributo, Indica el dianóstico CIE 10 de hospitalización</summary>
        private int _IdDiagnosticoHospitalizacion4;
        /// <summary>Atributo, Indica la fecha de hospitalización</summary>
        private DateTime _FechaHospitalizacion4;
        /// <summary>Atributo, Indica los días hospitalizados</summary>
        private int _DiasHospitalizacion4;

        #endregion

        #region NUTRICIÓN
        /// <summary>Atributo, Indica si consume desayuno</summary>
        private int _Desayuno;
        /// <summary>Atributo, Indica hora de desayuno</summary>
        private short _DesayunoHora;
        /// <summary>Atributo, Indica si consume almuerzo</summary>
        private int _Almuerzo;
        /// <summary>Atributo, Indica hora de almuerzo</summary>
        private short _AlmuerzoHora;
        /// <summary>Atributo, Indica si consume comida</summary>
        private int _Comida;
        /// <summary>Atributo, Indica hora de comida</summary>
        private short _ComidaHora;
        /// <summary>Atributo, Indica si consume entremes</summary>
        private int _Entremes;
        /// <summary>Atributo, Indica hora de entremes</summary>
        private short _EntremesHora;
        /// <summary>Atributo, Indica si consume cena</summary>
        private int _Cena;
        /// <summary>Atributo, Indica hora de cena</summary>
        private short _CenaHora;
        /// <summary>Atributo, Indica el Diámetro de la Cintura</summary>
        private decimal _DiametroCintura;
        /// <summary>Atributo, Indica el Diámetro de la cadera</summary>
        private decimal _DiametroCadera;
        /// <summary>Atributo, Indica la relación entre el Diámetro de la cadera y el Diámetro de la cintura</summary>
        private decimal _RelacionCinturaCadera;
        /// <summary>Atributo, Descripcion de la relación entre cintura y cadera</summary>
        private int _DescripcionRelacion;
        /// <summary>Atributo, valor de la masa grasa</summary>
        private decimal _MasaGrasa;
        /// <summary>Atributo, valor de la masa grama</summary>
        private decimal _MasaGrama;
        /// <summary>Atributo, Valor peso recomendable</summary>
        private decimal _PesoRecomendable;
        /// <summary>Atributo, Valor excedente grasa</summary>
        private decimal _ExcedenteGrasa;
        /// <summary>Atributo, Indica el tipo de delgadez</summary>
        private int _DiagnosticoNutricional;
        /// <summary>Atributo, Indica el id del diagnostico nutricional CIE 10</summary>
        private int _IdDiagnosticoNutricional;
        /// <summary>Atributo, Guarda las recomendaciones nutricionales</summary>
        private string _RecomendacionesNutricionales;
        /// <summary>Atributo, Indica los planes para alimentación saludable</summary>
        private int _AlimentacionSaludable;
        /// <summary>Atributo, Peso hace seis meses</summary>
        private decimal _PesoHace6Meses;
        /// <summary>Atributo, Consideras que en tu peso, ha habido una fluctuación mayor al 10% en los últimos dos años</summary>
        private int _PesoFluctuacion;
        /// <summary>Atributo, Cómo consideras que es tu apetito</summary>
        private int _ConsideracionApetito;
        /// <summary>Atributo, Con que frecuencia existe eliminación intestinal</summary>
        private int _EliminacionIntestinal;
        /// <summary>Atributo, Eres intolerante a algún alimento</summary>
        private int _IntoranciaAlimento;
        /// <summary>Atributo, descripcion de intolerancia a algún alimento</summary>
        private string _IntoranciaAlimentoEspecificacion;
        /// <summary>Atributo, Padeces alergia (s) con algún alimento</summary>
        private int _AlergiaAlimento;
        /// <summary>Atributo, Descripción de padecer alergia (s) con algún alimento</summary>
        private string _AlergiaAlimentoEspecificacion;
        /// <summary>Atributo, Lugar donde toma desayuno</summary>
        private int _DesayunoLugar;
        /// <summary>Atributo, Lugar donde toma almuerzo</summary>
        private int _AlmuerzoLugar;
        /// <summary>Atributo, Lugar donde toma comida</summary>
        private int _ComidaLugar;
        /// <summary>Atributo, Lugar donde toma entremes</summary>
        private int _EntremesLugar;
        /// <summary>Atributo, Lugar donde toma la cena</summary>
        private int _CenaLugar;
        /// <summary>Atributo, Reconocer cuando estás satisfecho</summary>
        private int _EstarSatisfecho;
        /// <summary>Atributo, Creer que te satisfaces con facilidad</summary>
        private int _SatisfaccionFacilidad;
        /// <summary>Atributo, Reconocer cuando tienes hambre</summary>
        private int _ReconocerHambre;
        /// <summary>Atributo, Acostumbrar a comer despacio</summary>
        private int _ComerDespacio;
        /// <summary>Atributo, A qué hora del día sientes mayor apetito</summary>
        private short _MayorApetitoHora;
        /// <summary>Atributo, A qué hora del día sientes antojos</summary>
        private short _AntojosHora;
        /// <summary>Atributo, En el último año te has sometido a alguna dieta</summary>
        private int _SometidoDieta;
        /// <summary>Atributo, Actualmente llevas a cabo alguna dieta</summary>
        private int _LlevasDieta;
        /// <summary>Atributo, Por quién fue prescrita</summary>
        private int _QuienPrescribe;
        /// <summary>Atributo, Cuál fue la principal razón que te motivó a iniciar una dieta</summary>
        private int _MotivoIniciarDieta;
        /// <summary>Atributo, Comparando con la dieta que llevabas , ¿cómo consideras que es la ingestión actual de tus alimentos?</summary>
        private int _IngestionAlimentos;
        /// <summary>Atributo, En caso de consumir algún complemento para bajar de peso, por quién fue prescrito</summary>
        private int _BajarPesoPrescrito;
        /// <summary>Atributo, Descripcion bajo peso prescrito</summary>
        private string _BajarPesoPrescritoEspecificacion;
        /// <summary>Atributo, Has padecido de algún trastorno de alimentación</summary>
        private int _TrastornoAlimentacion;
        /// <summary>Atributo, id del Trastorno</summary>
        private int _IdDiagnosticoTrastorno;
        /// <summary>Atributo, Hace cuánto tiempo lo padeciste</summary>
        private int _PadecerTrastorno;
        /// <summary>Atributo, A qué hora acostumbra levantarse entre semana</summary>
        private short _LevantarseEntreSemana;
        /// <summary>Atributo, A qué hora acostumbra salir de casa fin de semana</summary>
        private short _LevantarseFinDeSemana;
        /// <summary>Atributo, A qué hora acostumbra salir de casa entre semana</summary>
        private short _SalirCasaEntreSemana;
        /// <summary>Atributo, A qué hora acostumbra salir de casa fin de semana</summary>
        private short _SalirCasaFinDeSemana;
        /// <summary>Atributo, A qué hora acostumbra acostarse entre semana</summary>
        private short _AcostarseEntreSemana;
        /// <summary>Atributo, A qué hora acostumbra acostarse fin de semana</summary>
        private short _AcostarseFinDeSemana;
        /// <summary>Atributo, En promedio, que tan frecuentemente compras comida rápida</summary>
        private int _ComidaRapida;
        /// <summary>Atributo, Aproximadamente cuántos vasos de agua consumes al día</summary>
        private int _VasosAgua;
        #endregion

        #region ESTAMOS CONTIGO

        /// <summary>Atributo, Indica si se tomo la presion arterial en los ultimos 30 días </summary>
        private int _PresionArterial30dias;
        /// <summary>Atributo, Indica la fecha en la se tomo la presion arterial en los ultimos 30 días</summary>
        private string _FechaPresionArterial30dias;
        /// <summary>Atributo, Indica el valor de la presion arterial en los ultimos 30 días</summary>
        private int _ValorPresionArterial30dias;
        /// <summary>Atributo, Indica si se tomo la glucosa en los ultimos 30 días</summary>
        private int _Glucosa30dias;
        /// <summary>Atributo, Indica la fecha en la se tomo la glucosa en los ultimos 30 días</summary>
        private string _FechaGlucosa30dias;
        /// <summary>Atributo, Indica el valor de la glucosa en los ultimos 30 días</summary>
        private int _ValorGlucosa30dias;
        /// <summary>Atributo, Indica si se tomo el colesterol total en los ultimos 30 días</summary>
        private int _ColesterolTotal30Dias;
        /// <summary>Atributo, Indica la fecha en la se tomo el colesterol total en los ultimos 30 días</summary>
        private string _FechaColesterolTotal30Dias;
        /// <summary>Atributo, Indica el valor de el colesterol total en los ultimos 30 días</summary>
        private int _ValorColesterolTotal30Dias;
        /// <summary>Atributo, Indica si se tomo el colesterol HDL en los ultimos 30 días</summary>
        private int _ColesterolHDL30Dias;
        /// <summary>Atributo, Indica la fecha en la se tomo el colesterol HDL en los ultimos 30 días</summary>
        private string _FechaColesterolHDL30Dias;
        /// <summary>Atributo, Indica el valor de el colesterol HDL en los ultimos 30 días</summary>
        private int _ValorColesterolHDL30Dias;
        /// <summary>Atributo, Indica si se tomo el colesterol LDL en los ultimos 30 días</summary>
        private int _ColesterolLDL30Dias;
        /// <summary>Atributo, Indica la fecha en la se tomo el colesterol LDL en los ultimos 30 días</summary>
        private string _FechaColesterolLDL30Dias;
        /// <summary>Atributo, Indica el valor de el colesterol LDL en los ultimos 30 días</summary>
        private int _ValorColesterolLDL30Dias;
        /// <summary>Atributo, Indica si se tomo los trigliceridos en los ultimos 30 días</summary>
        private int _Trigliceridos30Dias;
        /// <summary>Atributo, Indica la fecha en la se tomo los trigliceridos en los ultimos 30 días</summary>
        private string _FechaTrigliceridos30Dias;
        /// <summary>Atributo, Indica el valor de los trigliceridos en los ultimos 30 días</summary>
        private int _ValorTrigliceridos30DiasHombres;
        /// <summary>Atributo, Indica el valor los trigliceridos en los ultimos 30 días</summary>
        private int _ValorTrigliceridos30DiasMujeres;

        #endregion

        #endregion

        #region Properties

        #region GENERAL
        /// <summary>Propiedad, Id de la solicitud</summary>
		public long IdSolicitud
        {
            get { return _IdSolicitud; }
            set { _IdSolicitud = value; }
        }
        /// <summary>Propiedad, Id de la consulta</summary>
        public long IdConsulta
        {
            get { return _IdConsulta; }
            set { _IdConsulta = value; }
        }
        /// <summary>Propiedad, Id de la cita asociada a la consulta</summary>
        public int cita_id
        {
            get { return _cita_id; }
            set { _cita_id = value; }
        }
        /// <summary>Propiedad, Id de la empresa en SICAU</summary>
        public int Empresa_id
        {
            get { return _Empresa_id; }
            set { _Empresa_id = value; }
        }
        /// <summary>Propiedad, Id del empleado al que se le realiza la consulta</summary>
        public int Id_empleado
        {
            get { return _Id_empleado; }
            set { _Id_empleado = value; }
        }
        /// <summary>Propiedad, Id del beneficiario al que se realiza la consulta</summary>
        public int Beneficiario_id
        {
            get { return _Beneficiario_id; }
            set { _Beneficiario_id = value; }
        }
        /// <summary>Propiedad, Id del prestador o solicitante que realiza la consulta</summary>
        public int IdPrestador
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }
        /// <summary>Propiedad, Id del tipo de consulta</summary>
        public short IdTipoConsulta
        {
            get { return _IdTipoConsulta; }
            set { _IdTipoConsulta = value; }
        }
        /// <summary>Propiedad, Id del tipo de enfermedad</summary>
        public short IdTipoEnfermedad
        {
            get { return _IdTipoEnfermedad; }
            set { _IdTipoEnfermedad = value; }
        }
        /// <summary>Propiedad, Motivo de la consulta</summary>
        public string Motivo
        {
            get { return _Motivo; }
            set { _Motivo = value; }
        }
        /// <summary>Propiedad, Contrarreferencia de la consulta</summary>
        public string Contrarreferencia
        {
            get { return _Contrarreferencia; }
            set { _Contrarreferencia = value; }
        }
        /// <summary>Propiedad, Texto de enfermedad actual</summary>
        public string EnfermedadActual
        {
            get { return _EnfermedadActual; }
            set { _EnfermedadActual = value; }
        }
        /// <summary>Propiedad, </summary>
        public string ObservacionesGenerales
        {
            get { return _ObservacionesGenerales; }
            set { _ObservacionesGenerales = value; }
        }
        /// <summary>Propiedad, </summary>
        public string PlanTratamiento
        {
            get { return _PlanTratamiento; }
            set { _PlanTratamiento = value; }
        }
        /// <summary>Propiedad, </summary>
        public DateTime CitaControl
        {
            get { return _CitaControl; }
            set { _CitaControl = value; }
        }
        /// <summary>Propiedad, Fecha de creación</summary>
        public DateTime FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }
        /// <summary>Propiedad, Fecha exacta de cuando se ingresa al primer paso de la consulta</summary>
        public DateTime FechaInicioCreacion
        {
            get { return _FechaInicioCreacion; }
            set { _FechaInicioCreacion = value; }
        }
        /// <summary>Propiedad, Fecha fin exacta de cuando se guarda el último paso de la consulta</summary>
        public DateTime FechaFinCreacion
        {
            get { return _FechaFinCreacion; }
            set { _FechaFinCreacion = value; }
        }
        /// <summary>Propiedad, Fecha de la última modificación</summary>
        public DateTime FechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }
        /// <summary>Propiedad, Id del usuario de creación</summary>
        public int IdUserCreacion
        {
            get { return _IdUserCreacion; }
            set { _IdUserCreacion = value; }
        }
        /// <summary>Propiedad, Id del usuario de creación en SICAU</summary>
        public int Usuario_idCreacion
        {
            get { return _Usuario_idCreacion; }
            set { _Usuario_idCreacion = value; }
        }
        /// <summary>Propiedad, Id de la solicitud en el sistema SICAU para cierre del caso</summary>
        public int Id_solicitud_SICAU
        {
            get { return _Id_solicitud_SICAU; }
            set { _Id_solicitud_SICAU = value; }
        }
        /// <summary>Propiedad, Número consecutivo de la consulta</summary>
        public int Consecutivo
        {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }
        /// <summary>Propiedad, Consecutivo de la consulta</summary>
        public string ConsecutivoNombre
        {
            get { return _ConsecutivoNombre; }
            set { _ConsecutivoNombre = value; }
        }
        /// <summary>Propiedad, Nombre del usuario</summary>
        public string NameUser
        {
            get { return _NameUser; }
            set { _NameUser = value; }
        }
        /// <summary>Propiedad, Arreglo que contiene los diagnosticos de la consulta</summary>
        public ArrayList ConsultaDiagnosticos
        {
            get { return _ConsultaDiagnosticos; }
            set { _ConsultaDiagnosticos = value; }
        }
        /// <summary>Propiedad, Id del proveedore que solicita la transcripción</summary>
        public int IdProveedorTranscripcion
        {
            get { return _IdProveedorTranscripcion; }
            set { _IdProveedorTranscripcion = value; }
        }
        /// <summary>Propiedad, Comentarios si la consulta es una transcripción</summary>
        public string ComentariosTranscripcion
        {
            get { return _ComentariosTranscripcion; }
            set { _ComentariosTranscripcion = value; }
        }
        /// <summary>Propiedad, Campo para escribir resultados de exámenes de laboratorio</summary>
        public string ExamenesLaboratorio
        {
            get { return _ExamenesLaboratorio; }
            set { _ExamenesLaboratorio = value; }
        }
        /// <summary>Propiedad, Arreglo que contiene los diagnosticos de la consulta</summary>
        public ArrayList ConsultaOpcion
        {
            get { return _ConsultaOpcion; }
            set { _ConsultaOpcion = value; }
        }

        /// <summary>Propiedad, Arreglo que contiene los diagnosticos de la consulta</summary>
        public bool Finalizada
        {
            get { return _Finalizada; }
            set { _Finalizada = value; }
        }

        /// <summary>Propiedad, Id de la sede de la consulta</summary>
        public int sede_id
        {
            get { return _sede_id; }
            set { _sede_id = value; }
        }
        /// <summary>Propiedad, Motivo de la consulta</summary>
        public string NombrePrestador
        {
            get { return _NombrePrestador; }
            set { _NombrePrestador = value; }
        }
        /// <summary>Propiedad, Id de la línea de negocio del empleado</summary>
        public int IdLineaNegocio
        {
            get { return _IdLineaNegocio; }
            set { _IdLineaNegocio = value; }
        }

        #endregion

        #region ANTECEDENTES
        //ANTECEDENTES
        /// <summary>Propiedad, Antecedentes médicos</summary>
        public string Medicos
        {
            get { return _Medicos; }
            set { _Medicos = value; }
        }
        /// <summary>Propiedad, Antecedentes Quirúrgicos</summary>
        public string Quirurgicos
        {
            get { return _Quirurgicos; }
            set { _Quirurgicos = value; }
        }
        /// <summary>Propiedad, Antecedentes Ginecobstétricos</summary>
        public string Ginecobstetricos
        {
            get { return _Ginecobstetricos; }
            set { _Ginecobstetricos = value; }
        }
        /// <summary>Propiedad, Antecedentes transfusionales</summary>
        public string Transfusionales
        {
            get { return _Transfusionales; }
            set { _Transfusionales = value; }
        }
        /// <summary>Propiedad, Antecedentes toxico alérgicos</summary>
        public string ToxicoAlergicos
        {
            get { return _ToxicoAlergicos; }
            set { _ToxicoAlergicos = value; }
        }
        /// <summary>Propiedad, Antecedentes farmacologicos</summary>
        public string Farmacologicos
        {
            get { return _Farmacologicos; }
            set { _Farmacologicos = value; }
        }
        /// <summary>Propiedad, Antecedentes familiares</summary>
        public string Familiares
        {
            get { return _Familiares; }
            set { _Familiares = value; }
        }
        /// <summary>Propiedad, Otros antecedentes</summary>
        public string OtrosAntecedentes
        {
            get { return _OtrosAntecedentes; }
            set { _OtrosAntecedentes = value; }
        }
        /// <summary>Propiedad, Menarquia</summary>
        public string Menarquia
        {
            get { return _Menarquia; }
            set { _Menarquia = value; }
        }
        /// <summary>Propiedad, </summary>
        public string FechaUltimaMestruacion
        {
            get { return _FechaUltimaMestruacion; }
            set { _FechaUltimaMestruacion = value; }
        }
        /// <summary>Propiedad, </summary>
        public short Gestaciones
        {
            get { return _Gestaciones; }
            set { _Gestaciones = value; }
        }
        /// <summary>Propiedad, </summary>
        public short Partos
        {
            get { return _Partos; }
            set { _Partos = value; }
        }
        /// <summary>Propiedad, </summary>
        public short Cesareas
        {
            get { return _Cesareas; }
            set { _Cesareas = value; }
        }
        /// <summary>Propiedad, </summary>
        public short Abortos
        {
            get { return _Abortos; }
            set { _Abortos = value; }
        }
        /// <summary>Propiedad, </summary>
        public short Vivos
        {
            get { return _Vivos; }
            set { _Vivos = value; }
        }
        /// <summary>Propiedad, Indica estado normal de antecedentes médicos</summary>
        public bool NormalMedicos
        {
            get { return _NormalMedicos; }
            set { _NormalMedicos = value; }
        }
        /// <summary>Propiedad, Indica estado normal de antecedentes quirúrgicos</summary>
        public bool NormalQuirurgicos
        {
            get { return _NormalQuirurgicos; }
            set { _NormalQuirurgicos = value; }
        }
        /// <summary>Propiedad, Indica estado normal de antecedentes ginecobstetricos</summary>
        public bool NormalGinecobstetricos
        {
            get { return _NormalGinecobstetricos; }
            set { _NormalGinecobstetricos = value; }
        }
        /// <summary>Propiedad, Indica estado normal de antecedentes transfusionales</summary>
        public bool NormalTransfusionales
        {
            get { return _NormalTransfusionales; }
            set { _NormalTransfusionales = value; }
        }
        /// <summary>Propiedad, Indica estado normal de antecedentes toxico alergicos</summary>
        public bool NormalToxicoAlergicos
        {
            get { return _NormalToxicoAlergicos; }
            set { _NormalToxicoAlergicos = value; }
        }
        /// <summary>Propiedad, Indica estado normal de antecedentes farmacologicos</summary>
        public bool NormalFarmacologicos
        {
            get { return _NormalFarmacologicos; }
            set { _NormalFarmacologicos = value; }
        }
        /// <summary>Propiedad, Indica estado normal de antecedentes familiares</summary>
        public bool NormalFamiliares
        {
            get { return _NormalFamiliares; }
            set { _NormalFamiliares = value; }
        }
        /// <summary>Propiedad, Indica estad normal de otros antecedentes</summary>
        public bool NormalOtrosAntecedentes
        {
            get { return _NormalOtrosAntecedentes; }
            set { _NormalOtrosAntecedentes = value; }
        }
        /// <summary>Propiedad, Indica riesgo cardiovascular</summary>
        public bool RiesgoCardiovascular
        {
            get { return _RiesgoCardiovascular; }
            set { _RiesgoCardiovascular = value; }
        }

        #endregion

        #region EXAMEN FÍSICO
        //EXAMEN FÍSICO
        /// <summary>Propiedad, Peso del paciente</summary>
        public decimal Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }
        /// <summary>Propiedad, Talla del peciente</summary>
        public decimal Talla
        {
            get { return _Talla; }
            set { _Talla = value; }
        }
        /// <summary>Propiedad, Indice de Masa Corporal del paciente</summary>
        public decimal IndiceMasaCorporal
        {
            get { return _IndiceMasaCorporal; }
            set { _IndiceMasaCorporal = value; }
        }
        /// <summary>Propiedad, Tensión arterial del paciente</summary>
        public string TensionArterial
        {
            get { return _TensionArterial; }
            set { _TensionArterial = value; }
        }
        /// <summary>Propiedad, Tensión arterial sistolica del paciente</summary>
        public string TensionArterialSistolica
        {
            get { return _TensionArterialSistolica; }
            set { _TensionArterialSistolica = value; }
        }
        /// <summary>Propiedad, Tensión arterial diastolica del paciente</summary>
        public string TensionArterialDiastolica
        {
            get { return _TensionArterialDiastolica; }
            set { _TensionArterialDiastolica = value; }
        }
        /// <summary>Propiedad, Frecuencia cardiaca del paciente</summary>
        public int FrecuenciaCardiaca
        {
            get { return _FrecuenciaCardiaca; }
            set { _FrecuenciaCardiaca = value; }
        }
        /// <summary>Propiedad, Frecuencia respiratoria del paciente</summary>
        public int FrecuenciaRespiratoria
        {
            get { return _FrecuenciaRespiratoria; }
            set { _FrecuenciaRespiratoria = value; }
        }
        /// <summary>Propiedad, Perimetro abdominal del paciente</summary>
        public decimal PerimetroAbdominal
        {
            get { return _PerimetroAbdominal; }
            set { _PerimetroAbdominal = value; }
        }
        /// <summary>Propiedad, Comentarios generales del exámen físico</summary>
        public string ComentariosExamenFisico
        {
            get { return _ComentariosExamenFisico; }
            set { _ComentariosExamenFisico = value; }
        }
        /// <summary>Propiedad, Aspecto general</summary>
        public string ExamenAspectoGeneral
        {
            get { return _ExamenAspectoGeneral; }
            set { _ExamenAspectoGeneral = value; }
        }
        /// <summary>Propiedad, Cabeza</summary>
        public string ExamenCabeza
        {
            get { return _ExamenCabeza; }
            set { _ExamenCabeza = value; }
        }
        /// <summary>Propiedad, Cuello</summary>
        public string ExamenCuello
        {
            get { return _ExamenCuello; }
            set { _ExamenCuello = value; }
        }
        /// <summary>Propiedad, Torax</summary>
        public string ExamenTorax
        {
            get { return _ExamenTorax; }
            set { _ExamenTorax = value; }
        }
        /// <summary>Propiedad, Abdomen</summary>
        public string ExamenAbdomen
        {
            get { return _ExamenAbdomen; }
            set { _ExamenAbdomen = value; }
        }
        /// <summary>Propiedad, Otros sistemas</summary>
        public string ExamenOtros
        {
            get { return _ExamenOtros; }
            set { _ExamenOtros = value; }
        }
        /// <summary>Propiedad, Indica estado normal de aspecto general</summary>
        public bool ExamenNormalAspectoGeneral
        {
            get { return _ExamenNormalAspectoGeneral; }
            set { _ExamenNormalAspectoGeneral = value; }
        }
        /// <summary>Propiedad, Indica estado normal de cabeza</summary>
        public bool ExamenNormalCabeza
        {
            get { return _ExamenNormalCabeza; }
            set { _ExamenNormalCabeza = value; }
        }
        /// <summary>Propiedad, Indica estado normal de cuello</summary>
        public bool ExamenNormalCuello
        {
            get { return _ExamenNormalCuello; }
            set { _ExamenNormalCuello = value; }
        }
        /// <summary>Propiedad, Indica estado normal de torax</summary>
        public bool ExamenNormalTorax
        {
            get { return _ExamenNormalTorax; }
            set { _ExamenNormalTorax = value; }
        }
        /// <summary>Propiedad, Indica estado normal de abdomen</summary>
        public bool ExamenNormalAbdomen
        {
            get { return _ExamenNormalAbdomen; }
            set { _ExamenNormalAbdomen = value; }
        }
        /// <summary>Propiedad, Indica estado normal de otros</summary>
        public bool ExamenNormalOtros
        {
            get { return _ExamenNormalOtros; }
            set { _ExamenNormalOtros = value; }
        }
        /// <summary>Propiedad, Temperatura del paciente</summary>
        public decimal Temperatura
        {
            get { return _Temperatura; }
            set { _Temperatura = value; }
        }
        /// <summary>Propiedad, Examen de piel y fanelas</summary>
        public string ExamenPielFanelas
        {
            get { return _ExamenPielFanelas; }
            set { _ExamenPielFanelas = value; }
        }
        /// <summary>Propiedad, Indica si piel y fanelas es normal</summary>
        public bool ExamenNormalPielFanelas
        {
            get { return _ExamenNormalPielFanelas; }
            set { _ExamenNormalPielFanelas = value; }
        }
        /// <summary>Propiedad, Examen de conjuntiva ocular</summary>
        public string ExamenConjuntivaOcular
        {
            get { return _ExamenConjuntivaOcular; }
            set { _ExamenConjuntivaOcular = value; }
        }
        /// <summary>Propiedad, Indica si conjuntiva ocular es normal</summary>
        public bool ExamenNormalConjuntivaOcular
        {
            get { return _ExamenNormalConjuntivaOcular; }
            set { _ExamenNormalConjuntivaOcular = value; }
        }
        /// <summary>Propiedad, Examen de reflejo corneal</summary>
        public string ExamenReflejoCorneal
        {
            get { return _ExamenReflejoCorneal; }
            set { _ExamenReflejoCorneal = value; }
        }
        /// <summary>Propiedad, Indica si reflejo corneal es normal</summary>
        public bool ExamenNormalReflejoCorneal
        {
            get { return _ExamenNormalReflejoCorneal; }
            set { _ExamenNormalReflejoCorneal = value; }
        }
        /// <summary>Propiedad, Examen de pupilas</summary>
        public string ExamenPupilas
        {
            get { return _ExamenPupilas; }
            set { _ExamenPupilas = value; }
        }
        /// <summary>Propiedad, Indica si pupilas es normal</summary>
        public bool ExamenNormalPupilas
        {
            get { return _ExamenNormalPupilas; }
            set { _ExamenNormalPupilas = value; }
        }
        /// <summary>Propiedad, Examen de oídos</summary>
        public string ExamenOidos
        {
            get { return _ExamenOidos; }
            set { _ExamenOidos = value; }
        }
        /// <summary>Propiedad, Indica si oidos es normal</summary>
        public bool ExamenNormalOidos
        {
            get { return _ExamenNormalOidos; }
            set { _ExamenNormalOidos = value; }
        }
        /// <summary>Propiedad, Examen de otoscopia</summary>
        public string ExamenOtoscopia
        {
            get { return _ExamenOtoscopia; }
            set { _ExamenOtoscopia = value; }
        }
        /// <summary>Propiedad, Indica si otoscopia es normal</summary>
        public bool ExamenNormalOtoscopia
        {
            get { return _ExamenNormalOtoscopia; }
            set { _ExamenNormalOtoscopia = value; }
        }
        /// <summary>Propiedad, Examen de rinoscopia</summary>
        public string ExamenRinoscopia
        {
            get { return _ExamenRinoscopia; }
            set { _ExamenRinoscopia = value; }
        }
        /// <summary>Propiedad, Indica si rinoscopia es normal</summary>
        public bool ExamenNormalRinoscopia
        {
            get { return _ExamenNormalRinoscopia; }
            set { _ExamenNormalRinoscopia = value; }
        }
        /// <summary>Propiedad, Examen de boca y faringe</summary>
        public string ExamenBocaFaringe
        {
            get { return _ExamenBocaFaringe; }
            set { _ExamenBocaFaringe = value; }
        }
        /// <summary>Propiedad, Indica si boca y faringe es normal</summary>
        public bool ExamenNormalBocaFaringe
        {
            get { return _ExamenNormalBocaFaringe; }
            set { _ExamenNormalBocaFaringe = value; }
        }
        /// <summary>Propiedad, Examen de amigdalas</summary>
        public string ExamenAmigdalas
        {
            get { return _ExamenAmigdalas; }
            set { _ExamenAmigdalas = value; }
        }
        /// <summary>Propiedad, Indica si amgdalas es normal</summary>
        public bool ExamenNormalAmigdalas
        {
            get { return _ExamenNormalAmigdalas; }
            set { _ExamenNormalAmigdalas = value; }
        }
        /// <summary>Propiedad, Examen de tiroides</summary>
        public string ExamenTiroides
        {
            get { return _ExamenTiroides; }
            set { _ExamenTiroides = value; }
        }
        /// <summary>Propiedad, Indica si tiroides es normal</summary>
        public bool ExamenNormalTiroides
        {
            get { return _ExamenNormalTiroides; }
            set { _ExamenNormalTiroides = value; }
        }
        /// <summary>Propiedad, Examen de adenopatias</summary>
        public string ExamenAdenopatias
        {
            get { return _ExamenAdenopatias; }
            set { _ExamenAdenopatias = value; }
        }
        /// <summary>Propiedad, Indica si adenopatias es normal</summary>
        public bool ExamenNormalAdenopatias
        {
            get { return _ExamenNormalAdenopatias; }
            set { _ExamenNormalAdenopatias = value; }
        }
        /// <summary>Propiedad, Examen de ruidos cardiacos</summary>
        public string ExamenRuidosCardiacos
        {
            get { return _ExamenRuidosCardiacos; }
            set { _ExamenRuidosCardiacos = value; }
        }
        /// <summary>Propiedad, Indica si ruidos cardiacos es normal</summary>
        public bool ExamenNormalRuidosCardiacos
        {
            get { return _ExamenNormalRuidosCardiacos; }
            set { _ExamenNormalRuidosCardiacos = value; }
        }
        /// <summary>Propiedad, Examen de ruidos respiratorios</summary>
        public string ExamenRuidosRespiratorios
        {
            get { return _ExamenRuidosRespiratorios; }
            set { _ExamenRuidosRespiratorios = value; }
        }
        /// <summary>Propiedad, Indica si ruidos respiratorios es normal</summary>
        public bool ExamenNormalRuidosRespiratorios
        {
            get { return _ExamenNormalRuidosRespiratorios; }
            set { _ExamenNormalRuidosRespiratorios = value; }
        }
        /// <summary>Propiedad, Examen palpación de abdomen</summary>
        public string ExamenPalpacionAbdomen
        {
            get { return _ExamenPalpacionAbdomen; }
            set { _ExamenPalpacionAbdomen = value; }
        }
        /// <summary>Propiedad, Indica si palpación es normal</summary>
        public bool ExamenNormalPalpacionAbdomen
        {
            get { return _ExamenNormalPalpacionAbdomen; }
            set { _ExamenNormalPalpacionAbdomen = value; }
        }
        /// <summary>Propiedad, Examen de genitales externos</summary>
        public string ExamenGenitalesExternos
        {
            get { return _ExamenGenitalesExternos; }
            set { _ExamenGenitalesExternos = value; }
        }
        /// <summary>Propiedad, Indica si genitales externos es normal</summary>
        public bool ExamenNormalGenitalesExternos
        {
            get { return _ExamenNormalGenitalesExternos; }
            set { _ExamenNormalGenitalesExternos = value; }
        }
        /// <summary>Propiedad, Examen de hernias</summary>
        public string ExamenHernias
        {
            get { return _ExamenHernias; }
            set { _ExamenHernias = value; }
        }
        /// <summary>Propiedad, Indica si hermias es normal</summary>
        public bool ExamenNormalHernias
        {
            get { return _ExamenNormalHernias; }
            set { _ExamenNormalHernias = value; }
        }
        /// <summary>Propiedad, Examen de columna vertebral</summary>
        public string ExamenColumnaVertebral
        {
            get { return _ExamenColumnaVertebral; }
            set { _ExamenColumnaVertebral = value; }
        }
        /// <summary>Propiedad, Indica si columna vertebral es normal</summary>
        public bool ExamenNormalColumnaVertebral
        {
            get { return _ExamenNormalColumnaVertebral; }
            set { _ExamenNormalColumnaVertebral = value; }
        }
        /// <summary>Propiedad, Examen de extremidades superiores</summary>
        public string ExamenExtremidadesSuperiores
        {
            get { return _ExamenExtremidadesSuperiores; }
            set { _ExamenExtremidadesSuperiores = value; }
        }
        /// <summary>Propiedad, Indica si extremidades superiores es normal</summary>
        public bool ExamenNormalExtremidadesSuperiores
        {
            get { return _ExamenNormalExtremidadesSuperiores; }
            set { _ExamenNormalExtremidadesSuperiores = value; }
        }
        /// <summary>Propiedad, Examen extremidades inferiores</summary>
        public string ExamenExtremidadesInferiores
        {
            get { return _ExamenExtremidadesInferiores; }
            set { _ExamenExtremidadesInferiores = value; }
        }
        /// <summary>Propiedad, Indica si extremidades inferiores es normal</summary>
        public bool ExamenNormalExtremidadesInferiores
        {
            get { return _ExamenNormalExtremidadesInferiores; }
            set { _ExamenNormalExtremidadesInferiores = value; }
        }
        /// <summary>Propiedad, Examen de varices</summary>
        public string ExamenVarices
        {
            get { return _ExamenVarices; }
            set { _ExamenVarices = value; }
        }
        /// <summary>Propiedad, Indica si varices es normal</summary>
        public bool ExamenNormalVarices
        {
            get { return _ExamenNormalVarices; }
            set { _ExamenNormalVarices = value; }
        }
        /// <summary>Propiedad, Examen neurológico</summary>
        public string ExamenNeurologico
        {
            get { return _ExamenNeurologico; }
            set { _ExamenNeurologico = value; }
        }
        /// <summary>Propiedad, Indica si neurológico es normal</summary>
        public bool ExamenNormalNeurologico
        {
            get { return _ExamenNormalNeurologico; }
            set { _ExamenNormalNeurologico = value; }
        }
        #endregion

        #region REVISIÓN POR SISTEMAS
        //REVISIÓN POR SISTEMAS
        /// <summary>Propiedad, Aspecto general</summary>
        public string AspectoGeneral
        {
            get { return _AspectoGeneral; }
            set { _AspectoGeneral = value; }
        }
        /// <summary>Propiedad, Cabeza</summary>
        public string Cabeza
        {
            get { return _Cabeza; }
            set { _Cabeza = value; }
        }
        /// <summary>Propiedad, Cuello</summary>
        public string Cuello
        {
            get { return _Cuello; }
            set { _Cuello = value; }
        }
        /// <summary>Propiedad, Torax</summary>
        public string Torax
        {
            get { return _Torax; }
            set { _Torax = value; }
        }
        /// <summary>Propiedad, Abdomen</summary>
        public string Abdomen
        {
            get { return _Abdomen; }
            set { _Abdomen = value; }
        }
        /// <summary>Propiedad, Otros sistemas</summary>
        public string Otros
        {
            get { return _Otros; }
            set { _Otros = value; }
        }
        /// <summary>Propiedad, Indica estado normal de aspecto general</summary>
        public bool NormalAspectoGeneral
        {
            get { return _NormalAspectoGeneral; }
            set { _NormalAspectoGeneral = value; }
        }
        /// <summary>Propiedad, Indica estado normal de cabeza</summary>
        public bool NormalCabeza
        {
            get { return _NormalCabeza; }
            set { _NormalCabeza = value; }
        }
        /// <summary>Propiedad, Indica estado normal de cuello</summary>
        public bool NormalCuello
        {
            get { return _NormalCuello; }
            set { _NormalCuello = value; }
        }
        /// <summary>Propiedad, Indica estado normal de torax</summary>
        public bool NormalTorax
        {
            get { return _NormalTorax; }
            set { _NormalTorax = value; }
        }
        /// <summary>Propiedad, Indica estado normal de abdomen</summary>
        public bool NormalAbdomen
        {
            get { return _NormalAbdomen; }
            set { _NormalAbdomen = value; }
        }
        /// <summary>Propiedad, Indica estado normal de otros</summary>
        public bool NormalOtros
        {
            get { return _NormalOtros; }
            set { _NormalOtros = value; }
        }
        #endregion

        #region HABITOS
        //HABITOS
        /// <summary>Propiedad, Indica si consume tabaco</summary>
        public int Tabaquismo
        {
            get { return _Tabaquismo; }
            set { _Tabaquismo = value; }
        }
        /// <summary>Propiedad, Indica si realiza actividad deportiva</summary>
        public int ActividadDeportiva
        {
            get { return _ActividadDeportiva; }
            set { _ActividadDeportiva = value; }
        }
        /// <summary>Propiedad, Indica si consume alcohol</summary>
        public int ConsumoAlcohol
        {
            get { return _ConsumoAlcohol; }
            set { _ConsumoAlcohol = value; }
        }
        /// <summary>Propiedad, Frecuencia de consumo alcohol</summary>
        public string FrecuenciaConsumo
        {
            get { return _FrecuenciaConsumo; }
            set { _FrecuenciaConsumo = value; }
        }
        /// <summary>Propiedad, Frecuencia de consumo tabaco</summary>
        public string FrecuenciaTabaquismo
        {
            get { return _FrecuenciaTabaquismo; }
            set { _FrecuenciaTabaquismo = value; }
        }
        /// <summary>Propiedad, Texto para vacunación</summary>
        public string Vacunacion
        {
            get { return _Vacunacion; }
            set { _Vacunacion = value; }
        }
        #endregion

        #region PRUEBAS BIOMÉTRICAS
        //PRUEBAS BIOMÉTRICAS
        /// <summary>Propiedad, Valor total de colesterol</summary>
        public int ColesterolTotal
        {
            get { return _ColesterolTotal; }
            set { _ColesterolTotal = value; }
        }
        /// <summary>Propiedad, Valor colesterol HDL</summary>
        public int ColesterolHDL
        {
            get { return _ColesterolHDL; }
            set { _ColesterolHDL = value; }
        }
        /// <summary>Propiedad, Valor colestero LDL</summary>
        public int ColesterolLDL
        {
            get { return _ColesterolLDL; }
            set { _ColesterolLDL = value; }
        }
        /// <summary>Propiedad, Valor trigliceridos</summary>
        public int Trigliceridos
        {
            get { return _Trigliceridos; }
            set { _Trigliceridos = value; }
        }
        /// <summary>Propiedad, Valor Indice Aterogenico</summary>
        public decimal IndiceAterogenico
        {
            get { return _IndiceAterogenico; }
            set { _IndiceAterogenico = value; }
        }
        /// <summary>Propiedad, Valor Antigeno de Prostata</summary>
        public decimal AntigenoProstata
        {
            get { return _AntigenoProstata; }
            set { _AntigenoProstata = value; }
        }
        /// <summary>Propiedad, Valor Glucemia Ayunas</summary>
        public int GlucemiaAyunas
        {
            get { return _GlucemiaAyunas; }
            set { _GlucemiaAyunas = value; }
        }
        /// <summary>Propiedad, Valor Glucemia sin Ayunas</summary>
        public int GlucemiaSinAyunas
        {
            get { return _GlucemiaSinAyunas; }
            set { _GlucemiaSinAyunas = value; }
        }
        /// <summary>Propiedad, Valor Hemoglobina Glucosilada</summary>
        public decimal HemoglobinaGlucosilada
        {
            get { return _HemoglobinaGlucosilada; }
            set { _HemoglobinaGlucosilada = value; }
        }
        /// <summary>Propiedad, Valor Homocisteina</summary>
        public decimal Homocisteina
        {
            get { return _Homocisteina; }
            set { _Homocisteina = value; }
        }
        /// <summary>Propiedad, Indica si hay existencia de microorganismos</summary>
        public int PresenciaMicroorganismos
        {
            get { return _PresenciaMicroorganismos; }
            set { _PresenciaMicroorganismos = value; }
        }
        /// <summary>Propiedad, Indica la fecha del papanicolau microbiológico</summary>
        public DateTime FechaPapanicolauMicro
        {
            get { return _FechaPapanicolauMicro; }
            set { _FechaPapanicolauMicro = value; }
        }
        /// <summary>Propiedad, Observaciones del papanicolau microbiológico</summary>
        public string ObservacionesPresenciaMicro
        {
            get { return _ObservacionesPresenciaMicro; }
            set { _ObservacionesPresenciaMicro = value; }
        }
        /// <summary>Propiedad, Indica el valor que corresponde al resultado morfológico</summary>
        public int ResultadoMorfologico
        {
            get { return _ResultadoMorfologico; }
            set { _ResultadoMorfologico = value; }
        }
        /// <summary>Propiedad, Indica el valor que corresponde al resultado en anormalidades de células epiteliales</summary>
        public int AnormalidadCelulasEpiteliales
        {
            get { return _AnormalidadCelulasEpiteliales; }
            set { _AnormalidadCelulasEpiteliales = value; }
        }
        /// <summary>Propiedad, Indica el valor que corresponde al resultado en células escamosas atípicas</summary>
        public int CelulasEscamosasAtipicas
        {
            get { return _CelulasEscamosasAtipicas; }
            set { _CelulasEscamosasAtipicas = value; }
        }
        /// <summary>Propiedad, Indica el valor que corresponde al resultado de la mamográfica</summary>
        public int Mamografia
        {
            get { return _Mamografia; }
            set { _Mamografia = value; }
        }
        /// <summary>Propiedad, Indica si es normal o anormal el examen de audiometría</summary>
        public int Audiometria
        {
            get { return _Audiometria; }
            set { _Audiometria = value; }
        }
        /// <summary>Propiedad, Describe las observaciones de examen de audimetría</summary>
        public string AudiometriaObservaciones
        {
            get { return _AudiometriaObservaciones; }
            set { _AudiometriaObservaciones = value; }
        }
        /// <summary>Propiedad, Indica si es normal o anormal el examen de rayos x</summary>
        public int RayosX
        {
            get { return _RayosX; }
            set { _RayosX = value; }
        }
        /// <summary>Propiedad, Describe las observaciones de examen de rayos X</summary>
        public string RayosXObservaciones
        {
            get { return _RayosXObservaciones; }
            set { _RayosXObservaciones = value; }
        }
        /// <summary>Propiedad, Indica si padece de miopía</summary>
        public bool Miopia
        {
            get { return _Miopia; }
            set { _Miopia = value; }
        }
        /// <summary>Propiedad, Valor de la miopía</summary>
        public decimal MiopiaValor
        {
            get { return _MiopiaValor; }
            set { _MiopiaValor = value; }
        }
        /// <summary>Propiedad, Observaciones de la miopia</summary>
        public string MiopiaObservaciones
        {
            get { return _MiopiaObservaciones; }
            set { _MiopiaObservaciones = value; }
        }
        /// <summary>Propiedad, Indica si padece de astigmatísmo</summary>
        public bool Astigmatismo
        {
            get { return _Astigmatismo; }
            set { _Astigmatismo = value; }
        }
        /// <summary>Propiedad, Valor de astigmatismo</summary>
        public decimal AstigmatismoValor
        {
            get { return _AstigmatismoValor; }
            set { _AstigmatismoValor = value; }
        }
        /// <summary>Propiedad, Observaciones de astigmatismo</summary>
        public string AstigmatismoObservaciones
        {
            get { return _AstigmatismoObservaciones; }
            set { _AstigmatismoObservaciones = value; }
        }
        /// <summary>Propiedad, Indica si padece de hipermetropía</summary>
        public bool Hipermetropia
        {
            get { return _Hipermetropia; }
            set { _Hipermetropia = value; }
        }
        /// <summary>Propiedad, Valor de la hipermetriopía</summary>
        public decimal HipermetropiaValor
        {
            get { return _HipermetropiaValor; }
            set { _HipermetropiaValor = value; }
        }
        /// <summary>Propiedad, Observaciones de la hipermetropia</summary>
        public string HipermetropiaObservaciones
        {
            get { return _HipermetropiaObservaciones; }
            set { _HipermetropiaObservaciones = value; }
        }
        /// <summary>Propiedad, Indica si padece de presbicia</summary>
        public bool Presbicia
        {
            get { return _Presbicia; }
            set { _Presbicia = value; }
        }
        /// <summary>Propiedad, Valor de la presbicia</summary>
        public decimal PresbiciaValor
        {
            get { return _PresbiciaValor; }
            set { _PresbiciaValor = value; }
        }
        /// <summary>Propiedad, Observaciones de la presbicia</summary>
        public string PresbiciaObservaciones
        {
            get { return _PresbiciaObservaciones; }
            set { _PresbiciaObservaciones = value; }
        }
        /// <summary>Propiedad, Indica si existe otro problema</summary>
        public bool OtrosExamenVisual
        {
            get { return _OtrosExamenVisual; }
            set { _OtrosExamenVisual = value; }
        }
        /// <summary>Propiedad, Id de diagnostico del examen visual</summary>
        public int IdDiagnosticoExamenVisual
        {
            get { return _IdDiagnosticoExamenVisual; }
            set { _IdDiagnosticoExamenVisual = value; }
        }
        /// <summary>Propiedad, Guarda el valor de colesterolHDL en mmol</summary>
        public decimal ColesterolHDLmmol
        {
            get { return _ColesterolHDLmmol; }
            set { _ColesterolHDLmmol = value; }
        }
        /// <summary>Propiedad, Observaciones de la Mamografia</summary>
        public string MamografiaObservaciones
        {
            get { return _MamografiaObservaciones; }
            set { _MamografiaObservaciones = value; }
        }
        /// <summary>Propiedad, Valor de la miopía del ojo izquierdo</summary>
        public decimal MiopiaValorOI
        {
            get { return _MiopiaValorOI; }
            set { _MiopiaValorOI = value; }
        }
        /// <summary>Propiedad, Valor de astigmatismo del ojo izquierdo</summary>
        public decimal AstigmatismoValorOI
        {
            get { return _AstigmatismoValorOI; }
            set { _AstigmatismoValorOI = value; }
        }
        /// <summary>Propiedad, Valor de la hipermetriopía del ojo izquierdo</summary>
        public decimal HipermetropiaValorOI
        {
            get { return _HipermetropiaValorOI; }
            set { _HipermetropiaValorOI = value; }
        }
        /// <summary>Propiedad, Valor de la presbicia del ojo izquierdo</summary>
        public decimal PresbiciaValorOI
        {
            get { return _PresbiciaValorOI; }
            set { _PresbiciaValorOI = value; }
        }

        #endregion

        #region WELLNESS
        /// <summary>Propiedad, indica si esta afiliado al programa wellness</summary>
        public int ProgramaWellness
        {
            get { return _ProgramaWellness; }
            set { _ProgramaWellness = value; }
        }
        /// <summary>Propiedad, Indica si esta firmado el acuerdo wellness</summary>
        public int FirmaWellness
        {
            get { return _FirmaWellness; }
            set { _FirmaWellness = value; }
        }
        #endregion

        #region HABITO DE FUMAR
        /// <summary>Propiedad, Indica la conducta frente a el cigarrillo</summary>
        public int ConductaCigarrillo
        {
            get { return _ConductaCigarrillo; }
            set { _ConductaCigarrillo = value; }
        }
        /// <summary>Propiedad, Indica el tiempo que transcurre desde que se levanta hasta encender el primer cigarrillo</summary>
        public int TiempoPrimerCigarrillo
        {
            get { return _TiempoPrimerCigarrillo; }
            set { _TiempoPrimerCigarrillo = value; }
        }
        /// <summary>Propiedad, Indica las dificultades para no fumar en lugares donde está prohibido </summary>
        public int DificultadFumar
        {
            get { return _DificultadFumar; }
            set { _DificultadFumar = value; }
        }
        /// <summary>Propiedad, Indica el cigarrillo le costaría más suprimir</summary>
        public int CigarrilloSuprimir
        {
            get { return _CigarrilloSuprimir; }
            set { _CigarrilloSuprimir = value; }
        }
        /// <summary>Propiedad, Indica cuantos cigarrillos fuma al día</summary>
        public int CigarrillosalDia
        {
            get { return _CigarrillosalDia; }
            set { _CigarrillosalDia = value; }
        }
        /// <summary>Propiedad, Indica si Fuma más frecuentemente durante las primeras horas del día que durante el resto del día</summary>
        public int FrecuenciaPrimerasHorasDia
        {
            get { return _FrecuenciaPrimerasHorasDia; }
            set { _FrecuenciaPrimerasHorasDia = value; }
        }
        /// <summary>Propiedad, Indica si Fuma cuándo debe guardar cama por una enfermedad la mayor parte del día</summary>
        public int FumaEnfermedad
        {
            get { return _FumaEnfermedad; }
            set { _FumaEnfermedad = value; }
        }
        /// <summary>Propiedad, Indica en que categoría entran la mayoría de cigarrillos que usted fuma</summary>
        public int CategoriaCigarrillos
        {
            get { return _CategoriaCigarrillos; }
            set { _CategoriaCigarrillos = value; }
        }
        /// <summary>Propiedad, Indica si Aspira el humo cuando fuma</summary>
        public int AspiraHumo
        {
            get { return _AspiraHumo; }
            set { _AspiraHumo = value; }
        }
        /// <summary>Propiedad, Indica hace cuanto dejo de fumar</summary>
        public decimal AnosDejoFumar
        {
            get { return _AnosDejoFumar; }
            set { _AnosDejoFumar = value; }
        }
        /// <summary>Propiedad, Indica el promedio diario de cigarrillos que fumaba durante los dos años previos de dejar el hábito</summary>
        public int PromedioDiarioX2Anos
        {
            get { return _PromedioDiarioX2Anos; }
            set { _PromedioDiarioX2Anos = value; }
        }
        /// <summary>Propiedad, Indica si convive habitualmente con un fumador</summary>
        public int ConviveFumador
        {
            get { return _ConviveFumador; }
            set { _ConviveFumador = value; }
        }


        #endregion

        #region CONSUMO ALCOHOL
        /// <summary>Propiedad, Indica las Copas a la semana que consume</summary>
        public int CopasSemana
        {
            get { return _CopasSemana; }
            set { _CopasSemana = value; }
        }
        /// <summary>Propiedad, indica si han criticado su consumo de alcoho</summary>
        public int CriticaAlcohol
        {
            get { return _CriticaAlcohol; }
            set { _CriticaAlcohol = value; }
        }
        /// <summary>Propiedad, Indica si se ha arrepentido de la cantidad de alcohol que consumió</summary>
        public int ArrepentidoAlcohol
        {
            get { return _ArrepentidoAlcohol; }
            set { _ArrepentidoAlcohol = value; }
        }
        /// <summary>Propiedad, indica si ha tenido lagunas por el consumo de alcohol</summary>
        public int LagunaAlcohol
        {
            get { return _LagunaAlcohol; }
            set { _LagunaAlcohol = value; }
        }
        /// <summary>Propiedad, Indica si alguna vez lo primero que ha consumido en la mañana ha sido una copa de alcohol</summary>
        public int MananaAlcohol
        {
            get { return _MananaAlcohol; }
            set { _MananaAlcohol = value; }
        }
        #endregion

        #region VACUNACION
        /// <summary>Propiedad, Indica si se ha aplicado la vacuna contra Influenza Estacional en el último año</summary>
        public int InfluenciaEstacional
        {
            get { return _InfluenciaEstacional; }
            set { _InfluenciaEstacional = value; }
        }
        /// <summary>Propiedad, Guarda fecha de aplicación de la vacuna influencia estacional</summary>
        public DateTime FechaInfluenzaEstacional
        {
            get { return _FechaInfluenzaEstacional; }
            set { _FechaInfluenzaEstacional = value; }
        }
        /// <summary>Propiedad, Indica si se ha aplicado la vacuna contra Influenza H1N1 en el último año</summary>
        public int InfluenciaH1N1
        {
            get { return _InfluenciaH1N1; }
            set { _InfluenciaH1N1 = value; }
        }
        /// <summary>Propiedad, Guarda fecha de aplicación de la vacuna influencia H1N1</summary>
        public DateTime FechaInfluenciaH1N1
        {
            get { return _FechaInfluenciaH1N1; }
            set { _FechaInfluenciaH1N1 = value; }
        }
        /// <summary>Propiedad, indica si se ha aplicado la vacuna contra Fiebre Amarilla</summary>
        public int FiebreAmarilla
        {
            get { return _FiebreAmarilla; }
            set { _FiebreAmarilla = value; }
        }
        /// <summary>Propiedad, Guarda fecha de aplicación de la vacuna contra fiebre amarilla</summary>
        public DateTime FechaFiebreAmarilla
        {
            get { return _FechaFiebreAmarilla; }
            set { _FechaFiebreAmarilla = value; }
        }
        /// <summary>Propiedad, </summary>
        public int HepatitisViral
        {
            get { return _HepatitisViral; }
            set { _HepatitisViral = value; }
        }
        /// <summary>Propiedad, Guarda fecha de aplicación de la vacuna contra hepatitis viral</summary>
        public DateTime FechaHepatitisViral
        {
            get { return _FechaHepatitisViral; }
            set { _FechaHepatitisViral = value; }
        }
        /// <summary>Propiedad, indica si se ha aplicado la vacuna contra el Tétanos</summary>
        public int ToxoideTetanico
        {
            get { return _ToxoideTetanico; }
            set { _ToxoideTetanico = value; }
        }
        /// <summary>Propiedad, Guarda fecha de aplicación de la vacuna contra en tetanos</summary>
        public DateTime FechaToxoideTetanico
        {
            get { return _FechaToxoideTetanico; }
            set { _FechaToxoideTetanico = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteInfluenciaEstacional
        {
            get { return _MarcaLoteInfluenciaEstacional; }
            set { _MarcaLoteInfluenciaEstacional = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteInfluenciaH1N1
        {
            get { return _MarcaLoteInfluenciaH1N1; }
            set { _MarcaLoteInfluenciaH1N1 = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteFiebreAmarilla
        {
            get { return _MarcaLoteFiebreAmarilla; }
            set { _MarcaLoteFiebreAmarilla = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteHepatitisViral
        {
            get { return _MarcaLoteHepatitisViral; }
            set { _MarcaLoteHepatitisViral = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteToxoideTetanico
        {
            get { return _MarcaLoteToxoideTetanico; }
            set { _MarcaLoteToxoideTetanico = value; }
        }
        /// <summary>Propiedad, </summary>
        public int HepatitisViralB
        {
            get { return _HepatitisViralB; }
            set { _HepatitisViralB = value; }
        }
        /// <summary>Propiedad, </summary>
        public DateTime FechaHepatitisViralB
        {
            get { return _FechaHepatitisViralB; }
            set { _FechaHepatitisViralB = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteHepatitisViralB
        {
            get { return _MarcaLoteHepatitisViralB; }
            set { _MarcaLoteHepatitisViralB = value; }
        }
        /// <summary>Propiedad, </summary>
        public int Meningococo
        {
            get { return _Meningococo; }
            set { _Meningococo = value; }
        }
        /// <summary>Propiedad, </summary>
        public DateTime FechaMeningococo
        {
            get { return _FechaMeningococo; }
            set { _FechaMeningococo = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteMeningococo
        {
            get { return _MarcaLoteMeningococo; }
            set { _MarcaLoteMeningococo = value; }
        }
        /// <summary>Propiedad, </summary>
        public int FiebreTifoidea
        {
            get { return _FiebreTifoidea; }
            set { _FiebreTifoidea = value; }
        }
        /// <summary>Propiedad, </summary>
        public DateTime FechaFiebreTifoidea
        {
            get { return _FechaFiebreTifoidea; }
            set { _FechaFiebreTifoidea = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteFiebreTifoidea
        {
            get { return _MarcaLoteFiebreTifoidea; }
            set { _MarcaLoteFiebreTifoidea = value; }
        }
        /// <summary>Propiedad, </summary>
        public int FiebreVPH
        {
            get { return _FiebreVPH; }
            set { _FiebreVPH = value; }
        }
        /// <summary>Propiedad, </summary>
        public DateTime FechaVPH
        {
            get { return _FechaVPH; }
            set { _FechaVPH = value; }
        }
        /// <summary>Propiedad, </summary>
        public string MarcaLoteVPH
        {
            get { return _MarcaLoteVPH; }
            set { _MarcaLoteVPH = value; }
        }


        #endregion

        #region SEDENTARISMO
        /// <summary>Propiedad, indica si practica deporte</summary>
        public int PracticaDeporte
        {
            get { return _PracticaDeporte; }
            set { _PracticaDeporte = value; }
        }
        /// <summary>Propiedad, Indica cuantas veces practica deporte a la semana</summary>
        public int PracticaDeporteSemana
        {
            get { return _PracticaDeporteSemana; }
            set { _PracticaDeporteSemana = value; }
        }
        /// <summary>Propiedad, Indica el Promedio de tiempo en minutos en cada sesión que practica deporte</summary>
        public int PromedioTiempoMinutos
        {
            get { return _PromedioTiempoMinutos; }
            set { _PromedioTiempoMinutos = value; }
        }
        /// <summary>Propiedad, indica el tipo de actividad física</summary>
        public int TipoActividadFisica
        {
            get { return _TipoActividadFisica; }
            set { _TipoActividadFisica = value; }
        }
        /// <summary>Propiedad, indica cuantas horas ve diarias en promedio de televisión</summary>
        public int HorasDiariasTV
        {
            get { return _HorasDiariasTV; }
            set { _HorasDiariasTV = value; }
        }
        /// <summary>Propiedad, indica cuantas si hizo deporte el ultimo mes</summary>
        public int rutinaEjercicioUltimoMes
        {
            get { return _rutinaEjercicioUltimoMes; }
            set { _rutinaEjercicioUltimoMes = value; }
        }
        /// <summary>Propiedad, indica la razon por que no hizo deporte el ultimo mes</summary>
        public int NoRutinaEjercicio
        {
            get { return _NoRutinaEjercicio; }
            set { _NoRutinaEjercicio = value; }
        }
        /// <summary>Propiedad, indica otra razon por que no hizo deporte el ultimo mes</summary>
        public string OtroMotivo
        {
            get { return _OtroMotivo; }
            set { _OtroMotivo = value; }
        }

        #endregion

        #region SALUD ORAL
        /// <summary>Propiedad, Indica si ha asistido a consulta odontológica en el último año</summary>
        public int ConsultaOdontologica
        {
            get { return _ConsultaOdontologica; }
            set { _ConsultaOdontologica = value; }
        }
        /// <summary>Propiedad, Indica cuantas veces se lava los dientes al día</summary>
        public int LavaDientes
        {
            get { return _LavaDientes; }
            set { _LavaDientes = value; }
        }
        /// <summary>Propiedad, Indica el uso de hilo dental todos los días</summary>
        public int SedaDental
        {
            get { return _SedaDental; }
            set { _SedaDental = value; }
        }
        #endregion

        #region ESTRES
        /// <summary>Propiedad, Indica si se ha sentido decaído (a), deprimido (a) o estresado (a) de manera persistente</summary>
        public int SentidoDecaido
        {
            get { return _SentidoDecaido; }
            set { _SentidoDecaido = value; }
        }
        /// <summary>Propiedad, Indica si se ha sentido estresado en los ultimos 3 meses</summary>
        public int SentidoEstresado
        {
            get { return _SentidoEstresado; }
            set { _SentidoEstresado = value; }
        }
        /// <summary>Propiedad, Indica si has tenido poco interés o placer al hacer las cosas</summary>
        public int InteresPlacer
        {
            get { return _InteresPlacer; }
            set { _InteresPlacer = value; }
        }
        /// <summary>Propiedad, Indica el nivel de estres</summary>
        public int NivelEstres
        {
            get { return _NivelEstres; }
            set { _NivelEstres = value; }
        }
        /// <summary>Propiedad, Indica el plan para controlar el estrés</summary>
        public int ControlarEstres
        {
            get { return _ControlarEstres; }
            set { _ControlarEstres = value; }
        }
        /// <summary>Propiedad, Indica el interes por hacer las cosas</summary>
        public int PocoInteres
        {
            get { return _PocoInteres; }
            set { _PocoInteres = value; }
        }
        /// <summary>Propiedad, Indica si se ha sentido decaido ultimamente</summary>
        public int SinEsperanza
        {
            get { return _SinEsperanza; }
            set { _SinEsperanza = value; }
        }

        #endregion

        #region EMOCIONAL
        /// <summary>Propiedad, Indica cómo calificarías la calidad general de tu sueño</summary>
        public int CalificacionSueno
        {
            get { return _CalificacionSueno; }
            set { _CalificacionSueno = value; }
        }
        /// <summary>Propiedad, Indica el estado Después de una noche habitual de sueño</summary>
        public int EstadoLevantarse
        {
            get { return _EstadoLevantarse; }
            set { _EstadoLevantarse = value; }
        }
        /// <summary>Propiedad, Indica el estado Después de una noche habitual de sueño en el ultimo mes</summary>
        public int DormidoSuficiente
        {
            get { return _DormidoSuficiente; }
            set { _DormidoSuficiente = value; }
        }
        /// <summary>Propiedad, Indica cuantas horas duerme regularmente</summary>
        public int HorasDuermeRegular
        {
            get { return _HorasDuermeRegular; }
            set { _HorasDuermeRegular = value; }
        }
        /// <summary>Propiedad, Indica Cómo califica su estado de ánimo emocional</summary>
        public int EstadoAnimoEmocional
        {
            get { return _EstadoAnimoEmocional; }
            set { _EstadoAnimoEmocional = value; }
        }
        #endregion

        #region ALIMENTACION INADECUADA

        /// <summary>Propiedad, Indica las porciones de frutas que consume</summary>
        public int PorcionesFrutas
        {
            get { return _PorcionesFrutas; }
            set { _PorcionesFrutas = value; }
        }
        /// <summary>Propiedad, Indica las porciones de vegetales que consume</summary>
        public int PorcionesVegetales
        {
            get { return _PorcionesVegetales; }
            set { _PorcionesVegetales = value; }
        }
        /// <summary>Propiedad, Indica la frecuencia que consume alimetos no sanos</summary>
        public int FrecuenciaCarne
        {
            get { return _FrecuenciaCarne; }
            set { _FrecuenciaCarne = value; }
        }
        /// <summary>Propiedad, Indica la frecuencia que consume alimetos sanos</summary>
        public int FrecuenciaSano
        {
            get { return _FrecuenciaSano; }
            set { _FrecuenciaSano = value; }
        }
        /// <summary>Propiedad, Indica las porciones de granos que consume</summary>
        public int FrecuenciaGranos
        {
            get { return _FrecuenciaGranos; }
            set { _FrecuenciaGranos = value; }
        }
        /// <summary>Propiedad, Indica las porciones de azucar que consume</summary>
        public int FrecuenciaAzucar
        {
            get { return _FrecuenciaAzucar; }
            set { _FrecuenciaAzucar = value; }
        }
        /// <summary>Propiedad, Indica las porciones de sodio que consume</summary>
        public int FrecuenciaSodio
        {
            get { return _FrecuenciaSodio; }
            set { _FrecuenciaSodio = value; }
        }

        #endregion

        #region COMPORTAMIENTOS DE RIESGO Y ACCIDENTALIDAD
        /// <summary>Propiedad, Indica si se Utiliza el Cinturón de seguridad</summary>
        public int CinturonSeguridad
        {
            get { return _CinturonSeguridad; }
            set { _CinturonSeguridad = value; }
        }
        /// <summary>Propiedad, Indica si Cuándo conduce el coche utiliza el celular con manos libres</summary>
        public int CocheCelular
        {
            get { return _CocheCelular; }
            set { _CocheCelular = value; }
        }
        /// <summary>Propiedad, Indica Qué tan cerca del límite de velocidad conduces generalmente</summary>
        public int LimiteVelocidad
        {
            get { return _LimiteVelocidad; }
            set { _LimiteVelocidad = value; }
        }
        /// <summary>Propiedad, Indica Con qué frecuencia en el último mes has manejado o viajado en un vehículo en el que posiblemente el conductor había bebido demasiado</summary>
        public int ConductorBebido
        {
            get { return _ConductorBebido; }
            set { _ConductorBebido = value; }
        }
        /// <summary>Propiedad, Indica Con qué frecuencia usas un casco cuando paseas en bicicleta o motocicleta</summary>
        public int Casco
        {
            get { return _Casco; }
            set { _Casco = value; }
        }
        /// <summary>Propiedad, Indica Con qué frecuencia usas filtro solar con factor de protección 15 o mayor cuando pasas tiempo al sol</summary>
        public int FiltroSolar
        {
            get { return _FiltroSolar; }
            set { _FiltroSolar = value; }
        }
        /// <summary>Propiedad, Indica si Has realizado alguna revisión de seguridad doméstica en los seis meses anteriores</summary>
        public int SeguridadDomestica
        {
            get { return _SeguridadDomestica; }
            set { _SeguridadDomestica = value; }
        }
        /// <summary>Propiedad, Indica las medidas de protección adecuadas frente al riesgo de contraer enfermedades de transmisión sexual</summary>
        public int TrasmisionSexual
        {
            get { return _TrasmisionSexual; }
            set { _TrasmisionSexual = value; }
        }
        #endregion

        #region PERCEPCIÓN DEL ESTADO DE SALUD
        /// <summary>Propiedad, Indica Cómo califica su estado de salud en términos generales</summary>
        public int EstadoSalud
        {
            get { return _EstadoSalud; }
            set { _EstadoSalud = value; }
        }
        /// <summary>Propiedad, Indica En general que tan dispuesto está a modificar sus hábitos de vida como son actividad física, dejar de fumar y un programa de educación en salud</summary>
        public int HabitosVida
        {
            get { return _HabitosVida; }
            set { _HabitosVida = value; }
        }
        #endregion

        #region ANTECEDENTES AUSENTISMO
        /// <summary>Propiedad, Indica si ha estado incapacitado en el último año</summary>
        public int Incapacitado
        {
            get { return _Incapacitado; }
            set { _Incapacitado = value; }
        }
        /// <summary>Propiedad, Indica el diagnóstico CIE10 de incapacidad</summary>
        public int IdDiagnosticoIncapacidad
        {
            get { return _IdDiagnosticoIncapacidad; }
            set { _IdDiagnosticoIncapacidad = value; }
        }
        /// <summary>Propiedad, Cantidad de días de incapacidad</summary>
        public int DiasIncapacidad
        {
            get { return _DiasIncapacidad; }
            set { _DiasIncapacidad = value; }
        }
        /// <summary>Propiedad, Indica el dianóstico CIE 10 de hospitalización</summary>
        public int IdDiagnosticoHospitalizacion1
        {
            get { return _IdDiagnosticoHospitalizacion1; }
            set { _IdDiagnosticoHospitalizacion1 = value; }
        }
        /// <summary>Propiedad, Indica la fecha de hospitalización</summary>
        public DateTime FechaHospitalizacion1
        {
            get { return _FechaHospitalizacion1; }
            set { _FechaHospitalizacion1 = value; }
        }
        /// <summary>Propiedad, Indica los días hospitalizados</summary>
        public int DiasHospitalizacion1
        {
            get { return _DiasHospitalizacion1; }
            set { _DiasHospitalizacion1 = value; }
        }
        /// <summary>Propiedad, Indica el dianóstico CIE 10 de hospitalización</summary>
        public int IdDiagnosticoHospitalizacion2
        {
            get { return _IdDiagnosticoHospitalizacion2; }
            set { _IdDiagnosticoHospitalizacion2 = value; }
        }
        /// <summary>Propiedad, Indica la fecha dehospitalización</summary>
        public DateTime FechaHospitalizacion2
        {
            get { return _FechaHospitalizacion2; }
            set { _FechaHospitalizacion2 = value; }
        }
        /// <summary>Propiedad, Indica los días hospitalizados</summary>
        public int DiasHospitalizacion2
        {
            get { return _DiasHospitalizacion2; }
            set { _DiasHospitalizacion2 = value; }
        }
        /// <summary>Propiedad, Indica el dianóstico CIE 10 de hospitalización</summary>
        public int IdDiagnosticoHospitalizacion3
        {
            get { return _IdDiagnosticoHospitalizacion3; }
            set { _IdDiagnosticoHospitalizacion3 = value; }
        }
        /// <summary>Propiedad, Indica la fecha de hospitalización</summary>
        public DateTime FechaHospitalizacion3
        {
            get { return _FechaHospitalizacion3; }
            set { _FechaHospitalizacion3 = value; }
        }
        /// <summary>Propiedad, Indica los días hospitalizados</summary>
        public int DiasHospitalizacion3
        {
            get { return _DiasHospitalizacion3; }
            set { _DiasHospitalizacion3 = value; }
        }
        /// <summary>Propiedad, Indica el dianóstico CIE 10 de hospitalización</summary>
        public int IdDiagnosticoHospitalizacion4
        {
            get { return _IdDiagnosticoHospitalizacion4; }
            set { _IdDiagnosticoHospitalizacion4 = value; }
        }
        /// <summary>Propiedad, Indica la fecha de hospitalización</summary>
        public DateTime FechaHospitalizacion4
        {
            get { return _FechaHospitalizacion4; }
            set { _FechaHospitalizacion4 = value; }
        }
        /// <summary>Propiedad, Indica los días hospitalizados</summary>
        public int DiasHospitalizacion4
        {
            get { return _DiasHospitalizacion4; }
            set { _DiasHospitalizacion4 = value; }
        }

        #endregion        

        #region NUTRICIÓN
        /// <summary>Propiedad, Indica si consume desayuno</summary>
        public int Desayuno
        {
            get { return _Desayuno; }
            set { _Desayuno = value; }
        }
        /// <summary>Propiedad, Indica hora de desayuno</summary>
        public short DesayunoHora
        {
            get { return _DesayunoHora; }
            set { _DesayunoHora = value; }
        }
        /// <summary>Propiedad, Indica si consume almuerzo</summary>
        public int Almuerzo
        {
            get { return _Almuerzo; }
            set { _Almuerzo = value; }
        }
        /// <summary>Propiedad, Indica hora de almuerzo</summary>
        public short AlmuerzoHora
        {
            get { return _AlmuerzoHora; }
            set { _AlmuerzoHora = value; }
        }
        /// <summary>Propiedad, Indica si consume comida</summary>
        public int Comida
        {
            get { return _Comida; }
            set { _Comida = value; }
        }
        /// <summary>Propiedad, Indica hora de comida</summary>
        public short ComidaHora
        {
            get { return _ComidaHora; }
            set { _ComidaHora = value; }
        }
        /// <summary>Propiedad, Indica si consume entremes</summary>
        public int Entremes
        {
            get { return _Entremes; }
            set { _Entremes = value; }
        }
        /// <summary>Propiedad, Indica hora de entremes</summary>
        public short EntremesHora
        {
            get { return _EntremesHora; }
            set { _EntremesHora = value; }
        }
        /// <summary>Propiedad, Indica si consume cena</summary>
        public int Cena
        {
            get { return _Cena; }
            set { _Cena = value; }
        }
        /// <summary>Propiedad, Indica hora de cena</summary>
        public short CenaHora
        {
            get { return _CenaHora; }
            set { _CenaHora = value; }
        }
        /// <summary>Propiedad, Indica el Diámetro de la Cintura</summary>
        public decimal DiametroCintura
        {
            get { return _DiametroCintura; }
            set { _DiametroCintura = value; }
        }
        /// <summary>Propiedad, Indica el Diámetro de la cadera</summary>
        public decimal DiametroCadera
        {
            get { return _DiametroCadera; }
            set { _DiametroCadera = value; }
        }
        /// <summary>Propiedad, Indica la relación entre el Diámetro de la cadera y el Diámetro de la cintura</summary>
        public decimal RelacionCinturaCadera
        {
            get { return _RelacionCinturaCadera; }
            set { _RelacionCinturaCadera = value; }
        }
        /// <summary>Propiedad, Descripcion de la relación entre cintura y cadera</summary>
        public int DescripcionRelacion
        {
            get { return _DescripcionRelacion; }
            set { _DescripcionRelacion = value; }
        }
        /// <summary>Propiedad, valor de la masa grasa</summary>
        public decimal MasaGrasa
        {
            get { return _MasaGrasa; }
            set { _MasaGrasa = value; }
        }
        /// <summary>Propiedad, valor de la masa grama</summary>
        public decimal MasaGrama
        {
            get { return _MasaGrama; }
            set { _MasaGrama = value; }
        }
        /// <summary>Propiedad, Valor peso recomendable</summary>
        public decimal PesoRecomendable
        {
            get { return _PesoRecomendable; }
            set { _PesoRecomendable = value; }
        }
        /// <summary>Propiedad, Valor excedente grasa</summary>
        public decimal ExcedenteGrasa
        {
            get { return _ExcedenteGrasa; }
            set { _ExcedenteGrasa = value; }
        }
        /// <summary>Propiedad, Indica el tipo de delgadez</summary>
        public int DiagnosticoNutricional
        {
            get { return _DiagnosticoNutricional; }
            set { _DiagnosticoNutricional = value; }
        }
        /// <summary>Propiedad, Indica el id del diagnostico nutricional CIE 10</summary>
        public int IdDiagnosticoNutricional
        {
            get { return _IdDiagnosticoNutricional; }
            set { _IdDiagnosticoNutricional = value; }
        }
        /// <summary>Propiedad, Guarda las recomendaciones nutricionales</summary>
        public string RecomendacionesNutricionales
        {
            get { return _RecomendacionesNutricionales; }
            set { _RecomendacionesNutricionales = value; }
        }
        /// <summary>Propiedad, Indica los planes para alimentación saludable</summary>
        public int AlimentacionSaludable
        {
            get { return _AlimentacionSaludable; }
            set { _AlimentacionSaludable = value; }
        }
        /// <summary>Propiedad, Peso hace seis meses</summary>
        public decimal PesoHace6Meses
        {
            get { return _PesoHace6Meses; }
            set { _PesoHace6Meses = value; }
        }
        /// <summary>Propiedad, Consideras que en tu peso, ha habido una fluctuación mayor al 10% en los últimos dos años</summary>
        public int PesoFluctuacion
        {
            get { return _PesoFluctuacion; }
            set { _PesoFluctuacion = value; }
        }
        /// <summary>Propiedad, Cómo consideras que es tu apetito</summary>
        public int ConsideracionApetito
        {
            get { return _ConsideracionApetito; }
            set { _ConsideracionApetito = value; }
        }
        /// <summary>Propiedad, Con que frecuencia existe eliminación intestinal</summary>
        public int EliminacionIntestinal
        {
            get { return _EliminacionIntestinal; }
            set { _EliminacionIntestinal = value; }
        }
        /// <summary>Propiedad, Eres intolerante a algún alimento</summary>
        public int IntoranciaAlimento
        {
            get { return _IntoranciaAlimento; }
            set { _IntoranciaAlimento = value; }
        }
        /// <summary>Propiedad, descripcion de intolerancia a algún alimento</summary>
        public string IntoranciaAlimentoEspecificacion
        {
            get { return _IntoranciaAlimentoEspecificacion; }
            set { _IntoranciaAlimentoEspecificacion = value; }
        }
        /// <summary>Propiedad, Padeces alergia (s) con algún alimento</summary>
        public int AlergiaAlimento
        {
            get { return _AlergiaAlimento; }
            set { _AlergiaAlimento = value; }
        }
        /// <summary>Propiedad, Descripción de padecer alergia (s) con algún alimento</summary>
        public string AlergiaAlimentoEspecificacion
        {
            get { return _AlergiaAlimentoEspecificacion; }
            set { _AlergiaAlimentoEspecificacion = value; }
        }
        /// <summary>Propiedad, Lugar donde toma desayuno</summary>
        public int DesayunoLugar
        {
            get { return _DesayunoLugar; }
            set { _DesayunoLugar = value; }
        }
        /// <summary>Propiedad, Lugar donde toma almuerzo</summary>
        public int AlmuerzoLugar
        {
            get { return _AlmuerzoLugar; }
            set { _AlmuerzoLugar = value; }
        }
        /// <summary>Propiedad, Lugar donde toma comida</summary>
        public int ComidaLugar
        {
            get { return _ComidaLugar; }
            set { _ComidaLugar = value; }
        }
        /// <summary>Propiedad, Lugar donde toma entremes</summary>
        public int EntremesLugar
        {
            get { return _EntremesLugar; }
            set { _EntremesLugar = value; }
        }
        /// <summary>Propiedad, Lugar donde toma la cena</summary>
        public int CenaLugar
        {
            get { return _CenaLugar; }
            set { _CenaLugar = value; }
        }
        /// <summary>Propiedad, Reconocer cuando estás satisfecho</summary>
        public int EstarSatisfecho
        {
            get { return _EstarSatisfecho; }
            set { _EstarSatisfecho = value; }
        }
        /// <summary>Propiedad, Creer que te satisfaces con facilidad</summary>
        public int SatisfaccionFacilidad
        {
            get { return _SatisfaccionFacilidad; }
            set { _SatisfaccionFacilidad = value; }
        }
        /// <summary>Propiedad, Reconocer cuando tienes hambre</summary>
        public int ReconocerHambre
        {
            get { return _ReconocerHambre; }
            set { _ReconocerHambre = value; }
        }
        /// <summary>Propiedad, Acostumbrar a comer despacio</summary>
        public int ComerDespacio
        {
            get { return _ComerDespacio; }
            set { _ComerDespacio = value; }
        }
        /// <summary>Propiedad, A qué hora del día sientes mayor apetito</summary>
        public short MayorApetitoHora
        {
            get { return _MayorApetitoHora; }
            set { _MayorApetitoHora = value; }
        }
        /// <summary>Propiedad, A qué hora del día sientes antojos</summary>
        public short AntojosHora
        {
            get { return _AntojosHora; }
            set { _AntojosHora = value; }
        }
        /// <summary>Propiedad, En el último año te has sometido a alguna dieta</summary>
        public int SometidoDieta
        {
            get { return _SometidoDieta; }
            set { _SometidoDieta = value; }
        }
        /// <summary>Propiedad, Actualmente llevas a cabo alguna dieta</summary>
        public int LlevasDieta
        {
            get { return _LlevasDieta; }
            set { _LlevasDieta = value; }
        }
        /// <summary>Propiedad, Por quién fue prescrita</summary>
        public int QuienPrescribe
        {
            get { return _QuienPrescribe; }
            set { _QuienPrescribe = value; }
        }
        /// <summary>Propiedad, Cuál fue la principal razón que te motivó a iniciar una dieta</summary>
        public int MotivoIniciarDieta
        {
            get { return _MotivoIniciarDieta; }
            set { _MotivoIniciarDieta = value; }
        }
        /// <summary>Propiedad, Comparando con la dieta que llevabas , ¿cómo consideras que es la ingestión actual de tus alimentos?</summary>
        public int IngestionAlimentos
        {
            get { return _IngestionAlimentos; }
            set { _IngestionAlimentos = value; }
        }
        /// <summary>Propiedad, En caso de consumir algún complemento para bajar de peso, por quién fue prescrito</summary>
        public int BajarPesoPrescrito
        {
            get { return _BajarPesoPrescrito; }
            set { _BajarPesoPrescrito = value; }
        }
        /// <summary>Propiedad, Descripcion bajo peso prescrito</summary>
        public string BajarPesoPrescritoEspecificacion
        {
            get { return _BajarPesoPrescritoEspecificacion; }
            set { _BajarPesoPrescritoEspecificacion = value; }
        }
        /// <summary>Propiedad, Has padecido de algún trastorno de alimentación</summary>
        public int TrastornoAlimentacion
        {
            get { return _TrastornoAlimentacion; }
            set { _TrastornoAlimentacion = value; }
        }
        /// <summary>Propiedad, id del Trastorno</summary>
        public int IdDiagnosticoTrastorno
        {
            get { return _IdDiagnosticoTrastorno; }
            set { _IdDiagnosticoTrastorno = value; }
        }
        /// <summary>Propiedad, Hace cuánto tiempo lo padeciste</summary>
        public int PadecerTrastorno
        {
            get { return _PadecerTrastorno; }
            set { _PadecerTrastorno = value; }
        }
        /// <summary>Propiedad, A qué hora acostumbra levantarse entre semana</summary>
        public short LevantarseEntreSemana
        {
            get { return _LevantarseEntreSemana; }
            set { _LevantarseEntreSemana = value; }
        }
        /// <summary>Propiedad, A qué hora acostumbra salir de casa fin de semana</summary>
        public short LevantarseFinDeSemana
        {
            get { return _LevantarseFinDeSemana; }
            set { _LevantarseFinDeSemana = value; }
        }
        /// <summary>Propiedad, A qué hora acostumbra salir de casa entre semana</summary>
        public short SalirCasaEntreSemana
        {
            get { return _SalirCasaEntreSemana; }
            set { _SalirCasaEntreSemana = value; }
        }
        /// <summary>Propiedad, A qué hora acostumbra salir de casa fin de semana</summary>
        public short SalirCasaFinDeSemana
        {
            get { return _SalirCasaFinDeSemana; }
            set { _SalirCasaFinDeSemana = value; }
        }
        /// <summary>Propiedad, A qué hora acostumbra acostarse entre semana</summary>
        public short AcostarseEntreSemana
        {
            get { return _AcostarseEntreSemana; }
            set { _AcostarseEntreSemana = value; }
        }
        /// <summary>Propiedad, A qué hora acostumbra acostarse fin de semana</summary>
        public short AcostarseFinDeSemana
        {
            get { return _AcostarseFinDeSemana; }
            set { _AcostarseFinDeSemana = value; }
        }
        /// <summary>Propiedad, En promedio, que tan frecuentemente compras comida rápida</summary>
        public int ComidaRapida
        {
            get { return _ComidaRapida; }
            set { _ComidaRapida = value; }
        }
        /// <summary>Propiedad, Aproximadamente cuántos vasos de agua consumes al día</summary>
        public int VasosAgua
        {
            get { return _VasosAgua; }
            set { _VasosAgua = value; }
        }
        #endregion

        #region ESTAMOS CONTIGO

        /// <summary>Propiedad, Indica si se tomo la presion arterial en los ultimos 30 días</summary>
        public int PresionArterial30dias
        {
            get { return _PresionArterial30dias; }
            set { _PresionArterial30dias = value; }
        }
        /// <summary>Atributo, Indica la fecha en la se tomo la presion arterial en los ultimos 30 días</summary>
        public string FechaPresionArterial30dias
        {
            get { return _FechaPresionArterial30dias; }
            set { _FechaPresionArterial30dias = value; }
        }
        /// <summary>Atributo, Indica el valor de la presion arterial en los ultimos 30 días</summary>
        public int ValorPresionArterial30dias
        {
            get { return _ValorPresionArterial30dias; }
            set { _ValorPresionArterial30dias = value; }
        }
        /// <summary>Atributo, Indica si se tomo la glucosa en los ultimos 30 días</summary>
        public int Glucosa30dias
        {
            get { return _Glucosa30dias; }
            set { _Glucosa30dias = value; }
        }
        /// <summary>Atributo, Indica la fecha en la se tomo la glucosa en los ultimos 30 días</summary>
        public string FechaGlucosa30dias
        {
            get { return _FechaGlucosa30dias; }
            set { _FechaGlucosa30dias = value; }
        }
        /// <summary>Atributo, Indica el valor de la glucosa en los ultimos 30 días</summary>
        public int ValorGlucosa30dias
        {
            get { return _ValorGlucosa30dias; }
            set { _ValorGlucosa30dias = value; }
        }
        /// <summary>Atributo, Indica si se tomo el colesterol total en los ultimos 30 días</summary>
        public int ColesterolTotal30Dias
        {
            get { return _ColesterolTotal30Dias; }
            set { _ColesterolTotal30Dias = value; }
        }
        /// <summary>Atributo, Indica la fecha en la se tomo el colesterol total en los ultimos 30 días</summary>
        public string FechaColesterolTotal30Dias
        {
            get { return _FechaColesterolTotal30Dias; }
            set { _FechaColesterolTotal30Dias = value; }
        }
        /// <summary>Atributo, Indica el valor de el colesterol total en los ultimos 30 días</summary>
        public int ValorColesterolTotal30Dias
        {
            get { return _ValorColesterolTotal30Dias; }
            set { _ValorColesterolTotal30Dias = value; }
        }
        /// <summary>Atributo, Indica si se tomo el colesterol HDL en los ultimos 30 días</summary>
        public int ColesterolHDL30Dias
        {
            get { return _ColesterolHDL30Dias; }
            set { _ColesterolHDL30Dias = value; }
        }
        /// <summary>Atributo, Indica la fecha en la se tomo el colesterol HDL en los ultimos 30 días</summary>
        public string FechaColesterolHDL30Dias
        {
            get { return _FechaColesterolHDL30Dias; }
            set { _FechaColesterolHDL30Dias = value; }
        }
        /// <summary>Atributo, Indica el valor de el colesterol HDL en los ultimos 30 días</summary>
        public int ValorColesterolHDL30Dias
        {
            get { return _ValorColesterolHDL30Dias; }
            set { _ValorColesterolHDL30Dias = value; }
        }
        /// <summary>Atributo, Indica si se tomo el colesterol LDL en los ultimos 30 días</summary>
        public int ColesterolLDL30Dias
        {
            get { return _ColesterolLDL30Dias; }
            set { _ColesterolLDL30Dias = value; }
        }
        /// <summary>Atributo, Indica la fecha en la se tomo el colesterol LDL en los ultimos 30 días</summary>
        public string FechaColesterolLDL30Dias
        {
            get { return _FechaColesterolLDL30Dias; }
            set { _FechaColesterolLDL30Dias = value; }
        }
        /// <summary>Atributo, Indica el valor de el colesterol LDL en los ultimos 30 días</summary>
        public int ValorColesterolLDL30Dias
        {
            get { return _ValorColesterolLDL30Dias; }
            set { _ValorColesterolLDL30Dias = value; }
        }
        /// <summary>Atributo, Indica si se tomo los trigliceridos en los ultimos 30 días</summary>
        public int Trigliceridos30Dias
        {
            get { return _Trigliceridos30Dias; }
            set { _Trigliceridos30Dias = value; }
        }
        /// <summary>Atributo, Indica la fecha en la se tomo los trigliceridos en los ultimos 30 días</summary>
        public string FechaTrigliceridos30Dias
        {
            get { return _FechaTrigliceridos30Dias; }
            set { _FechaTrigliceridos30Dias = value; }
        }
        /// <summary>Atributo, Indica el valor de los trigliceridos en los ultimos 30 días</summary>
        public int ValorTrigliceridos30DiasHombres
        {
            get { return _ValorTrigliceridos30DiasHombres; }
            set { _ValorTrigliceridos30DiasHombres = value; }
        }
        /// <summary>Atributo, Indica el valor los trigliceridos en los ultimos 30 días</summary>
        public int ValorTrigliceridos30DiasMujeres
        {
            get { return _ValorTrigliceridos30DiasMujeres; }
            set { _ValorTrigliceridos30DiasMujeres = value; }
        }


        #endregion

        //Prototipo0-DMA-12/09/2018-Ini
        #region HistoriaLaboral   
        #region Empresas
        public int HistLabIdGirosEmpresa1 { get; set; }
        public int HistLabAniosEmpresa1 { get; set; }
        public int HistLabMesesEmpresa1 { get; set; }
        public string HistLabPuestoEmpresa1 { get; set; }
        public int HistLabIdGirosEmpresa2 { get; set; }
        public int HistLabAniosEmpresa2 { get; set; }
        public int HistLabMesesEmpresa2 { get; set; }
        public string HistLabPuestoEmpresa2 { get; set; }
        public int HistLabIdGirosEmpresa3 { get; set; }
        public int HistLabAniosEmpresa3 { get; set; }
        public int HistLabMesesEmpresa3 { get; set; }
        public string HistLabPuestoEmpresa3 { get; set; }
        public int HistLabIdGirosEmpresa4 { get; set; }
        public int HistLabAniosEmpresa4 { get; set; }
        public int HistLabMesesEmpresa4 { get; set; }
        public string HistLabPuestoEmpresa4 { get; set; }
        public int HistLabIdGirosEmpresa5 { get; set; }
        public int HistLabAniosEmpresa5 { get; set; }
        public int HistLabMesesEmpresa5 { get; set; }
        public string HistLabPuestoEmpresa5 { get; set; }


        #endregion
        #region Datos
        public bool HistLabFisicoRuido { get; set; }
        public int HistLabAniosFisicoRuido { get; set; }
        public int HistLabMesesFisicoRuido { get; set; }
        public string HistLabComentariosFisicoRuido { get; set; }
        public bool HistLabFisicoIluminacion { get; set; }
        public int HistLabAniosFisicoIluminacion { get; set; }
        public int HistLabMesesFisicoIluminacion { get; set; }
        public string HistLabComentariosFisicoIluminacion { get; set; }
        public bool HistLabFisicoVibraciones { get; set; }
        public int HistLabAniosFisicoVibraciones { get; set; }
        public int HistLabMesesFisicoVibraciones { get; set; }
        public string HistLabComentariosFisicoVibraciones { get; set; }
        public bool HistLabFisicoRadiacion { get; set; }
        public int HistLabAniosFisicoRadiacion { get; set; }
        public int HistLabMesesFisicoRadiacion { get; set; }
        public string HistLabComentariosFisicoRadiacion { get; set; }
        public bool HistLabFisicoTempExtremas { get; set; }
        public int HistLabAniosFisicoTempExtremas { get; set; }
        public int HistLabMesesFisicoTempExtremas { get; set; }
        public string HistLabComentariosFisicoTempExtremas { get; set; }
        public bool HistLabFisicoOtro1 { get; set; }
        public int HistLabAniosFisicoOtro1 { get; set; }
        public int HistLabMesesFisicoOtro1 { get; set; }
        public string HistLabComentariosFisicoOtro1 { get; set; }
        public bool HistLabFisicoOtro2 { get; set; }
        public int HistLabAniosFisicoOtro2 { get; set; }
        public int HistLabMesesFisicoOtro2 { get; set; }
        public string HistLabComentariosFisicoOtro2 { get; set; }
        public bool HistLabFisicoOtro3 { get; set; }
        public int HistLabAniosFisicoOtro3 { get; set; }
        public int HistLabMesesFisicoOtro3 { get; set; }
        public string HistLabComentariosFisicoOtro3 { get; set; }
        public bool HistLabFisicoOtro4 { get; set; }
        public int HistLabAniosFisicoOtro4 { get; set; }
        public int HistLabMesesFisicoOtro4 { get; set; }
        public string HistLabComentariosFisicoOtro4 { get; set; }
        public bool HistLabFisicoOtro5 { get; set; }
        public int HistLabAniosFisicoOtro5 { get; set; }
        public int HistLabMesesFisicoOtro5 { get; set; }
        public string HistLabComentariosFisicoOtro5 { get; set; }
        public bool HistLabQuimicoPolvos { get; set; }
        public int HistLabAniosQuimicoPolvos { get; set; }
        public int HistLabMesesQuimicoPolvos { get; set; }
        public string HistLabComentariosQuimicoPolvos { get; set; }
        public bool HistLabQuimicoHumos { get; set; }
        public int HistLabAniosQuimicoHumos { get; set; }
        public int HistLabMesesQuimicoHumos { get; set; }
        public string HistLabComentariosQuimicoHumos { get; set; }
        public bool HistLabQuimicoRociosNeblina { get; set; }
        public int HistLabAniosQuimicoRociosNeblina { get; set; }
        public int HistLabMesesQuimicoRociosNeblina { get; set; }
        public string HistLabComentariosQuimicoRociosNeblina { get; set; }
        public bool HistLabQuimicoVapores { get; set; }
        public int HistLabAniosQuimicoVapores { get; set; }
        public int HistLabMesesQuimicoVapores { get; set; }
        public string HistLabComentariosQuimicoVapores { get; set; }
        public bool HistLabQuimicoGases { get; set; }
        public int HistLabAniosQuimicoGases { get; set; }
        public int HistLabMesesQuimicoGases { get; set; }
        public string HistLabComentariosQuimicoGases { get; set; }
        public bool HistLabQuimicoOtro1 { get; set; }
        public int HistLabAniosQuimicoOtro1 { get; set; }
        public int HistLabMesesQuimicoOtro1 { get; set; }
        public string HistLabComentariosQuimicoOtro1 { get; set; }
        public bool HistLabQuimicoOtro2 { get; set; }
        public int HistLabAniosQuimicoOtro2 { get; set; }
        public int HistLabMesesQuimicoOtro2 { get; set; }
        public string HistLabComentariosQuimicoOtro2 { get; set; }
        public bool HistLabQuimicoOtro3 { get; set; }
        public int HistLabAniosQuimicoOtro3 { get; set; }
        public int HistLabMesesQuimicoOtro3 { get; set; }
        public string HistLabComentariosQuimicoOtro3 { get; set; }
        public bool HistLabQuimicoOtro4 { get; set; }
        public int HistLabAniosQuimicoOtro4 { get; set; }
        public int HistLabMesesQuimicoOtro4 { get; set; }
        public string HistLabComentariosQuimicoOtro4 { get; set; }
        public bool HistLabQuimicoOtro5 { get; set; }
        public int HistLabAniosQuimicoOtro5 { get; set; }
        public int HistLabMesesQuimicoOtro5 { get; set; }
        public string HistLabComentariosQuimicoOtro5 { get; set; }
        public bool HistLabBiologicosBacteria { get; set; }
        public int HistLabAniosBiologicosBacteria { get; set; }
        public int HistLabMesesBiologicosBacteria { get; set; }
        public string HistLabComentariosBiologicosBacteria { get; set; }
        public bool HistLabBiologicosVirus { get; set; }
        public int HistLabAniosBiologicosVirus { get; set; }
        public int HistLabMesesBiologicosVirus { get; set; }
        public string HistLabComentariosBiologicosVirus { get; set; }
        public bool HistLabBiologicosParasitos { get; set; }
        public int HistLabAniosBiologicosParasitos { get; set; }
        public int HistLabMesesBiologicosParasitos { get; set; }
        public string HistLabComentariosBiologicosParasitos { get; set; }
        public bool HistLabBiologicosOtro1 { get; set; }
        public int HistLabAniosBiologicosOtro1 { get; set; }
        public int HistLabMesesBiologicosOtro1 { get; set; }
        public string HistLabComentariosBiologicosOtro1 { get; set; }
        public bool HistLabBiologicosOtro2 { get; set; }
        public int HistLabAniosBiologicosOtro2 { get; set; }
        public int HistLabMesesBiologicosOtro2 { get; set; }
        public string HistLabComentariosBiologicosOtro2 { get; set; }
        public bool HistLabBiologicosOtro3 { get; set; }
        public int HistLabAniosBiologicosOtro3 { get; set; }
        public int HistLabMesesBiologicosOtro3 { get; set; }
        public string HistLabComentariosBiologicosOtro3 { get; set; }
        public bool HistLabBiologicosOtro4 { get; set; }
        public int HistLabAniosBiologicosOtro4 { get; set; }
        public int HistLabMesesBiologicosOtro4 { get; set; }
        public string HistLabComentariosBiologicosOtro4 { get; set; }
        public bool HistLabBiologicosOtro5 { get; set; }
        public int HistLabAniosBiologicosOtro5 { get; set; }
        public int HistLabMesesBiologicosOtro5 { get; set; }
        public string HistLabComentariosBiologicosOtro5 { get; set; }
        public bool HistLabErgonomicosMovsRepetitivos { get; set; }
        public int HistLabAniosErgonomicosMovsRepetitivos { get; set; }
        public int HistLabMesesErgonomicosMovsRepetitivos { get; set; }
        public string HistLabComentariosErgonomicosMovsRepetitivos { get; set; }
        public bool HistLabErgonomicosPosturasForzadas { get; set; }
        public int HistLabAniosErgonomicosPosturasForzadas { get; set; }
        public int HistLabMesesErgonomicosPosturasForzadas { get; set; }
        public string HistLabComentariosErgonomicosPosturasForzadas { get; set; }
        public bool HistLabErgonomicosManejoManCajas { get; set; }
        public int HistLabAniosErgonomicosManejoManCajas { get; set; }
        public int HistLabMesesErgonomicosManejoManCajas { get; set; }
        public string HistLabComentariosErgonomicosManejoManCajas { get; set; }
        public bool HistLabErgonomicosBidepestacionProlongada { get; set; }
        public int HistLabAniosErgonomicosBidepestacionProlongada { get; set; }
        public int HistLabMesesErgonomicosBidepestacionProlongada { get; set; }
        public string HistLabComentariosErgonomicosBidepestacionProlongada { get; set; }
        public bool HistLabErgonomicosSedestacionProlongada { get; set; }
        public int HistLabAniosErgonomicosSedestacionProlongada { get; set; }
        public int HistLabMesesErgonomicosSedestacionProlongada { get; set; }
        public string HistLabComentariosErgonomicosSedestacionProlongada { get; set; }
        public bool HistLabErgonomicosOtro1 { get; set; }
        public int HistLabAniosErgonomicosOtro1 { get; set; }
        public int HistLabMesesErgonomicosOtro1 { get; set; }
        public string HistLabComentariosErgonomicosOtro1 { get; set; }
        public bool HistLabErgonomicosOtro2 { get; set; }
        public int HistLabAniosErgonomicosOtro2 { get; set; }
        public int HistLabMesesErgonomicosOtro2 { get; set; }
        public string HistLabComentariosErgonomicosOtro2 { get; set; }
        public bool HistLabErgonomicosOtro3 { get; set; }
        public int HistLabAniosErgonomicosOtro3 { get; set; }
        public int HistLabMesesErgonomicosOtro3 { get; set; }
        public string HistLabComentariosErgonomicosOtro3 { get; set; }
        public bool HistLabErgonomicosOtro4 { get; set; }
        public int HistLabAniosErgonomicosOtro4 { get; set; }
        public int HistLabMesesErgonomicosOtro4 { get; set; }
        public string HistLabComentariosErgonomicosOtro4 { get; set; }
        public bool HistLabErgonomicosOtro5 { get; set; }
        public int HistLabAniosErgonomicosOtro5 { get; set; }
        public int HistLabMesesErgonomicosOtro5 { get; set; }
        public string HistLabComentariosErgonomicosOtro5 { get; set; }
        public bool HistLabPsicosocialEstres { get; set; }
        public int HistLabAniosPsicosocialEstres { get; set; }
        public int HistLabMesesPsicosocialEstres { get; set; }
        public string HistLabComentariosPsicosocialEstres { get; set; }
        public bool HistLabPsicosocialBurnot { get; set; }
        public int HistLabAniosPsicosocialBurnot { get; set; }
        public int HistLabMesesPsicosocialBurnot { get; set; }
        public string HistLabComentariosPsicosocialBurnot { get; set; }
        public bool HistLabPsicosocialMobbing { get; set; }
        public int HistLabAniosPsicosocialMobbing { get; set; }
        public int HistLabMesesPsicosocialMobbing { get; set; }
        public string HistLabComentariosPsicosocialMobbing { get; set; }
        public bool HistLabPsicosocialTrabajoxTurnos { get; set; }
        public int HistLabAniosPsicosocialTrabajoxTurnos { get; set; }
        public int HistLabMesesPsicosocialTrabajoxTurnos { get; set; }
        public string HistLabComentariosPsicosocialTrabajoxTurnos { get; set; }
        public bool HistLabPsicosocialOtro1 { get; set; }
        public int HistLabAniosPsicosocialOtro1 { get; set; }
        public int HistLabMesesPsicosocialOtro1 { get; set; }
        public string HistLabComentariosPsicosocialOtro1 { get; set; }
        public bool HistLabPsicosocialOtro2 { get; set; }
        public int HistLabAniosPsicosocialOtro2 { get; set; }
        public int HistLabMesesPsicosocialOtro2 { get; set; }
        public string HistLabComentariosPsicosocialOtro2 { get; set; }
        public bool HistLabPsicosocialOtro3 { get; set; }
        public int HistLabAniosPsicosocialOtro3 { get; set; }
        public int HistLabMesesPsicosocialOtro3 { get; set; }
        public string HistLabComentariosPsicosocialOtro3 { get; set; }
        public bool HistLabPsicosocialOtro4 { get; set; }
        public int HistLabAniosPsicosocialOtro4 { get; set; }
        public int HistLabMesesPsicosocialOtro4 { get; set; }
        public string HistLabComentariosPsicosocialOtro4 { get; set; }
        public bool HistLabPsicosocialOtro5 { get; set; }
        public int HistLabAniosPsicosocialOtro5 { get; set; }
        public int HistLabMesesPsicosocialOtro5 { get; set; }
        public string HistLabComentariosPsicosocialOtro5 { get; set; }
        #endregion
        #region checklistlaboratorio
        public bool HistLabCheckBiometriaHematica { get; set; }
        public bool HistLabCheckGrupoSanguineo { get; set; }
        public bool HistLabCheckQuimicaSanguinea { get; set; }
        public bool HistLabCheckCoproparasitoscopio { get; set; }
        public bool HistLabCheckEgo { get; set; }
        public bool HistLabCheckExudadoFaringeo { get; set; }
        public bool HistLabCheckReaccionesFebriles { get; set; }
        public bool HistLabCheckTeleTorax { get; set; }
        public bool HistLabCheckRxColumnaLumbar { get; set; }
        public bool HistLabCheckAudiometria { get; set; }
        public bool HistLabCheckEspirometria { get; set; }
        public bool HistLabCheckElectrocardiograma { get; set; }
        public bool HistLabCheckPruebaEsfuerzo { get; set; }
        public bool HistLabCheckAgudezaVisual { get; set; }
        public bool HistLabCheckToxicologia { get; set; }
        public bool HistLabCheckPerfilDrogas { get; set; }
        public bool HistLabCheckDesintometriaOsea { get; set; }
        public bool HistLabCheckEcografia { get; set; }
        public bool HistLabCheckPruebasEgonometricas { get; set; }
        public bool HistLabCheckEvaluacionPsicologica { get; set; }
        public bool HistLabCheckOtro1 { get; set; }
        public bool HistLabCheckOtro2 { get; set; }
        public bool HistLabCheckOtro3 { get; set; }

        #endregion
        #endregion
        //Prototipo0-DMA-12/09/2018-Fin

        #endregion

        #region Enumeration

        /// <summary>
        /// Enumeración, lista los tipos de consulta
        /// </summary>
        public enum EnumTiposConsulta
        {
            PrimeraVez = 1,
            Consulta = 2,
            Control = 3,
            Transcripcion = 4
        }
        /// <summary>
        /// Enumeración, lista los tipos de Diagnosticos que existen en las formas
        /// </summary>
        public enum EnumTiposDiagnosticos
        {
            IdDiagnosticoExamenVisual = 1,
            IdDiagnosticoNutricional = 2,
            IdDiagnosticoIncapacidad = 3,
            IdDiagnosticoHospitalizacion1 = 4,
            IdDiagnosticoHospitalizacion2 = 5,
            IdDiagnosticoHospitalizacion3 = 6,
            IdDiagnosticoHospitalizacion4 = 7,
            IdDiagnosticoTrastorno = 8

        }
        #endregion

        #region Methods

        public Consulta()
        {
            this.Gestaciones = -1;
            this.Partos = -1;
            this.Cesareas = -1;
            this.Abortos = -1;
            this.Vivos = -1;
        }

        public int suma(int a, int b)
        {

            int resulado;
            resulado = a + b;

            return resulado;
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultConsulta()
        {
            DataSet dsList;
            try
            {
                dsList = this.List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultConsultaCompleta(int p_id_beneficiario, int p_id_empleado, object p_fecha_inicio, object p_fecha_fin)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("ConsultaCompleta", p_id_beneficiario, p_id_empleado, p_fecha_inicio, p_fecha_fin, this.IdConsulta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }


        /// <summary>
        /// Método para la consulta de la historia clínica
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultHistoriaClinica(int p_id_beneficiario, int p_id_empleado)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("HistoriaClinica", p_id_beneficiario, p_id_empleado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }



        /// <summary>
        /// Método para la inserción
        /// </summary>
        /// <returns>Id insertado</returns>
        public long InsertConsulta()
        {
            try
            {
                this.BeginTransaction();

                this.IdConsulta = Convert.ToInt32(this.Insert());
                this.Insert("ConsultaAntecedentes");
                this.Insert("ConsultaExamenFisico");
                this.Insert("ConsultaRevisionSistemas");
                this.Insert("ConsultaHabitos");
                this.Insert("ConsultaPruebasBiometricas");
                //DMA-Prototipo0-20/09/2018-Historia Laboral
                this.Insert("ConsultaHistoriaLaboral");

                if (this.ConsultaDiagnosticos != null)
                {
                    foreach (ConsultaDiagnosticos objConsultaDiagnostico in this.ConsultaDiagnosticos)
                    {
                        objConsultaDiagnostico.objTransaction = this.objTransaction;
                        objConsultaDiagnostico.IdConsulta = this.IdConsulta;
                        objConsultaDiagnostico.InsertConsultaDiagnosticos();

                        //Adicionar a catalogo de clasificación
                        /*if (objConsultaDiagnostico.IdDiagnosticoClasificacion != 0)
                        {
                            Diagnosticos objDiagnostico = new Diagnosticos();
                            objDiagnostico.IdDiagnostico = objConsultaDiagnostico.IdDiagnostico;
                            objDiagnostico.IdDiagnosticoClasificacion = objConsultaDiagnostico.IdDiagnosticoClasificacion;
                            objDiagnostico.UpdateDiagnosticoClasificacion();
                        }*/
                    }
                }

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }

            return this.IdConsulta;
        }

        /// <summary>
        /// Método para la modificación
        /// </summary>
        public void UpdateConsulta()
        {
            try
            {
                this.BeginTransaction();

                this.Update();
                this.Update("ConsultaAntecedentes");
                this.Update("ConsultaExamenFisico");
                this.Update("ConsultaRevisionSistemas");
                this.Update("ConsultaHabitos");
                this.Update("ConsultaPruebasBiometricas");
                //DMA-Prototipo0-20/09/2018-Historia Laboral
                this.Update("ConsultaHistoriaLaboral");

                this.Delete("ConsultaDiagnosticos", this.IdConsulta);

                if (this.ConsultaDiagnosticos != null)
                {
                    foreach (ConsultaDiagnosticos objConsultaDiagnostico in this.ConsultaDiagnosticos)
                    {
                        objConsultaDiagnostico.objTransaction = this.objTransaction;
                        objConsultaDiagnostico.IdConsulta = this.IdConsulta;
                        objConsultaDiagnostico.InsertConsultaDiagnosticos();
                    }
                }

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Método para la modificación
        /// </summary>
        public void UpdateConsultaObservaciones()
        {
            try
            {
                this.Update("ConsultaObservaciones", this.IdConsulta, this.ObservacionesGenerales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Método para la eliminación
        /// </summary>
        public void DeleteConsulta()
        {
            try
            {
                this.Delete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la carga de un objeto de este tipo
        /// </summary>
        public void GetConsulta()
        {
            try
            {
                this.Consult();
                this.Consult("ConsultaAntecedentes");
                this.Consult("ConsultaExamenFisico");
                this.Consult("ConsultaRevisionSistemas");
                this.Consult("ConsultaHabitos");
                this.Consult("ConsultaPruebasBiometricas");
                //DMA-Prototipo-27/09/2018
                this.Consult("ConsultaHistoriaLaboral");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>Devuelve un escalar con el dato de la consulta</returns>
        public string GetEscalarAlimentacionInadecuada(int opciones)
        {
            string Escalar;
            try
            {
                Escalar = this.EjecutarExecutescalarString("GetEscalarConsultaAlimentacionInadecuada", opciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Escalar;
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultConsultaBusqueda(object dateFrom, object dateUntil, int idUser)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("ConsultaBusqueda", dateFrom, dateUntil, idUser, this.IdConsulta, this.Empresa_id, this.Id_empleado, this.Beneficiario_id, this.ConsecutivoNombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        /// <summary>
        /// Método para la carga de un objeto de este tipo
        /// </summary>
        public int GetCantidadConsulta()
        {
            try
            {
                return Convert.ToInt32(this.ExecuteProcedure("GetCantidadConsulta", this.Id_empleado, this.Beneficiario_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Función que indica si existe la consulta en las tabla de estilo de vida
        /// </summary>
        /// <returns></returns>
        public bool ExisteConsultaEstiloVida()
        {
            bool bolExiste = false;
            DataSet dsList = new DataSet();
            try
            {
                dsList = this.List("ExisteConsultaEstiloVida");
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    bolExiste = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolExiste;
        }

        /// <summary>
        /// Función que indica si existe la consulta en las tabla de estamos contigo
        /// </summary>
        /// <returns></returns>
        public bool ExisteConsultaEstamosContigo()
        {
            bool bolExiste = false;
            DataSet dsList = new DataSet();
            try
            {
                dsList = this.List("ExisteEstamosContigo");
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    bolExiste = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolExiste;
        }


        /// <summary>
        /// Método para la inserción del formulario de estilo de vida
        /// </summary>
        /// <returns>Id insertado</returns>
        public long InsertConsultaEstiloVida()
        {
            try
            {
                this.BeginTransaction();


                this.Insert("ConsultaEstiloVidaParte1");
                this.Insert("ConsultaEstiloVidaParte2");
                this.Insert("ConsultaEstiloVidaParte3");
                if (this.ConsultaOpcion != null)
                {
                    foreach (ConsultaOpcion objConsultaOpcion in this.ConsultaOpcion)
                    {
                        objConsultaOpcion.objTransaction = this.objTransaction;
                        objConsultaOpcion.IdConsulta = this.IdConsulta;
                        objConsultaOpcion.InsertConsultaOpcion();
                    }
                }


                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }

            return this.IdConsulta;
        }

        /// <summary>
        /// Método para la inserción del formulario de estamos contigo
        /// </summary>
        /// <returns>Id insertado</returns>
        public long InsertConsultaEstamosContigo()
        {
            try
            {
                this.BeginTransaction();


                this.Insert("ConsultaEstamosContigo");


                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }

            return this.IdConsulta;
        }

        /// <summary>
        /// Método para la carga de un objeto del formulario de estilo de vida
        /// </summary>
        public void GetConsultaEstiloVida()
        {
            try
            {
                this.Consult("ConsultaEstiloVidaParte1");
                this.Consult("ConsultaEstiloVidaParte2");
                this.Consult("ConsultaEstiloVidaParte3");
                //OPCIONES
                ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                objConsultaOpcion.IdConsulta = this.IdConsulta;
                DataTable dt = objConsultaOpcion.ConsultConsultaOpcion().Tables[0];
                ArrayList arrConsultaOpcion = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    ConsultaOpcion objConsultaOpcionFila = new ConsultaOpcion();
                    objConsultaOpcionFila.IdConsulta = this.IdConsulta;
                    objConsultaOpcionFila.IdPreguntaRespuestaPadre = int.Parse(dr["IdPreguntaRespuestaPadre"].ToString());
                    objConsultaOpcionFila.IdPreguntaRespuesta = int.Parse(dr["IdPreguntaRespuesta"].ToString());
                    arrConsultaOpcion.Add(objConsultaOpcionFila);
                }

                this.ConsultaOpcion = arrConsultaOpcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la carga de un objeto del formulario de Estamos Contigo
        /// </summary>
        public void GetConsultaEstamosContigo()
        {
            try
            {
                this.Consult("ConsultaEstamosContigo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la modificación del formulario de estilo de vida
        /// </summary>
        public void UpdateConsultaEstiloVida()
        {
            try
            {
                this.BeginTransaction();

                this.Update("ConsultaEstiloVidaParte1");
                this.Update("ConsultaEstiloVidaParte2");
                this.Update("ConsultaEstiloVidaParte3");
                this.Delete("ConsultaOpcion", this.IdConsulta, "EstiloVida");
                if (this.ConsultaOpcion != null)
                {
                    foreach (ConsultaOpcion objConsultaOpcion in this.ConsultaOpcion)
                    {
                        objConsultaOpcion.objTransaction = this.objTransaction;
                        objConsultaOpcion.IdConsulta = this.IdConsulta;
                        objConsultaOpcion.InsertConsultaOpcion();
                    }
                }
                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Método para la modificación del formulario de estamos contigo
        /// </summary>
        public void UpdateConsultaEstamosContigo()
        {
            try
            {
                this.BeginTransaction();

                this.Update("ConsultaEstamosContigo");

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Función que indica si existe la consulta en las tabla de nutrición
        /// </summary>
        /// <returns></returns>
        public bool ExisteConsultaNutricion()
        {
            bool bolExiste = false;
            DataSet dsList = new DataSet();
            try
            {
                dsList = this.List("ExisteConsultaNutricion");
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    bolExiste = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolExiste;
        }
        /// <summary>
        /// Método para la inserción del formulario de nutrición
        /// </summary>
        /// <returns>Id insertado</returns>
        public long InsertConsultaNutricion()
        {
            try
            {
                this.BeginTransaction();


                this.Insert("ConsultaNutricion");
                if (this.ConsultaOpcion != null)
                {
                    foreach (ConsultaOpcion objConsultaOpcion in this.ConsultaOpcion)
                    {
                        objConsultaOpcion.objTransaction = this.objTransaction;
                        objConsultaOpcion.IdConsulta = this.IdConsulta;
                        objConsultaOpcion.InsertConsultaOpcion();
                    }
                }


                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }

            return this.IdConsulta;
        }
        /// <summary>
        /// Método para la carga de un objeto del formulario de nutrición
        /// </summary>
        public void GetConsultaNutricion()
        {
            try
            {

                this.Consult("ConsultaNutricion");
                //OPCIONES
                ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                objConsultaOpcion.IdConsulta = this.IdConsulta;
                DataTable dt = objConsultaOpcion.ConsultConsultaOpcion().Tables[0];
                ArrayList arrConsultaOpcion = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    ConsultaOpcion objConsultaOpcionFila = new ConsultaOpcion();
                    objConsultaOpcionFila.IdConsulta = this.IdConsulta;
                    objConsultaOpcionFila.IdPreguntaRespuestaPadre = int.Parse(dr["IdPreguntaRespuestaPadre"].ToString());
                    objConsultaOpcionFila.IdPreguntaRespuesta = int.Parse(dr["IdPreguntaRespuesta"].ToString());
                    arrConsultaOpcion.Add(objConsultaOpcionFila);
                }
                this.ConsultaOpcion = arrConsultaOpcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Método para la modificación del formulario de nutrición
        /// </summary>
        public void UpdateConsultaNutricion()
        {
            try
            {
                this.BeginTransaction();

                this.Update("ConsultaNutricion");
                this.Delete("ConsultaOpcion", this.IdConsulta, "Nutricion");
                if (this.ConsultaOpcion != null)
                {
                    foreach (ConsultaOpcion objConsultaOpcion in this.ConsultaOpcion)
                    {
                        objConsultaOpcion.objTransaction = this.objTransaction;
                        objConsultaOpcion.IdConsulta = this.IdConsulta;
                        objConsultaOpcion.InsertConsultaOpcion();
                    }
                }
                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Consultoria: PortoMX
        /// Fecha: 20/07/2010
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Finaliza la historoia clínica
        /// </summary>
        public void FinalizarConsulta()
        {
            try
            {
                this.BeginTransaction();

                this.Update("ConsultaFinalizar");

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        public int GetIdSedeCita()
        {
            try
            {
                return Convert.ToInt32(this.ExecuteProcedure("GetCitaSedeById", this.cita_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ListConsultasMorbilidad(object dateFrom, object dateUntil, string empresas, string sedes, string usuarios, string medicos)
        {
            DataSet dsList;

            try
            {
                dsList = this.List("ConsultasMorbilidad", dateFrom, dateUntil, empresas, sedes, usuarios, medicos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }


        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ListConsultasRiesgos(object dateFrom, object dateUntil, string empresas, string sedes, string usuarios, string medicos, string tiposconsulta, int beneficiarios)
        {
            DataSet dsList;

            try
            {
                dsList = this.List("ConsultasRiesgos", dateFrom, dateUntil, empresas, sedes, usuarios, medicos, tiposconsulta, beneficiarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }



        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ListConsultasRiesgosDetalle(object dateFrom, object dateUntil, string empresas, string sedes, string usuarios, string medicos, string tiposconsulta, int beneficiarios)
        {
            DataSet dsList;

            try
            {
                dsList = this.List("ConsultasRiesgosDetalle", dateFrom, dateUntil, empresas, sedes, usuarios, medicos, tiposconsulta, beneficiarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        /// <summary>
        /// Método para obtener los datos de la consulta anterior realizada al empleado
        /// </summary>
        public void GetUltimaConsulta(long p_idEmpleado, long p_idBeneficiario)
        {
            try
            {
                this.Consult("ConsultaUltima", p_idEmpleado, p_idBeneficiario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para obtener los datos de la consulta anterior realizada al empleado
        /// </summary>
        public void GetUltimaConsultaNuticion(long p_idEmpleado, long p_idBeneficiario)
        {
            try
            {
                this.Consult("ConsultaUltimaNutricion", p_idEmpleado, p_idBeneficiario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para obtener los datos de la consulta anterior realizada al empleado
        /// Autor: Ricardo Silva
        /// Fecha: 09/07/2012
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta WM</returns>

        public DataSet ConsultAntiguoPregunta(int IdPreguntaRespuesta)
        {
            DataSet dsList;

            try
            {
                dsList = this.List("PreguntaRespuestaAntiguo", IdPreguntaRespuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsList;
        }

        // <summary>
        // Descripción: Método para obtener los datos de la consulta anterior realizada al empleado
        // Autor: RAM
        // Fecha: 21/09/2015
        // </summary>
        // <returns>DataSet con los resultados de la consulta Historial</returns>

        public DataSet consultaHistorial(int IdEmpleado)
        {
            DataSet dsList;
            try
            {
                dsList = this.consultarProc("GetHistorialConsultas", IdEmpleado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        #endregion


    }
}


