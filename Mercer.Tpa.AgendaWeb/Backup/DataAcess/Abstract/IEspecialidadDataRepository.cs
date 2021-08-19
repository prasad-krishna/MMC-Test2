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
using System.Collections.Generic;
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess.Abstract
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Metodos de acceso a datos para consulta y registro de especialidades
    /// </summary>
    public interface IEspecialidadDataRepository
    {
        /// <summary>
        /// Retorna todas especialidades
        /// </summary>
        /// <returns>Lista tipo Especialidad con todas las especialidades disponibles</returns>
        IEnumerable<Especialidad> GetEspecialidades();

        /// <summary>
        /// Retorna una especialidad dado su id
        /// </summary>
        /// <param name="idEspecialidad"></param>
        /// <returns></returns>
        Especialidad GetEspecialidadById(int idEspecialidad);
    }
}