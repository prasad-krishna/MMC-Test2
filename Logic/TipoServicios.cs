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
	/// <remarks>Autor: Ricardo Silva</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class TipoServicios : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del tipo de servicio</summary>
		private int _IdTipoServicio;
		/// <summary>Atributo, Nombre del tipo de servicio</summary>
		private string _NombreTipoServicio;
		/// <summary>Atributo, Etiqueta a desplegar en la solcitud del servicio en cantidades</summary>
		private string _EtiquetaCantidad;
		/// <summary>Atributo, Etiqueta a desplegar en la solcitud del servicio en producto</summary>
		private string _EtiquetaProductoServicio;
        /// <summary>Atributo, Texto que aparecera en los formatos</summary>
		private string _TextoFormato;
        /// <summary>Atributo, Titulo que aparecera en los formatos</summary>
		private string _TituloFormato;

		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del tipo de servicio</summary>
		public int IdTipoServicio
		{
			get	{ return _IdTipoServicio; }
			set	{ _IdTipoServicio = value; }
		}
		/// <summary>Propiedad, Nombre del tipo de servicio</summary>
		public string NombreTipoServicio
		{
			get	{ return _NombreTipoServicio; }
			set	{ _NombreTipoServicio = value; }
		}
		/// <summary>Propiedad, Etiqueta a desplegar en la solcitud del servicio en cantidades</summary>
		public string EtiquetaCantidad
		{
			get	{ return _EtiquetaCantidad; }
			set	{ _EtiquetaCantidad = value; }
		}
		/// <summary>Propiedad, Etiqueta a desplegar en la solcitud del servicio en producto</summary>
		public string EtiquetaProductoServicio
		{
			get	{ return _EtiquetaProductoServicio; }
			set	{ _EtiquetaProductoServicio = value; }
		}
        /// <summary>Propiedad, Texto que aparecera en los formatos</summary>
        public string TextoFormato
        {
            get { return _TextoFormato; }
            set { _TextoFormato = value; }
        }
        /// <summary>Propiedad, Titulo que aparecera en los formatos</summary>
        public string TituloFormato
        {
            get { return _TituloFormato; }
            set { _TituloFormato = value; }
        }
		
		
		#endregion	
		
		#region Methods
		
		public TipoServicios()
		{
		}
	
		public TipoServicios(int IdTipoServicio, string NombreTipoServicio, string EtiquetaCantidad, string EtiquetaProductoServicio)
		{
			_IdTipoServicio = IdTipoServicio;
			_NombreTipoServicio = NombreTipoServicio;
			_EtiquetaCantidad = EtiquetaCantidad;
			_EtiquetaProductoServicio = EtiquetaProductoServicio;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultTipoServicios()
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

        /*Proyecto: AMEX
                    Requerimiento: Permisos de crear usuarios
                    Funcionalidad: Permite asociar permisos y reportes a los usuarios
                    Autor: Ricardo Jose Silva Gomez
                    Fecha: 07/06/2011                     
                  */
        //Inicio RJSG 07/06/2011

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultTipoServicioEmpresa(int idEmpresa)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("TipoServicioEmpresa", idEmpresa);
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
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultTipoServiciosAsignados(int idUser)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("UserTipoServiciosAsignados", idUser);
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
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultEmpresaTipoServiciosAsignados(int idEmpresa)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("EmpresaTipoServiciosAsignados", idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        //Fin RJSG 07/06/2011

		/// <summary>
		/// Método para la inserción
		/// </summary>
		/// <returns>Id insertado</returns>
		public int InsertTipoServicios()
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
		public void UpdateTipoServicios()
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
		public void DeleteTipoServicios()
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
		public void GetTipoServicios()
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


        /// <summary>
        /// Método para la carga de un objeto de este tipo
        /// </summary>
        public void GetFormatosTipoServicios(int idEmpresa)
        {
            try
            {
                this.Consult("FormatosTipoServicios", idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		#endregion
		
			
	}
}


