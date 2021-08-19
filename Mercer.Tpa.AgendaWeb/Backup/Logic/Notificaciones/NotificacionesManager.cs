using System;
using System.Collections.Generic;
using System.Web;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.Logic.Notificaciones
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Componente encargado de retornar información sobre las notificaciones
    /// </summary>
    public class NotificacionesManager
    {
        #region variables privadas

        private readonly NotificacionesDataRepository _notificacionesRep = new NotificacionesDataRepository();

        #endregion
        /// <summary>
        /// Retorna las ultimas notificaciones para el médico
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="totalRegistros"></param>
        /// <returns></returns>
        public IList<Notificacion> GetNotificacionesMedico(int idMedico, int totalRegistros, int idEmpresa)
        {
            var notificaciones = new List<Notificacion>();
            var logRegistros = _notificacionesRep.GetLogsCitasRegistradas(idMedico, totalRegistros,idEmpresa);
            var logCancelaciones = _notificacionesRep.GetLogsCitasCanceladas(idMedico, totalRegistros,idEmpresa);
            var logReprogramadas = _notificacionesRep.GetLogsCitasReprogramadas(idMedico, totalRegistros,idEmpresa);
      
            //Crear las notificaciones a partir de los logs de acciones
            foreach (var logCitaRegistrada in logRegistros)
            {
                notificaciones.Add(CrearNotificacionParaCitaRegistrada(logCitaRegistrada));
            }
            foreach (var logCitaCancelada in logCancelaciones)
            {
                notificaciones.Add(CrearNotificacionParaCitaCancelada(logCitaCancelada));
            }
            foreach (var logCitaReprogramada in logReprogramadas)
            {
                notificaciones.Add(CrearNotificacionParaCitaReprogramada(logCitaReprogramada));
            }

            /* Ordenar las notificciones de menor a mayor */
            notificaciones.Sort();
            return notificaciones.GetRange(0,
                                           totalRegistros > notificaciones.Count ? notificaciones.Count : totalRegistros);
        }

        private Notificacion CrearNotificacionParaCitaReprogramada(LogCitaReprogramada reprogramada)
        {
            var notificacion = new Notificacion();
            notificacion.LogOperacion = reprogramada;
            notificacion.Tipo = EnumTipoNotificacion.CitaReprogramada;
            notificacion.Info = string.Format("Cambiada de {0} a {1}", DateUtils.FormatFecha(reprogramada.FechaAnterior),
                                              DateUtils.FormatFecha(reprogramada.FechaNueva));
            notificacion.Info += " Paciente:" + reprogramada.InfoCita.NombrePaciente;
            return notificacion;
        }

        private Notificacion CrearNotificacionParaCitaCancelada(LogCitaCancelada logCitaCancelada)
        {
            var notificacion = new Notificacion();
            notificacion.LogOperacion = logCitaCancelada;
            notificacion.Tipo = EnumTipoNotificacion.CitaCancelada;
            notificacion.Info = string.Format("Fecha de cita: {0}",
                                              DateUtils.FormatFecha(logCitaCancelada.InfoCita.StartDate));
            notificacion.Info += " Paciente:" + logCitaCancelada.InfoCita.NombrePaciente;
            return notificacion;
        }

        private Notificacion CrearNotificacionParaCitaRegistrada(LogCitaRegistrada logCitaRegistrada)
        {
            var notificacion = new Notificacion();
            notificacion.LogOperacion = logCitaRegistrada;
            notificacion.Tipo = EnumTipoNotificacion.CitaRegistrada;
            notificacion.Info = string.Format("Fecha:{0}", DateUtils.FormatFecha(logCitaRegistrada.InfoCita.StartDate));
            notificacion.Info += " Paciente:" + logCitaRegistrada.InfoCita.NombrePaciente;
            return notificacion;
        }
    }
}
