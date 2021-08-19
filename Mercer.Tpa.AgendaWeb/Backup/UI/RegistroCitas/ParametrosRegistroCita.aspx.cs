using System;

namespace Mercer.Tpa.Agenda.Web.UI.RegistroCitas
{
    /// <summary>
    /// Proyecto: M�dulo Agenda M�dica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Contenedor de los par�metros recogidos de la p�gina para el registro de cita
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