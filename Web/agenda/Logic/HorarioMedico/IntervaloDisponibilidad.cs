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

namespace Mercer.Tpa.Agenda.Web.Logic.HorarioMedico
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Intervalo de disponiblidad para un medico en un dia especifico
    /// </summary>
    public class IntervaloDisponibilidad
    {
        public InfoPrestador Prestador { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public InfoSede Sede { get; set; }
        public bool EsDiaFestivo { get; set; }

        public DateTime FechaInicio
        {
            get
            {
                return Fecha.Date.Add(HoraInicio);
            }
        }

        public DateTime FechaFin
        {
            get
            {
                return Fecha.Date.Add(HoraFin);
            }
        }

        public TipoCita Tipo { get; set; }

        public IntervaloDisponibilidad()
        {
            
        }
    }
}