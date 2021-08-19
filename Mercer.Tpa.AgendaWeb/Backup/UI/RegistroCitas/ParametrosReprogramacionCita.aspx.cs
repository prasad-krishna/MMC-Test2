using System;
using Mercer.Tpa.Agenda.Web.Logic.Notificaciones;

namespace Mercer.Tpa.Agenda.Web.UI.RegistroCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Contenedor de los parametros requeridos para reprogramar cita
    /// </summary>
    internal  class ParametrosReprogramacionCita
    {
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public EnumOrigenModificacionCita Origen { get; set; }

        public string NombreSolicita { get; set; }

        public string NotasAdicionales { get; set; }

        public int IdMedio { get; set; }

        public string TelefonosContacto { get; set; }

        public int IdSede { get; set; }
    }
}