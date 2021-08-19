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
    /// Funcionalidad: Contiene los intervalos de trabajo de una semana
    /// especifica en el tiempo
    /// </summary>
    public class IntervalosSemanaEspecifica : IntervalosSemana
    {
        #region Propiedades

        /// <summary>
        /// Dia de referencia para identificar la semana actual
        /// </summary>
        public DateTime FechaSemana { get; set; }

        #endregion

        #region Métodos

        public IntervalosSemanaEspecifica()
        {
            FechaSemana = DateTime.Now;
        }

        #endregion
    }
}