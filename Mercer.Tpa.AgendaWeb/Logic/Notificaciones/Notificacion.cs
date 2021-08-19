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
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Una notificación se muestra al médico indicandole una novedad en su horario 
    /// </summary>
    public class Notificacion:IComparable<Notificacion>
    {
        #region Propiedades

        /// <summary>
        /// Tipo de notificacion
        /// </summary>
        public EnumTipoNotificacion Tipo { get; set; }

        /// <summary>
        /// Log asociado
        /// </summary>
        public LogOperacionCitaBase LogOperacion { get; set; }

        /// <summary>
        /// Titulo de notificacion
        /// </summary>
        public string Titulo
        {
            get { return GetDescripcionTipos(); }
        }

        /// <summary>
        /// Texto con información de la notificación
        /// </summary>
        public string Info{ get; set; }
     

        #endregion

        #region Métodos privados
        private string GetDescripcionTipos()
        {
            if (Tipo == EnumTipoNotificacion.CitaCancelada)
            {
                return "Cita cancelada";
            }
            else if (Tipo == EnumTipoNotificacion.CitaEnEspera)
            {
                return "Cita en espera";
            }
            else if (Tipo == EnumTipoNotificacion.CitaInasisida)
            {
                return "Cita inasistida";
            }
            else if (Tipo == EnumTipoNotificacion.CitaRegistrada)
            {
                return "Cita registrada";
            }
            else if (Tipo == EnumTipoNotificacion.CitaReprogramada)
            {
                return "Cita reprogramada";
            }
            return null;
        }
        #endregion

        #region Implementation of IComparable<Notificacion>

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Notificacion other)
        {
            if (LogOperacion.FechaNovedad == other.LogOperacion.FechaNovedad)
                return 0;
            return LogOperacion.FechaNovedad > other.LogOperacion.FechaNovedad ? -1 : 1;
        }

        #endregion
    }
}