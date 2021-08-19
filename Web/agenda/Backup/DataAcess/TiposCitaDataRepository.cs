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
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Acceso a datos para los Tipos de Cita (Registro y consulta)
    /// </summary>
    public class TiposCitaDataRepository : ITiposCitaDataRepository
    {
        #region Variables privadas
        readonly string _strCon = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion

        #region Métodos públicos

        /// <summary>
        /// Devuelve todos los tipos de cita asociados a la empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>Lista de tipoCita con todos los tipos de cita asociados a la empresa</returns>
        public IEnumerable<TipoCita> GetTiposCitaByEmpresa(int idEmpresa, string sortExpression, int startRowIndex, int maximumRows)
        {
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetTiposCitaByEmpresa]",idEmpresa,startRowIndex,maximumRows);
            List<TipoCita> tiposCitaDB =new List<TipoCita>();
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TipoCita tipoCita = new TipoCita();
                tipoCita.Name=(String)ds.Tables[0].Rows[i]["nombreTipoCita"];
                tipoCita.Id=(int)ds.Tables[0].Rows[i]["tipoCita_id"];
                tipoCita.Duration=(int)ds.Tables[0].Rows[i]["duracionTipoCita"];
                var activa = ds.Tables[0].Rows[i]["activa"];
                if (activa != null && activa != DBNull.Value)
                {
                    tipoCita.Activa = (bool)activa;
                }
                tiposCitaDB.Add(tipoCita);
            }
            return tiposCitaDB;

        }

        public IEnumerable<TipoCita> GetTiposCitaByEmpresa(int idEmpresa)
        {
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetTiposCitaByEmpresa]",idEmpresa,null,null);
            List<TipoCita> tiposCitaDB =new List<TipoCita>();
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TipoCita tipoCita = new TipoCita();
                tipoCita.Name=(String)ds.Tables[0].Rows[i]["nombreTipoCita"];
                tipoCita.Id=(int)ds.Tables[0].Rows[i]["tipoCita_id"];
                tipoCita.Duration=(int)ds.Tables[0].Rows[i]["duracionTipoCita"];
                var activa = ds.Tables[0].Rows[i]["activa"];
                if(activa!=null && activa!=DBNull.Value)
                {
                    tipoCita.Activa = (bool) activa;
                }
                tiposCitaDB.Add(tipoCita);
            }
            return tiposCitaDB;

        }

        /// <summary>
        /// Retorna solo los tipos de cita activos
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public IEnumerable<TipoCita> GetTiposCitaActivosByEmpresa(int idEmpresa)
        {
            List<TipoCita> activos = new List<TipoCita>();
            var todos = GetTiposCitaByEmpresa(idEmpresa);
            foreach (var tipoCita in todos)
            {
                if(tipoCita.Activa)
                    activos.Add(tipoCita);
            }
            return activos;
        }

        public TipoCita GetTiposCitaById(int idTipoCita)
        {
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetTiposCitaById]",idTipoCita);
            TipoCita tc=new TipoCita();
            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            tc.Id=(int)ds.Tables[0].Rows[0]["tipoCita_id"];
            tc.Name=(string)ds.Tables[0].Rows[0]["nombreTipoCita"];
            tc.Duration=(int)ds.Tables[0].Rows[0]["duracionTipoCita"];
            var activa = ds.Tables[0].Rows[0]["activa"];
            if (activa != null && activa != DBNull.Value)
            {
                tc.Activa = (bool)activa;
            }
            return tc;
        }
        /// <summary>
        /// Devuelve el total de tipos de citas que hay por empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns>numero total de tipos de cita</returns>
        public int GetTotalTiposCita(int idEmpresa)
        {
            int cantidadTiposCita=(int)SqlHelper.ExecuteScalar(_strCon, "[dbo].[GetCantidadTiposCita]", idEmpresa);
            return cantidadTiposCita;
        }
        public int GetTotalTiposCita(int idEmpresa, string sortExpression, int startRowIndex, int maximumRows)
        {
            int cantidadTiposCita=(int)SqlHelper.ExecuteScalar(_strCon, "[dbo].[GetCantidadTiposCita]", idEmpresa);
            return cantidadTiposCita;
        }
        /// <summary>
        /// Registra un nuevo tipo de cita asociado a una empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="tipoCita"></param>
        public void RegistrarTipoCita(int idEmpresa,TipoCita tipoCita)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[InsertTipoCita]", idEmpresa, tipoCita.Name,tipoCita.Duration);
        }
        public void EliminarTipoCita(int idTipoCita)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[DeleteTiposCita]",idTipoCita);
        }
        /// <summary>
        /// Actualiza un tipo de cita especifico asociado a una empresa especifica.
        /// (No actualiza la variable Activa, para eso utilizar los metodos ActivarTipoCita
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="tipoCita"></param>
        public void ActualizarTipoCita(int idEmpresa,TipoCita tipoCita)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateTipoCita]", idEmpresa,tipoCita.Id, tipoCita.Name,tipoCita.Duration);
        }

        /// <summary>
        /// Activa un tipo de cita
        /// </summary>
        /// <param name="idTipoCita"></param>
        public void ActivarTipoCita(int idTipoCita)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateTipoCitaActivar]", idTipoCita);
        }

        /// <summary>
        /// Desactiva un tipo de cita
        /// </summary>
        /// <param name="idTipoCita"></param>
        public void DesactivarTipoCita(int idTipoCita)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[UpdateTipoCitaDesactivar]", idTipoCita);
        }


        #endregion


    }
}
