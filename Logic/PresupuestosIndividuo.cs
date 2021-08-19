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
	public class PresupuestosIndividuo : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del presupuesto</summary>
		private long _IdPresupuestoIndividuo;
		/// <summary>Atributo, Id de la empresa en SICAU al que pertenece el presupuesto</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Id del empleado al que pertenece el prespuesto</summary>
		private int _Id_empleado;
		/// <summary>Atributo, Id del beneficiario al que pertenece el presupuesto</summary>
		private int _Beneficiario_id;
		/// <summary>Atributo, Tipo de proceso (reembolso y/o autorización) al que pertenece el presupuesto</summary>
		private short _IdTipoProceso;
		/// <summary>Atributo, Id del tipo de presupuesto</summary>
		private short _IdTipoPresupuesto;
		/// <summary>Atributo, Fecha Inicial del presupuesto</summary>
		private DateTime _FechaInicio;
		/// <summary>Atributo, Fecha Final del presupuesto, nulo en caso de no tener límite</summary>
		private DateTime _FechaFin;
		/// <summary>Atributo, Valor del Presupuesto</summary>
		private decimal _Presupuesto;
		/// <summary>Atributo, Valor utilizado del prespuesto</summary>
		private decimal _Utilizado;
		/// <summary>Atributo, Valor de exceso en caso de superar el tope</summary>
		private decimal _ExcesoPresupuesto;
		/// <summary>Atributo, Id del plan al que pertenece el presupuesto</summary>
		private int _IdPlanSolicitud;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del presupuesto</summary>
		public long IdPresupuestoIndividuo
		{
			get	{ return _IdPresupuestoIndividuo; }
			set	{ _IdPresupuestoIndividuo = value; }
		}
		/// <summary>Propiedad, Id de la empresa en SICAU al que pertenece el presupuesto</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
		/// <summary>Propiedad, Id del empleado al que pertenece el prespuesto</summary>
		public int Id_empleado
		{
			get	{ return _Id_empleado; }
			set	{ _Id_empleado = value; }
		}
		/// <summary>Propiedad, Id del beneficiario al que pertenece el presupuesto</summary>
		public int Beneficiario_id
		{
			get	{ return _Beneficiario_id; }
			set	{ _Beneficiario_id = value; }
		}
		/// <summary>Propiedad, Tipo de proceso (reembolso y/o autorización) al que pertenece el presupuesto</summary>
		public short IdTipoProceso
		{
			get	{ return _IdTipoProceso; }
			set	{ _IdTipoProceso = value; }
		}
		/// <summary>Propiedad, Id del tipo de presupuesto</summary>
		public short IdTipoPresupuesto
		{
			get	{ return _IdTipoPresupuesto; }
			set	{ _IdTipoPresupuesto = value; }
		}
		/// <summary>Propiedad, Fecha Inicial del presupuesto</summary>
		public DateTime FechaInicio
		{
			get	{ return _FechaInicio; }
			set	{ _FechaInicio = value; }
		}
		/// <summary>Propiedad, Fecha Final del presupuesto, nulo en caso de no tener límite</summary>
		public DateTime FechaFin
		{
			get	{ return _FechaFin; }
			set	{ _FechaFin = value; }
		}
		/// <summary>Propiedad, Valor del Presupuesto</summary>
		public decimal Presupuesto
		{
			get	{ return _Presupuesto; }
			set	{ _Presupuesto = value; }
		}
		/// <summary>Propiedad, Valor utilizado del prespuesto</summary>
		public decimal Utilizado
		{
			get	{ return _Utilizado; }
			set	{ _Utilizado = value; }
		}
		/// <summary>Propiedad, Valor de exceso en caso de superar el tope</summary>
		public decimal ExcesoPresupuesto
		{
			get	{ return _ExcesoPresupuesto; }
			set	{ _ExcesoPresupuesto = value; }
		}
		/// <summary>Propiedad, Id del plan al que pertenece el presupuesto</summary>
		public int IdPlanSolicitud
		{
			get	{ return _IdPlanSolicitud; }
			set	{ _IdPlanSolicitud = value; }
		}
		
		
		#endregion	

		#region Enumeration

		/// <summary>
		/// Enumeración, lista los tipos de solicitudes que permite el sistema
		/// </summary>
		public enum EnumTipoProceso
		{
			Reembolso = 1,
			Autorizacion = 2,
			ReembolsoyAutorizacion = 3,
			ReembolsosyAutorizacionSeparada = 4,
			Ordenes = 5
		}

		#endregion
		
		#region Methods
		
		public PresupuestosIndividuo()
		{
		}
	
		public PresupuestosIndividuo(long IdPresupuestoIndividuo, int Empresa_id, int Id_empleado, int Beneficiario_id, short IdTipoProceso, short IdTipoPresupuesto, DateTime FechaInicio, DateTime FechaFin, decimal Presupuesto, decimal Utilizado, decimal ExcesoPresupuesto)
		{
			_IdPresupuestoIndividuo = IdPresupuestoIndividuo;
			_Empresa_id = Empresa_id;
			_Id_empleado = Id_empleado;
			_Beneficiario_id = Beneficiario_id;
			_IdTipoProceso = IdTipoProceso;
			_IdTipoPresupuesto = IdTipoPresupuesto;
			_FechaInicio = FechaInicio;
			_FechaFin = FechaFin;
			_Presupuesto = Presupuesto;
			_Utilizado = Utilizado;
			_ExcesoPresupuesto = ExcesoPresupuesto;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultPresupuestosIndividuo()
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
		public int InsertPresupuestosIndividuo()
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
		/// Método para la inserción
		/// </summary>
		/// <returns>Id insertado</returns>
		public int InsertPresupuestosIndividuoAutomatico(long p_idPresupuestoEmpresa)
		{
			int id;
			try
			{
				id = Convert.ToInt32(this.Insert("PresupuestosIndividuoAutomatico",p_idPresupuestoEmpresa,this.Id_empleado,this.Beneficiario_id));
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
		public void UpdatePresupuestosIndividuo()
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
		/// Método para la modificación del valor del presupuesto del individuo
		/// </summary>
		public void UpdatePresupuestosIndividuoValor()
		{
			try
			{
				this.Update("ValorPresupuestosIndividuo",this.IdPresupuestoIndividuo);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método para la eliminación
		/// </summary>
		public void DeletePresupuestosIndividuo()
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
		public void GetPresupuestosIndividuo()
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
		public void GetPresupuestosIndividuoActual(DateTime p_fechaActual)
		{
			try
			{
				this.Consult("PresupuestosIndividuoActual", this.Id_empleado, this.Beneficiario_id, this.IdTipoProceso, p_fechaActual, this.IdPlanSolicitud);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
		#endregion		
			
	}
}


