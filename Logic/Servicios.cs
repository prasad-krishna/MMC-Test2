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
	public class Servicios : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del servicio</summary>
		private int _IdServicio;
		/// <summary>Atributo, Id del tipo de servicio</summary>
		private short _IdTipoServicio;
		/// <summary>Atributo, Nombre del servicio</summary>
		private string _NombreServicio;
		/// <summary>Atributo, Código del servicio</summary>
		private string _CodigoServicio;
		/// <summary>Atributo, Valor convenido o sugerido del servicio</summary>
		private decimal _ValorConvenio;
		/// <summary>Atributo, Nombre con varios campos concatenados</summary>
		private string _NombreCompleto;
		/// <summary>Atributo, Id de la empresa</summary>
		private int _empresa_id;
		/// <summary>Atributo, Texto informativo incluye</summary>
		private string _incluye;
		/// <summary>Atributo, Texto informativo excluye</summary>
		private string _excluye;
		/// <summary>Atributo, Texto informativo simultaneo</summary>
		private string _simultaneo;
		/// <summary>Atributo, Indica si esta activo</summary>			
		private int _Activo;
		/// <summary>Atributo, Arreglo que contiene los tipos de servicio</summary>
		private ArrayList _TipoServicios;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del servicio</summary>
		public int IdServicio
		{
			get	{ return _IdServicio; }
			set	{ _IdServicio = value; }
		}
		/// <summary>Propiedad, Id del tipo de servicio</summary>
		public short IdTipoServicio
		{
			get	{ return _IdTipoServicio; }
			set	{ _IdTipoServicio = value; }
		}
		/// <summary>Propiedad, Nombre del servicio</summary>
		public string NombreServicio
		{
			get	{ return _NombreServicio; }
			set	{ _NombreServicio = value; }
		}
		/// <summary>Propiedad, Código del servicio</summary>
		public string CodigoServicio
		{
			get	{ return _CodigoServicio; }
			set	{ _CodigoServicio = value; }
		}
		/// <summary>Propiedad, Valor convenido o sugerido del servicio</summary>
		public decimal ValorConvenio
		{
			get	{ return _ValorConvenio; }
			set	{ _ValorConvenio = value; }
		}
		/// <summary>Propiedad, Nombre con varios campos concatenados</summary>
		public string NombreCompleto
		{
			get	{ return _NombreCompleto; }
			set	{ _NombreCompleto = value; }
		}
		/// <summary>Propiedad, Id de la empresa en el sistema SICAU</summary>
		public int empresa_id
		{
			get	{ return _empresa_id; }
			set	{ _empresa_id = value; }
		}
		/// <summary>Propiedad, Nombre con varios campos concatenados</summary>
		public string Incluye
		{
			get { return _incluye; }
			set { _incluye = value; }
		}
		/// <summary>Propiedad, Nombre con varios campos concatenados</summary>
		public string Excluye
		{
			get { return _excluye; }
			set { _excluye = value; }
		}
		/// <summary>Propiedad, Nombre con varios campos concatenados</summary>
		public string Simultaneo
		{
			get { return _simultaneo; }
			set { _simultaneo = value; }
		}
		/// <summary>Propiedad, Activo con varios campos concatenados</summary>
		public int Activo
		{
			get { return _Activo; }
			set { _Activo = value; }
		}
		/// <summary>Propiedad, Listados de tipo de servicios</summary>
		public ArrayList TipoServicios
		{
			get	{ return _TipoServicios; }
			set	{ _TipoServicios = value; }
		}
		
		
		#endregion	

		#region Enumeration

		/// <summary>
		/// Enumeración, lista las acciones de registro en el log del sistema
		/// </summary>
		public enum EnumTiposServicio
		{
			Urgencias=1,
			ConsultaExternaGeneral=2,
			ConsultaExternaEspecializada=3,
			Internación=4,
			HospitalizaciónTtoQx=5,
			CirugíaAmbulatoria=6,
			ProcedimientosenConsultorio=7,
			Odontología=8,
			LaboratorioClínico=9,
			Imagenología=10,
			Patología=11,
			ExámenesEspecialesdeDiagnóstico=12,
			Terapias=13,
			ApoyoTerapeutico=14,
			BancodeSangre=15,
			TratamientodeCancer=16,
			Medicamentos=17,
			Vacunas=18,
			Suministros=19,
			ControlNutricional = 20,
			Odontologia = 21,
			Procedimientos = 22,
			ExamenesDiagnosticos = 23,
			Remisiones = 24
		}

		#endregion
		
		#region Methods
		
		public Servicios()
		{
		}
	
		public Servicios(int IdServicio, short IdTipoServicio, string NombreServicio, string CodigoServicio, decimal ValorConvenio)
		{
			_IdServicio = IdServicio;
			_IdTipoServicio = IdTipoServicio;
			_NombreServicio = NombreServicio;
			_CodigoServicio = CodigoServicio;
			_ValorConvenio = ValorConvenio;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultServicios()
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
		public DataSet ConsultServicioTipoServicios()
		{
			DataSet dsList;
			try
			{
				dsList= this.List("ServicioTipoServicio", this.IdServicio);
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
		public int InsertServicios()
		{
			int id;
			try
			{
				this.BeginTransaction();	
				id = Convert.ToInt32(this.Insert());

				if(this.TipoServicios != null)
				{
					foreach(int idTipoServicio in this.TipoServicios)
					{
						this.Insert("ServicioTipoServicio",id,idTipoServicio);
					}
				}

				this.CommitTransaction();
				
			}
			catch(Exception ex)
			{
				this.RollbackTransaction();
				throw ex;
			}
			return id;
		}

		/// <summary>
		/// Método para la modificación
		/// </summary>
		public void UpdateServicios()
		{
			try
			{
				this.BeginTransaction();
				this.Delete("ServicioTipoServicio",this.IdServicio);
				this.Update();

				if(this.TipoServicios != null)
				{
					foreach(int idTipoServicio in this.TipoServicios)
					{
						this.Insert("ServicioTipoServicio",this.IdServicio,idTipoServicio);
					}
				}

				this.CommitTransaction();
				
			}
			catch(Exception ex)
			{
				this.RollbackTransaction();
				throw ex;
			}
		}

		/// <summary>
		/// Método para la eliminación
		/// </summary>
		public void DeleteServicios()
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
		public void GetServicios()
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


