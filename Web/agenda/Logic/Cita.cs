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
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Entidad Cita
    /// </summary>
    public class Cita
    {
        #region Propiedades

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EnumEstadoCita EstadoCita { get; set; }
        public InfoPrestador Prestador { get; set; }
        public string NombrePaciente { get; set; }
        public InfoSede Sede { get; set; }
        public TipoCita Tipo { get; set; }
        public String NotasAdicionales { get; set; }
        public Boolean Recordatorio { get; set; }
        public string EmpleadoIdentificacion { get; set; }
        public string PacienteIdentificacion { get; set; }
        public int IdEmpleado { get; set; }
        public int IdBeneficiario { get; set; }
        public string TelefonosContacto { get; set; }
        public string nombreEmpresa { get; set; }
        

        /// <summary>
        /// Fecha con zona horaria UTC en que fue creada la cita.
        /// </summary>
        public DateTime? FechaInicioUtc { get; set; }

        #endregion

        #region Métodos

        /// <summary>
        /// publica la fecha  de la cita en formato de horas ej 4:00 am - 3:00pm
        /// </summary>
        private string GetRangoHoras()
        {
            return StartDate.ToShortTimeString() + " - " + EndDate.ToShortTimeString();
        }


        /// <summary>
        /// publica la fecha en formato de de diasm
        /// </summary>
        private string GetDia()
        {
            return ConfiguracionAgendaManager.FormatearFecha(StartDate);
        }

        #endregion

        #region Propiedades adicionales para presentación

        //TODO: Obtener cadena de Resource
        private int _label = 2;

        public string DescripcionEstado
        {
            get { return EstadoCita.ToString(); }
        }

        public string FormatoHoras
        {
            get { return GetRangoHoras(); }
        }

        public string FormatoDia
        {
            get { return GetDia(); }
        }

        public string NombreSede
        {
            get { return Sede == null ? string.Empty : Sede.Nombre; }
        }

        public int IdPrestador
        {
            get { return Prestador == null ? -1 : Prestador.Id; }
        }

        /// <summary>
        /// Duración en minutos
        /// </summary>
        public double TotalMinutes
        {
            get { return EndDate.Subtract(StartDate).TotalMinutes; }
        }

        public Boolean AllDay
        {
            get { return false; }
            set { }
        }

        public int Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public int Status
        {
            get { return 1; }
            set { }
        }

        public int Type
        {
            get { return 0; }
            set { }
        }

        public string RecurrenceInfo
        {
            get { return null; }
            set { }
        }

        public string ReminderInfo
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Retorna la duración en minutos
        /// </summary>
        public int Duracion
        {
            get
            {
                return Convert.ToInt32(EndDate.Subtract(StartDate).TotalMinutes);
            }
        }

        #endregion
    }
}