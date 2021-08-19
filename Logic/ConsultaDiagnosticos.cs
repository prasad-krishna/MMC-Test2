/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2008 by Mercer
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
	public class ConsultaDiagnosticos : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id de la consulta</summary>
		private long _IdConsulta;
		/// <summary>Atributo, Id del diagnóstico médico</summary>
		private int _IdDiagnostico;
		/// <summary>Atributo, Tiempo de evolución del dignóstico</summary>
		private decimal _TiempoEvolucion;
		/// <summary>Atributo, Perido de evolución del diagnóstico como días, meses, años</summary>
		private string _PeriodoEvolucion;
		/// <summary>Atributo, Id del tipo de diagnóstico</summary>
		private short _IdTipoDiagnostico;
        /// <summary>Atributo, Id de la clasificación del diagnóstico</summary>
        private int _IdDiagnosticoClasificacion;
        /// <summary>Atributo, Numero que ordena los diagnosticos en orden</summary>
        private int _OrdenDiagnosticos;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id de la consulta</summary>
		public long IdConsulta
		{
			get	{ return _IdConsulta; }
			set	{ _IdConsulta = value; }
		}
		/// <summary>Propiedad, Id del diagnóstico médico</summary>
		public int IdDiagnostico
		{
			get	{ return _IdDiagnostico; }
			set	{ _IdDiagnostico = value; }
		}
		/// <summary>Propiedad, Id del tipo diagnóstico</summary>
		public short IdTipoDiagnostico
		{
			get	{ return _IdTipoDiagnostico; }
			set	{ _IdTipoDiagnostico = value; }
		}
		/// <summary>Propiedad, Tiempo de evolución del dignóstico</summary>
		public decimal TiempoEvolucion
		{
			get	{ return _TiempoEvolucion; }
			set	{ _TiempoEvolucion = value; }
		}
		/// <summary>Propiedad, Perido de evolución del diagnóstico como días, meses, años</summary>
		public string PeriodoEvolucion
		{
			get	{ return _PeriodoEvolucion; }
			set	{ _PeriodoEvolucion = value; }
		}
        /// <summary>Propiedad, Id de la clasificación del diagnóstico</summary>
        public int IdDiagnosticoClasificacion
        {
            get { return _IdDiagnosticoClasificacion; }
            set { _IdDiagnosticoClasificacion = value; }
        }
        /// <summary>Propiedad, Numero que ordena los diagnosticos en orden</summary>
        public int OrdenDiagnosticos
        {
            get { return _OrdenDiagnosticos; }
            set { _OrdenDiagnosticos = value; }
        }		
		
		#endregion	
		
		#region Methods
		
		public ConsultaDiagnosticos()
		{
		}		

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultConsultaDiagnosticos()
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
		public int InsertConsultaDiagnosticos()
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
		public void UpdateConsultaDiagnosticos()
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
		public void DeleteConsultaDiagnosticos()
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
		public void GetConsultaDiagnosticos()
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


