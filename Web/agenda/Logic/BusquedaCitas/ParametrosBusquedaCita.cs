using System;
using System.Collections.Generic;

namespace Mercer.Tpa.Agenda.Web.Logic.BusquedaCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Contenedor de parámetros de búsqueda de citas
    /// </summary>
    public class ParametrosBusquedaCita
    {

        public ParametrosBusquedaCita()
        {

        }

        #region Propiedades

        public int IdSede { get; set; }

        public int IdEspecialidad { get; set; }

        public int IdMedico { get; set; }

        public int Recordatorio { get; set; }

        public int Estado { get; set; }

        public string IdentificacionEmpleado { get; set; }

        public string IdentificacionPaciente { get; set; }

        public string NombrePaciente { get; set; }

        public int IdUsuario { get; set; }
        //RAM*
        public string idEmpresa { get; set; }

        /// <summary>
        /// Si es true, se esta buscando en un rango de Horas especifico
        /// </summary>
        public bool HorarioEspecifico { get; set; }

        private TimeSpan _horaInicio = new TimeSpan(0,0,0);
        public TimeSpan HoraInicio
        {
            get { return _horaInicio; }
            set { _horaInicio = value; }
        }

        private TimeSpan _horaFin = new TimeSpan(23,59,59);
        public TimeSpan HoraFin
        {
            get { return _horaFin; }
            set { _horaFin = value; }
        }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        

        #endregion
    }
}