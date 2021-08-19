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
	public class EmpresaParentescos : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id de la empresa en SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Id del parentesco al que puede hacer reembolso y autorizaciones</summary>
		private short _IdParentesco;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id de la empresa en SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Id del parentesco al que puede hacer reembolso y autorizaciones</summary>
		public short IdParentesco
		{
			get	{ return _IdParentesco; }
			set	{ _IdParentesco = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public EmpresaParentescos()
		{
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultEmpresaParentescos()
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
		public int InsertEmpresaParentescos()
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
		public void UpdateEmpresaParentescos()
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
		public void DeleteEmpresaParentescos()
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
		public void GetEmpresaParentescos()
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


