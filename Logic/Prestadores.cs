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
	public class Prestadores : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del prestador</summary>
		private int _IdPrestador;
		/// <summary>Atributo, Nombre del prestador</summary>
		private string _NombrePrestador;
		/// <summary>Atributo, Id de la especialidad, en caso de nulo no está definida</summary>
		private int _IdEspecialidad;
		/// <summary>Atributo, Nombre de la especialidad, en caso de nulo no está definida</summary>
		private string _NombreEspecialidad;
		/// <summary>Atributo, id de la empresa en el sistema SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Teléfonos del prestador</summary>
		private string _Telefonos;
		/// <summary>Atributo, Direcciones del prestador</summary>
		private string _Direcciones;
		/// <summary>Atributo, Horario de atención del prestador</summary>
		private string _Horario;
		/// <summary>Atributo, Nit del prestador</summary>
		private string _Nit;
		/// <summary>Atributo, Fax del prestador</summary>
		private string _Fax;
		/// <summary>Atributo, Email del prestador</summary>
		private string _Email;
		/// <summary>Atributo, Estado del prestador</summary>
		private short _IdEstadoGeneral;
		/// <summary>Atributo, Ciudad del prestador</summary>
		private int _IdCiudad;
		/// <summary>Atributo, </summary>
		private string _Supraespecialidad;
		/// <summary>Atributo, Fecha</summary>
		private DateTime _FechaIngreso;
		/// <summary>IdTpaAnterior</summary>
		private string _IdTpaAnterior;
		/// <summary>Atributo, id de la empresaproveedor  en el sistema SICAU</summary>
		private int _Empresaproveedor;
		/// <summary>Atributo, id de la empresa prestadora en el sistema SICAU</summary>
		private int _Empresaprestador;
		/// <summary>Atributo, Fecha de modificacion del registro</summary>
		private DateTime _FechaModificacion;
		/// <summary>Atributo, Fecha de retiro</summary>
		private DateTime _FechaRetiro;
		/// <summary>Atributo, Nombre de la persona que aprueba el retiro</summary>
		private string _PersonaAprobacionRetiro;
		/// <summary>Atributo, Nombre de la persona que aprueba el ingreso</summary>
		private string _PersonaAprobacionIngreso;
		/// <summary>Atributo, Motivo del retiro</summary>
		private string _MotivoRetiro;
		/// <summary>Atributo, indica si esta activo para la empresa</summary>
		private bool _Activo;

        //Inicio Marco A. Herrera Gabriel 11/06/2010
        //Se agrega el atributo de cedula profesional e institución a la que pertenece el médico
        /// <summary>Atributo, indica la cedula y la institución del médico</summary>
        private string _Cedula;
        //Fin Marco A. Herrera Gabriel 11/06/2010

        //Inicio Emilio Bueno 17/10/2012
        //Se agregan los atributos de fecha de expedición e institución que otorgó el título profesional
        /// <summary>Atributo, Fecha de expedición título profesional</summary>
        private DateTime _FechaExpedicion;
        /// <summary>Atributo, Institución que otorgó el título profesional</summary>
        private string _Institucion;
        //Fin Emilio Bueno 17/10/2012

		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del prestador</summary>
		public int IdPrestador
		{
			get	{ return _IdPrestador; }
			set	{ _IdPrestador = value; }
		}
		/// <summary>Propiedad, Nombre del prestador</summary>
		public string NombrePrestador
		{
			get	{ return _NombrePrestador; }
			set	{ _NombrePrestador = value; }
		}
		/// <summary>Propiedad, Id de la especialidad, en caso de nulo no está definida</summary>
		public int IdEspecialidad
		{
			get	{ return _IdEspecialidad; }
			set	{ _IdEspecialidad = value; }
		}
		/// <summary>Propiedad, Nombre de la especialidad</summary>
		public string NombreEspecialidad
		{
			get	{ return _NombreEspecialidad; }
			set	{ _NombreEspecialidad = value; }
		}
		/// <summary>Propiedad, id de la empresa en el sistema SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Teléfonos del prestador</summary>
		public string Telefonos
		{
			get	{ return _Telefonos; }
			set	{ _Telefonos = value; }
		}
		/// <summary>Propiedad, Direcciones del prestador</summary>
		public string Direcciones
		{
			get	{ return _Direcciones; }
			set	{ _Direcciones = value; }
		}
		/// <summary>Propiedad, Horario de atención del prestador</summary>
		public string Horario
		{
			get	{ return _Horario; }
			set	{ _Horario = value; }
		}
		/// <summary>Propiedad, Nit del prestador</summary>
		public string Nit
		{
			get	{ return _Nit; }
			set	{ _Nit = value; }
		}
		/// <summary>Propiedad, Fax del prestador</summary>
		public string Fax
		{
			get	{ return _Fax; }
			set	{ _Fax = value; }
		}
		/// <summary>Propiedad, Email del prestador</summary>
		public string Email
		{
			get	{ return _Email; }
			set	{ _Email = value; }
		}
		/// <summary>Propiedad, Estado del prestador</summary>
		public short IdEstadoGeneral
		{
			get	{ return _IdEstadoGeneral; }
			set	{ _IdEstadoGeneral = value; }
		}
		/// <summary>Propiedad, Ciudad del prestador</summary>
		public int IdCiudad
		{
			get	{ return _IdCiudad; }
			set	{ _IdCiudad = value; }
		}
		/// <summary>Propiedad, Supraespecialidad</summary>
		public string Supraespecialidad
		{
			get { return _Supraespecialidad; }
			set { _Supraespecialidad = value; }
		}
		/// <summary>Propiedad, fecha de ingreso</summary>
		public DateTime FechaIngreso
		{
			get { return _FechaIngreso; }
			set { _FechaIngreso = value; }
		}
		/// <summary>Propiedad, Id en el anterior sistema TPA</summary>
		public string IdTpaAnterior
		{
			get { return _IdTpaAnterior; }
			set { _IdTpaAnterior = value; }
		}
		/// <summary>Propiedad, indica relación empresa proveedor</summary>
		public int Empresaproveedor
		{
			get { return _Empresaproveedor; }
			set { _Empresaproveedor = value; }
		}
		/// <summary>Propiedad, indica relación empresa prestador</summary>
		public int Empresaprestador
		{
			get { return _Empresaprestador; }
			set { _Empresaprestador = value; }
		}
		/// <summary>Propiedad, indica el estado en la empresa</summary>
		public bool Activo
		{
			get { return _Activo; }
			set { _Activo = value; }
		}
		/// <summary>Propiedad, Fecha de modificacion del registro</summary>
		public DateTime FechaModificacion
		{
			get	{ return _FechaModificacion; }
			set	{ _FechaModificacion = value; }
		}
		/// <summary>Propiedad, Fecha de retiro</summary>
		public DateTime FechaRetiro
		{
			get	{ return _FechaRetiro; }
			set	{ _FechaRetiro = value; }
		}
		/// <summary>Propiedad, Nombre de la persona que aprueba el retiro</summary>
		public string PersonaAprobacionRetiro
		{
			get	{ return _PersonaAprobacionRetiro; }
			set	{ _PersonaAprobacionRetiro = value; }
		}
		/// <summary>Propiedad, Nombre de la persona que aprueba el ingreso</summary>
		public string PersonaAprobacionIngreso
		{
			get	{ return _PersonaAprobacionIngreso; }
			set	{ _PersonaAprobacionIngreso = value; }
		}
		/// <summary>Propiedad, Motivo del retiro</summary>
		public string MotivoRetiro
		{
			get	{ return _MotivoRetiro; }
			set	{ _MotivoRetiro = value; }
		}

        //Inicio Marco A. Herrera Gabriel 11/06/2010
        //Se agrega el atributo de cedula profesional e institución a la que pertenece el médico
        ///<summary>Atributo, indica la cedula y la institución del médico</summary>
        public string Cedula
        {
            get { return _Cedula; }
            set { _Cedula = value; }
        }
		//Fin MAHG 11/06/2010

        //Inicio Emilio Bueno 17/10/2012
        //Se agregan las propiedades de fecha de expedición e institución que otorgó el título profesional
        /// <summary>Propiedad, Fecha de expedición título profesional</summary>
        public DateTime FechaExpedicion
        {
            get { return _FechaExpedicion; }
            set { _FechaExpedicion = value; }
        }
        /// <summary>Propiedad, Institución que otorgó el título profesional</summary>
        public string Institucion
        {
            get { return _Institucion; }
            set { _Institucion = value; }
        }
        //Fin Emilio Bueno 17/10/2012

		#endregion	
		
		#region Methods
		
		public Prestadores()
		{
		}
	
		public Prestadores(int IdPrestador, string NombrePrestador, int IdEspecialidad)
		{
			_IdPrestador = IdPrestador;
			_NombrePrestador = NombrePrestador;
			_IdEspecialidad = IdEspecialidad;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultPrestadores()
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
		/// Método para la consulta de prestadores de una empresa específica
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultEmpresaPrestadores()
		{
			DataSet dsList;
			try
			{
				dsList= this.List("EmpresaPrestadores");
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
		public int InsertPrestadores()
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
		public void UpdatePrestadores()
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
		public void DeletePrestadores()
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
		public void GetPrestadores()
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


