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
    /// Funcionalidad: Enumera los tipos de notificaciones posibles
    /// </summary>
    public enum EnumTipoNotificacion
    {
        CitaRegistrada = 1, //o CitaPendiente
        CitaReprogramada,
        CitaCancelada,
        CitaInasisida,
        CitaEnEspera
    }
}