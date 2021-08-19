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

namespace Mercer.Tpa.Agenda.Web.Logic
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Hugo Fernando Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Detalles de un paciente proveniente de SICAU
    /// </summary>
    public class InfoPaciente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Parentesco { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Genero { get; set; }

        public string Identificacion { get; set; }
    }
}