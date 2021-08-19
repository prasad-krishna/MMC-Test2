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

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Abstracción de un día que puede ser festivo o no
    /// </summary>
    public class Dia
    {
        #region Propiedades

        public DateTime fecha { get; set; }

        public bool IsFestivo { get; set; }

        public int idEmpresa { get; set; }

        public string dia
        {
            get { return fecha.Day.ToString(); }
        }
        #endregion

    }
}