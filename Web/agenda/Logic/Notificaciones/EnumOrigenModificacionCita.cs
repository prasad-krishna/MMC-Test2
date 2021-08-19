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
namespace Mercer.Tpa.Agenda.Web.Logic.Notificaciones
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Tipos de origen para las modificaciones de la cita
    /// </summary>
    public enum EnumOrigenModificacionCita
    {
        /// <summary>
        /// La acción fue generada por el paciente
        /// </summary>
        Paciente=1,
        /// <summary>
        /// La acción fue solicitada por el médico
        /// </summary>
        Medico=2,
        /// <summary>
        /// Otra persona solicitó la acción
        /// </summary>
        Otro=3
    }
}