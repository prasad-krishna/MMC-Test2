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
    /// Funcionalidad: Representa una regla que indica que para un dia especifico
    /// no se debe tener en cuenta un horario configurado
    /// </summary>
    public class ExcepcionIntervaloHorario
    {
        #region Propiedades

        public int Id { get; set; }
        public IntervaloHorarioSede Intervalo { get; set; }
        public DateTime Fecha { get; set; }

        #endregion

        #region Métodos públicos

        #endregion
    }
}