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
    /// Funcionalidad: Representa un intervalo de trabajo asociado a una sede
    /// </summary>
    [Serializable]
    public class IntervaloHorarioSede : IntervaloHorario,IComparable<IntervaloHorarioSede>
    {
        #region variables privadas

        private InfoPrestador _prestador;
        private InfoSede _sede;

        #endregion

        #region Propiedades

        public InfoSede Sede
        {
            get { return _sede; }
            set { _sede = value; }
        }

        public InfoPrestador Prestador
        {
            get { return _prestador; }
            set { _prestador = value; }
        }

        #endregion

        #region Métodos

        public IntervaloHorarioSede Clone()
        {
            var copia = new IntervaloHorarioSede();
            CopyTo(copia);
            return copia;
        }

        private void CopyTo(IntervaloHorario to)
        {
            base.CopyTo(to);
            (to as IntervaloHorarioSede).Prestador = Prestador;
            (to as IntervaloHorarioSede).Sede = Sede;
        }

        #endregion

        #region Implementation of IComparable<IntervaloHorarioSede>

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IntervaloHorarioSede other)
        {
            return FechaInicio.CompareTo(other.FechaInicio);
        }

        #endregion
    }
}