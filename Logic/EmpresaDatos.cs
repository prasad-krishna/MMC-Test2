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
	public class EmpresaDatos : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id de la empresa en SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Texto de la carta de asociación de solicitudes</summary>
		private string _TextoCarta;
		/// <summary>Atributo, Texto de la parte de firma de la carta del texto de solicitudes</summary>
		private string _FirmaCarta;
		/// <summary>Atributo, Texto encabezado de la carta del texto de solicitudes</summary>
		private string _EncabezadoCarta;
		/// <summary>Atributo, Texto con el valor de la UVR para la empresa</summary>
		private string _ValorUVR;
		/// <summary>Atributo, Letras de abreviación de la empresa</summary>
		private string _AbreviacionEmpresa;
		/// <summary>Atributo, Nombre dado a las preexistencias</summary>
		private string _TituloPreexistencias;
		/// <summary>Atributo, Indica si carga la fecha de hoy por defecto en la solicitud</summary>
		private bool _CargaFechaDefecto;
		/// <summary>Atributo, Id del estado por defecto en el ingreso de la solicitud</summary>
		private short _IdSolicitudEstadoDefecto;
		/// <summary>Atributo, Indica si la empresa realiza autorizaciones</summary>
		private bool _RealizaAutorizaciones;
		/// <summary>Atributo, Indica si la empresa realiza reembolsos</summary>
		private bool _RealizaReembolsos;
		/// <summary>Atributo, Indica si la empresa realiza consultas</summary>
		private bool _RealizaConsultas;
        /// <summary>Atributo, Indica si la empresa realiza citas a través del módulo de la agenda</summary>
        private bool _RealizaCitasAgenda;
		/// <summary>Atributo, Indica si la contraseña se bloquea con el numero de intentos indicados</summary>
		private short _IntentosBloqueaPassword;
		/// <summary>Atributo, Indica si la empresa caduca password con el numero de días indicado</summary>
		private int _DiasCaducaPassword;
        /// <summary>Atributo, Indica el id del plan de medicamento si la empresa lo requiere</summary>
        private string _IdPlanMedicamentos;

        /// Proyecto: AMEX
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 23/02/2010
        /// <summary>        
        /// Funcionalidad: Atributo, Indica el tiempo que tarda en expirar el password temporal
        /// </summary>
        private int _HorasCaducaPassword;

       
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id de la empresa en SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Texto de la carta de asociación de solicitudes</summary>
		public string TextoCarta
		{
			get	{ return _TextoCarta; }
			set	{ _TextoCarta = value; }
		}
		/// <summary>Propiedad, Texto dado a las preexistencias</summary>
		public string TituloPreexistencias
		{
			get	{ return _TituloPreexistencias; }
			set	{ _TituloPreexistencias = value; }
		}
		/// <summary>Propiedad, Texto de la parte de firma de la carta del texto de solicitudes</summary>
		public string FirmaCarta
		{
			get	{ return _FirmaCarta; }
			set	{ _FirmaCarta = value; }
		}
		/// <summary>Propiedad, Texto encabezado de la carta del texto de solicitudes</summary>
		public string EncabezadoCarta
		{
			get	{ return _EncabezadoCarta; }
			set	{ _EncabezadoCarta = value; }
		}
		/// <summary>Propiedad, Letras de la abreviación de la empresa</summary>
		public string AbreviacionEmpresa
		{
			get	{ return _AbreviacionEmpresa; }
			set	{ _AbreviacionEmpresa = value; }
		}
		/// <summary>Propiedad, Texto con el valor de la UVR para la empresa</summary>
		public string ValorUVR
		{
			get	{ return _ValorUVR; }
			set	{ _ValorUVR = value; }
		}
		/// <summary>Propiedad, Indica si carga la fecha de hoy por defecto en la solicitud</summary>
		public bool CargaFechaDefecto
		{
			get	{ return _CargaFechaDefecto; }
			set	{ _CargaFechaDefecto = value; }
		}
		/// <summary>Propiedad, Id del estado por defecto en el ingreso de la solicitud</summary>
		public short IdSolicitudEstadoDefecto
		{
			get	{ return _IdSolicitudEstadoDefecto; }
			set	{ _IdSolicitudEstadoDefecto = value; }
		}
		/// <summary>Propiedad,  Indica si la empresa realiza consultas</summary>
		public bool RealizaConsultas
		{
			get	{ return _RealizaConsultas; }
			set	{ _RealizaConsultas = value; }
		}
		/// <summary>Propiedad,  Indica si la empresa realiza reembolsos</summary>
		public bool RealizaReembolsos
		{
			get	{ return _RealizaReembolsos; }
			set	{ _RealizaReembolsos = value; }
		}
		/// <summary>Propiedad,  Indica si la empresa realiza consultas</summary>
		public bool RealizaAutorizaciones
		{
			get	{ return _RealizaAutorizaciones; }
			set	{ _RealizaAutorizaciones = value; }
		}
        /// <summary>Propiedad,  Indica si la empresa realiza citas a través del módulo de la agenda</summary>
        public bool RealizaCitasAgenda
        {
            get { return _RealizaCitasAgenda; }
            set { _RealizaCitasAgenda = value; }
        }
		/// <summary>Propiedad,  Indica si la contraseña se bloquea con el numero de intentos indicados</summary>
		public short IntentosBloqueaPassword
		{
			get	{ return _IntentosBloqueaPassword; }
			set	{ _IntentosBloqueaPassword = value; }
		}
		/// <summary>Propiedad,  Indica si la empresa caduca password con el numero de días indicado</summary>
		public int DiasCaducaPassword
		{
			get	{ return _DiasCaducaPassword; }
			set	{ _DiasCaducaPassword = value; }
		}

        /// Proyecto: AMEX
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 23/02/2010
        /// <summary>        
        /// Funcionalidad: Atributo, Indica el tiempo que tarda en expirar el password temporal
        /// </summary>
        public int HorasCaducaPassword
        {
            get { return _HorasCaducaPassword; }
            set { _HorasCaducaPassword = value; }
        }

        /// <summary>Propiedad, Indica el id del plan de medicamento si la empresa lo requiere</summary>
        public string IdPlanMedicamentos
        {
            get { return _IdPlanMedicamentos; }
            set { _IdPlanMedicamentos = value; }
        }
		
		#endregion	
		
		#region Methods
		
		public EmpresaDatos()
		{
		}
	
		public EmpresaDatos(int Empresa_id, string TextoCarta, string FirmaCarta)
		{
			_Empresa_id = Empresa_id;
			_TextoCarta = TextoCarta;
			_FirmaCarta = FirmaCarta;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultEmpresaDatos()
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
		public int InsertEmpresaDatos()
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
		public void UpdateEmpresaDatos()
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
		public void DeleteEmpresaDatos()
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
		public void GetEmpresaDatos()
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


