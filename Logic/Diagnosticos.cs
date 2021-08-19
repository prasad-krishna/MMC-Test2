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
	/// <remarks>Fecha de creaci�n: </remarks>
	public class Diagnosticos : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del diagn�stico m�dico</summary>
		private int _IdDiagnostico;
		/// <summary>Atributo, C�digo del diagn�stico m�dico</summary>
		private string _CodigoDiagnostico;
		/// <summary>Atributo, Nombre del diagn�stico m�dico</summary>
		private string _NombreDiagnostico;
        /// <summary>Atributo, Id de la clasificaci�n del diagn�stico</summary>
        private int _IdDiagnosticoClasificacion;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del diagn�stico m�dico</summary>
		public int IdDiagnostico
		{
			get	{ return _IdDiagnostico; }
			set	{ _IdDiagnostico = value; }
		}
		/// <summary>Propiedad, C�digo del diagn�stico m�dico</summary>
		public string CodigoDiagnostico
		{
			get	{ return _CodigoDiagnostico; }
			set	{ _CodigoDiagnostico = value; }
		}
		/// <summary>Propiedad, Nombre del diagn�stico m�dico</summary>
		public string NombreDiagnostico
		{
			get	{ return _NombreDiagnostico; }
			set	{ _NombreDiagnostico = value; }
		}
        /// <summary>Propiedad, Id de la clasificaci�n del diagn�stico</summary>
        public int IdDiagnosticoClasificacion
        {
            get { return _IdDiagnosticoClasificacion; }
            set { _IdDiagnosticoClasificacion = value; }
        }
		
		#endregion	
		
		#region Methods
		
		public Diagnosticos()
		{
		}
		
		/// <summary>
		/// M�todo para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultDiagnosticos()
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
		/// M�todo para la inserci�n
		/// </summary>
		/// <returns>Id insertado</returns>
		public int InsertDiagnosticos()
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
		/// M�todo para la modificaci�n
		/// </summary>
		public void UpdateDiagnosticos()
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
        /// M�todo para la modificaci�n
        /// </summary>
        public void UpdateDiagnosticoClasificacion()
        {
            try
            {
                this.Update("DiagnosticoClasificacion");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// M�todo para la eliminaci�n
		/// </summary>
		public void DeleteDiagnosticos()
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
		/// M�todo para la carga de un objeto de este tipo
		/// </summary>
		public void GetDiagnosticos()
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


