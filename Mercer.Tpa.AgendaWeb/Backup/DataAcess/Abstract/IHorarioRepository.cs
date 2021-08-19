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
using System.Collections.Generic;
using Mercer.Tpa.Agenda.Web.Logic.HorarioMedico;

namespace Mercer.Tpa.Agenda.Web.DataAcess.Abstract
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Operaciones de acceso a datos para registro y consulta de horario de médicos
    /// </summary>
    public interface IHorarioRepository
    {
             void InsertarIntervalo(IntervaloHorarioSede sede);

        void InsertarExcepcionHorario(ExcepcionIntervaloHorario excepcion);

        void ActualizarVigenciaFinIntervalo(IntervaloHorarioSede intervalo);
        IntervaloHorarioSede GetIntervaloHorario(int idIntervalo);
        List<IntervaloHorarioSede> GetListaIntervalosMedico(int idMedico,int idEmpresa);
        /// <summary>
        /// Retorna la lista de intervalos de disponibilidad para el médico únicamente
        /// en las sedes asociadas al usuario. (filtrando por empresa, es decir solo las sedes de la empresa actual)
        /// </summary>
        /// <param name="idMedico">id del médico</param>
        /// <param name="idUsuario">id del usuario</param>
        /// <returns></returns>
        List<IntervaloHorarioSede> GetListaIntervalosMedico(int idMedico,int idUsuario,int idEmpresa);

        List<ExcepcionIntervaloHorario> GetListaExcepcionesHorarioMedico(int idMedico, DateTime fechaInicio,
                                                                         DateTime fechaFin);
    }
}