using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;
using System.Collections;
using System.Collections.Generic;
namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Auto:Diego Montejano Avila
    /// Proyecto: Auditoria 2014
    /// Fecha: 2014/09/19
    /// Obtiene el disclaimer para mostrar despues del login
    /// </summary>
    public class Dislclaimer : GeneralProcess
    {
        #region Metodos
        /// <summary>
        /// Constructor
        /// </summary>
        public Dislclaimer()
        {
        }
        /// <summary>
        /// Auto:Diego Montejano Avila
        /// Proyecto: Auditoria 2014
        /// Fecha: 2014/09/19
        /// Obtiene la información para mostrar en el disclaimer
        /// </summary>
        /// <returns>string con información del disclaimer</returns>
        public string ConsultaDisclaimer()
        {
            DataSet dsCaracteresObligatoriosPassword = new DataSet();
            try
            {
                dsCaracteresObligatoriosPassword = this.List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCaracteresObligatoriosPassword.Tables[0].Rows[0][0].ToString();
        }

        #endregion

    }
}