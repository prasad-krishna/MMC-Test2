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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Acceso a datos para las notificaciones a médicos
    /// </summary>
    public class NotificacionesDataRepository
    {

        #region Variables privadas
        string conn = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion

        #region Métodos públicos

        public IList<LogCitaRegistrada> GetLogsCitasRegistradas(int idMedico, int totalRegistros, int idEmpresa)
        {
            var listaLogs = new List<LogCitaRegistrada>();
            var citasRep = new CitasDataRepository();
            using (var reader = SqlHelper.ExecuteReader(conn,"GetLogsCitasRegistradas",idMedico,totalRegistros,idEmpresa))
            {
                while (reader.Read())
                {
                    var log = new LogCitaRegistrada();
                    log.Id = (int)reader["Id"];
                    log.IdUsuario = (int) reader["IdUsuario"];
                    log.FechaNovedad = (DateTime) reader["FechaNovedad"];
                    var idCita = (int) reader["IdCita"];
                    log.InfoCita = citasRep.GetCitaById(idCita);
                    listaLogs.Add(log);
                }
            }
            return listaLogs;
        }

        public IList<LogCitaCancelada> GetLogsCitasCanceladas(int idMedico, int totalRegistros,int idEmpresa)
        {
            var listaLogs = new List<LogCitaCancelada>();
            var citasRep = new CitasDataRepository();
            var mediosRep = new MediosComunicacionDataRepository();

            using (var reader = SqlHelper.ExecuteReader(conn, "GetLogsCitasCanceladas", idMedico, totalRegistros,idEmpresa))
            {
                while (reader.Read())
                {
                    var log = new LogCitaCancelada();
                    log.Id = (int)reader["Id"];
                    log.IdUsuario = (int)reader["IdUsuario"];
                    log.FechaNovedad = (DateTime)reader["FechaNovedad"];
                    var idCita = (int)reader["IdCita"];
                    var idMedio = (int) reader["IdMedioComunicacion"];
                    log.InfoCita = citasRep.GetCitaById(idCita);
                    log.Medio = mediosRep.GetById(idMedio);
                    log.NombreSolicita = reader["NombreSolicita"].ToString();
                    log.Origen = (EnumOrigenModificacionCita) (int) reader["Origen"];
                    listaLogs.Add(log);
                }
            }
            return listaLogs;
        }

        public IList<LogCitaReprogramada> GetLogsCitasReprogramadas(int idMedico, int totalRegistros, int idEmpresa)
        {
            var listaLogs = new List<LogCitaReprogramada>();
            var citasRep = new CitasDataRepository();
            var mediosRep = new MediosComunicacionDataRepository();

            using (var reader = SqlHelper.ExecuteReader(conn, "GetLogsCitasReprogramadas", idMedico, totalRegistros, idEmpresa))
            {
                while (reader.Read())
                {
                    var log = new LogCitaReprogramada();
                    log.Id = (int)reader["Id"];
                    log.IdUsuario = (int)reader["IdUsuario"];
                    log.FechaNovedad = (DateTime)reader["FechaNovedad"];
                    var idCita = (int)reader["IdCita"];
                    var idMedio = (int)reader["IdMedioComunicacion"];
                    log.InfoCita = citasRep.GetCitaById(idCita);
                    log.Medio = mediosRep.GetById(idMedio);
                    log.NombreSolicita = reader["NombreSolicita"].ToString();
                    log.Origen = (EnumOrigenModificacionCita)(int)reader["Origen"];
                    log.FechaAnterior = (DateTime) reader["FechaAnterior"];
                    log.FechaNueva = (DateTime) reader["FechaNueva"];
                    listaLogs.Add(log);
                }
            }
            return listaLogs;
        }

        /// <summary>
        /// Retorna los registros con las fechas en que el paciente llegó a su cita.
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="totalRegistros"></param>
        /// <returns></returns>
        public IList<LogCitaEnEspera> GetLogsCitasEnEspera(int idMedico, int totalRegistros)
        {
            var listaLogs = new List<LogCitaEnEspera>();
            var citasRep = new CitasDataRepository();
            var mediosRep = new MediosComunicacionDataRepository();

            using (var reader = SqlHelper.ExecuteReader(conn, "GetLogsCitasEnEspera", idMedico, totalRegistros))
            {
                while (reader.Read())
                {
                    var log = new LogCitaEnEspera();
                    log.Id = (int)reader["Id"];
                    log.IdUsuario = (int)reader["IdUsuario"];
                    log.FechaNovedad = (DateTime)reader["FechaNovedad"];
                    var idCita = (int)reader["IdCita"];
                    log.InfoCita = citasRep.GetCitaById(idCita);
                    listaLogs.Add(log);
                }
            }
            return listaLogs;
        }

        #endregion

        #region Métodos privados

        

        #endregion

    }
}
