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
using System.Data.SqlClient;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Repositorio de acceso a datos para medios de comunicacion
    /// </summary>
    public class MediosComunicacionDataRepository
    {
        #region Variables privadas

        private readonly string _strCon =
            ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Retorna los medios de comunicacion para una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public IList<MedioComunicacion> GetAll(int idEmpresa)
        {
            var medios = new List<MedioComunicacion>();
            using (
                SqlDataReader reader = SqlHelper.ExecuteReader(_strCon, "GetMediosComunicacionEmpresa", idEmpresa, 0,
                                                               int.MaxValue))
            {
                while (reader.Read())
                {
                    var medio = new MedioComunicacion();
                    medio = new MedioComunicacion();
                    medio.Id = (int) reader["Id"];
                    medio.Nombre = reader["Nombre"].ToString();
                    object activa = reader["activa"];
                    if (activa != null && activa != DBNull.Value)
                    {
                        medio.Activa = (bool) activa;
                    }
                    medios.Add(medio);
                }
            }
            return medios;
        }

        /// <summary>
        /// Retorna los medios de comunicacion asociados a una empresa
        /// en estado activo
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public IList<MedioComunicacion> GetActivas(int idEmpresa)
        {
            var activas = new List<MedioComunicacion>();
            IList<MedioComunicacion> todas = GetAll(idEmpresa);
            foreach (MedioComunicacion medioComunicacion in todas)
            {
                if (medioComunicacion.Activa)
                {
                    activas.Add(medioComunicacion);
                }
            }
            return activas;
        }

        /// <summary>
        /// Retorna los medios de comunicacion para una empresa de forma paginada
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public IList<MedioComunicacion> GetAll(int idEmpresa, string sortExpression, int startRowIndex, int maximumRows)
        {
            var medios = new List<MedioComunicacion>();
            using (
                SqlDataReader reader = SqlHelper.ExecuteReader(_strCon, "GetMediosComunicacionEmpresa", idEmpresa,
                                                               startRowIndex, maximumRows))
            {
                while (reader.Read())
                {
                    var medio = new MedioComunicacion();
                    medio = new MedioComunicacion();
                    medio.Id = (int) reader["Id"];
                    medio.Nombre = reader["Nombre"].ToString();
                    object activa = reader["activa"];
                    if (activa != null && activa != DBNull.Value)
                    {
                        medio.Activa = (bool) activa;
                    }
                    medios.Add(medio);
                }
            }
            return medios;
        }

        /// <summary>
        /// Retorna el medio de comunicacion dado su id
        /// </summary>
        /// <param name="idMedio"></param>
        /// <returns></returns>
        public MedioComunicacion GetById(int idMedio)
        {
            MedioComunicacion medio = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(_strCon, "GetMedioComunicacion", idMedio))
            {
                while (reader.Read())
                {
                    medio = new MedioComunicacion();
                    medio.Id = (int) reader["Id"];
                    medio.Nombre = reader["Nombre"].ToString();
                    object activa = reader["activa"];
                    if (activa != null && activa != DBNull.Value)
                    {
                        medio.Activa = (bool) activa;
                    }
                }
            }
            return medio;
        }

        /// <summary>
        /// Retorna el total de medios paginados (Usado en pantalla de administracion)
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public int GetTotalMedios(int idEmpresa, int sortExpression, int startRowIndex, int maximumRows)
        {
            var cantidadMedios =
                (int) SqlHelper.ExecuteScalar(_strCon, "[dbo].[GetCantidadMediosComunicacion]", idEmpresa);
            return cantidadMedios;
        }

        /// <summary>
        /// Registra nuevo medio de comunicacion
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="medioComunicacion"></param>
        public void RegistrarMedio(int idEmpresa, MedioComunicacion medioComunicacion)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[InsertMedioComunicacion]", idEmpresa, medioComunicacion.Nombre);
        }

        /// <summary>
        /// Elimina medio de comunicacion
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="idMedio"></param>
        public void EliminarMedio(int idEmpresa, int idMedio)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[DeleteMedioComunicacion]", idMedio);
        }

        /// <summary>
        /// Actualiza valores del medio de comunicacion
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="medioComunicacion"></param>
        public void ActualizarMedio(int idEmpresa, MedioComunicacion medioComunicacion)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateMedioComunicacion]", idEmpresa, medioComunicacion.Id,
                                      medioComunicacion.Nombre);
        }

        /// <summary>
        /// Activar medio de comunicacion
        /// </summary>
        /// <param name="idMedio"></param>
        public void ActivarMedio(int idMedio)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateMedioComunicacionActivar]", idMedio);
        }

        /// <summary>
        /// Desactivar medio de comunicación
        /// </summary>
        /// <param name="idMedio"></param>
        public void DesactivarMedio(int idMedio)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateMedioComunicacionDesactivar]", idMedio);
        }

        #endregion
    }
}