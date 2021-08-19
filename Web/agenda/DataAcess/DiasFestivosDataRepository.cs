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
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Métdos de acceso a datos para días festivos
    /// </summary>
    public class DiasFestivosDataRepository : IDiasFestivosDataRepository
    {
        #region variables privadas

        readonly string _strCon = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Devuelve los dias festivos de determinado mes en determinado año en determinada empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IEnumerable<Dia> GetFestivosPorMes(int idEmpresa, int year, int month)
        {

            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetFestivos]", idEmpresa, new DateTime(year, month, 1));
            var festivosDb = new List<Dia>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Dia d = new Dia();
                d.fecha = (DateTime)ds.Tables[0].Rows[i]["fecha"];
                d.idEmpresa = idEmpresa;
                d.IsFestivo = true;
                festivosDb.Add(d);
            }
            return MergeFestivosIntoMonth(year, month, festivosDb);
        }
        /// <summary>
        /// Registra todos los festivos especificados en la lista festivos para determinada empresa
        /// Tambien se encarga de que sea de manera eficiente ,
        /// es decir solo manda a base de datos los necesarios ,
        /// sin redundancia de datos.
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="festivos"></param>
        public void RegistrarFestivosEnMes(int idEmpresa, int year, int month, List<Dia> festivos)
        {
            var festivosDb = new List<DateTime>();
            var pendientesPorAgregar = new List<Dia>();
            var pendientesPorBorrar = new List<Dia>();
            DataSet ds = SqlHelper.ExecuteDataset(_strCon, "[dbo].[GetFestivos]", idEmpresa, new DateTime(year, month, 1));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                festivosDb.Add((DateTime)ds.Tables[0].Rows[i]["fecha"]);
            }

            for (int i = 0; i < festivos.Count; i++)
            {
                if (festivos[i].IsFestivo && festivosDb.FindAll(x => x.Day == festivos[i].fecha.Day).Count == 0)
                {
                    //nuevo festivo registrado en el checkboxlist y sin registrar en la base de datos
                    Dia d = new Dia();
                    d.fecha = new DateTime(festivos[i].fecha.Year, festivos[i].fecha.Month, festivos[i].fecha.Day);
                    d.IsFestivo = true;
                    pendientesPorAgregar.Add(d);
                }
                else if (!festivos[i].IsFestivo && festivosDb.FindAll(x => x.Day == festivos[i].fecha.Day).Count != 0)
                {
                    //se quito en el checkboxlist, por lo tanto se debe borrar
                    Dia d = new Dia();
                    d.fecha = new DateTime(festivos[i].fecha.Year, festivos[i].fecha.Month, festivos[i].fecha.Day);
                    d.IsFestivo = false;
                    pendientesPorBorrar.Add(d);
                }

            }
            //insertar pendientes
            foreach (Dia d in pendientesPorAgregar)
            {
                DateTime dt = d.fecha;
                RegistrarDiaFestivo(idEmpresa, d);
            }
            //borrar pendientes
            foreach (Dia d in pendientesPorBorrar)
            {
                EliminarDiaFestivo(idEmpresa, d);
            }
        }
        /// <summary>
        /// Registra un diafestivo para determinada empresa en la base de datos
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="diaFestivo"></param>
        public void RegistrarDiaFestivo(int idEmpresa, Dia diaFestivo)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[InsertFestivo]", idEmpresa, diaFestivo.fecha);
        }
        /// <summary>
        /// Elimina un dia festivo para determinada empresa en la base de datos
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="diaFestivo"></param>
        public void EliminarDiaFestivo(int idEmpresa, Dia diaFestivo)
        {
            SqlHelper.ExecuteNonQuery(_strCon, "[dbo].[DeleteFestivo]", idEmpresa, diaFestivo.fecha);
        }

        /// <summary>
        /// Retorna la lista de fechas marcadas como festivos entre dos rangos de fechas
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetFestivosEntreFechas(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            List<DateTime> festivos = new List<DateTime>();
            using (var reader = SqlHelper.ExecuteReader(_strCon, "[dbo].[GetFestivosEntreFechas]", idEmpresa, fechaInicio, fechaFin))
            {
                while (reader.Read())
                {
                    festivos.Add((DateTime)reader["fecha"]);
                }
            }
            return festivos;
        }

        /// <summary>
        /// Determina si un día es festivo
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool EsFestivo(int IdEmpresa, DateTime value)
        {
            var festivosMes = GetFestivosPorMes(IdEmpresa, value.Year, value.Month);
            bool esfestivo = false;
            foreach (var dia in festivosMes)
            {
                if (dia.IsFestivo && dia.fecha.Date == value.Date)
                {
                    esfestivo = true;
                    break;
                }
            }
            return esfestivo;
        }
        #endregion

        #region Métodos privados

        /// <summary>
        /// Método utilitario , Combina los dias festivos de la lista festivos
        /// con todos los dias de un determinado mes , para poderlos mostrar facilmente 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="festivos"></param>
        /// <returns></returns>
        private IEnumerable<Dia> MergeFestivosIntoMonth(int year, int month, IList<Dia> festivos)
        {
            int totalDiasEnMes = DateTime.DaysInMonth(year, month);
            List<Dia> result = new List<Dia>();

            for (int i = 1; i <= totalDiasEnMes; i++)
            {
                result.Add(new Dia() { fecha = new DateTime(year, month, i), IsFestivo = false });
            }
            for (int i = 0; i < festivos.Count; i++)
            {
                result.RemoveAt(festivos[i].fecha.Day - 1);
                result.Insert(festivos[i].fecha.Day - 1, festivos[i]);
            }
            result.Sort((a, b) => a.fecha.Day.CompareTo(b.fecha.Day));
            return result;
        }

        #endregion


    }
}
