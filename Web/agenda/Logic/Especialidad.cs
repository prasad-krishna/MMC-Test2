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
using Mercer.Tpa.Agenda.Web.Logic.UtilidadesCadenas;

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Detalles de una especialidad
    /// </summary>
    public class Especialidad
    {
        #region variables privadas
        private string _nombre;
        #endregion

        #region Propiedades
        public int Id { get; set; }

        public string Nombre
        {
            get { return StringUtils.UppercaseWords(_nombre); }
            set { _nombre = value; }
        }

        #endregion


    }
}