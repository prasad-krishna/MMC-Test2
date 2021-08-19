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
	public class SIC_BENEFICIARIO : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, </summary>
		private int _Beneficiario_id;
		/// <summary>Atributo, </summary>
		private string _Tipo_doc;
		/// <summary>Atributo, </summary>
		private string _Nombre;
		/// <summary>Atributo, </summary>
		private string _Primer_nombre;
		/// <summary>Atributo, </summary>
		private string _Segundo_nombre;
		/// <summary>Atributo, </summary>
		private string _Primer_apellido;
		/// <summary>Atributo, </summary>
		private string _Segundo_apellido;
		/// <summary>Atributo, </summary>
		private DateTime _Fecha_nacimiento;
		/// <summary>Atributo, </summary>
		private DateTime _FechaIngresoPlan;
		/// <summary>Atributo, </summary>
		private string _Parentesco;
		/// <summary>Atributo, </summary>
		private int _IdParentesco;
		/// <summary>Atributo, </summary>
		private string _Telefono;
		/// <summary>Atributo, </summary>
		private string _Direccion;
		/// <summary>Atributo, </summary>
		private int _Genero;
		/// <summary>Atributo, </summary>
		private int _Estado;
		/// <summary>Atributo, </summary>
		private int _Eps;
		/// <summary>Atributo, </summary>
		private int _Ips;
		/// <summary>Atributo, </summary>
		private int _Ciudad;
		/// <summary>Atributo, </summary>
		private string _NombreCiudad;
		/// <summary>Atributo, </summary>
		private int _Id_empleado;
		/// <summary>Atributo, </summary>
		private string _Identificacion;
		/// <summary>Atributo, </summary>
		private int _Opcion;
		/// <summary>Atributo, </summary>
		private int _Edad;
        /// <summary>Atributo, </summary>
        private string _Parentescos;
        /// <summary>Atributo, Indica el id del plan de medicamento si la empresa lo requiere</summary>
        private string _IdPlanMedicamentos;
        /// <summary>Atributo, Campo para México</summary>
        private string _locDescripcion;
        /// <summary>Atributo, Campo para genero</summary>
        private string _sexo_texto;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, </summary>
		public int Beneficiario_id
		{
			get	{ return _Beneficiario_id; }
			set	{ _Beneficiario_id = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Tipo_doc
		{
			get	{ return _Tipo_doc; }
			set	{ _Tipo_doc = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Primer_nombre
		{
			get	{ return _Primer_nombre; }
			set	{ _Primer_nombre = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Nombre
		{
			get	{ return _Nombre; }
			set	{ _Nombre = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Segundo_nombre
		{
			get	{ return _Segundo_nombre; }
			set	{ _Segundo_nombre = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Primer_apellido
		{
			get	{ return _Primer_apellido; }
			set	{ _Primer_apellido = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Segundo_apellido
		{
			get	{ return _Segundo_apellido; }
			set	{ _Segundo_apellido = value; }
		}
		/// <summary>Propiedad, </summary>
		public DateTime Fecha_nacimiento
		{
			get	{ return _Fecha_nacimiento; }
			set	{ _Fecha_nacimiento = value; }
		}
		/// <summary>Propiedad, </summary>
		public DateTime FechaIngresoPlan
		{
			get	{ return _FechaIngresoPlan; }
			set	{ _FechaIngresoPlan = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Parentesco
		{
			get	{ return _Parentesco; }
			set	{ _Parentesco = value; }
		}
		/// <summary>Propiedad, </summary>
		public int IdParentesco
		{
			get	{ return _IdParentesco; }
			set	{ _IdParentesco = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Telefono
		{
			get	{ return _Telefono; }
			set	{ _Telefono = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Direccion
		{
			get	{ return _Direccion; }
			set	{ _Direccion = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Genero
		{
			get	{ return _Genero; }
			set	{ _Genero = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Estado
		{
			get	{ return _Estado; }
			set	{ _Estado = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Eps
		{
			get	{ return _Eps; }
			set	{ _Eps = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Ips
		{
			get	{ return _Ips; }
			set	{ _Ips = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Ciudad
		{
			get	{ return _Ciudad; }
			set	{ _Ciudad = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Id_empleado
		{
			get	{ return _Id_empleado; }
			set	{ _Id_empleado = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Identificacion
		{
			get	{ return _Identificacion; }
			set	{ _Identificacion = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Opcion
		{
			get	{ return _Opcion; }
			set	{ _Opcion = value; }
		}
		/// <summary>Propiedad, </summary>
		public string NombreCiudad
		{
			get	{ return _NombreCiudad; }
			set	{ _NombreCiudad = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Edad
		{
			get	{ return _Edad; }
			set	{ _Edad = value; }
		}
        /// <summary>Propiedad, </summary>
        public string Parentescos
        {
            get { return _Parentescos; }
            set { _Parentescos = value; }
        }
        /// <summary>Propiedad,  Indica el id del plan de medicamento si la empresa lo requiere</summary>
        public string IdPlanMedicamentos
        {
            get { return _IdPlanMedicamentos; }
            set { _IdPlanMedicamentos = value; }
        }
        /// <summary>Propiedad, </summary>
        public string locDescripcion
        {
            get { return _locDescripcion; }
            set { _locDescripcion = value; }
        }

        /// <summary>Propiedad, </summary>
        public string sexo_texto
        {
            get { return _sexo_texto; }
            set { _sexo_texto = value; }
        }


        
		
		
		#endregion	

		#region Enumeration

		/// <summary>
		/// Enumeración, lista los tipos de parentesco
		/// </summary>
		public enum EnumParentescos
		{
			Esposa = 1,
			Hijo = 2,
			PadreMadre = 4,
			Hermano = 71,
			Suegroa = 72,
			Tioa=73,
			Sobrinoa=74,
			Otro = 75				
		}

		#endregion

		#region Methods
		
		public SIC_BENEFICIARIO()
		{
		}
	
		
		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSIC_BENEFICIARIO()
		{
			DataSet dsList;
			try
			{				
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
                dsList = this.consultarProc("SICAM_EXPORT_BENEFICIARIO", this.IdParentesco, this.Id_empleado, this.Beneficiario_id, this.Opcion, this.Parentescos, this.IdPlanMedicamentos);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dsList;
		}

		/// <summary>
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void GetSIC_BENEFICIARIO()
		{
			try
			{
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
                this.ConsultSpecific("SICAM_EXPORT_BENEFICIARIO", this.Parentesco, this.Id_empleado, this.Beneficiario_id, this.Opcion, this.Parentescos, this.IdPlanMedicamentos);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}	
	
		/// <summary>
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void GetSIC_BENEFICIARIOByIdentificacion(int p_empresa_id)
		{
			try
			{
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
				this.ConsultSpecific("SICAM_EXPORT_BENEFICIARIO_IDENTIFICACION", this.Identificacion, p_empresa_id);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		
		#endregion
		
			
	}
}


