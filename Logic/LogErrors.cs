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
	public class LogErrors : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del error</summary>
		private long _IdLogError;
		/// <summary>Atributo, Mensaje de error</summary>
		private string _MessageError;
		/// <summary>Atributo, Id del usuario al que se le presentó el error </summary>
		private int _IdUser;
		/// <summary>Atributo, Id del usuario en SICAU al que se le presentó el error</summary>
		private int _Usuario_id;
		/// <summary>Atributo, Fecha en que se presentó error</summary>
		private DateTime _DateLogError;
		/// <summary>Atributo, IP desde la que se presenta el error</summary>
		private string _IP;
		/// <summary>Atributo, URL donde se presenta el error</summary>
		private string _PageError;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del error</summary>
		public long IdLogError
		{
			get	{ return _IdLogError; }
			set	{ _IdLogError = value; }
		}
		/// <summary>Propiedad, Mensaje de error</summary>
		public string MessageError
		{
			get	{ return _MessageError; }
			set	{ _MessageError = value; }
		}
		/// <summary>Propiedad, Id del usuario al que se le presentó el error </summary>
		public int IdUser
		{
			get	{ return _IdUser; }
			set	{ _IdUser = value; }
		}
		/// <summary>Propiedad, Id del usuario en SICAU al que se le presentó el error</summary>
		public int Usuario_id
		{
			get	{ return _Usuario_id; }
			set	{ _Usuario_id = value; }
		}
		/// <summary>Propiedad, Fecha en que se presentó error</summary>
		public DateTime DateLogError
		{
			get	{ return _DateLogError; }
			set	{ _DateLogError = value; }
		}
		/// <summary>Propiedad, IP desde la que se presenta el error</summary>
		public string IP
		{
			get	{ return _IP; }
			set	{ _IP = value; }
		}
		/// <summary>Propiedad, URL donde se presenta el error</summary>
		public string PageError
		{
			get	{ return _PageError; }
			set	{ _PageError = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public LogErrors()
		{
		}
	
		public LogErrors(long IdLogError, string MessageError, int IdUser, int Usuario_id, DateTime DateLogError, string IP, string PageError)
		{
			_IdLogError = IdLogError;
			_MessageError = MessageError;
			_IdUser = IdUser;
			_Usuario_id = Usuario_id;
			_DateLogError = DateLogError;
			_IP = IP;
			_PageError = PageError;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultLogErrors(object dateFrom, object dateUntil)
		{
			DataSet dsList;
			try
			{
				dsList= this.List(this.IdUser, dateFrom, dateUntil, this.Usuario_id);
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
		public int InsertLogErrors()
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
		public void UpdateLogErrors()
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
		public void DeleteLogErrors()
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
		public void GetLogErrors()
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


