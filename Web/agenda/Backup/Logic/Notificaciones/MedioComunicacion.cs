
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
*/using System;
using System.Collections.Generic;
using System.Web;

namespace Mercer.Tpa.Agenda.Web.Logic.Notificaciones
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Medio de comunicación
    /// </summary>
    public class MedioComunicacion
    {
        public MedioComunicacion()
        {
            
        }

        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }
        private bool _activa = true;
        public bool Activa
        {
            get { return _activa; }
            set { _activa = value; }
        }

        #endregion
    }
}
