
using Mercer.Medicines.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Threading;
using System.Globalization;


namespace Mercer.Medicines.Logic
{

    /// <summary>
    /// Proyecto: TPA-SICAM
    /// Autor: Marco A. Herrera G.
    /// Fecha: 19/01/10
    /// Funcionalidad: Validaciones para campos
    /// </summary>
    public class Validaciones
    {
        #region Atributos

        

        #endregion

        #region Propiedades

        public static string _CaracteresEspeciales;

        #endregion


        /// <summary>
        /// Constructor por default
        /// </summary>
        public Validaciones()
        { 
        
        }

        public static bool ExistenCaracteresEspeciales(string strTexto)
        {
            #region Declaración de variables;

            Constante objConstante;

            #endregion

            #region Inicialización de variables

            objConstante = new Constante();
            objConstante.GetConstante(Constante.EnumConstantes._CaracteresEspecialesLogin);

            #endregion

            _CaracteresEspeciales = objConstante.ConValor;

            foreach (char ch in _CaracteresEspeciales)
            {
                if (strTexto.ToString().Trim().IndexOf(ch) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Proyecto: AMEX
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Compara dos fechas y obtiene la diferencia en horas
        /// Fecha: 23/02/2010
        /// </summary>
        /// <param name="dtmTiempoComparar">Fecha a comparar</param>
        /// <param name="dtmTiempoReferencia">Fecha de referencia, puede ser la fecha de hoy</param>
        /// <returns></returns>
        public static double ObtenerHoras(DateTime dtmTiempoComparar, DateTime dtmTiempoReferencia)
        {
            #region Declaración de variables

            IFormatProvider ICulture;
            TimeSpan tmsDiferencia;

            #endregion

            #region Inicialización de variables

            ICulture = Thread.CurrentThread.CurrentCulture;

            #endregion

            try
            {
                tmsDiferencia = dtmTiempoReferencia.Subtract(dtmTiempoComparar);

                return tmsDiferencia.TotalHours;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}