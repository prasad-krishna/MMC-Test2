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

namespace Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Contiene los datos de la zona horaria
    /// </summary>
    public class ZonaHoraria
    {
        #region Propiedades
        /// <summary>
        /// Identificador de la zona horaria en la base de datos
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la zona horaria
        /// </summary>
        public string Nombre { get; set; }


        /// <summary>
        /// Offset del UTC (horas) para la zona
        /// </summary>
        public int OffsetHoras { get; set; }

        /// <summary>
        /// Offset del UTC (minutos) para la zona
        /// </summary>
        public int OffsetMinutos { get; set; }

        public int TotalMinutosOffset
        {
            get
            {
                if (OffsetHoras == 0)
                    return OffsetMinutos;
                return (Math.Abs(OffsetHoras)*60 + OffsetMinutos)*(Math.Abs(OffsetHoras)/OffsetHoras);
            }
        }

        public TimeSpan UtcOffset
        {
            get { return new TimeSpan(OffsetHoras, OffsetMinutos, 0); }
        }

        /// <summary>
        /// Retorna la hora actual en formato local para este TimeZone
        /// </summary>
        public DateTime HoraActualLocal
        {
            get { return DateTime.UtcNow.Add(UtcOffset); }
        }

        #endregion
    }
}