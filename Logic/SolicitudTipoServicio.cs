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
	public class SolicitudTipoServicio : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del grupo de tipo de solicitud de la solicitud</summary>
		private long _IdSolicitudTipoServicio;
		/// <summary>Atributo, Id de la solicitud a  la que pertenece</summary>
		private long _IdSolicitud;
		/// <summary>Atributo, Id del tipo de servicio </summary>
		private int _IdTipoServicio;
		/// <summary>Atributo, Id del proveedor</summary>
		private int _IdProveedor;
		/// <summary>Atributo, Id del tipo de atencion</summary>
		private short _IdTipoAtencion;
		/// <summary>Atributo, Id de la clase de atencion</summary>
		private short _IdClaseAtencion;
		/// <summary>Atributo, Id de la contingencia</summary>
		private short _IdContingencia;
		/// <summary>Atributo, Número de la factura</summary>
		private string _NumeroFactura;
		/// <summary>Atributo, Fecha de la factura</summary>
		private DateTime _FechaFactura;
		/// <summary>Atributo, Valor total  de la factura</summary>
		private decimal _ValorFactura;
		/// <summary>Atributo, Arreglo que contiene los servicio del tipo de solicitud</summary>
		private ArrayList _SolicitudServicios;
		/// <summary>Atributo, Fecha en que se radica la factura</summary>
		private DateTime _FechaRadicacionFactura;
		/// <summary>Atributo, Número de la cuenta de cobro relacionada a la factura</summary>
		private string _NumeroCuentaCobro;
		/// <summary>Atributo, Fecha de confirmación de liquidación de la factura</summary>
		private DateTime _FechaConfirmacion;
		/// <summary>Atributo, Id de el estado de la solicitud en general</summary>
		private short _IdSolicitudEstado;
		/// <summary>Atributo, Id del tipo se solicitud</summary>
		private short _IdTipoSolicitud;
		/// <summary>Atributo, Id del tipo se consecutivo que debe tener el tipo de solicitud</summary>
		private short _IdTipoConsecutivo;
		/// <summary>Atributo, Número consecutivo para nombrar la solicitud</summary>
		private long _Consecutivo;
		/// <summary>Atributo, Nombre del consecutivo de la solicitud</summary>
		private string _ConsecutivoNombre;
		/// <summary>Atributo, Id de la empresa en SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Arreglo que contiene los proveedores del tipo de servicio de la solicitud</summary>
		private ArrayList _SolicitudTipoServicioProveedores;
		/// <summary>Atributo, Arreglo que contiene los diagnosticos del tipo de servicio de la solicitud</summary>
		private ArrayList _SolicitudTipoServicioDiagnosticos;
		/// <summary>Atributo, Nombre de unidad que aprueba</summary>
		private string _UnidadAprobacion;
		/// <summary>Atributo, Valor total de servicios convenidos o solicitados</summary>
		private decimal _ValorConvenioSolicitado;
		/// <summary>Atributo, Valor total de los valores aprobados</summary>
		private decimal _ValorAprobado;
		/// <summary>Atributo, Indica si glosa o no glosa</summary>
		private bool _Glosa;	
		/// <summary>Atributo, Fecha de modificacion en el sistema</summary>
		private DateTime _FechaModificacion;
		/// <summary>Atributo, Fecha de liquidación en el sistema</summary>
		private DateTime _FechaLiquidacion;
		/// <summary>Atributo, Id del usuario de liquidacion si el usuario es del sistema</summary>
		private int _IdUserLiquidacion;
		/// <summary>Atributo, Id del usuario de liquidacion si el usuario es del sistema SICAU</summary>
		private int _Usuario_idLiquidacion;
		/// <summary>Atributo, Observaciones cuando hay anulación de la solicitud</summary>
		private string _ObservacionAnulacion;
		/// <summary>Atributo, Comentarios</summary>
		private string _Comentarios;
		/// <summary>Atributo, Cantidad de veces en que se solicitó la impresión</summary>
		private short _Impresiones;
		/// <summary>Atributo, Id del prestador que generó tipo de servicio</summary>
		private int _IdPrestador;
		/// <summary>Atributo, Id de la especialidad para remisiones</summary>
		private int _IdEspecialidad;  
		/// <summary>Atributo, Indica el detalle de servicios si es ingresada por el empleado</summary>
		private string _DetalleServicios;
		/// <summary>Atributo, Indica el detalle de los diagnósticos si es ingresada por el empleado</summary>
		private string _DescripcionDiagnosticos;

		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del grupo de tipo de solicitud de la solicitud</summary>
		public long IdSolicitudTipoServicio
		{
			get	{ return _IdSolicitudTipoServicio; }
			set	{ _IdSolicitudTipoServicio = value; }
		}
		/// <summary>Propiedad, Id de la solicitud a  la que pertenece</summary>
		public long IdSolicitud
		{
			get	{ return _IdSolicitud; }
			set	{ _IdSolicitud = value; }
		}
		/// <summary>Propiedad, Id del tipo de servicio </summary>
		public int IdTipoServicio
		{
			get	{ return _IdTipoServicio; }
			set	{ _IdTipoServicio = value; }
		}
		/// <summary>Propiedad, Id del proveedor</summary>
		public int IdProveedor
		{
			get	{ return _IdProveedor; }
			set	{ _IdProveedor = value; }
		}
		/// <summary>Propiedad, Id del tipo de atencion</summary>
		public short IdTipoAtencion
		{
			get	{ return _IdTipoAtencion; }
			set	{ _IdTipoAtencion = value; }
		}
		/// <summary>Propiedad, Id de la clase de atencion</summary>
		public short IdClaseAtencion
		{
			get	{ return _IdClaseAtencion; }
			set	{ _IdClaseAtencion = value; }
		}
		/// <summary>Propiedad, Id de la contingencia</summary>
		public short IdContingencia
		{
			get	{ return _IdContingencia; }
			set	{ _IdContingencia = value; }
		}
		/// <summary>Propiedad, Número de la factura</summary>
		public string NumeroFactura
		{
			get	{ return _NumeroFactura; }
			set	{ _NumeroFactura = value; }
		}
		/// <summary>Propiedad, Fecha de la factura</summary>
		public DateTime FechaFactura
		{
			get	{ return _FechaFactura; }
			set	{ _FechaFactura = value; }
		}
		/// <summary>Propiedad, Valor total  de la factura</summary>
		public decimal ValorFactura
		{
			get	{ return _ValorFactura; }
			set	{ _ValorFactura = value; }
		}
		/// <summary>Propiedad, Arreglo que contiene los tipos de servicio de la solicitud</summary>
		public ArrayList SolicitudServicios
		{
			get	{ return _SolicitudServicios; }
			set	{ _SolicitudServicios = value; }
		}
		/// <summary>Propiedad, Fecha en que se radica la factura</summary>
		public DateTime FechaRadicacionFactura
		{
			get	{ return _FechaRadicacionFactura; }
			set	{ _FechaRadicacionFactura = value; }
		}
		/// <summary>Propiedad, Número de la cuenta de cobro relacionada a la factura</summary>
		public string NumeroCuentaCobro
		{
			get	{ return _NumeroCuentaCobro; }
			set	{ _NumeroCuentaCobro = value; }
		}
		/// <summary>Propiedad, Fecha de confirmación de liquidación de la factura</summary>
		public DateTime FechaConfirmacion
		{
			get	{ return _FechaConfirmacion; }
			set	{ _FechaConfirmacion = value; }
		}
		/// <summary>Propiedad, Id de el estado de la solicitud en general</summary>
		public short IdSolicitudEstado
		{
			get	{ return _IdSolicitudEstado; }
			set	{ _IdSolicitudEstado = value; }
		}
		/// <summary>Propiedad, Id del tipo se solicitud</summary>
		public short IdTipoSolicitud
		{
			get	{ return _IdTipoSolicitud; }
			set	{ _IdTipoSolicitud = value; }
		}
		/// <summary>Propiedad, Id del tipo se consecutivo</summary>
		public short IdTipoConsecutivo
		{
			get	{ return _IdTipoConsecutivo; }
			set	{ _IdTipoConsecutivo = value; }
		}
		/// <summary>Propiedad, Número consecutivo para nombrar la solicitud</summary>
		public long Consecutivo
		{
			get	{ return _Consecutivo; }
			set	{ _Consecutivo = value; }
		}
		/// <summary>Propiedad, Nombre del consecutivo de la solicitud</summary>
		public string ConsecutivoNombre
		{
			get	{ return _ConsecutivoNombre; }
			set	{ _ConsecutivoNombre = value; }
		}
		/// <summary>Propiedad, Id de la empresa en SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Arreglo que contiene los proveedores del tipo de servicio de la solicitud</summary>
		public ArrayList SolicitudTipoServicioProveedores
		{
			get	{ return _SolicitudTipoServicioProveedores; }
			set	{ _SolicitudTipoServicioProveedores = value; }
		}
		/// <summary>Propiedad, Arreglo que contiene los diagnósticos del tipo de servicio de la solicitud</summary>
		public ArrayList SolicitudTipoServicioDiagnosticos
		{
			get	{ return _SolicitudTipoServicioDiagnosticos; }
			set	{ _SolicitudTipoServicioDiagnosticos = value; }
		}
		/// <summary>Propiedad, Nombre de la unidad que aprueba</summary>
		public string UnidadAprobacion
		{
			get	{ return _UnidadAprobacion; }
			set	{ _UnidadAprobacion = value; }
		}
		/// <summary>Propiedad, Indica si glosa o no glosa</summary>
		public bool Glosa
		{
			get	{ return _Glosa; }
			set	{ _Glosa = value; }
		}		
		/// <summary>Propiedad, Valor total de servicios convenidos o solicitados</summary>
		public decimal ValorConvenioSolicitado
		{
			get	{ return _ValorConvenioSolicitado; }
			set	{ _ValorConvenioSolicitado = value; }
		}
		/// <summary>Propiedad, Valor total de los valores aprobados</summary>
		public decimal ValorAprobado
		{
			get	{ return _ValorAprobado; }
			set	{ _ValorAprobado = value; }
		}
		/// <summary>Propiedad, Fecha de creación en el sistema</summary>
		public DateTime FechaLiquidacion
		{
			get	{ return _FechaLiquidacion; }
			set	{ _FechaLiquidacion = value; }
		}
		/// <summary>Propiedad, Fecha de creación en el sistema</summary>
		public DateTime FechaModificacion
		{
			get	{ return _FechaModificacion; }
			set	{ _FechaModificacion = value; }
		}
		/// <summary>Propiedad, Id del usuario de creación si el usuario es del sistema</summary>
		public int IdUserLiquidacion
		{
			get	{ return _IdUserLiquidacion; }
			set	{ _IdUserLiquidacion = value; }
		}
		/// <summary>Propiedad, Id del usuario de creación si el usuario es del sistema SICAU</summary>
		public int Usuario_idLiquidacion
		{
			get	{ return _Usuario_idLiquidacion; }
			set	{ _Usuario_idLiquidacion = value; }
		}
		/// <summary>Propiedad, Observaciones cuando hay anulación de la solicitud</summary>
		public string ObservacionAnulacion
		{
			get	{ return _ObservacionAnulacion; }
			set	{ _ObservacionAnulacion = value; }
		}
		/// <summary>Propiedad, Comentarios</summary>
		public string Comentarios
		{
			get	{ return _Comentarios; }
			set	{ _Comentarios = value; }
		}
		/// <summary>Propiedad, Cantidad de veces en que se solicitó la impresión</summary>
		public short Impresiones
		{
			get	{ return _Impresiones; }
			set	{ _Impresiones = value; }
		}
		/// <summary>Propiedad, Id del prestador que generó la solicitud</summary>
		public int IdPrestador
		{
			get	{ return _IdPrestador; }
			set	{ _IdPrestador = value; }
		}
		/// <summary>Propiedad, Id de la especialidad para remisiones</summary>
		public int IdEspecialidad
		{
			get        { return _IdEspecialidad; }
			set        { _IdEspecialidad = value; }
		}
		/// <summary>Propiedad, Detalle de los servicios de solicitudes de empleados</summary>
		public string DetalleServicios
		{
			get	{ return _DetalleServicios; }
			set	{ _DetalleServicios = value; }
		}
		/// <summary>Propiedad, Detalle de diagnósticos de solicitudes de empleados</summary>
		public string DescripcionDiagnosticos
		{
			get	{ return _DescripcionDiagnosticos; }
			set	{ _DescripcionDiagnosticos = value; }
		}

		
		#endregion	
		
		#region Methods
		
		public SolicitudTipoServicio()
		{
		}
	
		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitudTipoServicio()
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
		public DataSet ConsultSolicitudTipoServicioBusqueda(object dateFrom, object dateUntil, object dateCreateFrom, object dateCreateUntil, int idProveedor, int mesLiquidacion, int anoLiquidacion, int idSolicitudEstado1, int idSolicitudEstado2, int idSolicitudEstado3, long id_empleado, long id_beneficiario, long consecutivoDesde, long consecutivoHasta, int idTipoServicio, int migradas, object fechaConfirmacion)
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudTipoServicioBusqueda", this.IdTipoSolicitud, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3, dateFrom, dateUntil, dateCreateFrom, dateCreateUntil, idProveedor, this.ConsecutivoNombre, this.Empresa_id, mesLiquidacion, anoLiquidacion, id_empleado, id_beneficiario, this.NumeroFactura, consecutivoDesde, consecutivoHasta, idTipoServicio, migradas, fechaConfirmacion);
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
		public DataSet ConsultSolicitudTipoServicioEstado(Solicitud objSolicitud, int idSolicitudEstado1, int idSolicitudEstado2, int idSolicitudEstado3)
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudTipoServicioEstado", objSolicitud.IdSolicitud, objSolicitud.IdTipoSolicitud, idSolicitudEstado1, idSolicitudEstado2, idSolicitudEstado3, objSolicitud.Empresa_id);
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
		public DataSet ConsultSolicitudTipoServicioFormatos()
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudTipoServicioFormatos");
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
        public DataSet ConsultSolicitudTipoServicioFormatosControl()
        {
            DataSet dsList;
            try
            {
                dsList = this.List("SolicitudTipoServicioFormatosControl");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

		/// <summary>
		/// Método para la inserción
		/// </summary>
		/// <returns>Id insertado</returns>
		public int InsertSolicitudTipoServicio()
		{
			int id;
			try
			{
				id = Convert.ToInt32(this.Insert());
			
				if(this.SolicitudTipoServicioDiagnosticos != null)
				{
					foreach(SolicitudTipoServicioDiagnosticos objDiagnostico in this.SolicitudTipoServicioDiagnosticos)
					{
						objDiagnostico.objTransaction = this.objTransaction;
						objDiagnostico.IdSolicitud = this.IdSolicitud;
						objDiagnostico.IdSolicitudTipoServicio = id;
						objDiagnostico.InsertSolicitudTipoServicioDiagnosticos();			
					}
				}				
				if(this.SolicitudTipoServicioProveedores != null)
				{
					foreach(SolicitudTipoServicioProveedores objProveedor in this.SolicitudTipoServicioProveedores)
					{
						if(objProveedor.IdProveedor != 0)
						{
							objProveedor.objTransaction = this.objTransaction;
							objProveedor.IdSolicitud = this.IdSolicitud;
							objProveedor.IdSolicitudTipoServicio = id;
							objProveedor.InsertSolicitudTipoServicioProveedores();								
						}
					}
				}
				this.IdSolicitudTipoServicio = id;
				this.Update("SolicitudTipoServicioConsecutivo");
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
		public void UpdateSolicitudTipoServicio()
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
		/// Método para la modificación
		/// </summary>
		public void UpdateSolicitudTipoServicioImpresiones()
		{
			try
			{
				this.Update("SolicitudTipoServicioImpresiones", this.IdSolicitudTipoServicio, this.IdSolicitud);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Método para modificar el estado dependiendo del estado de los servicios
		/// </summary>
		public short UpdateEstadoSolicitudTipoServicio()
		{
			try
			{
				return Convert.ToInt16(this.Update("EstadoSolicitudTipoServicio",this.IdSolicitud, this.IdSolicitudTipoServicio));
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método para la modificación cuando hay cambio de estado de la solicitud
		/// </summary>
		public void UpdateSolicitudTipoServicioSolicitudEstado()
		{
			try
			{
				if(this.IdProveedor != 0)				
					this.Update("SolicitudTipoServicioSolicitudEstado");
				else
					if(this.FechaConfirmacion != new DateTime(1900,1,1) && this.FechaConfirmacion != new DateTime(1,1,1))
						this.Update("SolicitudTipoServicioSolicitudEstado",this.IdSolicitud, this.IdSolicitudTipoServicio,0,null,null,null,this.FechaConfirmacion,null,null,this.IdSolicitudEstado,this.ObservacionAnulacion);
					else
						this.Update("SolicitudTipoServicioSolicitudEstado",this.IdSolicitud, this.IdSolicitudTipoServicio,0,null,null,null,null,null,null,this.IdSolicitudEstado,this.ObservacionAnulacion);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método para la modificación del estado de una solicitud
		/// </summary>
		/// <returns>Id insertado</returns>
		public void UpdateSolicitudTipoServicioEstado(Solicitud objSolicitud)
		{
			try
			{
				this.BeginTransaction();
										
				this.UpdateSolicitudTipoServicioSolicitudEstado();

				foreach(SolicitudServicio objSolicitudServicio in this.SolicitudServicios)
				{
					objSolicitudServicio.objTransaction = this.objTransaction;						
					objSolicitudServicio.UpdateSolicitudServicioSolicitudEstado();
				}
					
				if(objSolicitud.IdPresupuestoEmpresa != 0)
				{
					PresupuestosEmpresa objPresupuestoEmpresa = new PresupuestosEmpresa();
					objPresupuestoEmpresa.objTransaction = this.objTransaction;
					objPresupuestoEmpresa.IdPresupuestoEmpresa = objSolicitud.IdPresupuestoEmpresa;
					objPresupuestoEmpresa.UpdateValorPresupuestosEmpresa();
				}
				if(objSolicitud.IdPresupuestoIndividuo != 0)
				{
					PresupuestosIndividuo objPresupuestoIndividuo = new PresupuestosIndividuo();
					objPresupuestoIndividuo.objTransaction = this.objTransaction;
					objPresupuestoIndividuo.IdPresupuestoIndividuo = objSolicitud.IdPresupuestoIndividuo;
					objPresupuestoIndividuo.UpdatePresupuestosIndividuoValor();
				}
				
				if(this.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado))
				{
					this.UpdateSolicitudLiquidacion();
				
					//Cerrar el caso en SICAU si se va a liquidar
					if(objSolicitud.Id_solicitud_SICAU != 0)
					{
						SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();					
						objEmpleado.CerrarCasoSIC_EMPLEADO(objSolicitud.Id_solicitud_SICAU);
					}
				}
				
				this.UpdateEstadoSolicitudTipoServicio();

				objSolicitud.objTransaction = this.objTransaction;
				objSolicitud.UpdateEstadoSolicitud();

				this.CommitTransaction();
			}
			catch(Exception ex)
			{
				this.RollbackTransaction();
				throw ex;
			}		
		}

		/// <summary>
		/// Método para la modificación cuando hay cambio de estado de la solicitud
		/// </summary>
		public void UpdateSolicitudLiquidacion()
		{
			try
			{
				this.Update("SolicitudLiquidacion");
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}


		/// <summary>
		/// Método para la eliminación
		/// </summary>
		public void DeleteSolicitudTipoServicio()
		{			
			try
			{
				this.BeginTransaction();			
				
				this.Delete("SolicitudesServicioSolicitudTipoServicio",this.IdSolicitud,this.IdSolicitudTipoServicio);
				this.Delete("SolicitudTipoServicioDiagnosticosSolicitudTipoServicio",this.IdSolicitud, this.IdSolicitudTipoServicio);
				this.Delete("SolicitudTipoServicioProveedoresSolicitudTipoServicio",this.IdSolicitud, this.IdSolicitudTipoServicio);
				this.Delete(this.IdSolicitudTipoServicio,this.IdSolicitud);	
				this.CommitTransaction();
			}
			catch(Exception ex)
			{
				this.RollbackTransaction();
				throw ex;
			}
		}

		/// <summary>
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void GetSolicitudTipoServicio()
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
		/// Método para la modificación del estado de una solicitud
		/// </summary>
		/// <returns>Id insertado</returns>
		public void UpdateLiquidarTipoServicioSolicitud(Solicitud objSolicitud)
		{
			short idSolicitudEstado;
		

			try
			{
				this.BeginTransaction();

				
				this.Update("SolicitudTipoServicioEstado");	

				foreach(SolicitudServicio objSolicitudServicio in this.SolicitudServicios)
				{
					objSolicitudServicio.objTransaction = this.objTransaction;						
					objSolicitudServicio.UpdateSolicitudServicioTipoServicioSolicitudEstado();
				}
				
				objSolicitud.objTransaction = this.objTransaction;
				idSolicitudEstado = Convert.ToInt16(objSolicitud.UpdateEstadoSolicitud());
				
				if(idSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado))
				{
					this.UpdateSolicitudLiquidacion();				
					//Cerrar el caso en SICAU si se va a liquidar
					if(objSolicitud.Id_solicitud_SICAU != 0)
					{
						SIC_EMPLEADO objEmpleado = new SIC_EMPLEADO();					
						objEmpleado.CerrarCasoSIC_EMPLEADO(objSolicitud.Id_solicitud_SICAU);
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
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void ReversarFactura()
		{
			try
			{
				this.Update("SolicitudTipoServicioFactura", this.NumeroFactura, this.Empresa_id, this.IdProveedor);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		
		#endregion
		
		#region Enumeration

		/// <summary>
		/// Enumeración, lista los tipos de permisos
		/// </summary>
		public enum EnumTipoConsecutivos
		{
			Nuevo = 1,
			Asociado = 2,
			Igual = 3
		}

		#endregion
			
	}
}


