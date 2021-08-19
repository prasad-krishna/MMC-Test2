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
	/// Esta clase provee la funcionalidad para administrar la información principal de la solicitud
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class Solicitud : GeneralProcess, ICloneable	
	{
		#region Attributes
		
		/// <summary>Atributo, Id de la solicitud</summary>
		private long _IdSolicitud;
		/// <summary>Atributo, Id del empleado al que se le realiza la solicitud</summary>
		private int _Id_empleado;
		/// <summary>Atributo, Id del beneficiario al que se realiza la solicitud</summary>
		private int _Beneficiario_id;
		/// <summary>Atributo, Id del tipo se solicitud</summary>
		private short _IdTipoSolicitud;
		/// <summary>Atributo, Fecha ingresada de la solicitud</summary>
		private DateTime _Fecha;
		/// <summary>Atributo, Fecha de creación en el sistema</summary>
		private DateTime _FechaCreacion;
		/// <summary>Atributo, Fecha de inicio de la creación en el sistema</summary>
		private DateTime _FechaInicioCreacion;
		/// <summary>Atributo, Id del diagnóstico</summary>
		private int _IdDiagnostico;
		/// <summary>Atributo, Id del prestador que generó la solicitud</summary>
		private int _IdPrestador;
		/// <summary>Atributo, Observaciones sobre la solicitud</summary>
		private string _Observaciones;
		/// <summary>Atributo, Documentos relacionados a la solicitud</summary>
		private string _Documentos;
		/// <summary>Atributo, Mes de liquidación en que se incluye la solicitud</summary>
		private short _MesLiquidacion;
		/// <summary>Atributo, Año de liquidación en que se incluye la solicitud</summary>
		private short _AnoLiquidacion;
		/// <summary>Atributo, Id de la forma de pago</summary>
		private short _IdFormaPago;
		/// <summary>Atributo, Valor total de servicios convenidos o solicitados</summary>
		private decimal _ValorTotalConvenioSolicitado;
		/// <summary>Atributo, Valor total de los valores aprobados</summary>
		private decimal _ValorTotalAprobado;
		/// <summary>Atributo, Valor total en factura</summary>
		private decimal _ValorTotalFactura;
		/// <summary>Atributo, Indica si glosa o no glosa</summary>
		private bool _Glosa;
		/// <summary>Atributo, Id de el estado de la solicitud en general</summary>
		private short _IdSolicitudEstado;
		/// <summary>Atributo, Valor total utilizado por el empleado</summary>
		private decimal _ValorUtilizadoEmpleado;
		/// <summary>Atributo, Cantidad de solicitudes realizadas por el empleado</summary>
		private int _CantidadSolicitudesEmpleado;
		/// <summary>Atributo, Id del presupuesto al que pertenece o es validada  la solicitud</summary>
		private long _IdPresupuestoIndividuo;
		/// <summary>Atributo, Arreglo que contiene los tipos de servicio de la solicitud</summary>
		private ArrayList _SolicitudTipoServicios;
		/// <summary>Atributo, Fecha de modificacion en el sistema</summary>
		private DateTime _FechaModificacion;
		/// <summary>Atributo, Id del presupuesto al que pertenece o es validada  la solicitud</summary>
		private long _IdPresupuestoEmpresa;
		/// <summary>Atributo, Id del usuario de creación si el usuario es del sistema</summary>
		private int _IdUserCreacion;
		/// <summary>Atributo, Id del usuario de creación si el usuario es del sistema SICAU</summary>
		private int _Usuario_idCreacion;
		/// <summary>Atributo, Número consecutivo para nombrar la solicitud</summary>
		private long _Consecutivo;
		/// <summary>Atributo, Nombre del consecutivo de la solicitud</summary>
		private string _ConsecutivoNombre;
		/// <summary>Atributo, Id de la solicitud en el sistema SICAU para cierre del caso</summary>
		private int _Id_solicitud_SICAU;
		/// <summary>Atributo, Nombre del usuario</summary>
		private string _NameUser;
		/// <summary>Atributo, Id de la empresa en SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Arreglo que contiene las anotaciones fijas asociadas a una solicitud</summary>
		private ArrayList _AnotacionesFijas;
		/// <summary>Atributo, Fecha de liquidación en el sistema</summary>
		private DateTime _FechaLiquidacion;
		/// <summary>Atributo, Id del usuario de liquidacion si el usuario es del sistema</summary>
		private int _IdUserLiquidacion;
		/// <summary>Atributo, Id del usuario de liquidacion si el usuario es del sistema SICAU</summary>
		private int _Usuario_idLiquidacion;
		/// <summary>Atributo, Observaciones cuando hay anulación de la solicitud</summary>
		private string _ObservacionAnulacion;
		/// <summary>Atributo, Id del plan al que pertenece el presupuesto</summary>
		private int _IdPlanSolicitud;
		/// <summary>Atributo, Id de la ciudad del usuario</summary>
		private int _IdCiudad;
		/// <summary>Atributo, Id de la consulta si la solicitud va asociada a esta</summary>
		private long _IdConsulta;
		/// <summary>Atributo, Objeto incapacidad para las ordenes</summary>
		private Incapacidad _objIncapacidad;
		/// <summary>Atributo, Indica si la solicitud fue realizada por el empleado</summary>
		private bool _SolicitudEmpleado;
		/// <summary>Atributo, Id de la ciudad de entrega seleccionado desde el portal</summary>
		private int _IdCiudadEntrega;
		

		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id de la solicitud</summary>
		public long IdSolicitud
		{
			get	{ return _IdSolicitud; }
			set	{ _IdSolicitud = value; }
		}
		/// <summary>Propiedad, Id del empleado al que se le realiza la solicitud</summary>
		public int Id_empleado
		{
			get	{ return _Id_empleado; }
			set	{ _Id_empleado = value; }
		}
		/// <summary>Propiedad, Id del beneficiario al que se realiza la solicitud</summary>
		public int Beneficiario_id
		{
			get	{ return _Beneficiario_id; }
			set	{ _Beneficiario_id = value; }
		}
		/// <summary>Propiedad, Id del tipo se solicitud</summary>
		public short IdTipoSolicitud
		{
			get	{ return _IdTipoSolicitud; }
			set	{ _IdTipoSolicitud = value; }
		}
		/// <summary>Propiedad, Fecha ingresada de la solicitud</summary>
		public DateTime Fecha
		{
			get	{ return _Fecha; }
			set	{ _Fecha = value; }
		}
		/// <summary>Propiedad, Fecha de creación en el sistema</summary>
		public DateTime FechaCreacion
		{
			get	{ return _FechaCreacion; }
			set	{ _FechaCreacion = value; }
		}
		/// <summary>Propiedad, Fecha de inicio de la creación en el sistema</summary>
		public DateTime FechaInicioCreacion
		{
			get	{ return _FechaInicioCreacion; }
			set	{ _FechaInicioCreacion = value; }
		}
		/// <summary>Propiedad, Id del diagnóstico</summary>
		public int IdDiagnostico
		{
			get	{ return _IdDiagnostico; }
			set	{ _IdDiagnostico = value; }
		}
		/// <summary>Propiedad, Id del prestador que generó la solicitud</summary>
		public int IdPrestador
		{
			get	{ return _IdPrestador; }
			set	{ _IdPrestador = value; }
		}
		/// <summary>Propiedad, Observaciones sobre la solicitud</summary>
		public string Observaciones
		{
			get	{ return _Observaciones; }
			set	{ _Observaciones = value; }
		}

		/// <summary>Propiedad, Documentos relacionados con la solicitud</summary>
		public string Documentos
		{
			get	{ return _Documentos; }
			set	{ _Documentos = value; }
		}

		/// <summary>Propiedad, Mes de liquidación en que se incluye la solicitud</summary>
		public short MesLiquidacion
		{
			get	{ return _MesLiquidacion; }
			set	{ _MesLiquidacion = value; }
		}
		/// <summary>Propiedad,  Año de liquidación en que se incluye la solicitud</summary>
		public short AnoLiquidacion
		{
			get	{ return _AnoLiquidacion; }
			set	{ _AnoLiquidacion = value; }
		}
		/// <summary>Propiedad, Id de la forma de pago</summary>
		public short IdFormaPago
		{
			get	{ return _IdFormaPago; }
			set	{ _IdFormaPago = value; }
		}
		/// <summary>Propiedad, Valor total de servicios convenidos o solicitados</summary>
		public decimal ValorTotalConvenioSolicitado
		{
			get	{ return _ValorTotalConvenioSolicitado; }
			set	{ _ValorTotalConvenioSolicitado = value; }
		}
		/// <summary>Propiedad, Valor total de los valores aprobados</summary>
		public decimal ValorTotalAprobado
		{
			get	{ return _ValorTotalAprobado; }
			set	{ _ValorTotalAprobado = value; }
		}
		/// <summary>Propiedad, Valor total en factura</summary>
		public decimal ValorTotalFactura
		{
			get	{ return _ValorTotalFactura; }
			set	{ _ValorTotalFactura = value; }
		}
		/// <summary>Propiedad, Indica si glosa o no glosa</summary>
		public bool Glosa
		{
			get	{ return _Glosa; }
			set	{ _Glosa = value; }
		}
		/// <summary>Propiedad, Id de el estado de la solicitud en general</summary>
		public short IdSolicitudEstado
		{
			get	{ return _IdSolicitudEstado; }
			set	{ _IdSolicitudEstado = value; }
		}
		/// <summary>Propiedad, Valor total utilizado en solicitudes por el empleado</summary>
		public decimal ValorUtilizadoEmpleado
		{
			get	{ return _ValorUtilizadoEmpleado; }
			set	{ _ValorUtilizadoEmpleado = value; }
		}
		/// <summary>Propiedad, Cantidad de solicitudes realizadas por el empleado</summary>
		public int CantidadSolicitudesEmpleado
		{
			get	{ return _CantidadSolicitudesEmpleado; }
			set	{ _CantidadSolicitudesEmpleado = value; }
		}
		/// <summary>Propiedad, Id del presupuesto al que pertenece o es validada  la solicitud</summary>
		public long IdPresupuestoIndividuo
		{
			get	{ return _IdPresupuestoIndividuo; }
			set	{ _IdPresupuestoIndividuo = value; }
		}
		/// <summary>Propiedad, Arreglo que contiene los tipos de servicio de la solicitud</summary>
		public ArrayList SolicitudTipoServicios
		{
			get	{ return _SolicitudTipoServicios; }
			set	{ _SolicitudTipoServicios = value; }
		}
		/// <summary>Propiedad, Anotaciones fijas asociadas a la solicitud</summary>
		public ArrayList AnotacionesFijas
		{
			get	{ return _AnotacionesFijas; }
			set	{ _AnotacionesFijas = value; }
		}
		/// <summary>Propiedad, Fecha de creación en el sistema</summary>
		public DateTime FechaModificacion
		{
			get	{ return _FechaModificacion; }
			set	{ _FechaModificacion = value; }
		}
		/// <summary>Propiedad, Id del presupuesto al que pertenece o es validada  la solicitud</summary>
		public long IdPresupuestoEmpresa
		{
			get	{ return _IdPresupuestoEmpresa; }
			set	{ _IdPresupuestoEmpresa = value; }
		}
		/// <summary>Propiedad, Id del usuario de creación si el usuario es del sistema</summary>
		public int IdUserCreacion
		{
			get	{ return _IdUserCreacion; }
			set	{ _IdUserCreacion = value; }
		}
		/// <summary>Propiedad, Id del usuario de creación si el usuario es del sistema SICAU</summary>
		public int Usuario_idCreacion
		{
			get	{ return _Usuario_idCreacion; }
			set	{ _Usuario_idCreacion = value; }
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
		/// <summary>Propiedad, Id de la solicitud en el sistema SICAU para cierre del caso</summary>
		public int Id_solicitud_SICAU
		{
			get	{ return _Id_solicitud_SICAU; }
			set	{ _Id_solicitud_SICAU = value; }
		}
		/// <summary>Propiedad, Nombre del usuario</summary>
		public string NameUser
		{
			get	{ return _NameUser; }
			set	{ _NameUser = value; }
		}
		/// <summary>Propiedad, Id de la empresa en SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Fecha de creación en el sistema</summary>
		public DateTime FechaLiquidacion
		{
			get	{ return _FechaLiquidacion; }
			set	{ _FechaLiquidacion = value; }
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
		/// <summary>Propiedad, Id del plan al que pertenece el presupuesto</summary>
		public int IdPlanSolicitud
		{
			get	{ return _IdPlanSolicitud; }
			set	{ _IdPlanSolicitud = value; }
		}
		/// <summary>Propiedad, Id de la ciudad del usuario</summary>
		public int IdCiudad
		{
			get	{ return _IdCiudad; }
			set	{ _IdCiudad = value; }
		}
		/// <summary>Propiedad, Id de la consulta si la solicitud va asociada a esta</summary>
		public long IdConsulta
		{
			get	{ return _IdConsulta; }
			set	{ _IdConsulta = value; }
		}
		/// <summary>Propiedad, Objeto incapacidad para las órdenes</summary>
		public Incapacidad objIncapacidad
		{
			get	{ return _objIncapacidad; }
			set	{ _objIncapacidad = value; }
		}
		/// <summary>Propiedad, Indica si la solicitud fue realizada por el empleado</summary>
		public bool SolicitudEmpleado
		{
			get	{ return _SolicitudEmpleado; }
			set	{ _SolicitudEmpleado = value; }
		}
		/// <summary>Propiedad, Id de la ciudad de entrega seleccinada desde el portal</summary>
		public int IdCiudadEntrega
		{
			get	{ return _IdCiudadEntrega; }
			set	{ _IdCiudadEntrega = value; }
		}
		
		#endregion	
		
		#region Enumeration

		/// <summary>
		/// Enumeración, lista los tipos de solicitudes que permite el sistema
		/// </summary>
		public enum EnumTipoSolicitud
		{
			Reembolso = 1,
			Autorizacion = 2,
			Orden = 3
		}

		/// <summary>
		/// Enumeración, lista los estados de una solicitud
		/// </summary>
		public enum EnumEstadoSolicitud
		{
			Pendiente = 1,
			Negado = 2,
			Aprobado = 3,
			Liquidado = 4,
			Anulado = 5
		}


		/// <summary>
		/// Enumeración, lista los motivos de los estados de una solicitud
		/// </summary>
		public enum EnumMotivosEstadoSolicitud
		{
			PorInventario = 1,
            PorAclarar = 2,
            VerificaciónTope = 3,
            PorCobertura = 4,
            Otro = 5,
            PorCobertura2 = 6,
			PorTope = 7,
			Otro2 = 8,
            Aprobado = 9,
            Liquidado = 10,
            Anulado = 11
		}

		#endregion

		#region Methods
		
		public Solicitud()
		{
		}

		
		
		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultSolicitud()
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
		public DataSet ConsultSolicitudAnotacionesFijas()
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudAnotacionesFijas");
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
		public DataSet ConsultSolicitudBusqueda(object dateFrom, object dateUntil, int idProveedor, int migradas, int solicitudEmpleado)
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudBusqueda", this.IdTipoSolicitud, this.IdSolicitudEstado, dateFrom, dateUntil, idProveedor, this.ConsecutivoNombre, this.Empresa_id, this.MesLiquidacion, this.AnoLiquidacion, this.Id_empleado, this.Beneficiario_id, migradas, solicitudEmpleado);
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
		public DataSet ConsultSolicitudBusquedaInternet(object dateFrom, object dateUntil, int idProveedor)
		{
			DataSet dsList;
			try
			{
				dsList= this.List("SolicitudBusquedaInternet", this.IdTipoSolicitud, this.IdSolicitudEstado, dateFrom, dateUntil, idProveedor, this.ConsecutivoNombre, this.Empresa_id, this.Id_empleado, this.Beneficiario_id, this.IdUserCreacion);
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
        public DataSet ConsultSolicitudOrden(int p_id_beneficiario, int p_id_empleado, DateTime p_fecha_inicio, DateTime p_fecha_fin)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("SolicitudOrden", p_id_beneficiario, p_id_empleado, p_fecha_inicio, p_fecha_fin);
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
		public long InsertSolicitud()
		{
			long idSolicitud = 0;
			long idTipoServicio;
			try
			{
				this.BeginTransaction();	
				idSolicitud = Convert.ToInt64(this.Insert());

				foreach(SolicitudTipoServicio objSolicitudTipoServicio in this.SolicitudTipoServicios)
				{
					objSolicitudTipoServicio.objTransaction = this.objTransaction;
					objSolicitudTipoServicio.IdSolicitud = idSolicitud;
					objSolicitudTipoServicio.Empresa_id = this.Empresa_id;
					idTipoServicio = objSolicitudTipoServicio.InsertSolicitudTipoServicio();

					foreach(SolicitudServicio objSolicitudServicio in objSolicitudTipoServicio.SolicitudServicios)
					{
						objSolicitudServicio.objTransaction = this.objTransaction;
						objSolicitudServicio.IdSolicitud = idSolicitud;
						objSolicitudServicio.IdSolicitudTipoServicio = idTipoServicio;
						objSolicitudServicio.InsertSolicitudServicio();
					}

					objSolicitudTipoServicio.IdSolicitudTipoServicio = idTipoServicio;
					objSolicitudTipoServicio.UpdateEstadoSolicitudTipoServicio();
				}

				if(this.AnotacionesFijas != null)
				{
					foreach(short idAnotacionFija in this.AnotacionesFijas)
					{
						this.Insert("SolicitudAnotacionesFijas",idSolicitud,idAnotacionFija);
					}
				}

				if(this.objIncapacidad != null)
				{
					this.objIncapacidad.IdSolicitud = idSolicitud;
					this.objIncapacidad.objTransaction = this.objTransaction;
					this.objIncapacidad.InsertIncapacidad();
				}

				this.IdSolicitud = idSolicitud;
				this.UpdateEstadoSolicitud();

				if(this.IdPresupuestoEmpresa != 0)
				{
					PresupuestosEmpresa objPresupuestoEmpresa = new PresupuestosEmpresa();
					objPresupuestoEmpresa.objTransaction = this.objTransaction;
					objPresupuestoEmpresa.IdPresupuestoEmpresa = this.IdPresupuestoEmpresa;
					objPresupuestoEmpresa.UpdateValorPresupuestosEmpresa();
				}
				if(this.IdPresupuestoIndividuo != 0)
				{
					PresupuestosIndividuo objPresupuestoIndividuo = new PresupuestosIndividuo();
					objPresupuestoIndividuo.objTransaction = this.objTransaction;
					objPresupuestoIndividuo.IdPresupuestoIndividuo = this.IdPresupuestoIndividuo;
					objPresupuestoIndividuo.UpdatePresupuestosIndividuoValor();
				}	
				
				this.CommitTransaction();
			}
			catch(Exception ex)
			{
				this.RollbackTransaction();
				throw ex;
			}
			return idSolicitud;
		}

		/// <summary>
		/// Método para la modificación
		/// </summary>
		public void UpdateSolicitud()
		{
			long idTipoServicio;
			try
			{
				this.BeginTransaction();

				this.Update();

				if(this.SolicitudTipoServicios != null)
				{
					this.Delete("SolicitudesServicioSolicitud",this.IdSolicitud);
					this.Delete("SolicitudTipoServicioDiagnosticos",this.IdSolicitud);
					this.Delete("SolicitudTipoServicioProveedores",this.IdSolicitud);
					this.Delete("SolicitudesTipoServicioSolicitud",this.IdSolicitud);

					foreach(SolicitudTipoServicio objSolicitudTipoServicio in this.SolicitudTipoServicios)
					{
						objSolicitudTipoServicio.objTransaction = this.objTransaction;
						objSolicitudTipoServicio.IdSolicitud = this.IdSolicitud;
						objSolicitudTipoServicio.Empresa_id = this.Empresa_id;
						idTipoServicio = objSolicitudTipoServicio.InsertSolicitudTipoServicio();
                       					
						if(objSolicitudTipoServicio.SolicitudServicios != null)
						{
							foreach(SolicitudServicio objSolicitudServicio in objSolicitudTipoServicio.SolicitudServicios)
							{
								objSolicitudServicio.objTransaction = this.objTransaction;
								objSolicitudServicio.IdSolicitud = this.IdSolicitud;
								objSolicitudServicio.IdSolicitudTipoServicio = idTipoServicio;
								objSolicitudServicio.InsertSolicitudServicio();
							}
						}

						objSolicitudTipoServicio.IdSolicitudTipoServicio = idTipoServicio;
						objSolicitudTipoServicio.UpdateEstadoSolicitudTipoServicio();
					}
				}

				this.Delete("SolicitudAnotacionesFijas",this.IdSolicitud);

				if(this.AnotacionesFijas != null)
				{
					foreach(short idAnotacionFija in this.AnotacionesFijas)
					{
						this.Insert("SolicitudAnotacionesFijas",this.IdSolicitud,idAnotacionFija);
					}
				}
				if(this.objIncapacidad != null)
				{
					this.objIncapacidad.objTransaction = this.objTransaction;

					if(this.objIncapacidad.IdIncapacidad != 0)
                        this.objIncapacidad.UpdateIncapacidad();
					else
						this.objIncapacidad.InsertIncapacidad();
				}

				this.IdSolicitudEstado = Convert.ToInt16(this.UpdateEstadoSolicitud());
				
				if((this.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado) ||  this.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado)) && this.IdPresupuestoEmpresa != 0)
				{
					PresupuestosEmpresa objPresupuestoEmpresa = new PresupuestosEmpresa();
					objPresupuestoEmpresa.objTransaction = this.objTransaction;
					objPresupuestoEmpresa.IdPresupuestoEmpresa = this.IdPresupuestoEmpresa;
					objPresupuestoEmpresa.UpdateValorPresupuestosEmpresa();
				}
				if((this.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Aprobado) ||  this.IdSolicitudEstado == Convert.ToInt16(Solicitud.EnumEstadoSolicitud.Liquidado)) && this.IdPresupuestoIndividuo != 0)
				{
					PresupuestosIndividuo objPresupuestoIndividuo = new PresupuestosIndividuo();
					objPresupuestoIndividuo.objTransaction = this.objTransaction;
					objPresupuestoIndividuo.IdPresupuestoIndividuo = this.IdPresupuestoIndividuo;
					objPresupuestoIndividuo.UpdatePresupuestosIndividuoValor();
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
		/// Método para modificar el estado dependiendo del estado de los servicios
		/// </summary>
		public short UpdateEstadoSolicitud()
		{
			try
			{
				return Convert.ToInt16(this.Update("EstadoSolicitud",this.IdSolicitud));
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		

		/// <summary>
		/// Método para la eliminación
		/// </summary>
		public void DeleteSolicitud()
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
		public void GetSolicitud()
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
		/// Método para la carga de los datos de valores utilizados y disponibles del empleado
		/// </summary>
		public void GetValoresEmpleado()
		{
			try
			{
				this.Consult("ValoresEmpleado",this.Id_empleado);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		//Método, modifica el estado total de una orden
		public void UpdateSolicitudEstado()
		{
			try
			{				

				this.BeginTransaction();
									
				foreach(SolicitudTipoServicio objSolicitudTipoServicio in this.SolicitudTipoServicios)
				{
					objSolicitudTipoServicio.objTransaction = this.objTransaction;					
					objSolicitudTipoServicio.UpdateSolicitudTipoServicioSolicitudEstado();

					foreach(SolicitudServicio objSolicitudServicio in objSolicitudTipoServicio.SolicitudServicios)
					{
						objSolicitudServicio.objTransaction = this.objTransaction;
						objSolicitudServicio.UpdateSolicitudServicioSolicitudEstado();
					}
				}				
					
				if(this.IdPresupuestoEmpresa != 0)
				{
					PresupuestosEmpresa objPresupuestoEmpresa = new PresupuestosEmpresa();
					objPresupuestoEmpresa.objTransaction = this.objTransaction;
					objPresupuestoEmpresa.IdPresupuestoEmpresa = this.IdPresupuestoEmpresa;
					objPresupuestoEmpresa.UpdateValorPresupuestosEmpresa();
				}
				if(this.IdPresupuestoIndividuo != 0)
				{
					PresupuestosIndividuo objPresupuestoIndividuo = new PresupuestosIndividuo();
					objPresupuestoIndividuo.objTransaction = this.objTransaction;
					objPresupuestoIndividuo.IdPresupuestoIndividuo = this.IdPresupuestoIndividuo;
					objPresupuestoIndividuo.UpdatePresupuestosIndividuoValor();
				}
				foreach(SolicitudTipoServicio objSolicitudTipoServicio in this.SolicitudTipoServicios)
				{
					objSolicitudTipoServicio.objTransaction = this.objTransaction;
					objSolicitudTipoServicio.UpdateEstadoSolicitudTipoServicio();
				}
				
				this.UpdateEstadoSolicitud();

				this.CommitTransaction();
			}
			catch(Exception ex)
			{
				this.RollbackTransaction();
				throw ex;
			}		
		}

		
		
		#endregion

		#region Miembros de ICloneable

		public object Clone()
		{
			Solicitud objSolicitud = new Solicitud();
			objSolicitud.IdSolicitud = this.IdSolicitud;
			objSolicitud.AnoLiquidacion = this.AnoLiquidacion;
			objSolicitud.AnotacionesFijas = this.AnotacionesFijas;
			objSolicitud.Beneficiario_id = this.Beneficiario_id;
			objSolicitud.CantidadSolicitudesEmpleado = this.CantidadSolicitudesEmpleado;
			objSolicitud.Consecutivo = this.Consecutivo;
			objSolicitud.ConsecutivoNombre = this.ConsecutivoNombre;
			objSolicitud.Documentos = this.Documentos;
			objSolicitud.Empresa_id = this.Empresa_id;
			objSolicitud.Fecha = this.Fecha;
			objSolicitud.FechaCreacion = this.FechaCreacion;
			objSolicitud.FechaLiquidacion = this.FechaLiquidacion;
			objSolicitud.FechaModificacion = this.FechaModificacion;
			objSolicitud.Glosa = this.Glosa;
			objSolicitud.Id_empleado = this.Id_empleado;
			objSolicitud.Id_solicitud_SICAU = this.Id_solicitud_SICAU;
			objSolicitud.IdDiagnostico = this.IdDiagnostico;
			objSolicitud.IdFormaPago = this.IdFormaPago;
			objSolicitud.IdPrestador = this.IdPrestador;
			objSolicitud.IdPresupuestoEmpresa = this.IdPresupuestoEmpresa;
			objSolicitud.IdPresupuestoIndividuo = this.IdPresupuestoIndividuo;
			objSolicitud.IdSolicitud = this.IdSolicitud;
			objSolicitud.IdSolicitudEstado = this.IdSolicitudEstado;
			objSolicitud.IdTipoSolicitud = this.IdTipoSolicitud;
			objSolicitud.IdUserCreacion = this.IdUserCreacion;
			objSolicitud.IdUserLiquidacion = this.IdUserLiquidacion;
			objSolicitud.MesLiquidacion = this.MesLiquidacion;
			objSolicitud.NameUser = this.NameUser;			
			objSolicitud.Observaciones = this.Observaciones;
			objSolicitud.SolicitudTipoServicios = this.SolicitudTipoServicios;		
			objSolicitud.Usuario_idCreacion = this.Usuario_idCreacion;
			objSolicitud.Usuario_idLiquidacion = this.Usuario_idLiquidacion;
			objSolicitud.ValorTotalAprobado = this.ValorTotalAprobado;
			objSolicitud.ValorTotalConvenioSolicitado = this.ValorTotalConvenioSolicitado;
			objSolicitud.ValorTotalFactura = this.ValorTotalFactura;
			objSolicitud.ValorUtilizadoEmpleado = this.ValorUtilizadoEmpleado;


			return objSolicitud;
		}

		#endregion
	}
}


