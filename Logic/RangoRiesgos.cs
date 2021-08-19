/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2008 by Mercer
'===============================================================================
*/

using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para administrar los rangos que se utilizan en el reporte de riesgos
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 10/05/2012</remarks>
    public class RangoRiesgos : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, </summary>
        private int _IdRangoRiesgos;
        /// <summary>Atributo, </summary>
        private string _RangoRiegosNombre;
        /// <summary>Atributo, </summary>
        private string _ReporteRiesgo;
        /// <summary>Atributo, </summary>
        private decimal _LimiteInferior;
        /// <summary>Atributo, </summary>
        private decimal _LimiteSuperior;
        /// <summary>Atributo, </summary>
        private decimal _Puntuacion;
        /// <summary>Atributo, </summary>
        private int _Orden;

        #endregion

        #region Properties

        /// <summary>Propiedad, </summary>
        public int IdRangoRiesgos
        {
            get { return _IdRangoRiesgos; }
            set { _IdRangoRiesgos = value; }
        }
        /// <summary>Propiedad, </summary>
        public string RangoRiegosNombre
        {
            get { return _RangoRiegosNombre; }
            set { _RangoRiegosNombre = value; }
        }
        /// <summary>Propiedad, </summary>
        public string ReporteRiesgo
        {
            get { return _ReporteRiesgo; }
            set { _ReporteRiesgo = value; }
        }
        /// <summary>Propiedad, </summary>
        public decimal LimiteInferior
        {
            get { return _LimiteInferior; }
            set { _LimiteInferior = value; }
        }
        /// <summary>Propiedad, </summary>
        public decimal LimiteSuperior
        {
            get { return _LimiteSuperior; }
            set { _LimiteSuperior = value; }
        }
        /// <summary>Propiedad, </summary>
        public decimal Puntuacion
        {
            get { return _Puntuacion; }
            set { _Puntuacion = value; }
        }
        /// <summary>Propiedad, </summary>
        public int Orden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }


        #endregion

        #region Methods

        public RangoRiesgos()
        {
        }

        public RangoRiesgos(int IdRangoRiesgos, string RangoRiegosNombre, string ReporteRiesgo, decimal LimiteInferior, decimal LimiteSuperior, decimal Puntuacion, int Orden)
        {
            _IdRangoRiesgos = IdRangoRiesgos;
            _RangoRiegosNombre = RangoRiegosNombre;
            _ReporteRiesgo = ReporteRiesgo;
            _LimiteInferior = LimiteInferior;
            _LimiteSuperior = LimiteSuperior;
            _Puntuacion = Puntuacion;
            _Orden = Orden;
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultRangoRiesgos()
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

        /// <summary>
        /// Método para la inserción
        /// </summary>
        /// <returns>Id insertado</returns>
        public int InsertRangoRiesgos()
        {
            int id;
            try
            {
                id = Convert.ToInt32(this.Insert());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        /// <summary>
        /// Método para la modificación
        /// </summary>
        public void UpdateRangoRiesgos()
        {
            try
            {
                this.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la eliminación
        /// </summary>
        public void DeleteRangoRiesgos()
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

        /// <summary>
        /// Método para la carga de un objeto de este tipo
        /// </summary>
        public void GetRangoRiesgos()
        {
            try
            {
                this.Consult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


    }
}
