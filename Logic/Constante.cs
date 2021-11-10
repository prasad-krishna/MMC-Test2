using Mercer.Medicines.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;


namespace Mercer.Medicines.Logic
{

    /// <summary>
    /// Proyecto: TPA-SICAM
    /// Autor: Marco A. Herrera G.
    /// Fecha: 19/01/10
    /// Funcionalidad: Provee al sistema de las constantes que existen en este.
    /// </summary> 
    public class Constante : GeneralProcess
    {
        #region Atributos

        /// <summary>
        /// Atributo, Clave de la constante
        /// </summary>
        private int intConClave;

        /// <summary>
        /// Atributo, Nombre de la constante
        /// </summary>
        private string strConNombre;

        /// <summary>
        /// Atributo, Descripción de la constante
        /// </summary>
        private string strConDescripcion;

        /// <summary>
        /// Atributo, Valor de la constante
        /// </summary>
        private string strConValor;
        
         #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad, Obtiene o establece la clave de la constante
        /// </summary>
        public int ConClave
        {
            get { return intConClave; }
            set { intConClave = value; }
        }

        /// <summary>
        /// Propiedad, Obtiene o establece el nombre de la constante
        /// </summary>
        public string ConNombre
        {
            get { return strConNombre; }
            set { strConNombre = value; }
        }

        /// <summary>
        /// Propiedad, Obtiene o establece la descripción de la constante
        /// </summary>
        public string ConDescripcion
        {
            get { return strConDescripcion; }
            set { strConDescripcion = value; }
        }

        /// <summary>
        /// Propiedad, Obtiene o establece el valor de la constante
        /// </summary>
        public string ConValor
        {
            get { return strConValor; }
            set { strConValor = value; }
        }

        /// <summary>
        /// Propiedad, Constantes existentes en el sistema
        /// </summary>
        public enum EnumConstantes 
        {
            _DiasPasswordExpira, _CaracteresEspecialesLogin, _TimeOutFormsAuthenticationTicket, _RSA_AA_SECURITYKEY
        };

        /// <summary>
        /// Propiedad, Colección de constantes
        /// </summary>
        public List<Constante> lstConstantes;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor, Inicializa las constantes del sistema
        /// </summary>
        public Constante()
        {           
            this.GetConstantes();            
        }

        public Constante(int intConClave, string strConNombre, string strConDescripcion, string strConValor)
        {
            this.intConClave = intConClave;
            this.strConNombre = strConNombre;
            this.strConDescripcion = strConDescripcion;
            this.strConValor = strConValor;            
        }
        #endregion

        #region Métodos

        private void GetConstantes()
        {
         
            
            lstConstantes = new List<Constante>();
            DataTable dtConstantes;
            

            try
            {
                dtConstantes = List().Tables[0];

                foreach(DataRow row in dtConstantes.Rows)
                {
                    lstConstantes.Add(new Constante( this.intConClave = int.Parse(row["conClave"].ToString()), 
                         this.ConNombre = row["conNombre"].ToString(),
                         this.ConDescripcion = row["conDescripcion"].ToString(),
                         this.ConValor = row["conValor"].ToString()));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void GetConstante(EnumConstantes enmConstante)
        {
            foreach (Constante objConstante in lstConstantes)
            {
                if (objConstante.ConNombre == enmConstante.ToString())
                {
                    this.intConClave = objConstante.ConClave;
                    this.ConNombre = objConstante.ConNombre;
                    this.ConDescripcion = objConstante.ConDescripcion;
                    this.ConValor = objConstante.ConValor;

                    return;
                }
                
            }
        }

        #endregion

    }
}