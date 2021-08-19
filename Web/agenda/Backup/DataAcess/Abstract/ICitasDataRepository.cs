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
using Mercer.Tpa.Agenda.Web.Logic;
using Mercer.Tpa.Agenda.Web.Logic.BusquedaCitas;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;

namespace Mercer.Tpa.Agenda.Web.DataAcess.Abstract
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Operaciones de acceso a datos asociadas a las Citas
    /// </summary>
    public interface ICitasDataRepository
    {
        SedesDataRepository RepositorioSedes { get; }

        TiposCitaDataRepository RepositorioTipoCita { get; }

        /// <summary>
        /// Registra una nueva cita en la base de datos
        /// </summary>
        /// <param name="cita">Instancia de Cita que se registra</param>
        void RegistrarCita(Cita cita, int idUsuario);

        /// <summary>
        /// Obtiene una cita dado su Id
        /// </summary>
        /// <param name="idCita">Identificador de la cita en la base de datos</param>
        /// <returns>Cita</returns>
        Cita GetCitaById(int idCita);


        /// <summary>
        /// Retorna los resultados de la búsqueda de citas para los parámetros especificados
        /// </summary>
        /// <param name="idSede">Id de la sede</param>
        /// <param name="idMedico">Id del médico</param>
        /// <param name="recordatorio">bit indicando si se buscan citas a las que ya se les realizó el recordatorio</param>
        /// <param name="estado">Código del estado de las citas que se están buscando</param>
        /// <param name="identificacionEmpleado">Documento de identificación del empleado</param>
        /// <param name="identificacionPaciente">Docmento de identificación del paciente</param>
        /// <param name="idEmpleado">Id del empleado en la base de datos</param>
        /// <param name="idPaciente"></param>
        /// <param name="nombrePaciente"></param>
        /// <param name="sortExpression">Expresión utilizada para ordenar los resultados</param>
        /// <param name="startRowIndex">(Paginación) registro inicial</param>
        /// <param name="maximumRows">(Paginación) total de registros</param>
        /// <returns>Lista de objetos de tipo CitaResult para mostrar en la búsqueda</returns>
        List<CitaResult> GetCitasBusqueda(ParametrosBusquedaCita parametros, string sortExpression, int startRowIndex, int maximumRows);

        /// <summary>
        /// Retorna las citas del médico entre dos fechas. En estado pendiente,en espera o finalizadas
        /// que se mostrarán en el calendario
        /// </summary>
        /// <param name="idMedico">Identificador del medico</param>
        /// <param name="startDate">Fecha de Inicio</param>
        /// <param name="endDate">Fecha de Finalizacion</param>
        /// <returns></returns>
        List<Cita> GetCitasMedico(int idMedico, DateTime startDate, DateTime endDate, int? idEmpresa);



        /// <summary>
        /// Cancela una cita y registra los datos de cancelación.
        /// </summary>
        /// <param name="idCita">Id de la cita a cancelar</param>
        /// <param name="idUsuario">Id del usuario que cancela</param>
        /// <param name="solicitante">Nombre de quien solicita</param>
        /// <param name="medio">Medio a travez del cual se solicita</param>
        /// <param name="fecha"></param>
        /// <param name="notas"></param>
        /// <param name="motivo"></param>
        void CancelarCita(int idCita, int idUsuario, string nombreSolicita, int idMedio, string notasAdicionales,
                          EnumOrigenModificacionCita origen);
        
        /// <summary>
        /// Registra un nuevo intento de ubicar al paciente para recordarle su cita
        /// </summary>
        /// <param name="idCita"></param>
        /// <param name="idUsuario"></param>
        /// <param name="medio">Medio por el cual se intento contactar al paciente</param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="exitoso"></param>
        void RegistrarRecordatorio(int idCita, int idUsuario, int medio, string notas, DateTime fecha, bool exitoso);


        /// <summary>
        /// Actualiza la cita indicando la llegada del paciente
        /// </summary>
        /// <param name="idCita">Id de la cita</param>
        void RegistrarLlegadaPaciente(int idCita,int idUsuario);
    }
}