namespace Mercer.Tpa.Agenda.Web.Logic.Notificaciones
{
    /// <summary>
    /// Proyecto: M�dulo Agenda M�dica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Registro de la cancelaci�n de una cita
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
        /// Notas adicionales a la cancelaci�n
        /// </summary>
        public string NotasAdicionales { get; set; }

        #endregion
    }
}