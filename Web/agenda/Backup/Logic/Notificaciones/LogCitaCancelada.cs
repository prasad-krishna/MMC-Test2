namespace Mercer.Tpa.Agenda.Web.Logic.Notificaciones
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Registro de la cancelación de una cita
    /// </summary>
    public class LogCitaCancelada : LogOperacionCitaBase
    {

        #region Propiedades

        public EnumOrigenModificacionCita Origen { get; set; }
        /// <summary>
        /// Nombre de quien solicita
        /// </summary>
        public string NombreSolicita{get;set;}

        /// <summary>
        /// Medio por el cual se solicita
        /// </summary>
        public MedioComunicacion Medio { get; set; }

        /// <summary>
        /// Notas adicionales a la cancelación
        /// </summary>
        public string NotasAdicionales { get; set; }

        #endregion
    }
}