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
using System.Collections;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para registrar y consultar los parentescos de la empresa
    /// </summary>
    /// <remarks>Autor: Ricardo Silva</remarks>
    /// <remarks>Fecha de creación: 24/10/2011 </remarks>
    public class Parentescos : GeneralProcess
    {
        private int _IdParentesco;
        /// <summary>Atributo, Id del parentesco</summary>
        private string _NombreParentesco;
        /// <summary>Atributo, Nombre del parentesco</summary>

        public int IdParentesco
        {
            get { return _IdParentesco; }
            set { _IdParentesco = value; }
        }

        /// <summary>Propiedad, Id de la consulta</summary>
        public string NombreParentesco
        {
            get { return _NombreParentesco; }
            set { _NombreParentesco = value; }
        }

        public Parentescos()
		{
			
		}		

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultParentescos()
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
        public DataSet ConsultEmpresaParentescosAsignados(int idEmpresa)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("EmpresaParentescosAsignados", idEmpresa);
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
        public int InsertParentescos()
        {
            int id;
            try
            {
                id = Convert.ToInt32(this.Insert());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

    }
}
