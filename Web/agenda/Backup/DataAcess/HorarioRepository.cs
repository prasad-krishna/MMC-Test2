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
using System.Configuration;
using System.Data;
using System.Web;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Repositorio de acceso a datos para las entidades relacionadas con el
    /// horario de sedes y medicos configurado
    /// </summary>
    public class HorarioRepository:IHorarioRepository
    {
        #region Variables privadas
        string conn = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion

        #region Operaciones configuracion horario medico

        /// <summary>
        /// Convierte el dataset en una lista de exceciones de horario
        /// </summary>
        /// <param name="dsExcepciones"></param>
        /// <returns></returns>
        private  List<ExcepcionIntervaloHorario> GetListaExcepcionesFromDataset(DataSet dsExcepciones)
        {
            var listaEx = new List<ExcepcionIntervaloHorario>();
            foreach (DataRow row in dsExcepciones.Tables[0].Rows)
            {
                var ex = new ExcepcionIntervaloHorario();
                ex.Id = Convert.ToInt32(row["Id"]);
                ex.Fecha = (DateTime)(row["Fecha"]);
                ex.Intervalo = GetIntervaloHorario( Convert.ToInt32(row["IdIntervalo"]));
                listaEx.Add(ex);
            }
            return listaEx;
        }

        /// <summary>
        /// Retorna los objetos de tio IntervaloHorarioSede a partir del dataset
        /// </summary>
        /// <param name="dsIntervalos"></param>
        /// <returns></returns>
        private static List<IntervaloHorarioSede> GetListaIntervalosFromDataset(DataSet dsIntervalos)
        {
            var lista = new List<IntervaloHorarioSede>();
            var prestadorRep = new PrestadoresDataRepository();
            var sedesRep = new SedesDataRepository();
            foreach (DataRow row in dsIntervalos.Tables[0].Rows)
            {
                var intervalo = new IntervaloHorarioSede();
                intervalo.Id = Convert.ToInt32(row["Id"]);
                intervalo.Fecha = (DateTime) row["FechaInicio"];
                intervalo.HoraInicio = ((DateTime)row["FechaInicio"]).TimeOfDay;
                var fechaFin = (DateTime) row["FechaFin"];
                intervalo.HoraFin = fechaFin.TimeOfDay;
                intervalo.Prestador = prestadorRep.GetPrestadorById(Convert.ToInt32(row["IdPrestador"]));
                intervalo.Sede = sedesRep.GetSedeById(Convert.ToInt32(row["IdSede"]));
                intervalo.VigenteDesde = (DateTime) row["VigenciaDesde"];
                intervalo.VigenteHasta = row["VigenciaHasta"]==DBNull.Value?null:((DateTime?)row["VigenciaHasta"]);
                lista.Add(intervalo);
            }
            return lista;
        }

        public void InsertarIntervalo(IntervaloHorarioSede intervalo)
        {
            SqlHelper.ExecuteNonQuery(conn, "InsertIntervaloHorarioMedico",
                                      intervalo.FechaInicio, 
                                      intervalo.FechaFin,
                                      intervalo.Sede.Id, 
                                      intervalo.Prestador.Id, 
                                      intervalo.VigenteDesde,
                                      intervalo.VigenteHasta);


        }

        /// <summary>
        /// Registra una nueva excepcion al horario del medico
        /// </summary>
        /// <param name="excepcion"></param>
        public void InsertarExcepcionHorario(ExcepcionIntervaloHorario excepcion)
        {
            if(excepcion == null)
                throw new ApplicationException("Parametro no puede ser nulo: excepcion intervalo");
            if(excepcion.Intervalo == null)
                throw new ApplicationException("La propiedad Intervalo no puede ser null (Excepcion horario)");
            SqlHelper.ExecuteNonQuery(conn, "InsertExcepcionHorarioMedico", excepcion.Intervalo.Id, excepcion.Fecha);
        }

        public void ActualizarVigenciaFinIntervalo(IntervaloHorarioSede intervalo)
        {
            if(intervalo == null)
                throw new ApplicationException("Parametro no puede ser null: intervalo");

            SqlHelper.ExecuteNonQuery(conn,  "UpdateVigenciaIntervaloHorarioMedico", intervalo.Id, intervalo.VigenteHasta);

        }

        public IntervaloHorarioSede GetIntervaloHorario(int idIntervalo)
        {
            IntervaloHorarioSede intervalo=null;
            var prestadorRep = new PrestadoresDataRepository();
            var sedesRep = new SedesDataRepository();
            using(var reader = SqlHelper.ExecuteReader(conn,"GetIntervaloHorarioMedico",idIntervalo))
            {
                while (reader.Read())
                {
                    intervalo = new IntervaloHorarioSede();
                    intervalo.Id = Convert.ToInt32(reader["Id"]);
                    intervalo.Fecha = (DateTime)reader["FechaInicio"];
                    intervalo.HoraInicio = ((DateTime)reader["FechaInicio"]).TimeOfDay;
                    var fechaFin = (DateTime)reader["FechaFin"];
                    intervalo.HoraFin = fechaFin.TimeOfDay;
                    intervalo.Prestador = prestadorRep.GetPrestadorById(Convert.ToInt32(reader["IdPrestador"]));
                    intervalo.Sede = sedesRep.GetSedeById(Convert.ToInt32(reader["IdSede"]));
                    intervalo.VigenteDesde = (DateTime)reader["VigenciaDesde"];
                    intervalo.VigenteHasta = reader["VigenciaHasta"] == DBNull.Value
                                                 ? null
                                                 : (DateTime?) reader["VigenciaHasta"];

                    return intervalo;
                }
            }
            return intervalo;
        }

        public List<IntervaloHorarioSede> GetListaIntervalosMedico(int idMedico,int idEmpresa)
        {
            DataSet dsIntervalos = SqlHelper.ExecuteDataset(conn, "[dbo].[GetIntervalosHorarioMedico]", idMedico,idEmpresa);
            return GetListaIntervalosFromDataset(dsIntervalos);
        }

        /// <summary>
        /// Retorna la lista de intervalos de disponibilidad para el médico únicamente
        /// en las sedes asociadas al usuario.
        /// </summary>
        /// <param name="idMedico">id del médico</param>
        /// <param name="idUsuario">id del usuario</param>
        /// <returns></returns>
        public List<IntervaloHorarioSede> GetListaIntervalosMedico(int idMedico, int idUsuario,int idEmpresa)
        {
            DataSet dsIntervalos = SqlHelper.ExecuteDataset(conn, "[dbo].[GetIntervalosHorarioMedicoUsuario]", idMedico,idUsuario,idEmpresa);
            return GetListaIntervalosFromDataset(dsIntervalos);
        }

        public List<ExcepcionIntervaloHorario> GetListaExcepcionesHorarioMedico(int idMedico,DateTime fechaInicio, DateTime fechaFin)
        {
            DataSet dsExcepciones = SqlHelper.ExecuteDataset(conn, "[dbo].[GetExcepcionesHorarioMedico]", idMedico,
                                                 fechaInicio, fechaFin);
            return GetListaExcepcionesFromDataset(dsExcepciones);
        }

        #endregion

        #region Zona horaria empresa
        /// <summary>
        /// Retorna el offset de la zona horaria configurado para la empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public int GetOffsetZonaHorariaEmpresa(int idEmpresa)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
            return (int)SqlHelper.ExecuteScalar(conn, "GetOffsetZonaHorariaEmpresa", idEmpresa);
        }

        public DataSet GetZonasHorarias()
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
            return SqlHelper.ExecuteDataset(conn, "ListZonasHorarias");
        }

        /// <summary>
        /// Retorna la zona horaria de la empresa
        /// o null si no tiene
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public ZonaHoraria GetZonaHorariaDeEmpresa(int idEmpresa)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
            ZonaHoraria zona = null;
            using (var reader = SqlHelper.ExecuteReader(conn, "GetZonasHorariaEmpresa", idEmpresa))
            {
                while (reader.Read())
                {
                    zona = new ZonaHoraria()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        OffsetHoras = Convert.ToInt32(reader["GmtOffset"]),
                        OffsetMinutos = Convert.ToInt32(reader["GmtOffsetMinutos"])
                    };
                }
            }
            return zona;
        }

        /// <summary>
        /// Configura la zona horaria para una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        public void SetZonaHorariaEmpresa(int idEmpresa, int idZonaHoraria)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
            int result = SqlHelper.ExecuteNonQuery(conn, "UpdateEmpresaZonaHoraria", idEmpresa, idZonaHoraria);
        }
        #endregion

    }
}
