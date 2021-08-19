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
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess.Abstract
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Métodos de consulta de pacientes
    /// </summary>
    public interface IPacientesDataRepository
    {
        /// <summary>
        /// obtiene toda la informacion de un paciente ,
        ///  dependiendo si se pasa un idempleado o
        ///  idbeneficiario , este metodo consulta la base de datos SICAM
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idBeneficiario"></param>
        /// <returns></returns>
        InfoPaciente GetPacienteByIds(int idEmpleado, int idBeneficiario);
    }
}