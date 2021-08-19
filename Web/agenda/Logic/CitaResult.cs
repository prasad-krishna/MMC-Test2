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
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesCadenas;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Representa un resultado de busqueda de la cita
    /// </summary>
    public class CitaResult
    {
        #region Variables privadas
        private string _nombrePrestador;
        #endregion

        #region Propiedades

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EnumEstadoCita EstadoCita { get; set; }
        public int IdPrestador { get; set; }

        public string NombrePrestador
        {
            get { return StringUtils.UppercaseWords(_nombrePrestador); }
            set { _nombrePrestador = value; }
        }

        public string NombrePaciente { get; set; }
        public int IdSede { get; set; }
        public string NombreSede { get; set; }
        public int IdTipoCita { get; set; }
        public string NombreTipoCita { get; set; }
        public int DuracionTipoCita { get; set; }
        public String NotasAdicionales { get; set; }
        public Boolean Recordatorio { get; set; }
        public string EmpleadoIdentificacion { get; set; }
        public string PacienteIdentificacion { get; set; }
        public int IdEmpleado { get; set; }
        public int IdBeneficiario { get; set; }
        public string TelefonosContacto { get; set; }

        public string FormatoHoras
        {
            get { return GetRangoHoras(); }
        }

        public string FormatoDia
        {
            get { return GetDia(); }
        }

        public string RecordatorioLegible
        {
            get { return GetBooleanoLegible(); }
        }

        /// <summary>
        /// Duración en minutos
        /// </summary>
        public double TotalMinutes
        {
            get { return EndDate.Subtract(StartDate).TotalMinutes; }
        }

        public string NombreEmpresa { get; set; }

        #endregion

        #region Métodos privados
        /// <summary>
        /// publica la fecha  de la cita en formato de horas ej 4:00 am - 3:00pm
        /// </summary>
        private string GetRangoHoras()
        {
            return string.Format("{0:hh:mm tt}-{1:hh:mm tt}", StartDate, EndDate);
        }

        /// <summary>
        /// publica la fecha en formato de de diasm
        /// </summary>
        private string GetDia()
        {
            return StartDate.ToShortDateString();
        }

        /// <summary>
        /// publica SI o No dependiendo de True o False
        /// </summary>
        private string GetBooleanoLegible()
        {
            if (Recordatorio)
            {
                return "Si";
            }
            return "No";
        }
        #endregion
    }
}