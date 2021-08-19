/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2012 by Mercer
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
	/// Esta clase provee la funcionalidad para las Líneas de Negocio
	/// </summary>
	/// <remarks>Autor: Emilio Bueno</remarks>
	/// <remarks>Fecha de creación: 26/09/2012</remarks>
    public class LineasNegocio : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, Id de la línea de negocio</summary>
        private int _IdLineaNegocio;
        /// <summary>Atributo, Nombre de la línea de negocio</summary>
        private string _NombreLineaNegocio;
        /// <summary>Atributo, Id de la empresa</summary>
        private int _empresa_id;
        /// <summary>Atributo, Estado de la línea de negocio</summary>
        private bool _activa;

        #endregion

        #region Properties

        /// <summary>Propiedad, Id de la línea de negocio</summary>
        public int IdLineaNegocio
        {
            get { return _IdLineaNegocio; }
            set { _IdLineaNegocio = value; }
        }
        /// <summary>Propiedad, Nombre de la línea de negocio</summary>
        public string NombreLineaNegocio
        {
            get { return _NombreLineaNegocio; }
            set { _NombreLineaNegocio = value; }
        }
        /// <summary>Propiedad, Id de la empresa</summary>
        public int empresa_id
        {
            get { return _empresa_id; }
            set { _empresa_id = value; }
        }
        /// <summary>Propiedad, Estado de la línea de negocio</summary>
        public bool activa
        {
            get { return _activa; }
            set { _activa = value; }
        }

        #endregion	

		#region Methods
		
		public LineasNegocio()
		{
		}
		
		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultLineasNegocio()
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
        public int InsertLineasNegocio()
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
        public void UpdateLineasNegocio()
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
		/// Método para la carga de un objeto de este tipo
		/// </summary>
        public void GetLineasNegocio()
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