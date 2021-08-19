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
	public class SIC_EMPLEADO : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, </summary>
		private int _Id_empleado;
		/// <summary>Atributo, </summary>
		private string _Identificacion;
		/// <summary>Atributo, </summary>
		private string _Primer_nombre;
		/// <summary>Atributo, </summary>
		private string _Apellido_paterno;
		/// <summary>Atributo, </summary>
		private string _Apellido_materno;
		/// <summary>Atributo, </summary>
		private int _Estado_civil;
		/// <summary>Atributo, </summary>
		private DateTime _Fecha_nacimiento;
		/// <summary>Atributo, </summary>
		private string _Direccion;
		/// <summary>Atributo, </summary>
		private string _Telefono;
		/// <summary>Atributo, </summary>
		private string _Celular;
		/// <summary>Atributo, </summary>
		private int _Sexo;
		/// <summary>Atributo, </summary>
		private string _Tipo_documento;
		/// <summary>Atributo, </summary>
		private int _Centro_costo_id;
		/// <summary>Atributo, </summary>
		private string _Segundo_nombre;
		/// <summary>Atributo, </summary>
		private int _Estado;
		/// <summary>Atributo, </summary>
		private string _IPS;
		/// <summary>Atributo, </summary>
		private string _EPS;
		/// <summary>Atributo, </summary>
		private string _PlanComplementario;
		/// <summary>Atributo, </summary>
		private float _Sueldo;
		/// <summary>Atributo, </summary>
		private int _Tipo_sueldo;
		/// <summary>Atributo, </summary>
		private string _Cargo;
		/// <summary>Atributo, </summary>
		private string _Profesion;
		/// <summary>Atributo, </summary>
		private int _Tipo_doc;
		/// <summary>Atributo, </summary>
		private string _Nombre_centro_costo;
		/// <summary>Atributo, </summary>
		private string _Codigo_centro_costo;
		/// <summary>Atributo, </summary>
		private string _Codigo;
		/// <summary>Atributo, </summary>
		private string _Nombre_completo;
		/// <summary>Atributo, </summary>
		private string _Correo;
		/// <summary>Atributo, </summary>
		private int _Edad;
		/// <summary>Atributo, </summary>
		private string _NombreCiudad;
		/// <summary>Atributo, </summary>
		private int _Empresa_id;
		/// <summary>Atributo, </summary>
		private DateTime _fecha_ingreso_salud;
		/// <summary>Atributo, </summary>
		private string _NombreEstado;
		/// <summary>Atributo, </summary>
		private DateTime _fecha_egreso;
        /// <summary>Atributo, Indica el id del plan de medicamento si la empresa lo requiere</summary>
        private string _IdPlanMedicamentos;
        /// <summary>Atributo, Campo para México</summary>
        private string _locDescripcion;
		
		
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, </summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
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
		public string Primer_nombre
		{
			get	{ return _Primer_nombre; }
			set	{ _Primer_nombre = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Apellido_paterno
		{
			get	{ return _Apellido_paterno; }
			set	{ _Apellido_paterno = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Apellido_materno
		{
			get	{ return _Apellido_materno; }
			set	{ _Apellido_materno = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Estado_civil
		{
			get	{ return _Estado_civil; }
			set	{ _Estado_civil = value; }
		}
		/// <summary>Propiedad, </summary>
		public DateTime Fecha_nacimiento
		{
			get	{ return _Fecha_nacimiento; }
			set	{ _Fecha_nacimiento = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Direccion
		{
			get	{ return _Direccion; }
			set	{ _Direccion = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Telefono
		{
			get	{ return _Telefono; }
			set	{ _Telefono = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Celular
		{
			get	{ return _Celular; }
			set	{ _Celular = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Sexo
		{
			get	{ return _Sexo; }
			set	{ _Sexo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Tipo_documento
		{
			get	{ return _Tipo_documento; }
			set	{ _Tipo_documento = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Centro_costo_id
		{
			get	{ return _Centro_costo_id; }
			set	{ _Centro_costo_id = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Segundo_nombre
		{
			get	{ return _Segundo_nombre; }
			set	{ _Segundo_nombre = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Estado
		{
			get	{ return _Estado; }
			set	{ _Estado = value; }
		}
		/// <summary>Propiedad, </summary>
		public string IPS
		{
			get	{ return _IPS; }
			set	{ _IPS = value; }
		}
		/// <summary>Propiedad, </summary>
		public string EPS
		{
			get	{ return _EPS; }
			set	{ _EPS = value; }
		}
		/// <summary>Propiedad, </summary>
		public string PlanComplementario
		{
			get	{ return _PlanComplementario; }
			set	{ _PlanComplementario = value; }
		}
		/// <summary>Propiedad, </summary>
		public float Sueldo
		{
			get	{ return _Sueldo; }
			set	{ _Sueldo = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Tipo_sueldo
		{
			get	{ return _Tipo_sueldo; }
			set	{ _Tipo_sueldo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Cargo
		{
			get	{ return _Cargo; }
			set	{ _Cargo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Profesion
		{
			get	{ return _Profesion; }
			set	{ _Profesion = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Tipo_doc
		{
			get	{ return _Tipo_doc; }
			set	{ _Tipo_doc = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Nombre_centro_costo
		{
			get	{ return _Nombre_centro_costo; }
			set	{ _Nombre_centro_costo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Codigo_centro_costo
		{
			get	{ return _Codigo_centro_costo; }
			set	{ _Codigo_centro_costo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Codigo
		{
			get	{ return _Codigo; }
			set	{ _Codigo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Nombre_completo
		{
			get	{ return _Nombre_completo; }
			set	{ _Nombre_completo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Correo
		{
			get	{ return _Correo; }
			set	{ _Correo = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Edad
		{
			get	{ return _Edad; }
			set	{ _Edad = value; }
		}
		/// <summary>Propiedad, </summary>
		public string NombreCiudad
		{
			get	{ return _NombreCiudad; }
			set	{ _NombreCiudad = value; }
		}
		/// <summary>Propiedad, </summary>
		public DateTime fecha_ingreso_salud
		{
			get	{ return _fecha_ingreso_salud; }
			set	{ _fecha_ingreso_salud = value; }
		}
		/// <summary>Propiedad, </summary>
		public DateTime fecha_egreso
		{
			get	{ return _fecha_egreso; }
			set	{ _fecha_egreso = value; }
		}
		/// <summary>Propiedad, </summary>
		public string NombreEstado
		{
			get	{ return _NombreEstado; }
			set	{ _NombreEstado = value; }
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
		
		
		#endregion	
		
		#region Methods
		
		public SIC_EMPLEADO()
		{
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSIC_EMPLEADO()
		{
			DataSet dsList;
			try
			{				
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
				dsList = this.consultarProc("SICAM_EXPORT_LISTEMPLEADO", this.Primer_nombre,this.Identificacion,this.Empresa_id);
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
        public DataSet ConsultSIC_USUARIOS(string p_parentescos, string p_idPlanMedicamentos)
		{
			DataSet dsList;
			try
			{				
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
                dsList = this.consultarProc("SICAM_EXPORT_LISTUSUARIOS", this.Primer_nombre, this.Identificacion, this.Empresa_id, this.Estado, p_parentescos, p_idPlanMedicamentos);
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
		public void GetSIC_EMPLEADO()
		{
			try
			{
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
				this.ConsultSpecific("SICAM_EXPORT_EMPLEADO", this.Id_empleado, this.IdPlanMedicamentos);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}	
	
		/// <summary>
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void GetSIC_EMPLEADOByIdentificacion()
		{
			try
			{
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
				this.ConsultSpecific("SICAM_EXPORT_EMPLEADO_IDENTIFICACION", this.Identificacion, this.Empresa_id);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}	



		/// <summary>
		/// Método, cierra una solicitud en SICAU
		/// </summary>
		/// <param name="p_id_solicitud_SICAU"></param>
		public void CerrarCasoSIC_EMPLEADO(int p_id_solicitud_SICAU)
		{		
			try
			{				
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
				this.ExecuteProcedure("SICAM_EXPORT_FINALIZAR_CASO", p_id_solicitud_SICAU);
			}
			catch(Exception ex)
			{
				throw ex;
			}		
		}
		
		#endregion
		
			
	}
}