using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web;

namespace Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Ofrece acceso a variables configuradas en web.config
    /// </summary>
    public static class ConfiguracionAgendaManager
    {
        /// <summary>
        /// Retorna el formato de fechas que se debe utilizar en la aplicación
        /// o el formato predefinido dia mes año en caso de que no se haya definido
        /// ninguno.
        /// </summary>
        public static string FormatoFecha
        {
            get
            {
                return (ConfigurationManager.AppSettings["FormatoFecha"] ?? "dd/MM/yyyy");
            }
        }

        public static string FormatoFechaGrid
        {
            get
            {
                return (ConfigurationManager.AppSettings["FormatoFechaGrilla"] ?? "{0:dd/MM/yyyy}");
            }
        }

        public static DateTime ParseDate(string strDate)
        {
            return DateTime.ParseExact(strDate, FormatoFecha, CultureInfo.InvariantCulture);
        }

        public static string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString(FormatoFecha);
        }
    }
}
