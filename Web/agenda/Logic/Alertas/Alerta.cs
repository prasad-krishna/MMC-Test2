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
namespace Mercer.Tpa.Agenda.Web.Logic.Alertas
{
    /// <summary>
    /// Representa una alerta que será mostrada al médico por el módulo de alertas
    /// </summary>
    public class Alerta
    {
        public string Id { get; set; }
        public EnumTipoAlerta Tipo { get; set; }
        public string Description { get; set; }
    }

    public enum EnumTipoAlerta
    {
        ProximaCita = 1,
    }
}