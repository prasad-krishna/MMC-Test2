using System;
using System.Configuration;
using System.Data.SqlClient;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Repositorio de acceso a los parametros de la agenda
    /// </summary>
    public class AgendaDataRepository
    {
        #region Variables privadas

        private readonly string _strCon =
            ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Retorna la configuracion de agenda, o null si no se encuentra.
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public ParametrosConfiguracionAgenda GetConfiguracionAgenda(int idEmpresa)
        {
            ParametrosConfiguracionAgenda parametros = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(_strCon, "GetConfiguracionAgenda", idEmpresa))
            {
                while(reader.Read())
                {
                    parametros = new ParametrosConfiguracionAgenda();
                    parametros.IdEmpresa = idEmpresa;
                    parametros.NumHorasLimiteModificacionCitas = Convert.ToDouble(reader["numHorasLimiteModificacionCita"]);  
                }

            }
            return parametros;
        }

        /// <summary>
        /// Inserta o actualiza los parámetros de la agenda
        /// </summary>
        /// <param name="parametros"></param>
        public void GuardarConfiguracionAgenda(ParametrosConfiguracionAgenda parametros)
        {
            if (parametros.IdEmpresa <= 0)
                throw new ApplicationException("IdEmpresa no esta asignado en (ParametrosConfiguracionAgenda)");
            ParametrosConfiguracionAgenda parametrosExistentes = GetConfiguracionAgenda(parametros.IdEmpresa);
            string spName = "InsertConfiguracionAgenda";
            if (parametrosExistentes != null)
            {
                spName = "UpdateConfiguracionAgenda";
            }
            SqlHelper.ExecuteNonQuery(_strCon, spName, parametros.IdEmpresa, parametros.NumHorasLimiteModificacionCitas);
        }

        #endregion
    }
}