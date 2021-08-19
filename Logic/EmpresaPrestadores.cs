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
    /// Funcionalidad: Clase que permite realizar las operaciones de ABC de la tabla EmpresaPrestadores
    /// Autor: Ricardo Jose Silva Gomez
    /// Fecha: 09/06/2011                     
    /// </summary>
    public class EmpresaPrestadores : GeneralProcess
    {
        #region Atributos

        private int empresa_id;
        private int idPrestador;
        private bool activo;
        private DateTime fechaRetiro; 
        private string personaAprobacionRetiro;
        private string personaAprobacionIngreso;
        private string motivoRetiro;        
        private DateTime fechaIngreso;
        private bool bolHabilitada;
        private string strIP;

        #endregion

        #region Propiedades


        public int Empresa_id
        {
            get { return this.empresa_id; }
            set { this.empresa_id = value; }
        }

        public int IdPrestador
        {
            get { return this.idPrestador; }
            set { this.idPrestador = value; }
        }

        public bool Activo
        {
            get { return this.activo; }
            set { this.activo = value; }
        }

        public DateTime FechaRetiro
        {
            get { return this.fechaRetiro; }
            set { this.fechaRetiro = value; }
        }


        public string PersonaAprobacionRetiro
        {
            get { return this.personaAprobacionRetiro; }
            set { this.personaAprobacionRetiro = value; }
        }

        public string PersonaAprobacionIngreso
        {
            get { return this.personaAprobacionIngreso; }
            set { this.personaAprobacionIngreso = value; }
        }


        public string MotivoRetiro
        {
            get { return this.motivoRetiro; }
            set { this.motivoRetiro = value; }
        }

        public DateTime FechaIngreso
        {
            get { return this.fechaIngreso; }
            set { this.fechaIngreso = value; }
        }


        public bool Habilitada
        {
            get { return this.bolHabilitada; }
            set { this.bolHabilitada = value; }
        }

        public string IP
        {
            get { return this.strIP; }
            set { this.strIP = value; }
        }

        #endregion

         /// <summary>
        /// Proyecto: AMEX
        /// Constructor por defecto
        /// Autor: Ricardo Jose Silva Gomez
        /// Fecha: 09/06/2011                              
        /// </summary>
        public EmpresaPrestadores()
        {

        }


        /// <summary>
        /// Método insertar el prestador en la empresa
        /// </summary>
        public void InsertPrestadoresEmpresa()
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
        /// Método para eliminar el prestador de la empresa
        /// </summary>
        public void DeletePrestadoresEmpresa()
        {
            try
            {
                this.Delete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
