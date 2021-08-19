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
    /// Esta clase provee la funcionalidad de asignar permisos a los reportes
    /// </summary>
    /// <remarks>Autor: Ricardo Silva</remarks>
    /// <remarks>Fecha de creación: </remarks>
    public class Reportes : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, Id del reporte</summary>
        private int _IdReporte;
        /// <summary>Atributo, Nombre del Reporte</summary>
        private string _NombreReporte;

        #endregion


        #region Properties

        /// <summary>Propiedad, id del reporte</summary>
        public int IdReporte
        {
            get { return _IdReporte; }
            set { _IdReporte = value; }
        }
        /// <summary>Propiedad, Nombre del reporte</summary>
        public string NombreReporte
        {
            get { return _NombreReporte; }
            set { _NombreReporte = value; }
        }

        #endregion

        #region Methods

        public Reportes()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }


        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultReportesUserAsignados(int idUser)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("UserReportesAsignados", idUser);
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
        /// <returns>DataSet con los resultados de los permisos para los reportes</returns>
        public DataSet ConsultReportes()
        {
            DataSet dsList;
            try
            {
                dsList = this.List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        #endregion
    }
}
