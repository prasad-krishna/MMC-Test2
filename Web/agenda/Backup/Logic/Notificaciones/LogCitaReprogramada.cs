using System;

namespace Mercer.Tpa.Agenda.Web.Logic.Notificaciones
{
    public class LogCitaReprogramada : LogOperacionCitaBase
    {
        #region Propiedades

        public EnumOrigenModificacionCita Origen { get; set; }
        /// <summary>
        /// Nombre de quien solicita
        /// </summary>
        public string NombreSolicita { get; set; }

        /// <summary>
        /// Medio por el cual se solicita
        /// </summary>
        public MedioComunicacion Medio { get; set; }

        /// <summary>
        /// Notas adicionales a la cancelación
        /// </summary>
        public string NotasAdicionales { get; set; }

        /// <summary>
        /// Fecha original de la cita antes de ser movida
        /// </summary>
        public DateTime FechaAnterior { get; set; }

        /// <summary>
        /// Nueva fecha de la cita
        /// </summary>
        public DateTime FechaNueva { get; set; }

        #endregion
    }
}