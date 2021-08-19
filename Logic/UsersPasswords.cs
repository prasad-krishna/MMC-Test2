using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;
using System.Collections;
using System.Collections.Generic;
namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Autor:Diego Montejano Avila
    /// Fecha:19/09/2014
    /// Proyecto:Auditoria 2014
    /// Clase para validar los ultimos 8 passwords
    /// </summary>
    public class UsersPasswords : GeneralProcess
    {
        #region Propiedades
        /// <summary>
        /// Id del usuario
        /// </summary>
        public int IdUser { get; set; }
        #endregion
        #region Metodos
        /// <summary>
        /// Constructor
        /// </summary>
        public UsersPasswords()
        {
        }
        public bool ValidaUltimosPasswords(string password)
        {
            try
            {
                DataSet dsPasswords = new DataSet();
                dsPasswords = ConsultaUltimosPasswords();
                foreach (DataRow drPsw in dsPasswords.Tables[0].Rows)
                {
                    if (Security.VerifyHash(password, drPsw[0].ToString()))
                        return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
        /// <summary>
        /// Autor:Diego Montejano Avila
        /// Fecha:19/09/2014
        /// Proyecto:Auditoria 2014
        /// Obtiene los últimos 8 passwords para validar que la nueva contraseña no se encuentre en estos.
        /// </summary>
        /// <returns>Dataset con los parametros</returns>
        private DataSet ConsultaUltimosPasswords()
        {
            DataSet dsPasswords = new DataSet();
            try
            {
                dsPasswords = this.List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsPasswords;
        }

        #endregion

    }
}