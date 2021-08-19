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
	/// Esta clase provee la funcionalidad para ingresar una empresa al sistema
	/// </summary>
	/// <remarks>Autor: Ricardo Silva</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class SIC_EMPRESA : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, id de la empresa</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Nombre de la empresa</summary>
		private string _Nombre;
        /// <summary>Atributo, Nombre de la empresa</summary>
        private string _AbreviacionEmpresa;
		/// <summary>Atributo, Identificacion de la empresa</summary>
		private string _Identificacion;
		/// <summary>Atributo, tipo de cliente </summary>
		private int _Tipo_cliente;
		/// <summary>Atributo, Tipo de empresa (Jumbo, large....)</summary>
		private int _Tipo_empresa;
		/// <summary>Atributo, Id empresa padre</summary>
		private int _Id_empresa_padre;
		/// <summary>Atributo, dirección de ubicacion de la empresa </summary>
		private string _Direccion;
		/// <summary>Atributo, telefono de contacto de la empresa </summary>
		private string _Telefono;
		/// <summary>Atributo, ciudad donde reside la empresa</summary>
		private int _Ciudad_id;
		/// <summary>Atributo, correo electronico de contacto de la empresa </summary>
		private string _Correo;
		/// <summary>Atributo, fax de la empresa</summary>
		private string _Fax;
        /// <summary>Atributo, persona de contacto de la empresa</summary>
        private string _Contacto;
        /// <summary>Atributo, Dias en los que caducara el password</summary>
        private int _DiasCaducaPassword;
        /// <summary>Atributo, numero de intentos en los que se bloqueara el usuario</summary>
        private int _IntentosBloqueaPassword;

        #region Permisos

        /// <summary>Atributo, indica si la empresa realiza autorizaciones</summary>
        private int _RealizaAutorizaciones;
        /// <summary>Atributo, indica si la empresa realiza reembolsos</summary>
        private int _RealizaReembolsos;
        /// <summary>Atributo, indica si la empresa realiza consultas</summary>
        private int _RealizaConsultas;
        /// <summary>Atributo, indica si la empresa realiza agendamiento de citas</summary>
        private int _RealizaCitasAgenda;

        #endregion

        #region Divisiones

        private int _DivColesterolGlicemia;
        /// <summary>Atributo, </summary>
        private int _DivExamenesLaboratorio;
        /// <summary>Atributo, </summary>
        private int _DivMujer;
        /// <summary>Atributo, </summary>
        private int _DivAudiometria;
        /// <summary>Atributo, </summary>
        private int _DivWellness;
        /// <summary>Atributo, </summary>
        private int _DivHabitoFumar;
        /// <summary>Atributo, </summary>
        private int _DivConsumoAlcohol;
        /// <summary>Atributo, </summary>
        private int _DivVacunacion;
        /// <summary>Atributo, </summary>
        private int _DivSedentarismo;
        /// <summary>Atributo, </summary>
        private int _DivSaludOral;
        /// <summary>Atributo, </summary>
        private int _DivEstres;
        /// <summary>Atributo, </summary>
        private int _DivEmocional;
        /// <summary>Atributo, </summary>
        private int _DivAccidentalidad;
        /// <summary>Atributo, </summary>
        private int _DivEstadoSalud;
        /// <summary>Atributo, </summary>
        private int _DivNutricion;
        /// <summary>Atributo, </summary>
        private int _DivAntecedentesAusentismo;
        /// <summary>Atributo, </summary>
        private int _DivRecomendaciones;
        /// <summary>Atributo, </summary>
        private int _DivDiastolicaSisTolica;
        /// <summary>Atributo, </summary>
        private int _DivPerimetroAbdominal;

        #endregion 


        #endregion

        #region Properties

        /// <summary>Propiedad, </summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Nombre
		{
			get	{ return _Nombre; }
			set	{ _Nombre = value; }
		}
        /// <summary>Propiedad, </summary>
        public string AbreviacionEmpresa
		{
            get { return _AbreviacionEmpresa; }
            set { _AbreviacionEmpresa = value; }
		}     
        /// <summary>Propiedad, </summary>
		public string Identificacion
		{
			get	{ return _Identificacion; }
			set	{ _Identificacion = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Tipo_cliente
		{
			get	{ return _Tipo_cliente; }
			set	{ _Tipo_cliente = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Tipo_empresa
		{
			get	{ return _Tipo_empresa; }
			set	{ _Tipo_empresa = value; }
		}
		/// <summary>Propiedad, </summary>
		public int Id_empresa_padre
		{
			get	{ return _Id_empresa_padre; }
			set	{ _Id_empresa_padre = value; }
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
		public int Ciudad_id
		{
			get	{ return _Ciudad_id; }
			set	{ _Ciudad_id = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Correo
		{
			get	{ return _Correo; }
			set	{ _Correo = value; }
		}
		/// <summary>Propiedad, </summary>
		public string Fax
		{
			get	{ return _Fax; }
			set	{ _Fax = value; }
		}
        /// <summary>Propiedad, </summary>
        public string Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DiasCaducaPassword
        {
            get { return _DiasCaducaPassword; }
            set { _DiasCaducaPassword = value; }
        }
        /// <summary>Propiedad, </summary>
        public int IntentosBloqueaPassword
        {
            get { return _IntentosBloqueaPassword; }
            set { _IntentosBloqueaPassword = value; }
        }

        #region Permisos

        /// <summary>Propiedad, </summary>
        public int RealizaAutorizaciones
        {
            get { return _RealizaAutorizaciones; }
            set { _RealizaAutorizaciones = value; }
        }
        /// <summary>Propiedad, </summary>
        public int RealizaReembolsos
        {
            get { return _RealizaReembolsos; }
            set { _RealizaReembolsos = value; }
        }
        /// <summary>Propiedad, </summary>
        public int RealizaConsultas
        {
            get { return _RealizaConsultas; }
            set { _RealizaConsultas = value; }
        }
        /// <summary>Propiedad, </summary>
        public int RealizaCitasAgenda
        {
            get { return _RealizaCitasAgenda; }
            set { _RealizaCitasAgenda = value; }
        }

        #endregion

        #region empresaDivisiones

        /// <summary>Propiedad, </summary>
        public int DivColesterolGlicemia
        {
            get { return _DivColesterolGlicemia; }
            set { _DivColesterolGlicemia = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivExamenesLaboratorio
        {
            get { return _DivExamenesLaboratorio; }
            set { _DivExamenesLaboratorio = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivMujer
        {
            get { return _DivMujer; }
            set { _DivMujer = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivAudiometria
        {
            get { return _DivAudiometria; }
            set { _DivAudiometria = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivWellness
        {
            get { return _DivWellness; }
            set { _DivWellness = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivHabitoFumar
        {
            get { return _DivHabitoFumar; }
            set { _DivHabitoFumar = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivConsumoAlcohol
        {
            get { return _DivConsumoAlcohol; }
            set { _DivConsumoAlcohol = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivVacunacion
        {
            get { return _DivVacunacion; }
            set { _DivVacunacion = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivSedentarismo
        {
            get { return _DivSedentarismo; }
            set { _DivSedentarismo = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivSaludOral
        {
            get { return _DivSaludOral; }
            set { _DivSaludOral = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivEstres
        {
            get { return _DivEstres; }
            set { _DivEstres = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivEmocional
        {
            get { return _DivEmocional; }
            set { _DivEmocional = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivAccidentalidad
        {
            get { return _DivAccidentalidad; }
            set { _DivAccidentalidad = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivEstadoSalud
        {
            get { return _DivEstadoSalud; }
            set { _DivEstadoSalud = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivNutricion
        {
            get { return _DivNutricion; }
            set { _DivNutricion = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivAntecedentesAusentismo
        {
            get { return _DivAntecedentesAusentismo; }
            set { _DivAntecedentesAusentismo = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivRecomendaciones
        {
            get { return _DivRecomendaciones; }
            set { _DivRecomendaciones = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivDiastolicaSisTolica
        {
            get { return _DivDiastolicaSisTolica; }
            set { _DivDiastolicaSisTolica = value; }
        }
        /// <summary>Propiedad, </summary>
        public int DivPerimetroAbdominal
        {
            get { return _DivPerimetroAbdominal; }
            set { _DivPerimetroAbdominal = value; }
        }          

    #endregion	
		
		#endregion	
		
		#region Methods
		
		public SIC_EMPRESA()
		{
		}
	
		public SIC_EMPRESA(int Empresa_id, string Nombre, string Identificacion, int Tipo_cliente, int Tipo_empresa, int Id_empresa_padre, string Direccion, string Telefono, int Ciudad_id, string Correo, string Fax)
		{
			_Empresa_id = Empresa_id;
			_Nombre = Nombre;
			_Identificacion = Identificacion;
			_Tipo_cliente = Tipo_cliente;
			_Tipo_empresa = Tipo_empresa;
			_Id_empresa_padre = Id_empresa_padre;
			_Direccion = Direccion;
			_Telefono = Telefono;
			_Ciudad_id = Ciudad_id;
			_Correo = Correo;
			_Fax = Fax;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSIC_EMPRESA()
		{
			DataSet dsList;
			try
			{
				this.typeConnection = Connection.EnumConnections.ConnectionSICAU;
				dsList = this.consultarProc("SICAM_EXPORT_EMPRESA");
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dsList;
		}

        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 11-02-10
        /// Funcionalidad: Inserta o actualiza una empresa en la tabla sic_empresa
        /// </summary>
        /// <returns></returns>
        public bool InsertSIC_EMPRESA(ArrayList lstParentescos, ArrayList lstTipoServicios,String titulo, String texto, int idUser)
        {
            int idEmpresa;

            try
            {
                this.BeginTransaction();

                idEmpresa = Convert.ToInt32(this.Insert());

                if (lstParentescos != null)
                {
                    foreach (int IdParentescos in lstParentescos)
                    {
                        this.Insert("EmpresaParentescos", idEmpresa, IdParentescos);
                    }
                }

                if (lstTipoServicios != null)
                {

                    foreach (int IdTipoServicios in lstTipoServicios)
                    {
                        this.Insert("EmpresaTipoServicios", idEmpresa, IdTipoServicios, texto, titulo);
                    }
                }

                this.Insert("EmpresaPrimerUsuario", idEmpresa, idUser);

                this.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
                return false;
            }
        }

       /// <summary>
       /// Método para la consulta
       /// </summary>
       /// <returns>DataSet con los resultados de la consulta</returns>
       public void GetSicEmpresa()
        {
            try
            {
                this.Consult();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }


       /// <summary>
       /// Método para la modificación
       /// </summary>
       public void UpdateEmpresa(ArrayList lstParentescos, ArrayList lstTipoServicios, String titulo, String texto)
       {
           int idEmpresa;

           try
           {
               this.BeginTransaction();

               idEmpresa = Convert.ToInt32(this.Update());

               //Recarga la lista de parentescos seleccionados
               if (lstParentescos != null)
                   this.Delete("ParentescosEmpresa", idEmpresa);

               if (lstParentescos != null)
               {
                   foreach (int IdParentescos in lstParentescos)
                   {
                       this.Insert("EmpresaParentescos", idEmpresa, IdParentescos);
                   }
               }

               //Recarga la lista de los tipos de servicio seleccionados
               if (lstTipoServicios != null)
                   this.Delete("EmpresaTipoServicios", idEmpresa);

               if (lstTipoServicios != null)
               {

                   foreach (int IdTipoServicios in lstTipoServicios)
                   {
                       this.Insert("EmpresaTipoServicios", idEmpresa, IdTipoServicios, texto, titulo);
                   }
               }

               this.CommitTransaction();
           }
           catch (Exception ex)
           {
               this.RollbackTransaction();
               throw ex;
           }
       }

       /// <summary>
       /// Método para la consulta
       /// </summary>
       /// <returns>DataSet con los resultados de la consulta</returns>
       public void  GetSicEmpresaDivisiones()
       {
           try
           {
               this.Consult("SIC_EMPRESA_DIVISIONES");
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultEmpresa()
        {
            DataSet dsList;
            try
            {
                dsList = this.List();
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


