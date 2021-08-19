using System;
using System.Collections.Generic;
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess.Abstract
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Metodos de acceso a datos para las operaciones de registro y consulta de festivos
    /// </summary>
    public interface IDiasFestivosDataRepository
    {
        /// <summary>
        /// Devuelve los dias festivos de determinado mes en determinado año en determinada empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IEnumerable<Dia> GetFestivosPorMes(int idEmpresa, int year, int month);

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
        void RegistrarFestivosEnMes(int idEmpresa, int year, int month, List<Dia> festivos);

        /// <summary>
        /// Registra un diafestivo para determinada empresa en la base de datos
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="diaFestivo"></param>
        void RegistrarDiaFestivo(int idEmpresa, Dia diaFestivo);

        /// <summary>
        /// Elimina un dia festivo para determinada empresa en la base de datos
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="diaFestivo"></param>
        void EliminarDiaFestivo(int idEmpresa, Dia diaFestivo);

        /// <summary>
        /// Retorna la lista de fechas marcadas como festivos entre dos rangos de fechas
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        IEnumerable<DateTime> GetFestivosEntreFechas(int idEmpresa, DateTime fechaInicio, DateTime fechaFin);
    }
}