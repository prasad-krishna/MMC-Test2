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
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Consulta de información de prestadores
    /// </summary>
    public class PrestadoresDataRepository : IPrestadoresDataRepository
    {
        #region Variables privadas
        string _strCon = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion

        #region Métodos públicos

        public PrestadoresDataRepository()
        {

        }

        /// <summary>
        /// Retorna todos los prestadores de una empresa asociados a una especialidad
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="idEspecialidad">id de la especialidad , pasar -1 si se quiere
        ///  retornar todos los prestadores de la empresa ignorando la especialidad
        /// </param>
        /// <returns>Lista tipo InfoPrestador con los prestadores</returns>
        public IEnumerable<InfoPrestador> GetPrestadoresPorEspecialidad(int idEmpresa, int idEspecialidad)
        {
            var ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[ListPrestadoresActivosPorEspecialidad]", idEmpresa, idEspecialidad);
            var prestadoresDb = new List<InfoPrestador>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var prestador = new InfoPrestador();
                prestador.Name = (String)ds.Tables[0].Rows[i]["NombrePrestador"];
                prestador.Id = (int)ds.Tables[0].Rows[i]["IdPrestador"];
                prestadoresDb.Add(prestador);
            }
            return prestadoresDb;

        }


        /// <summary>
        /// Retorna el prestador dado su id
        /// </summary>
        /// <param name="prestador"></param>
        /// <returns></returns>
        public InfoPrestador GetPrestadorById(int idPrestador)
        {
            EspecialidadDataRepository edr = new EspecialidadDataRepository();
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetPrestadores]", idPrestador, 0);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            var prestador = new InfoPrestador();
            prestador.Id = idPrestador;
            prestador.Name = ds.Tables[0].Rows[0]["NombrePrestador"].ToString();
            prestador.Especialidad = edr.GetEspecialidadById(Convert.ToInt32(ds.Tables[0].Rows[0]["IdEspecialidad"]));
            return prestador;
        }
        #endregion
        
    }
}
