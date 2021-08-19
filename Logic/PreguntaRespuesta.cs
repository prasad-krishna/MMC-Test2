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
    /// Esta clase provee la funcionalidad para
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: </remarks>
    public class PreguntaRespuesta : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, </summary>
        private int _IdPreguntaRespuesta;
        /// <summary>Atributo, </summary>
        private string _Descripcion;
        /// <summary>Atributo, </summary>
        private bool _Activa;
        /// <summary>Atributo, </summary>
        private string _Seccion;
        /// <summary>Atributo, </summary>
        private string _DescripcionPregunta;
        /// <summary>Atributo, </summary>
        private int _Puntuacion;

        #endregion

        #region Properties

        /// <summary>Propiedad, </summary>
        public int IdPreguntaRespuesta
        {
            get { return _IdPreguntaRespuesta; }
            set { _IdPreguntaRespuesta = value; }
        }
        /// <summary>Propiedad, </summary>
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        /// <summary>Propiedad, </summary>
        public string DescripcionPregunta
        {
            get { return _DescripcionPregunta; }
            set { _DescripcionPregunta = value; }
        }
        /// <summary>Propiedad, </summary>
        public bool Activa
        {
            get { return _Activa; }
            set { _Activa = value; }
        }
        /// <summary>Propiedad, </summary>
        public int Puntuacion
        {
            get { return _Puntuacion; }
            set { _Puntuacion = value; }
        }
        /// <summary>Propiedad, </summary>
        public string Seccion
        {
            get { return _Seccion; }
            set { _Seccion = value; }
        }


        #endregion

        #region Methods

        public PreguntaRespuesta()
        {
        }

        public PreguntaRespuesta(int IdPreguntaRespuesta, string Descripcion, bool Activa, string Seccion)
        {
            _IdPreguntaRespuesta = IdPreguntaRespuesta;
            _Descripcion = Descripcion;
            _Activa = Activa;
            _Seccion = Seccion;
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultPreguntaRespuesta()
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
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultPreguntaRespuestaBusqueda()
        {
            DataSet dsList;
            try
            {
                dsList = this.List("PreguntaRespuestaBusqueda");
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
        public int InsertPreguntaRespuesta()
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
        public void UpdatePreguntaRespuesta()
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
        public void DeletePreguntaRespuesta()
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
        public void GetPreguntaRespuesta()
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


