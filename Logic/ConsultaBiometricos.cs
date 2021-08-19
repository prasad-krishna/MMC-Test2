using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para las acciones de las consulta de Biometricos
    /// </summary>
    /// <remarks>Autor: RAM* Ricardo Ayala</remarks>
    /// <remarks>Fecha de creación: 06/10/2015 </remarks>

    public class ConsultaBiometricos : GeneralProcess
    {
        #region atributos
        private string _NumeroEmpleado;
        private string _NombreEmpleado;
        private string _ApellidoPaternoEmpleado;
        private string _ApellidoMaternoEmpleado;
        private DateTime _FechaNacimiento;

        private int _ColesterolTotal;
        private int _ColesterolHDL;
        private int _ColesterolLDL;
        private int _Trigliceridos;
        private decimal _IndiceAterogenico;
        private int _Glucemia;

        #endregion

        public string NumeroEmpleado
        {
            get { return _NumeroEmpleado; }
            set { _NumeroEmpleado = value; }
        }
        public string NombreEmpleado
        {
            get { return _NombreEmpleado; }
            set { _NombreEmpleado = value; }
        }
        public string ApellidoPaternoEmpleado
        {
            get { return _ApellidoPaternoEmpleado; }
            set { _ApellidoPaternoEmpleado = value; }
        }
        public string ApellidoMaternoEmpleado
        {
            get { return _ApellidoMaternoEmpleado; }
            set { _ApellidoMaternoEmpleado = value; }
        }
        public DateTime FechaNacimiento
        {
            get { return _FechaNacimiento; }
            set { _FechaNacimiento = value; }
        }

        public int ColesterolTotal
        {
            get { return _ColesterolTotal; }
            set { _ColesterolTotal = value; }
        }
        public int ColesterolHDL
        {
            get { return _ColesterolHDL; }
            set { _ColesterolHDL = value; }
        }
        public int ColesterolLDL
        {
            get { return _ColesterolLDL; }
            set { _ColesterolLDL = value; }
        }
        public int Trigliceridos
        {
            get { return _Trigliceridos; }
            set { _Trigliceridos = value; }
        }
        public decimal IndiceAterogenico
        {
            get { return _IndiceAterogenico; }
            set { _IndiceAterogenico = value; }
        }
        public int Glucemia
        {
            get { return _Glucemia; }
            set { _Glucemia = value; }
        }

        /// <summary>
        /// Descripción: Método para obtener los datos de la consulta anterior realizada al empleado
        /// Autor: RAM
        /// Fecha: 21/09/2015
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta Historial</returns>

        public DataSet InsertarBiometricos(int idEmpresa, int IdUsuario, string clave)
        {
            DataSet dsList;
            try
            {
                dsList = this.consultarProc("InsertPruebasBiometricas", idEmpresa, IdUsuario, clave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        /// <summary>
        /// Descripción: Método para obtener los datos de la consulta anterior realizada al empleado
        /// Autor: RAM
        /// Fecha: 21/09/2015
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta Historial</returns>

        public void InsertarBitacoraCargaExcel(int idEmpresa, int IdUsuario)
        {
            try
            {
                this.consultarProc("InsertBitacoraCargaExcel", idEmpresa, IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ConsultaUltimaCarga(int idEmpresa)
        {
            DataSet dsList;
            try
            {
                dsList = this.consultarProc("ConsultaBitacoraCargaExcel", idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        /// <summary>
        /// Descripción: Valida si el empleado no pertenece a la empresa
        /// Autor: RAM
        /// Fecha: 11/11/2015
        /// </summary>
        /// <returns>DataSet con las numeros de empleados que no pertenecen a la empresa.</returns>

        public DataSet ValidaNumeroEmpleadoBiometricos(int idEmpresa, string numeroEmpleados)
        {
            DataSet dsList;
            try
            {
                dsList = this.consultarProc("ValidaNumeroEmpleadoBiometricos", idEmpresa, numeroEmpleados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }
    }
}
