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

namespace Mercer.Medicines.Logic
{
	/// <summary>
	/// Esta clase provee la funcionalidad para
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class SolicitudTipoServicioProveedores : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del grupo de tipo de solicitud de la solicitud</summary>
		private long _IdSolicitudTipoServicio;
		/// <summary>Atributo, </summary>
		private long _IdSolicitud;
		/// <summary>Atributo, Id del proveedor</summary>
		private int _IdProveedor;
		/// <summary>Atributo, Consecutivo del proveedor</summary>
		private int _Consecutivo;
		/// <summary>Atributo, Indica si despliega la UVR en los formatos</summary>
		private bool _DespliegaUVR;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del grupo de tipo de solicitud de la solicitud</summary>
		public long IdSolicitudTipoServicio
		{
			get	{ return _IdSolicitudTipoServicio; }
			set	{ _IdSolicitudTipoServicio = value; }
		}
		/// <summary>Propiedad, </summary>
		public long IdSolicitud
		{
			get	{ return _IdSolicitud; }
			set	{ _IdSolicitud = value; }
		}
		/// <summary>Propiedad, Id del proveedor</summary>
		public int IdProveedor
		{
			get	{ return _IdProveedor; }
			set	{ _IdProveedor = value; }
		}
		/// <summary>Propiedad, Consecutivo del proveedor</summary>
		public int Consecutivo
		{
			get	{ return _Consecutivo; }
			set	{ _Consecutivo = value; }
		}
		/// <summary>Propiedad, Indica si despliega la UVR en los formatos</summary>
		public bool DespliegaUVR
		{
			get	{ return _DespliegaUVR; }
			set	{ _DespliegaUVR = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public SolicitudTipoServicioProveedores()
		{
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudTipoServicioProveedores()
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
		public int InsertSolicitudTipoServicioProveedores()
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
		/// Método para la eliminación
		/// </summary>
		public void DeleteSolicitudTipoServicioProveedores()
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
		public void GetSolicitudTipoServicioProveedores()
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
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public int GetCantidadProveedoresTipoServicioSolicitud()
		{
			try
			{
				return Convert.ToInt32(this.ExecuteProcedure("GetCantidadProveedoresTipoServicioSolicitud"));
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		
		#endregion
		
			
	}
}

