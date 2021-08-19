/*
HC, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer LLC.
HC is proprietary to Mercer LLC trade secret information. The
documentation and all related HC materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer LLC.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Mercer.Medicines.DataAccess;
using System.Data;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Proyecto: AMEX
    /// Requerimiento: Permisos de usuarios por empresa
    /// Funcionalidad: Clase que permite realizar las operaciones de ABC de la tabla EmpresaUsers
    /// Autor: Marco A. Herrera Gabriel
    /// Fecha: 09/07/2010                     
    /// </summary>
    public class EmpresaUsers : GeneralProcess
    {

        #region Atributos

        private int intEmpresa_id;
        private int intIdUser;
        private int intUsuarioCambio;
        private DateTime dtmUltimoCambio;
        private bool bolHabilitada;
        private string strIP;

        #endregion

        #region Propiedades

        public int Empresa_id
        {
            get { return this.intEmpresa_id; }
            set { this.intEmpresa_id = value; }
        }

        public int IdUser
        {
            get { return this.intIdUser; }
            set { this.intIdUser = value; }
        }

        public int UsuarioCambio
        {
            get { return this.intUsuarioCambio; }
            set { this.intUsuarioCambio = value; }
        }

        public DateTime UltimoCambio
        {
            get { return this.dtmUltimoCambio; }
        }

        public bool Habilitada
        {
            get { return this.bolHabilitada; }
            set { this.bolHabilitada = value; }
        }

        public string IP
        {
            get { return this.strIP;}
            set { this.strIP = value;}
        }

        #endregion

        /// <summary>
        /// Proyecto: AMEX
        /// Requerimiento: Permisos de usuarios por empresa
        /// Constructor por defecto
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 09/07/2010                     
        /// </summary>
        public EmpresaUsers()
        {

        }

        /// <summary>
        /// Proyecto: AMEX
        /// Requerimiento: Permisos de usuarios por empresa
        /// Constructor que inicializa al objeto
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 09/07/2010                     
        /// </summary>
        public EmpresaUsers(int intEmpresa_id, int intIdUser, int intUsuarioCambio)
        {
            this.intEmpresa_id = intEmpresa_id;
            this.intIdUser = intIdUser;
            this.intUsuarioCambio = intUsuarioCambio;
        }


        /// <summary>
        /// Proyecto: AMEX
        /// Requerimiento: Permisos de usuarios por empresa
        /// Funcionalidad: Inserta las empresas en las que tiene permisos un usuario
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 09/07/2010                     
        /// </summary>
        public bool InsertEmpresaUsers()
        {
            try
            {                
                this.Insert();                
            }
            catch (Exception ex)
            {                
                throw ex;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Proyecto: AMEX
        /// Requerimiento: Permisos de usuarios por empresa
        /// Funcionalidad: Obtiene los permisos que tiene un usuario
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 12/07/2010                     
        /// </summary>
        public DataSet ListEmpresaUsers()
        {
            try
            {
                return this.List();
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }


        /// <summary>
        /// Proyecto: AMEX
        /// Requerimiento: Permisos de usuarios por empresa
        /// Funcionalidad: Obtiene las empresas que tiene un usuario
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 12/07/2010                     
        /// </summary>
        public DataSet GetEmpresasUser()
        {
            try
            {
                return this.List("EmpresasUser");
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }


        /// <summary>
        /// Método, lista los corporativos configurados
        /// </summary>
        /// <returns></returns>
        public DataSet ListCorporativosUser()
        {
            try
            {
                return this.List("CorporativosUser");
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        /// <summary>
        /// Funcionalidad: Obtiene las sedes que tiene una empresa
        /// Autor: Ricardo silva
        /// Fecha: 16/05/2012                     
        /// </summary>
        public DataSet GetSedeEmpresa()
        {
            try
            {
                return this.List("SedesEmpresasAsignadas");
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }


        /// <summary>
        /// Funcionalidad: Obtiene las sedes que tiene una empresa
        /// Autor: Ricardo silva
        /// Fecha: 16/05/2012                     
        /// </summary>
        public DataSet GetSedeEmpresa(string empresas)
        {
            try
            {
                return this.List("SedesEmpresasAsignadas", empresas);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        /// <summary>
        /// Funcionalidad: Obtiene las sedes que tiene una empresa
        /// Autor: Ricardo silva
        /// Fecha: 16/05/2012                     
        /// </summary>
        public DataSet GetEmpresasCorporativo(string corporativos)
        {
            try
            {
                return this.List("EmpresasCorporativos", corporativos);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }


        /// <summary>
        /// Funcionalidad: Obtiene los usuarios de una sede
        /// Autor: Ricardo silva
        /// Fecha: 16/05/2012                     
        /// </summary>
        public DataSet GetSedeUsuarios()
        {
            try
            {
                return this.List("SedesUsuarios");
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        /// <summary>
        /// Funcionalidad: Obtiene los usuarios de una sede
        /// Autor: Ricardo silva
        /// Fecha: 16/05/2012                     
        /// </summary>
        public DataSet GetSedeUsuarios(string empresas)
        {
            try
            {
                return this.List("SedesUsuarios", empresas);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        /// <summary>
        /// Funcionalidad: Obtiene los medicos
        /// Autor: Ricardo silva
        /// Fecha: 16/05/2012                     
        /// </summary>
        public DataSet GetMedicos()
        {
            try
            {
                return this.List("MedicosFiltro");
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        /// <summary>
        /// Funcionalidad: Obtiene los medicos
        /// Autor: Ricardo silva
        /// Fecha: 16/05/2012                     
        /// </summary>
        public DataSet GetMedicos(string empresas )
        {
            try
            {
                return this.List("MedicosFiltro", empresas);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }


    }
}
