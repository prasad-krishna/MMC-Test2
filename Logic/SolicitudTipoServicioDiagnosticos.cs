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
	/// <remarks>Fecha de creaci�n: </remarks>
	public class SolicitudTipoServicioDiagnosticos : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del grupo de tipo de solicitud de la solicitud</summary>
		private long _IdSolicitudTipoServicio;
		/// <summary>Atributo, </summary>
		private long _IdSolicitud;
		/// <summary>Atributo, Id del diagn�stico m�dico</summary>
		private int _IdDiagnostico;
		/// <summary>Atributo, Tiempo de evoluci�n del dign�stico</summary>
		private decimal _TiempoEvolucion;
		/// <summary>Atributo, Perido de evoluci�n del diagn�stico como d�as, meses, a�os</summary>
		private string _PeriodoEvolucion;
		
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
		/// <summary>Propiedad, Id del diagn�stico m�dico</summary>
		public int IdDiagnostico
		{
			get	{ return _IdDiagnostico; }
			set	{ _IdDiagnostico = value; }
		}
		/// <summary>Propiedad, Tiempo de evoluci�n del dign�stico</summary>
		public decimal TiempoEvolucion
		{
			get	{ return _TiempoEvolucion; }
			set	{ _TiempoEvolucion = value; }
		}
		/// <summary>Propiedad, Perido de evoluci�n del diagn�stico como d�as, meses, a�os</summary>
		public string PeriodoEvolucion
		{
			get	{ return _PeriodoEvolucion; }
			set	{ _PeriodoEvolucion = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public SolicitudTipoServicioDiagnosticos()
		{
		}

		/// <summary>
		/// M�todo para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudTipoServicioDiagnosticos()
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
		/// M�todo para la inserci�n
		/// </summary>
		/// <returns>Id insertado</returns>
		public int InsertSolicitudTipoServicioDiagnosticos()
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
		/// M�todo para la eliminaci�n
		/// </summary>
		public void DeleteSolicitudTipoServicioDiagnosticos()
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
		/// M�todo para la carga de un objeto de este tipo
		/// </summary>
		public void GetSolicitudTipoServicioDiagnosticos()
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


