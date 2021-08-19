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
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesCadenas;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Detalles del médico utilizados por la funcionalidad de agenda
    /// </summary>
    [Serializable]
    public class InfoPrestador
    {
        #region Variables privadas
        private string _name;
        #endregion

        #region Propiedades

        public int Id { get; set; }

        public string Name
        {
            get { return StringUtils.UppercaseWords(_name); }
            set { _name = value; }
        }

        public Especialidad Especialidad { get; set; }

        #endregion

    }
}