/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2010 by Mercer
'===============================================================================
*/
using System;
using System.Globalization;
using Mercer.Tpa.Agenda.Web.DataAcess;
using Mercer.Tpa.Agenda.Web.Logic.ConfiguracionAgenda;

namespace Mercer.Tpa.Agenda.Web.Logic.UtilidadesFecha
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Utilidades para manipulación de fechas
    /// </summary>
    public static class DateUtils
    {
        /// <summary>
        /// Este metodo retorna el lunes de la semana actual, dada una
        /// fecha de referencia.
        /// La semana inicia el dia lunes
        /// </summary>
        /// <param name="fechaReferencia"></param>
        /// <returns></returns>
        public static DateTime GetPrimerDiaDeSemana(DateTime fechaReferencia, DayOfWeek firstDayOfWeek)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            DayOfWeek diaSemana;
            DateTime fechaActual = fechaReferencia;
            do
            {
                diaSemana = ciCurr.Calendar.GetDayOfWeek(fechaActual);
                fechaActual = fechaActual.AddDays(-1);
            } while (diaSemana != firstDayOfWeek);

            return fechaActual.AddDays(1).Date;
        }

        /// <summary>
        /// Retorna el ultimo día de la semana
        /// </summary>
        /// <param name="fechaReferencia"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <returns></returns>
        public static DateTime GetUltimoDiaSemana(DateTime fechaReferencia, DayOfWeek firstDayOfWeek)
        {
            DateTime primerDiaSemana = GetPrimerDiaDeSemana(fechaReferencia, firstDayOfWeek);
            return primerDiaSemana.AddDays(6);
        }

        public static int GetNumeroDeSemana(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        /// <summary>
        /// Retorna el numero de minutos de desviacion
        /// de la hora GMT para la empresa
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public static int GetGmtOffsetMinutos(int idEmpresa)
        {
            var horarioRep = new HorarioRepository();
            return horarioRep.GetOffsetZonaHorariaEmpresa(idEmpresa);
        }

        /// <summary>
        /// Determina si dos intervalos se intersectan
        /// 4:00pm - 6:00 pm no intersecta a 6:00pm - 8:00pm
        /// </summary>
        /// <param name="r1start">Inicio intervalo 1</param>
        /// <param name="r1end">Fin intervalo 1</param>
        /// <param name="r2start">Inicio intervalo 2</param>
        /// <param name="r2end">Fin intervalo 2</param>
        /// <returns></returns>
        public static bool Intersects(DateTime r1start, DateTime r1end, DateTime r2start, DateTime r2end)
        {
            return !(r2end <= r1start || r1end <= r2start);
        }

        /// <summary>
        /// Determina si dos intervalos de TimeSpan se intersectan
        /// </summary>
        /// <returns></returns>
        public static bool Intersects(TimeSpan r1start, TimeSpan r1end, TimeSpan r2start, TimeSpan r2end)
        {
            return !(r2end <= r1start || r1end <= r2start);
        }


        public static string GetNombreDia(DayOfWeek day)
        {
            switch (day)
            {
        
                case DayOfWeek.Monday:
                    return "Lunes";
                case DayOfWeek.Tuesday:
                    return "Martes";

                case DayOfWeek.Wednesday:
                    return "Miércoles";
                case DayOfWeek.Thursday:
                    return "Jueves";
                case DayOfWeek.Friday:
                    return "Viernes";
                case DayOfWeek.Saturday:
                    return "Sábado";
                case DayOfWeek.Sunday:
                    return "Domingo";
           
                
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Retorna la fecha para mostrar incluyendo horas minutos y segundos
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string FormatFecha(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy h:mm tt");
        }

        public static string  FormatSoloFecha(DateTime fecha)
        {
            return ConfiguracionAgendaManager.FormatearFecha(fecha);
            return fecha.ToString("dd/MM/yyyy");
        }

        public static int CalculateAge(DateTime birthDate)
        {
            // cache the current time
            DateTime now = DateTime.Today; // today is fine, don't need the timestamp from now
            // get the difference in years
            int years = now.Year - birthDate.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;

            return years;
        }

        /// <summary>
        /// Retorna formateada la parte del tiempo (horas y minutos) para la fecha especificada.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FormatSoloTiempo(DateTime time)
        {
            return time.ToString("hh:mm tt");
        }

        /// <summary>
        /// Convierte una fecha local a su equivalente Utc, utilizando la información de zona horaria
        /// </summary>
        /// <param name="zonaHoraria"></param>
        /// <param name="fechaLocal"></param>
        /// <returns></returns>
        public static DateTime ConvertirFechaLocalAUtc(ZonaHoraria zonaHoraria, DateTime fechaLocal)
        {
            if (zonaHoraria == null)
            {
                throw new ArgumentNullException("zonaHoraria", "Se requiere la zona horaria");
            }

            return fechaLocal.Subtract(zonaHoraria.UtcOffset);

        }


        /// <summary>
        /// Retorna la fecha local dada una fecha UTC y la zona horaria
        /// </summary>
        /// <param name="utctime"></param>
        /// <param name="zonaHoraria"></param>
        /// <returns></returns>
        public static DateTime ConvertirFechaUtcAFechaLocal(DateTime utctime, ZonaHoraria zonaHoraria)
        {
            if (zonaHoraria == null)
            {
                throw new ArgumentNullException("zonaHoraria", "Se requiere la zona horaria");
            }
            
            return utctime.Add(zonaHoraria.UtcOffset);
        }
    }
}