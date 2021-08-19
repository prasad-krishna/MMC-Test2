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
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha;
using Mercer.Tpa.Agenda.Web.Sistema;

namespace Mercer.Tpa.Agenda.Web.Logic.Alertas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Operaciones asociadas con la presentación de alertas al médico
    /// </summary>
    public class AlertasManager
    {
        #region Métodos publicos

        /// <summary>
        /// Retorna las alertas para el medico a partir de la hora actual
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="idEmpresa">Id de la empresa</param>
        /// <returns></returns>
        public List<Alerta> GetAlertasMedico(int idMedico, int idEmpresa)
        {
            //Obtener la hora actual local
            var rep = new HorarioRepository();
            var repCitas = new CitasDataRepository();
            ZonaHoraria zona = rep.GetZonaHorariaDeEmpresa(idEmpresa);
            var alertas = new List<Alerta>();
            DateTime fechaActualLocal = DateTime.Now;
            if (zona != null)
            {
                //La empresa tiene zona horaria.
                fechaActualLocal = zona.HoraActualLocal;
            }
            //Obtener las citas que comenzarán en los próximos 10 minutos
            List<Cita> citasMedico = repCitas.GetCitasMedico(idMedico, fechaActualLocal, fechaActualLocal.AddMinutes(10),SessionManager.IdEmpresa);
            foreach (Cita cita in citasMedico)
            {
                if (cita.EstadoCita == EnumEstadoCita.Pendiente)
                {
                    var alerta = new Alerta();
                    alerta.Id = "Cita-" + cita.Id;
                    alerta.Description = string.Format("Cita próxima:{0} {1}", cita.NombrePaciente, cita.FormatoHoras);
                    alerta.Tipo = EnumTipoAlerta.ProximaCita;
                    alertas.Add(alerta);
                }
            }

            

            //Mostrar alerta para los pacientes que hayan llegado recientemente
            return alertas;
        }

        #endregion
    }
}