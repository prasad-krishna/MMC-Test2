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
using System.Collections;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para las acciones de las consulta
    /// </summary>
    /// <remarks>Autor: Ricardo Silva</remarks>
    /// <remarks>Fecha de creación: 17/08/2012</remarks>
    public class ConsultaEstamosContigoGeneral : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, Id de la consulta</summary>
        private long _IdConsulta;
        /// <summary>Atributo, Id del tipo de de padecimiento que registra para cada consulta estamos contigo (Colesterol, Diabetes, hipertension, etc)</summary>
        private int _IdTipoPadecimiento;
        /// <summary>Atributo, Indica si marca visita al nutriologo</summary>
        private int _VisitaNutriologo;
        /// <summary>Atributo, Indica si marca que se apega a la dieta</summary>
        private int _ApegadoDieta;
        /// <summary>Atributo, Motivo por que no visita nutriologo</summary>
        private string _NoVisitaNutriologo;
        /// <summary>Atributo, Inidica si sigue indicaciones del médico </summary>
        private int _IndicacionesMedico;
        /// <summary>Atributo, Motivo por el que no cumple tratamiento</summary>
        private string _NoCumpleTratamientos;
        /// <summary>Atributo, Indica si tiene médico para el tratamiento</summary>
        private int _MedicoTratamiento;
        /// <summary>Atributo, Indica si ha tenido alguna complicación</summary>
        private int _Complicacion;
        /// <summary>Atributo, Texto para otra complicaciones</summary>
        private string _OtraComplicacion;
        /// <summary>Atributo, Texto para recomendaciones</summary>
        private string _Recomendaciones;
        /// <summary>Atributo, Fecha para la siguiente cita</summary>
        private DateTime _FechaSiguienteCita;
        /// <summary>Atributo, Arreglo que contiene las opciones multiples de respuestas</summary>
        private ArrayList _ConsultaOpcion;
        /// <summary>Atributo, Texto con medicamentos</summary>
        private string _Medicamentos;

        #endregion

        #region Properties

        /// <summary>Propiedad, Id de la consulta </summary>
        public long IdConsulta
        {
            get { return _IdConsulta; }
            set { _IdConsulta = value; }
        }
        /// <summary>Propiedad, Id del tipo de de padecimiento que registra para cada consulta estamos contigo (colesterol, diabetes, hipertension, etc)</summary>
        public int IdTipoPadecimiento
        {
            get { return _IdTipoPadecimiento; }
            set { _IdTipoPadecimiento = value; }
        }
        /// <summary>Propiedad, Indica si marca visita al nutriologo</summary>
        public int VisitaNutriologo
        {
            get { return _VisitaNutriologo; }
            set { _VisitaNutriologo = value; }
        }
        /// <summary>Propiedad, Indica si marca que se apega a la dieta</summary>
        public int ApegadoDieta
        {
            get { return _ApegadoDieta; }
            set { _ApegadoDieta = value; }
        }
        /// <summary>Propiedad, Motivo por que no visita nutriologo</summary>
        public string NoVisitaNutriologo
        {
            get { return _NoVisitaNutriologo; }
            set { _NoVisitaNutriologo = value; }
        }
        /// <summary>Propiedad, Inidica si sigue indicaciones del médico</summary>
        public int IndicacionesMedico
        {
            get { return _IndicacionesMedico; }
            set { _IndicacionesMedico = value; }
        }
        /// <summary>Propiedad, Motivo por el que no cumple tratamiento</summary>
        public string NoCumpleTratamientos
        {
            get { return _NoCumpleTratamientos; }
            set { _NoCumpleTratamientos = value; }
        }
        /// <summary>Propiedad, Indica si tiene médico para el tratamiento</summary>
        public int MedicoTratamiento
        {
            get { return _MedicoTratamiento; }
            set { _MedicoTratamiento = value; }
        }
        /// <summary>Propiedad, Indica si ha tenido alguna complicación</summary>
        public int Complicacion
        {
            get { return _Complicacion; }
            set { _Complicacion = value; }
        }
        /// <summary>Propiedad, Texto para otra complicaciones</summary>
        public string OtraComplicacion
        {
            get { return _OtraComplicacion; }
            set { _OtraComplicacion = value; }
        }
        /// <summary>Propiedad,  Texto para recomendaciones</summary>
        public string Recomendaciones
        {
            get { return _Recomendaciones; }
            set { _Recomendaciones = value; }
        }
        /// <summary>Propiedad, Fecha para la siguiente cita</summary>
        public DateTime FechaSiguienteCita
        {
            get { return _FechaSiguienteCita; }
            set { _FechaSiguienteCita = value; }
        }

        /// <summary>Propiedad, Arreglo que contiene las opciones multiples de respuestas</summary>
        public ArrayList ConsultaOpcion
        {
            get { return _ConsultaOpcion; }
            set { _ConsultaOpcion = value; }
        }
        /// <summary>Propiedad, Texto con medicamentos</summary>
        public string Medicamentos
        {
            get { return _Medicamentos; }
            set { _Medicamentos = value; }
        }


        #endregion

        #region Metodos

        /// <summary>
        /// Función que indica si existe la consulta en las tabla de estamos contigo generales
        /// </summary>
        /// <returns></returns>
        public bool ExisteConsultaEstamosContigoGenerales(long IdConsulta, int IdTipoPadecimiento)
        {
            bool bolExiste = false;
            DataSet dsList = new DataSet();
            try
            {
                dsList = this.List("ExisteEstamosContigoGenerales", IdConsulta, IdTipoPadecimiento);
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    bolExiste = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolExiste;
        }

         /// <summary>
        /// Método para la inserción del formulario de estamos contigo generales
        /// </summary>
        /// <returns>Id insertado</returns>
        public long InsertConsultaEstamosContigoGenerales()
        {
            try
            {
                this.BeginTransaction();


                this.Insert("ConsultaEstamosContigoGenerales");

                if (this.ConsultaOpcion != null)
                {
                    foreach (ConsultaOpcion objConsultaOpcion in this.ConsultaOpcion)
                    {
                        objConsultaOpcion.objTransaction = this.objTransaction;
                        objConsultaOpcion.IdConsulta = this.IdConsulta;
                        objConsultaOpcion.InsertConsultaOpcion();
                    }
                }	              

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }

            return this.IdConsulta;
        }


        /// <summary>
        /// Método para la carga de un objeto del formulario de Estamos Contigo
        /// </summary>
        public void GetConsultaEstamosContigoGenerales(long IdConsulta, int IdTipoPadecimiento)
        {
            try
            {
                this.Consult("ConsultaEstamosContigoGenerales",IdConsulta,IdTipoPadecimiento);
                //OPCIONES
                ConsultaOpcion objConsultaOpcion = new ConsultaOpcion();
                objConsultaOpcion.IdConsulta = this.IdConsulta;
                DataTable dt = objConsultaOpcion.ConsultConsultaOpcion().Tables[0];
                ArrayList arrConsultaOpcion = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    ConsultaOpcion objConsultaOpcionFila = new ConsultaOpcion();
                    objConsultaOpcionFila.IdConsulta = this.IdConsulta;
                    objConsultaOpcionFila.IdPreguntaRespuestaPadre = int.Parse(dr["IdPreguntaRespuestaPadre"].ToString());
                    objConsultaOpcionFila.IdPreguntaRespuesta = int.Parse(dr["IdPreguntaRespuesta"].ToString());
                    arrConsultaOpcion.Add(objConsultaOpcionFila);
                }

                this.ConsultaOpcion = arrConsultaOpcion;                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la modificación del formulario de estamos contigo generales
        /// </summary>
        public void UpdateConsultaEstamosContigoGenerales()
        {
            try
            {
                this.BeginTransaction();

                this.Update("ConsultaEstamosContigoGenerales");

                this.Delete("ConsultaOpcion", this.IdConsulta, "EstamosContigo");
                if (this.ConsultaOpcion != null)
                {
                    foreach (ConsultaOpcion objConsultaOpcion in this.ConsultaOpcion)
                    {
                        objConsultaOpcion.objTransaction = this.objTransaction;
                        objConsultaOpcion.IdConsulta = this.IdConsulta;
                        objConsultaOpcion.InsertConsultaOpcion();
                    }
                }
                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        #endregion

    }
}