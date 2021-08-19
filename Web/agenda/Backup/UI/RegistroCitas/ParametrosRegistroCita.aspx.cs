using System;

namespace Mercer.Tpa.Agenda.Web.UI.RegistroCitas
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Contenedor de los parámetros recogidos de la página para el registro de cita
    /// </summary>
    internal class ParametrosRegistroCita
    {
        public string NotasAdicionales { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int IdMedico { get; set; }

        public int IdSede { get; set; }

        public int IdTipoCita { get; set; }

        public string TelefonosContacto { get; set; }
    }
}