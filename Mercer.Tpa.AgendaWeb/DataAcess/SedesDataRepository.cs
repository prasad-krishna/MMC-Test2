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
using System.Data.SqlClient;
using System.Web;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesSedesUsuario;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Registro y consulta de información de Sedes en la base de datos
    /// </summary>
    public class SedesDataRepository : ISedesDataRepository
    {
        #region Variables privadas
        string _strCon = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion

        #region Métodos públicos

        /// <summary>
        /// Retorna todas las sedes asociadas a un codigo de empresa , Usado para cuando se necesite Paginacion
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa</returns>
        public IEnumerable<InfoSede> GetSedesByEmpresa(int idEmpresa, string sortExpression, int startRowIndex, int maximumRows)
        {
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetSedesByEmpresa]", idEmpresa, startRowIndex, maximumRows);
            List<InfoSede> sedesDB = new List<InfoSede>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                InfoSede sede = new InfoSede();
                sede.Nombre = (String)ds.Tables[0].Rows[i]["nombreSede"];
                sede.Descripcion = (String)ds.Tables[0].Rows[i]["descripcionSede"];
                sede.Id = (int)ds.Tables[0].Rows[i]["sede_id"];
                var activa = ds.Tables[0].Rows[0]["activa"];
                if (activa != null && activa != DBNull.Value)
                {
                    sede.Activa = (bool)activa;
                }
                sedesDB.Add(sede);
            }
            return sedesDB;

        }


        /// <summary>
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 29/07/2010
        /// Funcionalidad: Retorna todas las sedes asociadas a un codigo de empresa , Usado para cuando se necesite Paginacion        
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa</returns>
        public IEnumerable<InfoSede> GetSedesByEmpresaUser(int idEmpresa, int idUser, string sortExpression, int startRowIndex, int maximumRows)
        {
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetSedesByEmpresaUser]", idEmpresa, idUser, startRowIndex, maximumRows);
            List<InfoSede> sedesDB = new List<InfoSede>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                InfoSede sede = new InfoSede();
                sede.Nombre = (String)ds.Tables[0].Rows[i]["nombreSede"];
                sede.Descripcion = (String)ds.Tables[0].Rows[i]["descripcionSede"];
                sede.Id = (int)ds.Tables[0].Rows[i]["sede_id"];
                var activa = ds.Tables[0].Rows[0]["activa"];
                if (activa != null && activa != DBNull.Value)
                {
                    sede.Activa = (bool)activa;
                }
                sedesDB.Add(sede);
            }
            return sedesDB;

        }


        /// <summary>
        /// Retorna todas las sedes asociadas a un codigo de empresa
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa</returns>
        public IEnumerable<InfoSede> GetSedesByEmpresa(int idEmpresa)
        {

            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetSedesByEmpresa]", idEmpresa, null, null);
            List<InfoSede> sedesDB = new List<InfoSede>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                InfoSede sede = new InfoSede();
                sede.Nombre = (String)ds.Tables[0].Rows[i]["nombreSede"];
                sede.Descripcion = (String)ds.Tables[0].Rows[i]["descripcionSede"];
                sede.Id = (int)ds.Tables[0].Rows[i]["sede_id"];
                var activa = ds.Tables[0].Rows[i]["activa"];
                if (activa != null && activa != DBNull.Value)
                {
                    sede.Activa = (bool)activa;
                }
                sedesDB.Add(sede);
            }
            return sedesDB;

        }

        /// <summary>
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 02/08/2010
        /// Funcionalidad: Lista tipo InfoSede con todas las sedes de la empresa y usuario pasado por parámetro</returns>        
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="idUser">id del usuario</param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa y usuario pasado por parámetro</returns>
        public IEnumerable<InfoSede> GetSedesByEmpresaUser(int idEmpresa, int idUser)
        {

            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetSedesByEmpresaUser]", idEmpresa, idUser, null, null);
            List<InfoSede> sedesDB = new List<InfoSede>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                InfoSede sede = new InfoSede();
                sede.Nombre = (String)ds.Tables[0].Rows[i]["nombreSede"];
                sede.Descripcion = (String)ds.Tables[0].Rows[i]["descripcionSede"];
                sede.Id = (int)ds.Tables[0].Rows[i]["sede_id"];
                var activa = ds.Tables[0].Rows[i]["activa"];
                if (activa != null && activa != DBNull.Value)
                {
                    sede.Activa = (bool)activa;
                }
                sedesDB.Add(sede);
            }
            return sedesDB;

        }

        public IEnumerable<InfoSede> GetSedesActivasByEmpresa(int idEmpresa)
        {
            var sedes = GetSedesByEmpresa(idEmpresa);
            var sedesActivas = new List<InfoSede>();
            foreach (var sede in sedes)
            {
                if(sede.Activa)
                    sedesActivas.Add(sede);
            }
            return sedesActivas;
                
        }

        /// <summary>
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 02/08/2010
        /// Funcionalidad: Lista tipo InfoSede con todas las sedes de la empresa y usuario pasado por parámetro</returns>        
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="idUser">id del usuario</param>
        /// <returns>Lista tipo InfoSede con todas las sedes de la empresa y usuario pasado por parámetro</returns>
        public IEnumerable<InfoSede> GetSedesActivasByEmpresaUser(int idEmpresa, int idUser)
        {
            var sedes = GetSedesByEmpresaUser(idEmpresa, idUser);
            var sedesActivas = new List<InfoSede>();
            foreach (var sede in sedes)
            {
                if (sede.Activa)
                    sedesActivas.Add(sede);
            }
            return sedesActivas;
        }

        /// <summary>
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 29/07/2010
        /// Funcionalidad: Devuelve el total de sedopes que tiene una empresa y un usuario
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>total de sedes de una empresa</returns>
        public int GetTotalSedesByEmpresaUser(int idEmpresa, int idUser, int sortExpression, int startRowIndex, int maximumRows)
        {
            int cantidadSedes =(int)SqlHelper.ExecuteScalar(_strCon, "[dbo].[GetCantidadSedesByEmpresaUser]", idEmpresa, idUser);
            
            return cantidadSedes;
        }


        /// <summary>
        /// Devuelve el total de sedes que tiene una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>total de sedes de una empresa</returns>
        public int GetTotalSedes(int idEmpresa, int sortExpression, int startRowIndex, int maximumRows)
        {
            int cantidadSedes = (int)SqlHelper.ExecuteScalar(_strCon, "[dbo].[GetCantidadSedes]", idEmpresa);
            return cantidadSedes;
        }

        /// <summary>
        /// Registra una nueva sede asociada a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sede"></param>
        public int RegistrarSede(int idEmpresa, InfoSede sede)
        {
            SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_strCon, "[dbo].[InsertSede]");

            paramCollection[0].Value = idEmpresa;
            paramCollection[1].Value = sede.Nombre;
            paramCollection[2].Value = sede.Descripcion;

            SqlHelper.ExecuteNonQuery(_strCon, CommandType.StoredProcedure, "[dbo].[InsertSede]", paramCollection);
            
            return Convert.ToInt16(paramCollection[3].Value);
        }

        /// <summary>
        /// Elimina una sede asociada a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="idSede"></param>
        public void EliminarSede(int idEmpresa, int idSede)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[DeleteSede]", idEmpresa, idSede);
        }

        /// <summary>
        /// Actualiza una sede asociada a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sede"></param>
        public void ActualizarSede(int idEmpresa, InfoSede sede)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateSede]", idEmpresa, sede.Id, sede.Nombre, sede.Descripcion);

        }

        /// <summary>
        /// Retorna las sedes asociadas a un usuario y a la empresa especificada.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public List<InfoSede> GetSedesByUsuarioEmpresa(int idUsuario, int idEmpresa)
        {
            var sedes = new List<InfoSede>();
            using (SqlDataReader reader =  SqlHelper.ExecuteReader(_strCon, "[dbo].[GetSedesUsuarioEmpresa]", idUsuario,idEmpresa))
            {
                while (reader.Read())
                {
                    var sede = new InfoSede();
                    sede.Id = Convert.ToInt32(reader["sede_id"]);
                    sede.Nombre = reader["nombreSede"].ToString();
                    sede.Descripcion = reader["descripcionSede"].ToString();
                    var activa = reader["activa"];
                    if (activa != null && activa != DBNull.Value)
                    {
                        sede.Activa = (bool)activa;
                    }
                    sedes.Add(sede);

                }
            }
            return sedes;
        }

        /// <summary>
        /// Retorna todas las sedes (de varias empresas)
        /// asociadas al usuario (SOLO RETORNA IDS!)
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<InfoSede> GetSedesUsuario(int idUsuario)
        {
            var sedesDB = new List<InfoSede>();
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetSedesUsuario]", idUsuario);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                InfoSede sede = new InfoSede() { };
                sede.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["sede_id"]);
                sedesDB.Add(sede);
            }
            return sedesDB;
        }

        /// <summary>
        /// Asocia sedes al usuario y elimina relacion a sedes que hayan sido desmarcadas
        /// TODO: Transacciones
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="sedes"></param>
        public void RegistrarSedesUsuario(int idUsuario, List<InfoSedeVista> sedes, int idEmpresa)
        {
            var sedesAsociadas = GetSedesByUsuarioEmpresa(idUsuario,idEmpresa);
            var pendientesPorAgregar = new List<InfoSede>();
            var pendientesPorBorrar = new List<InfoSede>();
            for (int i = 0; i < sedes.Count; i++)
            {
                if (sedes[i].Selected && sedesAsociadas.FindAll(x => x.Id == sedes[i].Sede.Id).Count == 0)
                {
                    //nueva sede marcada en el checkboxlist y sin registrar en la base de datos
                    InfoSede sede = new InfoSede();
                    sede.Id = sedes[i].Sede.Id;
                    pendientesPorAgregar.Add(sede);
                }
                else if (!sedes[i].Selected && sedesAsociadas.FindAll(x => x.Id == sedes[i].Sede.Id).Count != 0)
                {
                    //se quito en el checkboxlist pero se encuentra asociada en la DB, por lo tanto se debe borrar
                    InfoSede sede = new InfoSede();
                    sede.Id = sedes[i].Sede.Id;
                    pendientesPorBorrar.Add(sede);
                }

            }
            //insertar pendientes
            foreach (InfoSede d in pendientesPorAgregar)
            {
                RegistrarUsuarioSede(idUsuario, d.Id);
            }
            //borrar pendientes
            foreach (InfoSede d in pendientesPorBorrar)
            {
                EliminarUsuarioSede(idUsuario, d.Id);
            }
        }

        /// <summary>
        /// Obtener una sede dado su id
        /// </summary>
        /// <param name="idSede"></param>
        /// <returns></returns>
        public InfoSede GetSedeById(int idSede)
        {
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetSedeById]", idSede);
            var sede = new InfoSede();
            if (ds.Tables[0].Rows.Count == 0)
                return null;

            sede.Nombre = (String)ds.Tables[0].Rows[0]["nombreSede"];
            sede.Descripcion = (String)ds.Tables[0].Rows[0]["descripcionSede"];
            sede.Id = (int)ds.Tables[0].Rows[0]["sede_id"];
            var activa = ds.Tables[0].Rows[0]["activa"];
            if(activa!=null && activa!=DBNull.Value)
            {
                sede.Activa = (bool) activa;
            }
           
            return sede;
 		}
        /// <summary>
        /// Registra una relacion entre un prestador y una sede , no es necesario el idempresa porq la sede es unica
        /// </summary>
        /// <param name="idPrestador"></param>
        /// <param name="idSede"></param>
        public void RegistrarUsuarioSede(int idUsuario, int idSede)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[InsertUsuarioSede]", idUsuario, idSede);
        }
        /// <summary>
        /// Elimina la relacion entre un prestador y una sede , no es necesario el idempresa porq la sede es unica
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idSede"></param>
        public void EliminarUsuarioSede(int idUsuario, int idSede)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[DeleteUsuarioSede]", idUsuario, idSede);
        }

        /// <summary>
        /// Marca una sede como activa
        /// </summary>
        /// <param name="idSede"></param>
        public void ActivarSede(int idSede)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateSedeActivar]", idSede);
        }

        /// <summary>
        /// Marca una sede como inactiva
        /// </summary>
        /// <param name="idSede"></param>
        public void DesactivarSede(int idSede)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateSedeDesactivar]", idSede);
        }

        #endregion


    }
}