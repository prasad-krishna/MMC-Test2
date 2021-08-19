/*
'===============================================================================
Delima Mercer (Colombia) Ltda, Sistema Autorizaciones y Reembolsos
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Delima Mercer (Colombia) Ltda.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Delima Mercer (Colombia) Ltda

Copyright (c) 2010 by Delima Mercer (Colombia) Ltda
'===============================================================================
*/

using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para administrar las acciones realizadas en el sistema
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 6 de octubre de 2008</remarks>
    public class Log : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, Id del log</summary>
        private long _IdLog;
        /// <summary>Atributo, Id de la acción generada</summary>
        private EnumActionsLog _IdActionLog;
        /// <summary>Atributo, Id de usuario que realiza la acción</summary>
        private int _IdUser;
        /// <summary>Atributo, Id de usuario del sistema SICAU que realiza la acción</summary>
        private int _usuario_id;
        /// <summary>Atributo, Id principal afectado en la acción</summary>
        private long _MainId;
        /// <summary>Atributo, Fecha y hora de la acción</summary>
        private DateTime _DateLog;
        /// <summary>Atributo, Dirección IP desde donde se realiza la acción</summary>
        private string _IP;
        /// <summary>Atributo, Detalle de la acción realizada</summary>
        private string _Detail;

        #endregion

        #region Properties

        /// <summary>Propiedad, Id del log</summary>
        public long IdLog
        {
            get { return _IdLog; }
            set { _IdLog = value; }
        }
        /// <summary>Propiedad, Id de la acción generada</summary>
        public EnumActionsLog IdActionLog
        {
            get { return _IdActionLog; }
            set { _IdActionLog = value; }
        }
        /// <summary>Propiedad, Id de usuario que realiza la acción </summary>
        public int IdUser
        {
            get { return _IdUser; }
            set { _IdUser = value; }
        }
        /// <summary>Propiedad, Id de usuario del sistema SICAU que realiza la acción </summary>
        public int usuario_id
        {
            get { return _usuario_id; }
            set { _usuario_id = value; }
        }
        /// <summary>Propiedad, Id principal afectado en la acción</summary>
        public long MainId
        {
            get { return _MainId; }
            set { _MainId = value; }
        }
        /// <summary>Propiedad, Fecha y hora de la acción</summary>
        public DateTime DateLog
        {
            get { return _DateLog; }
            set { _DateLog = value; }
        }
        /// <summary>Propiedad,  Dirección IP desde donde se realiza la acción</summary>
        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }
        /// <summary>Propiedad, Detalle de la acción realizada</summary>
        public string Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }


        #endregion

        #region Enumeration

        /// <summary>
        /// Enumeración, lista las acciones de registro en el log del sistema
        /// </summary>
        public enum EnumActionsLog
        {
            IngresarSistema = 1,
            CambiarContraseña = 2,
            InsertarUsuario = 3,
            ModificarUsuario = 4,
            IngresarPermiso = 5,
            EliminarPermiso = 6,
            InsertarMedicamento = 7,
            ModificarMedicamento = 8,
            IngresarSolictudAutorizacion = 9,
            IngresarSolictudReembolso = 10,
            ModificarSolictudAutorizacion = 11,
            ModificarSolictudReembolso = 12,
            EliminarSolicitudTipoServicio = 13,
            EliminarServicioSolicitud = 14,
            ModificarEstadoSolicitud = 15,
            IngresarDesdeSICAU = 16,
            ModificarEstadoTipoServicioSolicitud = 17,
            InsertarServicio = 50,
            ModificarServicio = 51,
            BorrarServicio = 52,
            InsertarProveedor = 53,
            ModificarProveedor = 54,
            BorrarProveedor = 55,
            InsertarPresupuestoIndividuo = 56,
            ModificarPresupuestoIndividuo = 57,
            BorrarPresupuestoIndividuo = 58,
            InsertarPresupuestoEmpresa = 59,
            ModificarPresupuestoEmpresa = 60,
            BorrarPresupuestoEmpresa = 61,
            IngresarConsulta = 62,
            ModificarConsulta = 63,
            IngresarSolicitudOrden = 64,
            ModificarSolicitudOrden = 65,
            InsertarPrestador = 66,
            ModificarPrestador = 67,
            BorrarPrestador = 68,
            ReversarFactura = 69,
            IngresarConsultaEstiloVida = 70,
            ModificarConsultaEstiloVida = 71,
            IngresarConsultaNutricion = 72,
            ModificarConsultaNutricion = 73,
            AgregarEmpresa = 75,
            ModificarEmpresa = 76,
            AsociarEmpresa = 77,
            IngresarRango = 78,
            ModificarRango = 79,
            EliminarRango = 80,
            ModificarPreguntaRespuesta = 81,
            IngresarConsultaEstamosContigo = 82,
            ModificarConsultaEstamosContigo = 83,
            AgregarLineaNegocio = 84,
            ModificarLineaNegocio = 85,
            Reseteodecontrasena = 86,
            EnvioCorreoUsuario = 87,
            EnvioCorreoContrasena = 88

        }

        #endregion

        #region Methods

        public Log()
        {
        }


        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultLog(object dateFrom, object dateUntil)
        {
            DataSet dsList;

            try
            {
                dsList = this.List(this.IdActionLog, this.IdUser, dateFrom, dateUntil, this.usuario_id);
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
        public int InsertLog()
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

        #endregion


    }
}


