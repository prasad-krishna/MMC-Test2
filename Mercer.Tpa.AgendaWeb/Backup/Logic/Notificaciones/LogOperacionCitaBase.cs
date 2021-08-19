using System;
using System.Collections.Generic;
using System.Web;

namespace Mercer.Tpa.Agenda.Web.Logic.Notificaciones
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Clase base de los registros asociados a cambios en la cita
    /// </summary>
    public class LogOperacionCitaBase
    {
        #region Propiedades
        /// <summary>
        /// Identificador de la novedad
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Cita relacionada
        /// </summary>
        public Cita InfoCita { get; set; }
        /// <summary>
        /// Fecha en que ocurrió la novedad
        /// </summary>
        public DateTime FechaNovedad { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó el registro
        /// </summary>
        public int IdUsuario { get; set; }
        #endregion

    }
}
