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
	public class PresupuestosEmpresa : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del prespuesto de toda la compañía</summary>
		private long _IdPresupuestoEmpresa;
		/// <summary>Atributo, id de la empresa en el sistema SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Fecha de inicio del presupuesto</summary>
		private DateTime _FechaInicio;
		/// <summary>Atributo, Fecha de fin del prespuesto, nulo cuando no tiene un límite</summary>
		private DateTime _FechaFin;
		/// <summary>Atributo, Valor del prespuesto, nulo cuando no hay límite</summary>
		private decimal _Presupuesto;
		/// <summary>Atributo, Id del proceso al que pertenece el prespuesto</summary>
		private short _IdTipoProceso;
		/// <summary>Atributo, Valor acumulado de lo que se ha utilizado</summary>
		private decimal _Utilizado;
		/// <summary>Atributo, Valor que indica el valor por el que se ha excedido el prespuesto</summary>
		private decimal _ExcesoPresupuesto;
		/// <summary>Atributo, Indica si se puede ingresar solicitudes sin importar si se supera el presupuesto</summary>
		private bool _IngresoConExceso;
		/// <summary>Atributo, Valor del prespuesto si todos los empleados tiene el mismo valor, permite creación automática del presupuesto</summary>
		private decimal _PresupuestoTodos;
		/// <summary>Atributo, Id del tipo de presupuesto si todos los empleados tienen el mismo valor, permite creacion automática del presupuesto</summary>
		private short _IdTipoPresupuestoTodos;
		/// <summary>Atributo, Id del plan al que pertenece el presupuesto</summary>
		private int _IdPlanSolicitud;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del prespuesto de toda la compañía</summary>
		public long IdPresupuestoEmpresa
		{
			get	{ return _IdPresupuestoEmpresa; }
			set	{ _IdPresupuestoEmpresa = value; }
		}
		/// <summary>Propiedad, id de la empresa en el sistema SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Fecha de inicio del presupuesto</summary>
		public DateTime FechaInicio
		{
			get	{ return _FechaInicio; }
			set	{ _FechaInicio = value; }
		}
		/// <summary>Propiedad, Fecha de fin del prespuesto, nulo cuando no tiene un límite</summary>
		public DateTime FechaFin
		{
			get	{ return _FechaFin; }
			set	{ _FechaFin = value; }
		}
		/// <summary>Propiedad, Valor del prespuesto, nulo cuando no hay límite</summary>
		public decimal Presupuesto
		{
			get	{ return _Presupuesto; }
			set	{ _Presupuesto = value; }
		}
		/// <summary>Propiedad, Id del proceso al que pertenece el prespuesto</summary>
		public short IdTipoProceso
		{
			get	{ return _IdTipoProceso; }
			set	{ _IdTipoProceso = value; }
		}
		/// <summary>Propiedad, Valor acumulado de lo que se ha utilizado</summary>
		public decimal Utilizado
		{
			get	{ return _Utilizado; }
			set	{ _Utilizado = value; }
		}
		/// <summary>Propiedad, Valor que indica el valor por el que se ha excedido el prespuesto</summary>
		public decimal ExcesoPresupuesto
		{
			get	{ return _ExcesoPresupuesto; }
			set	{ _ExcesoPresupuesto = value; }
		}
		/// <summary>Propiedad, Indica si se puede ingresar solicitudes sin importar si se supera el presupuesto</summary>
		public bool IngresoConExceso
		{
			get	{ return _IngresoConExceso; }
			set	{ _IngresoConExceso = value; }
		}
		/// <summary>Propiedad, Valor del prespuesto si todos los empleados tiene el mismo valor, permite creación automática del presupuesto</summary>
		public decimal PresupuestoTodos
		{
			get	{ return _PresupuestoTodos; }
			set	{ _PresupuestoTodos = value; }
		}
		/// <summary>Propiedad, Id del tipo de presupuesto si todos los empleados tienen el mismo valor, permite creacion automática del presupuesto</summary>
		public short IdTipoPresupuestoTodos
		{
			get	{ return _IdTipoPresupuestoTodos; }
			set	{ _IdTipoPresupuestoTodos = value; }
		}
		/// <summary>Propiedad, Id del plan al que pertenece el presupuesto</summary>
		public int IdPlanSolicitud
		{
			get	{ return _IdPlanSolicitud; }
			set	{ _IdPlanSolicitud = value; }
		}
		
		
		#endregion	
		
		#region Methods
		
		public PresupuestosEmpresa()
		{
		}
	
		public PresupuestosEmpresa(long IdPresupuestoEmpresa, int Empresa_id, DateTime FechaInicio, DateTime FechaFin, decimal Presupuesto, short IdTipoProceso, decimal Utilizado, decimal ExcesoPresupuesto, bool IngresoConExceso)
		{
			_IdPresupuestoEmpresa = IdPresupuestoEmpresa;
			_Empresa_id = Empresa_id;
			_FechaInicio = FechaInicio;
			_FechaFin = FechaFin;
			_Presupuesto = Presupuesto;
			_IdTipoProceso = IdTipoProceso;
			_Utilizado = Utilizado;
			_ExcesoPresupuesto = ExcesoPresupuesto;
			_IngresoConExceso = IngresoConExceso;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultPresupuestosEmpresa()
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
		public int InsertPresupuestosEmpresa()
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
		public void UpdatePresupuestosEmpresa()
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
		/// Método para la modificación del valor del presupuesto
		/// </summary>
		public void UpdateValorPresupuestosEmpresa()
		{
			try
			{
				this.Update("ValorPresupuestosEmpresa",this.IdPresupuestoEmpresa);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método para la eliminación
		/// </summary>
		public void DeletePresupuestosEmpresa()
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
		public void GetPresupuestosEmpresa()
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
		public void GetPresupuestosActual(DateTime p_fechaActual)
		{
			try
			{
				this.Consult("PresupuestosEmpresaActual", this.Empresa_id, this.IdTipoProceso, p_fechaActual, this.IdPlanSolicitud);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
		#endregion		
			
	}
}


