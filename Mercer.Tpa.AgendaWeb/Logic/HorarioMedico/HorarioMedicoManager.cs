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
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic.RegistroCitas;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.Logic.HorarioMedico
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Componente encargado de las operaciones de configuración del horario de los medicos
    /// </summary>
    public class HorarioMedicoManager
    {
        #region Variables privadas

        private readonly IHorarioRepository _repository;
        private ICitasDataRepository _repositorioCitas = new CitasDataRepository();
        private IDiasFestivosDataRepository _repositorioFestivos = new DiasFestivosDataRepository();
        private IPrestadoresDataRepository _repositorioPrestadores = new PrestadoresDataRepository();
        private ITiposCitaDataRepository _repositorioTipoCitas = new TiposCitaDataRepository();

        #endregion

        #region Propiedades

        /// <summary>
        /// HAcemos este metodo virtual para poder realizar el Mock
        /// en la prueba unitaria
        /// </summary>
        public virtual IPrestadoresDataRepository RepositorioPrestadores
        {
            get { return _repositorioPrestadores; }
            set { _repositorioPrestadores = value; }
        }


        public virtual ICitasDataRepository RepositorioCitas
        {
            get { return _repositorioCitas; }
            set { _repositorioCitas = value; }
        }


        public virtual IDiasFestivosDataRepository RepositorioFestivos
        {
            get { return _repositorioFestivos; }
            set { _repositorioFestivos = value; }
        }



        public virtual ITiposCitaDataRepository RepositorioTipoCitas
        {
            get { return _repositorioTipoCitas; }
            set { _repositorioTipoCitas = value; }
        }

        #endregion

        #region Métodos públicos

        public HorarioMedicoManager()
        {
            _repository = new HorarioRepository();
        }

        /// <summary>
        /// Inicializa nueva instancia de la clase usando el repositorio especificado
        /// </summary>
        /// <param name="rep">Instancia de repositorio de horario</param>
        public HorarioMedicoManager(IHorarioRepository rep)
        {
            _repository = rep;
        }

        #endregion

        #region Horario medico

        /// <summary>
        /// Obtener el horario de un medico para una semana.
        /// Incluye intervalos que se cruzan (Conflictos)
        /// </summary>
        /// <param name="idMedico">Id del medico</param>
        /// <param name="fechaReferencia">Cualquier día de la semana que se quiere obtener</param>
        /// <returns></returns>
        public List<IntervaloHorarioSede> GetHorarioMedicoParaSemana(int idMedico, DateTime fechaReferencia)
        {
            DateTime inicioSemana = DateUtils.GetPrimerDiaDeSemana(fechaReferencia, DayOfWeek.Monday);
            DateTime finSemana = DateUtils.GetUltimoDiaSemana(fechaReferencia, DayOfWeek.Monday).AddDays(1).AddSeconds(-1);
            return GetHorarioMedico(idMedico, inicioSemana, finSemana, null, true, SessionManager.IdEmpresa);
        }


        /// <summary>
        /// Retorna los intervalos configurados en el horario del medico, teniendo en cuenta excepciones agregadas
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="idUsuario">Id de usuario si se desea filtrar solo para sedes asociadas al usuario. Si es null
        /// se retornan los intervalos en todas las sedes</param>
        /// <param name="incluirConflictos">Incluye los intervalos que se intersectan si es true.</param>

        /// <param name="idEmpresa">Filtrar por id de empresa (la empresa del usuario logueado actualmente)</param>
        /// <returns></returns>
        public List<IntervaloHorarioSede> GetHorarioMedico(int idMedico, DateTime fechaInicio, DateTime fechaFin, int? idUsuario, bool incluirConflictos, int idEmpresa)
        {
            var intervalosPeriodo = new List<IntervaloHorarioSede>();
            List<IntervaloHorarioSede> listaIntervalos = null;
            if (idUsuario.HasValue)
            {
                listaIntervalos = _repository.GetListaIntervalosMedico(idMedico, idUsuario.Value, idEmpresa);
            }
            else
            {
                listaIntervalos = _repository.GetListaIntervalosMedico(idMedico, idEmpresa);
            }
            List<ExcepcionIntervaloHorario> listaExcepciones = _repository.GetListaExcepcionesHorarioMedico(idMedico,
                                                                                                            fechaInicio,
                                                                                                            fechaFin);

            /*ITerar por todos los intervalos, filtrar aquellos que no estan vigentes en la fecha especificada*/
            foreach (IntervaloHorarioSede intervalo in listaIntervalos)
            {
                DateTime fechaVigenciaLimite = intervalo.VigenteHasta.HasValue
                                                   ? intervalo.VigenteHasta.Value.AddDays(1).AddSeconds(-1)
                                                   : DateTime.MaxValue;



                /*Si el intervalo no es vigente en el intervalo solicitado, ignorarlo*/
                if (!DateUtils.Intersects(fechaInicio, fechaFin, intervalo.VigenteDesde, fechaVigenciaLimite))
                {

                    continue;
                }

                DateTime fechaActual = fechaInicio.Date;
                //Iterar por cada dia desde la fecha inicial hasta la fecha final e ir agregando el intervalo a cada dia
                while (fechaActual <= fechaFin)
                {
                    if (fechaActual.DayOfWeek == intervalo.Fecha.DayOfWeek)
                    {

                        /*Obtener los momentos exactos del intervalo para el día actual*/
                        var fechaIntervaloInicio = fechaActual.Add(intervalo.FechaInicio.TimeOfDay);
                        var fechaIntervaloFin = fechaActual.Add(intervalo.FechaFin.TimeOfDay);

                        //Asegurarse que el intervalo es vigente en el dia actual
                        if (!DateUtils.Intersects(fechaActual.Date, fechaActual.Date.AddDays(1), intervalo.VigenteDesde,
                                                  fechaVigenciaLimite))
                        {
                            fechaActual = fechaActual.AddDays(1);
                            continue;
                        }

                        /*Determinar si el intervalo en el día actual esta en el rango solicitado*/

                        if (!DateUtils.Intersects(fechaInicio, fechaFin, fechaIntervaloInicio, fechaIntervaloFin))
                        {
                            fechaActual = fechaActual.AddDays(1);
                            continue;
                        }

                        /*Estamos en el dia de la semana definido en el intervalo
                         Agregamos una copia del intervalo, con la fecha actalizada del dia.
                         * Solo si no existe una excepcion especifica para el dia
                        */
                        IntervaloHorarioSede intervaloDia = intervalo.Clone();
                        intervaloDia.Fecha = fechaActual;
                        if (!ExisteExcepcionParaElDia(intervaloDia, listaExcepciones))
                        {
                            /*Solo agregar intervalo si este no se intersecta con otro ya existente*/
                            bool intersecta = false;
                            /*Solo validamos si se intersecta cuando queremos excluir los conflictos*/
                            if (incluirConflictos == false)
                            {
                                /*Debemos filtrar los intervalos en conflicto*/
                                foreach (var intervaloExistente in intervalosPeriodo)
                                {
                                    if (DateUtils.Intersects(intervaloExistente.FechaInicio, intervaloExistente.FechaFin, intervaloDia.FechaInicio, intervaloDia.FechaFin))
                                    {
                                        intersecta = true;
                                        break;
                                    }
                                }
                            }

                            if (!intersecta)
                                intervalosPeriodo.Add(intervaloDia);
                        }
                    }
                    fechaActual = fechaActual.AddDays(1);
                }
            }

            /*Ordenarlos por fecha de inicio*/
            intervalosPeriodo.Sort();
            return intervalosPeriodo;
        }

        /// <summary>
        /// Busca en la lista de excepciones haber si para el dia del intervalo existe alguna excepcion
        /// </summary>
        /// <param name="intervaloDia"></param>
        /// <param name="excepciones"></param>
        /// <returns></returns>
        private static bool ExisteExcepcionParaElDia(IntervaloHorario intervaloDia,
                                                     IEnumerable<ExcepcionIntervaloHorario> excepciones)
        {
            foreach (ExcepcionIntervaloHorario ex in excepciones)
            {
                if (ex.Intervalo.Id == intervaloDia.Id && ex.Fecha.Date == intervaloDia.Fecha.Date)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Crea un intervalo en el horario del médico donde estará disponible para atender citas
        /// </summary>
        /// <param name="prestador"></param>
        /// <param name="sede"></param>
        /// <param name="fecha"></param>
        /// <param name="horaInicio"></param>
        /// <param name="horaFin"></param>
        /// <param name="vigenteDesde"></param>
        /// <param name="vigenteHasta"></param>
        public void AgregarIntervalo(InfoPrestador prestador, InfoSede sede, DateTime fecha, TimeSpan horaInicio,
                                     TimeSpan horaFin, DateTime vigenteDesde, DateTime? vigenteHasta)
        {
            var intervalo = new IntervaloHorarioSede();
            if (prestador == null)
                throw new ApplicationException("Prestador no puede ser nulo");
            if (sede == null)
                throw new ApplicationException("Sede no puede ser nulo");

            intervalo.Prestador = prestador;
            intervalo.Sede = sede;
            intervalo.Fecha = fecha.Date;
            intervalo.HoraInicio = horaInicio;
            intervalo.HoraFin = horaFin;
            intervalo.VigenteDesde = vigenteDesde;
            intervalo.VigenteHasta = vigenteHasta;
            if (intervalo.IsValid == false)
            {
                throw new ApplicationException("Intervalo no válido:" + intervalo.FechaInicio.ToString("s") + " " +
                                               intervalo.FechaFin.ToString("s"));
            }
            _repository.InsertarIntervalo(intervalo);
        }


        /// <summary>
        /// Eliminará el intervalo solo para la fecha especificada
        /// (Internamente crea una excepcion)
        /// </summary>
        /// <param name="intervalo"></param>
        /// <param name="fechaExcepcion"></param>
        public void EliminarIntervaloMedicoSoloEnFecha(IntervaloHorarioSede intervalo, DateTime fechaExcepcion)
        {
            if (intervalo == null)
                throw new ApplicationException("Intervalo no puede ser null");
            var excepcion = new ExcepcionIntervaloHorario
            {
                Intervalo = intervalo,
                Fecha = fechaExcepcion
            };
            _repository.InsertarExcepcionHorario(excepcion);
        }

        /// <summary>
        /// Elimina el intervalo a partir de la fecha. (nternamente actualiza la fecha de vigencia final
        /// del intervalo
        /// </summary>
        /// <param name="idIntervalo"></param>
        /// <param name="fechaInicio"></param>
        public void EliminarIntervaloMedicoDesde(IntervaloHorarioSede intervalo, DateTime fechaInicio)
        {
            if (intervalo == null)
                throw new ApplicationException("Intervalo no puede ser null");
            intervalo.VigenteHasta = fechaInicio.Date;

            //Agregar siempre una excepción para el día en que se elimina el intervalo
            EliminarIntervaloMedicoSoloEnFecha(intervalo, fechaInicio);

            //También se actualiza la fecha de vigencia del intervalo para que futuras ocurrencias no se muestren
            _repository.ActualizarVigenciaFinIntervalo(intervalo);
        }

        #endregion

        #region Consulta de disponibilidad para registrar citas

        /// <summary>
        /// Retorna los intervalos de disponibilidad donde se pueden registrar citas.
        /// NO se retorna un intervalo si tiene conflicto con otro (Se retorna el primero encontrado)
        /// </summary>
        /// <returns>Intervalos de disponibilidad que cúmplen con los parámetrois de búsqueda especificados</returns>
        public List<IntervaloDisponibilidad> GetIntervalosDisponibilidad(int idEmpresa,
                                                                         ParametrosBusquedaDisponibilidad parametros)
        {
            var intervalosDisponibles = new List<IntervaloDisponibilidad>();
            var listaMedicos = GetMedicosFiltrados(idEmpresa, parametros);
            var duracion = parametros.Duracion <= 0 ? 20 : parametros.Duracion;
            var festivos = GetFestivos(idEmpresa, parametros.FechaInicio, parametros.FechaFin);
            /*Para cada medico obtener el horario en el periodo especificado*/
            foreach (var medico in listaMedicos)
            {
                /*Se obtienen los intervalos de disponibilidad únicamente para las sedes a las que tiene acceso el usuario*/
                var intervalosHorario = GetHorarioMedico(medico.Id, parametros.FechaInicio,
                                                                                parametros.FechaFin, SessionManager.IdUser, false, idEmpresa);
                /*Obtener las citas del medico una hora antes de la fecha de inicio para que se incluyan las citas actuales*/
                var citasMedico = GetCitasMedico(medico.Id, parametros.FechaInicio.AddHours(-1), parametros.FechaFin);
                /* Si se especifico sede, filtramos por sede */
                if (parametros.IdSedes > 0)
                {
                    intervalosHorario.RemoveAll(i => i.Sede.Id != parametros.IdSedes);
                }
                /*Por cada intervalo del horario pueden resultar multiples intervalos de disponibilidad*/
                foreach (var intervaloHorario in intervalosHorario)
                {
                    var fechaActual = intervaloHorario.FechaInicio;
                    /*Armar los intervalos de disponibilidad  (ir sumando la duración del intervalo*/
                    while (fechaActual < intervaloHorario.FechaFin)
                    {
                        /*Inicio del intervalo disponible*/
                        DateTime inicioDisp = fechaActual;

                        /*Fin del intervalo disponible*/
                        DateTime finDisp = inicioDisp.AddMinutes(duracion);
                        /*Aumentar variable contador*/
                        fechaActual = fechaActual.AddMinutes(duracion);

                        /*El intervalo no se encuentra dentro del rango solicitado */
                        if (inicioDisp > parametros.FechaFin || finDisp < parametros.FechaInicio || finDisp > intervaloHorario.FechaFin)
                        {
                            continue;
                        }

                        /*Se paso al dia siguiente?, descartar
                         *Esta condicion habria que quitarla si se decide soportar intervalos que alcancen 
                         *a abarcar 2 dias (usando datetimes en vez de timespans)
                         */
                        if (finDisp.Date != inicioDisp.Date)
                        {
                            continue;
                        }
                        /*Verificar si se encuentra en el rango solicitado de horas*/
                        if (parametros.HorarioEspecifico)
                        {
                            /*Si el intervalo horario de los parametros no contiene la hora inicioDisp,finDisp, continuar*/
                            if (
                                !(inicioDisp.TimeOfDay >= parametros.HoraInicio &&
                                  finDisp.TimeOfDay <= parametros.HoraFin))
                            {
                                continue;
                            }
                        }
                        /*Verificar que el intervalo no tenga conflicto con las citas del medico */
                        bool descartar = false;
                        foreach (Cita cita in citasMedico)
                        {
                            if (DateUtils.Intersects(cita.StartDate, cita.EndDate, inicioDisp, finDisp))
                            {
                                /*Una cita ya esta programada y se cruza con el intervalo,
                                 * avanzamos la fecha al momento que termina la cita mas un segundo
                                 * Descartamos intervalo
                                 */
                                fechaActual = cita.EndDate;
                                descartar = true;
                                break;
                            }
                        }
                        if (descartar)
                        {
                            continue;
                        }
                        /*El intervalo de disponibilidad paso todas las pruebas, lo agregamos*/
                        var intervaloDisp = new IntervaloDisponibilidad();
                        intervaloDisp.Fecha = inicioDisp.Date;
                        intervaloDisp.HoraInicio = inicioDisp.TimeOfDay;
                        intervaloDisp.HoraFin = finDisp.TimeOfDay;
                        intervaloDisp.Prestador = medico;
                        intervaloDisp.Sede = intervaloHorario.Sede;
                        intervaloDisp.EsDiaFestivo = festivos.Contains(inicioDisp.Date);
                        intervaloDisp.Tipo = RepositorioTipoCitas.GetTiposCitaById(parametros.IdTipoCita);
                        if (intervaloDisp.Tipo == null)
                        {
                            throw new ArgumentException("No se encontró el tipo de cita con Id:" + parametros.IdTipoCita);
                        }
                        intervalosDisponibles.Add(intervaloDisp);
                    }
                }
            }
            return intervalosDisponibles;
        }

        /// <summary>
        /// Retorna la lista de días festivos asociados a la empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        private List<DateTime> GetFestivos(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = new List<DateTime>(RepositorioFestivos.GetFestivosEntreFechas(idEmpresa, fechaInicio, fechaFin));
            return lista;
        }

        /// <summary>
        /// Obtiene la lista de citas que ocupan el horario del médico
        /// NOTA: Se pasa null como parámetro de id de empresa al metodo GetCitasMedico
        /// para indicar que se requieren traer las citas en TODAS las empresas, pues se está mirando
        /// si el medico esta disponible o no.
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        private List<Cita> GetCitasMedico(int idMedico, DateTime fechaInicio, DateTime fechaFin)
        {
            List<Cita> citasMedico = RepositorioCitas.GetCitasMedico(idMedico, fechaInicio, fechaFin,null);
            citasMedico.RemoveAll(c => !DateUtils.Intersects(c.StartDate, c.EndDate, fechaInicio.Date,
                                                             fechaFin.Date.AddDays(1)));
            return citasMedico;
        }

        /// <summary>
        /// Retorna la lista de médicos a los que se les buscará disponibilidad
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="parametrosBusqueda">Parámetros que determinan los filtros (IdMedico, IdEspecialidad)</param>
        /// <returns></returns>
        private List<InfoPrestador> GetMedicosFiltrados(int idEmpresa,
                                                        ParametrosBusquedaDisponibilidad parametrosBusqueda)
        {
            var listaMedicos = new List<InfoPrestador>();
            if (parametrosBusqueda.IdMedico > 0)
            {
                /*Si se especifico el id del medico, solo retornamos dicho medico*/
                listaMedicos.Add(RepositorioPrestadores.GetPrestadorById(parametrosBusqueda.IdMedico));
            }
            else if (parametrosBusqueda.IdEspecialidad > 0)
            {
                /*Obtener los ids de los medicos que pertenecen a la especialidad*/
                IEnumerable<InfoPrestador> medicos = RepositorioPrestadores.GetPrestadoresPorEspecialidad(idEmpresa,
                                                                                                          parametrosBusqueda
                                                                                                              .
                                                                                                              IdEspecialidad);
                listaMedicos.AddRange(medicos);
            }
            else
            {
                /*Retornar todos los medicos*/
                IEnumerable<InfoPrestador> medicos = RepositorioPrestadores.GetPrestadoresPorEspecialidad(idEmpresa, -1);
                listaMedicos.AddRange(medicos);
            }
            return listaMedicos;
        }

        #endregion
    }
}