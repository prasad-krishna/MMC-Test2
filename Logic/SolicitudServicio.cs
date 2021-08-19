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
	/// Esta clase provee la funcionalidad para administrar los datos detallados de cada servicio o producto en la solicitud
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class SolicitudServicio : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, </summary>
		private long _IdSolicitud;
		/// <summary>Atributo, Id de la solicitud del servicio</summary>
		private long _IdSolicitudServicio;
		/// <summary>Atributo, Id del tipo de servicio al que pertenece</summary>
		private long _IdSolicitudTipoServicio;
		/// <summary>Atributo, Id del estado el servicio</summary>
		private short _IdSolicitudEstado;
		/// <summary>Atributo, Id del motivo del estado</summary>
		private short _IdSolicitudMotivo;
		/// <summary>Atributo, Id del medicamento</summary>
		private int _IdMedicamento;
		/// <summary>Atributo, Id del servicio</summary>
		private int _IdServicio;
		/// <summary>Atributo, Cantidad, días o sesiones solicitadas del servicio</summary>
		private string _Cantidad;
		/// <summary>Atributo, Dosis en caso de ser medicamento</summary>
		private string _Dosis;
		/// <summary>Atributo, Duración en caso de ser medicamento</summary>
		private string _Duracion;
		/// <summary>Atributo, Valor aprobado</summary>
		private decimal _ValorAprobado;
		/// <summary>Atributo, Valor en la factura</summary>
		private decimal _ValorFactura;
		/// <summary>Atributo, Valor convenido, sugerido o solicitado</summary>
		private decimal _ValorConvenioSolicitado;
		/// <summary>Atributo, Comentarios </summary>
		private string _Comentarios;
		/// <summary>Atributo, Valor en UVR del servicio</summary>
		private decimal _UVR;
		/// <summary>Atributo, Fecha de prestación del servicio</summary>
		private DateTime _FechaPrestacion;
		/// <summary>Atributo, Valor en UVR del servicio solicitado</summary>
		private decimal _UVRConvenioSolicitado;
		/// <summary>Atributo, Porcentaje de descuento del servicio</summary>
		private decimal _Descuento;
		/// <summary>Atributo, Indica si el servicio fue prestado una vez liquidado</summary>
		private bool _Prestado;
        /// <summary>Atributo, Vía de adminsitración en caso de ser medicamento</summary>
        private string _ViaAdministracion;
		
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, </summary>
		public long IdSolicitud
		{
			get	{ return _IdSolicitud; }
			set	{ _IdSolicitud = value; }
		}
		/// <summary>Propiedad, Id de la solicitud del servicio</summary>
		public long IdSolicitudServicio
		{
			get	{ return _IdSolicitudServicio; }
			set	{ _IdSolicitudServicio = value; }
		}
		/// <summary>Propiedad, Id del tipo de servicio al que pertenece</summary>
		public long IdSolicitudTipoServicio
		{
			get	{ return _IdSolicitudTipoServicio; }
			set	{ _IdSolicitudTipoServicio = value; }
		}
		/// <summary>Propiedad, Id del estado el servicio</summary>
		public short IdSolicitudEstado
		{
			get	{ return _IdSolicitudEstado; }
			set	{ _IdSolicitudEstado = value; }
		}
		/// <summary>Propiedad, Id del motivo del estado</summary>
		public short IdSolicitudMotivo
		{
			get	{ return _IdSolicitudMotivo; }
			set	{ _IdSolicitudMotivo = value; }
		}
		/// <summary>Propiedad, Id del medicamento</summary>
		public int IdMedicamento
		{
			get	{ return _IdMedicamento; }
			set	{ _IdMedicamento = value; }
		}
		/// <summary>Propiedad, Id del servicio</summary>
		public int IdServicio
		{
			get	{ return _IdServicio; }
			set	{ _IdServicio = value; }
		}
		/// <summary>Propiedad, Cantidad, días o sesiones solicitadas del servicio</summary>
		public string Cantidad
		{
			get	{ return _Cantidad; }
			set	{ _Cantidad = value; }
		}
		/// <summary>Propiedad, Dosis en caso de ser medicamento</summary>
		public string Dosis
		{
			get	{ return _Dosis; }
			set	{ _Dosis = value; }
		}
		/// <summary>Propiedad, Duración en caso de ser medicamento</summary>
		public string Duracion
		{
			get	{ return _Duracion; }
			set	{ _Duracion = value; }
		}
		/// <summary>Propiedad, Valor aprobado</summary>
		public decimal ValorAprobado
		{
			get	{ return _ValorAprobado; }
			set	{ _ValorAprobado = value; }
		}
		/// <summary>Propiedad, Valor en la factura</summary>
		public decimal ValorFactura
		{
			get	{ return _ValorFactura; }
			set	{ _ValorFactura = value; }
		}
		/// <summary>Propiedad, Valor convenido, sugerido o solicitado</summary>
		public decimal ValorConvenioSolicitado
		{
			get	{ return _ValorConvenioSolicitado; }
			set	{ _ValorConvenioSolicitado = value; }
		}
		/// <summary>Propiedad, Comentarios </summary>
		public string Comentarios
		{
			get	{ return _Comentarios; }
			set	{ _Comentarios = value; }
		}
		/// <summary>Propiedad, Valor en UVR del servicio</summary>
		public decimal UVR
		{
			get	{ return _UVR; }
			set	{ _UVR = value; }
		}
		/// <summary>Propiedad, Fecha de prestación del servicio</summary>
		public DateTime FechaPrestacion
		{
			get	{ return _FechaPrestacion; }
			set	{ _FechaPrestacion = value; }
		}	
		/// <summary>Propiedad, Valor en UVR del servicio solicitado</summary>
		public decimal UVRConvenioSolicitado
		{
			get	{ return _UVRConvenioSolicitado; }
			set	{ _UVRConvenioSolicitado = value; }
		}
		/// <summary>Propiedad, Porcentaje descuento del servicio</summary>
		public decimal Descuento
		{
			get	{ return _Descuento; }
			set	{ _Descuento = value; }
		}
		/// <summary>Propiedad, Indica si el servicio fue prestado una vez liquidado</summary>
		public bool Prestado
		{
			get	{ return _Prestado; }
			set	{ _Prestado = value; }
		}
        /// <summary>Propiedad, Vía de administración</summary>
        public string ViaAdministracion
        {
            get { return _ViaAdministracion; }
            set { _ViaAdministracion = value; }
        }
		
		#endregion	
		
		#region Methods
		
		public SolicitudServicio()
		{
		}		

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudServicio()
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
		/// Método para la consulta para formatos en estados correctos
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudServicioFormatos()
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudServicioFormatos");
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dsList;
		}

        /// <summary>
        /// Método para la consulta para formatos de medicamentos de control en estados correctos
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultSolicitudServicioFormatosControl(bool p_control)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("SolicitudServicioFormatosControl", this.IdSolicitud, this.IdSolicitudTipoServicio, this.IdSolicitudServicio, p_control);
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
		public DataSet ConsultSolicitudServicioBeneficiario(int p_id_beneficiario, int p_id_empleado, int p_idTipoServicio,DateTime p_fecha_inicio, DateTime p_fecha_fin)
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudServicioBeneficiario",p_id_beneficiario, p_id_empleado ,p_idTipoServicio,p_fecha_inicio,p_fecha_fin);
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
		public int InsertSolicitudServicio()
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
		public void UpdateSolicitudServicio()
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
		/// Método para la modificación cuando hay cambio de estado de la solicitud
		/// </summary>
		public void UpdateSolicitudServicioSolicitudEstado()
		{
			try
			{
				this.Update("SolicitudServicioSolicitudEstado");
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Método para la modificación cuando hay cambio de estado de la solicitud
		/// </summary>
		public void UpdateSolicitudServicioTipoServicioSolicitudEstado()
		{
			try
			{
				this.Update("SolicitudServicioTipoServicioSolictudEstado");
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método para la eliminación
		/// </summary>
		public void DeleteSolicitudServicio()
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
		public void GetSolicitudServicio()
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


