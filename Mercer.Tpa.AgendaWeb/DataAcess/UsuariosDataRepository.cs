using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.Logic;
using System.Data;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Repositorio de datos para los usuarios del sistema
    /// </summary>
    public class UsuariosDataRepository
    {
        #region Variables privadas
        readonly string _strCon = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion
        /// <summary>
        /// Retorna la lista de usuarios activos dado su id de empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public List<Usuario> GetUsuariosEmpresa(int idEmpresa)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(_strCon, "GetUsersEmpresaActivos", idEmpresa))
            {
                while (reader.Read())
                {
                    var usuario = new Usuario();
 
                    usuario.Id = Convert.ToInt32(reader["IdUser"]);
                    usuario.IdEmpresa = idEmpresa;
                    usuario.Nombre = reader["NameUser"] == DBNull.Value ? string.Empty : reader["NameUser"].ToString();
                    usuario.Login = reader["Login"].ToString();
                    usuario.IdCiudad = reader["IdCiudad"] == DBNull.Value ? -1 : Convert.ToInt32(reader["IdCiudad"]);
                    usuarios.Add(usuario);

                }
            }
            return usuarios;
        }

        /// <summary>
        /// Retorna un usuario dado su Id
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public Usuario GetUserById(int idUsuario)
        {
            Usuario usuario = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(_strCon, "ListUsers", idUsuario, 0, null, null, null, null, true))
            {
                while (reader.Read())
                {
                     usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader["IdUser"]);
                    usuario.IdEmpresa = Convert.ToInt32(reader["empresa_id"]);
                    usuario.Nombre = reader["NameUser"] == DBNull.Value ? string.Empty : reader["NameUser"].ToString();
                    usuario.Login = reader["Login"].ToString();
                    usuario.IdCiudad = reader["IdCiudad"] == DBNull.Value ? -1 : Convert.ToInt32(reader["IdCiudad"]);

                }
            }
            return usuario;
        }
        //RAM
        public DataSet GetEmpresasUser(int idUsuario)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[ListEmpresasUser]", idUsuario);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
    }
}
