using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para administrar una opcion que tiene pregunta, respuesta y existe 
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: </remarks>
    public class ConsultaOpcion : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, </summary>
        private long _IdConsulta;
        /// <summary>Atributo, Pregunta a la que pertenece la respuesta</summary>
        private int _IdPreguntaRespuestaPadre;
        /// <summary>Atributo, Respuesta de la pregunta</summary>
        private int _IdPreguntaRespuesta;

        #endregion

        #region Properties

        /// <summary>Propiedad, </summary>
        public long IdConsulta
        {
            get { return _IdConsulta; }
            set { _IdConsulta = value; }
        }
        /// <summary>Propiedad, Pregunta a la que pertenece la respuesta</summary>
        public int IdPreguntaRespuestaPadre
        {
            get { return _IdPreguntaRespuestaPadre; }
            set { _IdPreguntaRespuestaPadre = value; }
        }
        /// <summary>Propiedad, Respuesta de la pregunta</summary>
        public int IdPreguntaRespuesta
        {
            get { return _IdPreguntaRespuesta; }
            set { _IdPreguntaRespuesta = value; }
        }


        #endregion

        #region Methods

        public ConsultaOpcion()
        {
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultConsultaOpcion()
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
        public int InsertConsultaOpcion()
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
        public void UpdateConsultaOpcion()
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
        public void DeleteConsultaOpcion()
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
        public void GetConsultaOpcion()
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
        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultConsultaOpcionPadre()
        {
            DataSet dsList;
            try
            {
                dsList = this.List("ConsultaOpcionPadre");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }



        /// <summary>
        /// Proyecto: Wellness
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Obtiene los subProgramas que se eligieron en SIAU para el asegurado
        /// </summary>
        /// <returns></returns>
        public DataSet ConsultConsultaSeleccionWellness()
        {
            DataSet dsList;
            try
            {
                dsList = this.List("ConsultaSeleccionWellness");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }


        /// <summary>
        /// Proyecto: Wellness
        /// Autor: Marco A. Herrera Gabriel
        /// Funcionalidad: Obtiene los subProgramas que se eligieron en SIAU para el asegurado
        /// </summary>
        /// <returns></returns>
        public DataSet ConsultConsultaRecomendacionesWellness()
        {
            DataSet dsList;
            try
            {
                dsList = this.List("ConsultaRecomendacionesWellness");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }



        #endregion


    }
}
