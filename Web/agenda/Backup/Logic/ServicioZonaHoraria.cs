using System;
using System.Collections.Generic;
using System.Web;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Operaciones asociadas con la zona horaria configurada de la empresa
    /// </summary>
    public class ServicioZonaHoraria
    {
        private  HorarioRepository _horarioRep = new HorarioRepository();

        /// <summary>
        /// Retorna la hora actual local (si hay zona horaria definida)
        /// o la hora actual del servidor si no existe zona horaria definida
        /// </summary>
        /// <returns></returns>
        public  DateTime GetHoraLocal()
        {
            var horarioRep = new HorarioRepository();
            var zonaHoraria = horarioRep.GetZonaHorariaDeEmpresa(SessionManager.IdEmpresa);
            if(zonaHoraria == null)
            {
                return DateTime.Now;
            }
            else
            {
                return DateUtils.ConvertirFechaUtcAFechaLocal(DateTime.UtcNow, zonaHoraria);
            }
        }

        /// <summary>
        /// Retorna la hora en que la fecha inicio para la zona horaria UTC
        /// Requiere que este configurada la zona horaria de la empresa, de lo contrario 
        /// retorna NULL pues no es posible conocer la hora local
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="cita"></param>
        /// <returns></returns>
        public DateTime? ConvertirFechaLocalAUtc(int idEmpresa, DateTime fecha)
        {
            var zonaHoraria = _horarioRep.GetZonaHorariaDeEmpresa(idEmpresa);
            if (zonaHoraria == null)
                return null;
            /*Convertir la hora de inicio de la cita (local) a su equivalente UTC*/
            return DateUtils.ConvertirFechaLocalAUtc(zonaHoraria, fecha);
        }

    }
}
