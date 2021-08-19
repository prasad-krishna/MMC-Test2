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
	public class SIC_USUARIO : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, </summary>
		private int _Usuario_id;
		/// <summary>Atributo, </summary>
		private string _Nombre;
		/// <summary>Atributo, </summary>
		private string _Cargo;
		/// <summary>Atributo, </summary>
		private int _Perfil;
		/// <summary>Atributo, </summary>
		private int _Estado;
		/// <summary>Atributo, </summary>
		private string _Usr;
		/// <summary>Atributo, </summary>
		private string _Pwd;
		/// <summary>Atributo, </summary>
		private string _Email;
		/// <summary>Atributo, </summary>
		private int _Empresa_id;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, </summary>
		public int Usuario_id
		{
			get	{ return _Usuario_id; }
			set	{ _Usuario_id = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Nombre
		{
			get	{ return _Nombre; }
			set	{ _Nombre = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Cargo
		{
			get	{ return _Cargo; }
			set	{ _Cargo = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Perfil
		{
			get	{ return _Perfil; }
			set	{ _Perfil = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Estado
		{
			get	{ return _Estado; }
			set	{ _Estado = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Usr
		{
			get	{ return _Usr; }
			set	{ _Usr = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Pwd
		{
			get	{ return _Pwd; }
			set	{ _Pwd = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Email
		{
			get	{ return _Email; }
			set	{ _Email = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public SIC_USUARIO()
		{
		}
		

		/// <summary>
		/// Método para la consulta de usuarios en el SICAU
		/// </summary>
		/// <param name="p_opcion">1 consulta todos, 2 consulta específico</param>
		/// <returns>DataSet con los resultados de la consulta</returns>		
		public DataSet ConsultSIC_USUARIO(int p_opcion)
		{
			DataSet dsList;
			try
			{
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
				dsList = this.consultarProc("SICAM_EXPORT_USUARIO", p_opcion, this.Usuario_id);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dsList;
		}

		
		
		#endregion
		
			
	}
}
