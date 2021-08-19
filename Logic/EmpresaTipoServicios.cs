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
	public class EmpresaTipoServicios : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id de la empresa en el sistema SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Id del tipo de servicio</summary>
		private int _IdTipoServicio;
		/// <summary>Atributo, Indica si está activo</summary>
		private bool _Activo;
		/// <summary>Atributo, Número de días de la vigencia de la orden</summary>
		private short _DiasVigencia;
		/// <summary>Atributo, URL del formato para impresión </summary>
		private string _URLFormato;
		/// <summary>Atributo, Texto aclaritivo para el formato</summary>
		private string _TextoFormato;
		/// <summary>Atributo, Titulo de el formato</summary>
		private string _TituloFormato;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id de la empresa en el sistema SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Id del tipo de servicio</summary>
		public int IdTipoServicio
		{
			get	{ return _IdTipoServicio; }
			set	{ _IdTipoServicio = value; }
		}
		/// <summary>Propiedad, Indica si está activo</summary>
		public bool Activo
		{
			get	{ return _Activo; }
			set	{ _Activo = value; }
		}
		/// <summary>Propiedad, Número de días de la vigencia de la orden</summary>
		public short DiasVigencia
		{
			get	{ return _DiasVigencia; }
			set	{ _DiasVigencia = value; }
		}
		/// <summary>Propiedad, URL del formato para impresión</summary>
		public string URLFormato
		{
			get	{ return _URLFormato; }
			set	{ _URLFormato = value; }
		}
		/// <summary>Propiedad, Texto aclaritivo para el formato</summary>
		public string TextoFormato
		{
			get	{ return _TextoFormato; }
			set	{ _TextoFormato = value; }
		}
		/// <summary>Propiedad, Titulo de el formato</summary>
		public string TituloFormato
		{
			get	{ return _TituloFormato; }
			set	{ _TituloFormato = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public EmpresaTipoServicios()
		{
		}
	
		public EmpresaTipoServicios(int Empresa_id, int IdTipoServicio, bool Activo, short DiasVigencia)
		{
			_Empresa_id = Empresa_id;
			_IdTipoServicio = IdTipoServicio;
			_Activo = Activo;
			_DiasVigencia = DiasVigencia;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultEmpresaTipoServicios()
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
		public int InsertEmpresaTipoServicios()
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
		public void UpdateEmpresaTipoServicios()
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
		public void DeleteEmpresaTipoServicios()
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
		public void GetEmpresaTipoServicios()
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


