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
	/// Esta clase provee la funcionalidad para administrar consultas genéricas a tablas
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: 7 de octubre de 2008</remarks>
	public class GeneralTable : GeneralProcess
	{
		#region Attributes

		/// <summary>Atributo, Nombre de la tabla en la que se va a realizar la consulta</summary>
		private string _TableName;
		/// <summary>Atributo, Nombre general de las columna para realizar la consulta</summary>
		private string _ColumnName;
		/// <summary>Atributo, Id de la empresa en el sistema SICAU para filtro</summary>
		private int _empresa_id;
		/// <summary>Atributo, Id general</summary>
		private int _Id;
		/// <summary>Atributo, Nombre general</summary>
		private string _Nombre;
		/// <summary>Atributo, Id del usuario del sistema</summary>
		private int _IdUser;
		/// <summary>Atributo, Id del usuario del sistema SICAU</summary>
		private int _Usuario_id;
		/// <summary>Atributo, Nombre general de las columna para realizar la consulta</summary>
		private string _ColumnId;
		/// <summary>Atributo, Id del servicio que se consulta</summary>
		private int _codigoServicio;
        ///HC-New functionalities-001, PORTOMX, RAM, 15/09/2015
        /// <summary>Atributo, Ordenar columna o columnas para realizar la consulta</summary>
        private string _Sort;
        ///HC-New functionalities-001, PORTOMX, RAM, 15/09/2015
        /// <summary>Atributo, condicional o condicionales para realizar la consulta</summary>
        private string _Where;


		#endregion

		#region Properties

		/// <summary>Propiedad, Nombre de la tabla en la que se va a realizar la consulta</summary>
		public string TableName
		{
			get	{ return _TableName; }
			set	{ _TableName = value; }
		}
		/// <summary>Propiedad, Nombre general de las columna para realizar la consulta</summary>
		public string ColumnName
		{
			get	{ return _ColumnName; }
			set	{ _ColumnName = value; }
		}
		/// <summary>Propiedad, Id de la empresa en el sistema SICAU para filtro</summary>
		public int empresa_id
		{
			get	{ return _empresa_id; }
			set	{ _empresa_id = value; }
		}
		
		/// <summary>Propiedad, Id general</summary>
		public int Id
		{
			get	{ return _Id; }
			set	{ _Id = value; }
		}
		/// <summary>Propiedad, Nombre general</summary>
		public string Nombre
		{
			get	{ return _Nombre; }
			set	{ _Nombre = value; }
		}
		/// <summary>Propiedad, Id del usuario del sistema</summary>
		public int IdUser
		{
			get	{ return _IdUser; }
			set	{ _IdUser = value; }
		}
		/// <summary>Propiedad, Id del usuario del sistema SICAU</summary>
		public int Usuario_id
		{
			get	{ return _Usuario_id; }
			set	{ _Usuario_id = value; }
		}
		/// <summary>Propiedad, Nombre general de las columna para realizar la consulta</summary>
		public string ColumnId
		{
			get { return _ColumnId; }
			set { _ColumnId = value; }
		}
		/// <summary>Propiedad, id que se envia al procedimiento para su consulta.</summary>
		public int codigoServicio
		{
			get { return _codigoServicio; }
			set { _codigoServicio = value; }
		}
        /// HC-New functionalities-001, PORTOMX, RAM, 15/09/2015
        /// <summary>Propiedad,  Ordenar columna o columnas para realizar la consulta</summary>
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        /// HC-New functionalities-001, PORTOMX, RAM, 15/09/2015
        /// <summary>Propiedad, condicional o condicionales para realizar la consulta</summary>
        public string Where
        {
            get { return _Where; }
            set { _Where = value; }
        }
		

		
		#endregion

		#region Methods

		public GeneralTable()
		{
			
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
        /// HC-New functionalities-002, PORTOMX, RAM, 15/09/2015
		public DataSet ConsultGeneralTable()
		{
			DataSet dsList;
			try
			{
				dsList= this.List("GeneralTable", this.TableName, this.ColumnName, this.Sort, this.Where);
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
		public DataSet ConsultGeneralTableSP(string p_nameStoreProcedure, params object[] p_arParams)
		{
			DataSet dsList;
			
			try
			{
				dsList= this.List(p_nameStoreProcedure, p_arParams);
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
		public DataSet ConsultGeneralTableEmpresa()
		{
			DataSet dsList;
			
			try
			{
				dsList= this.List("GeneralTableEmpresa", this.TableName, this.ColumnName, this.empresa_id);
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
		public DataSet ConsultGeneralTableEmpresaActivos()
		{
			DataSet dsList;
			
			try
			{
				dsList= this.List("GeneralTableEmpresaActivos", this.TableName, this.ColumnName, this.empresa_id);
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
		public DataSet ConsultGeneralTableUsuario()
		{
			DataSet dsList;
			
			try
			{
				dsList= this.List("GeneralTableUsuario", this.TableName, this.ColumnName, this.IdUser, this.Usuario_id, this.empresa_id);
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return dsList;
		}

		/// <summary>
		/// Método para la carga de un objeto
		/// </summary>
		public void GetGeneralTable()
		{
			try
			{
				this.Consult("GeneralTable", this.TableName, this.ColumnName, this.Id);
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
		public DataSet ConsultGeneralTableIdTexto()
		{
			DataSet dsList;

			try
			{
				dsList = this.List("GeneralTableIdText", this.TableName, this.ColumnName, this.ColumnId);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return dsList;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta WM</returns>
        public DataSet ConsultGeneralServicioTipoServicio(int IdConsulta)
		{
			DataSet dsList;

			try
			{
                dsList = this.List("ServicioTipoServicio", IdConsulta);
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
