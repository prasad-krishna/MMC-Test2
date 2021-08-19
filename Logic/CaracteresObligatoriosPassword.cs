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
    /// Obtiene los caracteres que pueden ser validos para formar una contraseña aleatoria
    /// </summary>
    public class CaracteresObligatoriosPassword : GeneralProcess
    {
        #region Propiedades
        public bool ObtenerCaracteresValidos { get; set; }
        #endregion
        #region Metodos
        /// <summary>
        /// Constructor
        /// </summary>
        public CaracteresObligatoriosPassword()
        {
        }
        /// <summary>
        /// Auto:Diego Montejano Avila
        /// Proyecto: Auditoria 2014
        /// Fecha: 2014/09/19
        /// Obtiene los caracteres obligatorios para generar una contraseña
        /// </summary>
        /// <returns></returns>
        public DataSet ConsultaCaracteresObligatoriosPassword()
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
            return dsCaracteresObligatoriosPassword;
        }

        #endregion

    }
}