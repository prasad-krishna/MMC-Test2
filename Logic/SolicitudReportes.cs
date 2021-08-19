/*
'===============================================================================
Delima Mercer (Colombia) Ltda, Sistema Autorizaciones y Reembolsos
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Delima Mercer (Colombia) Ltda.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Delima Mercer (Colombia) Ltda

Copyright (c) 2010 by Delima Mercer (Colombia) Ltda
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
	public class SolicitudReportes : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del reporte de asociación de solicitudes</summary>
		private long _IdSolicitudReporte;
		/// <summary>Atributo, Fecha de creación</summary>
		private DateTime _FechaCreacion;
		/// <summary>Atributo, Id del usuario de creación</summary>
		private int _UsuarioCreacion;
		/// <summary>Atributo, Consecutivo del reporte</summary>
		private long _Consecutivo;
		/// <summary>Atributo, Nombre completo del consecutivo</summary>
		private string _ConsecutivoNombre;
		/// <summary>Atributo, Arreglo que contiene los tipos de servicio de la solicitud</summary>
		private ArrayList _Solicitudes;
		/// <summary>Atributo, Id de la empresa en SICAU</summary>
		private int _Empresa_id;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del reporte de asociación de solicitudes</summary>
		public long IdSolicitudReporte
		{
			get	{ return _IdSolicitudReporte; }
			set	{ _IdSolicitudReporte = value; }
		}
		/// <summary>Propiedad, Fecha de creación</summary>
		public DateTime FechaCreacion
		{
			get	{ return _FechaCreacion; }
			set	{ _FechaCreacion = value; }
		}
		/// <summary>Propiedad, Id del usuario de creación</summary>
		public int UsuarioCreacion
		{
			get	{ return _UsuarioCreacion; }
			set	{ _UsuarioCreacion = value; }
		}
		/// <summary>Propiedad, Consecutivo del reporte</summary>
		public long Consecutivo
		{
			get	{ return _Consecutivo; }
			set	{ _Consecutivo = value; }
		}
		/// <summary>Propiedad, Nombre completo del consecutivo</summary>
		public string ConsecutivoNombre
		{
			get	{ return _ConsecutivoNombre; }
			set	{ _ConsecutivoNombre = value; }
		}

		/// <summary>Propiedad, Arreglo que contiene las solicitudes</summary>
		public ArrayList Solicitudes
		{
			get	{ return _Solicitudes; }
			set	{ _Solicitudes = value; }
		}
		/// <summary>Propiedad, Id de la empresa en SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		
		#endregion	
		
		#region Methods
		
		public SolicitudReportes()
		{
		}
	
		public SolicitudReportes(long IdSolicitudReporte, DateTime FechaCreacion, int UsuarioCreacion, long Consecutivo, string ConsecutivoNombre)
		{
			_IdSolicitudReporte = IdSolicitudReporte;
			_FechaCreacion = FechaCreacion;
			_UsuarioCreacion = UsuarioCreacion;
			_Consecutivo = Consecutivo;
			_ConsecutivoNombre = ConsecutivoNombre;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudReportes()
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
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudesSolicitudReporte()
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudesSolicitudReporte");
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
		public long InsertSolicitudReportes(bool p_borrar, bool p_insertar)
		{
			try
			{
				this.BeginTransaction();

				if(p_borrar)
					this.Delete("SolicitudesSolicitudReportes",this.IdSolicitudReporte);
				if(p_insertar)
					this.IdSolicitudReporte = Convert.ToInt64(this.Insert());

				foreach(Solicitud objSolicitud in this.Solicitudes)
				{
					this.Insert("SolicitudSolicitudReportes", this.IdSolicitudReporte, objSolicitud.IdSolicitud);					
				}

				this.Consult();
			
				this.CommitTransaction();
			}
			catch(Exception ex)
			{
				this.RollbackTransaction();
				throw ex;
			}
			return this.IdSolicitudReporte;
		}

		

		/// <summary>
		/// Método para la modificación
		/// </summary>
		public void UpdateSolicitudReportes()
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
		public void DeleteSolicitudReportes()
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
		public void GetSolicitudReportes()
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


