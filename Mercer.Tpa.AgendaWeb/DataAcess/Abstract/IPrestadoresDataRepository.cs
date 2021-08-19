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
    /// Funcionalidad: Metodos de acceso y consulta de información de prestadores
    /// </summary>
    public interface IPrestadoresDataRepository
    {
        /// <summary>
        /// Retorna todos los prestadores de una empresa asociados a una especialidad
        /// </summary>
        /// <param name="idEmpresa">id de la empresa</param>
        /// <param name="idEspecialidad">id de la especialidad , pasar -1 si se quiere
        ///  retornar todos los prestadores de la empresa ignorando la especialidad
        /// </param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns>Lista tipo InfoPrestador con los prestadores</returns>
        IEnumerable<InfoPrestador> GetPrestadoresPorEspecialidad(int idEmpresa,int idEspecialidad);

        /// <summary>
        /// Retorna el prestador dado su id
        /// </summary>
        /// <param name="prestador"></param>
        /// <returns></returns>
        InfoPrestador GetPrestadorById(int idPrestador);
    }
}