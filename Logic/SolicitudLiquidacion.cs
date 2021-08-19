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

namespace Mercer.Medicines.Logic
{
	/// <summary>
	/// Esta clase provee la funcionalidad para
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class SolicitudLiquidacion : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del proceso de liquidación de la solicitud</summary>
		private long _IdSolicitudLiquidacion;
		/// <summary>Atributo, Id del grupo de tipo de solicitud de la solicitud</summary>
		private long _IdSolicitudTipoServicio;
		/// <summary>Atributo, Id de la solicitud</summary>
		private long _IdSolicitud;
		/// <summary>Atributo, Id del usuario que esta realizando la liquidación</summary>
		private int _IdUserCreacion;
		/// <summary>Atributo, Id del usuario que esta realizando la liquidación</summary>
		private int _Usuario_idCreacion;
		/// <summary>Atributo, Fecha de adición de la liquidación</summary>
		private DateTime _FechaCreacion;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del proceso de liquidación de la solicitud</summary>
		public long IdSolicitudLiquidacion
		{
			get	{ return _IdSolicitudLiquidacion; }
			set	{ _IdSolicitudLiquidacion = value; }
		}
		/// <summary>Propiedad, Id del grupo de tipo de solicitud de la solicitud</summary>
		public long IdSolicitudTipoServicio
		{
			get	{ return _IdSolicitudTipoServicio; }
			set	{ _IdSolicitudTipoServicio = value; }
		}
		/// <summary>Propiedad, Id de la solicitud</summary>
		public long IdSolicitud
		{
			get	{ return _IdSolicitud; }
			set	{ _IdSolicitud = value; }
		}
		/// <summary>Propiedad, Id del usuario que esta realizando la liquidación</summary>
		public int IdUserCreacion
		{
			get	{ return _IdUserCreacion; }
			set	{ _IdUserCreacion = value; }
		}
		/// <summary>Propiedad, Id del usuario que esta realizando la liquidación</summary>
		public int Usuario_idCreacion
		{
			get	{ return _Usuario_idCreacion; }
			set	{ _Usuario_idCreacion = value; }
		}
		/// <summary>Propiedad, Fecha de adición de la liquidación</summary>
		public DateTime FechaCreacion
		{
			get	{ return _FechaCreacion; }
			set	{ _FechaCreacion = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public SolicitudLiquidacion()
		{
		}
	
		public SolicitudLiquidacion(long IdSolicitudLiquidacion, long IdSolicitudTipoServicio, long IdSolicitud, int IdUserCreacion, int Usuario_idCreacion, DateTime FechaCreacion)
		{
			_IdSolicitudLiquidacion = IdSolicitudLiquidacion;
			_IdSolicitudTipoServicio = IdSolicitudTipoServicio;
			_IdSolicitud = IdSolicitud;
			_IdUserCreacion = IdUserCreacion;
			_Usuario_idCreacion = Usuario_idCreacion;
			_FechaCreacion = FechaCreacion;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudLiquidacion()
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
		public int InsertSolicitudLiquidacion()
		{
			int id;
			try
			{
				id = Convert.ToInt32(this.Insert());
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return id;
		}

		/// <summary>
		/// Método para la modificación
		/// </summary>
		public void UpdateSolicitudLiquidacion()
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
		public void DeleteSolicitudLiquidacion()
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
		public void GetSolicitudLiquidacion()
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

		
		#endregion
		
			
	}
}


