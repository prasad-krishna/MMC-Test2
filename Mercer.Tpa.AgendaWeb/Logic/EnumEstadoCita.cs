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
namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Posibles estados de una cita
    /// IMPORTANTE!: No cambiar los nombres pues tambien son usados desde javascript!!!.
    /// Agregar nuevos items si es necesario al final de la enumeración pues los indices no deben cambiar
    /// </summary>
    public enum EnumEstadoCita
    {
        /// <summary>
        /// La cita todavia no se ha realizado
        /// </summary>
        Pendiente = 1,
        /// <summary>
        /// El paciente asistio a al cita y se dio por finalizada
        /// </summary>
        Finalizada = 2,
        /// <summary>
        /// El paciente no se presento a la cita
        /// </summary>
        Inasistida = 3,
        /// <summary>
        /// El paciente cancelo la cita
        /// </summary>
        Cancelada = 4,
        /// <summary>
        /// El paciente llegó y esta esperando en la sala de espera
        /// </summary>
        Espera = 5
    }
}