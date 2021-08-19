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

namespace Mercer.Tpa.Agenda.Web.Logic.RegistroCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Contenedor de parámetros para la búsqueda de disponibilidad
    /// </summary>
    public class ParametrosBusquedaDisponibilidad
    {
        public int IdEspecialidad { get; set; }
        public int IdMedico { get; set; }
        public int IdSedes { get; set; }
        public int IdTipoCita { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool HorarioEspecifico { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        /// <summary>
        /// duracion en minutos
        /// </summary>
        public int Duracion { get; set; }
    }
}