using System;
using System.Collections.Generic;
using System.Web;

namespace Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Parametros de configuración de agenda
    /// </summary>
    public class ParametrosConfiguracionAgenda
    {
        #region Propiedades
        public int IdEmpresa { get; set; }
        /// <summary>
        /// Número de horas limite para modifcar una cita.
        /// No se podrá modificar una cita  si la cita tiene una proximidad
        /// menor a este parametro
        /// </summary>
        public double NumHorasLimiteModificacionCitas { get; set; }
        #endregion

    }
}
