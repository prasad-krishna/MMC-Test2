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
	public class Medicamentos : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del medicamento</summary>
		private int _IdMedicamento;
		/// <summary>Atributo, Codigo</summary>
		private string _CodigoPos;
		/// <summary>Atributo, Nombre Comercial</summary>
		private string _NombreComercial;
		/// <summary>Atributo, Principio activo</summary>
		private string _PrincipioActivo;
		/// <summary>Atributo, Forma farmaceutica</summary>
		private string _FormaFarmaceutica;
		/// <summary>Atributo, Presentación o empaque </summary>
		private string _Presentacion;
		/// <summary>Atributo, Cantidad</summary>
		private string _CantidadPresentacion;
		/// <summary>Atributo, Concentración</summary>
		private string _Concentracion;
		/// <summary>Atributo, Registro sanitario invima</summary>
		private string _RegistroSanitario;
		/// <summary>Atributo, Régimen</summary>
		private string _Regimen;
		/// <summary>Atributo, Id del laboratorio al que pertenece</summary>
		private int _IdLaboratorio;
		/// <summary>Atributo, Indica si es o no reembolsable</summary>
		private int _Reembolsable;
		/// <summary>Atributo, Id de la empresa</summary>
		private int _empresa_id;
		/// <summary>Atributo, Precio del distribuidor</summary>
		private decimal _PrecioDistribuidor;
		/// <summary>Atributo, Precio al público</summary>
		private decimal _PrecioPublico;
		/// <summary>Atributo, Nombre que concatena varios campos del medicamento</summary>
		private string _NombreCompleto;
		/// <summary>Atributo, Id del tipo de servicio</summary>
		private short _IdTipoServicio;
        /// <summary>Atributo, Indica el medicamento está activo</summary>
        private int _Activo;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del medicamento</summary>
		public int IdMedicamento
		{
			get	{ return _IdMedicamento; }
			set	{ _IdMedicamento = value; }
		}
		/// <summary>Propiedad, Codigo</summary>
		public string CodigoPos
		{
			get	{ return _CodigoPos; }
			set	{ _CodigoPos = value; }
		}
		/// <summary>Propiedad, Nombre Comercial</summary>
		public string NombreComercial
		{
			get	{ return _NombreComercial; }
			set	{ _NombreComercial = value; }
		}
		/// <summary>Propiedad, Principio activo</summary>
		public string PrincipioActivo
		{
			get	{ return _PrincipioActivo; }
			set	{ _PrincipioActivo = value; }
		}
		/// <summary>Propiedad, Forma farmaceutica</summary>
		public string FormaFarmaceutica
		{
			get	{ return _FormaFarmaceutica; }
			set	{ _FormaFarmaceutica = value; }
		}
		/// <summary>Propiedad, Presentación o empaque </summary>
		public string Presentacion
		{
			get	{ return _Presentacion; }
			set	{ _Presentacion = value; }
		}
		/// <summary>Propiedad, Cantidad</summary>
		public string CantidadPresentacion
		{
			get	{ return _CantidadPresentacion; }
			set	{ _CantidadPresentacion = value; }
		}
		/// <summary>Propiedad, Concentración</summary>
		public string Concentracion
		{
			get	{ return _Concentracion; }
			set	{ _Concentracion = value; }
		}
		/// <summary>Propiedad, Registro sanitario invima</summary>
		public string RegistroSanitario
		{
			get	{ return _RegistroSanitario; }
			set	{ _RegistroSanitario = value; }
		}
		/// <summary>Propiedad, Régimen</summary>
		public string Regimen
		{
			get	{ return _Regimen; }
			set	{ _Regimen = value; }
		}
		/// <summary>Propiedad, Id del laboratorio al que pertenece</summary>
		public int IdLaboratorio
		{
			get	{ return _IdLaboratorio; }
			set	{ _IdLaboratorio = value; }
		}
		/// <summary>Propiedad, Indica si es o no reembolsable</summary>
		public int Reembolsable
		{
			get	{ return _Reembolsable; }
			set	{ _Reembolsable = value; }
		}
		/// <summary>Propiedad, Id de la empresa en el sistema SICAU</summary>
		public int empresa_id
		{
			get	{ return _empresa_id; }
			set	{ _empresa_id = value; }
		}
		/// <summary>Propiedad, Precio del distribuidor</summary>
		public decimal PrecioDistribuidor
		{
			get	{ return _PrecioDistribuidor; }
			set	{ _PrecioDistribuidor = value; }
		}
		/// <summary>Propiedad, Precio al público</summary>
		public decimal PrecioPublico
		{
			get	{ return _PrecioPublico; }
			set	{ _PrecioPublico = value; }
		}
		/// <summary>Propiedad, Nombre que concatena varios campos del medicamento</summary>
		public string NombreCompleto
		{
			get	{ return _NombreCompleto; }
			set	{ _NombreCompleto = value; }
		}
		/// <summary>Propiedad, Id del tipo de servicio</summary>
		public short IdTipoServicio
		{
			get	{ return _IdTipoServicio; }
			set	{ _IdTipoServicio = value; }
		}
        /// <summary>Propiedad, Indica si el medicamento se encuentra activo</summary>
        public int Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
		
		
		#endregion	
		
		#region Methods
		
		public Medicamentos()
		{
		}
	
		

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultMedicamentos()
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
		public int InsertMedicamentos()
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
		public void UpdateMedicamentos()
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
		public void DeleteMedicamentos()
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
			public void GetMedicamentos()
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


