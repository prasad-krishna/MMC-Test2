/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2008 by Mercer
'===============================================================================
*/

using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;
using System.Collections;


namespace Mercer.Medicines.Logic
{
	/// <summary>
	/// Esta clase provee la funcionalidad para
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class Incapacidad : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id de la incapacidad</summary>
		private long _IdIncapacidad;
		/// <summary>Atributo, Id de la solicitud que la relaciona</summary>
		private long _IdSolicitud;
		/// <summary>Atributo, Id de la consulta que la relaciona</summary>
		private long _IdConsulta;
		/// <summary>Atributo, Fecha de inicio de la incapacidad</summary>
		private DateTime _FechaInicio;
		/// <summary>Atributo, Fecha de fin de la incapacidad</summary>
		private DateTime _FechaFin;
		/// <summary>Atributo, Indica si la incapacidad es una continuación</summary>
		private bool _Continuacion;
		/// <summary>Atributo, Indica si la incapacidad es una transcripción</summary>
		private bool _Transcripcion;
		/// <summary>Atributo, Observaciones de la incapacidad</summary>
		private string _Observaciones;
		/// <summary>Atributo, Arreglo que contiene los diagnosticos de la incapacidad</summary>
		private ArrayList _IncapacidadDiagnosticos;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id de la incapacidad</summary>
		public long IdIncapacidad
		{
			get	{ return _IdIncapacidad; }
			set	{ _IdIncapacidad = value; }
		}
		/// <summary>Propiedad, Id de la solicitud que la relaciona</summary>
		public long IdSolicitud
		{
			get	{ return _IdSolicitud; }
			set	{ _IdSolicitud = value; }
		}
		/// <summary>Propiedad, Id de la consulta que la relaciona</summary>
		public long IdConsulta
		{
			get	{ return _IdConsulta; }
			set	{ _IdConsulta = value; }
		}
		/// <summary>Propiedad, Fecha de inicio de la incapacidad</summary>
		public DateTime FechaInicio
		{
			get	{ return _FechaInicio; }
			set	{ _FechaInicio = value; }
		}
		/// <summary>Propiedad, Fecha de fin de la incapacidad</summary>
		public DateTime FechaFin
		{
			get	{ return _FechaFin; }
			set	{ _FechaFin = value; }
		}
		/// <summary>Propiedad, Indica si la incapacidad es una continuación</summary>
		public bool Continuacion
		{
			get	{ return _Continuacion; }
			set	{ _Continuacion = value; }
		}
		/// <summary>Propiedad, Indica si la incapacidad es una transcripción</summary>
		public bool Transcripcion
		{
			get	{ return _Transcripcion; }
			set	{ _Transcripcion = value; }
		}
		/// <summary>Propiedad, Observaciones de la incapacidad</summary>
		public string Observaciones
		{
			get	{ return _Observaciones; }
			set	{ _Observaciones = value; }
		}
		/// <summary>Propiedad, Arreglo que contiene los diagnósticos de la incapacidad</summary>
		public ArrayList IncapacidadDiagnosticos
		{
			get	{ return _IncapacidadDiagnosticos; }
			set	{ _IncapacidadDiagnosticos = value; }
		}
		
		#endregion	
		
		#region Methods
		
		public Incapacidad()
		{
		}
	
		public Incapacidad(long IdIncapacidad, long IdSolicitud, long IdConsulta, DateTime FechaInicio, DateTime FechaFin, bool Continuacion, bool Transcripcion, string Observaciones)
		{
			_IdIncapacidad = IdIncapacidad;
			_IdSolicitud = IdSolicitud;
			_IdConsulta = IdConsulta;
			_FechaInicio = FechaInicio;
			_FechaFin = FechaFin;
			_Continuacion = Continuacion;
			_Transcripcion = Transcripcion;
			_Observaciones = Observaciones;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultIncapacidad()
		{
			DataSet dsList;
			try
			{
				dsList= this.List();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dsList;
		}

		/// <summary>
		/// Método para la inserción
		/// </summary>
		/// <returns>Id insertado</returns>
		public long InsertIncapacidad()
		{
			try
			{
				this.IdIncapacidad = Convert.ToInt64(this.Insert());

				if(this.IncapacidadDiagnosticos != null)
				{
					foreach(IncapacidadDiagnosticos objDiagnostico in this.IncapacidadDiagnosticos)
					{
						objDiagnostico.objTransaction = this.objTransaction;
						objDiagnostico.IdIncapacidad = this.IdIncapacidad;					
						objDiagnostico.InsertIncapacidadDiagnosticos();			
					}
				}	
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return this.IdIncapacidad;
		}

		/// <summary>
		/// Método para la modificación
		/// </summary>
		public void UpdateIncapacidad()
		{
			try
			{
				this.Update();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método para la eliminación
		/// </summary>
		public void DeleteIncapacidad()
		{
			try
			{
				this.Delete();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void GetIncapacidad()
		{
			try
			{
				this.Consult();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}



        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ListIncapacidadAusentismo(object dateFrom, object dateUntil, string empresas, string sedes, string usuarios, string medicos)
        {
            DataSet dsList;

            try
            {
                dsList = this.List("IncapacidadAusentismo", dateFrom, dateUntil, empresas, sedes, usuarios, medicos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

		
		#endregion
		
			
	}
}


