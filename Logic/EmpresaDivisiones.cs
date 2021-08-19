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
	public class EmpresaDivisiones : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id de la empresa en SICAU</summary>
		private int _Empresa_id;
		/// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n h�bitos</summary>
        private bool _DivHabitos;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Clolesterol y Glicemia</summary>
        private bool _DivColesterolGlicemia;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n ex�menes laboratorio</summary>
        private bool _DivExamenesLaboratorio;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Mujer</summary>
        private bool _DivMujer;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Audiometr�a</summary>
        private bool _DivAudiometria;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Wellness</summary>
        private bool _DivWellness;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Habito de Fumar</summary>
        private bool _DivHabitoFumar;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Consumo de Alcohol</summary>
        private bool _DivConsumoAlcohol;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Vacunacion</summary>
        private bool _DivVacunacion;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Sedentarismo</summary>
        private bool _DivSedentarismo;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Salud Oral</summary>
        private bool _DivSaludOral;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Estres</summary>
        private bool _DivEstres;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Emocional</summary>
        private bool _DivEmocional;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Accidentalidad</summary>
        private bool _DivAccidentalidad;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Estado de Salud</summary>
        private bool _DivEstadoSalud;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Nutrici�n</summary>
        private bool _DivNutricion;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Antecedentes de Ausentismo</summary>
        private bool _DivAntecedentesAusentismo;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n Recomendaciones</summary>
        private bool _DivRecomendaciones;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n tensi�n Diast�lica y SisT�lica</summary>
        private bool _DivDiastolicaSisTolica;
        /// <summary>Atributo, Indica si la empresa tiene permiso para ver la divisi�n per�metro abdominal</summary>
        private bool _DivPerimetroAbdominal;
        
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id de la empresa en SICAU</summary>
		public int Empresa_id
		{
			get	{ return _Empresa_id; }
			set	{ _Empresa_id = value; }
		}
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n habito</summary>
        public bool DivHabitos
        {
            get { return _DivHabitos; }
            set { _DivHabitos = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Colesterol y Glicemia</summary>
        public bool DivColesterolGlicemia
        {
            get { return _DivColesterolGlicemia; }
            set { _DivColesterolGlicemia = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Ex�menes de Laboratorio</summary>
        public bool DivExamenesLaboratorio
        {
            get { return _DivExamenesLaboratorio; }
            set { _DivExamenesLaboratorio = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Mujer</summary>
        public bool DivMujer
        {
            get { return _DivMujer; }
            set { _DivMujer = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Audiometr�a</summary>
        public bool DivAudiometria
        {
            get { return _DivAudiometria; }
            set { _DivAudiometria = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Wellness</summary>
        public bool DivWellness
        {
            get { return _DivWellness; }
            set { _DivWellness = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Habito de Fumar</summary>
        public bool DivHabitoFumar
        {
            get { return _DivHabitoFumar; }
            set { _DivHabitoFumar = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Consumo de Alcohol</summary>
        public bool DivConsumoAlcohol
        {
            get { return _DivConsumoAlcohol; }
            set { _DivConsumoAlcohol = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Vacunacion</summary>
        public bool DivVacunacion
        {
            get { return _DivVacunacion; }
            set { _DivVacunacion = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Vacunacion</summary>
        public bool DivSedentarismo
        {
            get { return _DivSedentarismo; }
            set { _DivSedentarismo = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Salud Oral</summary>
        public bool DivSaludOral
        {
            get { return _DivSaludOral; }
            set { _DivSaludOral = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Estres</summary>
        public bool DivEstres
        {
            get { return _DivEstres; }
            set { _DivEstres = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Emocional</summary>
        public bool DivEmocional
        {
            get { return _DivEmocional; }
            set { _DivEmocional = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Accidentalidad</summary>
        public bool DivAccidentalidad
        {
            get { return _DivAccidentalidad; }
            set { _DivAccidentalidad = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Estado de Salud</summary>
        public bool DivEstadoSalud
        {
            get { return _DivEstadoSalud; }
            set { _DivEstadoSalud = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Nutricion</summary>
        public bool DivNutricion
        {
            get { return _DivNutricion; }
            set { _DivNutricion = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Antecedentes de Ausentismo</summary>
        public bool DivAntecedentesAusentismo
        {
            get { return _DivAntecedentesAusentismo; }
            set { _DivAntecedentesAusentismo = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n Recomendaciones</summary>
        public bool DivRecomendaciones
        {
            get { return _DivRecomendaciones; }
            set { _DivRecomendaciones = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n tensi�n Diast�lica y SisT�lica</summary>
        public bool DivDiastolicaSisTolica
        {
            get { return _DivDiastolicaSisTolica; }
            set { _DivDiastolicaSisTolica = value; }
        }
        /// <summary>Propiedad,  Indica si la empresa tiene permiso para ver la divisi�n per�metro abdominal</summary>
        public bool DivPerimetroAbdominal
        {
            get { return _DivPerimetroAbdominal; }
            set { _DivPerimetroAbdominal = value; }
        }
        
	
		#endregion	
		
		#region Methods
		
		public EmpresaDivisiones()
		{
		}

        public EmpresaDivisiones(int Empresa_id)
		{
			_Empresa_id = Empresa_id;
			
		}

		/// <summary>
		/// M�todo para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultEmpresaDivisiones()
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
        public int InsertEmpresaDivisiones()
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
        public void UpdateEmpresaDivisiones()
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
		/// M�todo para la eliminaci�n
		/// </summary>
        public void DeleteEmpresaDivisiones()
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
        public void GetEmpresaDivisiones()
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
        /// M�todo para la consulta la existencia de divisiones de la segunda p�gina
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public bool ConsultExisteEmpresaDivisiones()
        {
            DataSet dsList;
            bool bolExisteEmpresaDivisiones = false;
            try
            {
                dsList = this.List("ExisteEmpresaDivisiones");
                if (dsList.Tables[0].Rows.Count>0)
                {
                    bolExisteEmpresaDivisiones = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolExisteEmpresaDivisiones;
        }



        /// <summary>
        /// M�todo para la consulta la existencia de divisi�n nutrici�n
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public bool ConsultExisteEmpresaDivisionesNutricion()
        {
            DataSet dsList;
            bool bolExisteEmpresaDivisiones = false;
            try
            {
                dsList = this.List("ExisteEmpresaDivisionesNutricion");
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    bolExisteEmpresaDivisiones = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolExisteEmpresaDivisiones;
        }
		
		#endregion
		
			
	}
}


