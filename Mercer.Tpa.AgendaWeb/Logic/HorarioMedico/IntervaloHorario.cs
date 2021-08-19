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
    /// Funcionalidad:  Representa un intervalo de trabajo para un medico determinado.
    /// Solo es relevante la hora, minuto y segundo.
    /// No la fecha.
    /// </summary>
    [Serializable]
    public class IntervaloHorario:IComparable<IntervaloHorario>
    {
        #region Metodos

        protected void CopyTo(IntervaloHorario copia)
        {
            copia.Id = Id;
            copia.HoraInicio = HoraInicio;
            copia.HoraFin = HoraFin;
            copia.VigenteDesde = VigenteDesde;
            copia.VigenteHasta = VigenteHasta;
        }

        #endregion

        #region propiedades

        /// <summary>
        /// Identificador único del intervalo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fecha limite de vigencia para el intervalo actual
        /// </summary>
        public DateTime? VigenteHasta { get; set; }

        /// <summary>
        /// Fecha de vigencia inicial para el intervalo actual
        /// </summary>
        public DateTime VigenteDesde { get; set; }

        /// <summary>
        /// Día del intervalo (solo es relevante la parte Fecha)
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Inicio del intervalo
        /// </summary>
        public TimeSpan HoraInicio { get; set; }

        /// <summary>
        /// Fin del intervalo
        /// </summary>
        public TimeSpan HoraFin { get; set; }

        /// <summary>
        /// Retorna el momento exacto de inicio del intervalo
        /// </summary>
        public DateTime FechaInicio
        {
            get
            {
                return new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, HoraInicio.Hours, HoraInicio.Minutes,
                                    HoraInicio.Seconds);
            }
        }

        /// <summary>
        /// Retorna el momento de finalización para el día en que fue definido
        /// </summary>
        public DateTime FechaFin
        {
            get { return new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, HoraFin.Hours, HoraFin.Minutes, HoraFin.Seconds); }
        }

        /// <summary>
        /// Retorna una cadena con el rango de horas del intervalo
        /// </summary>
        public string Rango
        {
            get
            {
                var dtStart = new DateTime(2000, 1, 1, HoraInicio.Hours, HoraInicio.Minutes, HoraInicio.Seconds);
                var dtEnd = new DateTime(2000, 1, 1, HoraFin.Hours, HoraFin.Minutes, HoraFin.Seconds);
                return string.Format("{0:hh:mm tt} - {1:hh:mm tt} ", dtStart, dtEnd);
            }
        }

        /// <summary>
        /// Retorna true si el intervalo definido es válido
        /// </summary>
        public bool IsValid
        {
            get { return HoraFin > HoraInicio; }
        }

        #endregion

        #region Implementation of IComparable<IntervaloHorario>

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IntervaloHorario other)
        {
            return FechaInicio.CompareTo(other.FechaInicio);
        }

        #endregion
    }
}