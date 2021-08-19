/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2010 by Mercer
'===============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;
using Mercer.Tpa.Agenda.Web.Sistema;
using Mercer.Tpa.Agenda.Web.UI.Utils;

namespace Mercer.Tpa.Agenda.Web.UI.ConfiguracionHorario
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Módulo configurador de horarios
    /// </summary>
    public partial class ConfigurarHorarioSede : PaginaBaseAgenda
    {
        #region Variables privadas

        private DateTime _fechaActual;
        private string _postbackRef;
        private InfoPrestador _prestador;

        #endregion

        #region Eventos ciclo pagina

        /// <summary>
        /// En el viewstate cargamos la fecha de referencia actual para saber que semana mostrar
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            object[] totalState = null;
            if (savedState != null)
            {
                totalState = (object[]) savedState;
                if (totalState.Length != 2)
                {
                    throw new ApplicationException("Error cargando Viewstate");
                }
                // Load base state.
                base.LoadViewState(totalState[0]);
                // Load extra information specific to this control.
                if (totalState[1] != null)
                {
                    _fechaActual = (DateTime) totalState[1];
                }
            }
        }

        /// <summary>
        /// En el viewstate guardamos la fecha de referencia actual
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            object baseState = base.SaveViewState();
            var totalState = new object[2];
            totalState[0] = baseState;
            totalState[1] = _fechaActual;
            return totalState;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
                RegistrarFuncionClientePostback();
                CargarMedico();
                if (!IsPostBack)
                {
                    _fechaActual = DateTime.Now;
                    CargarListaSedes();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.Params["fechaActual"]))
                    {
                        _fechaActual = DateTime.Parse(Request.Params["fechaActual"]);
                    }
                }

                //TODO: Usar la hora de la zona horaria de la empresa para seleccionar la fecha correcta
                CargarHorarioMedico(_prestador.Id, _fechaActual);
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }

        /// <summary>
        /// Carga la información del médico
        /// </summary>
        private void CargarMedico()
        {
            var prestadorRep = new PrestadoresDataRepository();
            WebUtils.RequerirParametros("IdMedico");
            int idPrestador = Convert.ToInt32(Request.Params["IdMedico"]);
            _prestador = prestadorRep.GetPrestadorById(idPrestador);
            if (_prestador == null)
            {
                throw new ApplicationException("No se encontró médico con ID:" + idPrestador);
            }
            lblMedico.Text = Server.HtmlEncode(_prestador.Name);
            if(_prestador.Especialidad!=null)
            {
                lblEspecialidad.Text = Server.HtmlEncode(_prestador.Especialidad.Nombre ?? "");
            }
        }

        /// <summary>
        /// Registra la funcion en javascript que permite hacer postback
        /// </summary>
        private void RegistrarFuncionClientePostback()
        {
            _postbackRef = ClientScript.GetPostBackEventReference(this, "");
            var jsBuilder = new StringBuilder();
            jsBuilder.Append("function hacerPostback(){");
            jsBuilder.Append(_postbackRef);
            jsBuilder.Append("}");
            ClientScript.RegisterStartupScript(GetType(), "hacerPostback", jsBuilder.ToString(), true);
        }

        #endregion

        #region Carga listas

        /// <summary>
        /// Obtener la lista de sedes a las que tiene acceso el usuario
        /// </summary>
        private void CargarListaSedes()
        {
     
            var sedesRep = new SedesDataRepository();
            IEnumerable<InfoSede> sedes = sedesRep.GetSedesByUsuarioEmpresa(SessionManager.IdUser,SessionManager.IdEmpresa);
            ListaSedes.DataSource = sedes;
            ListaSedes.DataTextField = "Nombre";
            ListaSedes.DataValueField = "Id";
            ListaSedes.DataBind();
        }

        #endregion
        
        #region Eventos Controles



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                CrearIntervalos();
                //Refrescar
                CargarHorarioMedico(_prestador.Id, _fechaActual);
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }

        protected void BtnDeleteIntervalo_Click(object sender, EventArgs e)
        {
            try
            {
                EliminarIntervalos();
                //Refrescar
                CargarHorarioMedico(_prestador.Id, _fechaActual);
            }
            catch (Exception ex)
            {
                ctrError.MostrarError(ex);
            }
        }

        #endregion


        #region Metodos privados
        /// <summary>
        /// Cargar el horario para un medico especifico y la empresa actual
        /// </summary>
        private void CargarHorarioMedico(int idMedico, DateTime fechaReferencia)
        {
            var horarioMgr = new HorarioMedicoManager(new HorarioRepository());
            List<IntervaloHorarioSede> intervalosMedico = horarioMgr.GetHorarioMedicoParaSemana(idMedico,
                                                                                                fechaReferencia);
            ctrHorarioSemana.Intervalos = intervalosMedico;
            ctrHorarioSemana.FechaReferencia = fechaReferencia;
        }

        /// <summary>
        /// Agrega un intervalo para un medico, en una sede especifica.
        /// </summary>
        private void CrearIntervalos()
        {
            #region Obtener valores del request y controles

            var horarioMgr = new HorarioMedicoManager(new HorarioRepository());
            var sedesRep = new SedesDataRepository();
            WebUtils.RequerirParametros("horaInicio", "horaFin", "fechaVigenciaDesde", "vigenciaLimite");
            DateTime fechaVigenciaDesde = ConfiguracionAgendaManager.ParseDate(Request.Form["fechaVigenciaDesde"]);
            string strHoraInicio = Request.Form["horaInicio"];
            string strHoraFin = Request.Form["horaFin"];
            //El formato de horaInicio y fin es: hora-minutos
            string[] partesHoraInicio = strHoraInicio.Split('-');
            string[] partesHoraFin = strHoraFin.Split('-');
            if (partesHoraInicio.Length != 2)
                throw new ApplicationException("Error de formato en parametro horaInicio");
            if (partesHoraFin.Length != 2)
                throw new ApplicationException("Error de formato en parametro horaFin");

            int horasInicio = int.Parse(partesHoraInicio[0]);
            int minutosInicio = int.Parse(partesHoraInicio[1]);
            int horasFin = int.Parse(partesHoraFin[0]);
            int minutosFin = int.Parse(partesHoraFin[1]);

            if (ListaSedes.SelectedValue == null)
            {
                ctrMensaje.MostrarMensaje("Debe seleccionar una sede", EnumUserMessage.Advertencia);
                return;
            }
            int idSede = Convert.ToInt32(ListaSedes.SelectedValue);
            InfoSede sede = sedesRep.GetSedeById(idSede);
            DateTime? fechaVigenciaFinal;
            //Obtener fecha vigencia limite.
            if (string.IsNullOrEmpty(Request.Form["vigenciaLimite"]))
            {
                throw new ApplicationException("Parámetro requerido vigenciaLimite");
            }


            var vigenciaIntervalo = (OpcionVigenciaIntervalo)Convert.ToInt32(Request.Form["vigenciaLimite"]);
            switch (vigenciaIntervalo)
            {
                case OpcionVigenciaIntervalo.Siempre:
                    fechaVigenciaFinal = null;
                    break;
                case OpcionVigenciaIntervalo.Limite:
                    //El usuario especifico fecha final de vigencia
                    if (string.IsNullOrEmpty(Request.Form["fechaVigenciaLimite"]))
                    {
                        throw new ApplicationException(
                            "Requerido parametro fecha de vigencia final: fechaVigenciaLimite");
                    }
                    fechaVigenciaFinal = ConfiguracionAgendaManager.ParseDate(Request.Form["fechaVigenciaLimite"]);
                    break;
                default:
                    throw new ApplicationException("Opcion no soportada:" + vigenciaIntervalo);
            }

            #endregion

            //Iterar sobre los días de la semana en que se quieren crear intervalos e irlos creando
            foreach (ListItem item in ListaDias.Items)
            {
                if (item.Selected)
                {
                    var diaSemana = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.Value);
                    //Se tiene el dia de la semana, obtener la fecha en que debe ser creado el intervalo
                    //Para ello necesitamos la siguiente fecha que tenga el día de semana indicado
                    DateTime fechaIntervalo = fechaVigenciaDesde.Date;
                    do
                    {
                        if (fechaIntervalo.DayOfWeek == diaSemana)
                        {
                            var horaInicio = new TimeSpan(horasInicio, minutosInicio, 0);
                            var horaFin = new TimeSpan(horasFin, minutosFin, 0);
                            //Calcular fecha inicio, fecha fin y crear intervalo
                            horarioMgr.AgregarIntervalo(_prestador, sede, fechaIntervalo, horaInicio, horaFin,
                                                        fechaVigenciaDesde, fechaVigenciaFinal);
                            break;
                        }
                        fechaIntervalo = fechaIntervalo.AddDays(1);
                    } while (true);
                }
            }
        }

        /// <summary>
        /// Elimina un intervalo del horario del medico
        /// </summary>
        private void EliminarIntervalos()
        {
            var horarioRep = new HorarioRepository();
            var mgr = new HorarioMedicoManager(horarioRep);
            WebUtils.RequerirParametros("fechaEliminacion", "idIntervaloToDelete", "radioEliminar");
            //Parsear fecha eliminación que viene en formato iso
            DateTime fechaEliminacion =DateTime.Parse(Request["fechaEliminacion"]);
            int idIntervaloAEliminar = Convert.ToInt32(Request["idIntervaloToDelete"]);
            var opcionEliminacion = (OpcionEliminacionIntervalo)Convert.ToInt32(Request["radioEliminar"]);
            IntervaloHorarioSede intervalo = horarioRep.GetIntervaloHorario(idIntervaloAEliminar);
            if (intervalo == null)
            {
                throw new ApplicationException("No se encontró intervalo con id:" + idIntervaloAEliminar);
            }
            switch (opcionEliminacion)
            {
                case OpcionEliminacionIntervalo.SoloParaFecha:
                    mgr.EliminarIntervaloMedicoSoloEnFecha(intervalo, fechaEliminacion);
                    break;
                case OpcionEliminacionIntervalo.DeFechaEnAdelante:
                    mgr.EliminarIntervaloMedicoDesde(intervalo, fechaEliminacion);
                    break;
                default:
                    throw new ApplicationException("Opción no soportada:" + opcionEliminacion);
            }
        }

        #endregion
    }

    internal enum OpcionEliminacionIntervalo
    {
        /// <summary>
        /// Solo se eliminara el intervalo para la fecha especificada
        /// </summary>
        SoloParaFecha = 1,
        /// <summary>
        /// Se eliminara el intervalo a partir de la fecha.
        /// </summary>
        DeFechaEnAdelante = 2
    }


    public enum OpcionVigenciaIntervalo
    {
        /// <summary>
        /// El intervalo es vigente desde la fecha de vigencia inicial en adelante
        /// </summary>
        Siempre = 1,
        /// <summary>
        /// El intervalo debe tener una fecha de vigencia final
        /// </summary>
        Limite = 2,
    }
}