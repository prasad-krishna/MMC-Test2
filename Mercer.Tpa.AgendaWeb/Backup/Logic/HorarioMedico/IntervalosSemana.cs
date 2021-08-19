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
using System.Collections.Generic;

namespace Mercer.Tpa.Agenda.Web.Logic.HorarioMedico
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Intervalos de trabajo para una semana.
    /// </summary>
    public class IntervalosSemana
    {
        #region Variables privadas

        private List<IntervaloHorarioSede> _domingo = new List<IntervaloHorarioSede>();
        private List<IntervaloHorarioSede> _jueves = new List<IntervaloHorarioSede>();
        private List<IntervaloHorarioSede> _lunes = new List<IntervaloHorarioSede>();

        private List<IntervaloHorarioSede> _martes = new List<IntervaloHorarioSede>();

        private List<IntervaloHorarioSede> _miercoles = new List<IntervaloHorarioSede>();
        private List<IntervaloHorarioSede> _sabado = new List<IntervaloHorarioSede>();
        private List<IntervaloHorarioSede> _viernes = new List<IntervaloHorarioSede>();

        #endregion

        #region Propiedades

        public List<IntervaloHorarioSede> Lunes
        {
            get { return _lunes; }
            set { _lunes = value; }
        }

        public List<IntervaloHorarioSede> Martes
        {
            get { return _martes; }
            set { _martes = value; }
        }

        public List<IntervaloHorarioSede> Miercoles
        {
            get { return _miercoles; }
            set { _miercoles = value; }
        }

        public List<IntervaloHorarioSede> Jueves
        {
            get { return _jueves; }
            set { _jueves = value; }
        }

        public List<IntervaloHorarioSede> Viernes
        {
            get { return _viernes; }
            set { _viernes = value; }
        }

        public List<IntervaloHorarioSede> Sabado
        {
            get { return _sabado; }
            set { _sabado = value; }
        }

        public List<IntervaloHorarioSede> Domingo
        {
            get { return _domingo; }
            set { _domingo = value; }
        }

        #endregion
    }
}