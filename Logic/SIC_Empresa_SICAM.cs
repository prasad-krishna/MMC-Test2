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
    /// Autor: Marco A. Herrera Gabriel
    /// Proyecto: TPA-SICAM
    /// Fecha: 05/02/2010
    /// Funcionalidad: Liga las empresas existentes en HC con SICAM México
    /// </summary>
    public class SIC_EMPRESA_SICAM : GeneralProcess
    {
        #region Attributes

        /// <summary>
        /// Atributo: Clave de la empresa en HC
        /// </summary>
        private int intEmpresa_Id;
        /// <summary>
        /// Atributo: Clave de la empresa en SICAM
        /// </summary>
        private int intEmpClave;

        /// <summary>
        /// Atributo: Clave de la póliza que se va asociar
        /// </summary>
        private string strPolClave;
        #endregion

        #region Properties

        /// <summary>
        /// Propiedad: Clave de la empresa en HC
        /// </summary>
        public int Empresa_Id
        {
            get { return this.intEmpresa_Id; }
            set { this.intEmpresa_Id = value; }
        }

        /// <summary>
        /// Propiedad: Clave de la empresa en SICAM
        /// </summary>
        public int EmpClave
        {
            get { return this.intEmpClave; }
            set { this.intEmpClave = value; }
        }

        /// <summary>
        /// Propiedad: Clave de la póliza en SICAM
        /// </summary>
        public string PolClave
        {
            get {return this.strPolClave ;}
            set { this.strPolClave = value;}
        }
        #endregion

        #region Methods

        public SIC_EMPRESA_SICAM()
        {

        }

        /// <summary>
        /// Método que asocia las empresas
        /// </summary>
        public void InsertSIC_EMPRESA_SICAM()
        {
            try
            {
                this.Insert();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene las empresas registradas
        /// </summary>
        /// <returns></returns>
        public int GetSIC_EMPRESA_SICAM()
        {            
            try
            {
                return this.Consult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta las empresas existentes en SICAM
        /// </summary>
        /// <returns></returns>
        public DataSet Get_EMPRESAS()
        {
            try
            {
                return this.consultarProc("ListSIC_EMPRESA_SICAM");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta las la fecha en la que se activo la poliza para HC
        /// </summary>
        /// <returns>La fecha de la poliza</returns>
        public DataSet Get_FechaPoliza(int EmpClave)
        {
            try
            {
                return this.consultarProc("Listsicam_ModificacionesWellness", EmpClave);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }


        /// <summary>
        /// Consulta las pólizas activas que existen en SICAM en la empresa seleccionada
        /// </summary>
        /// <returns>Lista de pólizas activas</returns>
        public DataSet Get_Polizas()
        {
            try
            {
                return this.consultarProc("LisPolizasSICAM");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Raliza la importación de asegurados
        /// </summary>
        /// <returns></returns>
        public bool InsertAsegurados()
        {
            try
            {
                this.BeginTransaction();

                this.typeConnection = Connection.EnumConnections.ConnectionReembolsos;
                this.consultarProc("SICAM_IMPORT_ASEGURADOS");

                this.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Obtiene el listado de empresas existentes en SICAU y la relación de las empresas asociadas de SICAM
        /// </summary>
        /// <returns></returns>
        public DataSet ConsultSic_Empresa_SICAM()
        {
            DataSet dsList;
            try
            {
                this.typeConnection = Connection.EnumConnections.ConnectionReembolsos;
                dsList = this.consultarProc("SICAM_ObtenerEmpresas");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        public int DeleteAsociacion()
        {
            int bandera_elimiar;
            try
            {
                this.BeginTransaction();

                bandera_elimiar = Convert.ToInt32(this.Delete());

                this.CommitTransaction();

                return bandera_elimiar;
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                return 0;
            }
        }

        #endregion
    }
}


